using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Ext.Direct.Mvc;
using GenPres.PrescriptionCalculator;
using Csla;
using Newtonsoft.Json.Linq;

namespace GenPres.Controllers
{
    public class TPNController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Settings.SettingsManager.Instance.SetSettingsPath(HttpContext.ApplicationInstance.Server.MapPath("~/"));
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTPNPrescriptions(string PID)
        {
            List<Prescription> prescriptions = new List<Prescription>();
            PrescriptionCollection presCollection = PrescriptionCollection.GetTPNPrescriptionsByPid(PID);
            for (int i = 0; i < presCollection.Count; i++)
            {
                prescriptions.Add(presCollection[i]);
            }
            return this.Direct(prescriptions);
        }

        public ActionResult UpdateTPN(string PID, bool save, Prescription p, Drug dr, Component[] compts)
        {
            /* Ignore these properties in mapping */
            string[] ignoreList = new string[] { "Components", "PrescriptionDoses", "Substance", "Substances", "PrescriptionRateKAE", "ConcentrationKAE", "DoseRateKAE", "FrequencyKAE", "MappingProperties", "Id" };

            Prescription prescription;
            Drug drug;
            if (p.Id > 0 && save)
            {
                prescription = Prescription.GetPrescriptionById(p.Id);
                prescription.prepareSave = true;
                drug = prescription.Drug;
                Csla.Data.DataMapper.Map(p.Rate, prescription.Rate, ignoreList);
                Csla.Data.DataMapper.Map(p.Total, prescription.Total, ignoreList);

                for (int c = 0; c < compts.Length; c++)
                {
                    //Csla.Data.DataMapper.Map(compts[c], drug.Components[c], ignoreList);
                    drug.Components[c].ComponentIncrement = 0.0001m;
                    for (int s = 0; s < drug.Components[c].Substances.Count; s++)
                    {
                        drug.Components[c].Substances[s].SubstanceIncrements = new decimal[] { drug.Components[c].Substances[s].ComponentConcentration.BaseValue * 0.0001m };
                    }
                }

                prescription.Rectify();
            }
            else
            {

                prescription = Prescription.NewPrescription();
                drug = Drug.NewDrug();

                Csla.Data.DataMapper.Map(p, prescription, ignoreList);
                if (p.Id > 0) prescription.Id = p.Id;
                prescription.TPN = true;

                Csla.Data.DataMapper.Map(dr, drug, ignoreList);
                prescription.Drug = drug;
                prescription.Frequency.Value = 1;
                prescription.Frequency.Time = "dag";
                prescription.Frequency.State = "user";
                prescription.Time.Value = 24;
                prescription.Time.Unit = "uur";
                prescription.Time.State = "user";
                prescription.Total.Unit = "ml";
                prescription.Total.Time = "dag";
                prescription.Solution = true;
                prescription.Continuous = true;
                prescription.Quantity.Unit = "ml";
                /*if (p.Id == 0 || (p.Id > 0 && !save))
                {*/
                for (int i = 0; i < compts.Length; i++)
                {
                    Component component;

                    if (p.Id == 0 || (p.Id > 0 && !save))
                    {
                        component = drug.NewComponent();
                    }
                    else
                    {
                        component = prescription.Drug.Components[i];
                    }

                    Csla.Data.DataMapper.Map(compts[i], component, ignoreList);
                    component.ComponentIncrement = 0.0001m;
                    component.Concentration.Unit = "ml";
                    component.Concentration.Total = "ml";
                }
                TPNSubstanceConcentration tsc = new TPNSubstanceConcentration();
                tsc.DetermineSubstances(prescription);
                /*}
                else
                {
                    for (int c = 0; c < drug.Components.Count; c++)
                    {
                        drug.Components[c].ComponentIncrement = 0.0001m;
                        for (int s = 0; s < drug.Components[c].Substances.Count; s++)
                            drug.Components[c].Substances[s].SubstanceIncrements = new decimal[] { drug.Components[c].Substances[s].ComponentConcentration.BaseValue * 0.0001m };
                    }
                }*/


                if (compts.Length == 0)
                {
                    Component component = drug.NewComponent();
                    component.ComponentIncrement = 0.0001m;
                    component.Concentration.Unit = "ml";
                    component.Concentration.Total = "ml";
                    component.Quantity.Unit = "ml";
                    Substance substance = component.NewSubstance();
                    substance.ComponentConcentration.Unit = "mg";
                    substance.ComponentConcentration.Total = "ml";
                    substance.DrugConcentration.Unit = "mg";
                    substance.DrugConcentration.Total = "ml";
                    substance.Quantity.Unit = "mg";
                    substance.ComponentConcentration.BaseValue = 1;
                    substance.SubstanceName = "";
                    substance.SubstanceIncrements = new decimal[] { 1 * 0.0001m };
                    prescription.UpdateDoses(true);
                }
                if (drug.Components.Count > 0)
                {
                    /*if (p.Id == 0 || (p.Id > 0 && !save))
                    {*/
                    string[] selections = new string[prescription.Drug.Components.Count];
                    for (int i = 0; i < prescription.Drug.Components.Count; i++)
                        selections[i] = prescription.Drug.Components[i].Name;

                    TPNComponentCSVReader TPNCSVReader = new TPNComponentCSVReader("", false);
                    prescription.TPN_Determine(TPNCSVReader.GetSolutions(selections));
                    //}
                    prescription.Rectify();
                }
            }

            if (save)
            {
                PrescriptionSaveProcess savePrescription = new PrescriptionSaveProcess(prescription);
                Patient patient = Patient.GetPatientByPID(PID);
                if (patient == null)
                {
                    patient = Patient.NewPatient();
                    patient.PID = PID;
                }
                savePrescription.Save(prescription.Id, patient);
            }

            PrescriptionCollection presCollection = PrescriptionCollection.GetPrescriptionsByPid(PID);
            GenPres.DataProcessor.TotalData totalData = new GenPres.DataProcessor.TotalData();
            prescription.Totals = totalData.TransformCollection(presCollection, prescription, PID);
                
            return this.Direct(prescription);
        }

        public ActionResult GetComponentNames(Newtonsoft.Json.Linq.JObject paramvalues)
        {
            bool OnlySolutionComponents = paramvalues.Value<bool>("OnlySolutionComponents");
            string[] selections = paramvalues.Value<string>("selections").Split(';');
            
            TPNComponentCSVReader TPNCSVReader = new TPNComponentCSVReader("", OnlySolutionComponents);
            string[] components = TPNCSVReader.GetComponentNames(selections);

            Hashtable[] componentsResult = new Hashtable[components.Length];
            for (int i = 0; i < components.Length; i++)
            {
                Hashtable obj = new Hashtable();
                obj["Id"] = i;
                obj["Name"] = components[i];
                componentsResult[i] = obj;
            }
            return this.Direct(componentsResult);
        }

    }
}
