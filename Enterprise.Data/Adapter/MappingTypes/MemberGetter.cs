using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace Enterprise.Data.MappingTypes
{
    public class MemberGetter : IMemberGetter
    {
        protected Expression _expression;
        protected MemberGetter() { }
        //protected MemberInfo _memberInfo;
        protected Delegate _compiledExpression;

        public MemberGetter(Expression expression)
        {
            _expression = expression;
            if (_expression is LambdaExpression)
            {
                _compiledExpression = ((LambdaExpression)expression).Compile();
            }
            //_memberInfo = LambdaFunctions.FindProperty(expression);
        }
        public object GetValue(object source)
        {
            if (_compiledExpression != null)
            {
                return LambdaFunctions.GetValue(source, _compiledExpression);
            }
            return LambdaFunctions.GetValue(source, _expression);
        }
    }
}
