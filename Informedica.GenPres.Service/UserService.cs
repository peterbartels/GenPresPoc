using Informedica.GenPres.Business.Domain.Users;

namespace Informedica.GenPres.Service
{
    public static class UserService
    {
        public static bool AuthenticateUser(string userName, string password)
        {
            return User.AuthenticateUser(userName, password);            
        }
    }
}
