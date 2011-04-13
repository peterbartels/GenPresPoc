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
        ISimpleMappingExpression<TSrc, TDest> Map(Expression<Func<TSrc, object>> srcMember, Expression<Func<TDest, object>> destMember);
        ISimpleMappingExpression<TSrc, TDest> MapUsingTypeConverter<Interface, sourceType, destType>(Expression<Func<TSrc, sourceType>> srcMember, Expression<Func<TDest, destType>> destMember);
        ISimpleMappingExpression<TSrc, TDest> AutomapUsing<T>();
    }
    public interface ICslaLinqMappingExpression<TSrc, TDest> : ISimpleMappingExpression<TSrc, TDest>
    {
        
    }
}
