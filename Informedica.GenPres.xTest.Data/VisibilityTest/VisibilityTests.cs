using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Data.DTO.Prescriptions;
using Informedica.GenPres.Data.Visibility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenPres.xTest.Data.PropertyVisibilityTest
{
    [TestClass]
    public class VisibilityTests
    {
        [TestMethod]
        public void ThatVisibilityIsNotExecutedWhenNoGenericShapeAndRouteAreSet()
        {
            var p = Prescription.NewPrescription();
            p.Drug.Generic = "";
            p.Drug.Route = "";
            p.Drug.Shape = "";
            var prescriptionDto = new PrescriptionDto();
            Isolate.NonPublic.WhenCalled<PrescriptionVisibility>("Execute").CallOriginal();
            PrescriptionVisibility.Execute(p, prescriptionDto);
            Isolate.Verify.NonPublic.WasNotCalled(typeof(PrescriptionVisibility), "_setScenariosVisibility");
        }

        [TestMethod]
        public void ThatVisibilityIsNotExecutedWhenNoGenericIsSet()
        {
            var p = Prescription.NewPrescription();
            p.Drug.Generic = "";
            p.Drug.Route = "rect";
            p.Drug.Shape = "zetp";
            var prescriptionDto = new PrescriptionDto();
            Isolate.NonPublic.WhenCalled<PrescriptionVisibility>("Execute").CallOriginal();
            PrescriptionVisibility.Execute(p, prescriptionDto);
            Isolate.Verify.NonPublic.WasNotCalled(typeof(PrescriptionVisibility), "_setScenariosVisibility");
        }


        [TestMethod]
        public void ThatVisibilityIsNotExecutedWhenNoRouteIsSet()
        {
            var p = Prescription.NewPrescription();
            p.Drug.Generic = "paracetamol";
            p.Drug.Route = "";
            p.Drug.Shape = "zetp";
            var prescriptionDto = new PrescriptionDto();
            Isolate.NonPublic.WhenCalled<PrescriptionVisibility>("Execute").CallOriginal();
            PrescriptionVisibility.Execute(p, prescriptionDto);
            Isolate.Verify.NonPublic.WasNotCalled(typeof(PrescriptionVisibility), "_setScenariosVisibility");
        }

        [TestMethod]
        public void ThatVisibilityIsNotExecutedWhenNoShapeIsSet()
        {
            var p = Prescription.NewPrescription();
            p.Drug.Generic = "paracetamol";
            p.Drug.Route = "rect";
            p.Drug.Shape = "";
            var prescriptionDto = new PrescriptionDto();
            Isolate.NonPublic.WhenCalled<PrescriptionVisibility>("Execute").CallOriginal();
            PrescriptionVisibility.Execute(p, prescriptionDto);
            Isolate.Verify.NonPublic.WasNotCalled(typeof(PrescriptionVisibility), "_setScenariosVisibility");
        }
        [TestMethod]
        public void ThatVisibilityIsExecutedWhenGenericAndShapeAndRouteAreSet()
        {
            var p = Prescription.NewPrescription();
            p.Drug.Generic = "paracetamol";
            p.Drug.Shape = "zetp";
            p.Drug.Route = "rect";
            var prescriptionDto = new PrescriptionDto();
            Isolate.NonPublic.WhenCalled<PrescriptionVisibility>("Execute").CallOriginal();
            PrescriptionVisibility.Execute(p, prescriptionDto);
            Isolate.Verify.NonPublic.WasCalled(typeof(PrescriptionVisibility), "_setScenariosVisibility");
        }
    }
}
