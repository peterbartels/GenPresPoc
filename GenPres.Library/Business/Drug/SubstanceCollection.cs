using System;
using System.Collections;
using System.Collections.Generic;
using Csla;

namespace GenPres.Business
{
    [Serializable]
    public class SubstanceCollection : BusinessListBase<SubstanceCollection, Substance>, IDataCollection
    {
        private Component component;
        internal void SetComponent(Component c)
        {
            this.component = c;
        }

        public Csla.Core.IBusinessObject[] GetList()
        {
            return this.ToArray();
        }


        public void AddItem(object item)
        {
            this.Add((Substance)item);
        }


        public object GetParent()
        {
            return this.Parent;
        }

        internal Component GetComponent()
        {
            return component;
        }

        protected override object AddNewCore()
        {
            Substance item = Substance.NewSubstance();
            Add(item);
            return item;
        }

        #region Factory Methods

        internal static SubstanceCollection NewSubstanceCollection()
        {
            return DataPortal.Create<SubstanceCollection>();
        }

        internal static SubstanceCollection GetSubstanceCollection(object childData)
        {
            return DataPortal.Fetch<SubstanceCollection>(childData);
        }

        private SubstanceCollection()
        { }
        #endregion

        #region Data Access
        private void Child_Fetch()
        {
        }
        private void DataPortal_Fetch()
        {
            RaiseListChangedEvents = false;
            //foreach (var child in (IList<object>)childData)
                //this.Add(Substance.GetSubstance(Substance));
                RaiseListChangedEvents = true;
        }
        #endregion
    }
}
