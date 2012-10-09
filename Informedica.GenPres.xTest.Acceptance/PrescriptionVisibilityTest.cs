﻿using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Data.DTO.Prescriptions;
using Informedica.GenPres.Data.Visibility;

namespace Informedica.GenPres.xTest.Acceptance
{
    public class PrescriptionVisibilityTest
    {
        private bool useConfiguration = false;
        
        public PrescriptionVisibilityTest(string configurationSetting)
        {
            
        }

        private const string FilledInToken = "yes";
        public string DoseIsAVolume
        {
            set
            {
                _prescription.FirstSubstance.Quantity.Unit = (value == FilledInToken) ? "ml" : "mg";
                _prescription.FirstDose.Quantity.Unit = (value == FilledInToken) ? "ml" : "mg";
            }
        }
        public string AdminIsAVolume
        {
            set
            {
                _prescription.Drug.Quantity.Unit = (value == FilledInToken) ? "ml" : "mg";
                _prescription.Quantity.Unit = (value == FilledInToken) ? "ml" : "mg";
            }
        }

        public string SolutionIsVisible
        {
            get { return FormatVisibility(_prescriptionDto.prescriptionSolution); }
        }

        public string ConcentrationIsVisible
        {
            get { return FormatVisibility(_prescriptionDto.substanceDrugConcentration); }
        }

        public string OnRequestIsVisible
        {
            get
            {
                return FormatVisibility(_prescriptionDto.prescriptionOnrequest);
            }
        }

        public string OnRequestInput
        {
            set { _prescription.OnRequest = (value == FilledInToken); }
        }

        public string InfusionIsVisible
        {
            get { return FormatVisibility(_prescriptionDto.prescriptionInfusion); }
        }

        public string InfusionInput
        {
            set { _prescription.Infusion = (value == FilledInToken); }
        }

        public string GenericInput
        {
            set { _prescription.Drug.Generic = "Paracetamol"; }
        }

        public string RouteInput
        {
            set { _prescription.Drug.Route = "rect"; }
        }

        public string ShapeInput
        {
            set { _prescription.Drug.Shape = "zetp"; }
        }

        public string GenericVisible
        {
            get { return FormatVisibility(_prescriptionDto.drugGeneric); }
        }
        
        public string RouteVisible
        {
            get { return FormatVisibility(_prescriptionDto.drugRoute); }
        }
        
        public string ShapeVisible
        {
            get { return FormatVisibility(_prescriptionDto.drugShape); }
        }

        public string ContinuousIsVisible
        {
            get { return FormatVisibility(_prescriptionDto.prescriptionContinuous); }
        }

        public string ContinuousInput
        {
            set { _prescription.Continuous = (value == FilledInToken); }
        }

        public string FrequencyIsVisible 
        {
            get { return FormatVisibility(_prescriptionDto.prescriptionFrequency); }
        }

        public string DurationIsVisible
        {
            get { return FormatVisibility(_prescriptionDto.prescriptionDuration); }
        }
        public string DoseQuantityIsVisible
        {
            get { return FormatVisibility(_prescriptionDto.doseQuantity); }
        }

        public string AdminQuantityIsVisible
        {
            get { return FormatVisibility(_prescriptionDto.adminQuantity); }
        }

        public string DoseTotalIsVisible
        {
            get { return FormatVisibility(_prescriptionDto.doseTotal); }
        }

        public string AdminTotalIsVisible
        {
            get { return FormatVisibility(_prescriptionDto.adminTotal); }
        }

        public string DoseRateIsVisible
        {
            get { return FormatVisibility(_prescriptionDto.doseRate); }
        }

        public string AdminRateIsVisible
        {
            get { return FormatVisibility(_prescriptionDto.adminRate); }
        }

        

        private Prescription _prescription = Prescription.NewPrescription();
        private PrescriptionDto _prescriptionDto = new PrescriptionDto();

        public PrescriptionVisibilityTest()
        {

        }


        private string FormatVisibility(IVisible visibleProperty)
        {
            return FormatVisibilityBool(visibleProperty.visible);
        }

        private string FormatVisibilityBool(bool isVisible)
        {
            return (isVisible) ? "yes" : "no";
        }


        public void SetVisibility()
        {
            //PrescriptionVisibility.Execute(_prescription, _prescriptionDto);

        }

        public void CreatePrescription()
        {
            _prescription = Prescription.NewPrescription();
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

