using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Business.Service;
using GenPres.Business.Data;
using GenPres.Business.Domain;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace GenPres.Test.Unit
{
    [TestClass]
    public class PrescriptionTest
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
        public void Prescription_can_GetPrescriptionById()
        {
            
        }
    }
}
