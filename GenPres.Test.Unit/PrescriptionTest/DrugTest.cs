using System;
using GenPres.Business.Data.Client.PrescriptionData;
using GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Business.Domain.Prescription;

namespace GenPres.Test.Unit
{
    [TestClass]
    public class DrugTest : BaseGenPresTest
    {
        #region Constructor

        public DrugTest()
        { }

        #endregion

        #region TestContext
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #endregion

        [TestMethod]
        public void PrescriptionDto_canMapToDrugBO()
        {
            var pDto = new PrescriptionDto();

            pDto.drugGeneric = "paracetamol";
            pDto.drugRoute = "rect";
            pDto.drugShape = "zetp";
            
            Prescription p = PrescriptionAssembler.AssemblePrescriptionBo(pDto);
            IDrug d = p.Drug;

            Assert.AreEqual(d.Generic, "paracetamol");
            Assert.AreEqual(d.Route, "rect");
            Assert.AreEqual(d.Shape, "zetp");

        }
    }
}
