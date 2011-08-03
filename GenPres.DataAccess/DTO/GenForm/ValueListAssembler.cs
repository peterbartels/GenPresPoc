using System.Collections.ObjectModel;
using System.Linq;

namespace GenPres.DataAccess.DTO.GenForm
{
    public class ValueListAssembler
    {
        public static ReadOnlyCollection<ValueDto> AssembleValueListDto(string[] values)
        {
            var valueDtos = values.Select(
                t => new ValueDto {Value = t}
            ).ToList();

            return valueDtos.AsReadOnly();
        }
    }
}
