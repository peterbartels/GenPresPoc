using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Csla;
using Csla.Data;
using Csla.Security;
using GenPres.Database;

namespace GenPres.Business
{
    /// <summary>
    /// Component class (EditableChild object)
    /// </summary>
    [Serializable]
    public class Dose : StateBusinessBase<Dose>, IDataBusinessBase
    {

        #region Business Methods

        public override object GetDataAccessObject()
        {
            if (_cachedAccessObject == null) _cachedAccessObject = new GenPres.Database.Dose();
            return _cachedAccessObject;
        }

        internal static PropertyInfo<string> NameProperty = RegisterProperty(typeof(Dose), new PropertyInfo<string>("Name"));
        public string Name
        {
            get
            {
                string value = GetProperty(NameProperty);
                return value;
            }
            set
            {
                SetProperty(NameProperty, value);
            }
        }

        internal static PropertyInfo<int> IdProperty = RegisterProperty(typeof(Dose), new PropertyInfo<int>("Id"));
        public int Id
        {
            get
            {
                int value = GetProperty(IdProperty);
                return value;
            }
            set
            {
                SetProperty(IdProperty, value);
            }
        }

        public Substance Substance;

        internal static PropertyInfo<UnitValue> QuantityProperty = RegisterProperty(typeof(Dose), new PropertyInfo<UnitValue>("Quantity"));
        public UnitValue Quantity
        {
            get
            {
                UnitValue value = GetProperty(QuantityProperty);
                if (value == null)
                {
                    value = DataPortal.CreateChild<UnitValue>();
                    LoadProperty(QuantityProperty, value);
                }
                return value;
            }
            set
            {
                if (Quantity.Id > 0) value.Id = Quantity.Id;
                SetProperty(QuantityProperty, value);
            }
        }

        internal static PropertyInfo<UnitValue> RateProperty = RegisterProperty(typeof(Dose), new PropertyInfo<UnitValue>("Rate"));
        public UnitValue Rate
        {
            get
            {
                UnitValue value = GetProperty<UnitValue>(RateProperty);
                if (value == null)
                {
                    value = DataPortal.CreateChild<UnitValue>();
                    LoadProperty(RateProperty, value);
                }
                return value;
            }
            set {
                if (Rate.Id > 0) value.Id = Rate.Id;
                SetProperty(RateProperty, value); }
        }

        internal static PropertyInfo<UnitValue> TotalProperty = RegisterProperty(typeof(Dose), new PropertyInfo<UnitValue>("Total"));
        public UnitValue Total
        {
            get
            {
                UnitValue value = GetProperty<UnitValue>(TotalProperty);
                if (value == null)
                {
                    value = DataPortal.CreateChild<UnitValue>();
                    LoadProperty(TotalProperty, value);
                }
                return value;
            }
            set {
                if (Total.Id > 0) value.Id = Total.Id;
                SetProperty(TotalProperty, value); }
        }

        #endregion

        private Prescription GetPrescription()
        {
            try
            {
                DoseCollection doseCollection = (DoseCollection)this.Parent;
                return doseCollection.GetPrescription();
            }
            catch 
            {
                if (Parent == null)
                {
                    throw new Exception("Parent of dose is undefined (should be prescription).");
                }
                else
                {
                    throw new Exception("Cannot cast Parent to DoseCollection. Parent is of type: " + Parent.GetType());
                }
            }
        }
        
        #region Data Access

        /*
        protected override void Child_Create()
        {
            // TODO: load default values
            // omit this override if you have no defaults to set
            Quantity = DataPortal.CreateChild<UnitValue>();
            Total = DataPortal.CreateChild<UnitValue>();
            Rate = DataPortal.CreateChild<UnitValue>();
            base.Child_Create();
        }
        private void Child_Fetch(Database.Dose doseData)
        {
            using (BypassPropertyChecks)
            {
                Quantity = DataPortal.Fetch<UnitValue>(doseData.Quantity);
                Total = DataPortal.Fetch<UnitValue>(doseData.Total);
                Rate = DataPortal.Fetch<UnitValue>(doseData.Rate);
                Id = doseData.Id;
            }
        }
        private void Child_Insert(Prescription prescription)
        {
            using (var ctx = ContextManager<PrescriptionDataContext>.GetManager(
                DatabaseConnection.GetConnectionString(
                DatabaseConnection.DatabaseName.GENPRES), false))
            {
                using (BypassPropertyChecks)
                {

                    Database.Dose doseData = new Database.Dose();
                    //No properties to map?
                    //ApplyMapping(this, doseData);
                    
                    Quantity = Quantity.Save();
                    doseData.Quantity = DataCache.Instance.unitValueData;

                    Rate = Rate.Save();
                    doseData.Rate = DataCache.Instance.unitValueData;

                    Total = Total.Save();
                    doseData.Total = DataCache.Instance.unitValueData;

                    doseData.Substance = DataCache.Instance.substanceData;
                    doseData.Prescription = DataCache.Instance.prescriptionData;

                    FieldManager.UpdateChildren(this);
                    ctx.DataContext.Doses.InsertOnSubmit(doseData);
                }
            }
        }

        private void Child_Update(Prescription prescription)
        {
            using (var ctx = ContextManager<PrescriptionDataContext>.GetManager(
                DatabaseConnection.GetConnectionString(
                DatabaseConnection.DatabaseName.GENPRES), false))
            {
                using (BypassPropertyChecks)
                {
                    Database.Dose data = ctx.DataContext.Doses.Single<Database.Dose>(c => c.Id == this.Id);
                    Quantity.Save();
                    Total.Save();
                    Rate.Save();
                    ctx.DataContext.SubmitChanges();
                }
            }
        }
        private void Child_DeleteSelf(object parent)
        {
            // TODO: delete values
        }*/
        #endregion
        
        #region Factory Methods

        [RunLocal]
        internal static Dose NewDose()
        {
            return DataPortal.CreateChild<Dose>();
        }

        public static Dose GetDose(Database.Dose data)
        {
            return DataPortal.FetchChild<Dose>(data);
        }

        public Dose()
        { /* Require use of factory methods */ }

        #endregion
    }
}