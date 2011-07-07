using GenPres.Business.Data.DataAccess.Repository;
using GenPres.Business.Service;
using GenPres.Business.ServiceProvider;
using GenPres.DataAccess.Repository;
using GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace GenPres.Business.Test.PatientTest
{
    [TestClass]
    public class PatientRepositoryTest : BaseGenPresTest
    {
        private IPatientRepository _initializePatientRepositoryTest()
        {
            var repository = Isolate.Fake.Instance<PatientRepository>(Members.CallOriginal);
            DalServiceProvider.Instance.RegisterInstanceOfType<IPatientRepository>(repository);
            return repository;
        }

        [Isolated]
        [TestMethod]
        public void PatientService_GetPatientsByLogicalUnitId_calls_Repository()
        {
            _initializePatientRepositoryTest();
            var patientRepository = DalServiceProvider.Instance.Resolve<IPatientRepository>();
            PatientService.GetPatientsByLogicalUnit(1);
            Isolate.Verify.WasCalledWithExactArguments(() => patientRepository.GetPatientsByLogicalUnitId(1));
        }
    }
}
