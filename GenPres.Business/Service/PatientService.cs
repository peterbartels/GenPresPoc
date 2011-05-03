using GenPres.Business.Domain;
using GenPres.Business.Data.Client;

namespace GenPres.Business.Service
{
    public static class PatientService
    {
        public static LogicalUnitDto[] GetLogicalUnits()
        {
            ILogicalUnit[] logicalUnits = LogicalUnit.GetLogicalUnits();
            LogicalUnitDto[] logicalUnitDtos = new LogicalUnitDto[logicalUnits.Length];
            for (int i = 0; i < logicalUnits.Length; i++)
            {
                logicalUnitDtos[i] = LogicalUnitDtoAssembler.AssembleDto(logicalUnits[i]);
            }
            return logicalUnitDtos;
        }
    }
}
