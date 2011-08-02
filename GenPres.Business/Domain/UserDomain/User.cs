using System;
using GenPres.Business.Data;
using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.Domain.UserDomain;
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

namespace GenPres.Business.Domain.UserDomain
{
    
    public class User : IUser
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

        public void OnCreate()
        {
            
        }

        public void OnNew()
        {
            
        }

        public void OnInitExisting()
        {
            
        }

        private static IUserRepository Repository
        {
            get { return DalServiceProvider.Instance.Resolve<IUserRepository>(); }
        }

        public static IUser NewUser()
        {
            return ObjectFactory.New<IUser>();
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

