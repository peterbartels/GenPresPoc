using GenPres.Business.Data.Client.PrescriptionData;
using GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Business.Domain.PrescriptionDomain;

namespace GenPres.Business.Test.PrescriptionTest
{
    [TestClass]
    public class DrugTest : BaseGenPresTest
    {
        [TestMethod]
        public void PrescriptionDto_canMapToDrugBO()
        {
            var pDto = new PrescriptionDto();

            pDto.drugGeneric = "paracetamol";
            pDto.drugRoute = "rect";
            pDto.drugShape = "zetp";
            
            IPrescription p = PrescriptionAssembler.AssemblePrescriptionBo(pDto);
            IDrug d = p.Drug;

            Assert.AreEqual(d.Generic, "paracetamol");
            Assert.AreEqual(d.Route, "rect");
            Assert.AreEqual(d.Shape, "zetp");

        }
    }
}
