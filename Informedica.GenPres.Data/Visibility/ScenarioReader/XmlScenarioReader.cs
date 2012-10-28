using System.IO;
using System.Xml;
using System.Xml.Schema;

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
            var schemaReader = XmlReader.Create(new StringReader(xsdContents));
            var xmlSchema = XmlSchema.Read(schemaReader, _xsdValidationEventHandler);

            var xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.Schemas.Add(xmlSchema);
            xmlReaderSettings.Schemas.Compile();
            xmlReaderSettings.ValidationEventHandler += _xsdValidationEventHandler;
            xmlReaderSettings.ValidationType = ValidationType.Schema;
            xmlReaderSettings.ValidationFlags |= XmlSchemaValidationFlags.ProcessIdentityConstraints;
            xmlReaderSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            xmlReaderSettings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;

            using (var xmlReader = XmlReader.Create(new StringReader(_xmlDocument.OuterXml), xmlReaderSettings))
            {
                while (xmlReader.Read()) { }
            }

        }


        private void _xsdValidationEventHandler(object sender, ValidationEventArgs e)
        {
            throw e.Exception;
        }

        public PrescriptionVisibilityScenario[] GetScenarios()
        {
            var prescriptionVisibility = XmlScenarioDeserializer.Deserialize(_xmlDocument.OuterXml);
            return prescriptionVisibility.Scenario;
        }
    }
}
