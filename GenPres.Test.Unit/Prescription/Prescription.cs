using System;
using GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Business.Domain.Prescription;

namespace GenPres.Test.Unit
{
    [TestClass]
    public class PrescriptionTest : BaseGenPresTest
    {
        #region Constructor
        public PrescriptionTest()
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
        public void Prescription_creates_CreationDate()
        {
            var p = Prescription.NewPrescription();
            Assert.AreEqual(p.CreationDate.ToString("dd-MM-yyyy HH:mm"), DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
        }


    }
}
