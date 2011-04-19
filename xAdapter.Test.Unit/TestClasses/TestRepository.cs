using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using GenPres.Business.Data;
using Csla;

namespace xData.Test.Unit.TestClasses
{
    
    public interface ITestRepository
    {
        ITestClassCslaRoot GetById(int id);
    }

    public class TestRepository : ITestRepository
    {
        public ITestClassCslaRoot GetById(int id)
        {
            //using (var dtx = new TestDataManager().GetContext())
            //{
                var testTable = new TestTable { Id = 1, Name = "test" };
                return ObjectFactory.Fetch<ITestClassCslaRoot, TestClassCslaRoot>(testTable);
            //}
        }
    }
}
