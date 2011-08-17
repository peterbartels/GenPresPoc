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
        public UnitValueDto substanceQuantity { get; set; }
    }
}
