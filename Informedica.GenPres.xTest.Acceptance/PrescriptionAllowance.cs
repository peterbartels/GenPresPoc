using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Informedica.GenPres.Business.Calculation;
using Informedica.GenPres.Business.Domain.Prescriptions;

namespace Informedica.GenPres.xTest.Acceptance
{
    public class PrescriptionAllowance
    {
        public string DoseVolume { get; set; }
        public string AdminVolume { get; set; }
        public string Solution { get { return ""; } }
        public string Concentration { get { return ""; } }
        public string OnRequest { get; set; }
        public string Infusion { get; set; }
        public string Continuous { get; set; }

        private Prescription _prescription = Prescription.NewPrescription();

        public  PrescriptionAllowance()
        {
                
        }


        public void SetAllowance()
        {
            var calculator = new PrescriptionCalculator(_prescription);
            calculator.Calculate();
        }

        public void CreatePrescription()
        {
            _prescription = Prescription.NewPrescription();
        }

        public void execute()
        {
            SetAllowance();
        }

        public void reset()
        {
            CreatePrescription();
        }

    }
}

