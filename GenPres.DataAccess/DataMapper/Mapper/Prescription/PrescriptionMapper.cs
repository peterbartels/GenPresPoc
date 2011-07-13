﻿using GenPres.Business.Domain.Prescription;
using Drug = GenPres.Database.Drug;

namespace GenPres.DataAccess.DataMapper.Mapper.Prescription
{
    public class PrescriptionMapper
    {
        public IPrescription MapBusinessObjectToDao(IPrescription prescriptionBo)
        {
            var prescriptionDao = new Database.Prescription();
            prescriptionDao.StartDate = prescriptionBo.StartDate;
            
            var drugDao = new Drug();
            drugDao.Name = prescriptionBo.Drug.Generic;
            drugDao.Route = prescriptionBo.Drug.Route;
            drugDao.Shape = prescriptionBo.Drug.Shape;

            prescriptionDao.Drug = drugDao;

            /*
            using(var ctx = GenPresDataManager.GetManager())
            {
                ctx.GetContext().Prescriptions.InsertOnSubmit(prescriptionDao);
                ctx.GetContext().SubmitChanges();
            }*/

            return prescriptionBo;
        }

        public GenPres.Business.Domain.Prescription.Prescription MapDaoObjectToBusinessObject(Database.Prescription prescriptionDao)
        {
            var prescriptionBo = Business.Domain.Prescription.Prescription.NewPrescription();

            if (prescriptionDao.StartDate != null) prescriptionBo.StartDate = prescriptionDao.StartDate.Value;

            var drugDao = prescriptionDao.Drug;

            prescriptionBo.Drug.Generic = drugDao.Name;
            prescriptionBo.Drug.Route = drugDao.Route;
            prescriptionBo.Drug.Shape = drugDao.Shape;

            return prescriptionBo;
        }
    }
}