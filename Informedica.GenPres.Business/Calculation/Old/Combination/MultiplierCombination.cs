using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Informedica.GenPres.Business.Calculation.Old.Calculation;
using Informedica.GenPres.Business.Calculation.Old.Increment;
using Informedica.GenPres.Business.Calculation.Old.Math;
using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Business.Domain.Units;
using Informedica.GenPres.Business.Util;

namespace Informedica.GenPres.Business.Calculation.Old.Combination
{

    public class MultiplierCombination : ICalculationCombination
    {
        private readonly decimal[] values = new decimal[3];
        private readonly UnitValue[] _unitValues = new UnitValue[3];

        private Expression<Func<UnitValue>>[] _properties;
        private Prescription _prescription;

        public MultiplierCombination(Prescription prescription, params Expression<Func<UnitValue>>[] properties)
        {
            _prescription = prescription;
            _properties = properties;
            ConvertCombinationsValuesToArray();
        }

        internal decimal GetIncrementStep(int index, decimal substanceIncrement)
        {
            Factor propertyFactor = _unitValues[index].Factor;
            decimal increment = propertyFactor.GetIncrementStep(GetValue(index), substanceIncrement);
            return System.Math.Round(increment, 8, MidpointRounding.AwayFromZero);
        }

        internal decimal GetIncrementValue(int index, decimal incrementStep, decimal substanceIncrement)
        {
            Factor propertyFactor = _unitValues[index].Factor;
            decimal increment = propertyFactor.GetIncrementValue(incrementStep, substanceIncrement);
            return increment;
        }

        public decimal GetValue(int index)
        {
            return values[index];
        }

        public void SetValue(int index, decimal value)
        {
            values[index] = System.Math.Round(value, 8, MidpointRounding.AwayFromZero);
        }

        public Expression<Func<UnitValue>>[] GetProperties()
        {
            return _properties;
        }

        private static MemberExpression GetMemberInfo(Expression method)
        {
            var lambda = method as LambdaExpression;
            if (lambda == null)
                throw new ArgumentNullException("method");

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr =
                    ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
                throw new ArgumentException("method");

            return memberExpr;
        }

        public bool PropertyExpressionsAreEqual(Expression<Func<UnitValue>> property1, Expression<Func<UnitValue>> property2)
        {

            if (GetMemberInfo(property1).Member.Name == GetMemberInfo(property2).Member.Name
                && GetMemberInfo(property1).Member.DeclaringType.FullName == GetMemberInfo(property2).Member.DeclaringType.FullName
                )
            {
                return true;
            }
            return false;
        }

        public decimal GetConvertedValue(int index)
        {
            var prop = _properties[index];
            if (PropertyExpressionsAreEqual(prop, () =>_prescription.Frequency))
                return _unitValues[index].BaseValue / UnitConverter.GetUnitValue(_prescription.Frequency.Time, 1);

            if (PropertyExpressionsAreEqual(prop, () => _prescription.Quantity))
                return _unitValues[index].BaseValue;

            if (PropertyExpressionsAreEqual(prop, () => _prescription.Total))
                return _unitValues[index].BaseValue / UnitConverter.GetUnitValue(_prescription.Frequency.Time, 1);

            if (PropertyExpressionsAreEqual(prop, () => _prescription.Doses[0].Quantity))
                return _unitValues[index].BaseValue;

            if (PropertyExpressionsAreEqual(prop, () => _prescription.Doses[0].Total))
                return _unitValues[index].BaseValue / UnitConverter.GetUnitValue(_prescription.Frequency.Time, 1);

            throw new Exception("property has no convert configuration.");
        }

        public decimal SetBaseValue(int index)
        {
            var prop = _properties[index];
            if (PropertyExpressionsAreEqual(prop, () => _prescription.Frequency))
                _unitValues[index].BaseValue = values[index] * UnitConverter.GetUnitValue(_prescription.Frequency.Time, 1); ;

            if (PropertyExpressionsAreEqual(prop, () => _prescription.Quantity))
                _unitValues[index].BaseValue = values[index];

            if (PropertyExpressionsAreEqual(prop, () => _prescription.Total))
                _unitValues[index].BaseValue = values[index] * UnitConverter.GetUnitValue(_prescription.Frequency.Time, 1);

            if (PropertyExpressionsAreEqual(prop, () => _prescription.Doses[0].Quantity))
                _unitValues[index].BaseValue = values[index];

            if (PropertyExpressionsAreEqual(prop, () => _prescription.Doses[0].Total))
                _unitValues[index].BaseValue = values[index] * UnitConverter.GetUnitValue(_prescription.Frequency.Time, 1);

            return 0;
        }

        public UnitValue GetUnitValue(int index)
        {
            return _unitValues[index];
        }

        public void Calculate()
        {
            var pc = new PropertyCombinationCalculate();
            CorrectPropertyIncrements();
            
            if (CanBeCalculated()) 
                pc.Calculate(this, GetCalculatedIndex());

        }

        
        public UnitValue GetPropertyByName(string name)
        {
            var prop = (from i in _properties where PropertyHelper.MemberName(i) == name select i).FirstOrDefault();
            if (prop == null) return null;
            return PropertyHelper.GetUnitValue(prop);
        }
        public int GetCalculatedIndex()
        {
            for (int i = 0; i < values.Length; i++)
            {
                if(values[i] == 0)
                {
                    return i;
                }
            }
            return 0;
        }

        public int GetIndexToRectify()
        {
            List<string> propertiesSequenceNames = OldPrescriptionCalculator.PropertySequence();
            for (int p = 0; p < propertiesSequenceNames.Count; p++)
            {
                for (int i = 0; i < _unitValues.Length; i++)
                {
                    if (_unitValues[i].BaseValue != 0 /*&& _unitValues[i].UIState != "user"*/)
                    {
                        int i1 = i;
                        if (propertiesSequenceNames[p] == PropertyHelper.MemberName(_properties[i]))
                        {
                            return i;
                        }
                    }
                }
            }
            return 0;
        }

        public bool CanBeCalculated()
        {
            return (GetValuesCount() == 2);
        }

        public int GetUserCount()
        {
            return _unitValues.Count(t => t.UIState == "user");
        }

        public int GetValuesCount()
        {
            return _unitValues.Count(t => t.Value > 0);
        }

        public void Finish()
        {
            for (int i = 0; i < values.Length; i++)
            {
                SetBaseValue(i);
            }
        }

        public bool Validate()
        {
            File.AppendAllText(@"c:\temp\test.txt", "2: Value0=" + MathExt.FixPrecision(values[0]) + " Value1=" + MathExt.FixPrecision(values[1]) + " Value2=" + MathExt.FixPrecision(values[2]) + "\r\n");
            return MathExt.FixPrecision(values[0]) == MathExt.FixPrecision(values[1])*MathExt.FixPrecision(values[2]);
        }

        public void ConvertCombinationsValuesToArray()
        {
            for (int i = 0; i < _properties.Length; i++)
            {
                _unitValues[i] = PropertyHelper.GetUnitValue(_properties[i]);
                values[i] = System.Math.Round(GetConvertedValue(i), 8, MidpointRounding.AwayFromZero);
            }
        }

        public void CorrectPropertyIncrements()
        {
            for (int i = 0; i < _properties.Length; i++)
            {
                PropertyIncrement.CorrectPropertyIncrement(_unitValues[i], ref values[i]);
            }
        }
    }
}