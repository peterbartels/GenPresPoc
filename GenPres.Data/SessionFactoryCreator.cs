using GenPres.Data.Mappings;
using Informedica.DataAccess.Configurations;
using NHibernate;

namespace GenPres.Data
{
    public static class SessionFactoryManagerNew
    {
        private const string Test = "Test";

        static SessionFactoryManagerNew()
        {
            ConfigurationManager.Instance.AddInMemorySqLiteEnvironment<SubstanceMap>(Test);
        }

        public static ISessionFactory GetSessionFactory()
        {
            return GetSessionFactory("Test");
        }

        public static void BuildSchema(string environment, ISession session)
        {
            GetEnvironmentConfiguration(environment).BuildSchema(session);
        }

        public static ISessionFactory GetSessionFactory(string environment)
        {
            return GetEnvironmentConfiguration(environment).GetSessionFactory();
        }

        private static IEnvironmentConfiguration GetEnvironmentConfiguration(string name)
        {
            return ConfigurationManager.Instance.GetConfiguration(name);
        }
    }
}
