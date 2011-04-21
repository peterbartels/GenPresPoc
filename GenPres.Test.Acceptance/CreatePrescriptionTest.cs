using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Controllers;

namespace GenPres.Test.Acceptance
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class CreatePrescriptionTest
    {
        public CreatePrescriptionTest()
        {}

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

        [TestMethod]
        public void _can_process_NewPrescription()
        {
            //var result = new PrescriptionController().NewPrescription();
            //Assert.IsTrue(ActionParser.GetSuccessPropertyValue(result));
        }

        public class ActionParser
        {
            public static bool GetSuccessPropertyValue(ActionResult result)
            {
                throw new NotImplementedException();
            }
        }
        
    }
}
