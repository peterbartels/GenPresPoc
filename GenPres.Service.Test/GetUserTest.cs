using GenPres.Assembler;
using GenPres.Business.Service;
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
            var result = UserService.AuthenticateUser("Test", "Test");
            Assert.IsTrue(result);
        }
    }
}
