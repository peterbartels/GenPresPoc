using GenPres.Business.Domain;

namespace GenPres.Business.Data.DataAccess.Repository
{
    public interface IUserRepository
    {
        AvailableObject<IUser> GetUserByUsername(string user);
    }
}
