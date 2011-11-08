using GenPres.Business.Domain.Users;

namespace GenPres.Business.Data.IRepositories
{
    public interface IUserRepository
    {
        bool AuthenticateUserByUsernamePassword(string user, string password);
        void Save(User user);
    }
}
