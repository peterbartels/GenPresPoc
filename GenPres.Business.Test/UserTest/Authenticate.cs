using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.ServiceProvider;
using GenPres.DataAccess.Repositories;
using GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Business.Service;
using TypeMock.ArrangeActAssert;

namespace GenPres.Business.Test.UserTest
{
    /// <summary>
    /// Summary description for UserTest
    /// </summary>
    [TestClass]
    public class Authenticate : BaseGenPresTest
    {
        
        private IUserRepository _initializeUserTest()
        {
            var repository = Isolate.Fake.Instance<UserRepository>(Members.CallOriginal);
            DalServiceProvider.Instance.RegisterInstanceOfType<IUserRepository>(repository);
            return repository;
        }

        [TestMethod]
        public void User_can_Authenticate()
        {
            _initializeUserTest();
            string username = "test";
            string password = "Test";
            Assert.IsTrue(UserService.AuthenticateUser(username, password));
        }       
    }
}
