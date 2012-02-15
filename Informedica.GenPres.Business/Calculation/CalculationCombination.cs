using System;
using System.Linq;
using System.Linq.Expressions;
using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Business.Domain.Units;
using Informedica.GenPres.Business.Util;

namespace Informedica.GenPres.Business.Calculation
{
    public class CalculationCombination : ICalculationCombination
    {
        private readonly decimal[] _values = new decimal[3];
        
        private readonly Expression<Func<UnitValue>>[] _properties;
        private readonly Prescription _prescription;
        private bool _calculated = false;

        public CalculationCombination(Prescription prescription, params Expression<Func<UnitValue>>[] properties)
        {
            _prescription = prescription;
            _properties = properties;
            _values = PropertyHelper.ConvertUnitValuesToDecimalArray(_properties, _prescription);
        }

        public void Calculate()
        {
            if(CanBeCalculated())
            {
                var index = GetIndexToBeCalculated();
                if(index == 0){ SetValue(0, GetValue(1) * GetValue(2)); }
                if (index == 1) { SetValue(1, GetValue(0) / GetValue(2)); }
                if (index == 2) { SetValue(2, GetValue(0) / GetValue(1)); }
                _calculated = true;
            }
        }

        private decimal GetValue(int index)
        {
            return _values[index];
        }

        private void SetValue(int index, decimal value)
        {
            _values[index] = System.Math.Round(value, 8, MidpointRounding.AwayFromZero);
        }


        private bool CanBeCalculated()
        {
            return (from i in _values where i == 0 select i).Count() == 1;
        }

        private int GetIndexToBeCalculated()
        {
            for (int i = 0; i < _values.Length; i++)
            {
                if (_values[i] == 0)
                {
                    return i;
                }
            }
            return 0;
        }

        public void Finish()
        {
            if(_calculated)
            {
                for (int i = 0; i < _values.Length; i++)
                {
                    PropertyHelper.SetBaseValue(_properties[i], _values[i], _prescription);
                }
            }
        }
    }
}