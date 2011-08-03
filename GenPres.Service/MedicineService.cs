using System.Collections.ObjectModel;
using GenPres.Business.Domain.Prescriptions.Medicine;
using GenPres.DataAccess.DTO.GenForm;

namespace GenPres.Business.Service
{
    public static class MedicineService
    {
        public static ReadOnlyCollection<ValueDto> GetGenerics(string route, string shape)
        {
            return ValueListAssembler.AssembleValueListDto(Medicine.GetGenerics(route, shape));
        }
        public static ReadOnlyCollection<ValueDto> GetRoutes(string generic, string shape)
        {
            return ValueListAssembler.AssembleValueListDto(Medicine.GeRoutes(generic, shape));
        }
        public static ReadOnlyCollection<ValueDto> GetShapes(string generic, string route)
        {
            return ValueListAssembler.AssembleValueListDto(Medicine.GetShapes(generic, route));
        }
    }
}
