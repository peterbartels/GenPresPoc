using GenPres.Business.Domain.Prescription;

namespace GenPres.Business.Data.Client.PrescriptionData
{
    public class PrescriptionAssembler
    {
        public static Prescription AssemblePrescriptionBo(PrescriptionDto prescriptionDto)
        {
            var prescription = Prescription.NewPrescription();
            //prescription.StartDate = prescriptionDto.StartDate;
            var drug = prescription.Drug;
            drug.Generic = prescriptionDto.drugGeneric;
            drug.Route = prescriptionDto.drugRoute;
            drug.Shape = prescriptionDto.drugShape;
            return prescription;
        }

        public static PrescriptionDto AssemblePrescriptionDto(IPrescription prescription)
        {
            var prescriptionDto = new PrescriptionDto();
            prescriptionDto.StartDate = prescription.StartDate.ToString();
            var drug = prescription.Drug;
            prescriptionDto.drugGeneric = drug.Generic;
            prescriptionDto.drugRoute = drug.Route;
            prescriptionDto.drugShape = drug.Shape;
            return prescriptionDto;
        }
    }
}
