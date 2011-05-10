using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.Domain
{
    public interface IPatient
    {
        string LastName { get; set; }
        string FirstName { get; set; }
        string FullName { get; }
        string PID { get; set; }
        int Id { get; set; }
        int LogicalUnitId { get; set; }
    }
}
