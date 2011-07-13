using System;
using GenPres.Business.Data;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.Domain;
using GenPres.Business.Domain.Patient;
using GenPres.DataAccess.DataMapper.Mapper.User;
using GenPres.DataAccess.Object;
using DB=GenPres.Database;

namespace GenPres.DataAccess.Repositories
{
    public class UserRepository : Repository<IUser, DB.User>, IUserRepository
    {
        private UserMapper _userMapper = new UserMapper();

        public UserRepository() : base(new GenPresDataContextFactory())
        {
            
        }

        public override IDataMapper<IUser, Database.User> Mapper
        {
            get { return _userMapper; }
        }


        IdentityMap<IUser, Database.User>[] _identityMaps = new[]
        {
            new IdentityMap<IUser, Database.User>()                                                           
        };

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
