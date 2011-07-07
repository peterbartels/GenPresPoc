using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GenPres.Business.Data;
using GenPres.Business.Data.DataAccess.Mapper;
using GenPres.Business.Data.DataAccess.Mapper.User;
using GenPres.Business.Data.DataAccess.Repository;
using GenPres.Business.Domain;
using DB=GenPres.Database;
using GenPres.Business;

namespace GenPres.DataAccess.Repository
{
    public class UserRepository : Repository<DB.User>, IUserRepository
    {
        private UserMapper _userMapper = new UserMapper();

        public UserRepository() : base(new GenPresDataContext())
        {
            
        }

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

        public AvailableObject<IUser> GetUserByUsername(string user)
        {
            var foundUser = FindSingle(i => i.Username == "test");
            
            if(foundUser.IsAvailable)
            {
                var bo =_userMapper.MapDaoToBusinessObject(foundUser.Object, User.NewUser());
                return AvailableObject<IUser>.Create(true, bo);
            }

            return AvailableObject<IUser>.Create(false, User.NewUser());
        }
    }
}
