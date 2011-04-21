using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GenPres.Business.Data.DataAccess.Mapper;
using GenPres.Business.Data.DataAccess.Repository;
using GenPres.Business.Domain;
using DB=GenPres.Database;
using GenPres.Business;

namespace GenPres.DataAccess.Repository
{
    public class UserRepository : IUserRepository, IRepository<IUser>
    {
        private UserMapper _userMapper = new UserMapper();

        public IUser GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IUser GetByName(string name)
        {
            var user = User.NewUser();
            user.UserName = name;
            return user;
        }

        public ISingleObject<IUser> GetUserByUsername(string user)
        {
            using(var ctx = GenPresDataManager.GetManager().GetContext())
            {
                List<IUser> businessUsers = new List<IUser>();

                var foundUsers = (from i in ctx.User where i.Username == user select i);

                foreach (Database.User dbUser in foundUsers)
                {
                    businessUsers.Add(_userMapper.MapDaoToBusinessObject(dbUser, User.NewUser()));
                }

                return SingleObject<IUser>.GetSingleObject(businessUsers);
            }
        }
    }
}
