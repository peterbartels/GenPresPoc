using GenPres.Business.Domain.Units;

namespace GenPres.Data.DTO
{
    public class UnitValueDto
    {
        public decimal value { get; set; }
        public string unit { get; set; }
        public bool canBeSet { get; set; }

        public static UnitValueDto AssembleUnitValueDto(UnitValue unitValue)
        {
            var unitValueDto = new UnitValueDto();
            unitValueDto.value = unitValue.Value;
            unitValueDto.unit = unitValue.Unit;
            unitValueDto.canBeSet = unitValue.CanBeSet;
            return unitValueDto;
        }

        public static UnitValue AssembleUnitValue(UnitValue unitValue, UnitValueDto unitValueDto)
        {
            unitValue.Value = unitValueDto.value;
            unitValue.Unit = unitValueDto.unit;
            return unitValue;
        }   
    }
}
