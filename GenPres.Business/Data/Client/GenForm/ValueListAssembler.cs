using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GenPres.Business.Data.Client.GenForm
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
