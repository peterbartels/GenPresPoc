using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace GenPres.Data
{
    public static class SessionFactoryCreator
    {
        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(GetConnectionString()))
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<Mappings.PrescriptionMap>()
                .ExportTo(@"C:\development\GenPres\MappingsXml"))
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration config)
        {
            // first drop the database to recreate a new one
            new SchemaExport(config).Drop(false, true);
            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(config).Create(false, true);
        }

        private static string GetConnectionString()
        {
            return "";// DatabaseConnection.GetLocalConnectionString(DatabaseConnection.DatabaseName.GenFormTest);
        }
    }
}