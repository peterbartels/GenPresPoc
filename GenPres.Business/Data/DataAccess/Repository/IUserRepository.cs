using GenPres.Business.Domain;

namespace GenPres.Business.Data.DataAccess.Repository
{
    public interface IUserRepository
    {
        IUser GetUserByUsername(string userName);
        void MapToBusinessObject(object dao, IUser bo);
    }
}
