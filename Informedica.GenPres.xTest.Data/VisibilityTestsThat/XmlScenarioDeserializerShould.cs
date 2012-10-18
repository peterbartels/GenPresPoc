using Informedica.GenPres.Data.Visibility.ScenarioReader;
using Informedica.GenPres.xTest.Data.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Data.VisibilityTestsThat
{
    [TestClass]
    public class XmlScenarioDeserializerShould
    {
        [TestMethod]
        public void DeserializeSceanrioXmlToSccenarioObject()
        {
            var xml = Resources.visibilityScenariosXml;

            var deserializedScenarios = XmlScenarioDeserializer.Deserialize(xml);
            var scenarios = deserializedScenarios.Scenario;

            Assert.IsTrue(scenarios.Length== 1);
            Assert.IsTrue(scenarios[0].Generic == true);
            Assert.IsTrue(scenarios[0].GenericVisible == true);
        }
    }
}
