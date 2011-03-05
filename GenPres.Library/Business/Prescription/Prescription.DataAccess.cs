using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Data;
using Csla.Security;
using GenPres.Database;

namespace GenPres.Business
{
    public partial class Prescription : StateBusinessBase<Prescription>, IDataBusinessBase
    {
        [RunLocal]
        protected override void DataPortal_Create()
        {
            // TODO: load default values
            // omit this override if you have no defaults to set
            TPN = false;
            base.DataPortal_Create();
        }
        /*
        private void DataPortal_Fetch(Database.Prescription prescriptionData)
        {
            using (BypassPropertyChecks)
            {
                ApplyMapping(prescriptionData, this);
                Id = prescriptionData.Id;
                
                PrescriptionDoses = DoseCollection.GetDoseCollection(prescriptionData.Doses);
                for (int i = 0; i < prescriptionData.Doses.Count; i++)
                {
                    PrescriptionDoses.SetPrescription(this);
                    PrescriptionDoses.Add(DataPortal.FetchChild<Dose>(prescriptionData.Doses[i]));
                }
                
                if (prescriptionData.StartDate != null)
                {
                    StartDate = prescriptionData.StartDate.Value.ToString("yyyy\\/MM\\/dd");
                    StartTime= prescriptionData.StartDate.Value.ToString("HH:mm");
                }
                if (prescriptionData.EndDate != null)
                {
                    EndDate = prescriptionData.EndDate.Value.ToString("yyyy\\/MM\\/dd");
                    EndTime = prescriptionData.EndDate.Value.ToString("HH:mm");
                }

                Drug = DataPortal.FetchChild<Drug>(prescriptionData.Drug);
                Medicine = DataPortal.FetchChild<Medicine>(prescriptionData.Medicine);

                Quantity = DataPortal.Fetch<UnitValue>(prescriptionData.Quantity);
                Frequency = DataPortal.Fetch<UnitValue>(prescriptionData.Frequency);
                Total = DataPortal.Fetch<UnitValue>(prescriptionData.Total);
                Rate = DataPortal.Fetch<UnitValue>(prescriptionData.Rate);
                Time = DataPortal.Fetch<UnitValue>(prescriptionData.Time);
                AdjustWeight = DataPortal.Fetch<UnitValue>(prescriptionData.AdjustWeight);
                AdjustLength = DataPortal.Fetch<UnitValue>(prescriptionData.AdjustLength);
                //if(prescriptionData.Date != null) Date = prescriptionData.Date.Value;
                this.UpdateDoses(false);
            }
        }*/
        /*
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ContextManager<PrescriptionDataContext>.GetManager(
                DatabaseConnection.GetConnectionString(
                DatabaseConnection.DatabaseName.GENPRES), false))
            {
                    using (BypassPropertyChecks)
                {
                    Database.Prescription prescriptionData = new Database.Prescription();
                    
                    DataCache.Instance.PrescriptionId = null;
                    DataCache.Instance.prescriptionData = prescriptionData;
                    ApplyMapping(this, prescriptionData);

                    if (this.StartDate != "" && this.StartTime != "")
                        prescriptionData.StartDate = DateTime.Parse(this.StartDate.Split('T')[0] + "T" + this.StartTime);
                    else if (this.StartDate != "") prescriptionData.StartDate = DateTime.Parse(this.StartDate);

                    if (this.EndDate != "" && this.EndTime != "")
                        prescriptionData.EndDate = DateTime.Parse(this.EndDate.Split('T')[0] + "T" + this.EndTime);
                    else if (this.EndDate != "") prescriptionData.EndDate = DateTime.Parse(this.EndDate);

                    prescriptionData.Patient = DataCache.Instance.patientData;
                    Quantity = Quantity.Save();
                    prescriptionData.Quantity = DataCache.Instance.unitValueData;
                    Frequency = Frequency.Save();
                    prescriptionData.Frequency = DataCache.Instance.unitValueData;
                    Total = Total.Save();
                    prescriptionData.Total = DataCache.Instance.unitValueData;
                    Rate = Rate.Save();
                    prescriptionData.Rate = DataCache.Instance.unitValueData;
                    Time = Time.Save();
                    prescriptionData.Time = DataCache.Instance.unitValueData;
                    AdjustWeight = AdjustWeight.Save();
                    prescriptionData.AdjustWeight = DataCache.Instance.unitValueData;
                    AdjustLength = AdjustLength.Save();
                    prescriptionData.AdjustLength = DataCache.Instance.unitValueData;
                    FieldManager.UpdateChildren(this);
                    prescriptionData.Date = DateTime.Now;
                    prescriptionData.Drug = DataCache.Instance.drugData;
                    prescriptionData.Medicine = DataCache.Instance.medicineData;
                    ctx.DataContext.Prescriptions.InsertOnSubmit(prescriptionData);
                }
            }
        }*/
        /*
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var ctx = ContextManager<PrescriptionDataContext>.GetManager(
                DatabaseConnection.GetConnectionString(
                DatabaseConnection.DatabaseName.GENPRES), false))
            {
                if (this.Id == 0) DataPortal_Insert();

                Database.Prescription prescriptionData = ctx.DataContext.Prescriptions.Single<Database.Prescription>(c => c.Id == this.Id);
                //DataCache.Instance.PrescriptionId = this.Id;
                
                if (this.StartDate != "" && this.StartTime != "")
                    prescriptionData.StartDate = DateTime.Parse(this.StartDate.Split('T')[0] + "T" + this.StartTime);
                else if (this.StartDate != "") prescriptionData.StartDate = DateTime.Parse(this.StartDate);

                if (this.EndDate != "" && this.EndTime != "")
                    prescriptionData.EndDate = DateTime.Parse(this.EndDate.Split('T')[0] + "T" + this.EndTime);
                else if (this.EndDate != "") prescriptionData.EndDate = DateTime.Parse(this.EndDate);
                
                ApplyMapping(this, prescriptionData);
                Quantity.Save();
                Frequency.Save();
                Total.Save();
                Rate.Save();
                Time.Save();
                AdjustWeight.Save();
                AdjustLength.Save();
                FieldManager.UpdateChildren(this);
                ctx.DataContext.SubmitChanges();
                
            }
        }*/
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(new SingleCriteria<Prescription, int>(this.Id));
        }
        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SingleCriteria<Prescription, int> criteria)
        {
            // TODO: delete values
        }
    }
}
