using System;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Data.DTO.Prescriptions
{
    public class PrescriptionAssembler
    {
        public static Prescription AssemblePrescriptionBo(PrescriptionDto prescriptionDto)
        {
            var prescription = Prescription.NewPrescription();

            DateTime dt;
            if(DateTime.TryParse(prescriptionDto.startDate, out dt))
            {
                prescription.StartDate = dt;
            }
             
            prescription.Drug.Generic = prescriptionDto.drugGeneric;
            prescription.Drug.Route = prescriptionDto.drugRoute;
            prescription.Drug.Shape = prescriptionDto.drugShape;
            
            prescription.PID = prescriptionDto.PID;
            
            prescriptionDto.substanceQuantity = new UnitValueDto();
            prescription.Drug.Components[0].Substances[0].Quantity =
                UnitValueDto.AssembleUnitValue(prescription.Drug.Components[0].Substances[0].Quantity, prescriptionDto.substanceQuantity);
            
            return prescription;
        }

        public static PrescriptionDto AssemblePrescriptionDto(Prescription prescription)
        {
            var prescriptionDto = new PrescriptionDto();
            prescriptionDto.startDate = prescription.StartDate.ToString();
            prescriptionDto.Id = prescription.Id.ToString();

            prescriptionDto.drugGeneric = prescription.Drug.Generic;
            prescriptionDto.drugRoute = prescription.Drug.Route;
            prescriptionDto.drugShape = prescription.Drug.Shape;

            prescriptionDto.substanceQuantity =
                UnitValueDto.AssembleUnitValueDto(prescription.Drug.Components[0].Substances[0].Quantity);

            prescriptionDto.PID = prescription.PID;
            return prescriptionDto;
        }
    }
}
