using Informedica.GenPres.Data.Visibility;

namespace Informedica.GenPres.Data.DTO.Prescriptions
{
    public class PrescriptionDto
    {
        public PrescriptionDto()
        {
            drugGeneric = new StringDto() {value = "", visible = false};
            drugRoute = new StringDto() { value = "", visible = false };
            drugShape = new StringDto() { value = "", visible = false };

            substanceQuantity = new UnitValueDto();
            drugQuantity = new UnitValueDto();
            substanceDrugConcentration = new UnitValueDto();
            prescriptionFrequency = new UnitValueDto();
            prescriptionDuration = new UnitValueDto();
            doseQuantity = new UnitValueDto();
            doseTotal = new UnitValueDto();
            doseRate = new UnitValueDto();
            adminQuantity = new UnitValueDto();
            adminTotal = new UnitValueDto();
            adminRate = new UnitValueDto();
            patientWeight = new UnitValueDto();
            patientLength = new UnitValueDto();
            patientBSA = new UnitValueDto();

            prescriptionContinuous = new OptionDto();
            prescriptionInfusion = new OptionDto();
            prescriptionOnrequest = new OptionDto();
            prescriptionSolution = new OptionDto();
        }
        public string Id { get; set; }
        
        public string startDate { get; set; }
        public StringDto drugGeneric { get; set; }
        public StringDto drugRoute { get; set; }
        public StringDto drugShape { get; set; }
        public string PID { get; set; }

        public OptionDto prescriptionContinuous { get; set; }
        public OptionDto prescriptionInfusion { get; set; }
        public OptionDto prescriptionOnrequest { get; set; }
        public OptionDto prescriptionSolution { get; set; }

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

    public class StringDto : IVisible
    {
        public bool visible { get; set; }
        public string value { get; set; }
    }

    public class OptionDto : IVisible
    {
        public bool visible { get; set; }
        public string value { get; set; }
    }
}

