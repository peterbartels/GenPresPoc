using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Domain.Units;

namespace GenPres.Business.Domain.Prescriptions
{
    public class Component 
    {
        public virtual bool IsNew { get { return (Id == Guid.Empty); } }

        public virtual Guid Id { get; set; }

        public virtual UnitValue Quantity { get; set; }
        public virtual UnitValue DrugConcentration { get; set; }
        private decimal _componentIncrement;
        public virtual decimal ComponentIncrement
        {
            get
            {
                return _componentIncrement;
            }
            set
            {
                _componentIncrement = value;
            }
        }

        public virtual IList<Substance> Substances { get; set; }

        public virtual string Name { get; set; }

        protected Component()
        {
            
        }

        public static Component NewComponent()
        {
            var c = new Component();
            c.Substances = new List<Substance> { Substance.NewSubstance() };
            c.Quantity = UnitValue.NewUnitValue(false);
            c.DrugConcentration = UnitValue.NewUnitValue(false);
            return c;
        }
    }
}
