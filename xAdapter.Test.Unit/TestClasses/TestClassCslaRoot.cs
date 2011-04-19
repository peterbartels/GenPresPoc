using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business;
using Csla;

namespace xData.Test.Unit.TestClasses
{

    public interface ITestClassCslaRoot
    {
        int Id { get; set; }
        string Name { get; set; }
        TestClassCslaChild ChildClass { get; set; }
    }

    public static class ObjectFactory
    {
        public static TAbstractType Create<TAbstractType, TConcreteType>() where TConcreteType : TAbstractType
        {
            return (TAbstractType)DataPortal.Create<TConcreteType>();
        }
        public static TAbstractType Fetch<TAbstractType, TConcreteType>(object dataObject) 
            where TConcreteType : TAbstractType
        {
            return (TAbstractType)DataPortal.Fetch<TConcreteType>(dataObject);
        }
    }

    [Serializable]
    public class TestClassCslaRoot : BusinessBase<TestClassCslaRoot>, ITestClassCslaRoot
    {

        internal static PropertyInfo<int> IdProperty = RegisterProperty(typeof(TestClassCslaRoot), new PropertyInfo<int>("Id"));
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


        internal static PropertyInfo<string> NameProperty = RegisterProperty(typeof(TestClassCslaRoot), new PropertyInfo<string>("Name"));
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


        internal static PropertyInfo<TestClassCslaChild> SubTestClassProperty = RegisterProperty(typeof(TestClassCslaRoot), new PropertyInfo<TestClassCslaChild>("SubTestClass"));
        public TestClassCslaChild ChildClass
        {
            get
            {
                TestClassCslaChild value = GetProperty(SubTestClassProperty);
                return value;
            }
            set
            {
                SetProperty(SubTestClassProperty, value);
            }
        }

        private void DataPortal_Fetch(object dao)
        {
            var mapper = GenPresServiceProvider.Create().Resolve<ITestClassCslaRootMapper>();
            mapper.MapDaoToBo(dao, this);

        }

        public static TestClassCslaRoot NewCslaTestClass()
        {
            return DataPortal.Create<TestClassCslaRoot>();
        }

        public static ITestClassCslaRoot GetById(int id)
        {
            ITestRepository repository = GenPresServiceProvider.Create().Resolve<ITestRepository>();
            return repository.GetById(id);
        }
    }
}
