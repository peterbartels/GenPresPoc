using FluentNHibernate.Testing;
using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Data;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Data.NHibernateMappingsTest
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
