using System;
using GenPres.Business.Calculation.Math;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Business.Allowance;
namespace GenPres.Business.Domain.Units
{
    public class UnitValue : ICalculationStateTrackable, IPropertyAllowance
    {
        #region Private Properties
        private Guid _id = Guid.Empty;
        private decimal _value = 0;
        private decimal _baseValue = 0;
        private string _unit = "";
        private string _adjust = "";
        private string _total = "";
        private string _time = "";
        private string _uistate = "user";
        private decimal[] _increments = new decimal[1] { 0 };
        private Boolean _allowIncrementStep = true;
        private string _incrementDirection = "";
        private bool _propertyChanged = false;
        private int _currentIncrement = 0;
        public virtual bool ChangedByUser { get; set; }
        
        private Factor _factor;
        #endregion

        public virtual bool IsNew { get { return (Id == Guid.Empty); } }

        #region Public Properties
        public virtual Factor Factor
        {
            get
            {
                if(_factor == null) _factor = new Factor(this);
                return _factor;
            }
            set
            {
                _factor = value;
            }
        }

        public virtual Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public virtual decimal Value
        {
            get
            {
                if (_baseValue != 0)
                {
                    return MathExt.FixPrecision(GetUnitValue());
                }
                return MathExt.FixPrecision(_value);
            }
            set
            {
                _value = value;
                CalculateBaseValue();
            }
        }

        public virtual decimal BaseValue
        {
            get
            {
                return _baseValue;
            }
            set
            {
                _baseValue = value;
                GetUnitValue();
            }
        }

        public virtual string Unit
        {
            get
            {
                return _unit;
            }
            set
            {
                _unit = value;
                CalculateBaseValue();
            }
        }

        public virtual string Adjust
        {
            get
            {
                return _adjust;
            }
            set
            {
                _adjust = value;
                CalculateBaseValue();
            }
        }

        public virtual string Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value;
                CalculateBaseValue();
            }
        }

        public virtual string Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                CalculateBaseValue();
            }
        }

        public virtual string UIState
        {
            get
            {
                return _uistate;
            }
            set
            {
                _uistate = value;
            }
        }

        public virtual int CurrentIncrement
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

        public virtual decimal[] Increments
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

        public virtual string IncrementDirection
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

        public virtual bool PropertyHasChanged
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

        public virtual bool AllowIncrementStep
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

        public virtual bool CanBeSet { get; set; }

        protected UnitValue()
        {
            
        }

        #region Calculations
        //Calculate the base value (to base)
        private void CalculateBaseValue()
        {
            if (_value == 0)
            {
                _baseValue = 0;
                return;
            }
            if (_unit == "" && _time == "") return;

            decimal value = _value;
            if (!string.IsNullOrEmpty(_unit)) value = UnitConverter.GetBaseValue(_unit, _value);
            if (!string.IsNullOrEmpty(_time))
            {
                value = value / UnitConverter.GetBaseValue(_time, 1);
            }
            if (!string.IsNullOrEmpty(_total))
            {
                value = value / UnitConverter.GetBaseValue(_total, 1);
            }
            _baseValue = value;
        }

        //Calculate the unit value (from base)
        private decimal GetUnitValue()
        {
            if (_unit == "" && _time == "") return 0;

            decimal value = _baseValue;
            if (!string.IsNullOrEmpty(_unit)) value = UnitConverter.GetUnitValue(_unit, value);
            if (!string.IsNullOrEmpty(_time))
            {
                value = value * UnitConverter.GetBaseValue(_time, 1);
            }
            if (!string.IsNullOrEmpty(_total))
            {
                value = value*UnitConverter.GetBaseValue(_total, 1);
            }

            _value = value;
            return _value;
        }

        internal protected virtual void UpdateIncrements(Prescription p)
        {
            var incrementsValues = new decimal[_increments.Length];
            for (int i = 0; i < _increments.Length; i++)
            {
                decimal result = _increments[i];
                /*TODO if (Adjust != "" && p.GetAdjustValue(Adjust) > 0)
                {
                    decimal adjustVal = p.GetAdjustValue(Adjust);
                    result = result / adjustVal;
                    result = result / UnitConverter.GetUnitValue(Adjust, 1);
                }*/

                result = UnitConverter.GetUnitValue(Unit, result);

                if (!string.IsNullOrEmpty(_time))
                {
                    //TODO: fix properly!!
                    if (_time == "uur" || _time == "min")
                    {
                        result = result * (UnitConverter.GetBaseValue(_time, 1) / UnitConverter.GetBaseValue("uur", 1));
                    }
                    else
                    {
                        //TEMPWEG result = result * (UnitConverter.GetBaseValue(_time, 1) / UnitConverter.GetBaseValue(p.Frequency.Time, 1));
                    }
                }
                incrementsValues[i] = result;
            }
            Increments = incrementsValues;
        }
        #endregion

        public virtual CalculationState CalculationState { get; set; }

        public static UnitValue NewUnitValue(bool canBeSet)
        {
            var uv = new UnitValue();
            uv.CanBeSet = canBeSet;
            return uv;
        }
    }
}
