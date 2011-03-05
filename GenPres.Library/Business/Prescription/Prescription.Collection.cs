using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GenPres.Database;
using Csla;

namespace GenPres.Business
{
    [Serializable]
    public class PrescriptionCollection : BusinessListBase<PrescriptionCollection, Prescription>
    {

        protected override object AddNewCore()
        {
            Prescription item = Prescription.NewPrescription();
            Add(item);
            return item;
        }

        public void DeleteItem(int index)
        {
            base.RemoveItem(index);
        }

        #region Factory Methods

        public static PrescriptionCollection NewPrescriptionCollection()
        {
            return DataPortal.Create<PrescriptionCollection>();
        }

        public static PrescriptionCollection GetPrescriptionCollection()
        {
            return DataPortal.Fetch<PrescriptionCollection>("");
        }

        public static PrescriptionCollection GetPrescriptionsByPid(string pid)
        {
            Hashtable criteria = new Hashtable();
            criteria["pid"] = pid;
            criteria["TPN"] = false;
            return DataPortal.Fetch<PrescriptionCollection>(criteria);
        }

        public static PrescriptionCollection GetTPNPrescriptionsByPid(string pid)
        {
            Hashtable criteria = new Hashtable();
            criteria["pid"] = pid;
            criteria["TPN"] = true;

            return DataPortal.Fetch<PrescriptionCollection>(criteria);
        }
        

        private PrescriptionCollection()
        { }
        #endregion

        #region Data Access

        private void DataPortal_Fetch(Hashtable criteria)
        {
            using (var ctx = Csla.Data.ContextManager<PrescriptionDataContext>.GetManager(
                DatabaseConnection.GetConnectionString(
                DatabaseConnection.DatabaseName.GENPRES), false))
            {
                RaiseListChangedEvents = false;
                
                var data = (from pres in ctx.DataContext.Prescriptions
                            /*join pat in ctx.DataContext.Patients on pres.Patient equals pat
                            where pat.PID == ((string)criteria["pid"]) && where (pres.StartDate <= DateTime.Now && (pres.EndDate >= DateTime.Now || pres.EndDate == null))*/
                            select pres);
                if ((bool)criteria["TPN"]) data.Where(p => p.TPN == (bool)criteria["TPN"]);
                
                foreach (var pData in data) { Add(Prescription.GetPrescription(pData)); } 
                
                RaiseListChangedEvents = true;
            }
        }

        protected override void DataPortal_Update()
        {
            Child_Update();
        }
        
        [Transactional(TransactionalTypes.TransactionScope)]
        protected virtual void DataPortal_Delete(SingleCriteria<Prescription, int> criteria)
        {
            // TODO: delete values
        }

        #endregion
    }
}
