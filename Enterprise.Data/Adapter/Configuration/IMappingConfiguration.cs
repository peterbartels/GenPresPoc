using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Enterprise.Data.AdapterConfiguration
{
    public interface IMappingConfiguration
    {
        Type GetSourceType();
        List<AdapterTypes.IMappingType> GetMappingTypes();
        void AddMappingType(AdapterTypes.IMappingType type);
    }
    public interface IMappingExpression<TSrc, TDest>
    {
    }
    public interface ISimpleMappingExpression<TSrc, TDest> : IMappingExpression<TSrc, TDest>
    {
        ISimpleMappingExpression<TSrc, TDest> Map<TType>(Expression<Func<TSrc, TType>> srcMember, Expression<Func<TDest, TType>> destMember);

        

        ISimpleMappingExpression<TSrc, TDest> UseTypeConverter<TInterface, TSourceType, TDestType>(
            Expression<Func<TSrc, TSourceType>> srcMember, Expression<Func<TDest, TDestType>> destMember)
            where TInterface : ITypeConverter<TInterface>;

        ISimpleMappingExpression<TSrc, TDest> AutomapUsing<T>(); 
        ISimpleMappingExpression<TSrc, TDest> UseCustomTypeMap<TType, TCustomMapper>() where TCustomMapper : ICustomTypeMap;
        void MapChild<T>(Expression<Func<object, object>> source, Expression<Func<object, object>> destination);
    }
    public interface ICslaLinqMappingExpression<TSrc, TDest> : ISimpleMappingExpression<TSrc, TDest>
    {
        
    }
}
