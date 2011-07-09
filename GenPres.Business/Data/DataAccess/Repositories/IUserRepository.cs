using GenPres.Business.Domain;

namespace GenPres.Business.Data.DataAccess.Repositories
{
    public interface IUserRepository
    {
        AvailableObject<IUser> GetUserByUsername(string user);
    }
}
