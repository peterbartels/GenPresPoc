using System;
using System.Collections.Generic;
using System.Text;
using Csla;
using GenPres.Operations.Determination;
using GenPres.Operations;

namespace GenPres.Business
{
    public partial class Prescription : StateBusinessBase<Prescription>, IDataBusinessBase
    {
        #region DoseOperations
        public void UpdateDoses(bool addNew)
        {
            int doseIt = 0;
            for (int c = 0; c < Drug.Components.Count; c++)
            {
                Drug.Components[c].Substances.SetComponent(Drug.Components[c]);
                for (int s = 0; s < Drug.Components[c].Substances.Count; s++)
                {
                    Dose d;
                    Substance sub = Drug.Components[c].Substances[s];
                    sub.SetComponent(Drug.Components[c]);
                    if (addNew || doseIt == Doses.Count)
                    {
                        d = this.NewDose();
                        d.Substance = sub;
                        d.Quantity.Unit = sub.Quantity.Unit;
                        d.Total.Unit = sub.Quantity.Unit;
                        d.Total.Time = "dag";
                    }
                    else
                    {
                        d = Doses[doseIt];
                        if (d.Substance == null) d.Substance = sub;
                        //DBREFACT d.Substance.SubstanceData = sub.SubstanceData;
                    }
                    doseIt++;
                }
            }
        }
        #endregion

        public bool prepareSave = false;

        #region Determination,Rectification,Calculation,Validation
        
        public void Calculate()
        {
            /* Templates don't need rectification */
            //if (IsTemplate) return;
            CalculationProcess.Start(this);

            /* Get a new Rectification Object to rectify prescription */
            //PrescriptionRectification pr = new PrescriptionRectification(this);
            //pr.StartProcess();
            /* Rectify using PrescriptionRectification */
            //pr.re();
        }

        public void PreRectifcation_Determine()
        {
            /* Collection container of rules */
            List<IDetermination> determinationTypes = new List<IDetermination>();
            
            /* Rules: */
            determinationTypes.Add(new GenPres.Operations.Determination.Rules.ToAdjust());
            determinationTypes.Add(new GenPres.Operations.Determination.Rules.RetrieveIncrements());
            determinationTypes.Add(new GenPres.Operations.Determination.Rules.Continuous());
            determinationTypes.Add(new GenPres.Operations.Determination.Rules.InfusionTime());
            determinationTypes.Add(new GenPres.Operations.Determination.Rules.Solution());
            determinationTypes.Add(new GenPres.Operations.Determination.Rules.SetComponents());

            PrescriptionDetermination pd = new PrescriptionDetermination(this);
            pd.Determine(determinationTypes);
        }


        public void TPN_Determine(Dictionary<string, Dictionary<string, decimal>> solutions)
        {
            /* Collection container of rules */
            List<IDetermination> determinationTypes = new List<IDetermination>();
            
            //determinationTypes.Add(new Determination.Rules.Continuous());
            determinationTypes.Add(new GenPres.Operations.Determination.Rules.TPNSolution(solutions));
            
            /* Initiate using PrescriptionDetermination */
            PrescriptionDetermination pd = new PrescriptionDetermination(this);
            pd.Determine(determinationTypes);
        }

        public void ClearPrescription_Determine()
        {
            /* Collection container of rules */
            List<IDetermination> determinationTypes = new List<IDetermination>();

            /* Rules: */
            determinationTypes.Add(new GenPres.Operations.Determination.Rules.ToAdjust());
            
            /* Initiate using PrescriptionDetermination */
            PrescriptionDetermination pd = new PrescriptionDetermination(this);
            pd.Determine(determinationTypes);
        }

        public void PostRectification_Determine()
        {
            /* Collection container of rules */
            List<IDetermination> determinationTypes = new List<IDetermination>();

            /* Rules: */
            
            if (!IsTemplate)
            {
                determinationTypes.Add(new GenPres.Operations.Determination.Rules.SetStates());
                determinationTypes.Add(new GenPres.Operations.Determination.Rules.UpdateIncrements());
            }

            /* Initiate using PrescriptionDetermination */
            PrescriptionDetermination pd = new PrescriptionDetermination(this);
            pd.Determine(determinationTypes);
        }

        public void PostCalculation_Determine()
        {
            /* Collection container of rules */
            List<IDetermination> determinationTypes = new List<IDetermination>();

            /* Rules: */
            determinationTypes.Add(new GenPres.Operations.Determination.Rules.FromAdjust());

            /* Initiate using PrescriptionDetermination */
            PrescriptionDetermination pd = new PrescriptionDetermination(this);
            pd.Determine(determinationTypes);
        }
        #endregion

        #region Adjust
        public decimal GetAdjustValue(string unit)
        {
            switch (unit.ToLower().Trim())
            {
                case "m2": return AdjustBSA.BaseValue;
                case "kg": return AdjustWeight.BaseValue;
                case "cm": return AdjustLength.BaseValue;
                default: throw new Exception("Cannot find unit for determining AdjustValue: " + unit);
            }
        }
        #endregion
    }
}
