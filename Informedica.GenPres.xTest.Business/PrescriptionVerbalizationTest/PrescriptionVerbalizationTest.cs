﻿using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Business.Domain.Units;
using Informedica.GenPres.Business.Verbalization;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Business.PrescriptionVerbalizationTest
{
    [TestClass]
    public class PrescriptionVerbalizationTest : BaseGenPresTest
    {
        [TestMethod]
        public void ParacetamolRectNoOptions()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());

            prescription.Drug.Shape = "zetp";
            prescription.Drug.Route = "rect";

            prescription.Frequency.Value = 3;
            prescription.PatientLength = UnitConverter.GetBaseValue("kg", 10);
            prescription.FirstDose.Quantity.Value = 240;

            prescription.FirstDose.Total.Value = 72;
            prescription.FirstDose.Total.Adjust = "kg";
            prescription.FirstDose.Total.Time = "dag";

            prescription.Quantity.Value = 1;

            prescription.Total.Value = 21;
            prescription.Total.Time = "week";

            prescription.FirstSubstance.Quantity.Value = 240;
            
            var verbalization = PrescriptionVerbalization.Verbalize(prescription);
            //Assert.AreEqual(verbalization, "paracetamol 240 mg zetp rect 3 keer per dag 1 zetp = 240 mg, 21 zetp/week = 7200 mg/kg/dag");
        }
    }
}
