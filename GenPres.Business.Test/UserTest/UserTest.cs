using System.Linq;
using GenPres.Business.Data.DataAccess.Repository;
using GenPres.Business.ServiceProvider;
using GenPres.DataAccess;
using GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Business.Service;
using GenPres.DataAccess.Repository;
using TypeMock.ArrangeActAssert;
using GenPres.Business.Domain;

namespace GenPres.Business.Test.UserTest
{
    [TestClass]
    public class UserTest : BaseGenPresTest
    {
        private IUserRepository _initializeUserTest()
        {
            var repository = Isolate.Fake.Instance<UserRepository>(Members.CallOriginal);
            DalServiceProvider.Instance.RegisterInstanceOfType<IUserRepository>(repository);
            return repository;
        }

        [Isolated]
        [TestMethod]
        public void UserServiceLogin_calls_RepositoryGetUserByUsername()
        {
            _initializeUserTest();
            var userRepository = DalServiceProvider.Instance.Resolve<IUserRepository>();
            UserService.AuthenticateUser("test", "test");
            Isolate.Verify.WasCalledWithAnyArguments(() => userRepository.GetUserByUsername(""));
        }

        [TestMethod]
        public void DataContext_gets_a_user_from_database()
        {
            using (var ctx = GenPresDataManager.GetManager().GetContext())
            {
                var foundUser = (from i in ctx.User where i.Username == "Test" select i).FirstOrDefault();
                Assert.IsNotNull(foundUser);
            }
        }

        [TestMethod]
        public void UserRepositoryGetUserbyUsername_returns_availableUser()
        {
            _initializeUserTest();
            var userRepository = DalServiceProvider.Instance.Resolve<IUserRepository>();
            var user = userRepository.GetUserByUsername("Test");
            Assert.IsTrue(user.IsAvailable);
        }

        [TestMethod]
        public void _can_check_md5()
        {
            string md5 = "098f6bcd4621d373cade4e832627b4f6";
            string password = "test";
            Assert.IsTrue(AuthenticationFunctions.MD5(password) == md5);
        }

        [TestMethod]
        public void _User_UserName_is_LowerCase()
        {
            User user = User.NewUser();
            user.UserName = "TeSt";
            Assert.IsTrue(user.UserName == "test", "Username should be lowercase after setValue.");
        }

        [TestMethod]
        public void _User_IsDirty_after_propertyChange()
        {
            User user = User.NewUser();
            Assert.IsTrue(user.State == StatusEnum.New, "User (state) should be new after creation.");
            user.UserName = "TeSt";
            Assert.IsTrue(user.State == StatusEnum.Dirty, "User (state) should be dirty after change.");
        }
    }
}
