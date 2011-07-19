using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.ServiceProvider;

namespace GenPres.Business.Domain.PatientDomain
{
    public class LogicalUnit : ILogicalUnit
    {
        private int _id;
        private string _name;

        public int Id { get; set; }
        public string Name { get; set; }

        private static ILogicalUnitRepository Repository
        {
            get { return DalServiceProvider.Instance.Resolve<ILogicalUnitRepository>(); }
        }

        public static LogicalUnit NewLogicalUnit()
        {
            return new LogicalUnit();
        }

        public static ILogicalUnit[] GetLogicalUnits()
        {
            return Repository.GetLogicalUnits();
        }
    }
}
