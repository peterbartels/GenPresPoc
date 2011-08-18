﻿using GenPres.Business.Domain.Units;

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
            if (unitValue == null) unitValue = UnitValue.NewUnitValue(false);
            unitValueDto.value = unitValue.Value;
            unitValueDto.unit = unitValue.Unit;
            unitValueDto.canBeSet = unitValue.CanBeSet;
            return unitValueDto;
        }

        public static UnitValue AssembleUnitValue(UnitValue unitValue, UnitValueDto unitValueDto)
        {
            if(unitValue == null) unitValue = UnitValue.NewUnitValue(false);
            if (unitValueDto == null) unitValueDto = new UnitValueDto();
            unitValue.Value = unitValueDto.value;
            unitValue.Unit = unitValueDto.unit;
            return unitValue;
        }   
    }
}
