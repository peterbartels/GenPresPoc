using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GenPres.Business.Data.Client.GenForm
{
    public class ValueListAssembler
    {
        public static ReadOnlyCollection<string> AssembleValueListDto(string[] values)
        {
            var resultValues = values.ToList();
            return resultValues.AsReadOnly();
        }
    }
}
