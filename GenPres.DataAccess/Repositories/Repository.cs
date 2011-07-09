using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using GenPres.Business.Data.DataAccess;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.Domain;
using GenPres.Business.Domain.Patient;

namespace GenPres.DataAccess.Repositories
{
    public abstract class Repository<TDao, TBo> : IRepository<TDao, TBo>
    where TDao : class where TBo : ISavable
    {
        protected IDataContextFactory _dataContextFactory;

        public Repository(IDataContextFactory dataContextFactory)
        {
            _dataContextFactory = dataContextFactory;
        }

       
        public IEnumerable<TDao> All()
        {
            return GetTable;
        }

        public IEnumerable<TDao> FindAll(Func<TDao, bool> exp)
        {
            return GetTable.Where<TDao>(exp);
        }

        public ISingleObject<TDao> FindSingle(Func<TDao, bool> exp)
        {
            var records = GetTable.Where(exp);
            return SingleObject<TDao>.GetSingleObject(records);
        }

        public TDao Single(Func<TDao, bool> exp)
        {
            return GetTable.Single(exp);
        }

        public TDao First(Func<TDao, bool> exp)
        {
            return GetTable.First(exp);
        }

        public TDao Last(Func<TDao, bool> exp)
        {
            return GetTable.Last(exp);
        }

        public TDao GetById(int id)
        {
            return Get<TDao>(_dataContextFactory.Context, 1);
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


        public abstract IDataMapper<TBo, TDao> Mapper { get; }

        public int Count()
        {
            return GetTable.Count();
        }
        
        public virtual void MarkForDeletion(TDao entity)
        {
            _dataContextFactory.Context.GetTable<TDao>().DeleteOnSubmit(entity);
        }

        /// <summary>
        /// Create a new instance of type T.
        /// </summary>
        /// <returns></returns>
        public virtual TDao CreateInstance()
        {
            TDao entity = Activator.CreateInstance<TDao>();
            GetTable.InsertOnSubmit(entity);
            return entity;
        }

        public void SaveAll()
        {
            _dataContextFactory.SaveAll();
        }

        #region Properties

        private string PrimaryKeyName
        {
            get { return TableMetadata.RowType.IdentityMembers[0].Name; }
        }

        protected System.Data.Linq.Table<TDao> GetTable
        {
            get { return _dataContextFactory.Context.GetTable<TDao>(); }
        }

        private System.Data.Linq.Mapping.MetaTable TableMetadata
        {
            get { return _dataContextFactory.Context.Mapping.GetTable(typeof(TDao)); }
        }

        private System.Data.Linq.Mapping.MetaType ClassMetadata
        {
            get { return _dataContextFactory.Context.Mapping.GetMetaType(typeof(TDao)); }
        }

        #endregion

        public static TEntity Get<TEntity>(DataContext dataContext, int id)
        where TEntity : class
        {
            return Get<TEntity, int>(dataContext, id);
        }
        public static TEntity Get<TEntity, TKey>(DataContext dataContext, TKey id)
            where TEntity : class
        {
            // get the row from the database using the meta-model
            MetaType meta = dataContext.Mapping.GetTable(typeof(TEntity)).RowType;
            if (meta.IdentityMembers.Count != 1) throw new InvalidOperationException(
                "Composite identity not supported");
            string idName = meta.IdentityMembers[0].Member.Name;

            var param = Expression.Parameter(typeof(TEntity), "row");
            var lambda = Expression.Lambda<Func<TEntity, bool>>(
                Expression.Equal(
                    Expression.PropertyOrField(param, idName),
                    Expression.Constant(id, typeof(TKey))), param);

            return dataContext.GetTable<TEntity>().Single(lambda);
        }

    }
}
