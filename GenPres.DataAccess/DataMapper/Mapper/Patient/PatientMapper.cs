using GenPres.Business.Data.DataAccess;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Business.Domain;
using GenPres.Business.Domain.Patient;

namespace GenPres.DataAccess.DataMapper.Mapper.Patient
{
    public class PatientMapper : IDataMapper<IPatient, Database.Patient>
    {
        public Database.Patient MapFromBoToDao(IPatient patient, Database.Patient patientDao)
        {
            patientDao.PID = patient.PID;
            return patientDao;
        }

        public IPatient MapFromDaoToBo(Database.Patient patientDao, IPatient patient)
        {
            patient.PID = patientDao.PID;
            return patient;
        }
    }
}
