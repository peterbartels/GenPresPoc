using System;
using FluentNHibernate.Mapping;
using GenPres.Business.Domain.Patients;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Business.Domain.Units;

namespace GenPres.Data.Mappings
{
    public sealed class PatientMap : ClassMap<Patient>
    {
        public PatientMap()
        {
            Id(s => s.Id).GeneratedBy.GuidComb();
            Map(s => s.Pid).Not.Nullable();
            HasMany(x=>x.Prescriptions).Cascade.All();
            Component(x => x.Height).ColumnPrefix("Height_");
            Component(x => x.Weight).ColumnPrefix("Weight_");
        }
    }
}
