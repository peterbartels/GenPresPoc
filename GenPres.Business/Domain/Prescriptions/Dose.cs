﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Domain.Units;

namespace GenPres.Business.Domain.Prescriptions
{
    public class Dose
    {
        public virtual bool IsNew { get { return (Id == Guid.Empty); } }

        public virtual Guid Id { get; set; }

        public virtual UnitValue Quantity { get; set; }
        public virtual UnitValue Total { get; set; }
        public virtual UnitValue Rate { get; set; }
        public virtual decimal[] SubstanceIncrements { get; set; }

        protected Dose()
        {

        }

        public static Dose NewDose()
        {
            var s = new Dose();
            s.Quantity = UnitValue.NewUnitValue(false);
            s.Quantity.Unit = "mg";
            s.Total = UnitValue.NewUnitValue(false);
            s.Total.Unit = "mg";
            s.Rate = UnitValue.NewUnitValue(false);
            s.Rate.Unit = "mg";
            return s;
        }
    }
}
