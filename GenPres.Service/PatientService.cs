using System;
using System.Collections.ObjectModel;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Patients;
using GenPres.Business.Exceptions;
using GenPres.Data.DTO.Patients;
using StructureMap;

namespace GenPres.Service
{
    public static class PatientService
    {
        
        private static IPatientRepository Repository
        {
            get
            {
                return ObjectFactory.GetInstance<IPatientRepository>();
            }
        }

        private static IPdsmRepository PdmsRepository
        {
            get { return ObjectFactory.GetInstance<IPdsmRepository>(); }
        }

        public static Patient GetPatientByPid(string Pid)
        {
            if (Pid == "")
                throw new InvalidIdException();

            return Repository.GetPatientByPatientId(Pid);
        }

        public static PatientDto InsertFromPdms(string pid)
        {
            var patient = PdmsRepository.GetPatientByPid(pid);
            if (!Repository.PatientExists(pid))
            {
                Repository.Save(patient);
                return PatientAssembler.AssemblePatientDto(patient);
            }
            var dbPatient = GetPatientByPid(pid);
            dbPatient.Height = patient.Height;
            dbPatient.Weight = patient.Weight;
            Repository.Save(dbPatient);
            return PatientAssembler.AssemblePatientDto(dbPatient);
        }

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
            return PatientTreeAssembler.AssemblePatientTreeDto(Patient.GetPatientsByLogicalUnit(logicalUnit));
        }

        public static PatientDto SavePatient(string patientId)
        {
            return InsertFromPdms(patientId);
        }
    }
}
