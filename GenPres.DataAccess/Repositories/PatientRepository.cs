using System;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.Domain;
using GenPres.Business.Domain.Patient;
using GenPres.DataAccess.DataMapper.Mapper.Patient;

namespace GenPres.DataAccess.Repositories
{
    public class PatientRepository : Repository<Database.Patient, IPatient>, IPatientRepository
    {
        public PatientRepository()
            : base(new GenPresDataContextFactory())
        {
            
        }

        public IPatient GetByPid(string pid)
        {
            var patientDao = FindSingle(x => x.PID == pid);
            
            Patient newPatient = ObjectFactory<Patient>.Create(!patientDao.IsAvailable);

            if(patientDao.IsAvailable)
            {
                //_patientMapper.MapFromDaoToBo(patientDao.Object, newPatient);
            }else
            {
                newPatient.PID = pid;
            }

            return newPatient;
        }

        public override IDataMapper<IPatient, Database.Patient> Mapper
        {
            get { return new PatientMapper(); }
        }
    }
}
