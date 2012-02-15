using FluentNHibernate.Testing;
using GenPres.Business;
using GenPres.Business.Domain.Users;
using GenPres.Data;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Data.NHibernateMappingsTest
{
    [TestClass]
    public class UserMapTest : BaseGenPresTest
    {
        public UserMapTest() 
        {
            
        }

        [TestMethod]
        public void UserMapShouldCorrectlyMapUserBo()
        {
            new PersistenceSpecification<User>(SessionManager.SessionFactory.GetCurrentSession())
               .CheckProperty(b => b.UserName, "test")
               .CheckProperty(b => b.PassCrypt, AuthenticationFunctions.MD5("TEsT"))
               .VerifyTheMappings();
        }


    }
}
