
using System;
using FluentNHibernate.Testing;
using GenPres.Business.Domain.Prescriptions;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Data.PrescriptionTest
{
    [TestClass]
    public class PrescriptionMapTests : BaseGenPresTest
    {
        public PrescriptionMapTests() 
        {
            
        }

        [TestMethod]
        public void PrescriptionMapShouldCorrectlyMapPrescription()
        {
            /*new PersistenceSpecification<Prescription>(Context.CurrentSession())
               .CheckProperty(b => b.StartDate, DateTime.Now)
               .VerifyTheMappings();*/
        }
    }
}
