using System;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.Domain;
using GenPres.Business.Domain.Patient;
using GenPres.DataAccess.DataMapper.Mapper.Patient;
using GenPres.DataAccess.Object;

namespace GenPres.DataAccess.Repositories
{
    public class PatientRepository : Repository<IPatient, Database.Patient>, IPatientRepository
    {
        private readonly PatientMapper _patientMapper;
        
        public PatientRepository()
            : base(new GenPresDataContextFactory())
        {
            _patientMapper = new PatientMapper(_dataContextFactory);
        }

        internal PatientRepository(IDataContextFactory context)
            : base(context)
        {
            _patientMapper = new PatientMapper(_dataContextFactory);
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

        public IPatient GetByPid(string pid)
        {
            var patientDao = FindSingle(x => x.PID == pid);

            var newPatient = ObjectFactory.Create<Patient>(!patientDao.IsAvailable);

            if(patientDao.IsAvailable)
            {
                _patientMapper.MapFromDaoToBo(patientDao.Object, newPatient);
            }else
            {
                newPatient.PID = pid;
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
