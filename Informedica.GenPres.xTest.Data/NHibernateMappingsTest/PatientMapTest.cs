using FluentNHibernate.Testing;
using Informedica.GenPres.Business.Domain.Patients;
using Informedica.GenPres.Data;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Data.NHibernateMappingsTest
{
    [TestClass]
    public class PatientMapTest : BaseGenPresTest
    {
        [TestMethod]
        public void PatientMapShouldCorrectlyMapPatientBo()
        {
            new PersistenceSpecification<Patient>(SessionManager.SessionFactory.GetCurrentSession())
                .CheckProperty(c => c.Pid, "1234567")
                .VerifyTheMappings();
        }
    }
}
