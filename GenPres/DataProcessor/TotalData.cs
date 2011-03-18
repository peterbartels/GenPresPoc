using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Caching;
using GenPres.PrescriptionCalculator;
using Csla; 

namespace GenPres.DataProcessor
{
    
    public class TotalData
    {
        public TotalData() { }
        /*
        public void InsertInfoCache(int id, string patientId){
            Cache c = new Cache();
            c.Insert(
        }*/

        internal Hashtable TransformCollection(PrescriptionCollection presCollection, Prescription prescription, string patientPID)
        {
            PrescriptionCollection newPresCollection = PrescriptionCollection.NewPrescriptionCollection();
            for (int i = 0; i < presCollection.Count; i++)
            {
                if (presCollection[i].Id != prescription.Id)
                {
                    newPresCollection.Add(presCollection[i]);
                }
            }
            newPresCollection.Add(prescription);
            return TransformCollection(newPresCollection, patientPID);
        }
        internal Hashtable TransformCollection(PrescriptionCollection presCollection, string patientPID)
        {
            Hashtable result2 = new Hashtable();
            Hashtable[] result = new Hashtable[1];
            decimal glucose = 0;
            decimal natrium = 0;
            decimal eiwit = 0;
            decimal vet = 0;
            decimal kalium = 0;
            decimal calcium = 0;
            decimal phosphaat = 0;
            decimal volume = 0;
            decimal energie = 0;

            Patient pat = Patient.GetPatientByPID(patientPID);
            decimal adjustVal = 1;
            if(pat != null) 
                if (pat.Weight > 0) adjustVal = UnitConverter.GetUnitValue("kg", pat.Weight);


            Hashtable totals = new Hashtable();
            for (int i = 0; i < presCollection.Count; i++)
            {
                Prescription p = presCollection[i];
                
                if (p.Total.Unit == "ml" || p.Total.Unit == "l")
                {
                    volume += p.Total.BaseValue / adjustVal;
                }

                for (int d = 0; d < p.PrescriptionDoses.Count; d++)
                {
                    Dose dose = p.PrescriptionDoses[d];
                    Substance s = dose.Substance;

                    if (s.SubstanceName.Trim().ToLower() == "glucose")
                    {
                        glucose += dose.Total.BaseValue / adjustVal;
                    }
                    if (s.SubstanceName.Trim().ToLower() == "natrium")
                    {
                        natrium += dose.Total.BaseValue / adjustVal;
                    }
                    if (s.SubstanceName.Trim().ToLower() == "eiwit")
                    {
                        eiwit += dose.Total.BaseValue / adjustVal;
                    }
                    if (s.SubstanceName.Trim().ToLower() == "vet")
                    {
                        vet += dose.Total.BaseValue / adjustVal;
                    }
                    if (s.SubstanceName.Trim().ToLower() == "kalium")
                    {
                        kalium += dose.Total.BaseValue / adjustVal;
                    }
                    if (s.SubstanceName.Trim().ToLower() == "calcium")
                    {
                        calcium += dose.Total.BaseValue / adjustVal;
                    }
                    if (s.SubstanceName.Trim().ToLower() == "phosphaat")
                    {
                        phosphaat += dose.Total.BaseValue / adjustVal;
                    }
                }
            }

            totals["volume"] = ParseTotalValue(volume, "ml");
            totals["glucose"] = ParseTotalValue(glucose, "mmol");
            totals["natrium"] = ParseTotalValue(natrium, "mmol");
            totals["eiwit"] = ParseTotalValue(eiwit, "mmol");
            totals["vet"] = ParseTotalValue(vet, "mmol");
            totals["kalium"] = ParseTotalValue(kalium, "mmol");
            totals["calcium"] = ParseTotalValue(calcium, "mmol");
            totals["phosphaat"] = ParseTotalValue(phosphaat, "mmol");
            
            energie = (vet * 9) + (eiwit * 5) + (glucose * 4);
            totals["energie"] = energie.ToString("G6");

            result[0] = totals;
            result2["data"] = result;
            return result2;
        }

        private string ParseTotalValue(decimal value, string unit)
        {
            if (value == 0) return "0 " + unit;
            decimal resultValue = Math.Round(UnitConverter.GetUnitValue(unit, value * UnitConverter.GetBaseValue("dag", 1)), 2);

            return resultValue.ToString("G6") + " " + unit;
        }
    }
}
