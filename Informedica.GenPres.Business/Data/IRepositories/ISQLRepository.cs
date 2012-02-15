using Informedica.GenPres.Business.Domain;

namespace Informedica.GenPres.Business.Data.IRepositories
{
    public interface ISQLRepository<TBo> where TBo : ISavable
    {
        void Submit();

        int Count();

        TBo Save(TBo businessObject);
    } 
}
