
namespace GenPres.Business.Data.Client
{
    public class PatientTreeDto
    {
        public int id { get; set; }
        public string text { get; set; }
        public bool leaf { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PID { get; set; }
    }
}
