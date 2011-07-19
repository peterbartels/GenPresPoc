using GenPres.Business.Data.DataAccess;
using GenPres.Business.Data.DataAccess.Mappers;
using GenPres.Business.Domain;
using GenPres.Business.Domain.UserDomain;

namespace GenPres.DataAccess.DataMapper.Mapper.User
{
    public class UserMapper : IDataMapper<IUser, Database.User>
    {
        public Database.User MapFromBoToDao(IUser userBo, Database.User patientDao)
        {
            return new Database.User();
        }

        public IUser MapFromDaoToBo(Database.User daoObj, IUser userBo)
        {
            var userDao = (Database.User)daoObj;
            userBo.UserName = userDao.Username;
            userBo.PassCrypt = userDao.PassCrypt;
            return userBo;
        }
    }
}
