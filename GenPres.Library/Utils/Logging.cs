using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UmcuLib;

namespace GenPres.Operations
{
    public sealed class Logging
    {
        static Logging instance = null;
        private Loesje logger = new Loesje("GenPres");

        static readonly object padlock = new object();
        private string _logPath = "d:\\temp\\logging" + DateTime.Now.ToString("HH-mm-ss") + ".txt";
        Logging()
        {
        }

        public static Logging Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {

                        instance = new Logging();
                    }
                    return instance;
                }
            }
        }

        public void LogText(string text)
        {
            //logger.Log(text, LogLevels.Debug);
            //File.AppendAllText(_logPath, text + "\r\n");
        }
    }
}
