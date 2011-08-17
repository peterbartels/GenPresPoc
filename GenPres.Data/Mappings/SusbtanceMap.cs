using FluentNHibernate.Mapping;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Data.Mappings
{
    public sealed class SubstanceMap : ClassMap<Substance>
    {
        public SubstanceMap()
        {
            Id(s => s.Id).GeneratedBy.GuidComb();
            Component(x => x.Quantity).ColumnPrefix("Quantity_");
        }
    }
}
