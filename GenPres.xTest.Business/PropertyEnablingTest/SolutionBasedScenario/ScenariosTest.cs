using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using GenPres.Business.Allowance;
using GenPres.Business.Domain.Prescriptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.xTest.Base;

namespace GenPres.xTest.Business.VisibilityTest.Scenarios 
{
    [TestClass]
    public class ScenariosTest : BaseGenPresTest
    {
        [TestMethod]
        public void AdminVolumeIsDefaultFalse()
        {
            var prescription = Prescription.NewPrescription();
            Assert.IsFalse(prescription.AdminVolume, "DoseVolume should be default false");
        }

        [TestMethod]
        public void DoseVolumeIsDefaultFalse()
        {
            var prescription = Prescription.NewPrescription();
            Assert.IsFalse(prescription.DoseVolume, "DoseVolume should be default false");
        }

        [TestMethod]
        public void NoVolumesTest()
        {
            var prescription = CreateNoVolumes();
            PrescriptionAllowance.Determine(prescription);

            Assert.IsFalse(new[] { "l", "ml", "cl", "dl" }.Contains(prescription.Quantity.Unit.ToLower()), "AdminVolume should not contain volume");
            Assert.IsFalse(new[] { "l", "ml", "cl", "dl" }.Contains(prescription.Drug.Components[0].Substances[0].Quantity.Unit.ToLower()), "DoseVolume contain should not contain volume");

            Assert.IsFalse(prescription.Drug.Quantity.CanBeSet, "DrugQuantity should not be settable");
            Assert.IsFalse(prescription.Drug.Components[0].Substances[0].DrugConcentration.CanBeSet, "Substance.DrugConcentration should not be settable");

        }

        [TestMethod]
        public void AdminVolumeTest()
        {
            var prescription = CreateAdminVolume();
            PrescriptionAllowance.Determine(prescription);

            Assert.IsTrue(new[] { "l", "ml", "cl", "dl" }.Contains(prescription.Quantity.Unit.ToLower()), "AdminVolume contains volume");
            Assert.IsFalse(new[] { "l", "ml", "cl", "dl" }.Contains(prescription.Drug.Components[0].Substances[0].Quantity.Unit.ToLower()), "DoseVolume contain should not contain volume");

            Assert.IsTrue(prescription.Drug.Quantity.CanBeSet, "DrugQuantity should be settable");
            Assert.IsTrue(prescription.Drug.Components[0].Substances[0].DrugConcentration.CanBeSet, "Substance.DrugConcentration should be settable");
        }

        [TestMethod]
        public void DoseVolumeAdminVolumeTest()
        {
            var prescription = CreateDoseVolumeAdminVolume();
            PrescriptionAllowance.Determine(prescription);

            Assert.IsTrue(new[] { "l", "ml", "cl", "dl" }.Contains(prescription.Quantity.Unit.ToLower()), "AdminVolume contains volume");
            Assert.IsTrue(new[] { "l", "ml", "cl", "dl" }.Contains(prescription.Drug.Components[0].Substances[0].Quantity.Unit.ToLower()), "DoseVolume contains volume");

            Assert.IsTrue(prescription.Drug.Quantity.CanBeSet, "DrugQuantity should be settable");
            Assert.IsTrue(prescription.Drug.Components[0].Substances[0].DrugConcentration.CanBeSet, "Substance.DrugConcentration should be settable");
        }

        [TestMethod]
        public void NoOptionsTest()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            PrescriptionAllowance.Determine(prescription);

            Assert.IsTrue(prescription.Frequency.CanBeSet);
            Assert.IsFalse(prescription.Duration.CanBeSet);
            Assert.IsTrue(prescription.Quantity.CanBeSet);
            Assert.IsTrue(prescription.Doses[0].Quantity.CanBeSet);
            Assert.IsTrue(prescription.Total.CanBeSet);
            Assert.IsTrue(prescription.Doses[0].Total.CanBeSet);
            Assert.IsFalse(prescription.Rate.CanBeSet);
            Assert.IsFalse(prescription.Doses[0].Rate.CanBeSet);
        }

        [TestMethod]
        public void OnRequestTest()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            prescription.OnRequest = true;
            PrescriptionAllowance.Determine(prescription);

            Assert.IsTrue(prescription.Frequency.CanBeSet);
            Assert.IsFalse(prescription.Duration.CanBeSet);
            Assert.IsTrue(prescription.Quantity.CanBeSet);
            Assert.IsTrue(prescription.Doses[0].Quantity.CanBeSet);
            Assert.IsFalse(prescription.Total.CanBeSet);
            Assert.IsFalse(prescription.Doses[0].Total.CanBeSet);
            Assert.IsFalse(prescription.Rate.CanBeSet);
            Assert.IsFalse(prescription.Doses[0].Rate.CanBeSet);
        }

        [TestMethod]
        public void OnRequestContinuousTest()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            prescription.OnRequest = true;
            prescription.Continuous = true;
            PrescriptionAllowance.Determine(prescription);

            Assert.IsTrue(prescription.Frequency.CanBeSet);
            Assert.IsFalse(prescription.Duration.CanBeSet);
            Assert.IsFalse(prescription.Quantity.CanBeSet);
            Assert.IsFalse(prescription.Doses[0].Quantity.CanBeSet);
            Assert.IsFalse(prescription.Total.CanBeSet);
            Assert.IsFalse(prescription.Doses[0].Total.CanBeSet);
            Assert.IsTrue(prescription.Rate.CanBeSet);
            Assert.IsTrue(prescription.Doses[0].Rate.CanBeSet);
        }

        [TestMethod]
        public void OnRequestInfusionTest()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            prescription.OnRequest = true;
            prescription.Infusion = true;
            PrescriptionAllowance.Determine(prescription);

            Assert.IsTrue(prescription.Frequency.CanBeSet);
            Assert.IsTrue(prescription.Duration.CanBeSet);
            Assert.IsTrue(prescription.Quantity.CanBeSet);
            Assert.IsTrue(prescription.Doses[0].Quantity.CanBeSet);
            Assert.IsFalse(prescription.Total.CanBeSet);
            Assert.IsFalse(prescription.Doses[0].Total.CanBeSet);
            Assert.IsTrue(prescription.Rate.CanBeSet);
            Assert.IsFalse(prescription.Doses[0].Rate.CanBeSet);
        }

        [TestMethod]
        public void OnRequestInfusionContinuousTest()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            prescription.OnRequest = true;
            prescription.Continuous = true;
            prescription.Infusion = true;

            PrescriptionAllowance.Determine(prescription);

            Assert.IsTrue(prescription.Frequency.CanBeSet);
            Assert.IsTrue(prescription.Duration.CanBeSet);
            Assert.IsTrue(prescription.Quantity.CanBeSet);
            Assert.IsTrue(prescription.Doses[0].Quantity.CanBeSet);
            Assert.IsFalse(prescription.Total.CanBeSet);
            Assert.IsFalse(prescription.Doses[0].Total.CanBeSet);
            Assert.IsTrue(prescription.Rate.CanBeSet);
            Assert.IsTrue(prescription.Doses[0].Rate.CanBeSet);
        }

        [TestMethod]
        public void ContinuousTest()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            prescription.Continuous = true;
            PrescriptionAllowance.Determine(prescription);

            Assert.IsFalse(prescription.Frequency.CanBeSet);
            Assert.IsFalse(prescription.Duration.CanBeSet);
            Assert.IsFalse(prescription.Quantity.CanBeSet);
            Assert.IsFalse(prescription.Doses[0].Quantity.CanBeSet);
            Assert.IsFalse(prescription.Total.CanBeSet);
            Assert.IsFalse(prescription.Doses[0].Total.CanBeSet);
            Assert.IsTrue(prescription.Rate.CanBeSet);
            Assert.IsTrue(prescription.Doses[0].Rate.CanBeSet);
        }

        [TestMethod]
        public void ContinuousInfusionTest()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            prescription.Continuous = true;
            prescription.Infusion = true;
            PrescriptionAllowance.Determine(prescription);

            Assert.IsTrue(prescription.Rate.CanBeSet);
            Assert.IsTrue(prescription.Doses[0].Rate.CanBeSet);
        }

        [TestMethod]
        public void InfusionTest()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            prescription.Infusion = true;
            PrescriptionAllowance.Determine(prescription);

            Assert.IsTrue(prescription.Frequency.CanBeSet);
            Assert.IsTrue(prescription.Duration.CanBeSet);
            Assert.IsTrue(prescription.Quantity.CanBeSet);
            Assert.IsTrue(prescription.Doses[0].Quantity.CanBeSet);
            Assert.IsTrue(prescription.Total.CanBeSet);
            Assert.IsTrue(prescription.Doses[0].Total.CanBeSet);
            Assert.IsFalse(prescription.Doses[0].Rate.CanBeSet);
            Assert.IsTrue(prescription.Rate.CanBeSet);
        }
    }
}

