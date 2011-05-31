using GenPres.Business.WebService;

namespace GenPres.Business.Domain.Prescription.Medicine
{
    public class Medicine
    {
        #region Generics, Shapes and Routes

        private static readonly GenFormService GenFormService = new GenFormService();

        public static string[] GetGenerics()
        {
            return GenFormService.GetGenerics();
        }
        public static string[] GetShapes()
        {
            return GenFormService.GetShapes();
        }
        public static string[] GeRoutes()
        {
            return GenFormService.GetRoutes();
        }

        #endregion
    }
}
