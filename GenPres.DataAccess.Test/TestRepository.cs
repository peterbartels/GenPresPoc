using System;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.DataAccess.Repositories;

namespace GenPres.DataAccess.Test
{
    
    public class TestRepository : Repository<PrescriptionBo, Prescription>
    {
        public TestRepository()
            : base(new TestDataContextFactory())
        {
            
        }

        public override IDataMapper<PrescriptionBo, Prescription> Mapper
        {
            get { throw new NotImplementedException(); }
        }
    }
}
