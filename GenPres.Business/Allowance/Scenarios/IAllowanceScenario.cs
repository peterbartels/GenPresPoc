using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using GenPres.Business.Domain.Units;

namespace GenPres.Business.Allowance.Scenarios
{
    public interface IAllowanceScenario
    {
        bool[] criteria { get; set; }
        List<Expression<Func<UnitValue>>> hideFields {get;set;}

    }
}
