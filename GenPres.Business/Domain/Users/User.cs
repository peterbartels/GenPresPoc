using System;
using GenPres.Business.Data;
using GenPres.Business.Aspect;
using GenPres.Business.Data.IRepositories;

public enum StatusEnum
{
    New = 0,
    Dirty = 1
}

public interface IChangeTrackable
{
    StatusEnum State { get; set; }
}

namespace GenPres.Business.Domain.Users
{
    
    public class User : IUser
    {
        private StatusEnum _state = StatusEnum.New;

        public StatusEnum State
        {
            get { return _state; }
            set { _state = value; }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public int Id { get; set; }

        [LowerCase]
        [ChangeState]
        public string UserName { get; set; }


        [ChangeState]
        public string PassCrypt { get; set; }

        public bool IsNew { get { return (Id == 0); } }

        private static IUserRepository Repository
        {
            get { return StructureMap.ObjectFactory.GetInstance<IUserRepository>(); }
        }

        public static IUser NewUser()
        {
            return ObjectCreator.New<IUser>();
        }

        public static bool AuthenticateUser(string username, string password)
        {
            AvailableObject<IUser> userObjectAvailable = Repository.GetUserByUsername(username);
            if (userObjectAvailable.IsAvailable)
            {
                return (AuthenticationFunctions.MD5(password) == userObjectAvailable.Object.PassCrypt);
            }
            return false;
        }
    }
}

