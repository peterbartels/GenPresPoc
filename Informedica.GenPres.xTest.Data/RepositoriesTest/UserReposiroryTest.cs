using System;
using System.Collections.Generic;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Patients;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Business.Domain.Users;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock.ArrangeActAssert;

namespace GenPres.xTest.Data.RepositoriesTest
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
