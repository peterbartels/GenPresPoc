
namespace GenPres.DataAccess.Test
{
    public class TestRepository : Repository.Repository<TestRootTable>
    {
        public TestRepository()
            : base(new TestDataContextFactory())
        {
            
        }
    }
}
