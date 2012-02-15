using System;
using Informedica.GenPres.Business.Domain.Units;

namespace Informedica.GenPres.xTest.Calculation.CalculatorTests
{
    public class CalculatorCreator
    {
        public static void SetRandomValue(UnitValue uv, decimal increment)
        {
            var rnd = new Random();
            var nr = rnd.Next(1, 500);
            uv.Value = nr*increment;
        }
    }
}
