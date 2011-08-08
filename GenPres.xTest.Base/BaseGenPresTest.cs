using GenPres.Assembler;

namespace GenPres.xTest.Base
{
    public class BaseGenPresTest
    {
        
        public BaseGenPresTest()
        {
            GenPresApplication.Initialize();
            Settings.SettingsManager.Instance.Initialize();
        }
    }
}
