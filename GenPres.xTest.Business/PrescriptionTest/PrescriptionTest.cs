using System;
using System.Linq;
using GenPres.Business.Domain.Units;
using GenPres.Data.DTO.Prescriptions;
using GenPres.Data.Repositories;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.xTest.Business.PrescriptionTest
{
    [TestClass]
    public class PrescriptionTest : BaseGenPresTest
    {   
        [TestMethod]
        public void PrescriptionCreatesCreationDate()
        {
            var p = Prescription.NewPrescription();
            if (p.CreationDate != null) Assert.AreEqual(DateTime.Now.ToString("dd-MM-yyyy HH:mm"), p.CreationDate.Value.ToString("dd-MM-yyyy HH:mm"));
        }


        [TestMethod]
        public void PrescriptionCreatesDrug()
        {
            var p = Prescription.NewPrescription();
            Assert.IsNotNull(p.Drug);
        }

        [TestMethod]
        public void PrescriptionCreatesComponent()
        {
            var p = Prescription.NewPrescription();
            Assert.IsTrue(p.Drug.Components.Count == 1);
        }

        [TestMethod]
        public void PrescriptionCreatesSubstance()
        {
            var p = Prescription.NewPrescription();
            Assert.IsTrue(p.Drug.Components[0].Substances.Count == 1);
        }
        
        [TestMethod]
        public void PrescriptionDtoCanMapToPrescriptionBo()
        {
            var pDto = new PrescriptionDto();
            pDto.startDate = DateTime.Parse("2011-07-01 12:00:00").ToString();
            Prescription p = PrescriptionAssembler.AssemblePrescriptionBo(pDto);
            Assert.AreEqual(p.StartDate, DateTime.Parse("2011-07-01 12:00:00"));
        }

        [TestMethod]
        public void PrescriptionBoCanMapToPrescriptionDto()
        {
            Prescription p = Prescription.NewPrescription();
            p.StartDate = DateTime.Parse("2011-07-01 12:00:00");
            PrescriptionDto pDto = PrescriptionAssembler.AssemblePrescriptionDto(p);
            Assert.AreEqual(pDto.startDate, "7/1/2011 12:00:00 PM");
        }

        [TestMethod]
        public void DrugShouldRetreiveSubstanceIncrementsWhenDrugGenericRouteShapeAreKnown()
        {
            Prescription p = Prescription.NewPrescription();
            p.Drug.Generic = "paracetamol";
            p.Drug.Route = "rect";
            p.Drug.Shape = "zetp";
            Assert.IsTrue(p.Drug.Components[0].Substances[0].SubstanceIncrements.Length > 0);
        }

        [TestMethod]
        public void DrugShouldRetreiveComponentIncrementWhenDrugGenericRouteShapeAreKnown()
        {
            Prescription p = Prescription.NewPrescription();
            p.Drug.Generic = "paracetamol";
            p.Drug.Route = "rect";
            p.Drug.Shape = "zetp";
            Assert.IsTrue(p.Drug.Components[0].ComponentIncrement != 0);
        }




        [TestMethod]
        public void PrescriptionCanSetPatientweight()
        {
            Prescription p = Prescription.NewPrescription();
            
            p.PatientWeight = UnitConverter.GetBaseValue("kg", 10);
            p.PatientWeightUnit = "kg";

            p.FirstDose.Quantity.Unit = "mg";
            p.FirstDose.Quantity.Value = 10000;
            p.FirstDose.Quantity.Adjust = "kg";
            
            Assert.AreEqual(p.FirstDose.Quantity.AdjustWeightValue, 10000);
            Assert.AreEqual(p.FirstDose.Quantity.Value, 10000);
            Assert.AreEqual(p.FirstDose.Quantity.BaseValue, 100);
        }

        [TestMethod]
        public void PrescriptionCanSetPatientLength()
        {
            Prescription p = Prescription.NewPrescription();
            p.PatientLength = UnitConverter.GetBaseValue("cm", 180);
            p.PatientLengthUnit = "cm";

            p.FirstDose.Quantity.Unit = "mg";
            p.FirstDose.Quantity.Value = 200;
            p.FirstDose.Quantity.Adjust = "cm";

            Assert.AreEqual(1.8m, p.FirstDose.Quantity.AdjustLengthValue);
        }

        [TestMethod]
        public void PrescriptionCanCalculateBSA()
        {
            Prescription p = Prescription.NewPrescription();
            p.PatientLength = UnitConverter.GetBaseValue("cm", 185);
            p.PatientLengthUnit = "cm";
            p.PatientWeight = UnitConverter.GetBaseValue("kg", 90); ;
            p.PatientWeightUnit = "kg";
            Assert.AreEqual(2.14m, Math.Round(p.PatientBsa, 2));
        }

    }
}
