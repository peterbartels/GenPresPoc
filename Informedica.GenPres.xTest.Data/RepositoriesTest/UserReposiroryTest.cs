using Informedica.GenPres.Business.Data.IRepositories;
using Informedica.GenPres.Business.Domain.Users;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenPres.xTest.Data.RepositoriesTest
{
    [TestClass]
    public class UserRepositoryTests : BaseGenPresTest
    {
        [Isolated]
        [TestMethod]
        public void ThatUserRepositoryCanAuthenticateUserByUsernamePassword()
        {
            var repos = IsolateObjectMethod<IUserRepository>("GetUserByUsernamePassword", User.NewUser());
            Assert.IsNotNull(repos.AuthenticateUserByUsernamePassword("user", "password"));
        }
    }
}
