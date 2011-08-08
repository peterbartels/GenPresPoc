using System.Data.Linq;
using GenPres.Business.Domain.Patients;
using GenPres.Data.Managers;

namespace GenPres.Data.DAO.Mapper.Patient
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
            patientDao.PID = patientBo.Pid;
            return patientDao;
        }

        public override IPatient MapFromDaoToBo(Database.Patient patientDao, IPatient patientBo)
        {
            patientBo.Id = patientDao.Id;
            patientBo.Pid = patientDao.PID;
            return patientBo;
            
        }

        public DataContext Context
        {
            get { return ContextManager.Context; }
        }
    }
}
