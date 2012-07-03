using Informedica.GenPres.Data.DTO.Prescriptions;
using Informedica.GenPres.Data.Visibility;
using Informedica.GenPres.xTest.Base;
using Informedica.GenPres.xTest.Base.TestFixtures.PrescriptionFixtures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Informedica.GenPres.Business.Domain.Prescriptions;

namespace Informedica.GenPres.xTest.Data.PropertyVisibilityTest
{
    [TestClass]
    public class DefaultVisibilitySettingsShould : BaseGenPresTest
    {
        [TestMethod]
        public void ThatIfDrugGenericRouteAndShapeAreNotSetSuldbstanceQuantityIsNotVisible()
        {
            var prescription = Prescription.NewPrescription();
            prescription.Drug.Generic = "";
            prescription.Drug.Route= "";
            prescription.Drug.Shape = "";
            var prescriptionDto = new PrescriptionDto();
            PrescriptionVisibility.Execute(prescription, prescriptionDto);
            Assert.IsFalse(prescriptionDto.substanceQuantity.visible);
        }
        
        [TestMethod]
        public void ThatIfDrugGenericRouteAndShapeAreSetSubstanceQuantityIsVisible()
        {
            var prescription = Prescription.NewPrescription();
            prescription.Drug.Generic = "paracetamol";
            prescription.Drug.Route = "rect";
            prescription.Drug.Shape = "zetp";
            var prescriptionDto = new PrescriptionDto();
            PrescriptionVisibility.Execute(prescription, prescriptionDto);
            Assert.IsTrue(prescriptionDto.substanceQuantity.visible);
        }
    }

}
