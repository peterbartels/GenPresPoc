using System;
using GenPres.Assembler;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace GenPres.xTest.Base
{
    [TestClass]
    public class BaseGenPresTest
    {
        public BaseGenPresTest()
        {
            GenPresApplication.Initialize();
            Settings.SettingsManager.Instance.Initialize();
        }

        [TestInitialize]
        public void MyTestInitialize()
        {
            TestSessionManager.InitTestSessionFactory();
            TestSessionManager.Init();
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            TestSessionManager.Close();
        }

        protected T IsolateObject<T>()
        {
            var repos = Isolate.Fake.Instance<T>();
            ObjectFactory.Inject(repos);
            return repos;
        }

        protected T IsolateObjectMethod<T>(string methodName, object returnValue)
        {
            var obj = ObjectFactory.GetInstance<T>();
            Isolate.NonPublic.WhenCalled(obj, methodName).WillReturn(returnValue);
            return obj;
        }

        protected static void CheckVerifiyException(Exception e, string message)
        {
            if (!(e is VerifyException))
            {
                throw e;
            }
            Assert.Fail(message);
        }

        public string InsertPrescription(NHibernateRepository<Prescription, Guid> _repository  )
        {
            var prescriptionBo = new Prescription();
            prescriptionBo.StartDate = DateTime.Now;
            _repository.Add(prescriptionBo);
            return prescriptionBo.Id.ToString();
        }

        public Prescription CreateParacetamolRect(Prescription prescription)
        {
            prescription.Drug.Generic = "paracetamol";
            prescription.Drug.Route = "rect";
            prescription.Drug.Shape = "zetp";
            prescription.SetDefaultUnits("mg", "zetp");
            return prescription;
        }

        public Prescription CreateEpinephrineIv(Prescription prescription)
        {
            prescription.Drug.Generic = "epinephrine";
            prescription.Drug.Route = "iv";
            prescription.Drug.Shape = "infusievloeistof";
            prescription.SetDefaultUnits("mg", "ml");
            return prescription;
       }

        private Prescription CreateFzIV(Prescription prescription)
        {
            prescription.Drug.Generic = "fysiologish zout";
            prescription.Drug.Route = "iv";
            prescription.Drug.Shape = "infusievloeistof";
            prescription.SetDefaultUnits("ml", "ml");
            return prescription;
        }

        public Prescription CreateNoVolumes()
        {
            var prescription = CreateParacetamolRect(Prescription.NewPrescription());
            prescription.Frequency.Value = 2;
            prescription.Total.Value = 7;
            prescription.Drug.Quantity.Value = 2;
            prescription.Drug.Components[0].Substances[0].Quantity.Value = 0.4m;
            return prescription;
        }

        public Prescription CreateAdminVolume()
        {
            var prescription = CreateEpinephrineIv(Prescription.NewPrescription());
            prescription.Frequency.Value = 1;
            prescription.Quantity.Value = 1;
            prescription.Doses[0].Quantity.Value = 10;
            prescription.Doses[0].Quantity.Unit = "microg";
            prescription.Drug.Quantity.Value = 10;
            prescription.Drug.Components[0].Substances[0].DrugConcentration.Value = 0.1m;
            prescription.Drug.Components[0].Substances[0].Quantity.Value = 1;
            return prescription;
        }

        public Prescription CreateDoseVolumeAdminVolume()
        {
            var prescription = CreateFzIV(Prescription.NewPrescription());

            prescription.Quantity.Value = 200;
            prescription.Doses[0].Quantity.Value = 10;
            prescription.Drug.Quantity.Value = 10;
            prescription.Drug.Components[0].Substances[0].Quantity.Value = 1000;
            return prescription;
        }
    }
}
