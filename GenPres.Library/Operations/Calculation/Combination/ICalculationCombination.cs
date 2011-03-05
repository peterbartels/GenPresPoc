using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Csla;
using GenPres.Business;

namespace GenPres.Operations.Calculation
{
    public interface ICalculationCombination
    {
        bool Contains(PropertyInfo<UnitValue> propertyToContain);
        int GetNotSetCount();
        void Calculate();
        void SetLastPropertyCalculated(PropertyInfo<UnitValue> lastPropertyCalculated);
        PropertyInfo<UnitValue> GetAt(int index);
        void CalculateSibblings(int index);
        bool HasDependent();
    }
}
