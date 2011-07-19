using System;
using System.Collections.ObjectModel;
using GenPres.Business.Data.Client.Patient;
using GenPres.Business.Domain;
using GenPres.Business.Data.Client;
using GenPres.Business.Domain.PatientDomain;

namespace GenPres.Business.Service
{
    public static class PatientService
    {
        public static LogicalUnitDto[] GetLogicalUnits()
        {
            var logicalUnits = LogicalUnit.GetLogicalUnits();
            
            var logicalUnitDtos = new LogicalUnitDto[logicalUnits.Length];

            for (int i = 0; i < logicalUnits.Length; i++)
            {
                logicalUnitDtos[i] = LogicalUnitDtoAssembler.AssembleDto(logicalUnits[i]);
            }
            return logicalUnitDtos;
        }

        public static ReadOnlyCollection<PatientTreeDto> GetPatientsByLogicalUnit(int logicalUnit)
        {
            return PatientTreeAssembler.AssemblePatientTreeDto(PatientCollection.GetPatientsByLogicalUnit(logicalUnit));
        }


    }
}
