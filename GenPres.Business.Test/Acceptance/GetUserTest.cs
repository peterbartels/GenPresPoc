using GenPres.Controllers;
using GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.Business.Test.Acceptance
{
    /// <summary>
    /// Summary description for GetUserTest
    /// </summary>
    [TestClass]
    public class GetUserTest : BaseGenPresTest
    {
        [TestMethod]
        public void CanLoginUser()
        {
            var loginController = new UserController();

            var result = loginController.Login("Test", "Test");

            Assert.IsTrue(ActionResultParser.GetSuccessValueFromActionResult(result), "System user NOT successfully logged in");
        }
    }
}
