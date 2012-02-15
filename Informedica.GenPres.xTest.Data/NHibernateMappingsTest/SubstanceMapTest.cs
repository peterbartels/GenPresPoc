using FluentNHibernate.Testing;
using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Data;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Data.NHibernateMappingsTest
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
