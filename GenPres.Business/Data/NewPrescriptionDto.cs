using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.Data
{
    public class NewPrescriptionDto : IDto
    {
        public string DrugName { get; set; }
        public string DrugShape { get; set; }
        public string DrugRoute { get; set; }

        public bool PrescriptionSolution { get; set; }
        public bool PrescriptionOnrequest { get; set; }
        public bool PrescriptionContinuous { get; set; }
        public bool PrescriptionInfusion { get; set; }
        public string PrescriptionRemarks { get; set; }

        public int PrescriptionId { get; set; }

        public string PrescriptionStartDate { get; set; }
        public string PrescriptionEndDate { get; set; }

        public string PrescriptionStartTime { get; set; }
        public string PrescriptionEndTime { get; set; }

        public UnitValueDto PrescriptionAdjustWeight { get; set; }
        public UnitValueDto PrescriptionAdjustLength { get; set; }
        public UnitValueDto PrescriptionAdjustBSA { get; set; }

        public UnitValueDto DoseQuantity { get; set; }
        public UnitValueDto DoseTotal { get; set; }
        public UnitValueDto DoseRate { get; set; }
        public UnitValueDto PrescriptionFrequency { get; set; }
        public UnitValueDto PrescriptionTime { get; set; }
        public UnitValueDto AdminQuantity { get; set; }
        public UnitValueDto AdminTotal { get; set; }
        public UnitValueDto AdminRate { get; set; }
        public UnitValueDto SubstanceQuantity { get; set; }
        public UnitValueDto SubstanceDrugConcentration { get; set; }
        public UnitValueDto DrugQuantity { get; set; }

        public System.Collections.Hashtable Totals { get; set; }
    }
}
