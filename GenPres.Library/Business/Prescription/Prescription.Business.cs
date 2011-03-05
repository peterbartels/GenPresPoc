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
    /// Prescription class (EditableRoot object)
    /// </summary>
    [Serializable]
    public partial class Prescription : StateBusinessBase<Prescription>, IDataBusinessBase
    {
        #region Business Properties

        public override object GetDataAccessObject()
        {
            if (_cachedAccessObject == null) _cachedAccessObject = new GenPres.Database.Prescription();
            return _cachedAccessObject;
        }

        public System.Collections.Hashtable Totals = new System.Collections.Hashtable();

        internal static PropertyInfo<Drug> DrugProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<Drug>("Drug"));
        public Drug Drug
        {
            get
            {
                Drug value = GetProperty(DrugProperty);
                return value;
            }
            set
            {
                SetProperty(DrugProperty, value);
            }
        }

        internal static PropertyInfo<Medicine> MedicineProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<Medicine>("Medicine"));
        public Medicine Medicine
        {
            get
            {
                Medicine value = GetProperty(MedicineProperty);
                return value;
            }
            set
            {
                SetProperty(MedicineProperty, value);
            }
        }

        internal static PropertyInfo<DoseCollection> DoseListProperty = RegisterProperty<DoseCollection>(p => p.Doses);
        public DoseCollection Doses
        {
            get
            {
                if (!(FieldManager.FieldExists(DoseListProperty)))
                {
                    LoadProperty(DoseListProperty, DoseCollection.NewDoseCollection());
                }
                return GetProperty(DoseListProperty);
            }
            set
            {
                SetProperty(DoseListProperty, value);
            }
        }

        public Dose NewDose()
        {
            Doses.SetPrescription(this);
            return Doses.AddNew();
        }

        internal static PropertyInfo<string> StateProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<string>("State"));
        public string State
        {
            get
            {
                string value = GetProperty(StateProperty);
                return value;
            }
            set
            {
                SetProperty(StateProperty, value);
            }
        }

        internal static PropertyInfo<string> RemarksProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<string>("Remarks"));
        public string Remarks
        {
            get
            {
                string value = GetProperty(RemarksProperty);
                return value;
            }
            set
            {
                SetProperty(RemarksProperty, value);
            }
        }


        internal static PropertyInfo<DateTime> DateProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<DateTime>("Date"));
        public DateTime Date
        {
            get
            {
                DateTime value = GetProperty(DateProperty);
                return value;
            }
            set
            {
                SetProperty(DateProperty, value);
            }
        }

        internal static PropertyInfo<DateTime> StartDateTimeProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<DateTime>("StartDateTime"));
        public DateTime StartDateTime
        {
            get
            {
                DateTime value = GetProperty(StartDateTimeProperty);
                return value;
            }
            set
            {
                SetProperty(StartDateTimeProperty, value);
            }
        }

        internal static PropertyInfo<DateTime> EndDateTimeProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<DateTime>("EndDateTime"));
        public DateTime EndDateTime
        {
            get
            {
                DateTime value = GetProperty(EndDateTimeProperty);
                return value;
            }
            set
            {
                SetProperty(EndDateTimeProperty, value);
            }
        }
        internal static PropertyInfo<string> StartDateProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<string>("StartDate"));
        public string StartDate
        {
            get
            {
                string value = GetProperty(StartDateProperty);
                if (value == "" || value == null) value = System.DateTime.Now.ToString("yyyy\\/MM\\/dd");
                return value;
            }
            set
            {
                if (value == null) return;
                value = value.Replace('-', '/');
                if (value.Contains("T")) value = value.Split('T')[0];
                SetProperty(StartDateProperty, value);
            }
        }

        internal static PropertyInfo<string> EndDateProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<string>("EndDate"));
        public string EndDate
        {
            get
            {
                string value = GetProperty(EndDateProperty);
                return value;
            }
            set
            {
                if (value == null) return;
                value = value.Replace('-', '/');
                if (value.Contains("T")) value = value.Split('T')[0];
                SetProperty(EndDateProperty, value);
            }
        }


        internal static PropertyInfo<string> StartTimeProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<string>("StartTime"));
        public string StartTime
        {
            get
            {
                string value = GetProperty(StartTimeProperty);
                if (value == "" || value == null)
                {
                    value = System.DateTime.Now.ToString("HH:mm");
                }
                return value;
            }
            set
            {
                SetProperty(StartTimeProperty, value);
            }
        }

        internal static PropertyInfo<string> EndTimeProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<string>("EndTime"));
        public string EndTime
        {
            get
            {
                string value = GetProperty(EndTimeProperty);
                return value;
            }
            set
            {
                SetProperty(EndTimeProperty, value);
            }
        }


        internal static PropertyInfo<int> IdProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<int>("Id"));
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

        internal static PropertyInfo<bool> ContinuousProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<bool>("Continuous"));
        public bool Continuous
        {
            get
            {
                bool value = GetProperty(ContinuousProperty);
                return value;
            }
            set
            {
                SetProperty(ContinuousProperty, value);
            }
        }

        internal static PropertyInfo<bool> InfusionProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<bool>("Infusion"));
        public bool Infusion
        {
            get
            {
                bool value = GetProperty(InfusionProperty);
                return value;
            }
            set
            {
                SetProperty(InfusionProperty, value);
            }
        }

        internal static PropertyInfo<bool> OnrequestProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<bool>("Onrequest"));
        public bool Onrequest
        {
            get
            {
                bool value = GetProperty(OnrequestProperty);
                return value;
            }
            set
            {
                SetProperty(OnrequestProperty, value);
            }
        }

        internal static PropertyInfo<bool> SolutionProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<bool>("Solution"));
        public bool Solution
        {
            get
            {
                bool value = GetProperty(SolutionProperty);
                return value;
            }
            set
            {
                SetProperty(SolutionProperty, value);
            }
        }
        internal static PropertyInfo<bool> TPNProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<bool>("TPN"));
        public bool TPN
        {
            get
            {
                bool value = GetProperty(TPNProperty);
                return value;
            }
            set
            {
                SetProperty(TPNProperty, value);
            }
        }
        internal static PropertyInfo<bool> IsTemplateProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<bool>("IsTemplate"));
        public bool IsTemplate
        {
            get
            {
                bool value = GetProperty(IsTemplateProperty);
                return value;
            }
            set
            {
                SetProperty(IsTemplateProperty, value);
            }
        }

        internal static PropertyInfo<bool> UsesTemplateProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<bool>("UsesTemplate"));
        public bool UsesTemplate
        {
            get
            {
                bool value = GetProperty(UsesTemplateProperty);
                return value;
            }
            set
            {
                SetProperty(UsesTemplateProperty, value);
            }
        }

        internal static PropertyInfo<UnitValue> AdjustWeightProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<UnitValue>("AdjustWeight"));
        public UnitValue AdjustWeight
        {
            get
            {
                UnitValue value = GetProperty<UnitValue>(AdjustWeightProperty);
                if (value == null)
                {
                    value = DataPortal.CreateChild<UnitValue>();
                    LoadProperty(AdjustWeightProperty, value);
                }
                return value;
            }
            set {
                if (AdjustWeight.Id > 0) value.Id = AdjustWeight.Id;
                SetProperty(AdjustWeightProperty, value); }
        }

        internal static PropertyInfo<UnitValue> AdjustLengthProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<UnitValue>("AdjustLength"));
        public UnitValue AdjustLength
        {
            get
            {
                UnitValue value = GetProperty<UnitValue>(AdjustLengthProperty);
                if (value == null)
                {
                    value = DataPortal.CreateChild<UnitValue>();
                    LoadProperty(AdjustLengthProperty, value);
                }
                return value;
            }
            set {
                if (AdjustLength.Id > 0) value.Id = AdjustLength.Id;
                SetProperty(AdjustLengthProperty, value); }
        }

        internal static PropertyInfo<UnitValue> AdjustBSAProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<UnitValue>("AdjustBSA"));
        public UnitValue AdjustBSA
        {
            get
            {
                UnitValue value = GetProperty<UnitValue>(AdjustBSAProperty);
                if (value == null)
                {
                    value = DataPortal.CreateChild<UnitValue>();
                    LoadProperty(AdjustBSAProperty, value);
                }
                return value;
            }
            set {
                if (AdjustBSA.Id > 0) value.Id = AdjustBSA.Id;
                SetProperty(AdjustBSAProperty, value); }
        }

        internal static PropertyInfo<UnitValue> FrequencyProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<UnitValue>("Frequency"));
        public UnitValue Frequency
        {
            get {
                UnitValue value = GetProperty(FrequencyProperty);
                if (value == null)
                {
                    value = DataPortal.CreateChild<UnitValue>();
                    value.Increments = new decimal[1] { 1 };
                    LoadProperty(FrequencyProperty, value);
                }
                if (value.Factor == null)
                {
                    value.Factor = new Factor(TotalProperty);
                    value.Increments = new decimal[] { 1 };
                    value.AllowIncrementStep = true;
                }
                return value;
            }
            set {

                if (Frequency.Id > 0) value.Id = Frequency.Id;
                SetProperty(FrequencyProperty, value);
            }
        }

        internal static PropertyInfo<UnitValue> QuantityProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<UnitValue>("Quantity"));        
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
                if (value.Factor == null && Drug != null)
                {
                    value.Factor = new Factor(TotalProperty);
                    value.Increments = new decimal[] { Drug.Components[0].ComponentIncrement };
                    value.AllowIncrementStep = true;
                }
                return value;
            }
            set
            {
                if (Quantity.Id > 0) value.Id = Quantity.Id;
                SetProperty(QuantityProperty, value);
            }
        }

        internal static PropertyInfo<UnitValue> RateProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<UnitValue>("Rate"));
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
                SetProperty(RateProperty, value); 
            }
        }

        internal static PropertyInfo<UnitValue> TotalProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<UnitValue>("Total"));
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
                if (value.Factor == null && Drug != null)
                {
                    value.Factor = new Factor(TotalProperty);
                    value.Increments = new decimal[] {Drug.Components[0].ComponentIncrement};
                    value.AllowIncrementStep = true;
                }
                return value;
            }
            set {
                if (Total.Id > 0) value.Id = Total.Id;
                SetProperty(TotalProperty, value); }
        }

        internal static PropertyInfo<UnitValue> TimeProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<UnitValue>("Time"));
        public UnitValue Time
        {
            get
            {
                UnitValue value = GetProperty<UnitValue>(TimeProperty);
                if (value == null)
                {
                    value = DataPortal.CreateChild<UnitValue>();
                    LoadProperty(TimeProperty, value);
                }
                return value;
            }
            set {
                if (Time.Id > 0) value.Id = Time.Id;
                SetProperty(TimeProperty, value); }
        }

        #endregion
    }
}
