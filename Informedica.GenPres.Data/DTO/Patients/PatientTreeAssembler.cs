﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Informedica.GenPres.Business.Domain.Patients;

namespace Informedica.GenPres.Data.DTO.Patients
{
    public class PatientTreeAssembler
    {
        public static ReadOnlyCollection<PatientTreeDto> AssemblePatientTreeDto(ReadOnlyCollection<Patient> patients)
        {
            var patientTreeDtos = new List<PatientTreeDto>();
            for (int i = 0; i < patients.Count; i++)
            {
                var dto = new PatientTreeDto();
                dto.id = patients[i].Id.ToString();
                dto.PID = patients[i].Pid;
                dto.FirstName = patients[i].FirstName;
                dto.LastName = patients[i].LastName;
                dto.text = patients[i].FullName;

                dto.RegisterDate = patients[i].RegisterDate.ToString("dd-MM-yyyy");
                dto.Bed = patients[i].Bed;
                dto.Unit = patients[i].Unit;

                dto.leaf = true;
                patientTreeDtos.Add(dto);
            }
            return patientTreeDtos.AsReadOnly();
        }
    }
}
