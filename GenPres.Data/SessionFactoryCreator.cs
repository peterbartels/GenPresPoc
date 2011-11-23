using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using GenPres.Data.Connections;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace GenPres.Data
{
    public static class SessionFactoryCreator
    {
        private static Configuration _configuration;
        private static ISessionFactory _sessionFactory;
      
        public static ISessionFactory CreateSessionFactory(DatabaseConnection.DatabaseName databaseName)
        {
            var fluentConfiguration = Fluently.Configure();
            
            if(databaseName == DatabaseConnection.DatabaseName.GenPresTest)
            {
                //fluentConfiguration.Database(SQLiteConfiguration.Standard.InMemory().ConnectionString("Data Source=:memory:; Version=3; New=True;").Raw("connection.release_mode", "on_close").ShowSql());
                fluentConfiguration.Database(MsSqlConfiguration.MsSql2008.ConnectionString(GetConnectionString(databaseName)));
            }else
            {
                fluentConfiguration.Database(MsSqlConfiguration.MsSql2008.ConnectionString(GetConnectionString(databaseName)));
            }
            
            fluentConfiguration.Mappings(x => x.FluentMappings.AddFromAssemblyOf<Mappings.UserMap>()
                .ExportTo(@"C:\development\GenPres\MappingsXml"))
                .CurrentSessionContext<NHibernate.Context.ThreadStaticSessionContext>()
                .ExposeConfiguration(cfg => _configuration = cfg)
                .Diagnostics(x => x.OutputToFile("c:\\temp\\test.-txt"));
                        
            _sessionFactory =  fluentConfiguration.BuildSessionFactory();
             
            return _sessionFactory;
        }

        public static void BuildSchema(ISession session)
        {
            var export = new SchemaExport(_configuration);
            export.Execute(false, true, false);
            //new SchemaExport(_configuration).Drop(false, true);
            //new SchemaExport(_configuration).Create(false, true);
        }

        private static string GetConnectionString(DatabaseConnection.DatabaseName databaseName)
        {
            return DatabaseConnection.GetLocalConnectionString(databaseName);
        }
    }
}