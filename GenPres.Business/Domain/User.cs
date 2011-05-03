using System;
using GenPres.Business.Data.DataAccess.Repository;
using GenPres.Business.ServiceProvider;
using GenPres.Business.Aspect;

public enum StatusEnum
{
    New = 0,
    Dirty = 1
}

public interface IChangeTrackable
{
    StatusEnum State { get; set; }
}

namespace GenPres.Business.Domain
{
    
    public class User : IUser, IChangeTrackable
    {
        private StatusEnum _state = StatusEnum.New;

        public StatusEnum State
        {
            get { return _state; }
            set { _state = value; }
        }

        private int _id;
        private string _userName;
        private string _password;

        public int Id { get; set; }

        [LowerCase]
        [ChangeState]
        public string UserName { get; set; }

        [ChangeState]
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

