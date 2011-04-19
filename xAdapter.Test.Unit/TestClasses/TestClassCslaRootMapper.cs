using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using  Enterprise.Data;

namespace xData.Test.Unit.TestClasses
{
    public interface ITestClassCslaRootMapper
    {
        void MapBoToDao(ITestClassCslaRoot bo, TestTable dao);
        void MapDaoToBo(object dao, ITestClassCslaRoot bo);
    }
    public class TestClassCslaRootMapper : ITestClassCslaRootMapper
    {         
        public void MapBoToDao(ITestClassCslaRoot bo, TestTable dao)
        {
            dao.Id = bo.Id;
            dao.Name = bo.Name;

            var adapter = new SimpleAdapter<ITestClassCslaRoot, TestTable>();
            adapter.ConfigureMapping()
                .MapChild<DaoToCslaObjectMapper>(src => src.ChildClass, dest => dest.SubTestTable1)
            ;
            throw new NotImplementedException();
        }

        public void MapDaoToBo(object daoObj, ITestClassCslaRoot bo)
        {
            
            var adapter = new SimpleAdapter<TestTable, ITestClassCslaRoot>();

            bo.Id = ((TestTable) daoObj).Id;
            bo.Name = ((TestTable) daoObj).Name;

            adapter.ConfigureMapping()
                .Map(src=>src.Id, dest=>dest.Id)
                .Map(src=>src.Name, dest=>dest.Name)
                //.MapChild<DaoToCslaObjectMapper>(src => src.SubTestTable1, dest => dest.ChildClass)
            ;

            adapter.Map((TestTable)daoObj, bo);
        }
    }

    public class DaoToCslaObjectMapper
    {
        public void Map<TType>(Enterprise.Data.MappingTypes.MemberAccessor accessor, object rootBo, object childDao)
            where TType : Csla.Core.BusinessBase
        {
            var newObj = Csla.DataPortal.FetchChild<TType>(childDao);
            accessor.setValue(rootBo, newObj);
        }
    }
}
