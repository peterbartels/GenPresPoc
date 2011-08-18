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

            prescription.Continuous = prescriptionDto.continuous;
            prescription.Continuous = prescriptionDto.continuous;
            prescription.Continuous = prescriptionDto.continuous;

            prescription.Drug.Quantity = UnitValueDto.AssembleUnitValue(prescription.Drug.Quantity, prescriptionDto.drugQuantity);

            prescription.Quantity = UnitValueDto.AssembleUnitValue(prescription.Quantity, prescriptionDto.prescriptionQuantity);
            prescription.Rate = UnitValueDto.AssembleUnitValue(prescription.Rate, prescriptionDto.prescriptionRate);
            prescription.Total = UnitValueDto.AssembleUnitValue(prescription.Total, prescriptionDto.prescriptionTotal);

            prescription.Doses[0].Quantity = UnitValueDto.AssembleUnitValue(prescription.Doses[0].Quantity, prescriptionDto.doseQuantity);
            prescription.Doses[0].Total = UnitValueDto.AssembleUnitValue(prescription.Doses[0].Total, prescriptionDto.doseTotal);
            prescription.Doses[0].Rate = UnitValueDto.AssembleUnitValue(prescription.Doses[0].Rate, prescriptionDto.doseRate);
            
            prescription.Drug.Components[0].Substances[0].Quantity = UnitValueDto.AssembleUnitValue(prescription.Drug.Components[0].Substances[0].Quantity, prescriptionDto.substanceQuantity);
            prescription.Drug.Components[0].Substances[0].DrugConcentration = UnitValueDto.AssembleUnitValue(prescription.Drug.Components[0].Substances[0].DrugConcentration, prescriptionDto.substanceDrugConcentration);
            
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

            prescriptionDto.substanceQuantity = UnitValueDto.AssembleUnitValueDto(prescription.Drug.Components[0].Substances[0].Quantity);
            prescriptionDto.substanceDrugConcentration = UnitValueDto.AssembleUnitValueDto(prescription.Drug.Components[0].Substances[0].DrugConcentration);

            prescriptionDto.prescriptionQuantity = UnitValueDto.AssembleUnitValueDto(prescription.Quantity);
            prescriptionDto.prescriptionTotal = UnitValueDto.AssembleUnitValueDto(prescription.Total);
            prescriptionDto.prescriptionRate = UnitValueDto.AssembleUnitValueDto(prescription.Rate);

            prescriptionDto.drugQuantity = UnitValueDto.AssembleUnitValueDto(prescription.Drug.Quantity);

            prescriptionDto.doseQuantity = UnitValueDto.AssembleUnitValueDto(prescription.Doses[0].Quantity);
            prescriptionDto.doseTotal = UnitValueDto.AssembleUnitValueDto(prescription.Doses[0].Total);
            prescriptionDto.doseRate = UnitValueDto.AssembleUnitValueDto(prescription.Doses[0].Rate);

            prescriptionDto.PID = prescription.PID;
            
            return prescriptionDto;
        }
    }
}
