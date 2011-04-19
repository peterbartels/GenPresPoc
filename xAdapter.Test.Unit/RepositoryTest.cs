using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xData.Test.Unit.TestClasses;
using TypeMock;
using TypeMock;
using TypeMock.ArrangeActAssert;
using GenPres.Business;

namespace xData.Test.Unit
{
    /// <summary>
    /// Summary description for RepositoryTest
    /// </summary>
    [TestClass]
    public class RepositoryTest
    {
        public RepositoryTest()
        {}

        #region Context
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
        #endregion

        public static TestInstanceRegistrator TestInstanceRegistrator;

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        
         [ClassInitialize()]
         public static void MyClassInitialize(TestContext testContext)
         {
             TestInstanceRegistrator = new TestInstanceRegistrator();
         }
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
        public void BusinessObjectGetCallsRepositoryFetch()
        {
            var repository = TestInstanceRegistrator.repository;
            ITestClassCslaRoot root = TestClassCslaRoot.GetById(1);
            Isolate.Verify.WasCalledWithExactArguments(() => repository.GetById(1));
        }

        [TestMethod]
        public void BusinessObjectCallsRepositoryMethod()
        {
            var repository = TestInstanceRegistrator.repository;
            ITestClassCslaRoot root = TestClassCslaRoot.GetById(1);
            Isolate.Verify.WasCalledWithExactArguments(() => repository.GetById(1));
        }

        [TestMethod]
        public void BusinessObjectFetchCallsMapper()
        {
            var mapper = TestInstanceRegistrator.mapper;
            ITestClassCslaRoot root = TestClassCslaRoot.GetById(1);
            Isolate.Verify.WasCalledWithAnyArguments(() => mapper.MapDaoToBo(null, null));
        }

        [TestMethod]
        public void MapperFetchesDaoToBo()
        {
            ITestClassCslaRoot test = TestClassCslaRoot.GetById(1);
            Assert.IsTrue(test.Id == 1, "Id is not mapped from DAO to BusinessObject");
            Assert.IsTrue(test.Name == "test", "Name is not mapped from DAO to BusinessObject");    
        }
        [TestMethod]
        public void MapperFetchesChildObject()
        {
            ITestClassCslaRoot test = TestClassCslaRoot.GetById(1);
            Assert.IsTrue(test.ChildClass != null, "ChildClass in TestClassCslaRoot is not created.");
        }
    }

    public class TestInstanceRegistrator
    {
        public TestClassCslaRootMapper mapper;
        public TestRepository repository;

        public TestInstanceRegistrator() {
            
            repository = Isolate.Fake.Instance<TestRepository>(Members.CallOriginal);
            GenPresServiceProvider.Create().RegisterInstanceOfType<ITestRepository>(repository);

            mapper = Isolate.Fake.Instance<TestClassCslaRootMapper>(Members.CallOriginal);
            GenPresServiceProvider.Create().RegisterInstanceOfType<ITestClassCslaRootMapper>(mapper);

        }
    }
}
