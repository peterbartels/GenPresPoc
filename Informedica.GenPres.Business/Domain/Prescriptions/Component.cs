using System;
using System.Collections.Generic;
using Informedica.GenPres.Business.Domain.Units;

namespace Informedica.GenPres.Business.Domain.Prescriptions
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
            c.Quantity = UnitValue.NewUnitValue();
            c.DrugConcentration = UnitValue.NewUnitValue();
            return c;
        }
    }
}
