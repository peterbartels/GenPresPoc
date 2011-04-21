using GenPres.Business.Domain;

namespace GenPres.Business.Data.DataAccess.Repository
{
    public interface IUserRepository
    {
        ISingleObject<IUser> GetUserByUsername(string user);
    }
}
