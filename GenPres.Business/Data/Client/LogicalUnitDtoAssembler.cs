using GenPres.Business.Domain;

namespace GenPres.Business.Data.Client
{
    public class LogicalUnitDtoAssembler
    {
        public static LogicalUnitDto AssembleDto(ILogicalUnit logicalUnit)
        {
            LogicalUnitDto logicalUnitDto = new LogicalUnitDto();
            logicalUnitDto.Id = logicalUnit.Id;
            logicalUnitDto.Name = logicalUnit.Name;
            return logicalUnitDto;
        }
    }
}
