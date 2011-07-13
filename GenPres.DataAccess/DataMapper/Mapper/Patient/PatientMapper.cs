using System;
using System.Data.Linq;
using GenPres.Business.Data.DataAccess;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Business.Domain;
using GenPres.Business.Domain.Patient;
using GenPres.DataAccess.Repositories;

namespace GenPres.DataAccess.DataMapper.Mapper.Patient
{
    public class PatientMapper : IDataMapper<IPatient, Database.Patient>
    {
        private DataContext _context;

        public PatientMapper(DataContext context)
        {
            _context = context;
        }
        public Database.Patient MapFromBoToDao(IPatient patient, Database.Patient patientDao)
        {
            patientDao.PID = patient.PID;
            return patientDao;
        }

        public IPatient MapFromDaoToBo(Database.Patient patientDao, IPatient patient)
        {
            patient.PID = patientDao.PID;
            //patient.Id = (int) Repository<IPatient, Database.Patient>.GetIdValue(patientDao);

            return patient;
        }

        public DataContext Context
        {
            get { return _context; }
        }
    }
}
