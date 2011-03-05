using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business;

namespace GenPres.Operations.Determination.Rules
{
    class ToAdjust : IDetermination
    {
        public bool Determine(Prescription p)
        {
            if (!(p.AdjustWeight.Value > 0)) return false;

            if (p.AdjustLength.Value > 0)
            {
                decimal bsa = 0.20247m * (decimal)Math.Pow((double)p.AdjustLength.BaseValue, 0.725) * (decimal)Math.Pow((double)UnitConverter.GetUnitValue("kg", p.AdjustWeight.BaseValue), 0.425);
                p.AdjustBSA.Value = bsa;
            }

            /* Dose.Quantity */
            if (p.Doses[0].Quantity.Adjust != "")
            {
                decimal val = p.Doses[0].Quantity.BaseValue;
                decimal adjustVal = p.GetAdjustValue(p.Doses[0].Quantity.Adjust);
                val = val * adjustVal;
                val = val * UnitConverter.GetUnitValue(p.Doses[0].Quantity.Adjust, 1);
                p.Doses[0].Quantity.BaseValue = val;
            }

            /* Dose.Total */
            if (p.Doses[0].Total.Adjust != "")
            {
                decimal val = p.Doses[0].Total.BaseValue;
                decimal adjustVal = p.GetAdjustValue(p.Doses[0].Total.Adjust);
                val = val * adjustVal;
                val = val * UnitConverter.GetUnitValue(p.Doses[0].Total.Adjust, 1);
                p.Doses[0].Total.BaseValue = val;
            }


            /* Dose.Rate */
            if (p.Doses[0].Rate.Adjust != "")
            {
                decimal val = p.Doses[0].Rate.BaseValue;
                decimal adjustVal = p.GetAdjustValue(p.Doses[0].Rate.Adjust);
                val = val * adjustVal;
                val = val * UnitConverter.GetUnitValue(p.Doses[0].Rate.Adjust, 1);
                p.Doses[0].Rate.BaseValue = val;
            }

            return true;
        }
    }
}
