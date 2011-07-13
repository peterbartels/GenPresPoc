using GenPres.Business.Domain;

namespace GenPres.Business.Data.DataAccess.Mappers
{
    public interface IDataMapper<T, TC> where T : ISavable
    {
        TC MapFromBoToDao(T bo, TC dao);
        T MapFromDaoToBo(TC dao, T bo);
    }
}