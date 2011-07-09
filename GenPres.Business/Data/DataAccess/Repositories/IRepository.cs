using System;
using System.Collections.Generic;
using System.Data.Linq;
using GenPres.Business.Data.DataAccess.Mappers;

namespace GenPres.Business.Data.DataAccess.Repositories
{
    public interface IRepository<TDao, TBo> where TDao : class
    {
        IEnumerable<TDao> All();

        IEnumerable<TDao> FindAll(Func<TDao, bool> exp);

        TDao Single(Func<TDao, bool> exp);

        TDao First(Func<TDao, bool> exp);

        void MarkForDeletion(TDao entity);

        TDao CreateInstance();

        void SaveAll();

        int Count();

        TDao GetById(int id);

        TBo Save(TBo businessObject);

        IDataMapper<TBo, TDao> Mapper { get; }

    } 
}
