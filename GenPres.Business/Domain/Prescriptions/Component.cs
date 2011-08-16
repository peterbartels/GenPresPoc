using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Domain.Units;

namespace GenPres.Business.Domain.Prescriptions
{
    public class Component 
    {

        public bool IsNew { get { return (Id == Guid.Empty); } }

        public Guid Id { get; set; }

        public UnitValue Quantity { get; set; }
        public UnitValue DrugConcentration { get; set; }
        public decimal ComponentIncrement { get; set; }
        public List<Substance> Substances { get; set; }

        public Component()
        {
            Substances = new List<Substance> {new Substance()};
        }
    }
}
