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
    
    public class User : IChangeTrackable
    {
        private StatusEnum _state = StatusEnum.New;

        public virtual StatusEnum State
        {
            get { return _state; }
            set { _state = value; }
        }

        public virtual void Save()
        {
            Repository.Save(this);
        }

        public virtual Guid Id { get; set; }

        [LowerCase]
        [ChangeState]
        public virtual string UserName { get; set; }


        [ChangeState]
        public virtual string PassCrypt { get; set; }

        public virtual bool IsNew { get { return (Id == Guid.Empty); } }

        private static IUserRepository Repository
        {
            get { return StructureMap.ObjectFactory.GetInstance<IUserRepository>(); }
        }

        public static User NewUser()
        {
            return new User();
        }

        public static bool AuthenticateUser(string username, string password)
        {
            return Repository.AuthenticateUserByUsernamePassword(username.ToLower(), password);
        }
    }
}

