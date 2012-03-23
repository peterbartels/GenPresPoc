using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Business.Domain.Units;

namespace Informedica.GenPres.Business.Verbalization
{
    public class PrescriptionVerbalization
    {
        private static Prescription _prescription;
        
        public static string Verbalize(Prescription prescription)
        {
            var prescriptionVerbalization = new PrescriptionVerbalization(prescription);
            return prescriptionVerbalization._verbalize();
        }

        private PrescriptionVerbalization(Prescription prescription)
        {
            _prescription = prescription;
            var substance = _prescription.Drug.Components[0].Substances[0];
        }


        public string _verbalize()
        {
            /*if (new NoVolumes(_prescription).IsTrue() && new NoOptions(_prescription).IsTrue())
                return
                    SpaceEnd(_prescription.Drug.Generic) +
                    SpaceEnd(GetUnitVerbalization(_prescription.FirstSubstance.Quantity)) +
                    SpaceEnd(_prescription.Drug.Shape) +
                    SpaceEnd(_prescription.Drug.Route) +
                    GetfrequencyVerbalization(_prescription.Frequency) +
                    CommaEnd(VerbalizeUnitValueExplanation(_prescription.Quantity, _prescription.FirstDose.Quantity)) + 
                    VerbalizeUnitValueExplanation(_prescription.Total, _prescription.FirstDose.Total) 
                ;*/
            return "";
        }

        public static string GetfrequencyVerbalization(UnitValue frequency)
        {
            return ((frequency.Value > 0 && frequency.Time != "") ? 
                VerbalizeDecimal(frequency.Value) + " keer per " + frequency.Time + " " 
            :  "");
        }

        public string VerbalizeUnitValueExplanation(UnitValue unitValue1, UnitValue unitValue2)
        {
            string result = "";

            if(unitValue1.Value > 0)
                result = result + GetUnitVerbalization(unitValue1);
            
            if (unitValue1.Value > 0 && unitValue2.Value > 0)
                result = result + " = ";
            
            if(unitValue2.Value > 0)
                result = result + GetUnitVerbalization(unitValue2);

            return result;
        }

        public static string CommaEnd(string input)
        {
            return (input == "") ? "" : input + ", ";
        }

        public static string SpaceEnd(string input)
        {
            return (input == "") ? "" : input + ' ';
        }

        public static string GetUnitVerbalization(UnitValue uv)
        {
            var res = "";
            if (uv.Value > 0)
            {
                res = res + VerbalizeDecimal(uv.Value);
                res = res + " " + uv.Unit;
                if(uv.Unit != "")
                {
                    if (uv.Adjust != "")
                    {
                        res = res + "/" + uv.Adjust;
                    }
                    if (uv.Total != "")
                    {
                        res = res + "/" + uv.Total;
                    }
                    if(uv.Time != "")
                    {
                        res = res + "/" + uv.Time;    
                    }
                }
            }
            return res;
        }

        public static string VerbalizeDecimal(decimal decimalValue)
        {
            return decimalValue.ToString().Contains(".") ? decimalValue.ToString("#.##########").TrimEnd('0') : decimalValue.ToString();
        }
    }
}
    
