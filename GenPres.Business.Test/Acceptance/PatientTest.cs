using System.Collections.ObjectModel;
using GenPres.Business.Data.Client.Patient;
using GenPres.Business.Data.DataAccess.Repository;
using GenPres.Business.Service;
using GenPres.Business.ServiceProvider;
using GenPres.DataAccess.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace GenPres.Business.Test.Acceptance
{
    [TestClass]
    public class PatientTest
    {
        public PatientTest()
        {
            Settings.SettingsManager.Instance.Initialize();
        }

        #region TestContext
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
        #endregion


        private IPatientRepository _initializePatientTest()
        {
            var repository = Isolate.Fake.Instance<PatientRepository>(Members.CallOriginal);
            DalServiceProvider.Instance.RegisterInstanceOfType<IPatientRepository>(repository);
            return repository;
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
        public void _can_GetPatientsByLogicalId()
        {
            _initializePatientTest();
            ReadOnlyCollection<PatientTreeDto> patients = PatientService.GetPatientsByLogicalUnit(1);
            Assert.IsTrue(patients.Count > 0);
        }
    }
}
