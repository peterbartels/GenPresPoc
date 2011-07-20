﻿using System;
using System.Collections.Generic;
using System.Xml;
using CodeProject.Chidi.Cryptography;
namespace Settings
{
    public class SettingsManager
    {
        static SettingsManager instance = null;
        static readonly object padlock = new object();
        private string _path = @"C:\Development\GenPres-Development\GenPres\GenPres.Web\";
        private XmlDocument _settingsDoc = new XmlDocument();

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

        public void Initialize(string path)
        {
            _path = path;
            string file = _path + "\\Settings.xml";
            if (!System.IO.File.Exists(file))
            {
                throw new Exception("Could not find settings file in path: " + file);
            }
            _settingsDoc.Load(file);
        }

        public void Initialize()
        {
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
        public string ReadSecureSetting(string database, string name)
        {
            string machineCrypt = GetSecureMachineName("");
            string machineStr = "/settings/serversettings/machine[key='" + machineCrypt + "']/database[name='" + database + "']/" + name;
            XmlNode machineNode = _settingsDoc.SelectSingleNode(machineStr);
            if (machineNode == null)
            {
                throw new Exception("The setting " + name + " could not be found for this machine: " + machineCrypt);
            }
            _crypt.Key = _key;
            return _crypt.Decrypt(machineNode.InnerText);
        }

        public void CreateSecureSetting(string computerName, string database, string name, string value)
        {
            if (value == "") return;
            string xmlPathServer = "/settings/serversettings[1]";
            XmlNode serverNode = _settingsDoc.SelectSingleNode(xmlPathServer);
            string machineCrypt = GetSecureMachineName(computerName);

            string machineStr = "/settings/serversettings/machine[key='" + machineCrypt + "']/database[name='" + database + "']";
            XmlNode databaseNode = _settingsDoc.SelectSingleNode(machineStr);
            if (databaseNode == null)
            {
                XmlNode newMachineNode = _settingsDoc.CreateElement("machine");
                serverNode.AppendChild(newMachineNode);
                XmlNode keyNode = _settingsDoc.CreateElement("key");
                keyNode.InnerText = machineCrypt;
                newMachineNode.AppendChild(keyNode);

                XmlNode newDatabaseNode = _settingsDoc.CreateElement("database");
                newMachineNode.AppendChild(newDatabaseNode);
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
