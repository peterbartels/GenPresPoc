using System;
using GenPres.Assembler;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Data;
using GenPres.Data.Connections;
using GenPres.Data.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Base
{
    [TestClass]
    public class BaseGenPresTest
    {
        public BaseGenPresTest()
        {
            GenPresApplication.Initialize();
            SessionManager.Instance.InitSessionFactory(DatabaseConnection.DatabaseName.GenPresTest, true);
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
