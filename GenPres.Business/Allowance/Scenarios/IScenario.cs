using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.Allowance.Scenarios
{
    public interface IScenario
    {
        PropertyAllowanceConfig[] PropertyAllowance { get; }
        bool[] Criteria();
    }
}
