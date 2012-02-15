using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GenPres.Business.WebService
{
    public interface IGenFormWebServices
    {
        string[] GetGenerics(string route, string shape);
        string[] GetRoutes(string generic, string shape);
        string[] GetShapes(string generic, string route);
        ReadOnlyCollection<SelectionItem> GetComponentUnits(string generic, string route, string shape);
        ReadOnlyCollection<SelectionItem> GetSubstanceUnits(string generic, string route, string shape);
    }
}
