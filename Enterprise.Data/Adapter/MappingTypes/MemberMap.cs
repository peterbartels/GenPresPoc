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
    internal class MemberMap<TSource, TDestination> : IMappingType
    {
        private IMemberGetter source { get; set; }
        private MemberAccessor destination { get; set; }
        private SimpleAdapter<TSource, TDestination> _adapter;

        internal MemberMap(SimpleAdapter<TSource, TDestination> adapter, IMemberGetter src, MemberAccessor dest)
        {
            _adapter = adapter;
            source = src;
            destination = dest;
        }

        public void Map(object src, object dest)
        {   
            object srcValue = source.GetValue(src);
            if (srcValue != null)
            {
                IMappingConfiguration mapConfig = _adapter.GetMappingConfiguration(srcValue.GetType());
                if (mapConfig != null)
                {
                    _adapter.Map(srcValue, destination.GetValue(dest));
                    return;
                }
            }
            destination.setValue(dest, srcValue);
        }
    }
}
