using System;

namespace GenPres.Business.Domain.Patients
{
    public interface IPatient : ISavable
    {
        int Id { get; set; }

        string LastName { get; set; }
        string FirstName { get; set; }
        string FullName { get; }
        string Pid { get; set; }
        
        int LogicalUnitId { get; set; }

        decimal Length { get; set; }
        decimal Weight { get; set; }

        string Gender { get; set; }
        string Unit { get; set; }
        string Bed { get; set; }
        string Birthdate { get; set; }
        string Age { get; set; }
        DateTime RegisterDate { get; set; }
        int DaysRegistered { get; set; }

        void Save();
    }
}
