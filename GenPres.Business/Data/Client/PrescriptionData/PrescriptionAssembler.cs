using GenPres.Business.Domain.PrescriptionDomain;

namespace GenPres.Business.Data.Client.PrescriptionData
{
    public class PrescriptionAssembler
    {
        public static Prescription AssemblePrescriptionBo(PrescriptionDto prescriptionDto)
        {
            var prescription = Prescription.NewPrescription();
            prescription.Id = prescriptionDto.Id;

            var drug = prescription.Drug;
            if (drug != null)
            {
                drug.Generic = prescriptionDto.drugGeneric;
                drug.Route = prescriptionDto.drugRoute;
                drug.Shape = prescriptionDto.drugShape;
            }
            prescription.PID = prescriptionDto.PID;
            return prescription;
        }

        public static PrescriptionDto AssemblePrescriptionDto(IPrescription prescription)
        {
            var prescriptionDto = new PrescriptionDto();
            prescriptionDto.StartDate = prescription.StartDate.ToString();
            prescriptionDto.Id = prescription.Id;

            var drug = prescription.Drug;
            if(drug!=null)
            {
                prescriptionDto.drugGeneric = drug.Generic;
                prescriptionDto.drugRoute = drug.Route;
                prescriptionDto.drugShape = drug.Shape;    
            }

            prescriptionDto.PID = prescription.PID;
            return prescriptionDto;
        }
    }
}
