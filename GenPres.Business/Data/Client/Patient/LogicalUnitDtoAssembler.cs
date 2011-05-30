using GenPres.Business.Domain;
using GenPres.Business.Domain.Patient;

namespace GenPres.Business.Data.Client.Patient
{
    public class LogicalUnitDtoAssembler
    {
        public static LogicalUnitDto AssembleDto(ILogicalUnit logicalUnit)
        {
            LogicalUnitDto logicalUnitDto = new LogicalUnitDto();
            logicalUnitDto.id = logicalUnit.Id;
            logicalUnitDto.text = logicalUnit.Name;
            logicalUnitDto.leaf = true;

            return logicalUnitDto;
        }
    }
}
