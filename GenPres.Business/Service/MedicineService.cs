using System.Collections.ObjectModel;
using GenPres.Business.Data.Client.GenForm;
using GenPres.Business.Domain.Prescription.Medicine;

namespace GenPres.Business.Service
{
    public static class MedicineService
    {
        public static ReadOnlyCollection<string> GetGenerics()
        {
            return ValueListAssembler.AssembleValueListDto(Medicine.GetGenerics());
        }
        public static ReadOnlyCollection<string> GetRoutes()
        {
            return ValueListAssembler.AssembleValueListDto(Medicine.GeRoutes());
        }
        public static ReadOnlyCollection<string> GetShapes()
        {
            return ValueListAssembler.AssembleValueListDto(Medicine.GetShapes());
        }
    }
}
