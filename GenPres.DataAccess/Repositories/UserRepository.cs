using System;
using GenPres.Business.Data;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.Domain;
using GenPres.DataAccess.DataMapper.Mapper.User;
using DB=GenPres.Database;

namespace GenPres.DataAccess.Repositories
{
    public class UserRepository : Repository<DB.User, User>, IUserRepository
    {
        private UserMapper _userMapper = new UserMapper();

        public UserRepository() : base(new GenPresDataContextFactory())
        {
            
        }

        public IUser GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override IDataMapper<User, Database.User> Mapper
        {
            get { throw new NotImplementedException(); }
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
                var bo =_userMapper.MapFromDaoToBo(foundUser.Object, User.NewUser());
                return AvailableObject<IUser>.Create(true, bo);
            }

            return AvailableObject<IUser>.Create(false, User.NewUser());
        }
    }
}
