using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GenPres.Business.Calculation.Old.Combination;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Business.Domain.Units;
using GenPres.Business.Util;

namespace GenPres.Business.Calculation.Old
{

    public class OldPrescriptionCalculator
    {
        public decimal[] SubstanceIncrements = new decimal[1]{0.24m};
        public decimal[] ComponentIncrements = new decimal[1] { 1 };

        private readonly List<Combination.ICalculationCombination> _combinations = new List<Combination.ICalculationCombination>();
        private readonly Prescription _prescription;

        public static List<string> PropertySequence()
        {
            var prescription = Prescription.NewPrescription();
            return new List<string>()
            {
                PropertyHelper.MemberName(() => prescription.Doses[0].Total),
                PropertyHelper.MemberName(() => prescription.Doses[0].Quantity),
                PropertyHelper.MemberName(() => prescription.Drug.Components[0].Substances[0].DrugConcentration),
                PropertyHelper.MemberName(() => prescription.Duration),
                PropertyHelper.MemberName(() => prescription.Drug.Components[0].Substances[0].Quantity),
                PropertyHelper.MemberName(() => prescription.Drug.Components[0].Quantity),
                PropertyHelper.MemberName(() => prescription.Doses[0].Rate),
                PropertyHelper.MemberName(() => prescription.Drug.Quantity),
                PropertyHelper.MemberName(() => prescription.Total),
                PropertyHelper.MemberName(() => prescription.Quantity),
                PropertyHelper.MemberName(() => prescription.Rate),
            };
        }

        public OldPrescriptionCalculator(Prescription prescription)
        {
            _prescription = prescription;
            SetIncrements(_prescription);
        }

        public void SetCombinations()
        {
            if (_combinations.Count != 0) return;
            
            _combinations.Add(new MultiplierCombination(
                _prescription,
                () => _prescription.Total, () => _prescription.Frequency, () => _prescription.Quantity
            ));

            _combinations.Add(new MultiplierCombination(
               _prescription,
               () => _prescription.Doses[0].Total, () => _prescription.Frequency, () => _prescription.Doses[0].Quantity
           ));
        }

        public void Start()
        {
            SetCombinations();
            Execute();

            for (int i = 0; i < _combinations.Count; i++) _combinations[i].Finish();
        }

        public void AddCalculation(Combination.ICalculationCombination combi)
        {
            _combinations.Add(combi);
        }

        private void CorrectPropertyIncrements(int index)
        {
            
        }

        public void Execute()
        {
            for (int i = 0; i < _combinations.Count; i++)
                _combinations[i].Calculate();   
        }

        public void ConvertCombinationsValuesToArray()
        {
            for (int i = 0; i < _combinations.Count; i++)
            {
                _combinations[i].ConvertCombinationsValuesToArray();    
            }
        }

        public void Finish()
        {
            _combinations[0].Finish();
        }

        public static void Calculate(Prescription prescription)
        {
            var pc = new OldPrescriptionCalculator(prescription);
            pc.Start();
        }

        private static void SetIncrements(Prescription prescription)
        {
            var componentInc = new decimal[] { 1 };
            var substanceInc = new decimal[] { 0.24m };
            var freqInc = new decimal[] { 1 };

            SetIncrementValues(prescription, () => prescription.Frequency, freqInc, true);
            SetIncrementValues(prescription, () => prescription.Quantity, componentInc, true);
            SetIncrementValues(prescription, () => prescription.Total, componentInc, true);

            SetIncrementValues(prescription, () => prescription.Doses[0].Quantity, substanceInc, true);
            SetIncrementValues(prescription, () => prescription.Doses[0].Total, substanceInc, true);
        }

        private static void SetIncrementValues(Prescription p, Expression<Func<UnitValue>> property, decimal[] values, bool incrementStepping)
        {
            UnitValue unitValue = PropertyHelper.GetUnitValue(property);
            unitValue.Factor.IncrementSizes = values;
            unitValue.Factor.IncrementStepping = incrementStepping;
        }

        

        public void CheckStates()
        {
            SetCombinations();
            for (int i = 0; i < _combinations.Count; i++)
            {
                var combinationsCheck = (from c in _combinations where c.GetUserCount() == 3 select c);
                
                var sequence = PropertySequence();

                for (int j = 0; j < sequence.Count; j++)
                {
                    var property = _combinations[i].GetPropertyByName(sequence[j]);
                    if(property != null)
                    {
                        if (!property.ChangedByUser)
                        {
                            property.UIState = "calculated";
                            property.Value = 0;
                            break;
                        }
   
                    }
                }
            }
        }
    } 
}
