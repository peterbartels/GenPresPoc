using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GenPres.PrescriptionCalculator;
using Csla; 

namespace GenPres
{
    public class TPNSubstanceConcentration
    {
        Dictionary<string, Dictionary<string, decimal>> _componentsConfig;
        private Prescription _prescription;
        public TPNSubstanceConcentration()
        {
        }

        public void DetermineSubstances(Prescription p)
        {
            _prescription = p;
            string concentrations = System.IO.File.ReadAllText(@"C:\tpn_concentrations.csv");
            string[] concentrationLines = concentrations.Split('\n');
            string[] concentrationSubstances = concentrationLines[0].Split(';');

            _componentsConfig = new Dictionary<string, Dictionary<string, decimal>>();

            for (int l = 1; l < concentrationLines.Length; l++)
            {
                string[] substanceQuantities = concentrationLines[l].Split(';');
                _componentsConfig[substanceQuantities[0]] = new Dictionary<string, decimal>();
                for (int s = 1; s < substanceQuantities.Length; s++)
                {
                    decimal val = Decimal.Parse(substanceQuantities[s]);
                    _componentsConfig[substanceQuantities[0]][concentrationSubstances[s]] = val;
                }
            }

            Drug drug = p.Drug;

            for (int c = 0; c < p.Drug.Components.Count; c++)
            {
                Component component = p.Drug.Components[c];
                string name = component.Name;
                AddSubstanceByName(name, component);
            }
            if (!(_prescription.prepareSave && _prescription.Id>0)) p.UpdateDoses(true);
        }
        private void AddSubstanceByName(string name, Component component)
        {
            Dictionary<string, decimal> values;
            if (_componentsConfig.TryGetValue(name, out values))
            {
                foreach (KeyValuePair<string, decimal> key in values)
                {
                    if (key.Value > 0)
                    {
                        Substance substance;
                        if (!(_prescription.prepareSave && _prescription.Id > 0))
                        {
                            substance = component.NewSubstance();
                        }
                        else
                        {
                            substance = component.Substances.SingleOrDefault(s => s.SubstanceName == key.Key);
                            if (substance == null) return;
                        }
                        substance.ComponentConcentration.Unit = "mg";
                        substance.ComponentConcentration.Total = "ml";
                        substance.DrugConcentration.Unit = "mg";
                        substance.DrugConcentration.Total = "ml";

                        substance.Quantity.Unit = "mg";
                        substance.ComponentConcentration.BaseValue = key.Value;
                        substance.SubstanceName = key.Key;
                        substance.SubstanceIncrements = new decimal[] { key.Value * 0.0001m };
                        

                    }
                }
            }

        }
    }
}
