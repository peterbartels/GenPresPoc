using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.DataAccess.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class RepositoryTest
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

        [TestMethod]
        public void _repository_can_Insert_and_Delete()
        {
            
            TestRepository testRepository = new TestRepository();
            int oldCount = testRepository.Count();
            var newObj = testRepository.CreateInstance();
            newObj.Name = "test3";
            testRepository.SaveAll();
            int newCount = testRepository.Count();
            Assert.AreEqual(oldCount + 1, newCount);

            //var last = testRepository.Last(x => x.Id > 0);
            //testRepository.MarkForDeletion(last);
            //testRepository.SaveAll();
            //Assert.AreEqual(oldCount, testRepository.Count());
        }

        [TestMethod]
        public void _repository_can_GetAll()
        {
            TestRepository testRepository = new TestRepository();
            var all = testRepository.All();
            Assert.IsTrue(all.Count() == 2);
        }

        [TestMethod]
        public void _repository_can_GetSingle()
        {
            TestRepository testRepository = new TestRepository();
            var single = testRepository.FindSingle(x=>x.Id == 1);

            Assert.IsTrue(single.IsAvailable);
            Assert.IsNotNull(single.Object);
        }

        [TestMethod]
        public void _repository_can_GetFirst()
        {
            TestRepository testRepository = new TestRepository();
            var first = testRepository.First(x => x.Id == 1);
            Assert.IsNotNull(first);
        }

        [TestMethod]
        public void _repository_can_GetLast()
        {
            TestRepository testRepository = new TestRepository();
            var last = testRepository.Last(x => x.Id > 0);
            Assert.IsNotNull(last);
        }

        [TestMethod]
        public void _repository_can_GetByPrimaryId()
        {
            TestRepository testRepository = new TestRepository();
            var byId = testRepository.GetById(1);
            Assert.IsNotNull(byId);
        }
    }
}
