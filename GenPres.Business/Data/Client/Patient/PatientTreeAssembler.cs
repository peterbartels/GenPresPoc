using System.Collections.Generic;
using System.Collections.ObjectModel;
using GenPres.Business.Domain;
using GenPres.Business.Domain.PatientDomain;

namespace GenPres.Business.Data.Client.Patient
{
    public class PatientTreeAssembler
    {
        public static ReadOnlyCollection<PatientTreeDto> AssemblePatientTreeDto(ReadOnlyCollection<IPatient> patients)
        {
            var patientTreeDtos = new List<PatientTreeDto>();
            for (int i = 0; i < patients.Count; i++)
            {
                var dto = new PatientTreeDto();
                dto.id = patients[i].Id;
                dto.PID = patients[i].PID;
                dto.FirstName = patients[i].FirstName;
                dto.LastName = patients[i].LastName;
                dto.text = patients[i].FullName;

                dto.Weight = patients[i].Weight;
                dto.Length = patients[i].Length;
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
