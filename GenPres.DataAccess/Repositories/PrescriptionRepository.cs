using System;
using System.Linq;
using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.Domain.Prescription;
using GenPres.DataAccess.DataMapper.Mapper.Prescription;
using DB = GenPres.Database;

namespace GenPres.DataAccess.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly PrescriptionMapper _prescriptionMapper = new PrescriptionMapper();

        public IPrescription[] GetPrescriptions()
        {
            IQueryable<Database.Prescription> prescriptionDaos;

            using(var ctx = GenPresDataManager.GetManager())
            {
                prescriptionDaos = from pres in ctx.GetContext().Prescriptions
                                       /*where pres.EndDate < DateTime.Now */
                                       select pres;
            

                var prescriptions = new Prescription[prescriptionDaos.Count()];

                for (var i = 0; i < prescriptionDaos.Count(); i++)
                {
                    prescriptions[i] = _prescriptionMapper.MapDaoObjectToBusinessObject(prescriptionDaos.ToArray()[i]);
                }
                return prescriptions;
            }
            throw new Exception("Cannot do");
        }

        public void SavePrescription(IPrescription prescription)
        {
            _prescriptionMapper.MapBusinessObjectToDao(prescription);
        }
    }
}
