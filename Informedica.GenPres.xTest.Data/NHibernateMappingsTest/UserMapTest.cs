using FluentNHibernate.Testing;
using Informedica.GenPres.Business;
using Informedica.GenPres.Business.Domain.Users;
using Informedica.GenPres.Data;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Data.NHibernateMappingsTest
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
