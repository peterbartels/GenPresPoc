using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Enterprise.Data.MappingTypes
{
    public class MemberAccessor : MemberGetter
    {
        public MemberAccessor(Expression expression)
        {
            base._expression = expression;
            //base._memberInfo = LambdaFunctions.FindProperty(expression);
        }
        public void setValue(object destination, object value)
        {
            LambdaFunctions.SetValue(destination, value, base._expression);
        }
    }
}
