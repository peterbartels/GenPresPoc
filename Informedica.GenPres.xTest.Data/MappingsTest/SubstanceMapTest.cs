using FluentNHibernate.Testing;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Data;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Data.MappingsTest
{
    [TestClass]
    public class SubstanceMapTest : BaseGenPresTest
    {
        [TestMethod]
        public void SubstanceMapShouldCorrectlyMapSubstanceBo()
        {
            new PersistenceSpecification<Substance>(SessionManager.SessionFactory.GetCurrentSession())
                .CheckProperty(c => c.Name, "test")
               .VerifyTheMappings();
        }
    }
}
