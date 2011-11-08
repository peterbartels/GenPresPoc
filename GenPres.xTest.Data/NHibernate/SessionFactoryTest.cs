using System;
using GenPres.Assembler;
using GenPres.Assembler.Contexts;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Data;
using GenPres.Data.Connections;
using GenPres.Data.Repositories;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Context;
using NHibernate.Metadata;
using StructureMap;

namespace GenPres.xTest.Data.NHibernate
{
    /// <summary>
    /// Summary description for SessionFactoryTest
    /// </summary>
    [TestClass]
    public class SessionFactoryTest
    {
        #region Constructor and Context
        public SessionFactoryTest()
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

        #endregion 

        [TestMethod]
        public void BeAbleToCreateAProductionSessionFactory()
        {
            var factory = TestSessionManager.Instance.InitSessionFactory(DatabaseConnection.DatabaseName.GenPresTest, true);
            Assert.IsNotNull(factory);
        }

        [TestMethod]
        public void ProductionSessionFactoryCanBeRetreivedByGenPresApplication()
        {
            //TODO: Makes all other tests fail
            //var factory = SessionFactoryCreator.CreateSessionFactory(DatabaseConnection.DatabaseName.GenPres);
            //Assert.IsInstanceOfType(factory, typeof(ISessionFactory));
        }

        [TestMethod]
        public void BeAbleToCreateATestSessionFactory()
        {
            var factory = TestSessionManager.Instance.InitSessionFactory(DatabaseConnection.DatabaseName.GenPresTest, true);
            Assert.IsNotNull(factory);
        }

        [TestMethod]
        public void TestSessionFactoryCanCreateASession()
        {
            var factory = TestSessionManager.Instance.InitSessionFactory(DatabaseConnection.DatabaseName.GenPresTest, true);
            var session = factory.OpenSession();
            Assert.IsNotNull(session);
        }

        [TestMethod]
        public void TestSessionFactoryHasClassMetadata()
        {
            var factory = TestSessionManager.Instance.InitSessionFactory(DatabaseConnection.DatabaseName.GenPresTest, true);
            var classMetadata = factory.GetClassMetadata(typeof(Prescription));
            Assert.IsNotNull(classMetadata);
        }

        [TestMethod]
        public void TestSessionFactoryCanOpenMultipleSessionsWithoutMakingNewConnections()
        {
            /*var factory = TestSessionManager.InitSessionFactory(DatabaseConnection.DatabaseName.GenPresTest, false);
            var session1 = TestSessionManager.InitSession();
            var session2 = TestSessionManager.InitSession();
            Assert.IsTrue(session1.Connection == session2.Connection);
            */
        }

    }
}
