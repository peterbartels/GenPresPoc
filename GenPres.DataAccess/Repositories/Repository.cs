﻿using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.Domain;
using GenPres.DataAccess.Object;
using System.Reflection;

namespace GenPres.DataAccess.Repositories
{
    public abstract class Repository<TBo, TDao> : IRepository<TBo>
    where TDao : class where TBo : ISavable
    {
        protected IDataContextFactory _dataContextFactory;

        public abstract IDataMapper<TBo, TDao> Mapper { get; }

        public Repository(IDataContextFactory dataContextFactory)
        {
            _dataContextFactory = dataContextFactory;
        }

        public IEnumerable<TDao> AddDaoListToIdentityMap(IEnumerable<TDao> _list)
        {
            foreach (TDao dao in _list)
            {
                AddSingleToIdentityMap(dao);
            }
            return _list;
        }
        
        public TDao AddSingleToIdentityMap(TDao dao)
        {
            return dao;
        }

        public IEnumerable<TDao> All()
        {
            return AddDaoListToIdentityMap(GetTable);
        }

        public IEnumerable<TDao> FindAll(Func<TDao, bool> exp)
        {
            return AddDaoListToIdentityMap(GetTable.Where<TDao>(exp));
        }

        public ISingleObject<TDao> FindSingle(Func<TDao, bool> exp)
        {
            var records = AddDaoListToIdentityMap(GetTable.Where(exp));
            return SingleObject<TDao>.GetSingleObject(records);
        }

        public TDao Single(Func<TDao, bool> exp)
        {
            return AddSingleToIdentityMap(GetTable.Single(exp));
        }

        public TDao First(Func<TDao, bool> exp)
        {
            return AddSingleToIdentityMap(GetTable.First(exp));
        }

        public TDao Last(Func<TDao, bool> exp)
        {
            return AddSingleToIdentityMap(GetTable.Last(exp));
        }

        public TDao GetById(int id)
        {
            /*for (int i = 0; i < GetIdentityMaps.Length; i++)
            {
                if (GetIdentityMaps[i].GetDaoType() == typeof(TDao))
                {
                    if(GetIdentityMaps[i].ContainsKey(id))
                    {
                        return GetIdentityMaps[i].Find(id);
                    }
                }
            }*/
            return AddSingleToIdentityMap(Get<TDao>(id));
        }

        public TBo Save(TBo businessObject)
        {
            TDao dao;
            if (businessObject.IsNew)
            {
                dao = CreateInstance();
            }
            else
            {
                dao = GetById(businessObject.Id);
            }
            Mapper.MapFromBoToDao(businessObject, dao);
            return businessObject;
        }

        public int Count()
        {
            return GetTable.Count();
        }
        
        public virtual void MarkForDeletion(TDao entity)
        {
            _dataContextFactory.Context.GetTable<TDao>().DeleteOnSubmit(entity);
        }

        public virtual TDao CreateInstance()
        {
            TDao entity = Activator.CreateInstance<TDao>();
            GetTable.InsertOnSubmit(entity);
            return entity;
        }

        public void Submit()
        {
            _dataContextFactory.Submit();
        }

        #region Properties

        private string PrimaryKeyName
        {
            get { return TableMetadata.RowType.IdentityMembers[0].Name; }
        }

        protected Table<TDao> GetTable
        {
            get { return _dataContextFactory.Context.GetTable<TDao>(); }
        }

        private MetaTable TableMetadata
        {
            get { return _dataContextFactory.Context.Mapping.GetTable(typeof(TDao)); }
        }

        private MetaType ClassMetadata
        {
            get { return _dataContextFactory.Context.Mapping.GetMetaType(typeof(TDao)); }
        }

        #endregion

        public TEntity Get<TEntity>(int id)
        where TEntity : class
        {
            return Get<TEntity, int>(id);
        }
        public TEntity Get<TEntity, TKey>(TKey id)
            where TEntity : class
        {
            // get the row from the database using the meta-model
            MetaType meta = _dataContextFactory.Context.Mapping.GetTable(typeof(TEntity)).RowType;
            
            if (meta.IdentityMembers.Count != 1) throw new InvalidOperationException("Composite identity not supported");

            string idName = meta.IdentityMembers[0].Member.Name;

            var param = Expression.Parameter(typeof(TEntity), "row");
            var lambda = Expression.Lambda<Func<TEntity, bool>>(
                Expression.Equal(
                    Expression.PropertyOrField(param, idName),
                    Expression.Constant(id, typeof(TKey))), param);

            return _dataContextFactory.Context.GetTable<TEntity>().Single(lambda);
        }

        public object GetIdValue<TEntity>(TEntity dao)
            where TEntity : class
        {
            // get the row from the database using the meta-model
            MetaType meta = _dataContextFactory.Context.Mapping.GetTable(typeof(TEntity)).RowType;
            
            if (meta.IdentityMembers.Count != 1) throw new InvalidOperationException("Composite identity not supported");

            string idName = meta.IdentityMembers[0].Member.Name;
            return dao.GetType().GetProperty(idName).GetValue(dao, null);
        }

    }
}