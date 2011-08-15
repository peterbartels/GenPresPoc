using System;
using System.Collections.Generic;
using GenPres.Business.Domain.Units;
using GenPres.Business.WebService;

namespace GenPres.Business.Domain.Prescriptions
{
    public class Drug
    {
        #region Private Fields
        
        private string _generic;

        private string _route;

        private string _shape;
        
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

        public virtual List<IComponent> Components { get; set; }

        #endregion

        protected Drug()
        {
            
        }

        public void CheckIncrements()
        {
            if (Generic != "" && Route != "" && Shape != "")
            {
                var genFormService = new GenFormService();
                var substanceIncrements = genFormService.GetSubstanceIncrements(Generic, Route, Shape);
                Components[0].Substances[0].SubstanceIncrements = substanceIncrements;
                var componentIncrements = genFormService.GetComponentIncrements(Generic, Route, Shape);
                if(componentIncrements.Length == 1)
                {
                    Components[0].ComponentIncrement = componentIncrements[0];
                }
            }
        }

        public static Drug NewDrug()
        {
            var drug = new Drug
            {
                Quantity = new UnitValue(), 
                Components = new List<IComponent> {new Component()}
            };
            return drug;
        }


        #region ISavable Implementation

        public bool IsNew { get { return (Id == 0); } }

        public int Id { get; set; }
        #endregion
    }
}
