using FluentNHibernate.Mapping;
using GenPres.Business.Domain.Prescriptions;
using GenPres.xTest.Data;

namespace GenPres.xTest.Base.Mappers
{
    public class PrescriptionMap : ClassMap<PrescriptionBo>
    {
        public PrescriptionMap()
        {
            Id(s => s.Id).GeneratedBy.GuidComb();
            Map(s => s.StartDate);
        }
    }
}
