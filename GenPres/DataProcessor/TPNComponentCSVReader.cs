using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GenPres
{
    public class TPNComponentCSVReader
    {
        private string[] componentNames;
        private string[] lines;
        private bool OnlySolutionComponents;
        private int solutionValue = 0;
        
        public TPNComponentCSVReader(string path, bool onlySolutionComponents)
        {
            path = @"C:\tpn_scenarios.csv";
            string contents = System.IO.File.ReadAllText(path);
            contents = contents.Replace("\r" ,"");
            lines = contents.Split('\n');
            componentNames = lines[0].Split(';');
            OnlySolutionComponents = onlySolutionComponents;
        }
        public string[] GetComponentNames(string[] selections)
        {
            List<string> result = new List<string>();
            int start = 0;
            int end = componentNames.Length;
            
            for (int i = start; i < end; i++)
            {
                if (componentNames[i].Trim() != "" && possibile(componentNames[i], selections)) result.Add(componentNames[i]);
            }
            return result.ToArray();
        }
        public bool possibile(string name, string[] selections)
        {
            for (int i = 1; i < lines.Length; i++)
            {
                string[] options = lines[i].Split(';');
                string componentName = options[0];
                if (selections.Contains<string>(componentName))
                {
                    for (int o = 1; o < options.Length; o++)
                    {
                        string option = options[o].Trim().ToLower();
                        if (componentNames[o] == name)
                        {
                            if (option == "x")
                            {
                                return false;
                            }
                            if (OnlySolutionComponents)
                            {
                                int value = 0;
                                if (!int.TryParse(option, out value))
                                    return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public Dictionary<string, Dictionary<string, decimal>> GetSolutions(string[] selections)
        {
            Dictionary<string, Dictionary<string, decimal>> values = new Dictionary<string, Dictionary<string, decimal>>();

            for (int l = 1; l < lines.Length; l++)
            {
                for (int c = 0; c < componentNames.Length; c++)
                {
                    string[] options = lines[l].Split(';');
                    string componentName = options[0];
                    if (selections.Contains<string>(componentName))
                    {
                        values[componentName] = new Dictionary<string, decimal>();
                        for (int o = 1; o < options.Length; o++)
                        {
                            string option = options[o].Trim().ToLower();
                            decimal value = 0;
                            if (decimal.TryParse(option, out value))
                            {
                                if (value > 0) values[componentName][componentNames[o]] = value;
                            }
                        }
                    }
                }
            }
            return values;
        }

    }
}
