using Informedica.GenPres.Data.DTO.Prescriptions;
using Informedica.GenPres.Data.Visibility;
using Informedica.GenPres.xTest.Base;
using Informedica.GenPres.xTest.Base.TestFixtures.PrescriptionFixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Business.PropertyVisibilityTest
{
    /// <summary>
    /// Summary description for CanBeSetOfPrescriptionPropertiesTests
    /// </summary>
    [TestClass]
    public class DefaultVisibilitySettingsTest : BaseGenPresTest
    {
        [TestMethod]
        public void WhenDrugHasNoGenericSubstanceQuantityCanNotBeSet()
        {
            var prescription = NoVolumeNoOptionsFixture.CreateParacetamolGeneric();
            prescription.Drug.Generic = "";
            var prescriptionDto = new PrescriptionDto();
            PrescriptionVisbility.Determine(prescription, prescriptionDto);
            Assert.IsFalse(prescriptionDto.substanceQuantity.IsVisible);
        }

        
        [TestMethod]
        public void WhenDrugGenericRouteAndShapeIsSetSubstanceQuantityCanBeSet()
        {
            var prescription = NoVolumeNoOptionsFixture.CreateParacetamolGeneric();
            var prescriptionDto = new PrescriptionDto();
            PrescriptionVisbility.Determine(prescription, prescriptionDto);
            Assert.IsTrue(prescriptionDto.substanceQuantity.IsVisible);
        }
    }

}
