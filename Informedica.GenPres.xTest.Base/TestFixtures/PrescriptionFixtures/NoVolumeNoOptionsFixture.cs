using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Informedica.GenPres.Business.Domain.Prescriptions;

namespace Informedica.GenPres.xTest.Base.TestFixtures.PrescriptionFixtures
{
    public class NoVolumeNoOptionsFixture
    {
        public static Prescription CreateParacetamolGeneric()
        {
            var prescription = Prescription.NewPrescription();
            prescription.Drug.Generic = "paracetamol";
            prescription.Drug.Route = "rect";
            prescription.Drug.Shape = "zetp";
            prescription.SetDefaultUnits("mg", "zetp");
            return prescription;
        }

        public static Prescription GetParacetamolPrescription()
        {
            var prescription = CreateParacetamolGeneric();
            prescription.Frequency.Value = 2;
            prescription.Total.Value = 6;
            prescription.Drug.Quantity.Value = 2;
            prescription.Drug.Components[0].Substances[0].Quantity.Value = 0.4m;
            return prescription;
        }
    }
}
