using GenPres.Database;

namespace GenPres.Data.Managers
{
    public class GenPresDataManager : DataManager<PrescriptionDataContext>
    {
        public GenPresDataManager()
            : base(DatabaseConnection.GetConnectionString(DatabaseConnection.DatabaseName.GENPRES))
        {}

        public static GenPresDataManager GetManager()
        {
            GenPresDataManager mgr = new GenPresDataManager();
            return mgr;
        }
    }
}
