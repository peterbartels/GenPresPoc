using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enterprise.Data;
using Csla;

namespace xAdapter.Test.Unit
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
            CslaTestClass testClass = CslaTestClass.NewCslaTestClass();

            TestTable testTable = new TestTable();
            testTable.Id = 1;
            testTable.Name = "Test";

            CslaLinqAdapter<CslaTestClass, TestTable> adapter = new CslaLinqAdapter<CslaTestClass, TestTable>();

            adapter.ConfigureMapping()
                .Map(x => x.Id, y => y.Id)
                .Map(x => x.Name, y => y.Name)
            ;

            adapter.Map(testClass, testTable);

            Assert.AreEqual(testTable.Id, testClass.Id);
            Assert.AreEqual(testTable.Name, testClass.Name);
        }
    }

    [Serializable]
    internal class CslaTestClass : BusinessBase<CslaTestClass>
    {

        internal static PropertyInfo<int> IdProperty = RegisterProperty(typeof(CslaTestClass), new PropertyInfo<int>("Id"));
        public int Id
        {
            get
            {
                int value = GetProperty(IdProperty);
                return value;
            }
            set
            {
                SetProperty(IdProperty, value);
            }
        }


        internal static PropertyInfo<string> NameProperty = RegisterProperty(typeof(CslaTestClass), new PropertyInfo<string>("Name"));
        public string Name
        {
            get
            {
                string value = GetProperty(NameProperty);
                return value;
            }
            set
            {
                SetProperty(NameProperty, value);
            }
        }


        internal static PropertyInfo<string> SubTestClassProperty = RegisterProperty(typeof(CslaTestClass), new PropertyInfo<string>("SubTestClass"));
        public string SubTestClass
        {
            get
            {
                string value = GetProperty(SubTestClassProperty);
                return value;
            }
            set
            {
                SetProperty(SubTestClassProperty, value);
            }
        }

        public static CslaTestClass NewCslaTestClass()
        {
            return DataPortal.Create<CslaTestClass>();
        }
    }

    [Serializable]
    internal class CslaSubTestClass : BusinessBase<CslaSubTestClass>
    {

        internal static PropertyInfo<int> IdProperty = RegisterProperty(typeof(CslaSubTestClass), new PropertyInfo<int>("Id"));
        public int Id
        {
            get
            {
                int value = GetProperty(IdProperty);
                return value;
            }
            set
            {
                SetProperty(IdProperty, value);
            }
        }


        internal static PropertyInfo<string> NameProperty = RegisterProperty(typeof(CslaSubTestClass), new PropertyInfo<string>("Name"));
        public string Name
        {
            get
            {
                string value = GetProperty(NameProperty);
                return value;
            }
            set
            {
                SetProperty(NameProperty, value);
            }
        }


        public static CslaSubTestClass NewCslaTestClass()
        {
            return DataPortal.Create<CslaSubTestClass>();
        }
    }    
}
