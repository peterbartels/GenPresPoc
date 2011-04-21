using GenPres.Business.Domain;
using DB = GenPres.Database;

namespace GenPres.Business.Data.DataAccess.Mapper
{
    public class UserMapper : IMapper
    {
        public IUser MapDaoToBusinessObject(object daoObj, IUser userBo)
        {
            var userDao = (DB.User)daoObj;
            userBo.UserName = userDao.Username;
            userBo.PassCrypt = userDao.PassCrypt;
            return userBo;
        }
    }
}
