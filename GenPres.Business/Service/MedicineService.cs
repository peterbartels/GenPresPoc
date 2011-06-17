using System.Collections.ObjectModel;
using GenPres.Business.Data.Client.GenForm;
using GenPres.Business.Domain.Prescription.Medicine;

namespace GenPres.Business.Service
{
    public static class MedicineService
    {
        public static ReadOnlyCollection<ValueDto> GetGenerics()
        {
            return ValueListAssembler.AssembleValueListDto(Medicine.GetGenerics());
        }
        public static ReadOnlyCollection<ValueDto> GetRoutes()
        {
            return ValueListAssembler.AssembleValueListDto(Medicine.GeRoutes());
        }
        public static ReadOnlyCollection<ValueDto> GetShapes()
        {
            return ValueListAssembler.AssembleValueListDto(Medicine.GetShapes());
        }
    }
}
