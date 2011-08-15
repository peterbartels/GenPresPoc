using GenPres.Data.DTO.Prescriptions;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.xTest.Business.PrescriptionTest
{
    [TestClass]
    public class DrugTest : BaseGenPresTest
    {
        
        [TestMethod]
        public void PrescriptionDtoCanMapToDrugBo()
        {
            var pDto = new PrescriptionDto();

            pDto.drugGeneric = "paracetamol";
            pDto.drugRoute = "rect";
            pDto.drugShape = "zetp";
            
            Prescription p = PrescriptionAssembler.AssemblePrescriptionBo(pDto);
            Drug d = p.Drug;

            Assert.AreEqual(d.Generic, "paracetamol");
            Assert.AreEqual(d.Route, "rect");
            Assert.AreEqual(d.Shape, "zetp");

        }
    }
}
