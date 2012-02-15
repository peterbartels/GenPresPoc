using System.Collections.Generic;
using GenPres.Business.Domain;

namespace GenPres.Business.Data.IRepositories
{
    public interface IRepository<T, TId> : IEnumerable<T>
        where T : class
    {
        void Add(T item);
        bool Contains(T item);
        int Count { get; }
        bool Remove(T item);
    }

}
