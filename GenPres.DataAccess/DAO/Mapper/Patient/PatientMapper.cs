using System.Data.Linq;
using GenPres.Business.Domain.Patients;
using GenPres.DataAccess.Managers;

namespace GenPres.DataAccess.DAO.Mapper.Patient
{
    public class PatientMapper : DataMapper<IPatient, Database.Patient>
    {
        public PatientMapper()
            : base(StructureMap.ObjectFactory.GetInstance<IDataContextManager>())
        {
        }

        public PatientMapper(IDataContextManager context)
            : base(context)
        {
            
        }

        public override Database.Patient MapFromBoToDao(IPatient patientBo, Database.Patient patientDao)
        {
            patientDao.PID = patientBo.PID;
            return patientDao;
        }

        public override IPatient MapFromDaoToBo(Database.Patient patientDao, IPatient patientBo)
        {
            patientBo.Id = patientDao.Id;
            patientBo.PID = patientDao.PID;
            return patientBo;
            
        }

        public DataContext Context
        {
            get { return ContextManager.Context; }
        }
    }
}
