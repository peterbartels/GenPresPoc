﻿using System;
using System.Linq;
using GenPres.Data.Managers;
using GenPres.Database;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace GenPres.xTest.Data
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class RepositoryTest
    {
        public RepositoryTest()
        {
            ObjectFactory.Configure(x => x.For<IDataContextManager>().Use<TestDataContextManager>());
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
        public void MapperCanMapChild()
        {
            /* From dao to bo */
            var prDao = new Prescription();
            prDao.Drug = new Drug();
            prDao.Drug.Name = "paracetamol";
            var pr = new PrescriptionBo();
            var prMapper = new PrescriptionMapper();
            prMapper.MapFromDaoToBo(prDao, pr);
            Assert.IsNotNull(pr.Drug);
            Assert.IsTrue(pr.Drug.Generic == prDao.Drug.Name);

            /* From Bo to Dao */
            prDao = new Prescription();
            prMapper.MapFromBoToDao(pr, prDao);
            Assert.IsTrue(prDao.Drug.Name == pr.Drug.Generic);
        }

        [TestMethod]
        public void MapperCanMapCollection()
        {
            /* From dao to bo */
            var prDao = new Prescription();
            prDao.Drug = new Drug();
            prDao.Drug.Name = "paracetamol";
            var c = new Component();
            c.ComponentName = "test";
            prDao.Drug.Components.Add(c);
            var pr = new PrescriptionBo();
            var prMapper = new PrescriptionMapper();
            prMapper.MapFromDaoToBo(prDao, pr);
            
            /* From Bo to Dao */
            Assert.IsTrue(pr.Drug.Components[0].Name == "test");
            prDao.Drug.Components.Clear();
            prDao.Drug.Name = "";
            prMapper.MapFromBoToDao(pr, prDao);
        }

        [TestMethod]
        public void RepositoryCanInsertAndDelete()
        {

            var testRepository = new TestRepository();
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
        public void RepositoryCanGetAll()
        {
            var testRepository = new TestRepository();
            var all = testRepository.All();
            Assert.IsTrue(all.Count() > 0);
        }

        [TestMethod]
        public void _repository_can_GetSingle()
        {
            var testRepository = new TestRepository();
            var single = testRepository.FindSingle(x=>x.Id == 5);

            Assert.IsTrue(single.IsAvailable);
            Assert.IsNotNull(single.Object);
        }

        [TestMethod]
        public void RepositoryCanGetFirst()
        {
            var testRepository = new TestRepository();
            var first = testRepository.First(x => x.Id > 0);
            Assert.IsNotNull(first);
        }

        [TestMethod]
        public void RepositoryCanGetLast()
        {
            var testRepository = new TestRepository();
            var last = testRepository.Last(x => x.Id > 0);
            Assert.IsNotNull(last);
        }

        [TestMethod]
        public void RepositoryCanGetByPrimaryId()
        {
            var testRepository = new TestRepository();
            var byId = testRepository.GetById(5);
            Assert.IsNotNull(byId);
        }
    }
}