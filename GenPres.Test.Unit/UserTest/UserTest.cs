using System.Linq;
using GenPres.Business.Data.DataAccess.Repository;
using GenPres.Business.ServiceProvider;
using GenPres.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Business.Service;
using GenPres.Business;
using GenPres.DataAccess.Repository;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace GenPres.Test.Unit.UserTest
{
    /// <summary>
    /// Summary description for UserTest
    /// </summary>
    [TestClass]
    public class UserTest
    {
        public UserTest()
        {
            Settings.SettingsManager.Instance.SetSettingsPath(@"C:\Users\vaio\Documents\GenPres_Refactored\GenPres\GenPres.Web\");
        }

        #region TestContext
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        #endregion

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

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
        public void UserRepositoryGetUserbyUsername_returns_IUser()
        {
            _initializeUserTest();
            var userRepository = DalServiceProvider.Instance.Resolve<IUserRepository>();
            var user = userRepository.GetUserByUsername("Test");
            Assert.IsTrue(user.UserName == "Test");
        }

        [TestMethod]
        public void _can_check_md5()
        {
            string md5 = "098f6bcd4621d373cade4e832627b4f6";
            string password = "test";
            Assert.IsTrue(AuthenticationFunctions.MD5(password) == md5);
        }
    }
}
