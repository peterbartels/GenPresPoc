using GenPres.Business.Data.Client.GenForm;
using GenPres.Business.WebService;
using GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.Business.Test.GenFormTest
{
    /// <summary>
    /// Summary description for WebServicesTest
    /// </summary>
    [TestClass]
    public class WebServicesTest : BaseGenPresTest
    {
        private readonly GenFormService genFormService = new GenFormService();

        [TestMethod]
        public void _can_GetGenerics_from_GenFormService()
        {
            var generics = genFormService.GetGenerics("","");
            Assert.IsTrue(generics.Length > 0);
        }

        [TestMethod]
        public void _can_GetRoutes_from_GenFormService()
        {
            var routes = genFormService.GetRoutes("", "");
            Assert.IsTrue(routes.Length > 0);
        }

        [TestMethod]
        public void _can_GetShapes_from_GenFormService()
        {
            var shapes = genFormService.GetShapes("", "");
            Assert.IsTrue(shapes.Length > 0);
        }

        [TestMethod]
        public void _can_AssembleValueListFromGenerics()
        {
            var list = ValueListAssembler.AssembleValueListDto(genFormService.GetGenerics("", ""));
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void _can_AssembleValueListFromShapes()
        {
            var list = ValueListAssembler.AssembleValueListDto(genFormService.GetShapes("", ""));
            Assert.IsTrue(list.Count > 0);
        }

        [TestMethod]
        public void _can_AssembleValueListFromRoutes()
        {
            var list = ValueListAssembler.AssembleValueListDto(genFormService.GetRoutes("", ""));
            Assert.IsTrue(list.Count > 0);
        }
    }
}
