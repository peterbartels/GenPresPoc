using System;
using System.Collections.Generic;
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
    }
}
