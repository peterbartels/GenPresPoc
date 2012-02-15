using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GenPres.Business.Data.IRepositories;
using NHibernate;
using NHibernate.Linq;

namespace GenPres.Data.Repositories
{
    public class NHibernateRepository<T, TId> : NHibernateBase, IRepository<T, TId>
        where T : class
    {
        public NHibernateRepository(ISessionFactory factory) : base(factory) { }

        public IEnumerator<T> GetEnumerator()
        {
            return Transact(() => Session.Query<T>().GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Transact(() => GetEnumerator());
        }
        public T FindSingle(Func<T, bool> s)
        {
            return this.SingleOrDefault(s);
        }

        public IEnumerable<T> Find(Func<T, bool> s)
        {
            return Transact(() => Session.Query<T>().Where(s));
        }

        public virtual void Add(T item)
        {
            Transact(() => Session.Save(item));
        }

        public virtual void SaveOrUpdate(T item)
        {
            Transact(() => Session.SaveOrUpdate(item));
        }

        public virtual bool Contains(T item)
        {
            //if (item.IdIsDefault(item.Id)) return false;
            //return Transact(() => Session.Get<T>(item.Id)) != null;
            return false;
        }

        public virtual int Count
        {
            get { return Transact(() => Session.Query<T>().Count()); }
        }

        public virtual bool Remove(T item)
        {
            Transact(() => Session.Delete(item));
            return true;
        }
    }
}
