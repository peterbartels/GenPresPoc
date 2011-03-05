using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business;
//using GenPres.Calculator.Rectification;
//using GenPres.PrescriptionCalculator.Rectification.CalculationTypes;
//using GenPres.PrescriptionCalculator.Rectification.RectificationTypes;

namespace GenPres.Operations.Determination.Rules
{
    /*
     * This calss is used to determine which Component can be added
     * when a solution is used
     */
    class SetComponents : IDetermination
    {
        private Prescription prescription;
        public bool Determine(Prescription p)
        {
            prescription = p;
            Substance currentSubstance = prescription.Doses[0].Substance;
            Dose currentDose = prescription.Doses[0];

            if (!prescription.Solution) return false;
            if (prescription.Drug.SolutionType == "") return false;

            Component subComponent;
            Substance subComponentSubstance;
            Dose subDose;

            if (prescription.Drug.Components.Count > 1)
            {
                subComponent = prescription.Drug.Components[1];
                subComponentSubstance = subComponent.Substances[0];
                subDose = prescription.Doses[1];
            }else{
                subComponent = prescription.Drug.NewComponent();
                subComponentSubstance = subComponent.NewSubstance();
                subDose = prescription.NewDose();
                prescription.UpdateDoses(false);
            }

            subComponent.Quantity.Unit = "ml";
            subComponent.DrugConcentration.Unit = "ml";
            subComponent.DrugConcentration.Total = "ml";

            prescription.Drug.Components[0].Substances[0].ComponentConcentration.Unit = "mg";
            prescription.Drug.Components[0].Substances[0].ComponentConcentration.Total = "ml";

            subComponent.ComponentIncrement = 0.0001m;

            switch (prescription.Drug.SolutionType)
            {
                case "FZ":
                    subComponentSubstance.SubstanceName = "Natrium";
                    subComponentSubstance.Quantity.Unit = "mmol";
                    subComponentSubstance.ComponentConcentration.BaseValue = 0.15m;
                    subComponentSubstance.ComponentConcentration.Unit = "mmol";
                    subComponentSubstance.ComponentConcentration.Total = "ml"; 
                    subComponentSubstance.DrugConcentration.Unit = "mmol";
                    subComponentSubstance.DrugConcentration.Total = "ml";
                    subDose.Quantity.Unit = "mmol";
                    subDose.Rate.Unit = "mmol";
                    subDose.Rate.Time = "uur";
                    subDose.Total.Unit = "mmol";
                    subDose.Total.Time = "dag";
                    subComponent.Substances[0].SubstanceIncrements = new Decimal[] { 0.000015m };
                    break;
                case "Glucose 5%":
                    subComponentSubstance.SubstanceName = "Glucose";
                    subComponentSubstance.Quantity.Unit = "ml";
                    subComponentSubstance.ComponentConcentration.BaseValue = 5;
                    subComponentSubstance.ComponentConcentration.Unit = "mg";
                    subComponentSubstance.ComponentConcentration.Total = "ml";
                    subComponentSubstance.DrugConcentration.Unit = "mg";
                    subComponentSubstance.DrugConcentration.Total = "ml";
                    subDose.Quantity.Unit = "mg";
                    subDose.Rate.Unit = "mg";
                    subDose.Rate.Time = "uur";
                    subDose.Total.Unit = "mg";
                    subDose.Total.Time = "dag";
                    subComponent.Substances[0].SubstanceIncrements = new Decimal[] { 0.005m };
                    break;
                case "Glucose 10%":
                    subComponentSubstance.SubstanceName = "Glucose";
                    subComponentSubstance.Quantity.Unit = "ml";
                    subComponentSubstance.ComponentConcentration.BaseValue = 10;
                    subComponentSubstance.ComponentConcentration.Unit = "mg";
                    subComponentSubstance.ComponentConcentration.Total = "ml";
                    subComponentSubstance.DrugConcentration.Unit = "mg";
                    subComponentSubstance.DrugConcentration.Total = "ml";
                    subDose.Quantity.Unit = "mg";
                    subDose.Rate.Unit = "mg";
                    subDose.Rate.Time = "uur";
                    subDose.Total.Unit = "mg";
                    subDose.Total.Time = "dag";
                    subComponent.Substances[0].SubstanceIncrements = new Decimal[] { 0.01m };
                    break;
                case "":
                    subComponentSubstance.Quantity.Value = 0;
                    subComponentSubstance.ComponentConcentration.Value = 0;
                    break;
            }

            if (prescription.Drug.Quantity.BaseValue > 0)
            {
                subComponent.Quantity.BaseValue = prescription.Drug.Quantity.BaseValue - 0.0001m;
            }
            return false;
        }
    }
}
