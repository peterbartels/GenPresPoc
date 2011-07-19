using System;
using System.Data.Linq;
using GenPres.Business.Data.DataAccess;
using GenPres.Business.Domain.PatientDomain;
using GenPres.DataAccess.Repositories;

namespace GenPres.DataAccess.DataMapper.Mapper.Patient
{
    public class PatientMapper : DataMapper<IPatient, Database.Patient>
    {
        public PatientMapper()
            : base(new GenPresDataContextFactory())
        {
        }

        public PatientMapper(IDataContextFactory context)
            : base(context)
        {
            
        }

        public override Database.Patient MapFromBoToDao(IPatient patientBo, Database.Patient patientDao)
        {
            patientDao.Id = (int)Repository<IPatient, Database.Patient>.GetIdValue(patientDao, _contextFactory.Context);
            patientDao.PID = patientBo.PID;
            return patientDao;
        }

        public override IPatient MapFromDaoToBo(Database.Patient patientDao, IPatient patientBo)
        {
            patientBo.PID = patientDao.PID;
            return patientBo;
            
        }

        public DataContext Context
        {
            get { return _contextFactory.Context; }
        }
    }
}
