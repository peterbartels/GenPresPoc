using GenPres.Business.Domain;

namespace GenPres.Business.Data.Client
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
