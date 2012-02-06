using FluentNHibernate.Testing;
using GenPres.Business.Domain.Patients;
using GenPres.Data;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Data.NHibernateMappingsTest
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
