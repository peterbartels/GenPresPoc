using GenPres.Business.Service;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Service
{
    /// <summary>
    /// Summary description for GetUserTest
    /// </summary>
    [TestClass]
    public class UserTest : BaseGenPresTest
    {
        [TestMethod]
        public void CanLoginUser()
        {
            var result = UserService.AuthenticateUser("Test", "Test");
            Assert.IsTrue(result);
        }
    }
}
