using FluentNHibernate.Mapping;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Data.Mappings
{
    public sealed class ComponentMap : ClassMap<Component>
    {
        public ComponentMap()
        {
            Id(s => s.Id).GeneratedBy.GuidComb();

            Map(s => s.Name);
            Component(x => x.DrugConcentration).ColumnPrefix("DrugConcentration_");
            Component(x=>x.Quantity).ColumnPrefix("Quantity_");
            HasMany(x => x.Substances).Cascade.All();

            
        }
    }
}
