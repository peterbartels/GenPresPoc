using Informedica.GenPres.Service;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Business.UserTest
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
