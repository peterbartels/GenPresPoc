using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Business.Domain;
using System.Reflection;

namespace GenPres.DataAccess.DataMapper
{
    public abstract class DataMapper<TBo, TDao> : IDataMapper<TBo, TDao> where TBo : class, ISavable
    {
        protected IDataContextManager ContextManager;

        protected DataMapper(IDataContextManager dataContextManager)
        {
            ContextManager = dataContextManager;
        }

        public abstract TDao MapFromBoToDao(TBo _bo, TDao _dao);
        public abstract TBo MapFromDaoToBo(TDao _dao, TBo _bo);
    }


    public class ChildMapper<TSrcBo, TSrcDao> 
        where TSrcBo : ISavable
    {
        private TSrcBo _srcBo;
        private TSrcDao _srcDao;
        private IDataContextManager ContextManager;

        public ChildMapper(TSrcBo srcBo, TSrcDao srcDao, IDataContextManager contextManager)
        {
            _srcBo = srcBo;
            _srcDao = srcDao;
            ContextManager = contextManager;
        }

        private void _mapObject<TDestBo, TDestDao>(Expression boConnection, Expression daoConnection, Type mapperType, bool toBo)
            where TDestBo : ISavable
            where TDestDao : class
        {
            object fromObj;
            object toObj;
            Type destType;
            Expression fromExpression;
            object srcParent;
            string mappingName;

            if (toBo)
            {
                srcParent = _srcBo;
                destType = typeof(TDestBo);
                fromObj = GetValue(daoConnection, _srcDao);
                toObj = GetValue(boConnection, _srcBo);
                fromExpression = boConnection;
                mappingName = "MapFromDaoToBo";
            }
            else
            {
                srcParent = _srcDao;
                fromObj = GetValue(boConnection, _srcBo);
                toObj = GetValue(daoConnection, _srcDao);
                destType = typeof(TDestDao);
                fromExpression = daoConnection;
                mappingName = "MapFromBoToDao";
            }

            if (fromObj != null)
            {
                if (toObj == null)
                {
                    toObj = ObjectFactory.InitExisting(destType);
                }
                SetValue(fromExpression, srcParent, toObj);
                _invokeMapper(mapperType, mappingName, fromObj, toObj);
            }
        }

        private void _invokeMapper(Type mapperType, string mappingName, object fromValue, object toValue)
        {
            var mapper = Activator.CreateInstance(mapperType, new object[] { ContextManager });
            var args = new[] { fromValue, toValue };
            mapperType.InvokeMember(mappingName, BindingFlags.InvokeMethod, null, mapper, args);
        }

        public void Map<TDestBo, TDestDao>(Expression<Func<TSrcBo, TDestBo>> boConnection, Expression<Func<TSrcDao, TDestDao>> daoConnection, Type mapperType, bool toBo)
            where TDestBo : ISavable where TDestDao : class
        {
            _mapObject<TDestBo, TDestDao>(boConnection.Body, daoConnection.Body, mapperType, toBo);
        }

        public void MapCollection<TDestBo, TDestDao>(Expression<Func<TSrcBo, TDestBo>> boConnection, Expression<Func<TSrcDao, TDestDao>> daoConnection, Type mapperType, bool toBo)
            where TDestBo : IList
            where TDestDao : IList
        {
            IList fromList;
            IList toList;
            string mappingName;
            Expression fromExpression;
            if (toBo)
            {
                fromList = (IList)GetValue(daoConnection.Body, _srcDao);
                toList = (IList)GetValue(boConnection.Body, _srcBo);
                mappingName = "MapFromDaoToBo";
                fromExpression = boConnection.Body;
            }else
            {
                fromList = (IList)GetValue(boConnection.Body, _srcBo);
                toList = (IList)GetValue(daoConnection.Body, _srcDao);
                mappingName = "MapFromBoToDao";
                fromExpression = daoConnection.Body;
            }
            
            if(toBo)
            {
                if (fromList != null)
                {
                    Type toListType = ((PropertyInfo)((MemberExpression)fromExpression).Member).PropertyType;
                    if (toList == null)
                        toList = (IList)Activator.CreateInstance(toListType);
                    else
                        toList.Clear();

                    for (int i = 0; i < fromList.Count; i++)
                    {
                        Type listItemsType = toList.GetType().GetGenericArguments()[0];
                        object newObj = ObjectFactory.InitExisting(listItemsType);
                        toList.Add(newObj);
                        _invokeMapper(mapperType, mappingName, fromList[i], newObj);
                    }
                    SetValue(boConnection.Body, _srcBo, toList);
                }
            }
        }

        static public Type GetDeclaredType<TSelf>(TSelf self)
        {
            return typeof(TSelf);
        }

        private static object GetValue(Expression me, object obj)
        {
            return ((PropertyInfo)((MemberExpression)me).Member).GetValue(obj, null);
        }

        private static void SetValue(Expression me, object obj, object value)
        {
            ((PropertyInfo)((MemberExpression)me).Member).SetValue(obj, value, null);
        }
    }
}
