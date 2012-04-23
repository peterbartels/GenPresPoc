using Informedica.GenPres.Assembler;
using Informedica.GenPres.Data;
using Informedica.GenPres.Data.Connections;
using Informedica.GenPres.Service;
using NHibernate;

namespace Informedica.GenPres.xTest.Acceptance
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
    }}
