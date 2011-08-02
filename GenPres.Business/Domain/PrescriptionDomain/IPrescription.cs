using System;
using GenPres.Business.Domain.UnitDomain;

namespace GenPres.Business.Domain.PrescriptionDomain
{
    public interface IPrescription : ISavable
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        DateTime CreationDate { get; set; }
        IDrug Drug { get; set; }
        string PID { get; set; }
        UnitValue Frequency { get; set; }
        UnitValue Quantity { get; set; }
        UnitValue Total { get; set; }
        UnitValue Rate { get; set; }
        void Save(string patientId);
    }
}

