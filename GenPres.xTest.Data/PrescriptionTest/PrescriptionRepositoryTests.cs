using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Prescriptions;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock.ArrangeActAssert;

namespace GenPres.xTest.Data.PrescriptionTest
{
    [TestClass]
    public class PrescriptionRepositoryTests : BaseGenPresTest
    {
        [Isolated]
        [TestMethod]
        public void ThatPrescriptionCanBeGetById()
        {
            var repos = IsolateRepos("GetById", Prescription.NewPrescription());
            Assert.IsNotNull(repos.GetPrescriptionById(Guid.NewGuid()));
        }

        [Isolated]
        [TestMethod]
        public void ThatPrescriptionCanBeGetByPatientId()
        {
            var repos = IsolateRepos("GetByPatientId", new[] { Prescription.NewPrescription() });
            Assert.IsNotNull(repos.GetPrescriptionsByPatientId("1234567"));
        }

        private IPrescriptionRepository IsolateRepos(string methodName, object returnValue)
        {
            var repos = ObjectFactory.GetInstance<IPrescriptionRepository>();
            Isolate.NonPublic.WhenCalled(repos, methodName).WillReturn(returnValue);
            return repos;
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
