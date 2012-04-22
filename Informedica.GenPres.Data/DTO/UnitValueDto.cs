using Informedica.GenPres.Business.Domain.Units;
using Informedica.GenPres.Data.Visibility;

namespace Informedica.GenPres.Data.DTO
{
    public class UnitValueDto : IVisibility
    {
        public decimal value { get; set; }
        public string unit { get; set; }
        public bool canBeSet { get; set; }
        public string totalUnit { get; set; }
        public string timeUnit { get; set; }
        public string adjustUnit { get; set; }
        public string state { get; set; }
        public bool changedByUser { get; set; }
        public bool visible { get; set; }

        public static UnitValueDto AssembleUnitValueDto(UnitValue unitValue)
        {
            var unitValueDto = new UnitValueDto();
            if (unitValue == null) unitValue = UnitValue.NewUnitValue();

            unitValueDto.value = unitValue.Value;
            unitValueDto.unit = unitValue.Unit;

            unitValueDto.adjustUnit = unitValue.Adjust;
            unitValueDto.timeUnit = unitValue.Time;
            unitValueDto.totalUnit = unitValue.Total;
            unitValueDto.state = unitValue.UIState;
            
            return unitValueDto;
        }

        public static UnitValue AssembleUnitValue(UnitValue unitValue, UnitValueDto unitValueDto)
        {
            if(unitValue == null) unitValue = UnitValue.NewUnitValue();
            if (unitValueDto == null) unitValueDto = new UnitValueDto();
            unitValue.Value = unitValueDto.value;
            unitValue.Unit = unitValueDto.unit;

            unitValue.ChangedByUser = unitValueDto.changedByUser;
            unitValue.Adjust = unitValueDto.adjustUnit;
            unitValue.Time = unitValueDto.timeUnit;
            unitValue.Total = unitValueDto.totalUnit;

            unitValue.UIState = unitValueDto.state;
            return unitValue;
        }   
    }
}
