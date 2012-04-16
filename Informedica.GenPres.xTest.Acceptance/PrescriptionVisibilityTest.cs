using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Data.DTO.Prescriptions;
using Informedica.GenPres.Data.Visibility;

namespace Informedica.GenPres.xTest.Acceptance
{
    public class PrescriptionVisibilityTest
    {
        private const string FilledInToken = "AAAAA";

        public string DoseVolume
        {
            set
            {
                _prescription.FirstSubstance.Quantity.Unit = (value == FilledInToken) ? "ml" : "mg";
                _prescription.FirstDose.Quantity.Unit = (value == FilledInToken) ? "ml" : "mg";
            }
        }
        public string AdminVolume {set
        {
            _prescription.Drug.Quantity.Unit = (value == FilledInToken) ? "ml" : "mg";
            _prescription.Quantity.Unit = (value == FilledInToken) ? "ml" : "mg";
        }}

        public string Solution
        {
            get { return FormatVisibility(_prescriptionDto.prescriptionSolution); }
        }

        public string Concentration
        {
            get { return FormatVisibility(_prescriptionDto.substanceDrugConcentration); }
        }
        
        public string OnRequest { get
        {
            return FormatVisibility(_prescriptionDto.prescriptionOnrequest);
        }
            set { _prescription.OnRequest = (value == FilledInToken); }
        }

        public string Infusion
        {
            get { return FormatVisibility(_prescriptionDto.prescriptionInfusion); }
            set { _prescription.Infusion = (value == FilledInToken); }
        }

        public string Continuous
        {
            get { return FormatVisibility(_prescriptionDto.prescriptionContinuous); }
            set { _prescription.Continuous = (value == FilledInToken); }
        }

        public string Frequency
        {
            get { return FormatVisibility(_prescriptionDto.prescriptionFrequency); }
        }

        public string Duration
        {
            get { return FormatVisibility(_prescriptionDto.prescriptionDuration); }
        }
        public string DoseQuantity
        {
            get { return FormatVisibility(_prescriptionDto.doseQuantity); }
        }

        public string AdminQuantity
        {
            get { return FormatVisibility(_prescriptionDto.adminQuantity); }
        }

        public string DoseTotal
        {
            get { return FormatVisibility(_prescriptionDto.doseTotal); }
        }

        public string AdminTotal
        {
            get { return FormatVisibility(_prescriptionDto.adminTotal); }
        }

        public string DoseRate
        {
            get { return FormatVisibility(_prescriptionDto.doseRate); }
        }

        public string AdminRate
        {
            get { return FormatVisibility(_prescriptionDto.adminRate); }
        }
        private Prescription _prescription = Prescription.NewPrescription();
        private PrescriptionDto _prescriptionDto = new PrescriptionDto();

        public  PrescriptionVisibilityTest()
        {
                
        }


        private string FormatVisibility(IPropertyVisibility visibleProperty)
        {
            return (visibleProperty.visible) ? "XXXXX" : "------"; 
        }


        public void SetVisibility()
        {
            PrescriptionVisibility.Determine(_prescription, _prescriptionDto);
        }

        public void CreatePrescription()
        {
            _prescription = Prescription.NewPrescription();
            _prescription.Drug.Generic = "paracetamol";
            _prescription.Drug.Shape = "zetp";
            _prescription.Drug.Route = "rect";

            _prescriptionDto = new PrescriptionDto();
        }

        public void execute()
        {
            SetVisibility();
        }

        public void reset()
        {
            CreatePrescription();
        }
    }
}

