using System;
using Informedica.GenPres.Business.Data.IRepositories;
using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenPres.xTest.Data.RepositoriesTest
{
    [TestClass]
    public class PrescriptionRepositoryTests : BaseGenPresTest
    {
        [Isolated]
        [TestMethod]
        public void ThatPrescriptionCanBeGetById()
        {
            var repos = IsolateObjectMethod<IPrescriptionRepository>("GetById", Prescription.NewPrescription());
            Assert.IsNotNull(repos.GetPrescriptionById(Guid.NewGuid()));
        }

        [Isolated]
        [TestMethod]
        public void ThatPrescriptionCanBeGetByPatientId()
        {
            var repos = IsolateObjectMethod<IPrescriptionRepository>("GetByPatientId", new[] { Prescription.NewPrescription() });
            Assert.IsNotNull(repos.GetPrescriptionsByPatientId("1234567"));
        }

        [Isolated]
        [TestMethod]
        public void ThatPrescriptionCanNotBeGetByEmptyId()
        {
            var repos = ObjectFactory.GetInstance<IPrescriptionRepository>();
            Assert.IsNull(repos.GetPrescriptionById(Guid.Empty));
        }
    }
}
