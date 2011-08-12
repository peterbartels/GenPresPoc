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
        private static void InitializeUserTest()
        {
            var registry = new Registry();
            
            var repository = Isolate.Fake.Instance<UserSqlRepository>(Members.CallOriginal);
            registry.For<IUserRepository>().Use(repository);

            ObjectFactory.Configure(x => x.AddRegistry(registry));
            return;
        }

        [Isolated]
        [TestMethod]
        public void UserServiceLoginCallsRepositoryGetUserByUsername()
        {
            InitializeUserTest();
            var userRepository = StructureMap.ObjectFactory.GetInstance<IUserRepository>();
            UserService.AuthenticateUser("test", "test");
            Isolate.Verify.WasCalledWithAnyArguments(() => userRepository.GetUserByUsername(""));
        }

        [TestMethod]
        public void DataContextGetsAUserFromDatabase()
        {
            using (var ctx = GenPresDataManager.GetManager().GetContext())
            {
                var foundUser = (from i in ctx.User where i.Username == "Test" select i).FirstOrDefault();
                Assert.IsNotNull(foundUser);
            }
        }

        [TestMethod]
        public void UserRepositoryGetUserbyUsernameReturnsAvailableUser()
        {
            var userRepository = StructureMap.ObjectFactory.GetInstance<IUserRepository>();
            var user = userRepository.GetUserByUsername("Test");
            Assert.IsTrue(user.IsAvailable);
        }

        [TestMethod]
        public void CanCheckMd5()
        {
            string md5 = "098f6bcd4621d373cade4e832627b4f6";
            string password = "test";
            Assert.IsTrue(AuthenticationFunctions.MD5(password) == md5);
        }

        [TestMethod]
        public void UserUserNameIsLowerCase()
        {
            IUser user = User.NewUser();
            user.UserName = "TeSt";
            Assert.IsTrue(user.UserName == "test", "Username should be lowercase after setValue.");
        }

        [TestMethod]
        public void _User_IsDirty_after_propertyChange()
        {
            IUser user = User.NewUser();
            Assert.IsTrue(user.State == StatusEnum.New, "User (state) should be new after creation.");
            user.UserName = "TeSt";
            Assert.IsTrue(user.State == StatusEnum.Dirty, "User (state) should be dirty after change.");
        }
    }
}