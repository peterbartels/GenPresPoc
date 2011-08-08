using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Domain.Units;

namespace GenPres.Business.Domain.Prescriptions
{
    public interface IComponent : ISavable
    {
        UnitValue Quantity { get; set; }
        UnitValue DrugConcentration { get; set; }
        decimal ComponentIncrement { get; set; }
        List<ISubstance> Substances{ get; set; }
    }
}
