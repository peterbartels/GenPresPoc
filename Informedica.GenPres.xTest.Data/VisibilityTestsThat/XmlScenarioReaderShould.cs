using System.Xml;
using System.Xml.Schema;
using Informedica.GenPres.Data.Visibility.ScenarioReader;
using Informedica.GenPres.xTest.Data.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Data.VisibilityTest
{
    [TestClass]
    public class XmlScenarioReaderShould
    {
        private string _scenariosXml = Resources.visibilityScenariosXml;
        private string _scenariosXsd = Resources.visibilityScenariosXsd;

        [TestMethod]
        [ExpectedException(typeof(XmlException),"An invalid XML was allowed")]
        public void ThrowExceptionWhenCreatedWithInvalidXml()
        {
            var scenarioReader = new XmlScenarioReader("thisisnotxml", _scenariosXsd);
        }

        [TestMethod]
        [ExpectedException(typeof(XmlSchemaException), "An invalid XSD was allowed")]
        public void ThrowExceptionWhenDoesNotMatchXsd()
        {
            var scenarioReader = new XmlScenarioReader("<thisisxml />", "<thisisnotaxsd />");
        }

        [TestMethod]
        [ExpectedException(typeof(XmlSchemaValidationException), "A xml could not be validated against a xsd")]
        public void ThrowExceptionWhenXmlCannotBeValidatedByAXsd()
        {
            var scenarioReader = new XmlScenarioReader("<PrescriptionVisibility><b>test</b></PrescriptionVisibility>", _scenariosXsd);
        }

        [TestMethod]
        public void GetScenariosFromXmlFileWithValidXsd()
        {
            var scenarioReader = new XmlScenarioReader(_scenariosXml, _scenariosXsd);
            var scenarios = scenarioReader.GetScenarios();
            Assert.IsTrue(scenarios.Length == 1);
        }
    }
}
