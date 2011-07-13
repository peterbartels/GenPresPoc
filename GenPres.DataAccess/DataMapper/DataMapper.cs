using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Business.Domain;
using System.Reflection;

namespace GenPres.DataAccess.DataMapper
{
    public abstract class DataMapper<TBo, TDao> : IDataMapper<TBo, TDao> where TBo : ISavable
    {
        protected ISavable _bo;
        protected object _dao;

        protected DataMapper(TBo bo, TDao dao)
        {
            _bo = bo;
            _dao = dao;
        }

        public abstract void InitChildMappings();
        public abstract void MapFromBoToDao();
        public abstract void MapFromDaoToBo();

        public TDao MapFromBoToDao(TBo bo, TDao dao)
        {
            throw new NotImplementedException();
        }

        public TBo MapFromDaoToBo(TDao dao, TBo bo)
        {
            throw new NotImplementedException();
        }
    }


    public class ChildMapper<TSrcBo, TSrcDao> 
        where TSrcBo : ISavable
    {
        private TSrcBo _srcBo;
        private TSrcDao _srcDao;

        public ChildMapper(TSrcBo srcBo, TSrcDao srcDao)
        {
            _srcBo = srcBo;
            _srcDao = srcDao;
        }
        private void _mapObject<TDestBo, TDestDao>(Expression boConnection, Expression daoConnection, Type mapperType, bool toBo)
            where TDestBo : ISavable
            where TDestDao : class
        {
            object fromValue;
            object toValue;
            Type destType;
            Expression fromExpression;
            object srcParent;
            string mappingName;

            if (toBo)
            {
                srcParent = _srcBo;
                destType = typeof(TDestBo);
                fromValue = GetValue(daoConnection, _srcDao);
                toValue = GetValue(boConnection, _srcBo);
                fromExpression = boConnection;
                mappingName = "MapFromDaoToBo";
            }
            else
            {
                srcParent = _srcDao;
                fromValue = GetValue(boConnection, _srcBo);
                toValue = GetValue(daoConnection, _srcDao);
                destType = typeof(TDestDao);
                fromExpression = daoConnection;
                mappingName = "MapFromBoToDao";
            }

            if (fromValue != null)
            {
                object newObj;
                if (toValue == null)
                {
                    newObj = Activator.CreateInstance(destType);
                }else
                {
                    newObj = toValue;
                }
                SetValue(fromExpression, srcParent, newObj);
                var mapper = Activator.CreateInstance(mapperType, new object[] { GetValue(boConnection, _srcBo), GetValue(daoConnection, _srcDao) });
                mapperType.InvokeMember(mappingName, BindingFlags.InvokeMethod, null, mapper, null);
                
            }
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
            string mappingName = "MapFromDaoToBo";
            if(toBo)
            {
                IList fromList = (IList)GetValue(daoConnection.Body, _srcDao);
                IList toList = (IList)GetValue(boConnection.Body, _srcBo);
                if (fromList != null)
                {
                    Type toListType = ((PropertyInfo)((MemberExpression)boConnection.Body).Member).PropertyType;
                    if (toList == null)
                        toList = (IList)Activator.CreateInstance(toListType);
                    else
                        toList.Clear();

                    for (int i = 0; i < fromList.Count; i++)
                    {
                        Type listItemsType = toList.GetType().GetGenericArguments()[0];
                        object newObj = Activator.CreateInstance(listItemsType);
                        toList.Add(newObj);

                        object mapper = Activator.CreateInstance(mapperType, new[] { newObj, fromList[i] });
                        mapperType.InvokeMember(mappingName, BindingFlags.InvokeMethod, null, mapper, null);
                    }
                    SetValue(boConnection.Body, _srcBo, toList);
                }
            }else
            {
                IList fromList = (IList)GetValue(boConnection.Body, _srcBo);
                IList toList = (IList)GetValue(daoConnection.Body, _srcDao);
                if (fromList != null)
                {
                    Type toListType = ((PropertyInfo)((MemberExpression)daoConnection.Body).Member).PropertyType;
                    if (toList == null)
                        toList = (IList)Activator.CreateInstance(toListType);
                    else
                        toList.Clear();

                    for (int i = 0; i < fromList.Count; i++)
                    {
                        Type listItemsType = toList.GetType().GetGenericArguments()[0];
                        object newObj = Activator.CreateInstance(listItemsType);
                        toList.Add(newObj);

                        object mapper = Activator.CreateInstance(mapperType, new[] { fromList[i], newObj });
                        mapperType.InvokeMember(mappingName, BindingFlags.InvokeMethod, null, mapper, null);
                    }
                    SetValue(daoConnection.Body, _srcDao, toList);
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
