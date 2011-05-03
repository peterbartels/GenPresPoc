using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.Domain
{
    public interface ILogicalUnit
    {
        int Id { get; set; }
        string Name { get; set; }
    }
}
