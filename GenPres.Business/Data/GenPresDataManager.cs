using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DB = GenPres.Database;
using System.Linq;
using Enterprise.Data;

namespace GenPres.Business.Data
{
    public class GenPresDataManager : DataManager<DB.PrescriptionDataContext>
    {
        public GenPresDataManager()
            : base(DB.DatabaseConnection.GetConnectionString(DB.DatabaseConnection.DatabaseName.GENPRES))
        {}

        public static GenPresDataManager GetManager()
        {
            GenPresDataManager mgr = new GenPresDataManager();
            return mgr;
        }
    }
}
