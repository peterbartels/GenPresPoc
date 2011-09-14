using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Domain.Units;
namespace GenPres.Business.Allowance.Scenarios
{
    public class PropertyAllowanceConfig
    {
        private bool _allow;
        private IPropertyAllowance _property;
        public PropertyAllowanceConfig(bool allow, IPropertyAllowance property)
        {
            _allow = allow;
            _property = property;
        }
    }
}
