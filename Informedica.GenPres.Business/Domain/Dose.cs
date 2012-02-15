using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;

namespace GenPres.Business.Domain
{
    internal class Dose : BusinessBase<Dose>
    {
        #region Business Methods

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
            set
            {
                if (Rate.Id > 0) value.Id = Rate.Id;
                SetProperty(RateProperty, value);
            }
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
            set
            {
                if (Total.Id > 0) value.Id = Total.Id;
                SetProperty(TotalProperty, value);
            }
        }

        #endregion

        #region Factory Methods

        [RunLocal]
        internal static Dose NewDose()
        {
            return DataPortal.CreateChild<Dose>();
        }

        public static Dose GetDose(object data)
        {
            return DataPortal.FetchChild<Dose>(data);
        }

        private Dose() { /* Require use of factory methods */ }

        #endregion
    }
}
