using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Domain.Units;

namespace GenPres.Business.Domain.Prescriptions
{
    public class Substance : ISubstance
    {
        public bool IsNew { get { return (Id == 0); } }
        public int Id { get; set; }
        public UnitValue Quantity { get; set; }
        public UnitValue DrugConcentration { get; set; }
        public UnitValue ComponentConcentration { get; set; }
        public decimal[] SubstanceIncrements { get; set; }
    }
}
