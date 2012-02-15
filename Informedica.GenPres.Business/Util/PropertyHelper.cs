using System;
using System.Linq;
using System.Linq.Expressions;
using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Business.Domain.Units;

namespace Informedica.GenPres.Business.Util
{
    public class PropertyHelper
    {
        public static UnitValue GetUnitValue(Expression<Func<UnitValue>> expression)
        {
            Expression ex = expression;
            return (UnitValue)Expression.Lambda(expression.Body).Compile().DynamicInvoke();
        }

        public static UnitValue GetPropertyByName(Expression<Func<UnitValue>>[] properties, string name)
        {
            var prop = (from i in properties where MemberName(i) == name select i).FirstOrDefault();
            if (prop == null) return null;
            return GetUnitValue(prop);
        }

        public static bool PropertyExpressionsEquals(Expression<Func<UnitValue>> property1, Expression<Func<UnitValue>> property2)
        {

            if (GetMemberInfo(property1).Member.Name == GetMemberInfo(property2).Member.Name
                && GetMemberInfo(property1).Member.DeclaringType.FullName == GetMemberInfo(property2).Member.DeclaringType.FullName
                )
            {
                return true;
            }
            return false;
        }

        public static string ClassName(Expression<Func<UnitValue>> expression)
        {
            return (((MemberExpression) expression.Body).Member).DeclaringType.Name;
        }

        public static string MemberName(Expression<Func<UnitValue>> expression)
        {
            return (((MemberExpression) expression.Body).Member).Name;
        }

        public static MemberExpression GetMemberInfo(Expression method)
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


        public static decimal[] ConvertUnitValuesToDecimalArray(Expression<Func<UnitValue>>[] properties, Prescription prescription)
        {
            decimal[] values = new decimal[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                //_unitValues[i] = GetUnitValue(properties[i]);
                values[i] = Math.Round(GetConvertedValue(properties[i], prescription), 8, MidpointRounding.AwayFromZero);
            }
            return values;
        }


        private static decimal GetConvertedValue(Expression<Func<UnitValue>> prop, Prescription prescription)
        {
            if (PropertyExpressionsEquals(prop, () => prescription.Frequency))
                return GetUnitValue(prop).BaseValue / UnitConverter.GetBaseValue(prescription.Frequency.Time, 1);

            if (PropertyExpressionsEquals(prop, () => prescription.Doses[0].Quantity))
                return GetUnitValue(prop).BaseValue;

            if (PropertyExpressionsEquals(prop, () => prescription.Doses[0].Total))
                return GetUnitValue(prop).BaseValue / UnitConverter.GetBaseValue(prescription.Frequency.Time, 1);

            if (PropertyExpressionsEquals(prop, () => prescription.Quantity))
                return GetUnitValue(prop).BaseValue;

            if (PropertyExpressionsEquals(prop, () => prescription.Total))
                return GetUnitValue(prop).BaseValue / UnitConverter.GetBaseValue(prescription.Frequency.Time, 1);

            if (PropertyExpressionsEquals(prop, () => prescription.FirstSubstance.Quantity))
                return GetUnitValue(prop).BaseValue;

            if (PropertyExpressionsEquals(prop, () => prescription.FirstSubstance.DrugConcentration))
                return GetUnitValue(prop).BaseValue;

            if (PropertyExpressionsEquals(prop, () => prescription.Drug.Quantity))
                return GetUnitValue(prop).BaseValue;

            if (PropertyExpressionsEquals(prop, () => prescription.Rate))
                return GetUnitValue(prop).BaseValue / UnitConverter.GetBaseValue("uur", 1);

            if (PropertyExpressionsEquals(prop, () => prescription.FirstDose.Rate))
                return GetUnitValue(prop).BaseValue / UnitConverter.GetBaseValue("uur", 1);

            if (PropertyExpressionsEquals(prop, () => prescription.Duration))
                return prescription.Duration.BaseValue / UnitConverter.GetBaseValue("uur", 1);

            throw new Exception("property has no convert configuration " + ((MemberExpression)prop.Body).Member.DeclaringType.FullName + "." + ((MemberExpression)prop.Body).Member.Name);
        }


        public static void SetBaseValue(Expression<Func<UnitValue>> prop, decimal value, Prescription prescription)
        {
            if (PropertyExpressionsEquals(prop, () => prescription.Frequency))
                GetUnitValue(prop).BaseValue = value * UnitConverter.GetBaseValue(prescription.Frequency.Time, 1); ;

            if (PropertyExpressionsEquals(prop, () => prescription.Doses[0].Quantity))
                GetUnitValue(prop).BaseValue = value;

            if (PropertyExpressionsEquals(prop, () => prescription.Doses[0].Total))
                GetUnitValue(prop).BaseValue = value * UnitConverter.GetBaseValue(prescription.Frequency.Time, 1);

            if (PropertyExpressionsEquals(prop, () => prescription.Total))
                GetUnitValue(prop).BaseValue = value * UnitConverter.GetBaseValue(prescription.Frequency.Time, 1);

            if (PropertyExpressionsEquals(prop, () => prescription.Quantity))
                GetUnitValue(prop).BaseValue = value;

            if (PropertyExpressionsEquals(prop, () => prescription.Drug.Quantity))
                GetUnitValue(prop).BaseValue = value;

            if (PropertyExpressionsEquals(prop, () => prescription.FirstSubstance.DrugConcentration))
                GetUnitValue(prop).BaseValue = value;

            if (PropertyExpressionsEquals(prop, () => prescription.FirstSubstance.Quantity))
                GetUnitValue(prop).BaseValue = value;

            if (PropertyExpressionsEquals(prop, () => prescription.FirstDose.Rate))
                GetUnitValue(prop).BaseValue = value * UnitConverter.GetBaseValue("uur", 1); ;

            if (PropertyExpressionsEquals(prop, () => prescription.Rate))
                GetUnitValue(prop).BaseValue = value * UnitConverter.GetBaseValue("uur", 1); ;

            if (PropertyExpressionsEquals(prop, () => prescription.Duration))
                GetUnitValue(prop).BaseValue = value * UnitConverter.GetBaseValue("uur", 1);
        }
    }
}
