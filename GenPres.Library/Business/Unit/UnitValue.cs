using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Data;
using GenPres.Operations;
using GenPres.Database;

namespace GenPres.Business
{
    [Serializable]
    public class UnitValue : StateBusinessBase<UnitValue>, IDataBusinessBase
    {
        private UnitValue() { /* Require use of factory methods */ }

        [RunLocal]
        protected override void Child_Create()
        {
            base.Child_Create();
        }
        public override object GetDataAccessObject()
        {
            if (_cachedAccessObject == null) _cachedAccessObject = new GenPres.Database.UnitValue();
            return _cachedAccessObject;
        }

        #region Private Properties
        private int _id = 0;
        private decimal _value = 0;
        private decimal _baseValue = 0;
        private string _unit = "";
        private string _adjust = "";
        private string _total = "";
        private string _time = "";
        private string _uistate = "";
        private decimal[] _increments = new decimal[1] { 0 };
        private Boolean _allowIncrementStep = true;
        private string _incrementDirection = "";
        private bool _propertyChanged = false;
        private int _currentIncrement = 0;
        [NonSerialized]
        private Factor _factor;
        #endregion

        #region Public Properties
        public Factor Factor
        {
            get
            {
                return _factor;
            }
            set
            {
                _factor = value;
            }
        }

        internal static PropertyInfo<int> IdProperty = RegisterProperty(typeof(UnitValue), new PropertyInfo<int>("Id"));
        public int Id
        {
            get
            {
                _id = GetProperty(IdProperty);
                return _id;
            }
            set
            {
                _id = value;
                SetProperty(IdProperty, _id);   
            }
        }

        internal static PropertyInfo<decimal> ValueProperty = RegisterProperty(typeof(UnitValue), new PropertyInfo<decimal>("Value"));
        public decimal Value
        {
            get
            {
                if (_baseValue != 0)
                {
                    return getUnitValue();
                }
                _value = GetProperty(ValueProperty);
                return _value;
            }
            set
            {
                _value = value;
                calculateBaseValue();
                SetProperty(ValueProperty, _value);
            }
        }
        internal static PropertyInfo<decimal> BaseValueProperty = RegisterProperty(typeof(UnitValue), new PropertyInfo<decimal>("BaseValue"));
        public decimal BaseValue
        {
            get
            {
                _baseValue = GetProperty(BaseValueProperty);
                return _baseValue;
            }
            set
            {
                _baseValue = value;
                getUnitValue();
                SetProperty(BaseValueProperty, _baseValue);
            }
        }

        internal static PropertyInfo<string> UnitProperty = RegisterProperty(typeof(UnitValue), new PropertyInfo<string>("Unit"));
        public string Unit
        {
            get
            {
                _unit = GetProperty(UnitProperty);
                return _unit;
            }
            set
            {
                _unit = value;
                calculateBaseValue();
                SetProperty(UnitProperty, _unit);
            }
        }

        internal static PropertyInfo<string> AdjustProperty = RegisterProperty(typeof(UnitValue), new PropertyInfo<string>("Adjust"));
        public string Adjust
        {
            get
            {
                _adjust = GetProperty(AdjustProperty);
                return _adjust;
            }
            set
            {
                _adjust = value;
                calculateBaseValue();
                SetProperty(AdjustProperty, value);
            }
        }

        internal static PropertyInfo<string> TotalProperty = RegisterProperty(typeof(UnitValue), new PropertyInfo<string>("Total"));
        public string Total
        {
            get
            {
                _total = GetProperty(TotalProperty);
                return _total;
            }
            set
            {
                _total = value;
                calculateBaseValue();
                SetProperty(TotalProperty, _total);
            }
        }

        internal static PropertyInfo<string> TimeProperty = RegisterProperty(typeof(UnitValue), new PropertyInfo<string>("Time"));
        public string Time
        {
            get
            {
                _time = GetProperty(TimeProperty);
                return _time;
            }
            set
            {
                _time = value;
                calculateBaseValue();
                SetProperty(TimeProperty, _time);
            }
        }

        internal static PropertyInfo<string> UIStateProperty = RegisterProperty(typeof(UnitValue), new PropertyInfo<string>("UIState"));
        public string UIState
        {
            get
            {
                _uistate = GetProperty(UIStateProperty);
                return _uistate;
            }
            set
            {
                _uistate = value;
                SetProperty(UIStateProperty, _uistate);
            }
        }

        public int CurrentIncrement
        {
            get
            {
                return _currentIncrement;
            }
            set
            {
                _currentIncrement = value;
            }
        }

        public decimal[] Increments
        {
            get
            {
                return _increments;
            }
            set
            {
                _increments = value;
            }
        }

        public string IncrementDirection
        {
            get
            {
                return _incrementDirection;
            }
            set
            {
                _incrementDirection = value;
            }
        }

        public bool PropertyHasChanged
        {
            get
            {
                return _propertyChanged;
            }
            set
            {
                _propertyChanged = value;
            }
        }

        public bool AllowIncrementStep
        {
            get
            {
                return _allowIncrementStep;
            }
            set
            {
                _allowIncrementStep = value;
            }
        }
        #endregion 

        #region Calculations
        //Calculate the base value (to base)
        private void calculateBaseValue()
        {
            if (_value == 0) return;
            if (_unit == "" && _time == "") return;

            decimal value = _value;
            if (_unit != "") value = UnitConverter.GetBaseValue(_unit, _value);
            if (_time != "")
            {
                value = value / UnitConverter.GetBaseValue(_time, 1);
            }
            if (_total != "")
            {
                value = value / UnitConverter.GetBaseValue(_total, 1);
            }
            _baseValue = value;
            SetProperty(BaseValueProperty, _baseValue);
        }

        //Calculate the unit value (from base)
        private decimal getUnitValue()
        {
            if (_unit == "" && _time == "") return 0;

            decimal value = _baseValue;
            if (_unit != "") value = UnitConverter.GetUnitValue(_unit, value);
            if (_time != "")
            {
                value = value * UnitConverter.GetBaseValue(_time, 1);
            }
            if (_total != "")
            {
                value = value * UnitConverter.GetBaseValue(_total, 1);
            }

            _value = value;
            SetProperty(ValueProperty, _value);
            return _value;
        }

        public void UpdateIncrements(Prescription p)
        {
            decimal[] incrementsValues = new decimal[_increments.Length];
            for (int i = 0; i < _increments.Length; i++)
            {
                decimal result = _increments[i];
                if (Adjust != "" && p.GetAdjustValue(Adjust) > 0)
                {
                    decimal adjustVal = p.GetAdjustValue(Adjust);
                    result = result / adjustVal;
                    result = result / UnitConverter.GetUnitValue(Adjust, 1);
                }
                
                result = UnitConverter.GetUnitValue(Unit, result);

                if (_time != "")
                {
                    //TODO: fix properly!!
                    if (_time == "uur" || _time == "min")
                    {
                        result = result * (UnitConverter.GetBaseValue(_time, 1) / UnitConverter.GetBaseValue("uur", 1));
                    }
                    else
                    {
                        result = result * (UnitConverter.GetBaseValue(_time, 1) / UnitConverter.GetBaseValue(p.Frequency.Time, 1));
                    }
                }
                incrementsValues[i] = result;
            }
            Increments = incrementsValues;
        }
        #endregion 
    }
}
