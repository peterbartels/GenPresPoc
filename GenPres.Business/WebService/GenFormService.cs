using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.WebService
{
    public class GenFormService : IGenFormService
    {
        public string[] GetGenerics()
        {
            return new string[] {"paracetamol", "dopamine"};
        }

        public string[] GetRoutes()
        {
            return new string[] { "or", "rect", "iv" };
        }

        public string[] GetShapes()
        {
            return new string[] { "zetp", "infuus", "tabl" };
        }
    }
}
