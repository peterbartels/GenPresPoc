using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using GenPres.Business;

namespace GenPres.Operations.Calculation
{
    class ContinuousCalculation
    {
        public static decimal[] GetContinuousIncrementSize(
            ContinuousCombination combination,
            decimal continuousValue,
            decimal substanceIncrement,
            Factor factor)
        {
            PropertyManager pm = PropertyManager.Instance;
            decimal[] valsFloor = combination.GetPropertiesValuesArray();
            
            decimal[] incrementStepsFloor = new decimal[valsFloor.Length];
            decimal[] incrementStepsCeiling = new decimal[valsFloor.Length];

            int nonCalculatedIndex = combination.GetNonCalculatedFieldIndex();
            int continuousIndex = combination.GetIndexByProperty(combination.GetContinuousProperty());
            
            valsFloor[continuousIndex] = continuousValue;

            MathExt.CalculateRawValues(nonCalculatedIndex, valsFloor);
            decimal[] valsCeiling = new decimal[3];

            for (int i = 0; i < valsFloor.Length; i++)
            {
                if (i == continuousIndex) continue;
                decimal incrementStep = combination.GetIncrementStep(i, valsFloor[i], substanceIncrement);
                //if (!pm.IsPrescriptionSolution()) incrementStep = 1;
                incrementStepsCeiling[i] = Math.Ceiling(incrementStep);
                incrementStepsFloor[i] = MathExt.RoundToInt(Math.Floor(incrementStep));

                valsFloor[i] = combination.GetIncrementValue(i, incrementStepsFloor[i], substanceIncrement);
                valsCeiling[i] = combination.GetIncrementValue(i, incrementStepsCeiling[i], substanceIncrement);
            }
            
            MathExt.CalculateRawValues(continuousIndex, valsFloor);
            MathExt.CalculateRawValues(continuousIndex, valsCeiling);

            decimal[] vals = valsCeiling;
            decimal[] incrementSteps = incrementStepsCeiling;
            if(Math.Abs(valsFloor[continuousIndex] - continuousValue) <= Math.Abs(valsCeiling[continuousIndex] - continuousValue))
            {
                vals = valsFloor;
                incrementSteps = incrementStepsFloor;
            }
            MathExt.CalculateRawValues(continuousIndex, incrementSteps);
            

            factor.Value = vals[continuousIndex];
            factor.IncrementStep = incrementSteps[continuousIndex];

            return incrementSteps;
        }
    }
}
