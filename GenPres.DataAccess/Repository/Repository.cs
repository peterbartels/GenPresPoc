using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using GenPres.Business;
using GenPres.Business.Data.DataAccess.Repository;
using GenPres.DataAccess.DataMapper;
using StructureMap;
using GenPres.Database;

namespace GenPres.DataAccess.Repository
{
    public class Repository<T> : IRepository<T>
    where T : class
    {
        protected IDataContextFactory _dataContextFactory;

        public Repository(IDataContextFactory dataContextFactory)
        {
            _dataContextFactory = dataContextFactory;
        }

       
        public IEnumerable<T> All()
        {
            return GetTable;
        }

        public IEnumerable<T> FindAll(Func<T, bool> exp)
        {
            return GetTable.Where<T>(exp);
        }

        public ISingleObject<T> FindSingle(Func<T, bool> exp)
        {
            var records = GetTable.Where(exp);
            return SingleObject<T>.GetSingleObject(records);
        }

        public T Single(Func<T, bool> exp)
        {
            return GetTable.Single(exp);
        }

        public T First(Func<T, bool> exp)
        {
            return GetTable.First(exp);
        }

        public T Last(Func<T, bool> exp)
        {
            return GetTable.Last(exp);
        }

        public T GetById(int id)
        {
            return Get<T>(_dataContextFactory.Context, 1);
        }

        public int Count()
        {
            return GetTable.Count();
        }
        
        public virtual void MarkForDeletion(T entity)
        {
            _dataContextFactory.Context.GetTable<T>().DeleteOnSubmit(entity);
        }

        /// <summary>
        /// Create a new instance of type T.
        /// </summary>
        /// <returns></returns>
        public virtual T CreateInstance()
        {
            T entity = Activator.CreateInstance<T>();
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

        protected System.Data.Linq.Table<T> GetTable
        {
            get { return _dataContextFactory.Context.GetTable<T>(); }
        }

        private System.Data.Linq.Mapping.MetaTable TableMetadata
        {
            get { return _dataContextFactory.Context.Mapping.GetTable(typeof(T)); }
        }

        private System.Data.Linq.Mapping.MetaType ClassMetadata
        {
            get { return _dataContextFactory.Context.Mapping.GetMetaType(typeof(T)); }
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
