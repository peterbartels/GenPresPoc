using Informedica.GenPres.Business.Domain;

namespace Informedica.GenPres.Business.Data
{
    public interface IDataMapper<T, TC> where T : ISavable
    {
        TC MapFromBoToDao(T bo, TC dao);
        T MapFromDaoToBo(TC dao, T bo);
    }
}