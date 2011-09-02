using System;
using System.Collections.Generic;
using System.Linq;
using GenPres.Business.Domain.Patients;
using GenPres.Data.Repositories;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Data.PrescriptionTest
{
    [TestClass]
    public class PrescriptionDataTest : BaseGenPresTest
    {
        private const string PatientId = "1234567";
        private List<Guid> _prescriptionsId = new List<Guid>();

        [TestMethod]
        public void CanExecutePrescriptionDataMethods()
        {
            CanSaveAPrescription();
            CanSaveAPrescription();
            CanGetPrescriptionsByPatientId();
            CanGetPrescriptionById();
        }

        private void CanGetPrescriptionById()
        {
            
        }

        private void CanGetPrescriptionsByPatientId()
        {
            var pr = new PrescriptionRepository();
            var prescriptions = pr.GetPrescriptionsByPatientId(PatientId);
            Assert.IsTrue(prescriptions.Length == 2);
            Assert.IsTrue(prescriptions[0].Id == _prescriptionsId[0]);
            Assert.IsTrue(prescriptions[1].Id == _prescriptionsId[1]);
        }

        private void CanSaveAPrescription()
        {
            var pr = new PatientRepository();

            var prescription = CreatePrescriptionWithAllPropertiesSet();

            var pat = Patient.NewPatient();
            pat.Pid = PatientId;
            
            pat.Prescriptions.Add(prescription);
            pr.SaveOrUpdate(pat);

            Assert.IsTrue(pat.Id != Guid.Empty, "PatientId is not set.");
            Assert.IsTrue(prescription.Id != Guid.Empty, "Prescription id is not set");
            
            _prescriptionsId.Add(prescription.Id);

            Assert.IsTrue(prescription.Doses[0].Id != Guid.Empty, "prescription dose id is not set.");
            Assert.IsTrue(prescription.Drug.Components[0].Id != Guid.Empty, "component id is not set.");
            Assert.IsTrue(prescription.Drug.Components[0].Substances[0].Id != Guid.Empty, "substance id is not set.");
        }

    }
}
