using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.Service;
using GenPres.Business.ServiceProvider;
using GenPres.DataAccess.Repositories;
using GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace GenPres.Business.Test.PatientTest
{
    [TestClass]
    public class PatientRepositoryTest : BaseGenPresTest
    {
        private IPdsmRepository _initializePatientRepositoryTest()
        {
            var repository = Isolate.Fake.Instance<PdmsRepository>(Members.CallOriginal);
            DalServiceProvider.Instance.RegisterInstanceOfType<IPdsmRepository>(repository);
            return repository;
        }

        [Isolated]
        [TestMethod]
        public void PatientService_GetPatientsByLogicalUnitId_calls_Repository()
        {
            _initializePatientRepositoryTest();
            var patientRepository = DalServiceProvider.Instance.Resolve<IPdsmRepository>();
            PatientService.GetPatientsByLogicalUnit(1);
            Isolate.Verify.WasCalledWithExactArguments(() => patientRepository.GetPatientsByLogicalUnitId(1));
        }
    }
}
