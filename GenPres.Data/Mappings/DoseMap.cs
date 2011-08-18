using System;
using FluentNHibernate.Mapping;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Data.Mappings
{
    public sealed class DoseMap : ClassMap<Dose>
    {
        public DoseMap()
        {
            Id(s => s.Id).GeneratedBy.GuidComb();

            Component(s => s.Quantity).ColumnPrefix("Quantity");
            Component(s => s.Total).ColumnPrefix("Total"); ;
            Component(s => s.Rate).ColumnPrefix("Rate"); ;
        }

    }
}
