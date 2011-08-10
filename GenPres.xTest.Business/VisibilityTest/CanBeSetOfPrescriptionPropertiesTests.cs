using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace GenPres.xTest.Business.VisibilityTest
{
    /// <summary>
    /// Summary description for CanBeSetOfPrescriptionPropertiesTests
    /// </summary>
    [TestClass]
    public class CanBeSetOfPrescriptionPropertiesTests : BaseGenPresTest
    {
        public CanBeSetOfPrescriptionPropertiesTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void WhenDrugHasNoGenericSubstanceQuantityCanBeSet()
        {
            var prescription = CreatePrescriptionWithAllPropertiesSet();
            prescription.Drug.Generic = "";
            Assert.IsTrue(!prescription.Drug.Quantity.CanBeSet);
        }

        [TestMethod]
        public void WhenDrugGenericIsSetDetemineCanBeSetIsCalled()
        {
            var prescription = CreatePrescriptionWithAllPropertiesSet();
            var setter = Isolate.Fake.Instance<PrescriptionSetter>();
            prescription.Drug.Generic = "";
            Isolate.Verify.WasCalledWithAnyArguments(() => setter.DetemineCanBeSet());
        }
    }

    public class PrescriptionSetter
    {
        public void DetemineCanBeSet()
        {
            throw new NotImplementedException();
        }
    }
}
