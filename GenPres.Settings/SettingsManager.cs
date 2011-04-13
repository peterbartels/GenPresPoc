using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using CodeProject.Chidi.Cryptography;
namespace Settings
{
    public class SettingsManager
    {
        static SettingsManager instance = null;
        static readonly object padlock = new object();
        private string _path;
        private XmlDocument _settingsDoc = new XmlDocument();
        
        private string _key = SecurityKey.Key;

        private SymCryptography _crypt = new SymCryptography(SymCryptography.ServiceProviderEnum.Rijndael);

        #region Singleton
        public SettingsManager()
        {}

        public static SettingsManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SettingsManager();
                    }
                    return instance;
                }
            }
        }
        #endregion

        public void SetSettingsPath(string path)
        {
            _path = path;
            string file = _path + "Settings.xml";
            if (!System.IO.File.Exists(file))
            {
                throw new Exception("Could not find settings file in path: " + file);
            }
            _settingsDoc.Load(file);
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
        public string ReadSecureSetting(string name)
        {
            string machineCrypt = GetSecureMachineName("");
            string machineStr = "/settings/serversettings/machine[key='" + machineCrypt + "']/" + name;
            XmlNode machineNode = _settingsDoc.SelectSingleNode(machineStr);
            if (machineNode == null)
            {
                throw new Exception("The setting " + name + " could not be found for this machine: " + machineCrypt);
            }
            _crypt.Key = _key;
            return _crypt.Decrypt(machineNode.InnerText);
        }

        public void CreateSecureSetting(string computerName, string name, string value)
        {
            if (value == "") return;
            string xmlPathServer = "/settings/serversettings[1]";
            XmlNode serverNode = _settingsDoc.SelectSingleNode(xmlPathServer);
            string machineCrypt = GetSecureMachineName(computerName);
            
            string machineStr = "/settings/serversettings/machine[key='" + machineCrypt + "']";
            XmlNode machineNode = _settingsDoc.SelectSingleNode(machineStr);
            if (machineNode == null)
            {
                XmlNode newMachineNode = _settingsDoc.CreateElement("machine");
                serverNode.AppendChild(newMachineNode);
                XmlNode keyNode = _settingsDoc.CreateElement("key");
                keyNode.InnerText = machineCrypt;
                newMachineNode.AppendChild(keyNode);
                machineNode = newMachineNode;
            }
            else
            {
                string appstr = "/settings/serversettings/machine[key='" + machineCrypt + "']/" + name;
                XmlNode appnode = _settingsDoc.SelectSingleNode(appstr);
                if (appnode != null)
                {
                    appnode.ParentNode.RemoveChild(appnode);
                }
            }
            XmlNode newSettingsNode = _settingsDoc.CreateElement(name);
            newSettingsNode.InnerText = GetSecureSetting(value);
            machineNode.AppendChild(newSettingsNode);
            _settingsDoc.Save(_path + "Settings.xml");
        }
    }
}
