using System;
using System.Linq.Expressions;
using System.Reflection;
using GenPres.Business.Domain.Units;

namespace GenPres.Business.Calculation
{
    public static class PropertyHelper
    {
        public static UnitValue GetUnitValue(object obj, Expression<Func<UnitValue>> expression)
        {
            return (UnitValue)((PropertyInfo)((MemberExpression)expression.Body).Member).GetValue(obj, null);
        }
        public static string ClassName(Expression<Func<UnitValue>> expression)
        {
            return (((MemberExpression) expression.Body).Member).DeclaringType.Name;
        }
        public static string MemberName(Expression<Func<UnitValue>> expression)
        {
            return (((MemberExpression) expression.Body).Member).Name;
        }
        //((System.Reflection.MemberInfo)(((new System.Linq.Expressions.Expression.MemberExpressionProxy(expression.Body)).Member).DeclaringType)).Name
    }
}
