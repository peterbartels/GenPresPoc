using GenPres.Business.Service;
using GenPres.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.Test.Acceptance
{
    /// <summary>
    /// Summary description for GetUserTest
    /// </summary>
    [TestClass]
    public class GetUserTest
    {
        public GetUserTest()
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

        [TestMethod]
        public void CanLoginUser()
        {
            var loginController = new UserController();

            var result = loginController.Login("Test", "test");

            Assert.IsTrue(ActionResultParser.GetSuccessValueFromActionResult(result), "System user NOT successfully logged in");
        }
    }
}
