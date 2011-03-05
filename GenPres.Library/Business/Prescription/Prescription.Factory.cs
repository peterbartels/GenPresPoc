using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using Csla.Data;
using Csla.Security;
using GenPres.Database;

namespace GenPres.Business
{
    public partial class Prescription : StateBusinessBase<Prescription>, IDataBusinessBase
    {
        #region Factory Methods

        public static Prescription NewPrescription()
        {
            return DataPortal.Create<Prescription>();
        }

        public static Prescription GetPrescription(object childData)
        {
            Prescription p = DataPortal.Fetch<Prescription>(childData);
            p.UpdateDoses(false);
            return p;
        }

        public static Prescription GetPrescriptionById(int id)
        {
            Prescription p = null;

            using (var ctx = Csla.Data.ContextManager<PrescriptionDataContext>.GetManager(
                GenPres.Database.DatabaseConnection.GetConnectionString(
                GenPres.Database.DatabaseConnection.DatabaseName.GENPRES), false))
            {
                Database.Prescription data = ctx.DataContext.Prescriptions.Single<Database.Prescription>(c => c.Id == id);
                p = DataPortal.Fetch<Prescription>(data);
            }
            p.UpdateDoses(false);

            return p;
        }

        public static Prescription GetTemplate(string name, string route, string shape)
        {
            Prescription p = null;

            using (var ctx = Csla.Data.ContextManager<Database.PrescriptionDataContext>.GetManager(
                DatabaseConnection.GetConnectionString(
                DatabaseConnection.DatabaseName.GENPRES), false))
            {
                var data = (from pres in ctx.DataContext.Prescriptions
                            join drug in ctx.DataContext.Drugs on pres.Drug equals drug
                            where drug.Name == name && drug.Route == route && drug.Shape == shape && pres.IsTemplate == true /*&& (pres.StartDate <= DateTime.Now && (pres.EndDate >= DateTime.Now || pres.EndDate == null))*/
                            select pres);
                
                if(data.Count<Database.Prescription>() > 0){
                    p = DataPortal.Fetch<Prescription>(data.First<Database.Prescription>());
                }
            }
            if(p!=null) 
                p.UpdateDoses(true);

            return p;
        }

        public Prescription()
        {/* Require use of factory methods */ }

        #endregion
    }
}
