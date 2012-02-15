using GenPres.Business.WebService;

namespace GenPres.Business.Domain.Prescriptions.Medicine
{
    public class Medicine
    {
        private static IGenFormWebServices GenFormWebService
        {
            get { return StructureMap.ObjectFactory.GetInstance<IGenFormWebServices>(); }
        }
        #region Generics, Shapes and Routes

        public static string[] GetGenerics(string route, string shape)
        {
            return GenFormWebService.GetGenerics(route, shape);
        }
        public static string[] GeRoutes(string generic, string shape)
        {
            return GenFormWebService.GetRoutes(generic, shape);
        }
        public static string[] GetShapes(string generic, string route)
        {
            return GenFormWebService.GetShapes(generic, route);
        }

        #endregion
    }
}
