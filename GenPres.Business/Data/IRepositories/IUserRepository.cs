using GenPres.Business.Domain.Users;

namespace GenPres.Business.Data.IRepositories
{
    public interface IUserRepository
    {
        AvailableObject<IUser> GetUserByUsername(string user);
    }
}
