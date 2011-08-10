using FluentNHibernate.Mapping;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Data.Mappings
{
    public class PrescriptionMap : ClassMap<Prescription>
    {
        public PrescriptionMap()
        {
            Id(s => s.Id).GeneratedBy.GuidComb();
            Map(s => s.StartDate).Not.Nullable();
        }
    }
}
