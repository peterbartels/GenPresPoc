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
            References(x => x.Patient);
            Component(x => x.Drug);

        }
    }
}
