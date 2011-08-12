using System;
using FluentNHibernate.Mapping;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Data.Mappings
{
    public class PrescriptionMap : ClassMap<Prescription>
    {
        
        public PrescriptionMap()
        {
            //Id(s => s.Id).GeneratedBy.GuidComb();
            Id(s => s.Id);
            //Map(s => s.StartDate);
        }
    }
}
