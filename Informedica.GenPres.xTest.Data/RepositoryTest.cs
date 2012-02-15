using System;
using System.Diagnostics;
using System.Linq;
using GenPres.Assembler;
using GenPres.Assembler.Contexts;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Data;
using GenPres.Data.Managers;
using GenPres.Data.Repositories;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace GenPres.xTest.Data
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class RepositoryTest : BaseGenPresTest
    {
        public RepositoryTest()
        {
            
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

        private static NHibernateRepository<Prescription, Guid> _repository;

        private IDisposable GetContext()
        {
            return ObjectFactory.GetInstance<SessionContext>();
        }

        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            
        }

        [TestMethod]
        public void RunTests()
        {
            _repository = new NHibernateRepository<Prescription, Guid>(SessionManager.Instance.SessionFactoryFromInstance);
            CanInsertAPrescription();
            //RepositoryCanGetAll();
            SessionManager.Instance.CloseSession();
        }

        private string _latestInsertedPrescriptionGuid = "";

        private void CanInsertAPrescription()
        {
            var count = _repository.Count;
            InsertPrescription(_repository);
            _latestInsertedPrescriptionGuid = InsertPrescription(_repository);
            var newCount = _repository.Count;
            Assert.IsTrue((newCount == count + 2), "Inserted 2 prescriptions but newCount is not oldCount + 2: " + new StackFrame().GetMethod().Name);
            Assert.IsTrue((_latestInsertedPrescriptionGuid != Guid.Empty.ToString()), "Prescription Id is not updated: " + new StackFrame().GetMethod().Name);
        }

        private  void RepositoryCanGetAll()
        {
            var all = _repository.Select(x => true);
            int count = all.Count();
            Assert.IsTrue(all.Count() == 2, "Should be 2 prescriptions in testbase but "+ count + " are found" );
        }

        [TestMethod]
        public void _repository_can_GetSingle()
        {
            var obj = _repository.Single(x => x.Id.ToString() == _latestInsertedPrescriptionGuid);            
            Assert.IsNotNull(obj.Id.ToString() == _latestInsertedPrescriptionGuid);
        }

        [TestMethod]
        public void RepositoryCanGetFirst()
        {
            var obj = _repository.First();            
            Assert.IsNotNull(obj.Id.ToString() != Guid.Empty.ToString());
        }

        [TestMethod]
        public void RepositoryCanGetLast()
        {
            var obj = _repository.Last();            
            Assert.IsNotNull(obj.Id.ToString() != Guid.Empty.ToString());
        }

        [TestMethod]
        public void RepositoryCanGetById()
        {
            var obj = _repository.Single(x=>x.Id.ToString() == _latestInsertedPrescriptionGuid);            
            Assert.IsNotNull(obj.Id.ToString() == _latestInsertedPrescriptionGuid);
        }
    }
}
