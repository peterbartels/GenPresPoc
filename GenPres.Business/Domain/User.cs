using GenPres.Business.Data.DataAccess.Repository;
using GenPres.Business.ServiceProvider;
using GenPres.Business;

namespace GenPres.Business.Domain
{
    public class User : IUser
    {
        private int _id;
        private string _userName;
        private string _password;

        public int Id { get; set; }
        public string UserName { get; set; }
        public string PassCrypt { get; set; }


        private static IUserRepository Repository
        {
            get { return DalServiceProvider.Instance.Resolve<IUserRepository>(); }
        }

        private static IUser GetUserByUserName(string username)
        {
            return Repository.GetUserByUsername(username);
        }

        public static User NewUser()
        {
            return new User();
        }

        public static IUser FetchUser(object data)
        {
            IUser user = new User();
            Repository.MapToBusinessObject(data, user);
            return user;
        }

        public static bool AuthenticateUser(string username, string password)
        {
            IUser user = GetUserByUserName(username);
            return AuthenticationFunctions.MD5(password) == user.PassCrypt;
        }
    }
}
