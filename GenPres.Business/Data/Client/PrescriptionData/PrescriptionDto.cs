using System;

namespace GenPres.Business.Data.Client.PrescriptionData
{
    public class PrescriptionDto
    {
        public int id { get; set; }
        public DateTime StartDate { get; set; }
        public string drugGeneric { get; set; }
        public string drugRoute { get; set; }
        public string drugShape { get; set; }
    }
}
