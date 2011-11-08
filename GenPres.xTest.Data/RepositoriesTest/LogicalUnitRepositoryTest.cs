using System;
using System.Collections.Generic;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Patients;
using GenPres.Business.Domain.Prescriptions;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using TypeMock.ArrangeActAssert;

namespace GenPres.xTest.Data.RepositoriesTest
{
    [TestClass]
    public class LogicalUnitRepositoryTests : BaseGenPresTest
    {
        [Isolated]
        [TestMethod]
        public void ThatPdmsRepositoryCanGetPatientByPid()
        {
            var repos = IsolateObjectMethod<ILogicalUnitRepository>("GetLogicalUnitsFromDatabase", new[] { LogicalUnit.NewLogicalUnit() });
            Assert.IsTrue(repos.GetLogicalUnits().Length > 0);
        }

        
    }
}
