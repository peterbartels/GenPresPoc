using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;

namespace GenPres.Business.Domain
{
    [Serializable]
    internal class Prescription : BusinessBase<Prescription>, IPrescription, IIngredient
    {
        #region Business Properties

        private Ingredient _ingredient;

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

        internal static PropertyInfo<Medicine> MedicineProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<Medicine>("Medicine"));
        internal Medicine Medicine
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


        internal static PropertyInfo<ComponentCollection> ComponentListProperty = RegisterProperty<ComponentCollection>(p => p.Components);
        public ComponentCollection Components
        {
            get
            {
                if (!(FieldManager.FieldExists(ComponentListProperty)))
                {
                    LoadProperty(ComponentListProperty, ComponentCollection.NewComponentCollection());
                }
                return GetProperty(ComponentListProperty);
            }
            set
            {
                SetProperty(ComponentListProperty, value);
            }
        }

        public Component NewComponent()
        {
            return Components.AddNew(); ;
        }


        internal static PropertyInfo<string> GenericProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<string>("Generic"));
        public string Generic
        {
            get
            {
                string value = GetProperty(GenericProperty);
                return value;
            }
            set
            {
                SetProperty(GenericProperty, value);
            }
        }


        internal static PropertyInfo<string> SolutionTypeProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<string>("SolutionType"));
        public string SolutionType
        {
            get
            {
                string value = GetProperty(SolutionTypeProperty);
                return value;
            }
            set
            {
                SetProperty(SolutionTypeProperty, value);
            }
        }


        internal static PropertyInfo<string> ShapeProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<string>("Shape"));
        public string Shape
        {
            get
            {
                string value = GetProperty(ShapeProperty);
                return value;
            }
            set
            {
                SetProperty(ShapeProperty, value);
            }
        }

        internal static PropertyInfo<string> RouteProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<string>("Route"));
        public string Route
        {
            get
            {
                string value = GetProperty(RouteProperty);
                return value;
            }
            set
            {
                SetProperty(RouteProperty, value);
            }
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

        internal static PropertyInfo<DateTime> StartDateProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<DateTime>("StartDate"));
        public DateTime StartDate
        {
            get
            {
                DateTime value = GetProperty(StartDateProperty);
                return value;
            }
            set
            {
                SetProperty(StartDateProperty, value);
            }
        }

        internal static PropertyInfo<DateTime> EndDateProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<DateTime>("EndDate"));
        public DateTime EndDate
        {
            get
            {
                DateTime value = GetProperty(EndDateProperty);
                return value;
            }
            set
            {
                if (value == null) return;
                SetProperty(EndDateProperty, value);
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
                return value;
            }
            set
            {
                SetProperty(AdjustWeightProperty, value);
            }
        }

        internal static PropertyInfo<UnitValue> AdjustLengthProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<UnitValue>("AdjustLength"));
        public UnitValue AdjustLength
        {
            get
            {
                UnitValue value = GetProperty<UnitValue>(AdjustLengthProperty);
                return value;
            }
            set
            {
                SetProperty(AdjustLengthProperty, value);
            }
        }

        internal static PropertyInfo<UnitValue> AdjustBSAProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<UnitValue>("AdjustBSA"));
        public UnitValue AdjustBSA
        {
            get
            {
                UnitValue value = GetProperty<UnitValue>(AdjustBSAProperty);
                return value;
            }
            set
            {
                SetProperty(AdjustBSAProperty, value);
            }
        }

        internal static PropertyInfo<UnitValue> FrequencyProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<UnitValue>("Frequency"));
        public UnitValue Frequency
        {
            get
            {
                UnitValue value = GetProperty(FrequencyProperty);
                return value;
            }
            set
            {
                SetProperty(FrequencyProperty, value);
            }
        }


        internal static PropertyInfo<UnitValue> TimeProperty = RegisterProperty(typeof(Prescription), new PropertyInfo<UnitValue>("Time"));
        public UnitValue Time
        {
            get
            {
                UnitValue value = GetProperty<UnitValue>(TimeProperty);
                return value;
            }
            set
            {
                SetProperty(TimeProperty, value);
            }
        }

        public UnitValue Quantity
        {
            get
            {
                return _ingredient.Quantity;
            }
            set
            {
                _ingredient.Quantity = value;
            }
        }

        public UnitValue ContainerConcentration
        {
            get
            {
                return _ingredient.ContainerConcentration;
            }
            set
            {
                _ingredient.ContainerConcentration = value;
            }
        }

        public UnitValue TotalConcentration
        {
            get
            {
                return _ingredient.TotalConcentration;
            }
            set
            {
                _ingredient.TotalConcentration = value;
            }
        }

        public UnitValue DoseQuantity
        {
            get
            {
                return _ingredient.Dose.Quantity;
            }
            set
            {
                _ingredient.Dose.Quantity = value;
            }
        }

        public UnitValue DoseTotal
        {
            get
            {
                return _ingredient.Dose.Total;
            }
            set
            {
                _ingredient.Dose.Total = value;
            }
        }


        public UnitValue DoseRate
        {
            get
            {
                return _ingredient.Dose.Rate;
            }
            set
            {
                _ingredient.Dose.Rate = value;
            }
        }

        #endregion

        #region Factory Methods
        [RunLocal]
        internal static IPrescription NewPrescription()
        {
            return DataPortal.Create<Prescription>();
        }

        public new IPrescription Save()
        {
            return base.Save();
        }
        #endregion

        #region Data Access

        protected override void DataPortal_Create()
        {
            setDefaults(this);
            _ingredient = DataPortal.CreateChild<Ingredient>();
        }

        #endregion

        private static void setDefaults(Prescription prescription)
        {
            prescription.Components.AddNew();
        }
    }
}
