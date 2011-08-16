using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Data.Mappings
{
    public class DrugMap : ClassMap<Drug>
    {
        public DrugMap()
        {
            Id(s => s.Id).GeneratedBy.GuidComb();
        }
    }
}
