using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.Domain
{
    internal interface IDose
    {
        UnitValue DoseQuantity { get; set; }
        UnitValue DoseTotal { get; set; }
        UnitValue DoseRate { get; set; }
    }
}
