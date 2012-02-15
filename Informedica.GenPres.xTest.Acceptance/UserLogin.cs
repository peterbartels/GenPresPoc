using GenPres.Assembler;
using GenPres.Business.Service;
using GenPres.Data.Connections;
using GenPres.Service;
using GenPres.xTest.Base;
using NHibernate;

namespace GenPres.xTest.Acceptance
{
    public class UserLogin
    {
        public int Department;
        public string Username;
        public string Password;

        private ISessionFactory _sessionFactory = TestSessionManager.InitSessionFactory(DatabaseConnection.DatabaseName.GenPresTest, true);
            
        public UserLogin()
        {
            GenPresApplication.Initialize();
            Settings.SettingsManager.Instance.Initialize();
            TestSessionManager.InitSession();
        }

        public bool AutenticateUser()
        {
            return UserService.AuthenticateUser(Username, Password);
        }

        public int GetDepartmentCount()
        {
            return PatientService.GetLogicalUnits().Length;
        }

        public bool DepartmentHasPatients()
        {
            return (PatientService.GetPatientsByLogicalUnit(Department).Count > 0);
        }
    }
}
