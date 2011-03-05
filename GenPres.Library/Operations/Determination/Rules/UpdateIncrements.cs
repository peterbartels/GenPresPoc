using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Csla;
using GenPres.Business;

namespace GenPres.Operations.Determination.Rules
{
    class UpdateIncrements : IDetermination
    {
        public bool Determine(Prescription prescription)
        {
            return false;
            bool AllowIncrementStepping = false;
            if (prescription.Solution) AllowIncrementStepping = true;

            IncrementDetermination id = new IncrementDetermination(new IncrementDeterminationEntity[]{
                new IncrementDeterminationEntity(
                    Dose.TotalProperty, new PropertyInfo<UnitValue>[] {Substance.DrugConcentrationProperty}, AllowIncrementStepping
                ),
                new IncrementDeterminationEntity(
                    Dose.TotalProperty, new PropertyInfo<UnitValue>[] {Dose.QuantityProperty}, true
                ),
                new IncrementDeterminationEntity(
                    Dose.QuantityProperty, new PropertyInfo<UnitValue>[] {Substance.DrugConcentrationProperty}, AllowIncrementStepping
                )/*,
                new IncrementDeterminationEntity(
                    Dose.TotalProperty, new PropertyInfo<UnitValue>[] {Substance.QuantityProperty, Drug.QuantityProperty}, AllowIncrementStepping, true
                )*/,
                new IncrementDeterminationEntity(
                    Dose.TotalProperty, new PropertyInfo<UnitValue>[] {Prescription.FrequencyProperty}, true
                ),
                new IncrementDeterminationEntity(
                    Dose.TotalProperty, new PropertyInfo<UnitValue>[] {Prescription.FrequencyProperty, Prescription.QuantityProperty}, AllowIncrementStepping
                ),
                new IncrementDeterminationEntity(
                    Prescription.TotalProperty, new PropertyInfo<UnitValue>[] {Prescription.FrequencyProperty}, true
                ),
                new IncrementDeterminationEntity(
                    Dose.TotalProperty, new PropertyInfo<UnitValue>[] {Prescription.TotalProperty}, AllowIncrementStepping
                ),
                new IncrementDeterminationEntity(
                    Dose.QuantityProperty, new PropertyInfo<UnitValue>[] {Prescription.QuantityProperty}, AllowIncrementStepping
                ),
                new IncrementDeterminationEntity(
                    Dose.RateProperty, new PropertyInfo<UnitValue>[] {Prescription.RateProperty}, AllowIncrementStepping
                ),
                new IncrementDeterminationEntity(
                    Dose.RateProperty, new PropertyInfo<UnitValue>[] {Substance.DrugConcentrationProperty}, AllowIncrementStepping
                ),
                new IncrementDeterminationEntity(
                    Dose.RateProperty, new PropertyInfo<UnitValue>[] {Substance.QuantityProperty, Drug.QuantityProperty}, AllowIncrementStepping, true
                ),
                new IncrementDeterminationEntity(
                    Dose.QuantityProperty, new PropertyInfo<UnitValue>[] {Substance.QuantityProperty, Drug.QuantityProperty}, AllowIncrementStepping, true
                ),
                new IncrementDeterminationEntity(
                    Dose.QuantityProperty, new PropertyInfo<UnitValue>[] {Prescription.TotalProperty, Prescription.FrequencyProperty}, AllowIncrementStepping, true
                )
            }, prescription);

            id.Execute();

            return false;
        }
    }
}
