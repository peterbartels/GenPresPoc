using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Ext.Direct.Mvc;
using GenPres.PrescriptionCalculator;
using DB=GenPres.Database;
using System.Linq;
using Csla;
using Newtonsoft.Json.Linq;

namespace GenPres.Controllers
{
    
    public class PrescriptionController : Controller
    {
        //
        // GET: /Prescription/
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Settings.SettingsManager.Instance.SetSettingsPath(HttpContext.ApplicationInstance.Server.MapPath("~/"));
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StopPrescription(int id, string status)
        {
            Prescription prescription = Prescription.GetPrescriptionById(id);
            prescription.EndDate = DateTime.Now.ToString("yyyy-MM-ddT00:00");
            prescription.EndTime = DateTime.Now.ToString("HH:mm");
            prescription.State = status;
            prescription.Save();
            return this.Direct(true);
        }
        /// <summary>
        /// Gets a collection of prescription for the prescriptionGrid
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public ActionResult GetPrescriptionGrid(string Pid)
        {
            PrescriptionCollection presCollection = PrescriptionCollection.GetPrescriptionsByPid(Pid);
            GenPres.DataProcessor.GridData gridData = new GenPres.DataProcessor.GridData();
            return this.Direct(gridData.TransformCollection(presCollection));
        }

        /// <summary>
        /// Calculate the totals (kalium, calcium, etc) for a prescription
        /// </summary>
        /// <param name="patientID"></param>
        /// <returns></returns>
        public ActionResult GetTotal(string patientID)
        {
            PrescriptionCollection presCollection = PrescriptionCollection.GetPrescriptionsByPid(patientID);
            GenPres.DataProcessor.TotalData totalData = new GenPres.DataProcessor.TotalData();
            return this.Direct(totalData.TransformCollection(presCollection, patientID));
        }

        public ActionResult UpdatePatientData(string patientID, UnitValue weight, UnitValue length)
        {
            Patient p = Patient.GetPatientByPID(patientID);
            if (p == null)
            {
                p = Patient.NewPatient();
                p.PID = patientID;
            }
            p.Weight = weight.BaseValue;
            p.Length = length.BaseValue;
            if (p.IsNew)
            {
                PrescriptionSaveProcess.SavePatient(p);
            }
            else
            {
                p.Save();
            }

            return this.Direct(true);
        }
    
        public ActionResult UpdatePrescription(Prescription p, Dose d, Drug dr, Component c, Substance s, string PID, bool save)
        {
            /* Ignore these properties in mapping */
            string[] ignoreList = new string[] { "Components", "PrescriptionDoses", "Substance", "Substances", "PrescriptionRateKAE", "ConcentrationKAE", "DoseRateKAE", "FrequencyKAE", "MappingProperties", "Id" };

            Prescription prescription;
            Drug drug;
            Dose dose;
            Component component;
            Substance substance;
            if (save && p.Id > 0)
            {
                prescription = Prescription.GetPrescriptionById(p.Id);
                drug = prescription.Drug;
                dose = prescription.PrescriptionDoses[0];
                component = drug.Components[0];
                substance = component.Substances[0];
            }
            else
            {
                /* Create an empty prescription */
                prescription = Prescription.NewPrescription();
                drug = Drug.NewDrug();
                dose = prescription.NewDose();
                component = drug.NewComponent();
                substance = component.NewSubstance();

                if (p.Id > 0) prescription.Id = p.Id;

                if (save) prescription.prepareSave = true;

                if (!p.UsesTemplate && !p.IsTemplate)
                {
                    if (dr.Name != "" && dr.Route != "" && dr.Shape != "")
                    {
                        Prescription template = Prescription.GetTemplate(dr.Name, dr.Route, dr.Shape);
                        if (template != null)
                        {
                            template.AdjustWeight.BaseValue = p.AdjustWeight.BaseValue;
                            template.AdjustLength.BaseValue = p.AdjustLength.BaseValue;
                            template.Id = 0;
                            template.IsTemplate = false;
                            template.UsesTemplate = true;
                            template.PreRectifcation_Determine();
                            template.Calculate();
                            template.Rectify();
                            template.PostRectification_Determine();
                            template.PostCalculation_Determine();

                            PrescriptionCollection presCollection2 = PrescriptionCollection.GetPrescriptionsByPid(PID);
                            presCollection2.Add(template);
                            GenPres.DataProcessor.TotalData totalData2 = new GenPres.DataProcessor.TotalData();
                            prescription.Totals = totalData2.TransformCollection(presCollection2, PID);
                            return this.Direct(template);
                        }
                    }
                }
            }

            /*Map Component*/
            Csla.Data.DataMapper.Map(c, component, ignoreList);

            /*Map Substance */
            Csla.Data.DataMapper.Map(s, substance, ignoreList);

            /*Map Drug */
            Csla.Data.DataMapper.Map(dr, drug, ignoreList);

            /*Map Prescription */
            Csla.Data.DataMapper.Map(p, prescription, ignoreList);

            /*Map Dose */
            Csla.Data.DataMapper.Map(d, dose, ignoreList);

            if (save && p.IsTemplate)
            {
                prescription.AdjustWeight.BaseValue = 0;
                prescription.AdjustWeight.Value = 0;
                prescription.AdjustLength.Value = 0;
            }

            prescription.Drug = drug;
            prescription.UpdateDoses(false);

            /* Calculation and Determinitaion MODEL */
            prescription.PreRectifcation_Determine();
            prescription.Rectify();
            prescription.PostRectification_Determine();
            prescription.PostCalculation_Determine();

            PrescriptionSaveProcess savePrescription = new PrescriptionSaveProcess(prescription);
            Patient patient = Patient.GetPatientByPID(PID);
            if (patient != null)
            {
                if (patient.Weight > 0 && prescription.AdjustWeight.State != "User")
                {
                    prescription.AdjustWeight.BaseValue = patient.Weight;
                    prescription.AdjustWeight.State = "Calculated";
                }
                if (patient.Length > 0 && prescription.AdjustWeight.State != "User")
                {
                    prescription.AdjustLength.BaseValue = patient.Length;
                    prescription.AdjustLength.State = "Calculated";
                }
            }
            else
            {
                patient = Patient.NewPatient();
                patient.PID = PID;
            }

            if (save)
            {
                savePrescription.Save(prescription.Id, patient);
            }
            
            PrescriptionCollection presCollection = PrescriptionCollection.GetPrescriptionsByPid(PID);
            GenPres.DataProcessor.TotalData totalData = new GenPres.DataProcessor.TotalData();
            prescription.Totals = totalData.TransformCollection(presCollection, prescription, PID);
            
            if (p.Id > 0)
            {
                prescription.Id = p.Id;
            }
            return this.Direct(prescription);
        }

        public ActionResult GetPrescriptionById(int id, string PID)
        {
            Prescription prescription = Prescription.GetPrescriptionById(id);
            
            prescription.UsesTemplate = true;
            prescription.PreRectifcation_Determine();
            prescription.Rectify();
            prescription.PostRectification_Determine();
            prescription.PostCalculation_Determine();
            return this.Direct(prescription);
        }

        public ActionResult ClearPrescription(string PID, decimal weight, decimal height)
        {
            //Create an empty prescription
            Prescription prescription = Prescription.NewPrescription();
            Drug drug = Drug.NewDrug();
            Dose dose = prescription.NewDose();

            Component component = drug.NewComponent();
            Substance substance = component.NewSubstance();

            prescription.Drug = drug;
            prescription.UpdateDoses(false);

            prescription.AdjustWeight.Unit = "kg";
            prescription.AdjustWeight.State = "Calculated";
            prescription.AdjustWeight.BaseValue = weight;

            prescription.AdjustLength.Unit = "cm";
            prescription.AdjustWeight.State = "Calculated";
            prescription.AdjustLength.BaseValue = height;

            prescription.AdjustBSA.Unit = "m2";

            Patient patient = Patient.GetPatientByPID(PID);
            if (patient != null)
            {
                if (patient.Weight > 0)
                {
                    prescription.AdjustWeight.Unit = "kg";
                    prescription.AdjustWeight.BaseValue = patient.Weight;
                    prescription.AdjustWeight.State = "Calculated";
                }
                if (patient.Length > 0)
                {
                    prescription.AdjustLength.Unit = "cm";
                    prescription.AdjustLength.BaseValue = patient.Length;
                    prescription.AdjustLength.State = "Calculated";
                }
            }

            prescription.ClearPrescription_Determine();

            return this.Direct(prescription);
        }
    }
}
//TEST CODE for manually creating a mapping:
/*Dictionary<string, object> test = new Dictionary<string, object>();
            foreach (JProperty k in context["Prescription"])
            {
                JValue val = (JValue)k.Value;
                test.Add(k.Name, val.Value);
            }
            Csla.Data.DataMapper.Map(test, p);
            */
