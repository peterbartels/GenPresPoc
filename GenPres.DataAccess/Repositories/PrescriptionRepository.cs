using System;
using System.Linq;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.Domain;
using GenPres.Business.Domain.Patient;
using GenPres.Business.Domain.PrescriptionDomain;
using GenPres.DataAccess.DataMapper.Mapper.PrescriptionMapper;
using DB = GenPres.Database;
using Patient = GenPres.Database.Patient;

namespace GenPres.DataAccess.Repositories
{
    public class PrescriptionRepository : Repository<Prescription, Database.Prescription>, IPrescriptionRepository
    {
        private readonly PrescriptionMapper _prescriptionMapper;

        public PrescriptionRepository()
            : base(new GenPresDataContextFactory())
        {
            _prescriptionMapper = new PrescriptionMapper(_dataContextFactory);
        }

        internal PrescriptionRepository(IDataContextFactory context)
            : base(context)
        {
            _prescriptionMapper = new PrescriptionMapper(_dataContextFactory);
        }

        public Prescription[] GetPrescriptions(string patientId)
        {
            IQueryable<Database.Prescription> prescriptionDaos;

            using(var ctx = GenPresDataManager.GetManager())
            {
                prescriptionDaos = (from pres in ctx.GetContext().Prescriptions
                                    join pat in ctx.GetContext().Patients on pres.Patient equals pat
                                    where pat.PID == (patientId) && (pres.StartDate <= DateTime.Now && (pres.EndDate >= DateTime.Now || pres.EndDate == null))
                                    select pres);
            

                var prescriptions = new Prescription[prescriptionDaos.Count()];

                for (var i = 0; i < prescriptionDaos.Count(); i++)
                {
                    prescriptions[i] = _prescriptionMapper.MapFromDaoToBo(prescriptionDaos.ToArray()[i], NewExistingBo<Prescription>());
                }
                return prescriptions;
            }
        }

        public Prescription GetPrescriptionById(int id)
        {
            return _prescriptionMapper.MapFromDaoToBo(GetById(id), NewExistingBo<Prescription>());
        }

        public void SavePrescription(Prescription prescription, string patientId)
        {
            Database.Prescription prDao;
            if(prescription.Id == 0)
            {
                var pr = new PatientRepository(_dataContextFactory);
                Patient patient = pr.FindOrCreatePatient(patientId);
                prDao = NewDao();
                patient.Prescriptions.Add(prDao);    
            }else
            {
                prDao = GetById(prescription.Id);
;           }
            
            _prescriptionMapper.MapFromBoToDao(prescription, prDao);
            Submit();
        }

        public override IDataMapper<Prescription, Database.Prescription> Mapper
        {
            get { return _prescriptionMapper; }
        }
    }
}
