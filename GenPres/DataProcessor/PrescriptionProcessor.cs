using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Ext.Direct.Mvc;
using GenPres.PrescriptionCalculator;
using Csla; 


namespace GenPres.DataProcessor
{
    public class PrescriptionProcessor
    {
        private Newtonsoft.Json.Linq.JObject prescriptionContext;
        public Prescription prescription;
        private int Id;
        private GenForm.Products.Products ProductsWS = new GenForm.Products.Products();
        private string[] ConcentrationKAEs;
        private string[] ComponentKAEs;
        private GenForm.Products.DrugData drugData;
        private string ShapeUnit;
        private string GenericUnit;


        public PrescriptionProcessor(Newtonsoft.Json.Linq.JObject pc) 
        {
            this.prescriptionContext = pc;
        }

        public Prescription InitPrescription(int id, bool save)
        {
            Drug drug = Drug.NewDrug();
            //string generic = prescriptionContext.Value<string>("generic");
            //GenForm.Products.Products ProductsWS = new GenForm.Products.Products();
            //GenForm.Products.ProductInfo[] products = ProductsWS.GetProductsInfo(generic);

            Id = id;

            if (Id > 0)
            {
                prescription = Prescription.GetPrescriptionById(Id);
            }
            else
            {
                prescription = Prescription.NewPrescription();
            }
            
            Component component;
            Substance substance;
            if (!(id > 0))
            {
                component = drug.NewComponent();
                /* Not working at the moment, missings substances
                 * if (products.Length == 1)
                {
                    for (int i = 0; i < products[0].Substances.Length; i++)
                    {
                        Substance s = component.NewSubstance(); ;
                        s.DrugConcentration = products[0].Substances[i].Concentration;
                        s.DrugConcentrationUnit = products[0].Substances[i].Unit.Trim();
                        s.Unit = products[0].Substances[i].Unit.Trim();
                        s.Name = products[0].Substances[i].Name.Trim();
                        s.SetBaseValue("Quantity", products[0].Substances[i].Quantity);
                    }
                    substance = component.Substances[0];
                }
                else
                {*/
                    substance = component.NewSubstance();
                //}

                prescription.Drug = drug;
                prescription.UpdateDoses(true);
            }
            else
            {
                drug = prescription.Drug;
                component = drug.Components[0];
                substance = component.Substances[0];
            }

            double baseWeight = 0;
            if (prescriptionContext.Value<string>("weight") != "") baseWeight = prescriptionContext.Value<double>("weight") / 1000;

            if (prescriptionContext.Value<string>("weightUnit") != "") prescription.AdjustValueWeightUnit = prescriptionContext.Value<string>("weightUnit");

            double baseLength = 0;
            if (prescriptionContext.Value<string>("length") != "") baseLength = prescriptionContext.Value<double>("length") / 100;

            double bsa = 0;
            if (baseLength > 0 && baseWeight > 0)
                bsa = 0.20247 * Math.Pow(baseLength, 0.725) * Math.Pow(baseWeight, 0.425);
            
            prescription.SetBaseValue("AdjustValueWeight", baseWeight);
            prescription.AdjustValueBSA = bsa;
            if (prescriptionContext.Value<string>("length") != "") prescription.AdjustValueLength = prescriptionContext.Value<double>("length");

            prescription.Continuous = prescriptionContext.Value<bool>("continuous");
            prescription.Infusion = prescriptionContext.Value<bool>("infusion");
            prescription.Solution = prescriptionContext.Value<bool>("issolution");

            //Set prescription units
            prescription.FrequencyTimeUnit = prescriptionContext.Value<string>("frequencyUnit");

            //Set administration units
            prescription.QuantityPackageUnit = prescriptionContext.Value<string>("administrationQuantityUnit");
            prescription.TotalPackageUnit = prescriptionContext.Value<string>("administrationQuantityTotalUnit");
            prescription.TotalTimeUnit = prescriptionContext.Value<string>("administrationQuantityTotalTime");
            prescription.RateTimeUnit = prescriptionContext.Value<string>("administrationQuantityRateTime");
            prescription.RatePackageUnit = prescriptionContext.Value<string>("administrationQuantityRateUnit");

            //set dosage units
            Dose firstDose = prescription.PrescriptionDoses[0];
            firstDose.QuantitySubstanceUnit = prescriptionContext.Value<string>("dosageQuantityUnit");
            firstDose.TotalSubstanceUnit = prescriptionContext.Value<string>("dosageQuantityTotalUnit");
            firstDose.TotalTimeUnit = prescriptionContext.Value<string>("dosageQuantityTotalTime");
            firstDose.TotalSubstanceUnit = prescriptionContext.Value<string>("dosageQuantityTotalUnit");
            firstDose.RateSubstanceUnit = prescriptionContext.Value<string>("dosageQuantityRateUnit");
            firstDose.RateTimeUnit = prescriptionContext.Value<string>("dosageQuantityRateTime");

            if (prescription.AdjustValueWeight > 0)
            {
                firstDose.QuantityAdjustUnit = prescriptionContext.Value<string>("dosageQuantityAdjust");
                firstDose.TotalAdjustUnit = prescriptionContext.Value<string>("dosageQuantityTotalAdjust");
                firstDose.RateAdjustUnit = prescriptionContext.Value<string>("dosageQuantityRateAdjust");
            }

            for (int i = 1; i < prescription.PrescriptionDoses.Count; i++)
            {
                prescription.PrescriptionDoses[i].QuantitySubstanceUnit = prescriptionContext.Value<string>("dosageQuantityUnit");
                prescription.PrescriptionDoses[i].TotalSubstanceUnit = prescriptionContext.Value<string>("dosageQuantityTotalUnit");
                prescription.PrescriptionDoses[i].TotalTimeUnit = prescriptionContext.Value<string>("dosageQuantityTotalTime");
                prescription.PrescriptionDoses[i].TotalSubstanceUnit = prescriptionContext.Value<string>("dosageQuantityTotalUnit");
                prescription.PrescriptionDoses[i].RateSubstanceUnit = prescriptionContext.Value<string>("dosageQuantityRateUnit");
                prescription.PrescriptionDoses[i].RateTimeUnit = prescriptionContext.Value<string>("dosageQuantityRateTime");
                if (prescription.AdjustValueWeight > 0)
                {
                    prescription.PrescriptionDoses[i].QuantityAdjustUnit = prescriptionContext.Value<string>("dosageQuantityAdjust");
                    prescription.PrescriptionDoses[i].TotalAdjustUnit = prescriptionContext.Value<string>("dosageQuantityTotalAdjust");
                    prescription.PrescriptionDoses[i].RateAdjustUnit = prescriptionContext.Value<string>("dosageQuantityRateAdjust");
                }
            }

            //Set Substance concentration units
            substance.DrugConcentrationUnit = prescriptionContext.Value<string>("solutionConcentrationUnit");
            substance.DrugConcentrationUnitTotal = prescriptionContext.Value<string>("solConcentrationTotalSelect");

            prescription.TimeUnit = prescriptionContext.Value<string>("durationUnit");
            drug.PackageUnit = prescriptionContext.Value<string>("solutionQuantityUnit");
            substance.Unit = prescriptionContext.Value<string>("quantityUnit");
            substance.Name = prescriptionContext.Value<string>("generic");

            drug.Name = prescriptionContext.Value<string>("generic");
            drug.Shape = prescriptionContext.Value<string>("shape");
            drug.Route = prescriptionContext.Value<string>("route");

            prescription.SetBaseValue("Frequency", prescriptionContext.Value<double>("frequency"));
            prescription.SetBaseValue("Time", prescriptionContext.Value<double>("duration"));
            
            if(prescriptionContext.Value<double>("quantity") > 0) substance.SetBaseValue("Quantity", prescriptionContext.Value<double>("quantity"));

            drug.SetBaseValue("Quantity", prescriptionContext.Value<double>("solutionQuantity"));
            substance.SetBaseValue("DrugConcentration", prescriptionContext.Value<double>("solutionConcentration"));

            prescription.SetBaseValue("Quantity", prescriptionContext.Value<double>("administrationQuantity"));
            prescription.SetBaseValue("Total", prescriptionContext.Value<double>("administrationQuantityTotal"));
            prescription.SetBaseValue("Rate", prescriptionContext.Value<double>("administrationQuantityRate"));

            firstDose.SetBaseValue("Quantity", prescriptionContext.Value<double>("dosageQuantity"));
            firstDose.SetBaseValue("Total", prescriptionContext.Value<double>("dosageQuantityTotal"));
            firstDose.SetBaseValue("Rate", prescriptionContext.Value<double>("dosageQuantityRate"));

            if (prescription.Continuous)
            {
                prescription.RateTimeUnit = "uur";
                prescription.TimeUnit = "uur";
                prescription.SetBaseValue("Time", 1);
                prescription.FrequencyTimeUnit = "dag";
                prescription.SetBaseValue("Frequency", 24);
            }

            if (!(id > 0))
            {
                string solutionType = prescriptionContext.Value<string>("solutionType");
                /*if (solutionType != "" && ConcentrationKAEs.Length == 1)
                {
                    Component subComponent = drug.NewComponent();
                    Substance s = subComponent.NewSubstance();
                    double calc = 1;
                    switch (solutionType)
                    {
                        case "FZ":
                            s.Unit = "mmol";
                            s.ComponentConcentration = 0.1;
                            s.ComponentConcentrationUnit = "mmol";
                            s.ComponentConcentrationUnitTotal = "ml";
                            break;
                        case "Glucose 5%":
                            s.Unit = "%";
                            s.ComponentConcentration = 5;
                            s.ComponentConcentrationUnit = "%";
                            s.ComponentConcentrationUnitTotal = "ml";
                            break;
                        case "Glucose 10%":
                            s.Quantity = 1;
                            s.Unit = "%";
                            s.ComponentConcentration = 10;
                            s.ComponentConcentrationUnit = "%";
                            s.ComponentConcentrationUnitTotal = "ml";
                            break;
                    }
                    if (drug.Quantity > 0)
                    {
                        double concentrationKAE = double.Parse(ConcentrationKAEs[0]);
                        double result = drug.Quantity - (substance.Quantity / concentrationKAE);
                        //subComponent.Concentration = result;
                        subComponent.Quantity = result;
                    }
                }*/
            }
            
            
            return prescription;
        }

        internal void Save(Newtonsoft.Json.Linq.JObject patientContext)
        {
            PrescriptionSaveProcess savePrescription = new PrescriptionSaveProcess(prescription, patientContext);
            savePrescription.Save(Id);
        }
        internal void ApplyCalculations()
        {
            Calculator calc = new Calculator(ref prescription);
        }
        internal void ApplyRectification(string latestProperyChanged)
        {
            drugData = ProductsWS.GetGenPresDrugData(prescription.Drug.Name, "", "", "", "", "", prescription.Drug.Shape, prescription.Drug.Route);
            ConcentrationKAEs = drugData.ConcentrationKAEs;
            ComponentKAEs = drugData.ComponentKAEs;

            //PrescriptionRectification pRectification = new PrescriptionRectification(prescription, ComponentKAEs, ConcentrationKAEs, latestProperyChanged);
            //pRectification.Rectify();
        }
        internal void ApplyRules(string latestProperyChanged)
        {   
            //FixUnits fixUnits = new FixUnits(ref prescription);


            if (drugData.SubstanceUnits.Length == 0)
            {
                throw new Exception("No generic units found for " + prescription.Drug.Name + ", " + prescription.Drug.Shape + ", " + prescription.Drug.Route);
            }
            if (drugData.ComponentUnits.Length == 0)
            {
                throw new Exception("No shape units found for " + prescription.Drug.Name + ", " + prescription.Drug.Shape + ", " + prescription.Drug.Route);
            }
            GenericUnit = drugData.SubstanceUnits[0];
            ShapeUnit = drugData.ComponentUnits[0];
            //ValueCorrection fixVals = new ValueCorrection(ref prescription, latestProperyChanged, ConcentrationKAEs, ComponentKAEs, ShapeUnit, GenericUnit);
            //DecisionSupport decision = new DecisionSupport(ref prescription);
        }

        internal void Validate()
        {
            //ValidatePrescription vp = new ValidatePrescription(prescription);
        }

        internal Hashtable GetHashData()
        {
            Hashtable result = new Hashtable();

            Dose firstDose = prescription.PrescriptionDoses[0];
            Substance substance = firstDose.Substance;

            result["frequency"] = prescription.GetUnitValue("Frequency");
            result["quantity"] = substance.GetUnitValue("Quantity");
            result["duration"] = prescription.GetUnitValue("Time");

            result["solutionConcentration"] = substance.GetUnitValue("DrugConcentration");
            result["solutionQuantity"] = prescription.Drug.GetUnitValue("Quantity");
            
            result["dosageQuantity"] = firstDose.GetUnitValue("Quantity");
            result["dosageQuantityTotal"] = firstDose.GetUnitValue("Total");
            result["dosageQuantityRate"] = firstDose.GetUnitValue("Rate");

            result["administrationQuantity"] = prescription.GetUnitValue("Quantity");
            result["administrationQuantityTotal"] = prescription.GetUnitValue("Total");
            result["administrationQuantityRate"] = prescription.GetUnitValue("Rate");

            result["bsa"] = prescription.AdjustValueBSA;
            
            //result["remarks"] = "test";
            /*
            result["frequencyUnit"] = prescription.FrequencyTimeUnit;
            result["quantityUnit"] = substance.Unit;
            result["durationUnit"] = prescription.TimeUnit;
            result["solutionConcentrationUnit"] = substance.DrugConcentrationUnit;
            result["solConcentrationTotalSelect"] = substance.DrugConcentrationUnitTotal;
            result["solutionQuantityUnit"] = prescription.Drug.PackageUnit;
            result["dosageQuantityAdjust"] = firstDose.QuantityAdjustUnit;
            result["dosageQuantityTotalUnit"] = firstDose.TotalSubstanceUnit;
            result["dosageQuantityTotalTime"] = firstDose.TotalTimeUnit;
            result["dosageQuantityRateUnit"] = firstDose.RateSubstanceUnit;
            result["dosageQuantityRateTime"] = firstDose.RateTimeUnit;
            result["administrationQuantityUnit"] = prescription.QuantityPackageUnit;
            result["administrationQuantityTotalUnit"] = prescription.TotalPackageUnit;
            result["administrationQuantityTotalTime"] = prescription.TotalTimeUnit;
            result["administrationQuantityRateUnit"] = prescription.RatePackageUnit;

            result["dosageQuantityUnit"] = firstDose.QuantitySubstanceUnit;
             */

            if (prescription.Id > 0)
            {
                result["id"] = prescription.Id;
            }
            return result;
        }
    }
}
