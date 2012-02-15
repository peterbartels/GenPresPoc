using GenPres.Business.Domain.Patients;

namespace GenPres.Data.DTO.Patients
{
    public class LogicalUnitDtoAssembler
    {
        public static LogicalUnitDto AssembleDto(LogicalUnit logicalUnit)
        {
            LogicalUnitDto logicalUnitDto = new LogicalUnitDto();
            logicalUnitDto.id = logicalUnit.Id;
            logicalUnitDto.text = logicalUnit.Name;
            logicalUnitDto.leaf = true;

            return logicalUnitDto;
        }
    }
}
