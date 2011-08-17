using FluentNHibernate.Mapping;
using GenPres.Business.Domain.Units;

namespace GenPres.Data.Mappings
{
    public class UnitValueMap : ComponentMap<UnitValue>
    {
        public UnitValueMap()
        {
            Map(x => x.BaseValue);
            Map(x => x.Value);
            Map(x => x.Unit);
        }
    }
}
