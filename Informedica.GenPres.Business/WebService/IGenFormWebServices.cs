using System.Collections.ObjectModel;

namespace Informedica.GenPres.Business.WebService
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
