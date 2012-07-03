using Informedica.GenPres.Data.Presenters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenPres.xTest.Data.VisibilityTest
{
    [TestClass]
    public class PrescriptionPresenterShould
    {
        [TestMethod]
        [Isolated]
        public void call_PrescriptionVisibilityManager_ApplyToDto_when_SetVisibility_is_called()
        {
            var presenter = new PrescriptionPresenter();
            var prescriptionVisibilityManager = Isolate.Fake.Instance<IPrescriptionVisibilityManager>();
            Isolate.WhenCalled(() => PrescriptionPresenter.GetVisibilityManager()).WillReturn(prescriptionVisibilityManager);
            presenter.SetVisibility();
            Isolate.Verify.WasCalledWithAnyArguments(()=>prescriptionVisibilityManager.ApplyToDto());

        }
    }
}
