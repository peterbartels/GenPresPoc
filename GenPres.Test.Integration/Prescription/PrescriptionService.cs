using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Controllers;
using GenPres.Business.Service;
using Enterprise.Service;

namespace GenPres.Test.Integration.Prescription
{
    [TestClass]
    public class PrescriptionServiceTest
    {
        public PrescriptionServiceTest()
        {}

        #region testContext
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
        public void _can_process_NewPrescription()
        {
             PrescriptionService.NewPrescription();  
        }
    }
}
