﻿
namespace GenPres.Data.DTO.Patients
{
    public class PatientDto
    {
        public string Id { get; set; }
        public string PID { get; set; }
        public UnitValueDto Weight { get; set; }
        public UnitValueDto Height { get; set; }
    }
}
