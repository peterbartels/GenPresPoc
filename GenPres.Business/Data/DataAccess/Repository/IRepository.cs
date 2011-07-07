using System;
using System.Collections.Generic;

namespace GenPres.Business.Data.DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Return all instances of type T.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> All();

        /// <summary>
        /// Return all instances of type T that match the expression exp.
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        IEnumerable<T> FindAll(Func<T, bool> exp);

        /// <summary>Returns the single entity matching the expression.
        /// Throws an exception if there is not exactly one such entity.</summary>
        /// <param name="exp"></param><returns></returns>
        T Single(Func<T, bool> exp);

        /// <summary>Returns the first element satisfying the condition.</summary>
        /// <param name="exp"></param><returns></returns>
        T First(Func<T, bool> exp);

        /// <summary>
        /// Mark an entity to be deleted when the context is saved.
        /// </summary>
        /// <param name="entity"></param>
        void MarkForDeletion(T entity);

        /// <summary>
        /// Create a new instance of type T.
        /// </summary>
        /// <returns></returns>
        T CreateInstance();

        /// <summary>Persist the data context.</summary>
        void SaveAll();
    } 
}
