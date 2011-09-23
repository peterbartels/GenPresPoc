using System;

namespace GenPres.Data.DTO.Prescriptions
{
    public class PrescriptionDto
    {
        public PrescriptionDto()
        {
            drugGeneric = "";
            drugRoute = "";
            drugShape = "";
        }
        public string Id { get; set; }
        
        public string startDate { get; set; }
        public string drugGeneric { get; set; }
        public string drugRoute { get; set; }
        public string drugShape { get; set; }
        public string PID { get; set; }

        public string prescriptionContinuous { get; set; }
        public string prescriptionInfusion { get; set; }
        public string prescriptionOnrequest { get; set; }
        public string prescriptionSolution { get; set; }

        public UnitValueDto substanceQuantity { get; set; }
        public UnitValueDto drugQuantity { get; set; }
        public UnitValueDto substanceDrugConcentration { get; set; }

        public UnitValueDto prescriptionFrequency { get; set; }
        public UnitValueDto prescriptionDuration { get; set; }

        public UnitValueDto doseQuantity { get; set; }
        public UnitValueDto doseTotal { get; set; }
        public UnitValueDto doseRate { get; set; }

        public UnitValueDto adminQuantity { get; set; }
        public UnitValueDto adminTotal { get; set; }
        public UnitValueDto adminRate { get; set; }

        public string verbalization { get; set; }

        public UnitValueDto patientWeight { get; set; }
        public UnitValueDto patientLength { get; set; }
        public UnitValueDto patientBSA { get; set; }

        public bool DoseVolume { get; set; }
        public bool AdminVolume { get; set; }
    }
}

