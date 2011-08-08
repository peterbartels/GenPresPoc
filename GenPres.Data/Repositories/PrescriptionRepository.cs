using System;
using System.Linq;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Business.Data.IRepositories;
using GenPres.Data.DAO.Mapper.PrescriptionMapper;
using GenPres.Data.Managers;
using GenPres.Database;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Data.Repositories
{
    public class PrescriptionRepository : Repository<IPrescription, Database.Prescription>, IPrescriptionRepository
    {
        private readonly PrescriptionMapper _prescriptionMapper;

        public PrescriptionRepository()
            : base(StructureMap.ObjectFactory.GetInstance<IDataContextManager>())
        {
            _prescriptionMapper = new PrescriptionMapper(DataContextManager);
        }

        internal PrescriptionRepository(IDataContextManager context)
            : base(context)
        {
            _prescriptionMapper = new PrescriptionMapper(DataContextManager);
        }

        public IPrescription[] GetPrescriptions(string patientId)
        {
            IQueryable<Database.Prescription> prescriptionDaos;

            using (var ctx = StructureMap.ObjectFactory.GetInstance<IDataContextManager>().Context)
            {
                var pContext = (PrescriptionDataContext) ctx;

                prescriptionDaos = (from pres in pContext.Prescriptions
                                    join pat in pContext.Patients on pres.Patient equals pat
                                    where pat.PID == (patientId) && (pres.StartDate <= DateTime.Now && (pres.EndDate >= DateTime.Now || pres.EndDate == null))
                                    select pres);


                var prescriptions = new IPrescription[prescriptionDaos.Count()];

                for (var i = 0; i < prescriptionDaos.Count(); i++)
                {
                    prescriptions[i] = _prescriptionMapper.MapFromDaoToBo(prescriptionDaos.ToArray()[i], NewExistingBo<IPrescription>());
                }
                return prescriptions;
            }
        }

        public IPrescription GetPrescriptionById(int id)
        {
            return _prescriptionMapper.MapFromDaoToBo(GetById(id), NewExistingBo<IPrescription>());
        }

        public void SavePrescription(IPrescription prescription, string patientId)
        {
            Database.Prescription prDao;
            if(prescription.Id == 0)
            {
                var pr = new PatientRepository(DataContextManager);
                Patient patient = pr.FindOrCreatePatient(patientId);
                prDao = NewDao();
                patient.Prescriptions.Add(prDao);
                
            }else
            {
                prDao = GetById(prescription.Id);
;           }
            
            _prescriptionMapper.MapFromBoToDao(prescription, prDao);
            _identifiersAfterSubmit.Add(prDao, prescription);
            Submit();
        }

        public override IDataMapper<IPrescription, Database.Prescription> Mapper
        {
            get { return _prescriptionMapper; }
        }
    }
}
