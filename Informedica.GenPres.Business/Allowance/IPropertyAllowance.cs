using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.Allowance
{
    public interface IPropertyAllowance
    {
        bool CanBeSet { get; set; }
    }
}
