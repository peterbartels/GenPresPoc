using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Informedica.GenPres.Business.Data.IRepositories;
using Informedica.GenPres.Business.Exceptions;
using Informedica.GenPres.Business.WebService;
using Informedica.GenPres.Data.DTO;
using Informedica.GenPres.Data.DTO.Prescriptions;
using Informedica.GenPres.Service;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenPres.xTest.Service
{
    [TestClass]
    public class PrescriptionTest : BaseGenPresTest
    {
        #region TestContext
        private TestContext testContextInstance;


        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #endregion

        public PrescriptionTest()
        {
            
        }

        [Isolated]
        [TestMethod]
        public void GetPrescriptionByIdShouldCallPrescriptionRepository()
        {
            var repos = IsolateObject<IPrescriptionRepository>();
            Isolate.WhenCalled(() => repos.GetPrescriptionById(Guid.Empty)).ReturnRecursiveFake();
            var prescriptions = PrescriptionService.GetPrescriptionById(Guid.NewGuid().ToString());
            try
            {
                Isolate.Verify.WasCalledWithAnyArguments(() => repos.GetPrescriptionById(Guid.NewGuid()));
            }
            catch (Exception e)
            {
                CheckVerifiyException(e, "Prescription Repository method GetPrescriptionById was not called by Prescription Service method GetPrescriptionById");
            }
        }

        [Isolated]
        [TestMethod]
        public void GetPrescriptionsByPatientIdShouldCallPrescriptionRepository()
        {
            var repos = IsolateObject<IPrescriptionRepository>();
            Isolate.WhenCalled(() => repos.GetPrescriptionsByPatientId("1234567")).ReturnRecursiveFake();
            var prescriptions = PrescriptionService.GetPrescriptions("1234567");
            try
            {
                Isolate.Verify.WasCalledWithAnyArguments(() => repos.GetPrescriptionsByPatientId(""));
            }
            catch (Exception e)
            {
                CheckVerifiyException(e, "Prescription Repository method GetPrescriptionsByPatientId was not called by Prescription Service method GetPrescriptions");
            }
        }


        [Isolated]
        [TestMethod]
        public void ThatPrescriptionSaveShouldCallPrescriptionRepository()
        {
            var repos = IsolateObject<IPrescriptionRepository>();
            Isolate.WhenCalled(() => repos.SavePrescription(null, "")).DoInstead(delegate{});
            PrescriptionService.SavePrescription(new PrescriptionDto(), "1234567");
            try
            {
                Isolate.Verify.WasCalledWithAnyArguments(() => repos.SavePrescription(null, ""));
            }
            catch (Exception e)
            {
                CheckVerifiyException(e, "Prescription Repository method GetPrescriptionsByPatientId was not called by Prescription Service method GetPrescriptions");
            }
        }

        [Isolated]
        [TestMethod]
        public void ThatPrescriptionByIdShouldThrowInvalidIdExceptionWhenIdIsInvalid()
        {
            var repos = IsolateObject<IPrescriptionRepository>();
            Isolate.WhenCalled(() => repos.GetPrescriptionById(Guid.Empty)).ReturnRecursiveFake();
            try
            {
                PrescriptionService.GetPrescriptionById("0");
                PrescriptionService.GetPrescriptionById(Guid.Empty.ToString());
                Assert.Fail("GetPrescriptionById does not throw InvalidIdException.");
            }
            catch (InvalidIdException e)
            {
                
            }
        }

        [TestMethod]
        public void PrescriptionServiceGetSubstanceUnitsShouldCallGenFormWebServices()
        {
            var repos = IsolateObject<IGenFormWebServices>();
            Isolate.WhenCalled(() => repos.GetSubstanceUnits("", "", "")).WillReturn(new ReadOnlyCollection<SelectionItem>(new List<SelectionItem>()));
            var substanceUnits = PrescriptionService.GetSubstanceUnits("", "", "");
            try
            {
                Isolate.Verify.WasCalledWithAnyArguments(() => repos.GetSubstanceUnits("", "", ""));
            }
            catch (Exception e)
            {
                CheckVerifiyException(e, "GenFormWebServices GetSubstanceUnits Gen was not called by Prescription Service method GetSubstanceUnits");
            }
        }

        [TestMethod]
        public void PrescriptionServiceGetComponentUnitsShouldCallGenFormWebServices()
        {
            var repos = IsolateObject<IGenFormWebServices>();
            Isolate.WhenCalled(() => repos.GetComponentUnits("", "", "")).WillReturn(new ReadOnlyCollection<SelectionItem>(new List<SelectionItem>()));
            var componentUnits = PrescriptionService.GetComponentUnits("", "", "");
            try
            {
                Isolate.Verify.WasCalledWithAnyArguments(() => repos.GetComponentUnits("", "", ""));
            }
            catch (Exception e)
            {
                CheckVerifiyException(e, "GenFormWebServices GetComponentUnits Gen was not called by Prescription Service method GetComponentUnits");
            }
        }


        [Isolated]
        [TestMethod]
        public void ThatGetPrescriptionsShouldThrowInvalidIdExceptionWhenPatientIdIsInvalid()
        {
            var repos = IsolateObject<IPrescriptionRepository>();
            Isolate.WhenCalled(() => repos.GetPrescriptionsByPatientId("")).ReturnRecursiveFake();
            try
            {
                PrescriptionService.GetPrescriptions("");
                Assert.Fail("GetPrescriptionById does not throw InvalidIdException.");
            }
            catch (InvalidIdException e)
            {

            }
        }

        [Isolated]
        [TestMethod]
        public void ThatPrescriptionByIdShouldThrowsNoExistingIdWhenIdIsDoesNotExist()
        {
            try
            {
                PrescriptionService.GetPrescriptionById(Guid.NewGuid().ToString());
                Assert.Fail("GetPrescriptionById does not throw UnknownIdException.");
            }
            catch (UnknownIdException e)
            {

            }
        }

        [TestMethod]
        public void ThatPrescriptionsServiceCanGetPrescriptionById()
        {
            PatientService.SavePatient("1234567");
            var prescriptionDto = new PrescriptionDto();
            prescriptionDto.drugGeneric.value = "paracetamol";
            prescriptionDto.drugRoute.value = "rect";
            prescriptionDto.drugShape.value = "zetp";

            PrescriptionService.SavePrescription(prescriptionDto, "1234567");
            var prescriptions = PrescriptionService.GetPrescriptions("1234567");
            var lastPrescription = prescriptions[0];
            var prescription = PrescriptionService.GetPrescriptionById(lastPrescription.Id);
            Assert.IsTrue(prescription.PID != "" && prescription.Id != Guid.Empty.ToString());
        }

        [TestMethod]
        public void ThatPrescriptionsServiceCanSavePrescription()
        {   
            var pDto = new PrescriptionDto();
            pDto.drugGeneric.value = "paracetamol";
            pDto.drugRoute.value = "rect";
            pDto.drugShape.value = "zetp";
            pDto.startDate = "2011-07-01 12:00:00";
            pDto.substanceQuantity = new UnitValueDto();
            pDto.substanceQuantity.unit = "mg";
            pDto = PrescriptionService.SavePrescription(pDto, "8697898");
            Assert.IsTrue(pDto.Id != Guid.Empty.ToString());
        }
        
        [TestMethod]
        public void ThatPrescriptionsServiceCanGetPrescriptions()
        {
            var prescriptionDto = new PrescriptionDto();
            prescriptionDto.drugGeneric.value = "paracetamol";
            prescriptionDto.drugRoute.value = "rect";
            prescriptionDto.drugShape.value = "zetp";
            PrescriptionService.SavePrescription(prescriptionDto, "0004588");
            var prescriptions = PrescriptionService.GetPrescriptions("0004588");
            Assert.IsTrue(prescriptions.Count > 0);
        }
    }
}
