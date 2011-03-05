using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business;

namespace GenPres.Operations.Calculation
{
    public class IncrementDetermination
    {
        PropertyManager pm = PropertyManager.Instance;
        ICalculationCombination _combi;

        public IncrementDetermination(ICalculationCombination combi)
        {
            _combi = combi;
        }
        public void SetSingleValues()
        {
            if (!pm.IsPrescriptionSolution())
            {
                if (_combi.GetNotSetCount() == 2 && pm.GetValue(_combi.GetAt(0)) > 0)
                {
                    Factor productFactor = pm.GetFactor(_combi.GetAt(0));
                    Factor firstDivisorFactor = pm.GetFactor(_combi.GetAt(1));

                    List<decimal> incrementValues = new List<decimal>();
                    List<decimal> incrementValues2 = new List<decimal>();

                    bool containsFirstIncrementStep = true;
                    foreach (decimal incrementValue in pm.GetSubstanceIncrementValues())
                    {
                        decimal checkResult = MathExt.FixPrecision((pm.GetValue(_combi.GetAt(0)) / productFactor.GetIncrementValue(1, incrementValue)));
                        if (productFactor.GetIncrementStep(incrementValue) == 1 || (_combi.HasDependent() && MathExt.Mod(checkResult, 1) == 0))
                        {
                            incrementValues.Add(firstDivisorFactor.GetIncrementValue(checkResult, incrementValue));
                        }
                        else if (MathExt.Mod(checkResult, 1) == 0) containsFirstIncrementStep = false;
                    }
                    if (incrementValues.Distinct().ToList().Count == 1 && containsFirstIncrementStep)
                    {
                        pm.TrySetValue(_combi.GetAt(1), incrementValues[0]);
                        _combi.Calculate();
                        _combi.CalculateSibblings(0);
                        _combi.CalculateSibblings(1);
                        _combi.CalculateSibblings(2);
                    }
                }
            }
        }
        public void SetIncrements(List<decimal> incrementValues)
        {
            pm.GetUnitValue(_combi.GetAt(0), 0).Increments = incrementValues.ToArray();
            pm.GetUnitValue(_combi.GetAt(0), 0).AllowIncrementStep = true;
            if (_combi.HasDependent() && !pm.IsPrescriptionSolution())
                pm.GetUnitValue(_combi.GetAt(0), 0).AllowIncrementStep = false;

            pm.GetUnitValue(_combi.GetAt(0), 0).UpdateIncrements(pm.GetPrescription());
        }
        public void DetermineIncrements()
        {
            if (_combi is ContinuousCombination) return;
            List<decimal> incrementValues = new List<decimal>();
            if (!pm.IsPrescriptionSolution() || !_combi.HasDependent())
            {
                incrementValues = GetValuesByIncrementSteps();
            }
            else
            {
                incrementValues = GetContinuousIncrementSteps();
            }
            
            if (incrementValues.Count == 0) return;

            SetIncrements(incrementValues);
            if (pm.IsPrescriptionSolution() && ((AbstractCombination)_combi).DependencyCombinationCanBeUsedForDetermination(Substance.DrugConcentrationProperty, true))
            {
                CalculateDependent();
            }
            //pm.GetFactor(combi.GetAt(0)).IncrementSizes = incrementValues.ToArray();
        }

        public void CalculateDependent()
        {
            if (pm.IsPrescriptionSolution() && _combi.HasDependent())
            {
                AbstractCombination p = (AbstractCombination)(
                    from i in pm.incrementCombinationList
                    where
                        i.Contains(_combi.GetAt(0)) && !(i is ContinuousCombination) && !((AbstractCombination)i).HasDependent()
                    select
                        i
                    ).FirstOrDefault<ICalculationCombination>();
                if (p != null)
                {
                    IncrementDetermination id = new IncrementDetermination(p);
                    id.DetermineIncrements();
                }
            }
        }

        public List<decimal> GetValuesByIncrementSteps()
        {
            List<decimal> incrementValues = new List<decimal>();
            foreach (decimal incrementValue in pm.GetSubstanceIncrementValues())
            {
                if (((AbstractCombination)_combi).CanBeUsedForIncrementDetermination(_combi.GetAt(1)))
                {
                    decimal step = pm.GetFactor(_combi.GetAt(1)).GetIncrementStep(incrementValue);
                    decimal incrementVal = ((AbstractCombination)_combi).GetIncrementValue(0, 1, incrementValue);
                    if (step > 0)
                    {
                        incrementValues.Add(step * incrementVal);
                    }
                }
                if (((AbstractCombination)_combi).CanBeUsedForIncrementDetermination(_combi.GetAt(2)) && _combi.GetAt(2) != Substance.DrugConcentrationProperty)
                {
                    decimal step = pm.GetFactor(_combi.GetAt(2)).GetIncrementStep(incrementValue);
                    decimal incrementVal = ((AbstractCombination)_combi).GetIncrementValue(0, 1, incrementValue);
                    if (step > 0)
                    {
                        incrementValues.Add(step * incrementVal);
                    }
                }
            }
            return incrementValues;
        }

        public List<decimal> GetContinuousIncrementSteps()
        {
            List<decimal> incrementValues = new List<decimal>();
            if (((AbstractCombination)_combi).DependencyCombinationCanBeUsedForDetermination(Substance.DrugConcentrationProperty, true))
            {
                incrementValues.Add(pm.GetBaseValue(Substance.DrugConcentrationProperty) * pm.GetComponentIncrementValue());
                return incrementValues;
            }
            else
            {
                if (pm.GetBaseValue(Drug.QuantityProperty) > 0)
                {
                    foreach (decimal incrementValue in pm.GetSubstanceIncrementValues())
                    {
                        incrementValues.Add((incrementValue / pm.GetBaseValue(Drug.QuantityProperty)) * pm.GetComponentIncrementValue());
                    }
                    pm.GetFactor(_combi.GetAt(0)).IncrementSizes = incrementValues.ToArray();
                    return GetValuesByIncrementSteps();
                }
            }

            incrementValues = GetValuesByIncrementSteps();
            return incrementValues;
        }
    }
}
