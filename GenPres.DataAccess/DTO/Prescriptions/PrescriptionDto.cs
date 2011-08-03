namespace GenPres.DataAccess.DTO.Prescriptions
{
    public class PrescriptionDto
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string drugGeneric { get; set; }
        public string drugRoute { get; set; }
        public string drugShape { get; set; }
        public string PID { get; set; }
    }
}
