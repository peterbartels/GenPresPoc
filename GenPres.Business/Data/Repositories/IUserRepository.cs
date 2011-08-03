using GenPres.Business.Domain;
using GenPres.Business.Domain.Users;

namespace GenPres.Business.Data.DataAccess.Repositories
{
    public interface IUserRepository
    {
        AvailableObject<IUser> GetUserByUsername(string user);
    }
}
