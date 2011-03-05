using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using GenPres.Operations;
using Csla;
using GenPres.Business;
using GenPres.Business.GenForm.Products;

//using Newtonsoft.Json.Linq;

namespace GenPres.Operations.Determination.Rules
{
    /*
     *  This class gets all the increments from GenForm
     */
    class RetrieveIncrements : IDetermination
    {
        public bool Determine(Prescription prescription)
        {
            //CacheManager CacheManager = (CacheManager)CacheFactory.GetCacheManager();
            Component pComponent = prescription.Drug.Components[0];
            Substance pSubstance = prescription.Drug.Components[0].Substances[0];

            if (prescription.Medicine != null)
            {
                //If ComponentIncrement are already defined, they are overridden by the GUI
                if (prescription.Medicine.ComponentIncrement.BaseValue > 0 && prescription.Medicine.DoseIncrement.BaseValue > 0)
                {
                    pComponent.ComponentIncrement = prescription.Medicine.ComponentIncrement.BaseValue;
                    pSubstance.SubstanceIncrements = new decimal[] { prescription.Medicine.DoseIncrement.BaseValue };
                    return false;
                }
            }

            DrugData drugData;

            string key = prescription.Drug.Name + "-" + prescription.Drug.Shape + "-" + prescription.Drug.Route;
            /*if (drugDataCache.Contains(key))
            {
                drugData = (GenForm.Products.DrugData)drugDataCache[key];
            }
            else
            {
             */
                //Call the webservice
                Products ProductsWS = new Products();
                ProductsWS.Url = Settings.SettingsManager.Instance.ReadSecureSetting("GenFormWebService");

                drugData = ProductsWS.GetGenPresDrugData(
                    prescription.Drug.Name,
                    "",
                    "",
                    "",
                    "",
                    "",
                    prescription.Drug.Shape,
                    prescription.Drug.Route
                );


                //drugDataCache.Add(key, drugData, CacheItemPriority.Normal, null, new SlidingTime(System.TimeSpan.FromMinutes(30)));
            //}
            
            
            /* Cannot calculate when multiple ComponentIncrement are present, the drug is not defined */
            if (drugData.ComponentKAEs.Length > 1)
            {
                //pComponent.ComponentIncrement = new decimal(0);
                //pSubstance.SubstanceIncrements = new decimal[0];    
                return false;
            }

            //Parse string array to Double array
            if (Utils.ConvertStringArrayToDoubleList(drugData.ComponentKAEs).ToArray().Length > 0)
            {
                pComponent.ComponentIncrement = Utils.ConvertStringArrayToDoubleList(drugData.ComponentKAEs).ToArray()[0];
            }
            pSubstance.SubstanceIncrements = Utils.ConvertStringArrayToDoubleList(drugData.ConcentrationKAEs).ToArray();

            return true;
        }
    }
}
