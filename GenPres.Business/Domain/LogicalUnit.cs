using System;
using GenPres.Business.Data.DataAccess.Repository;
using GenPres.Business.ServiceProvider;
using GenPres.Business.Aspect;

namespace GenPres.Business.Domain
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
