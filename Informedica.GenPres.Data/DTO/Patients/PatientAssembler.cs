using Informedica.GenPres.Business.Domain.Patients;

namespace Informedica.GenPres.Data.DTO.Patients
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
