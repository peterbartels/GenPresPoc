using System;
using System.Linq.Expressions;
using System.Reflection;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Business.Domain.Units;

namespace GenPres.Business.Util
{
    public static class PropertyHelper
    {
        public static UnitValue GetUnitValue(Expression<Func<UnitValue>> expression)
        {
            Expression ex = expression;
            return (UnitValue)Expression.Lambda(expression.Body).Compile().DynamicInvoke();
        }

        public static string ClassName(Expression<Func<UnitValue>> expression)
        {
            return (((MemberExpression) expression.Body).Member).DeclaringType.Name;
        }

        public static string MemberName(Expression<Func<UnitValue>> expression)
        {
            return (((MemberExpression) expression.Body).Member).Name;
        }
    }
}
