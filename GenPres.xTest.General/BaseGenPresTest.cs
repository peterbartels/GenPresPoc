using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Assembler;

namespace GenPres.xTest.General
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
