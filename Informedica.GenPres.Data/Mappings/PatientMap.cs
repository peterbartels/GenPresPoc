using FluentNHibernate.Mapping;
using Informedica.GenPres.Business.Domain.Patients;

namespace Informedica.GenPres.Data.Mappings
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
