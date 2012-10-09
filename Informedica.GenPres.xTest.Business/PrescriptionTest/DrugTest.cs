using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Data.DTO.Prescriptions;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Business.PrescriptionTest
{
    [TestClass]
    public class DrugTest : BaseGenPresTest
    {
        
        [TestMethod]
        public void PrescriptionDtoCanMapToDrugBo()
        {
            var pDto = new PrescriptionDto();

            pDto.drugGeneric.value = "paracetamol";
            pDto.drugRoute.value = "rect";
            pDto.drugShape.value = "zetp";
            
            Prescription p = PrescriptionAssembler.AssemblePrescriptionBo(pDto);
            Drug d = p.Drug;

            Assert.AreEqual(d.Generic, "paracetamol");
            Assert.AreEqual(d.Route, "rect");
            Assert.AreEqual(d.Shape, "zetp");

        }
    }
}
