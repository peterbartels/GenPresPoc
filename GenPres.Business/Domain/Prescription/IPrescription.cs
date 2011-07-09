using System;

namespace GenPres.Business.Domain.Prescription
{
    public interface IPrescription
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        DateTime CreationDate { get; set; }
        IDrug Drug { get; set; }
        string PID { get; set; }
    }
}
