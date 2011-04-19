using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enterprise.Data;
using xData.Test.Unit.TestClasses;
using Csla;

namespace xData.Test.Unit
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class CslaLinqAdapterTest
    {
        public CslaLinqAdapterTest()
        { }

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

        [TestMethod]
        public void _can_Map_To_LinqObject()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void _can_Map_to_CslaObject()
        {
            TestClassCslaRoot testClass = TestClassCslaRoot.NewCslaTestClass();

            TestTable testTable = new TestTable();
            testTable.Id = 1;
            testTable.Name = "Test";

            CslaLinqAdapter<TestClassCslaRoot, TestTable> adapter = new CslaLinqAdapter<TestClassCslaRoot, TestTable>();

            adapter.ConfigureMapping()
                .Map<int>(x => x.Id, y => y.Id)
                .Map(x => x.Name, y => y.Name)
            ;

            adapter.Map(testClass, testTable);

            Assert.AreEqual(testTable.Id, testClass.Id);
            Assert.AreEqual(testTable.Name, testClass.Name);
        }
    }    
}
