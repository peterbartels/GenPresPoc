using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Operations
{
    public static class Utils
    {
        public static List<decimal> ConvertStringArrayToDoubleList(string[] input)
        {
            List<decimal> newValues = new List<decimal>();
            for (int i = 0; i < input.Length; i++)
            {
                try
                {
                    newValues.Add((decimal) double.Parse(input[i], System.Globalization.CultureInfo.CreateSpecificCulture("en-US")));
                }
                catch 
                {
                    try
                    {
                        decimal value = (decimal)double.Parse(input[i], System.Globalization.CultureInfo.CreateSpecificCulture("en-US"));
                        newValues.Add((decimal)value);
                    }
                    catch
                    {
                        throw new Exception("Cannot parse string value: " + input[i]);
                    }
                }
            }
            return newValues;
        }
    }
}
