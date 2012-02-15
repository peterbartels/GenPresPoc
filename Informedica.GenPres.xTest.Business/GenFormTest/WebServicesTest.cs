using Informedica.GenPres.Business.WebService;
using Informedica.GenPres.Data.DTO.GenForm;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Business.GenFormTest
{
    /// <summary>
    /// Summary description for WebServicesTest
    /// </summary>
    [TestClass]
    public class WebServicesTest : BaseGenPresTest
    {
        private readonly GenFormWebServices _genFormService = new GenFormWebServices();

        [TestMethod]
        public void CanGetGenericsFromGenFormService()
        {
            var generics = _genFormService.GetGenerics("","");
            Assert.IsTrue(generics.Length > 0);
            
        }

        [TestMethod]
        public void CanGetRoutesFromGenFormService()
        {
            var routes = _genFormService.GetRoutes("", "");
            Assert.IsTrue(routes.Length > 0);
        }

        [TestMethod]
        public void CanGetShapesFromGenFormService()
        {
            var shapes = _genFormService.GetShapes("", "");
            Assert.IsTrue(shapes.Length > 0);
        }

        [TestMethod]
        public void CanAssembleValueListFromGenerics()
        {
            var list = ValueListAssembler.AssembleValueListDto(_genFormService.GetGenerics("", ""));
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void CanAssembleValueListFromShapes()
        {
            var list = ValueListAssembler.AssembleValueListDto(_genFormService.GetShapes("", ""));
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void CanAssembleValueListFromRoutes()
        {
            var list = ValueListAssembler.AssembleValueListDto(_genFormService.GetRoutes("", ""));
            Assert.IsTrue(list.Count > 0);
        }
    }
}
