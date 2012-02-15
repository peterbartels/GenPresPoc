using System;
using Informedica.GenPres.Business.Data.IRepositories;
using Informedica.GenPres.Service;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenPres.xTest.Service
{
    /// <summary>
    /// Summary description for GetUserTest
    /// </summary>
    [TestClass]
    public class UserTest : BaseGenPresTest
    {
        [TestMethod]
        public void UserServiceAuthenticateUserShouldCallUserRepositoryGetUserByUsernamePassword()
        {
            var repos = IsolateObject<IUserRepository>();
            Isolate.WhenCalled(() => repos.AuthenticateUserByUsernamePassword("", "")).ReturnRecursiveFake();
            var authenticatedUser = UserService.AuthenticateUser("","");
            try
            {
                Isolate.Verify.WasCalledWithAnyArguments(() => repos.AuthenticateUserByUsernamePassword("", ""));
            }
            catch (Exception e)
            {
                CheckVerifiyException(e, "User Repository method GetUserByUsernamePassword was not called by User Service method AuthenticateUser");
            }
        }
        
        [TestMethod]
        public void ThatUserServiceShouldBeAbleToAuthenticateAUser()
        {
            var result = UserService.AuthenticateUser("Peter", "Secret");
            Assert.IsTrue(result);
        }
    }
}
