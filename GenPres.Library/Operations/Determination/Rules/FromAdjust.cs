using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business;

namespace GenPres.Operations.Determination.Rules
{
    class FromAdjust : IDetermination
    {
        public bool Determine(Prescription p)
        {
            if (!(p.AdjustWeight.Value > 0)) return false;


            /* Dose.Quantity */
            if (p.Doses[0].Quantity.Adjust != "")
            {
                decimal val = p.Doses[0].Quantity.BaseValue;
                decimal adjustVal = p.GetAdjustValue(p.Doses[0].Quantity.Adjust);
                val = val / adjustVal;
                val = val / UnitConverter.GetUnitValue(p.Doses[0].Quantity.Adjust, 1);
                p.Doses[0].Quantity.BaseValue = val;
            }

            /* Dose.Total */
            if (p.Doses[0].Total.Adjust != "")
            {
                decimal val = p.Doses[0].Total.BaseValue;
                decimal adjustVal = p.GetAdjustValue(p.Doses[0].Total.Adjust);
                val = val / adjustVal;
                val = val / UnitConverter.GetUnitValue(p.Doses[0].Total.Adjust, 1);
                p.Doses[0].Total.BaseValue = val;
            }

            /* Dose.Rate */
            if (p.Doses[0].Rate.Adjust != "")
            {
                decimal val = p.Doses[0].Rate.BaseValue;
                decimal adjustVal = p.GetAdjustValue(p.Doses[0].Rate.Adjust);
                val = val / adjustVal;
                val = val / UnitConverter.GetUnitValue(p.Doses[0].Rate.Adjust, 1);
                p.Doses[0].Rate.BaseValue = val;
            }

            return true;
        }
    }
}
