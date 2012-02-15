using Informedica.GenPres.Business.Domain.Users;

namespace Informedica.GenPres.Business.Data.IRepositories
{
    public interface IUserRepository
    {
        bool AuthenticateUserByUsernamePassword(string user, string password);
        void Save(User user);
    }
}
