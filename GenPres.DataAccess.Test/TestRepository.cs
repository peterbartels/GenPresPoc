
using System;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Business.Domain;
using GenPres.DataAccess.Repositories;

namespace GenPres.DataAccess.Test
{
    public class Test : ISavable
    {
        public bool IsNew { get; set; }
        public void OnCreate()
        {
            throw new NotImplementedException();
        }

        public void OnNew()
        {
            throw new NotImplementedException();
        }

        public void OnInitExisting()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public int Id
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }

    public class TestRepository : Repository<TestRootTable, Test>
    {
        public TestRepository()
            : base(new TestDataContextFactory())
        {
            
        }

        public override IDataMapper<Test, TestRootTable> Mapper
        {
            get { throw new NotImplementedException(); }
        }
    }
}
