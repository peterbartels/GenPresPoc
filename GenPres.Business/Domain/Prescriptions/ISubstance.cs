using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Domain.Units;

namespace GenPres.Business.Domain.Prescriptions
{
    public interface ISubstance : ISavable
    {
        UnitValue Quantity { get; set; }
        UnitValue DrugConcentration { get; set; }
        UnitValue ComponentConcentration { get; set; }
        decimal[] SubstanceIncrements { get; set; }
    }
}
