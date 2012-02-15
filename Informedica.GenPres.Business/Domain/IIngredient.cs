using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.Domain
{
    internal interface IIngredient : IDose
    {
        UnitValue Quantity { get; set; }
        UnitValue TotalConcentration { get; set; }
        UnitValue ContainerConcentration { get; set; }
    }
}
