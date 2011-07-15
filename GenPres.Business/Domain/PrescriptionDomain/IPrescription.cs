using System;

namespace GenPres.Business.Domain.PrescriptionDomain
{
    public interface IPrescription : ISavable
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        DateTime CreationDate { get; set; }
        Drug Drug { get; set; }
        string PID { get; set; }
    }
}
