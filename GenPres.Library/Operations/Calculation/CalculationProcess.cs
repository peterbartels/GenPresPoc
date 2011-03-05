using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business;
using GenPres.Operations.Calculation;

namespace GenPres.Operations
{
    public class CalculationProcess
    {
        public static void Start(Prescription prescription, Medicine medicine)
        {
            PropertyManager propertyManager = PropertyManager.Instance;
            propertyManager.SetPrescription(prescription);

            decimal[] componentInc = new decimal[] { medicine.ComponentIncrement.BaseValue };
            decimal[] substanceInc = new decimal[] { medicine.DoseIncrement.BaseValue };

            AbstractCombination medicineCombination = new AbstractCombination(
                Medicine.DoseIncrementProperty, Medicine.QuantityProperty, Medicine.ComponentIncrementProperty
            );
            medicineCombination.Calculate();

        }
        public static void Start(Prescription prescription)
        {
            PropertyManager propertyManager = PropertyManager.Instance;
            propertyManager.SetPrescription(prescription);

            if (prescription.Drug.Components[0].ComponentIncrement == 0) return;
            if (prescription.Drug.Components[0].Substances[0].SubstanceIncrements.Length == 0) return;

            CalculationProcess._setFactors(prescription);

            List<ICalculationCombination> pList = new List<ICalculationCombination>();
            propertyManager.propertyCombinationList = pList;

            ContinuousCombination SubstanceQuantity = new ContinuousCombination(
                Substance.DrugConcentrationProperty, //==> ContinuousProperty
                Substance.QuantityProperty, Substance.DrugConcentrationProperty, Drug.QuantityProperty
            );
            ContinuousCombination PrescriptionRate = new ContinuousCombination(
                Prescription.TimeProperty, //==> ContinuousProperty
                Prescription.QuantityProperty, Prescription.TimeProperty, Prescription.RateProperty
            );

            MultiplierCombination PrescriptionTotal = new MultiplierCombination(
                Prescription.TotalProperty, Prescription.FrequencyProperty, Prescription.QuantityProperty
            );
            MultiplierCombination DoseTotalConcentration = new MultiplierCombination(
                Dose.TotalProperty, Prescription.TotalProperty, Substance.DrugConcentrationProperty
            );
            MultiplierCombination DoseQuantityConcentration = new MultiplierCombination(
                Dose.QuantityProperty, Prescription.QuantityProperty, Substance.DrugConcentrationProperty
            );
            MultiplierCombination DoseTotal = new MultiplierCombination(
               Dose.TotalProperty, Prescription.FrequencyProperty, Dose.QuantityProperty
            );
            MultiplierCombination DoseRateConcentration = new MultiplierCombination(
                Dose.RateProperty, Prescription.RateProperty, Substance.DrugConcentrationProperty
            );

            pList.Add(SubstanceQuantity);
            pList.Add(DoseTotalConcentration);
            pList.Add(DoseQuantityConcentration);
            pList.Add(PrescriptionTotal);
            pList.Add(PrescriptionRate);
            pList.Add(DoseRateConcentration);
            pList.Add(DoseTotal);
            
            DoseQuantityConcentration.SetDependency(Dose.QuantityProperty, Substance.DrugConcentrationProperty);
            DoseTotalConcentration.SetDependency(Dose.TotalProperty, Substance.DrugConcentrationProperty);
            DoseRateConcentration.SetDependency(Dose.RateProperty, Substance.DrugConcentrationProperty);
            
            PrescriptionTotal.Calculate();
            SubstanceQuantity.Calculate();
            DoseQuantityConcentration.Calculate();
            DoseTotalConcentration.Calculate();
            PrescriptionRate.Calculate();
            DoseRateConcentration.Calculate();

            DoseTotal.Calculate();

            propertyManager.incrementCombinationList = new List<ICalculationCombination>()
            {
                SubstanceQuantity, PrescriptionTotal, DoseTotal, DoseTotalConcentration, DoseQuantityConcentration, DoseRateConcentration
            };

            propertyManager.CorrectPropertyValues();
            
        }

        private static void _setFactors(Prescription p)
        {
            decimal[] componentInc = new decimal[] { p.Drug.Components[0].ComponentIncrement };
            decimal[] substanceInc = p.Drug.Components[0].Substances[0].SubstanceIncrements;

            _setFactor(p.Quantity, Prescription.QuantityProperty, componentInc, true);
            _setFactor(p.Total, Prescription.TotalProperty, componentInc, true);
            _setFactor(p.Rate, Prescription.RateProperty, componentInc, true);
            _setFactor(p.Frequency, Prescription.FrequencyProperty, new decimal[] { 1 }, true);

            _setFactor(p.Drug.Components[0].Substances[0].DrugConcentration, Substance.DrugConcentrationProperty, new decimal[] { }, true);
            _setFactor(p.Time, Prescription.TimeProperty, new decimal[] { }, true);

            _setFactor(p.Drug.Components[0].Substances[0].Quantity, Substance.QuantityProperty, substanceInc, p.Solution);
            _setFactor(p.Drug.Quantity, Drug.QuantityProperty, componentInc, true);

            if (p.Solution) substanceInc = new decimal[] { };
            _setFactor(p.Doses[0].Quantity, Dose.QuantityProperty, substanceInc, true);
            _setFactor(p.Doses[0].Total, Dose.TotalProperty, substanceInc, true);
            _setFactor(p.Doses[0].Rate, Dose.RateProperty, substanceInc, true);

        }
        private static void _setFactor(UnitValue uv, Csla.PropertyInfo<UnitValue> type, decimal[] increments, bool IncrementStepping)
        {
            uv.Factor = new Factor(type);
            uv.Factor.IncrementStepping = IncrementStepping;
            uv.Factor.IncrementSizes = increments;
        }
    } 
}
