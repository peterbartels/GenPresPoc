using System;
using GenPres.Business.Calculation.Math;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Business.Domain.Units
{
    public class UnitValue : IUnitValue, ICalculationStateTrackable
    {
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


        private Factor _factor;
        #endregion

        public bool IsNew { get { return (Id == 0); } }

        #region Public Properties
        public Factor Factor
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

        public int Id
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

        public decimal Value
        {
            get
            {
                if (_baseValue != 0)
                {
                    return MathExt.FixPrecision(getUnitValue());
                }
                return MathExt.FixPrecision(_value);
            }
            set
            {
                _value = value;
                calculateBaseValue();
            }
        }

        public decimal BaseValue
        {
            get
            {
                return _baseValue;
            }
            set
            {
                _baseValue = value;
                getUnitValue();
            }
        }

        public string Unit
        {
            get
            {
                return _unit;
            }
            set
            {
                _unit = value;
                calculateBaseValue();
            }
        }

        public string Adjust
        {
            get
            {
                return _adjust;
            }
            set
            {
                _adjust = value;
                calculateBaseValue();
            }
        }

        public string Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value;
                calculateBaseValue();
            }
        }

        public string Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                calculateBaseValue();
            }
        }

        public string UIState
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
            if (_value == 0)
            {
                _baseValue = 0;
                return;
            }
            if (_unit == "" && _time == "") return;

            decimal value = _value;
            if (_unit != "" && _unit != null) value = UnitConverter.GetBaseValue(_unit, _value);
            if (_time != "" && _time != null)
            {
                value = value / UnitConverter.GetBaseValue(_time, 1);
            }
            if (_total != "" && _total != null)
            {
                value = value / UnitConverter.GetBaseValue(_total, 1);
            }
            _baseValue = value;
        }

        //Calculate the unit value (from base)
        private decimal getUnitValue()
        {
            if (_unit == "" && _time == "") return 0;

            decimal value = _baseValue;
            if (_unit != "" && _unit != null) value = UnitConverter.GetUnitValue(_unit, value);
            if (_time != "" && _time != null)
            {
                value = value * UnitConverter.GetBaseValue(_time, 1);
            }
            if (_total != "" && _total != null)
            {
                value = value*UnitConverter.GetBaseValue(_total, 1);
            }

            _value = value;
            return _value;
        }

        internal void UpdateIncrements(Prescription p)
        {
            decimal[] incrementsValues = new decimal[_increments.Length];
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

                if (_time != "" && _time != null)
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

        public CalculationState CalculationState { get; set; }
    }
}
