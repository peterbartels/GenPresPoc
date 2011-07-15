using GenPres.Business.WebService;

namespace GenPres.Business.Domain.PrescriptionDomain.Medicine
{
    public class Medicine
    {
        #region Generics, Shapes and Routes

        private static readonly GenFormService GenFormService = new GenFormService();

        public static string[] GetGenerics(string route, string shape)
        {
            return GenFormService.GetGenerics(route, shape);
        }
        public static string[] GeRoutes(string generic, string shape)
        {
            return GenFormService.GetRoutes(generic, shape);
        }
        public static string[] GetShapes(string generic, string route)
        {
            return GenFormService.GetShapes(generic, route);
        }

        #endregion
    }
}
