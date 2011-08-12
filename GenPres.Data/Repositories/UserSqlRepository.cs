using GenPres.Business.Data;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Users;
using GenPres.Data.DAO.Mapper.User;
using GenPres.Data.Managers;
using User = GenPres.Database.User;

namespace GenPres.Data.Repositories
{
    public class UserSqlRepository : SqlRepository<IUser, User>, IUserRepository
    {
        private UserMapper _userMapper = new UserMapper();

        public UserSqlRepository()
            : base(StructureMap.ObjectFactory.GetInstance<IDataContextManager>())
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
            var user = Business.Domain.Users.User.NewUser();
            user.UserName = name;
            return user;
        }

        public AvailableObject<IUser> GetUserByUsername(string user)
        {
            var foundUser = FindSingle(i => i.Username == "test");
            
            if(foundUser.IsAvailable)
            {
                var bo =_userMapper.MapFromDaoToBo(foundUser.Object, Business.Domain.Users.User.NewUser());
                return AvailableObject<IUser>.Create(true, bo);
            }

            return AvailableObject<IUser>.Create(false, Business.Domain.Users.User.NewUser());
        }
    }
}
