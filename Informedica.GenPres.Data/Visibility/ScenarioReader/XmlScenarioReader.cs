using System.IO;
using System.Xml;

namespace Informedica.GenPres.Data.Visibility.ScenarioReader
{
    public class XmlScenarioReader : IScenarioReader
    {
        private XmlDocument _xmlDocument = new XmlDocument();

        public XmlScenarioReader(string xmlContents, string xsdContents)
        {
            _xmlDocument.LoadXml(xmlContents);
            _xmlValidate(xsdContents);
        }

        private void _xmlValidate(string xsdContents)
        {
            var settings = new XmlReaderSettings();
            var xsdValidationReader = XmlReader.Create(new StringReader(xsdContents));
            settings.Schemas.Add(null, xsdValidationReader);
            settings.ValidationType = ValidationType.Schema;
            var reader = XmlReader.Create(new StringReader(_xmlDocument.OuterXml), settings);
        }

        public PrescriptionVisibilityScenario[] GetScenarios()
        {
            var prescriptionVisibility = XmlScenarioDeserializer.Deserialize(_xmlDocument.OuterXml);
            return prescriptionVisibility.Scenario;
        }
    }
}
