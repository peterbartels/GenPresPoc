using System;
using System.Linq;
using GenPres.Business.Data.DataAccess.Mapper;
using GenPres.Business.Data.DataAccess.Repository;
using GenPres.Business.Domain;

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

        public IUser GetUserByUsername(string user)
        {
            using(var ctx = GenPresDataManager.GetManager().GetContext())
            {
                var foundUser = (from i in ctx.User where i.Username == user select i).FirstOrDefault();
                return User.FetchUser(foundUser);
            }
        }

        public void MapToBusinessObject(object dao, IUser bo)
        {
            _userMapper.MapDaoToBusinessObject(dao, bo);
        }
    }
}
