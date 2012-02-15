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
            
            dto.Weight = UnitValueDto.AssembleUnitValueDto(patient.Weight);
            dto.Height = UnitValueDto.AssembleUnitValueDto(patient.Height);

   
            return dto;
        }
    }
}
