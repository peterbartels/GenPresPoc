using System;
using System.Linq.Expressions;
using System.Reflection;
using GenPres.Business.Domain.Calculation.Combination;
using GenPres.Business.Domain.PrescriptionDomain;
using GenPres.Business.Domain.UnitDomain;

namespace GenPres.Business.Domain.Calculation
{

    public class PrescriptionCalculator
    {
        public static decimal[] SubstanceIncrements = new decimal[1]{0.2m};
        public static decimal[] ComponentIncrements = new decimal[1] { 1 };

        public static void Calculate(Prescription prescription)
        {
            _setIncrements(prescription);

            var prescriptionTotal = new MultiplierCombination(
                prescription, 
                () => prescription.Total, () => prescription.Frequency, () => prescription.Quantity
            );
            prescriptionTotal.Calculate();
        }

        public static void _setIncrements(Prescription prescription)
        {
            decimal[] componentInc = new decimal[] { 1 };
            decimal[] freqInc = new decimal[] { 1 };

            _setIncrementValues(prescription, () => prescription.Frequency, freqInc, true);
            _setIncrementValues(prescription, () => prescription.Quantity, componentInc, true);
            _setIncrementValues(prescription, () => prescription.Total, componentInc, true);
        }

        private static void _setIncrementValues(Prescription p, Expression<Func<UnitValue>> property, decimal[] values, bool incrementStepping)
        {
            UnitValue unitValue = PropertyHelper.GetUnitValue(p, property);
            unitValue.Factor.IncrementSizes = values;
            unitValue.Factor.IncrementStepping = incrementStepping;
        }
    } 
}
