using System;
using System.Collections;
using System.Collections.Generic;
using Csla;

namespace GenPres.Business
{
    [Serializable]
    public class DoseCollection : BusinessListBase<DoseCollection, Dose>, IDataCollection
    {
        private Prescription prescription;
        internal void SetPrescription(Prescription p)
        {
            this.prescription = p;
        }

        
        public Csla.Core.IBusinessObject[] GetList()
        {
            return this.ToArray();
        }

        public object GetParent()
        {
            return this.Parent;
        }

        internal Prescription GetPrescription()
        {
            return this.prescription;
        }


        public void AddItem(object item)
        {
            object newObj = AddNewCore();
            newObj = item;
        }

        protected override object AddNewCore()
        {
            Dose item = Dose.NewDose();
            Add(item);
            return item;
        }

        #region Factory Methods

        internal static DoseCollection NewDoseCollection()
        {
            return DataPortal.CreateChild<DoseCollection>();
        }

        internal static DoseCollection GetDoseCollection(object childData)
        {
            return DataPortal.FetchChild<DoseCollection>(childData);
        }

        private DoseCollection()
        { }
        #endregion

        #region Data Access
        private void Child_Fetch()
        {
            RaiseListChangedEvents = false;
        }
        #endregion


        [RunLocal]
        protected override void DataPortal_Create()
        {
            MarkAsChild();
            base.DataPortal_Create();
        }
    }
}
