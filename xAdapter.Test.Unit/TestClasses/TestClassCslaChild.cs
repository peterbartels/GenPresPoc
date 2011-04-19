using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;

namespace xData.Test.Unit.TestClasses
{
    [Serializable]
    public class TestClassCslaChild : BusinessBase<TestClassCslaChild>
    {

        internal static PropertyInfo<int> IdProperty = RegisterProperty(typeof(TestClassCslaChild), new PropertyInfo<int>("Id"));
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


        internal static PropertyInfo<string> NameProperty = RegisterProperty(typeof(TestClassCslaChild), new PropertyInfo<string>("Name"));
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

        public static TestClassCslaChild NewCslaTestClass()
        {
            return DataPortal.Create<TestClassCslaChild>();
        }
    }
}
