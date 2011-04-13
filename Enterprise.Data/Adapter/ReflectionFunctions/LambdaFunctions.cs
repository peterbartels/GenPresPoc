using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Enterprise.Data
{
    public static class LambdaFunctions
    {
        public static PropertyInfo AsPropertyInfo(Expression expression)
        {
            PropertyInfo info = null;
            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                info = ((MemberExpression)expression).Member as PropertyInfo;
            }
            return info;
        }
        private static Expression GetExpression(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Convert:
                    return ((UnaryExpression)expression).Operand;
                case ExpressionType.Lambda:
                    return ((LambdaExpression)expression).Body;
            }
            return expression;
        }
        public static FieldInfo AsFieldInfo(Expression expression)
        {
            FieldInfo info = null;
            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                info = ((MemberExpression)expression).Member as FieldInfo;
            }
            return info;
        }
        public static MemberTypes? GetMemberType(Expression expression)
        {
            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                return ((MemberExpression)expression).Member.MemberType;
            }
            return null;
        }
        public static object GetValue(object o, Delegate member)
        {
            return member.DynamicInvoke(o);
        }
        public static object GetValue(object o, Expression member)
        {
            if (member.NodeType == ExpressionType.Lambda)
            {
                LambdaExpression lambdaExpr = member as LambdaExpression;
                lambdaExpr.Compile();
                return lambdaExpr.Compile().DynamicInvoke(o);
            }
            else
            {
                Expression ex = GetExpression(member);
                if (GetMemberType(ex) == MemberTypes.Property)
                {
                    PropertyInfo targetPropertyInfo = LambdaFunctions.AsPropertyInfo(ex);
                    return targetPropertyInfo.GetValue(o, null);
                }
                if (GetMemberType(ex) == MemberTypes.Field)
                {
                    FieldInfo targetFieldInfo = AsFieldInfo(ex);
                    return targetFieldInfo.GetValue(o);
                }
            }
            return null;
        }

        public static void SetValue(object destination, object value, Expression targetExpression)
        {
            object realTarget = null;

            Expression expression = targetExpression;
            while (true)
            {
                if (expression.NodeType != ExpressionType.Convert && expression.NodeType != ExpressionType.Lambda) break;
                expression = GetExpression(expression); ;
            }

            if (targetExpression is LambdaExpression)
            {
                realTarget = FindRealTarget(expression, destination);
            }
            
            if (realTarget != null)
            {
                if (GetMemberType(expression) == MemberTypes.Property)
                {
                    PropertyInfo targetPropertyInfo = LambdaFunctions.AsPropertyInfo(expression);
                    targetPropertyInfo.SetValue(realTarget, value, null);
                }
                if (GetMemberType(expression) == MemberTypes.Field)
                {
                    FieldInfo targetFieldInfo = AsFieldInfo(expression);
                    targetFieldInfo.SetValue(destination, value);
                }
            }
        }


        private static object FindRealTarget(Expression ex, object destination)
        {
            object realTarget = destination;

            if (ex.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression memberExpression = ex as MemberExpression;
                if (memberExpression.Expression.NodeType != ExpressionType.Parameter)
                {
                    ParameterExpression parameter = GetParameterExpression(memberExpression.Expression);
                    if (parameter != null)
                    {
                        LambdaExpression _targetExpression = Expression.Lambda(memberExpression.Expression, parameter);
                        realTarget = _targetExpression.Compile().DynamicInvoke(destination);
                    }
                }
            }
            return realTarget;
        }
        private static ParameterExpression GetParameterExpression(Expression expression)
        {
            while (expression.NodeType == ExpressionType.MemberAccess)
            {
                expression = ((MemberExpression)expression).Expression;
            }
            if (expression.NodeType == ExpressionType.Parameter)
            {
                return (ParameterExpression)expression;
            }
            return null;
        }

        public static Type GetType(MemberInfo member)
        {
            if (member is FieldInfo)
            {
                return (member as FieldInfo).FieldType;
            }
            if (member is PropertyInfo)
            {
                return (member as PropertyInfo).PropertyType;
            }
            return null;
        }
        public static MemberInfo FindProperty(Expression lambdaExpression)
        {
            Expression expressionToCheck = lambdaExpression;

            bool done = false;

            while (!done)
            {
                switch (expressionToCheck.NodeType)
                {
                    case ExpressionType.Convert:
                        expressionToCheck = ((UnaryExpression)expressionToCheck).Operand;
                        break;
                    case ExpressionType.Lambda:
                        expressionToCheck = ((LambdaExpression)expressionToCheck).Body;
                        break;
                    case ExpressionType.MemberAccess:
                        var memberExpression = ((MemberExpression)expressionToCheck);

                        if (memberExpression.Expression.NodeType != ExpressionType.Parameter &&
                            memberExpression.Expression.NodeType != ExpressionType.Convert)
                        {
                            //throw new ArgumentException(string.Format("Expression '{0}' must resolve to top-level member.", lambdaExpression), "lambdaExpression");
                            return null;
                        }

                        MemberInfo member = memberExpression.Member;

                        return member;
                    default:
                        done = true;
                        break;
                }
            }

            return null;
        }
    }
}
