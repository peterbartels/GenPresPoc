using GenPres.Business.Domain;
using GenPres.Business.Domain.UserDomain;

namespace GenPres.Business.Service
{
    public static class UserService
    {
        public static bool AuthenticateUser(string userName, string password)
        {
            return User.AuthenticateUser(userName, password);            
        }
    }
}
