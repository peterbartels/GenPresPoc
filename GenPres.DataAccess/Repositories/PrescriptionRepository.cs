using System;
using System.Linq;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.ServiceProvider;
using GenPres.DataAccess.DataMapper.Mapper.PrescriptionMapper;
using GenPres.Database;
using DB = GenPres.Database;
using Patient = GenPres.Database.Patient;
using GenPres.Business.Domain.PrescriptionDomain;

namespace GenPres.DataAccess.Repositories
{
    public class PrescriptionRepository : Repository<IPrescription, Database.Prescription>, IPrescriptionRepository
    {
        private readonly PrescriptionMapper _prescriptionMapper;

        public PrescriptionRepository()
            : base(DalServiceProvider.Instance.Resolve<IDataContextManager>())
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

            using (var ctx = DalServiceProvider.Instance.Resolve<IDataContextManager>().Context)
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
