using System.Collections.Generic;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Business.Calculation
{
    public class PrescriptionCalculator
    {
        private Prescription _prescription;
        public PrescriptionCalculator(Prescription prescription)
        {
            _prescription = prescription;
            setCombinations();
        }
        
        private readonly List<ICalculationCombination> _calculationCombination = new List<ICalculationCombination>();

        public void AddCombination(ICalculationCombination combination)
        {
            _calculationCombination.Add(combination);
        }

        public void Calculate()
        {
            for (int i = 0; i < _calculationCombination.Count; i++)
            {
                _calculationCombination[i].Calculate();
                _calculationCombination[i].Finish();
            }
        }

        private void setCombinations()
        {
            AddCombination(new CalculationCombination(_prescription, () => _prescription.FirstDose.Total, () => _prescription.Frequency, () => _prescription.FirstDose.Quantity));
            AddCombination(new CalculationCombination(_prescription, () => _prescription.Total, () => _prescription.Frequency, () => _prescription.Quantity));
            AddCombination(new CalculationCombination(_prescription, () => _prescription.FirstSubstance.Quantity, () => _prescription.FirstSubstance.DrugConcentration, () => _prescription.Drug.Quantity));
            AddCombination(new CalculationCombination(_prescription, () => _prescription.FirstDose.Quantity, () => _prescription.Quantity, () => _prescription.FirstSubstance.DrugConcentration));
            AddCombination(new CalculationCombination(_prescription, () => _prescription.FirstDose.Total, () => _prescription.Total, () => _prescription.FirstSubstance.DrugConcentration));
            AddCombination(new CalculationCombination(_prescription, () => _prescription.FirstDose.Rate, () => _prescription.Rate, () => _prescription.FirstSubstance.DrugConcentration));
            AddCombination(new CalculationCombination(_prescription, () => _prescription.FirstDose.Quantity, () => _prescription.FirstDose.Rate, () => _prescription.Duration));
            AddCombination(new CalculationCombination(_prescription, () => _prescription.Quantity, () => _prescription.Rate, () => _prescription.Duration));
        }
    }
}