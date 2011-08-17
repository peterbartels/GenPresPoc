using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Data.Mappings
{
    public sealed class DrugMap : ComponentMap<Drug>
    {
        public DrugMap()
        {
            Map(x => x.Generic);
            Map(x => x.Route);
            Map(x => x.Shape);
            Component(x => x.Quantity).ColumnPrefix("Quantity_");
            HasMany(x => x.Components).Cascade.All();
        }
    }
}
