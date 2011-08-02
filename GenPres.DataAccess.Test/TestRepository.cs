using System;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.DataAccess.Repositories;
using GenPres.Database;
using GenPres.xTest.General;

namespace GenPres.DataAccess.Test
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
