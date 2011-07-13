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
        public void _mapper_can_MapChild()
        {
            var prDao = new Prescription();
            prDao.Drug = new Drug();
            prDao.Drug.Name = "paracetamol";

            var pr = new PrescriptionBo();
            var prMapper = new PrescriptionMapper(pr, prDao);
            prMapper.InitChildMappings();
            prMapper.MapFromDaoToBo();
            Assert.IsNotNull(pr.Drug);
            Assert.IsTrue(pr.Drug.Generic == prDao.Drug.Name);
        }

        [TestMethod]
        public void _mapper_can_MapCollection()
        {
            var prDao = new Prescription();
            prDao.Drug = new Drug();
            prDao.Drug.Name = "paracetamol";
            Component c = new Component();
            c.ComponentName = "test";
            prDao.Drug.Components.Add(c);
            var pr = new PrescriptionBo();
            var prMapper = new PrescriptionMapper(pr, prDao);
            prMapper.InitChildMappings();
            prMapper.MapFromDaoToBo();
            Assert.IsTrue(pr.Drug.Components[0].Name == "test");

            prDao.Drug.Components.Clear();
            prDao.Drug.Name = "";
            prMapper.MapFromBoToDao();
        }

        [TestMethod]
        public void _repository_can_Insert_and_Delete()
        {
            
            TestRepository testRepository = new TestRepository();
            int oldCount = testRepository.Count();
            var newObj = testRepository.CreateInstance();
            newObj.StartDate = DateTime.Now;
            testRepository.Submit();
            int newCount = testRepository.Count();
            Assert.AreEqual(oldCount + 1, newCount);
            var last = testRepository.Last(x => x.Id > 0);
            testRepository.MarkForDeletion(last);
            testRepository.Submit();
            Assert.AreEqual(oldCount, testRepository.Count());
        }

        [TestMethod]
        public void _repository_can_GetAll()
        {
            TestRepository testRepository = new TestRepository();
            var all = testRepository.All();
            Assert.IsTrue(all.Count() > 0);
        }

        [TestMethod]
        public void _repository_can_GetSingle()
        {
            TestRepository testRepository = new TestRepository();
            var single = testRepository.FindSingle(x=>x.Id == 14);

            Assert.IsTrue(single.IsAvailable);
            Assert.IsNotNull(single.Object);
        }

        [TestMethod]
        public void _repository_can_GetFirst()
        {
            TestRepository testRepository = new TestRepository();
            var first = testRepository.First(x => x.Id > 0);
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
            var byId = testRepository.GetById(14);
            Assert.IsNotNull(byId);
        }
    }
}
