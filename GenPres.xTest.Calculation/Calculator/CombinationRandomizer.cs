using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GenPres.Business.Calculation.Combination;
using GenPres.xTest.Business.Calculator;

namespace GenPres.xTest.Calculation.Calculator
{
    public class CombinationRandomizer
    {
        public static void RandomizeCombination(MultiplierCombination combi)
        {
            var rnd = new Random();
            var total = rnd.Next(40, 120) * combi.GetUnitValue(0).Factor.IncrementSizes[0];
            var nr1 = rnd.Next(1, 60) * combi.GetUnitValue(1).Factor.IncrementSizes[0];
            var nr2 = rnd.Next(1, 60) * combi.GetUnitValue(2).Factor.IncrementSizes[0];
            combi.GetUnitValue(0).Value = total;
            combi.GetUnitValue(1).Value = nr1;
            combi.GetUnitValue(2).Value = nr2;
            File.AppendAllText(CombinationTest._testPath, "1: Value0=" + total + " Value1=" + nr1 + " Value2=" + nr2 + "\r\n");
        }
    }
}
