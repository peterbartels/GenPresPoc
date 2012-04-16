using System;
//TEMPWEG using Informedica.GenPres.Business.Aspect;
using Informedica.GenPres.Business.Data.IRepositories;

namespace Informedica.GenPres.Business.Domain.Users
{
    public enum StatusEnum
    {
        New = 0,
        Dirty = 1
    }

    public interface IChangeTrackable
    {
        StatusEnum State { get; set; }
    }

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

        //TEMPWEG [LowerCase]
        //TEMPWEG [ChangeState]
        public virtual string UserName { get; set; }


        //TEMPWEG [ChangeState]
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