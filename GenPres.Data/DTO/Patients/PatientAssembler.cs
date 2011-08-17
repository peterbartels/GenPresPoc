using System.Collections.Generic;
using System.Collections.ObjectModel;
using GenPres.Business.Domain.Patients;

namespace GenPres.Data.DTO.Patients
{
    public class PatientAssembler
    {
        public static PatientDto AssemblePatientDto(Patient patient)
        {
            var dto = new PatientDto();
            dto.Id = patient.Id.ToString();
            dto.PID = patient.Pid;
            
            dto.Weight = new UnitValueDto();
            dto.Height = new UnitValueDto();

            dto.Weight.value = patient.Weight.Value;
            dto.Height.value = patient.Height.Value;

            dto.Weight.unit = patient.Weight.Unit;
            dto.Height.unit = patient.Height.Unit;
   
            return dto;
        }
    }
}
