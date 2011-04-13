using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace Enterprise.Data.AdapterTypes
{
    class MapUsingTypeConverter<TSource, TDestination, TSourceType, TDestType> : IMappingType
    {
        Type _type;
        private Expression<Func<TSource, TSourceType>> sourceProp { get; set; }
        private Expression<Func<TDestination, TDestType>> destinationProp { get; set; }

        public MapUsingTypeConverter(Type type, Expression<Func<TSource, TSourceType>> srcMember, Expression<Func<TDestination, TDestType>> destMember)
        {
            _type = type;
            sourceProp = srcMember;
            destinationProp = destMember;
        }
        public void Map(object src, object dest)
        {
            object c = Activator.CreateInstance(_type);
            //object value = _type.InvokeMember("Convert", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public, null, c, new object[] { LambdaFunctions.GetValue<TSource>(src, sourceProp)});
            /*if (value.GetType() != ReflectionFunctions.GetType(destinationProp))
            {
                throw new Exception("Cannot cast property " + value.GetType().Name + " given type " + value.GetType() + " expected type: " + ReflectionFunctions.GetType(destinationProp));
            }*/
            //ReflectionFunctions.SetValue(dest, value, destinationProp);
        }
    }
}
