using System;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Data.Repositories;
using GenPres.Database;
using GenPres.xTest.Base;

namespace GenPres.xTest.Data
{
    
    public class TestRepository : Repository<PrescriptionBo, Prescription>
    {
        public TestRepository()
            : base(new TestDataContextManager())
        {
            
        }

        public override IDataMapper<PrescriptionBo, Prescription> Mapper
        {
            get { throw new NotImplementedException(); }
        }
    }
}
