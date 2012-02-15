using System;
using System.Linq.Expressions;
using Informedica.GenPres.Business.Domain.Units;

namespace Informedica.GenPres.Business.Calculation.Old.Combination
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
