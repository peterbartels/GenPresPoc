﻿
namespace Informedica.GenPres.Data.DTO.Patients
{
    public class PatientTreeDto
    {
        public string id { get; set; }
        public string text { get; set; }
        public bool leaf { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PID { get; set; }

        public string RegisterDate { get; set; }
        public string Days { get; set; }
        public string Bed { get; set; }
        public string Unit { get; set; }
    }
}
