namespace Informedica.GenPres.Data.Visibility.ScenarioReader
{
    public class XmlScenarioDeserializer
    {
        private static System.Xml.Serialization.XmlSerializer _serializer;

        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((_serializer == null))
                {
                    _serializer = new System.Xml.Serialization.XmlSerializer(typeof (PrescriptionVisibility));
                }
                return _serializer;
            }
        }


        public static PrescriptionVisibility Deserialize(string xml)
        {
            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return
                    ((PrescriptionVisibility)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }
    }
}