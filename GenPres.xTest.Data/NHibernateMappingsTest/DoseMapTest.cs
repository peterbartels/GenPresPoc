using FluentNHibernate.Testing;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Data;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Data.NHibernateMappingsTest
{
    [TestClass]
    public class DoseMapTest : BaseGenPresTest
    {
        [TestMethod]
        public void DoseMapShouldCorrectlyMapDoseBo()
        {
            new PersistenceSpecification<Dose>(SessionManager.SessionFactory.GetCurrentSession())
               .VerifyTheMappings();
        }
    }
}
