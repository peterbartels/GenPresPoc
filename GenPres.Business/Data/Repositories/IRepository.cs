using GenPres.Business.Domain;

namespace GenPres.Business.Data.DataAccess.Repositories
{
    public interface IRepository<TBo> where TBo : ISavable
    {
        void Submit();

        int Count();

        TBo Save(TBo businessObject);
    } 
}
