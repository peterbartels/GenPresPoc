using System;
using Informedica.GenPres.Business;
using Informedica.GenPres.Business.Data.IRepositories;
using Informedica.GenPres.Business.Domain.Users;

namespace Informedica.GenPres.Data.Repositories
{
    public class UserRepository : NHibernateRepository<User, Guid>, IUserRepository
    {

        public UserRepository()
            : base(SessionManager.SessionFactory)
        {
            
        }

        public User GetByName(string name)
        {
            var user = User.NewUser();
            user.UserName = name;
            return user;
        }

        public bool AuthenticateUserByUsernamePassword(string user, string password)
        {
            return (GetUserByUsernamePassword(user, password) != null);
        }

        private User GetUserByUsernamePassword(string user, string password)
        {
            return FindSingle(i => i.UserName == user && i.PassCrypt == AuthenticationFunctions.MD5(password));
        }

        public void Save(User user)
        {
            base.Add(user);
        }
    }
}
