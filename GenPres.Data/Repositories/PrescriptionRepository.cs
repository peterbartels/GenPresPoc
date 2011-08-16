using System;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Data.Repositories
{
    public class PrescriptionRepository : NHibernateRepository<Prescription, Guid>, IPrescriptionRepository
    {
        

        public PrescriptionRepository()
            : base(SessionManager.SessionFactory)
        {
            
        }


        public Prescription[] GetPrescriptions(string patientId)
        {
            //IQueryable<Database.Prescription> prescriptionDaos;
            /*
            using (var ctx = StructureMap.ObjectFactory.GetInstance<IDataContextManager>().Context)
            {
                
                prescriptionDaos = (from pres in pContext.Prescriptions
                                    join pat in pContext.Patients on pres.Patient equals pat
                                    where pat.PID == (patientId) && (pres.StartDate <= DateTime.Now && (pres.EndDate >= DateTime.Now || pres.EndDate == null))
                                    select pres);


                var prescriptions = new Prescription[prescriptionDaos.Count()];

                for (var i = 0; i < prescriptionDaos.Count(); i++)
                {
                    prescriptions[i] = _prescriptionMapper.MapFromDaoToBo(prescriptionDaos.ToArray()[i], NewExistingBo<Prescription>());
                }*/
            var prescriptions = new Prescription[0];
            return prescriptions;
            
        }

        public Prescription GetPrescriptionById(int id)
        {
            return null;// _prescriptionMapper.MapFromDaoToBo(GetById(id), NewExistingBo<Prescription>());
        }

        public void SavePrescription(Prescription prescription, string patientId)
        {
            /*Database.Prescription prDao;
            if(prescription.Id == 0)
            {
                var pr = new PatientRepository();
                Patient patient = pr.FindOrCreatePatient(patientId);
                prDao = NewDao();
                //patient.Prescriptions.Add(prDao);
                
            }else
            {
                prDao = GetById(prescription.Id);
;           }
            
            _prescriptionMapper.MapFromBoToDao(prescription, prDao);
            _identifiersAfterSubmit.Add(prDao, prescription);
            Submit();
             */
        }
    }
}
