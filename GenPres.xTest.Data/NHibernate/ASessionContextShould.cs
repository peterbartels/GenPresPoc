using System;
using GenPres.Assembler;
using GenPres.Assembler.Contexts;
using GenPres.Data;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Data.NHibernate
{
    [TestClass]
    public class ASessionContextShould : BaseGenPresTest
    {
        [TestMethod]
        public void AlwaysReturnASessionObject()
        {
            using (SessionContext.UseContext())
            {
                Assert.IsNotNull(SessionManager.Instance.SessionFactoryFromInstance.GetCurrentSession());
            }
        }

        [TestMethod]
        public void HaveClosedTheSessionAfterDispose()
        {
            using (SessionContext.UseContext())
            {
                Assert.IsNotNull(SessionManager.Instance.SessionFactoryFromInstance.GetCurrentSession());
            }

            try
            {
                SessionManager.Instance.SessionFactoryFromInstance.GetCurrentSession();
                Assert.Fail("No session should be open after disposal of SessionBuilder");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }
    }
}