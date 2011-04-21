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

        public static User NewUser()
        {
            return new User();
        }

        public static bool AuthenticateUser(string username, string password)
        {
            ISingleObject<IUser> singleUserObject = Repository.GetUserByUsername(username);
            if(singleUserObject.IsAvailable)
            {
                return (AuthenticationFunctions.MD5(password) == ((SingleObject<IUser>) singleUserObject).ObjectResult.PassCrypt);
            }
            return false;
        }
    }
}

