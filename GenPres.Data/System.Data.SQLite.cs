using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.SQLite
{
    public class SQLiteDatabaseScope : IDisposable
    {

        private const string CONNECTION_STRING = "Data Source=:memory:;Version=3;New=True;";

        
        private object sync = new object();
        private NHibernate.Cfg.Configuration config;
        private NHibernate.ISessionFactory sessionFactory;
        private string initialDataFilename;
        private SQLiteConnection connection;

        public SQLiteDatabaseScope(NHibernate.Cfg.Configuration Configuration,
            NHibernate.ISessionFactory SessionFactory)
        {
            config = Configuration;
            sessionFactory = SessionFactory;
        }

        public SQLiteDatabaseScope(NHibernate.Cfg.Configuration Configuration,
            NHibernate.ISessionFactory SessionFactory,
            string InitialDataFilename)
            : this(Configuration, SessionFactory)
        {
            initialDataFilename = InitialDataFilename;
        }

        public NHibernate.ISession OpenSession()
        {
            return sessionFactory.OpenSession(GetConnection());
        }

        public NHibernate.ISession OpenSession(NHibernate.IInterceptor Interceptor)
        {
            return sessionFactory.OpenSession(GetConnection(), Interceptor);
        }

        public NHibernate.IStatelessSession OpenStatelessSession()
        {
            return sessionFactory.OpenStatelessSession(GetConnection());
        }

        private SQLiteConnection GetConnection()
        {
            if (null == connection)
                BuildConnection();
            return connection;
        }

        private void BuildConnection()
        {
            connection = new SQLiteConnection(CONNECTION_STRING);
            connection.Open();
            BuildSchema();
            if (!string.IsNullOrEmpty(initialDataFilename))
                new SQLiteDataLoader(connection, initialDataFilename).ImportData();
        }

        private void BuildSchema()
        {
            NHibernate.Tool.hbm2ddl.SchemaExport se;
            se = new NHibernate.Tool.hbm2ddl.SchemaExport(config);
            se.Execute(false, true, false, connection, null);
        }

        private bool disposedValue = false;

        protected void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    if (null != connection)
                    {
                        connection.Dispose();
                    }
                }
            }
            this.disposedValue=true;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}


namespace System.Data.SQLite
{
    public class SQLiteDataLoader
    {

 
        private const string ATTACHED_DB = "zxcvbnmInitialData";

        private SQLiteConnection connection;
        private string initialDataFilename;

        public SQLiteDataLoader(SQLiteConnection Connection,
            string InitialDataFilename)
        {
            connection = Connection;
            initialDataFilename = InitialDataFilename;
        }

        public void ImportData()
        {
            DataTable dt = connection.GetSchema(SQLiteMetaDataCollectionNames.Tables);
            var tableNames = (from DataRow R in dt.Rows
                             select (string)R["TABLE_NAME"]).ToArray();
            AttachDatabase();
            foreach (string tableName in tableNames)
            {
                CopyTableData(tableName);
            }
            DetachDatabase();
        }

        private void AttachDatabase()
        {
            SQLiteCommand cmd = new SQLiteCommand(connection);
            cmd.CommandText = String.Format("ATTACH '{0}' AS {1}", initialDataFilename, ATTACHED_DB);
            cmd.ExecuteNonQuery();
        }

        private void DetachDatabase()
        {
            SQLiteCommand cmd = new SQLiteCommand(connection);
            cmd.CommandText = string.Format("DETACH {0}", ATTACHED_DB);
            cmd.ExecuteNonQuery();
        }

        private void CopyTableData(string TableName)
        {
            int rowsAffected;
            SQLiteCommand cmd = new SQLiteCommand(connection);
            cmd.CommandText = string.Format("INSERT INTO {0} SELECT * FROM {1}.{0}", TableName, ATTACHED_DB );
            rowsAffected = cmd.ExecuteNonQuery();
        }

    }
}