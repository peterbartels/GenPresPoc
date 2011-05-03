using System.Linq;
using GenPres.Business.Data.DataAccess.Repository;
using GenPres.Business.ServiceProvider;
using GenPres.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Business.Service;
using GenPres.Business;
using GenPres.DataAccess.Repository;
using TypeMock.ArrangeActAssert;
using GenPres.Business.Domain;

namespace GenPres.Test.Integration
{
    [TestClass]
    public class LogicalUnitTest
    {
        public LogicalUnitTest()
        {
            Settings.SettingsManager.Instance.Initialize();
        }

        private TestContext testContextInstance;

        #region TestContext
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

        private ILogicalUnitRepository _initializeLogicalUnitTest()
        {
            var repository = Isolate.Fake.Instance<LogicUnitRepository>(Members.CallOriginal);
            DalServiceProvider.Instance.RegisterInstanceOfType<ILogicalUnitRepository>(repository);
            return repository;
        }

        [Isolated]
        [TestMethod]
        public void LogicalUnitGetLogicalUnits_calls_RepositoryGetLogicalUnits()
        {
            _initializeLogicalUnitTest();
            var logicalUnitRepository = DalServiceProvider.Instance.Resolve<ILogicalUnitRepository>();
            LogicalUnit.GetLogicalUnits();
            Isolate.Verify.WasCalledWithAnyArguments(() => logicalUnitRepository.GetLogicalUnits());
        }
    }
}
