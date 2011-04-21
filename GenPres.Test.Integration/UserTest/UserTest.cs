using GenPres.Business.Data.DataAccess.Repository;
using GenPres.Business.ServiceProvider;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Business.Service;
using GenPres.DataAccess.Repository;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace GenPres.Test.Integration.UserTest
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

        private IUserRepository _initializeUserTest()
        {
            var repository = Isolate.Fake.Instance<UserRepository>(Members.CallOriginal);
            DalServiceProvider.Instance.RegisterInstanceOfType<IUserRepository>(repository);
            return repository;
        }

        #region TestContext
        private TestContext testContextInstance;

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

        [TestMethod]
        public void User_can_Authenticate()
        {
            _initializeUserTest();
            string username = "Test";
            string password = "test";
            Assert.IsTrue(UserService.AuthenticateUser(username, password));
        }       
    }
}
