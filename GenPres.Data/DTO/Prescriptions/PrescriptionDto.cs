namespace GenPres.Data.DTO.Prescriptions
{
    public class PrescriptionDto
    {
        public string Id { get; set; }
        
        public string startDate { get; set; }
        public string drugGeneric { get; set; }
        public string drugRoute { get; set; }
        public string drugShape { get; set; }
        public string PID { get; set; }

        public bool continuous { get; set; }
        public bool infusion { get; set; }
        public bool onrequest { get; set; }
        public bool solution { get; set; }

        public UnitValueDto substanceQuantity { get; set; }
        public UnitValueDto drugQuantity { get; set; }
        public UnitValueDto substanceDrugConcentration { get; set; }

        public UnitValueDto prescriptionQuantity { get; set; }
        public UnitValueDto prescriptionTotal { get; set; }
        public UnitValueDto prescriptionRate { get; set; }

        public UnitValueDto doseQuantity { get; set; }
        public UnitValueDto doseTotal { get; set; }
        public UnitValueDto doseRate { get; set; }
    }
}

