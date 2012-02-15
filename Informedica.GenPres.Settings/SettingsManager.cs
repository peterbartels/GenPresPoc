using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using CodeProject.Chidi.Cryptography;

namespace Settings
{
    public class SettingsManager
    {
        private static string _path = @"C:\Development\GenPres\Informedica.GenPres.Web\";
        private XmlDocument _settingsDoc = null;

        public const string GenPresConnectionString = "GenPresConnectionString";
        public const string PdmsConnectionString = "PdmsConnectionString";
        public const string GenFormWebservice = "GenFormWebservice";
        public const string DatabaseName = "GenPres";

        private string _key = SecurityKey.Key;

        private SymCryptography _crypt = new SymCryptography(SymCryptography.ServiceProviderEnum.Rijndael);

        #region Singleton
        public SettingsManager()
        {}

        public static SettingsManager Instance
        {
            get
            {
                var _instance = new SettingsManager();
                return _instance;
            }
        }
        #endregion

        public void Initialize(string path)
        {
            _path = path;
            string file = _path + "\\Settings.xml";
            if (!System.IO.File.Exists(file))
            {
                throw new Exception("Could not find settings file in path: " + file);
            }
            if(_settingsDoc == null)
            {
                LoadDoc(file);
            }
        }

        private void LoadDoc(string file)
        {
            int i = 0;
            while(i<10)
            {
                try
                {
                    _settingsDoc = new XmlDocument();
                    _settingsDoc.LoadXml(ReadFile(file));
                    break;
                }catch
                {
                    System.Threading.Thread.Sleep(100);    
                }
                i++;
            }
        }

        public void Initialize()
        {
            string file = _path + "Settings.xml";

            if (!System.IO.File.Exists(file))
            {
                throw new Exception("Could not find settings file in path: " + file);
            }
            try
            {
                LoadDoc(file);
            }catch(System.IO.IOException exception)
            {
                if(!(exception is System.IO.FileNotFoundException))
                {
                    Initialize();
                }
            }
            
        }

        public string ReadFile(string path)
        {
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var sr = new StreamReader(fs);
            string contents = sr.ReadToEnd();
            sr.Close();
            return contents;
        }

        public string GetSettingsPath()
        {
            return _path;
        }
        public string GetSecureMachineName(string computerName)
        {
            _crypt.Key = _key;
            if (computerName == "")
            {
                computerName = Environment.MachineName;
            }
            return _crypt.Encrypt(computerName.ToLower());
        }

        private string GetSecureSetting(string value)
        {
            _crypt.Key = _key;
            return _crypt.Encrypt(value);

        }
        public string ReadSecureSetting(string database, string name)
        {
            string machineCrypt = GetSecureMachineName("");
            string machineStr = "/settings/serversettings/machine[key='" + machineCrypt + "']/database[name='" + database + "']/" + name;
            if(_settingsDoc == null) Initialize();
            XmlNode machineNode = _settingsDoc.SelectSingleNode(machineStr);
            if (machineNode == null)
            {
                throw new Exception("The setting " + name + " could not be found for this machine: " + machineCrypt + " and databasename:" + database);
            }
            _crypt.Key = _key;
            return _crypt.Decrypt(machineNode.InnerText);
        }

        public void CreateSecureSetting(string computerName, string database, string name, string value)
        {
            if (value == "") return;
            if (_settingsDoc == null) Initialize();

            string xmlPathServer = "/settings/serversettings[1]";
            XmlNode serverNode = _settingsDoc.SelectSingleNode(xmlPathServer);
            string machineCrypt = GetSecureMachineName(computerName);
            string machineStr = "/settings/serversettings/machine[key='" + machineCrypt + "']";
            XmlNode machineNode = _settingsDoc.SelectSingleNode(machineStr);
            if (machineNode == null)
            {
                machineNode = _settingsDoc.CreateElement("machine");
                serverNode.AppendChild(machineNode);
                XmlNode keyNode = _settingsDoc.CreateElement("key");
                keyNode.InnerText = machineCrypt;
                machineNode.AppendChild(keyNode);
            }
            machineStr = "/settings/serversettings/machine[key='" + machineCrypt + "']/database[name='" + database + "']";
            XmlNode databaseNode = _settingsDoc.SelectSingleNode(machineStr);
            if (databaseNode == null)
            {
                XmlNode newDatabaseNode = _settingsDoc.CreateElement("database");
                machineNode.AppendChild(newDatabaseNode);
                XmlNode dbNameNode = _settingsDoc.CreateElement("name");
                dbNameNode.InnerText = database;
                newDatabaseNode.AppendChild(dbNameNode);
                databaseNode = newDatabaseNode;
            }
            else
            {
                string appstr = "/settings/serversettings/machine[key='" + machineCrypt + "']/database[name='" + database + "']/" + name;
                XmlNode appnode = _settingsDoc.SelectSingleNode(appstr);
                if (appnode != null)
                {
                    appnode.ParentNode.RemoveChild(appnode);
                }
            }
            XmlNode newSettingsNode = _settingsDoc.CreateElement(name);
            newSettingsNode.InnerText = GetSecureSetting(value);
            databaseNode.AppendChild(newSettingsNode);
            _settingsDoc.Save(_path + "Settings.xml");
        }


        public IEnumerable<String> GetNames()
        {
            var list = new List<String>();
            if (_settingsDoc == null) Initialize();
            foreach (System.Xml.XmlElement node in _settingsDoc.GetElementsByTagName("machine"))
            {
                foreach (System.Xml.XmlElement child in node.ChildNodes)
                {
                    if (child.Name != "key") list.Add(child.Name);
                }
            }
            return list;
        }
    }
}
