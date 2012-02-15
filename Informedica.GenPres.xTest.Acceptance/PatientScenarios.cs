using System;
using GenPres.Assembler;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Business.Domain.Units;
using GenPres.Business.Service;
using GenPres.Data.Connections;
using GenPres.Service;
using GenPres.xTest.Base;

namespace GenPres.xTest.Acceptance
{
    public class PatientScenarios
    {
        public decimal Weight;
        public decimal Length;
        public string WeightUnit;
        public string LengthUnit;

        public PatientScenarios()
        {
            
        }

        public decimal CalculateBSA()
        {
            Prescription p = Prescription.NewPrescription();
            p.PatientLength = UnitConverter.GetBaseValue(LengthUnit, Length);
            p.PatientLengthUnit = LengthUnit;
            p.PatientWeight = UnitConverter.GetBaseValue(WeightUnit, Weight); ;
            p.PatientWeightUnit = WeightUnit;
            return Math.Round(p.PatientBsa, 2);
        }
    }
}
