using System;
using FluentNHibernate.Mapping;
using GenPres.Business.Domain.Patients;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Data.Mappings
{
    public class PatientMap : ClassMap<Patient>
    {
        public PatientMap()
        {
            Id(s => s.Id).GeneratedBy.GuidComb();
            Map(s => s.Pid);
            HasMany(x=>x.Prescriptions);
        }
    }
}
