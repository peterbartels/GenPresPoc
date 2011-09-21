
using System;
using System.Linq.Expressions;
using GenPres.Business.Domain.Units;

namespace GenPres.Business.Calculation.Combination
{
    public interface ICalculationCombination
    {
        void Calculate();
        void Finish();
        bool CanBeCalculated();
        void ConvertCombinationsValuesToArray();
        void CorrectPropertyIncrements();
        int GetUserCount();
        Expression<Func<UnitValue>>[] GetProperties();
        UnitValue GetPropertyByName(string name);
    }
}
