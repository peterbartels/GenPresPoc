using GenPres.Business.Domain.Prescriptions;
using GenPres.Business.Domain.Units;

namespace GenPres.Business.Verbalization
{
    public class PrescriptionVerbalization
    {
        public static string Verbalize(Prescription prescription)
        {
            var substance = prescription.Drug.Components[0].Substances[0];
            return
                SpaceEnd(prescription.Drug.Generic) +
                SpaceEnd(GetUnitVerbalization(substance.Quantity) +
                SpaceEnd(prescription.Drug.Shape) +
                SpaceEnd(prescription.Drug.Route) +
                SpaceEnd(GetUnitVerbalization(prescription.Frequency)) +
                SpaceEnd(GetUnitVerbalization(prescription.Quantity)) +
                SpaceEnd(GetUnitVerbalization(prescription.Total)) +
                SpaceEnd(GetUnitVerbalization(prescription.Doses[0].Quantity))
            );
        }

        public static string SpaceEnd(string input)
        {
            return (input == "") ? "" : input + ' ';
        }

        public static string GetUnitVerbalization(UnitValue uv)
        {
            var res = "";
            if (uv.Value.ToString() != "0")
            {
                res = res + uv.Value.ToString();
                if(uv.Unit != "")
                {
                    res = res + " " + uv.Unit;
                }
            }
            return res;
        }
    }
}
    
