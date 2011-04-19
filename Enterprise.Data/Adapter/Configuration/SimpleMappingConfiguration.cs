using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Enterprise.Data.AdapterTypes;
using Enterprise.Data.MappingTypes;

namespace Enterprise.Data.AdapterConfiguration
{
    public class SimpleMappingConfiguration<TSource, TDestination> 
        : IMappingConfiguration, ISimpleMappingExpression<TSource, TDestination>
    {
        internal List<IMappingType> _mappingTypes { get; set; }
        private SimpleAdapter<TSource, TDestination> _adapter;

        public SimpleMappingConfiguration(SimpleAdapter<TSource, TDestination> adapter)
        {
            _mappingTypes = new List<IMappingType>();
            _adapter = adapter;
        }

        internal static SimpleMappingConfiguration<TSource, TDestination> GetMappingConfiguration(SimpleAdapter<TSource, TDestination> adapter)
        {
            SimpleMappingConfiguration<TSource, TDestination> mappingConfiguration = new SimpleMappingConfiguration<TSource, TDestination>(adapter);
            return mappingConfiguration;
        }

        public ISimpleMappingExpression<TSource, TDestination> Map<TType>(Expression<Func<TSource, TType>> sourceMember, Expression<Func<TDestination, TType>> destinationMember)
        {
            AddMappingType(new MemberMap<TSource, TDestination>(_adapter, new MemberGetter(sourceMember), new MemberAccessor(destinationMember)));
            return this;
        }
        public ISimpleMappingExpression<TSource, TDestination> UseTypeConverter<TTypeConverter, TSourceType, TDestType>(Expression<Func<TSource, TSourceType>> sourceMember, Expression<Func<TDestination, TDestType>> destinationMember)
             where TTypeConverter:ITypeConverter<TTypeConverter>
        {
            AddMappingType(new MapUsingTypeConverter<TSource, TDestination, TSourceType, TDestType>(typeof(TTypeConverter), sourceMember, destinationMember));
            return this;
        }
        public ISimpleMappingExpression<TSource, TDestination> AutomapUsing<T>()
        {
            AddMappingType(new AutoMap<TSource, TDestination>(_adapter, typeof(TSource), typeof(TDestination), typeof(T)));
            return this;
        }

        public ISimpleMappingExpression<TSource, TDestination> UseCustomTypeMap<TType, TCusomMapper>()
            where TCusomMapper : ICustomTypeMap
        {
            throw new NotImplementedException();
        }

        public void MapChild<T>(Expression<Func<object, object>> source, Expression<Func<object, object>> destination)
        {
            throw new NotImplementedException();
        }

        public void AddMappingType(IMappingType type)
        {
            _mappingTypes.Add(type);
        }

        public Type GetSourceType()
        {
            return typeof(TSource);
        }

        public List<AdapterTypes.IMappingType> GetMappingTypes()
        {
            return _mappingTypes;
        }
    }
}
