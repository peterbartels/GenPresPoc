using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using Enterprise.Data.AdapterConfiguration;
using Enterprise.Data.MappingTypes;

namespace Enterprise.Data.AdapterTypes
{
    internal class AutoMap<TSource, TDestination> : IMappingType
    {
        internal AutoMap(SimpleAdapter<TSource, TDestination> adapter, Type srcType, Type destType, Type destinationType)
        {
            IMappingConfiguration configuration = adapter.GetMappingConfiguration(srcType);
            /*if (destinationType == srcType)
            {
                srcType = destType;
                destType = destinationType;
            }*/
            MemberInfo[] srcMembers = srcType.GetMembers();
            MemberInfo[] destMembers = destType.GetMembers();
            foreach (MemberInfo srcMember in srcMembers)
            {
                if (srcMember.MemberType == MemberTypes.Property || srcMember.MemberType == MemberTypes.Field)
                {
                    MemberInfo destMember = (from i in destMembers where i.Name == srcMember.Name select i).SingleOrDefault<MemberInfo>();
                    
                    //We found a member!
                    if(destMember != null){

                        var entityParam = Expression.Parameter(srcMember.ReflectedType, srcMember.Name);
                        MemberExpression srcExpression = Expression.MakeMemberAccess(entityParam, srcMember);

                        entityParam = Expression.Parameter(destMember.ReflectedType, srcMember.Name);
                        MemberExpression destExpression = Expression.MakeMemberAccess(entityParam, destMember);

                        if (srcExpression.Type != destExpression.Type) continue;
                        
                        MemberMap<TSource, TDestination> map = new MemberMap<TSource, TDestination>(adapter, new MemberGetter(srcExpression), new MemberAccessor(destExpression));
                        configuration.AddMappingType(map);
                    }
                }
            }


        }
        public void Map(object src, object dest)
        {
            //Does nothing
        }
    }
}
