using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GenPres.Business.WebService
{
    public class GenFormService : IGenFormService
    {
        public string[] GetGenerics(string route, string shape)
        {
            if (route == "rect") return new string[] { "paracetamol"};
            if (shape == "zetp") return new string[] { "paracetamol" };

            if (route == "iv") return new string[] { "dopamine" };
            if (shape == "infuus") return new string[] { "dopamine" };
            return new string[] {"paracetamol", "dopamine"};
        }

        public string[] GetRoutes(string generic, string shape)
        {
            if (generic == "paracetamol") return new string[] { "rect" };
            if (generic == "dopamine") return new string[] { "iv" };
            if (shape == "infuus") return new string[] { "iv" };
            if (shape == "zetp") return new string[] { "rect" };
            return new string[] { "or", "rect", "iv" };
        }

        public string[] GetShapes(string generic, string route)
        {
            if (generic == "paracetamol") return new string[] { "zetp" };
            if (generic == "dopamine") return new string[] { "infuus" };

            if (route == "rect") return new string[] { "zetp" };
            if (route == "iv") return new string[] { "infuus" };
            return new string[] { "zetp", "infuus"};
        }

        public decimal[] GetComponentIncrements(string generic, string route, string shape)
        {
            return new[]{1m};
        }

        public decimal[] GetSubstanceIncrements(string generic, string route, string shape)
        {
            return new[] { 0.05m, 0.075m, 0,1m, 0.225m, 0.5m };
        }

        public ReadOnlyCollection<ComboValue> GetSubstanceUnits(string generic, string shape, string route)
        {
            return new[]
                       {
                           new ComboValue() {selected = false, Value = "gram"},
                           new ComboValue() {selected = true, Value = "mg"},
                           new ComboValue(){selected = false, Value = "microgr"}
                       }.ToList().AsReadOnly(); ;
        }

        public ReadOnlyCollection<ComboValue> GetComponentUnits(string generic, string shape, string route)
        {
            if(generic == "paracetamol")
            {
                return new[] { new ComboValue() { selected = true, Value = "zetp" } }.ToList().AsReadOnly(); ;    
            }
            if (generic == "dopamine")
            {
                return new[]
                       {
                           new ComboValue() {selected = false, Value = "l"},
                           new ComboValue() {selected = true, Value = "ml"}
                       }.ToList().AsReadOnly();
            }
            return new ComboValue[] { }.ToList().AsReadOnly(); ;   
        }
    }
}
