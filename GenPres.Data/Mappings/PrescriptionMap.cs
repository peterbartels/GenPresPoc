using System;
using FluentNHibernate.Mapping;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Data.Mappings
{
    public sealed class PrescriptionMap : ClassMap<Prescription>
    {
        public PrescriptionMap()
        {
            Id(s => s.Id).GeneratedBy.GuidComb();
            
            Map(s => s.StartDate);
            Map(s => s.EndDate);
            Map(s => s.CreationDate);

            Map(s => s.PID);

            Map(s => s.Continuous);
            Map(s => s.Infusion);
            Map(s => s.OnRequest);
            Map(s => s.Solution);

            Component(x => x.Drug).ColumnPrefix("Drug"); ;

            Component(s => s.Frequency).ColumnPrefix("Frequency");
            
            Component(s => s.Quantity).ColumnPrefix("Quantity"); ;
            Component(s => s.Total).ColumnPrefix("Total"); ;
            Component(s => s.Rate).ColumnPrefix("Rate"); ;

            References(x => x.Patient);
            
            HasMany(x => x.Doses).Cascade.All();

        }
    }
}
