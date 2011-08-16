using System;
using GenPres.Assembler;
using GenPres.Business.Domain;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Data;
using GenPres.Data.Connections;
using GenPres.Data.Repositories;
using GenPres.xTest.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Base
{
    [TestClass]
    public class BaseGenPresTest
    {
        public BaseGenPresTest()
        {
            GenPresApplication.Initialize();
            SessionManager.Instance.InitSessionFactory(DatabaseConnection.DatabaseName.GenPresTest);
            Settings.SettingsManager.Instance.Initialize();
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            SessionManager.Instance.CloseSession();
        }

        public string InsertPrescription(NHibernateRepository<Prescription, Guid> _repository  )
        {
            var prescriptionBo = new Prescription();
            prescriptionBo.StartDate = DateTime.Now;
            _repository.Add(prescriptionBo);
            return prescriptionBo.Id.ToString();
        }

        public Prescription CreatePrescriptionWithAllPropertiesSet()
        {
            var prescription = Prescription.NewPrescription();
            prescription.Drug.Generic = "paracetamol";
            prescription.Drug.Route = "rect";
            prescription.Drug.Shape = "zetp";

            prescription.Frequency.Value = 2;
            prescription.Frequency.Time = "dag";

            prescription.Quantity.Unit = "zetp";

            prescription.Total.Unit = "zetp";
            prescription.Total.Time = "dag";
            prescription.Total.Value = 7;

            prescription.Drug.Quantity.Unit = "zetp";
            prescription.Drug.Quantity.Value = 2;

            prescription.Drug.Components[0].Substances[0].Quantity.Value = 0.4m;
            prescription.Drug.Components[0].Substances[0].Quantity.Unit = "mg";
            return prescription;
        }
    }
}
