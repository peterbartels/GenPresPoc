using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using GenPres.PrescriptionCalculator;
using Csla; 

namespace GenPres.DataProcessor
{
    public class GridData
    {
        public GridData() { }
        private string specifier="G6";
        internal Hashtable TransformCollection(PrescriptionCollection presCollection)
        {
            Hashtable result2 = new Hashtable();
            Hashtable[] result = new Hashtable[presCollection.Count];
            for (int i = 0; i < presCollection.Count; i++)
            {
                Prescription p = presCollection[i];
                p.Calculate();
                
                Hashtable pres = new Hashtable();

                pres["startDate"] = "";
                pres["endDate"] = "";
                
                if (p.StartDate != "") pres["startDate"] = p.StartDate.Split('T')[0] + " "  + p.StartTime;
                if (p.EndDate != "") pres["endDate"] = p.EndDate.Split('T')[0] + " " + p.EndTime;

                pres["drug"] = GetDrugInfo(p);
                pres["administration"] = GetAdministrationInfo(p);
                pres["dosage"] = GetDosageInfo(p);
                pres["id"] = p.Id;
                result[i] = pres;
            }
            result2["data"] = result;
            result2["total"] = result.Length;
            return result2;
        }

        private string GetDrugInfo(Prescription p)
        {
            string result = "";
            result += p.Drug.Name;
            result += " ";
            if (p.Drug.Components.Count > 0)
            {
                result += Math.Round(p.Drug.Quantity.Value, 2);
                result += " ";
                result += p.Drug.Quantity.Unit;
            }
            else
            {
                result += Math.Round(p.PrescriptionDoses[0].Substance.Quantity.Value,2).ToString(specifier);
                result += " ";
                result += p.PrescriptionDoses[0].Substance.Quantity.Unit;
            }
            result += " ";
            result += p.Drug.Route;

            if (p.Drug.Quantity != p.Drug.Components[0].Quantity)
            {
                //TODO: make information
            }
            else
            {
                //TODO Add drug package
            }

            return result;
        }

        private string GetAdministrationInfo(Prescription p)
        {
            string result = "";
            if (p.Continuous)
            {
                result += Math.Round(p.Rate.Value,2).ToString(specifier);
                result += " ";
                result += p.Rate.Unit;
                result += " per ";
                result += p.Rate.Time;
            }

            if (!p.Continuous)
            {
                result += Math.Round(p.Frequency.Value,2).ToString(specifier);
                result += " keer per ";
                result += p.Frequency.Time;
                result += " ";
                result += Math.Round(p.Quantity.Value,2).ToString(specifier);
                result += " ";
                result += p.Quantity.Unit;
            }

            if (p.Infusion)
            {
                result += " ";
                result += Math.Round(p.Time.Value,2).ToString(specifier);
                result += " per ";
                result += p.Time.Unit;
            }
            return result;
        }


        private string GetDosageInfo(Prescription p)
        {
            string result = "";
            Dose d = p.PrescriptionDoses[0];

            if (p.Continuous)
            {
                result += Math.Round(d.Rate.Value,2).ToString(specifier);
                result += " ";
                result += d.Rate.Unit;
                if (d.Rate.Adjust != "")
                {
                    result += " per ";
                    result += d.Rate.Adjust;
                }
                result += " per ";
                result += d.Rate.Time;
            }

            if (p.Infusion || (!p.Infusion && !p.Continuous))
            {
                result += Math.Round(d.Total.Value,2).ToString(specifier);
                result += " ";
                result += d.Total.Unit;
                if (d.Total.Adjust != "")
                {
                    result += " per ";
                    result += d.Total.Adjust;
                }
                result += " per ";
                result += d.Total.Time;
            }

            return result;
        }
    }
}
