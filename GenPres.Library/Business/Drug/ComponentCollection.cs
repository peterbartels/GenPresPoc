using System;
using System.Collections;
using System.Collections.Generic;
using Csla;

namespace GenPres.Business
{
    [Serializable]
    public class ComponentCollection : BusinessListBase<ComponentCollection, Component>, IDataCollection
    {
        private Drug drug;
        
        internal void SetDrug(Drug p)
        {
            this.drug = p;
        }


        public void AddItem(object item)
        {
            this.Add((Component)item);
        }


        public Csla.Core.IBusinessObject[] GetList()
        {
            return this.ToArray();
        }

        internal Drug GetDrug()
        {
            return this.drug;
        }

        public object GetParent()
        {
            return this.Parent;
        }
        protected override object AddNewCore()
        {
            Component item = Component.NewComponent();
            Add(item);
            return item;
        }

        #region Factory Methods

        internal static ComponentCollection NewComponentCollection()
        {
            return DataPortal.CreateChild<ComponentCollection>();
        }

        internal static ComponentCollection GetComponentCollection(object childData)
        {
            return DataPortal.FetchChild<ComponentCollection>(childData);
        }

        private ComponentCollection()
        { }
        #endregion
        
        #region Data Access
        private void Child_Fetch()
        {
            RaiseListChangedEvents = false;
        }

        private void DataPortal_Fetch()
        {
            RaiseListChangedEvents = false;
            //foreach (var child in (IList<object>)childData)
                //this.Add(Component.GetComponent(Component));
            RaiseListChangedEvents = true;
        }
        #endregion
    }
}
