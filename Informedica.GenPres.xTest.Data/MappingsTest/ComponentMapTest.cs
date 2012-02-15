using FluentNHibernate.Testing;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Data;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Data.MappingsTest
{
    [TestClass]
    public class ComponentMapTest : BaseGenPresTest
    {
        [TestMethod]
        public void ComponentMapShouldCorrectlyMapComponentBo()
        {
            new PersistenceSpecification<Component>(SessionManager.SessionFactory.GetCurrentSession())
                .CheckProperty(c=>c.Name, "test")
                .VerifyTheMappings();
        }
    }
}
