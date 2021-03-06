﻿using System.Collections.Generic;
using Informedica.GenPres.Business.Domain.Units;
using Informedica.GenPres.Business.WebService;

namespace Informedica.GenPres.Business.Domain.Prescriptions
{
    public class Drug
    {
        #region Private Fields
        
        private string _generic = "";

        private string _route = "";

        private string _shape = "";
        
        #endregion 

        #region Public Properties

        public virtual UnitValue Quantity { get; set; }


        public virtual string Generic
        {
            get { return _generic; }
            set { _generic = value;
                CheckIncrements();
            }
        }

        public virtual string Route
        {
            get { return _route; }
            set { _route = value;
                CheckIncrements();
            }
        }

        public virtual string Shape
        {
            get { return _shape; }
            set { _shape = value;
                CheckIncrements();
            }
        }

        public virtual IList<Component> Components { get; set; }


        public virtual Prescription Prescription{ get; set; }


        #endregion

        protected Drug()
        {
            
        }

        public virtual void CheckIncrements()
        {
            if (Generic != "" && Route != "" && Shape != "" && Components != null)
            {
                var genFormService = new GenFormWebServices();
                var substanceIncrements = genFormService.GetSubstanceIncrements(Generic, Route, Shape);
                
                if(Components.Count == 0) return;
                if(Components[0].Substances.Count == 0) return;

                Components[0].Substances[0].SubstanceIncrements = substanceIncrements;
                var componentIncrements = genFormService.GetComponentIncrements(Generic, Route, Shape);
                if(componentIncrements.Length == 1)
                {
                    Components[0].ComponentIncrement = componentIncrements[0];
                }
            }
        }

        public static Drug NewDrug(Prescription p)
        {
            var drug = new Drug
            {
                Quantity = UnitValue.NewUnitValue(), 
                Components = new List<Component> {Component.NewComponent()},
                Prescription = p
            };
            return drug;
        }
    }
}
