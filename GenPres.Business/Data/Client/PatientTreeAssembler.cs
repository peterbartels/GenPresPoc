using System.Collections.Generic;
using System.Collections.ObjectModel;
using GenPres.Business.Domain;

namespace GenPres.Business.Data.Client
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
                dto.Firstname = patients[i].FirstName;
                dto.Lastname = patients[i].LastName;
                dto.text = patients[i].FullName;
                dto.leaf = true;
                patientTreeDtos.Add(dto);
            }
            return patientTreeDtos.AsReadOnly();
        }
    }
}
