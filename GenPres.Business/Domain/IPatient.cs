using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.Domain
{
    public interface IPatient
    {
        int Id { get; set; }

        string LastName { get; set; }
        string FirstName { get; set; }
        string FullName { get; }
        string PID { get; set; }
        
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


    }
}
