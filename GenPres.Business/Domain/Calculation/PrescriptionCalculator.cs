using System;
using System.Collections.Generic;
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

        private List<ICalculationCombination> _combinations = new List<ICalculationCombination>();

        public void Start(IPrescription prescription)
        {
            _setIncrements(prescription);

            _combinations.Add(new MultiplierCombination(
                prescription,
                () => prescription.Total, () => prescription.Frequency, () => prescription.Quantity
            ));

            _combinations[0].Calculate();
            _combinations[0].Finish();
        }

        public static void Calculate(IPrescription prescription)
        {
            var pc = new PrescriptionCalculator();
            pc.Start(prescription);
        }

        public static void _setIncrements(IPrescription prescription)
        {
            decimal[] componentInc = new decimal[] { 1 };
            decimal[] freqInc = new decimal[] { 1 };

            _setIncrementValues(prescription, () => prescription.Frequency, freqInc, true);
            _setIncrementValues(prescription, () => prescription.Quantity, componentInc, true);
            _setIncrementValues(prescription, () => prescription.Total, componentInc, true);
        }

        private static void _setIncrementValues(IPrescription p, Expression<Func<UnitValue>> property, decimal[] values, bool incrementStepping)
        {
            UnitValue unitValue = PropertyHelper.GetUnitValue(p, property);
            unitValue.Factor.IncrementSizes = values;
            unitValue.Factor.IncrementStepping = incrementStepping;
        }
    } 
}
