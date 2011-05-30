using GenPres.Business.Domain;

namespace GenPres.Business.Data.DataAccess.Mapper.User
{
    public class UserMapper : IMapper
    {
        public IUser MapDaoToBusinessObject(object daoObj, IUser userBo)
        {
            var userDao = (Database.User)daoObj;
            userBo.UserName = userDao.Username;
            userBo.PassCrypt = userDao.PassCrypt;
            return userBo;
        }
    }
}
