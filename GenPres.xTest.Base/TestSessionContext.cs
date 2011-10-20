using System;
using GenPres.Assembler;
using GenPres.Assembler.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using StructureMap;

namespace GenPres.xTest.Base
{
    [TestClass]
    public abstract class TestSessionContext
    {
        protected SessionContext Context;
        private readonly bool _commit;

        protected TestSessionContext(Boolean commit)
        {
            _commit = commit;
            Initialize();
        }

        private static void Initialize()
        {
            ObjectFactory.Configure(x => x.For<ISessionFactory>().HybridHttpOrThreadLocalScoped().Use(GenPresApplication.SessionFactory));
        }

        [TestInitialize]
        public void MyTestInitialize()
        {
            if (_commit) DatabaseCleaner.CleanDataBase();
            Context = new SessionContext();
            Context.CurrentSession().BeginTransaction();
        }

        [TestCleanup]
        public void MyTestCleanup()
        {
            try
            {
                if (_commit)
                {
                    Context.CurrentSession().Transaction.Commit();
                }
                else Context.CurrentSession().Transaction.Rollback();

            }
            catch (Exception)
            {
                Context.CurrentSession().Transaction.Rollback();
                throw;
            }
            finally
            {
                Context.Dispose();
                Context = null;
            }
        }
    }
}