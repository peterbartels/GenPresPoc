using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GenPres.Business.Calculation.Combination;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Business.Domain.Units;

namespace GenPres.Business.Calculation
{

    public class PrescriptionCalculator
    {
        public static decimal[] SubstanceIncrements = new decimal[1]{0.2m};
        public static decimal[] ComponentIncrements = new decimal[1] { 1 };

        private readonly List<ICalculationCombination> _combinations = new List<ICalculationCombination>();
        private readonly IPrescription _prescription;

        public PrescriptionCalculator(IPrescription prescription)
        {
            _prescription = prescription;
            SetIncrements(_prescription);
        }

        public void Start()
        {
            _combinations.Add(new MultiplierCombination(
                _prescription,
                () => _prescription.Total, () => _prescription.Frequency, () => _prescription.Quantity
            ));
            _combinations[0].Finish();
        }

        public void AddCalculation(ICalculationCombination combi)
        {
            _combinations.Add(combi);
        }

        public void ExecuteCalculation()
        {
            _combinations[0].Calculate();
        }

        public void ConvertCombinationsValuesToArray()
        {
            for (int i = 0; i < _combinations.Count; i++)
            {
                _combinations[i].ConvertCombinationsValuesToArray();    
            }
        }




        public void FinishCalculation()
        {
            _combinations[0].Finish();
        }

        public static void Calculate(IPrescription prescription)
        {
            var pc = new PrescriptionCalculator(prescription);
            pc.Start();
        }

        private static void SetIncrements(IPrescription prescription)
        {
            var componentInc = new decimal[] { 1 };
            var freqInc = new decimal[] { 1 };

            SetIncrementValues(prescription, () => prescription.Frequency, freqInc, true);
            SetIncrementValues(prescription, () => prescription.Quantity, componentInc, true);
            SetIncrementValues(prescription, () => prescription.Total, componentInc, true);
        }

        private static void SetIncrementValues(IPrescription p, Expression<Func<UnitValue>> property, decimal[] values, bool incrementStepping)
        {
            UnitValue unitValue = PropertyHelper.GetUnitValue(p, property);
            unitValue.Factor.IncrementSizes = values;
            unitValue.Factor.IncrementStepping = incrementStepping;
        }
    } 
}
