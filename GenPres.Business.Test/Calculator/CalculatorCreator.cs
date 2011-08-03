using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Domain.Units;

namespace GenPres.Business.Test.Calculator
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
