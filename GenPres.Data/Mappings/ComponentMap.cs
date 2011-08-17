using FluentNHibernate.Mapping;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Data.Mappings
{
    public sealed class ComponentMap : ClassMap<Component>
    {
        public ComponentMap()
        {
            Id(s => s.Id).GeneratedBy.GuidComb();
            Component(x=>x.Quantity).ColumnPrefix("Quantity_");
            HasMany(x => x.Substances).Cascade.All();
        }
    }
}
