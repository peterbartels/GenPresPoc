using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Business;
using System.Collections;
using System.Data;
using Csla;
using GenPres.Business.Data;
using DB=GenPres.Database;

namespace GenPres.Test
{
    [TestClass]
    public class TestDataAccess
    {
        #region General Settings
        public TestDataAccess()
        {
            Settings.SettingsManager.Instance.SetSettingsPath("c:\\development\\GenPres\\GenPres.Web\\");
            System.Configuration.ConfigurationManager.AppSettings["CslaAutoCloneOnUpdate"] = "false";
        }

        private TestContext testContextInstance;

        /*
         * Gets or sets the test context which provides
         * information about and functionality for the current test run.
         */
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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        #region FetchFromDatabase
        [TestMethod]
        public void TestFetch()
        {

            Prescription prescription = Prescription.NewPrescription();
            Drug drug = Drug.NewDrug();
            Dose dose = prescription.NewDose();

            Component component = drug.NewComponent();
            Substance substance = component.NewSubstance();

            prescription.Drug = drug;
            prescription.Medicine = Medicine.NewMedicine();
            prescription.UpdateDoses(false);

            prescription.AdjustWeight.Unit = "kg";
            prescription.AdjustWeight.UIState = "Calculated";
            prescription.AdjustWeight.BaseValue = 1;

            prescription.AdjustLength.Unit = "cm";
            prescription.AdjustWeight.UIState = "Calculated";
            prescription.AdjustLength.BaseValue = 1;

            prescription.AdjustBSA.Unit = "m2";

            Patient patient = Patient.GetPatientByPID("123456");
            if (patient != null)
            {
                if (patient.Weight > 0)
                {
                    prescription.AdjustWeight.Unit = "kg";
                    prescription.AdjustWeight.BaseValue = patient.Weight;
                    prescription.AdjustWeight.UIState = "Calculated";
                }
                if (patient.Length > 0)
                {
                    prescription.AdjustLength.Unit = "cm";
                    prescription.AdjustLength.BaseValue = patient.Length;
                    prescription.AdjustLength.UIState = "Calculated";
                }
            }

            prescription.ClearPrescription_Determine();
            
            PrescriptionCollection presCollection = PrescriptionCollection.GetPrescriptionsByPid("1");
            for (int i = 0; i < presCollection.Count; i++)
            {
                Prescription p = presCollection[i];
                p.PreRectifcation_Determine();
                p.Calculate();
            }
        }
        #endregion

        #region Update To Database
        [TestMethod]
        public void TestUpdatePatient()
        {
            Prescription prescription = Prescription.NewPrescription();
            Prescription p = new Prescription();
            p.Quantity.Unit = "zetp";
            p.Quantity.Value = 2;
            p.Total.Unit = "zetp";
            p.Frequency.Time = "dag";
            p.Frequency.Value = 2;
        }
        #endregion

        #region GUI DTO
        [TestMethod]
        public void TestUpdatePrescription()
        {
            PrescriptionFactory pf = new PrescriptionFactory("zetp", "mg");
            Prescription p = pf.CalculateParacetamol();

            string connectionString = DB.DatabaseConnection.GetConnectionString(DB.DatabaseConnection.DatabaseName.GENPRES);
            using (var DataAccess = new DataAccessManager(Csla.Data.ContextManager<DB.PrescriptionDataContext>.GetManager(connectionString, false).DataContext)){
                PrescriptionDAO.LoadMapping();
                Prescription pres = Prescription.GetPrescriptionById(1);
                pres.Frequency.Value = 1234;
                pres.Drug.Components[0].Substances[0].Quantity.Value = 9998175;
                Component c = pres.Drug.Components.AddNew();
                Substance s = c.Substances.AddNew();
                s.Quantity.Value = 19101982;
                s.Quantity.Unit = "mg";
                pres.Doses[0].Quantity.Value = 8388;
                pres.Save();
            }
        }

        [TestMethod]
        public void Test_FromPrescriptionDTO_ToPrescriptionBO()
        {
            PrescriptionFactory pf = new PrescriptionFactory("zetp", "mg");
            PrescribeMedicationDTO test = new PrescribeMedicationDTO();

            test.DrugName = "paracetamol";
            test.DrugShape = "zetp";
            test.DrugRoute = "rect";

            test.DoseTotal = new UnitValueDTO();
            test.DoseTotal.Value = 300;
            test.DoseTotal.Unit = "mg";
            
            test.Frequency = new UnitValueDTO();
            test.Frequency.Unit = "dag";
            test.Frequency.Value = 2;

            test.DrugQuantity = new UnitValueDTO();
            test.DrugQuantity.Value = 75;
            test.DrugQuantity.Unit = "mg";

            Prescription p = PrescribeMedicationDTO.MapTo(test, pf.GetPrescription());
            p.PreRectifcation_Determine();
            p.Calculate();
            Assert.AreEqual(Math.Round(p.Doses[0].Total.Value, 3), 600);
        }


        #endregion

        [TestMethod]
        public void Test_FromPrescriptionDAO_ToPrescriptionBO()
        {
            PrescriptionFactory pf = new PrescriptionFactory("zetp", "mg");
            DB.Prescription prescriptionDAO;

            string connectionString = DB.DatabaseConnection.GetConnectionString(DB.DatabaseConnection.DatabaseName.GENPRES);
            using (var DataAccess = new DataAccessManager(Csla.Data.ContextManager<DB.PrescriptionDataContext>.GetManager(connectionString, false).DataContext))
            {
                PrescriptionDAO.LoadMapping();
                prescriptionDAO = ((DB.PrescriptionDataContext)(DataAccess.DataContext)).Prescriptions.Single<Database.Prescription>(c => c.Id == 1);
                Prescription p = pf.CalculateParacetamol();
                PrescriptionDAO.MapTo(prescriptionDAO, p);
            }
        }
    }

}
