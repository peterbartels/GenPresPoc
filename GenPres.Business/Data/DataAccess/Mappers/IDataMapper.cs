namespace GenPres.Business.Data.DataAccess.Mappers
{
    public interface IDataMapper<T, TC>
    {
        TC MapFromBoToDao(T bo, TC dao);
        T MapFromDaoToBo(TC dao, T bo);
    }
}