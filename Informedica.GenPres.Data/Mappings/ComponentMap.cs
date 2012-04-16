using FluentNHibernate.Mapping;
using Informedica.GenPres.Business.Domain.Prescriptions;

namespace Informedica.GenPres.Data.Mappings
{
    public sealed class CMap : ClassMap<Component>
    {
        public CMap()
        {
            Id(s => s.Id).GeneratedBy.GuidComb();

            Map(s => s.Name);
            //Component(x => x.DrugConcentration).ColumnPrefix("DrugConcentration_");
            //Component(x=>x.Quantity).ColumnPrefix("Quantity_");
            HasMany(x => x.Substances).Cascade.All();

            
        }
    }
}
