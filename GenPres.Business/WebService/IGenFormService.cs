using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.WebService
{
    public interface IGenFormService
    {
        string[] GetGenerics(string route, string shape);
        string[] GetRoutes(string generic, string shape);
        string[] GetShapes(string generic, string route);
    }
}
