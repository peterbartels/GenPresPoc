using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Data;
using GenPres.Database;


namespace GenPres.Business
{
    public class PrescriptionSaveProcess
    {
        private Prescription prescription;
        
        public PrescriptionSaveProcess(Prescription p)
        {
            prescription = p;
        }

        public static void SavePatient(Patient patient)
        {
            using (var ctx = ContextManager<PrescriptionDataContext>.GetManager(
                DatabaseConnection.GetConnectionString(
                DatabaseConnection.DatabaseName.GENPRES), false))
            {
                patient = patient.Save();
                ctx.DataContext.SubmitChanges();
            }
        }

        public void Save(int id, Patient patient)
        {
            // No need to set when a template is configured;
            if (prescription.IsTemplate)
            {
                prescription.AdjustWeight.Unit = "kg";
                prescription.AdjustLength.Unit = "cm";
                prescription.AdjustWeight.Value = 0.0m;
                prescription.AdjustLength.Value = 0.0M;
                prescription.AdjustBSA.Value = 0.0M;
            }
            else
            {
                if (prescription.AdjustWeight.BaseValue > 0)
                {
                    patient.Weight = prescription.AdjustWeight.BaseValue;
                }
                if (prescription.AdjustLength.BaseValue > 0)
                {
                    patient.Length = prescription.AdjustLength.BaseValue;
                }
            }
            //prescription.Patient = patient;

            using (var ctx = ContextManager<PrescriptionDataContext>.GetManager(
                DatabaseConnection.GetConnectionString(
                DatabaseConnection.DatabaseName.GENPRES), false))
            {
                patient = patient.Save();
                prescription = prescription.Save();
                ctx.DataContext.SubmitChanges();
                //if(DataCache.Instance.prescriptionData != null) prescription.Id = DataCache.Instance.prescriptionData.Id;
            }
        }
    }
}

