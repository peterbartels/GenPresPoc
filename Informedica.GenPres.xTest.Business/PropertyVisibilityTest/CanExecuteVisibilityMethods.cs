using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Data.DTO.Prescriptions;
using Informedica.GenPres.Data.Visibility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenPres.xTest.Business.PropertyVisibilityTest
{
    [TestClass]
    public class CanExecuteVisibilityMethods
    {
        [TestMethod]
        public void CanCreatePrescriptionVisibilityClass()
        {
            PrescriptionVisibility.Determine(Prescription.NewPrescription(), new PrescriptionDto());
        }

        [TestMethod]
        public void DoesNotDetermineVisibilityWhenNoOptionsAreSet()
        {
            var p = Prescription.NewPrescription();
            p.Drug.Generic = "";
            p.Drug.Route = "";
            p.Drug.Shape = "";
            var prescriptionDto = new PrescriptionDto();
            var pv = new PrescriptionVisibility(p, prescriptionDto);
            Isolate.NonPublic.WhenCalled(pv, "ExecuteDetermination").CallOriginal();
            pv.CheckCombinations();
            Isolate.Verify.NonPublic.WasNotCalled(pv, "ExecuteDetermination");
        }

        [TestMethod]
        public void DoesNotDetermineVisibilityWhenGenericIsNotSet()
        {
            var p = Prescription.NewPrescription();
            p.Drug.Generic = "";
            p.Drug.Route = "rect";
            p.Drug.Shape = "zetp";
            var prescriptionDto = new PrescriptionDto();
            var pv = new PrescriptionVisibility(p, prescriptionDto);
            Isolate.NonPublic.WhenCalled(pv, "ExecuteDetermination").CallOriginal();
            pv.CheckCombinations();
            Isolate.Verify.NonPublic.WasNotCalled(pv, "ExecuteDetermination");
        }


        [TestMethod]
        public void DoesNotDetermineVisibilityWhenRouteIsNotSet()
        {
            var p = Prescription.NewPrescription();
            p.Drug.Generic = "paracetamol";
            p.Drug.Route = "";
            p.Drug.Shape = "zetp";
            var prescriptionDto = new PrescriptionDto();
            var pv = new PrescriptionVisibility(p, prescriptionDto);
            Isolate.NonPublic.WhenCalled(pv, "ExecuteDetermination").CallOriginal();
            pv.CheckCombinations();
            Isolate.Verify.NonPublic.WasNotCalled(pv, "ExecuteDetermination");
        }

        [TestMethod]
        public void DoesNotDetermineVisibilityWhenShapeIsNotSet()
        {
            var p = Prescription.NewPrescription();
            p.Drug.Generic = "paracetamol";
            p.Drug.Route = "rect";
            p.Drug.Shape = "";
            var prescriptionDto = new PrescriptionDto();
            var pv = new PrescriptionVisibility(p, prescriptionDto);
            Isolate.NonPublic.WhenCalled(pv, "ExecuteDetermination").CallOriginal();
            pv.CheckCombinations();
            Isolate.Verify.NonPublic.WasNotCalled(pv, "ExecuteDetermination");
        }
        [TestMethod]
        public void DoesDetermineVisibilityWhenGenericAndShapeAndRouteAreSet()
        {
            var p = Prescription.NewPrescription();
            p.Drug.Generic = "paracetamol";
            p.Drug.Shape = "zetp";
            p.Drug.Route = "rect";
            var prescriptionDto = new PrescriptionDto();
            var pv = new PrescriptionVisibility(p, prescriptionDto);
            Isolate.NonPublic.WhenCalled(pv, "ExecuteDetermination").CallOriginal();
            pv.CheckCombinations();
            Isolate.Verify.NonPublic.WasCalled(pv, "ExecuteDetermination");
        }
    }
}
