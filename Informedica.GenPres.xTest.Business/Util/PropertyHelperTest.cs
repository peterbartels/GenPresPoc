using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Business.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Business.Util
{
    [TestClass]
    public class PropertyHelperTest
    {
        [TestMethod]
        public void PropertyHelperCanGetPropertyInfo()
        {
            var prescription = Prescription.NewPrescription();
            prescription.Quantity.Unit = "mg";
            var qty = PropertyHelper.GetUnitValue(() => prescription.Quantity);
            Assert.IsTrue(qty.Unit == "mg");
        }
        [TestMethod]
        public void PropertyHelperCanGetPropertyInfoWithNestedExpression()
        {
            var prescription = Prescription.NewPrescription();
            prescription.Doses[0].Quantity.Unit = "mg";
            var qty = PropertyHelper.GetUnitValue(() => prescription.Doses[0].Quantity);
            Assert.IsTrue(qty.Unit == "mg");
        }
        [TestMethod]
        public void PropertyHelperCanGetPropertyInfoWithAnyParent()
        {
            var prescription = Prescription.NewPrescription();
            var dose = prescription.Doses[0];
            dose.Quantity.Unit = "mg";
            var qty = PropertyHelper.GetUnitValue(() => dose.Quantity);
            Assert.IsTrue(qty.Unit == "mg");
        }
        [TestMethod]
        public void PropertyHelperCanGetPropertyInfoWithSelfElement()
        {
            var prescription = Prescription.NewPrescription();
            var quantity = prescription.Doses[0].Quantity;
            quantity.Unit = "mg";
            var qty = PropertyHelper.GetUnitValue(() => quantity);
            Assert.IsTrue(qty.Unit == "mg");
        }
    }
}
