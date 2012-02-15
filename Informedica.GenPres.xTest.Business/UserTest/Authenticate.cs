using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Business.Service;

namespace GenPres.xTest.Business.UserTest
{
    /// <summary>
    /// Summary description for UserTest
    /// </summary>
    [TestClass]
    public class Authenticate : BaseGenPresTest
    {
        [TestMethod]
        public void UserCanAuthenticate()
        {
            string username = "Peter";
            string password = "Secret";
            Assert.IsTrue(UserService.AuthenticateUser(username, password));
        }
    }
}
