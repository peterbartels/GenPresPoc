using System.Linq;
using GenPres.Business;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Users;
using GenPres.Data.Managers;
using GenPres.Data.Repositories;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Business.Service;
using TypeMock.ArrangeActAssert;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace GenPres.xTest.Business.UserTest
{
    [TestClass]
    public class UserTest : BaseGenPresTest
    {
        private IUserRepository IsolateUserRepository()
        {
            var repos = Isolate.Fake.Instance<IUserRepository>();
            ObjectFactory.Inject(repos);
            return repos;
        }

        [Isolated]
        [TestMethod]
        public void ThatUserServiceLoginCallsRepositoryGetUserByUsername()
        {
            var userRepository = IsolateUserRepository();
            UserService.AuthenticateUser("test", "test");
            Isolate.Verify.WasCalledWithAnyArguments(() => userRepository.AuthenticateUserByUsernamePassword("", ""));
        }

        [TestMethod]
        public void ThatUserRepositoryCanGetUserbyUsernameAndReturnsAvailableUser()
        {
            var userRepository = StructureMap.ObjectFactory.GetInstance<UserRepository>();
            var newUser = User.NewUser();
            newUser.UserName = "Test";
            newUser.PassCrypt = AuthenticationFunctions.MD5("test");
            newUser.Save();
            var user = userRepository.AuthenticateUserByUsernamePassword("test", "test");
            Assert.IsTrue(user);
        }

        [TestMethod]
        public void ThatAuthenticationFunctionCanCheckMd5()
        {
            string md5 = "098f6bcd4621d373cade4e832627b4f6";
            string password = "test";
            Assert.IsTrue(AuthenticationFunctions.MD5(password) == md5);
        }

        [TestMethod]
        public void ThatUserUserNameIsLowerCase()
        {
            User user = User.NewUser();
            user.UserName = "TeSt";
            Assert.IsTrue(user.UserName == "test", "Username should be lowercase after setValue.");
        }

        [TestMethod]
        public void ThatUserIsDirtyAfterPropertyChange()
        {
            User user = User.NewUser();
            Assert.IsTrue(user.State == StatusEnum.New, "User (state) should be new after creation.");
            user.UserName = "TeSt";
            Assert.IsTrue(user.State == StatusEnum.Dirty, "User (state) should be dirty after change.");
        }
    }
}