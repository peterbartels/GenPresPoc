using DB = GenPres.Database;
using Csla;

namespace GenPres.Business.Data.DataAccess
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
