using System;
using Informedica.GenPres.Business.Domain.Units;

namespace Informedica.GenPres.Business.Domain.Prescriptions
{
    public class Substance 
    {
        public virtual bool IsNew { get { return (Id == Guid.Empty); } }

        public virtual Guid Id { get; set; }

        public virtual UnitValue Quantity { get; set; }
        public virtual UnitValue DrugConcentration { get; set; }
        public virtual UnitValue ComponentConcentration { get; set; }
        public virtual decimal[] SubstanceIncrements { get; set; }

        public virtual string Name { get; set; }

        protected Substance()
        {
           
        }

        public static Substance NewSubstance()
        {
            var s = new Substance();
            s.Quantity = UnitValue.NewUnitValue(false);
            s.Quantity.Unit = "mg";
            s.DrugConcentration = UnitValue.NewUnitValue(false);
            s.ComponentConcentration = UnitValue.NewUnitValue(false);
            return s;
        }
    }
}
