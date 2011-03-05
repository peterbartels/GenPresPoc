using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business;

namespace GenPres.Test
{
    class PrescriptionFactory
    {
        Prescription _prescription = Prescription.NewPrescription();
        public PrescriptionFactory(string adminUnit, string doseUnit)
        {
            _prescription.Quantity.Unit = adminUnit;

            _prescription.Rate.Unit = adminUnit;
            _prescription.Rate.Time = "uur";

            _prescription.Total.Unit = adminUnit;
            _prescription.Total.Time = "dag";
            
            _prescription.Drug = Drug.NewDrug();
            _prescription.Drug.Quantity.Unit = adminUnit;
            
            Component c = _prescription.Drug.Components.AddNew();
            Substance s = _prescription.Drug.Components[0].Substances.AddNew();
            
            s.DrugConcentration.Unit = doseUnit;
            s.DrugConcentration.Total = adminUnit;
            s.Quantity.Unit = doseUnit;
            
            Dose d = _prescription.Doses.AddNew();
            d.Quantity.Unit = doseUnit;
            d.Total.Unit = doseUnit;
            d.Total.Time = "dag";
            d.Rate.Unit = doseUnit;
            d.Rate.Time = "uur";
            
            _prescription.Frequency.Time = "dag";
            _prescription.Time.Time = "uur";
            _prescription.Medicine = Medicine.NewMedicine();

            _prescription.UpdateDoses(false);
        }

        public Prescription GetPrescription()
        {
            return _prescription;
        }
        public Prescription CalculateParacetamol()
        {
            _prescription.Drug.Name = "paracetamol";
            _prescription.Drug.Shape = "zetp";
            _prescription.Drug.Route = "rect";
            _prescription.Doses[0].Substance.Quantity.Value = 75;
            _prescription.Doses[0].Quantity.Value = 300;
            _prescription.Frequency.Value = 2;
            _prescription.PreRectifcation_Determine();
            _prescription.Calculate();
            return _prescription;
        }

    }
}
