using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.WebService
{
    public interface IGenFormService
    {
        string[] GetGenerics();
        string[] GetRoutes();
        string[] GetShapes();
    }
}
