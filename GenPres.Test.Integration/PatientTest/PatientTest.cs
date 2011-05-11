using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using GenPres.Business.Data.DataAccess.Repository;
using GenPres.Business.Service;
using GenPres.Business.ServiceProvider;
using GenPres.DataAccess.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace GenPres.Test.Integration.PatientTest
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


        private IPatientRepository _initializePatientRepositoryTest()
        {
            var repository = Isolate.Fake.Instance<PatientRepository>(Members.CallOriginal);
            DalServiceProvider.Instance.RegisterInstanceOfType<IPatientRepository>(repository);
            return repository;
        }

        [Isolated]
        [TestMethod]
        public void PatientService_GetPatientsByLogicalUnitId_calls_Repository()
        {
            _initializePatientRepositoryTest();
            var patientRepository = DalServiceProvider.Instance.Resolve<IPatientRepository>();
            PatientService.GetPatientsByLogicalUnit(1);
            Isolate.Verify.WasCalledWithExactArguments(() => patientRepository.GetPatientsByLogicalUnitId(1));
        }
    }
}
