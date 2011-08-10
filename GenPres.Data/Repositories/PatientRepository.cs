﻿using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain;
using GenPres.Business.Domain.Patients;
using GenPres.Data.DAO.Mapper.Patient;
using GenPres.Data.Managers;

namespace GenPres.Data.Repositories
{
    public class PatientRepository : Repository<IPatient, Database.Patient>, IPatientRepository
    {
        private readonly PatientMapper _patientMapper;
        
        public PatientRepository()
            : base(StructureMap.ObjectFactory.GetInstance<IDataContextManager>())
        {
            _patientMapper = new PatientMapper(DataContextManager);
        }

        internal PatientRepository(IDataContextManager context)
            : base(context)
        {
            _patientMapper = new PatientMapper(DataContextManager);
        }

        public Database.Patient FindOrCreatePatient(string patientId)
        {
            Database.Patient patient;
            var foundPatient = FindSingle(x => x.PID == patientId);
            if (!foundPatient.IsAvailable)
            {
                patient = NewDao();
                var pdmsRepos = new PdmsRepository();
                IPatient patientBo = pdmsRepos.GetPatientsByPatientId(patientId);
                _patientMapper.MapFromBoToDao(patientBo, patient);
            }
            else
            {
                patient = foundPatient.Object;
            }
            return patient;
        }

        public bool PatientExists(string patientId)
        {
            var foundPatient = FindSingle(x => x.PID == patientId);
            if (!foundPatient.IsAvailable) return false;
            return true;
        }

        public IPatient GetByPid(string pid)
        {
            var patientDao = FindSingle(x => x.PID == pid);

            var newPatient = ObjectCreator.Create<Patient>(!patientDao.IsAvailable);

            if(patientDao.IsAvailable)
            {
                _patientMapper.MapFromDaoToBo(patientDao.Object, newPatient);
            }else
            {
                newPatient.Pid = pid;
            }
            return newPatient;
        }

        public override IDataMapper<IPatient, Database.Patient> Mapper
        {
            get { return _patientMapper; }
        }

        readonly IdentityMap<IPatient, Database.Patient>[] _identityMaps = new[]
        {
            new IdentityMap<IPatient, Database.Patient>()                                                           
        };
    }
}