using System;
using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Business.Domain.Units;

namespace Informedica.GenPres.Data.DTO.Prescriptions
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

            prescription.Frequency = UnitValueDto.AssembleUnitValue(prescription.Frequency, prescriptionDto.prescriptionFrequency);
            prescription.Duration = UnitValueDto.AssembleUnitValue(prescription.Duration, prescriptionDto.prescriptionDuration);

            prescription.Continuous = (prescriptionDto.prescriptionContinuous == "on");
            prescription.OnRequest = (prescriptionDto.prescriptionOnrequest == "on");
            prescription.Solution = (prescriptionDto.prescriptionSolution == "on");
            prescription.Infusion = (prescriptionDto.prescriptionInfusion == "on");

            prescription.Drug.Quantity = UnitValueDto.AssembleUnitValue(prescription.Drug.Quantity, prescriptionDto.drugQuantity);

            prescription.Quantity = UnitValueDto.AssembleUnitValue(prescription.Quantity, prescriptionDto.adminQuantity);
            prescription.Rate = UnitValueDto.AssembleUnitValue(prescription.Rate, prescriptionDto.adminRate);
            prescription.Total = UnitValueDto.AssembleUnitValue(prescription.Total, prescriptionDto.adminTotal);

            prescription.Doses[0].Quantity = UnitValueDto.AssembleUnitValue(prescription.Doses[0].Quantity, prescriptionDto.doseQuantity);
            prescription.Doses[0].Total = UnitValueDto.AssembleUnitValue(prescription.Doses[0].Total, prescriptionDto.doseTotal);
            prescription.Doses[0].Rate = UnitValueDto.AssembleUnitValue(prescription.Doses[0].Rate, prescriptionDto.doseRate);
            
            prescription.Drug.Components[0].Substances[0].Quantity = UnitValueDto.AssembleUnitValue(prescription.Drug.Components[0].Substances[0].Quantity, prescriptionDto.substanceQuantity);
            prescription.Drug.Components[0].Substances[0].DrugConcentration = UnitValueDto.AssembleUnitValue(prescription.Drug.Components[0].Substances[0].DrugConcentration, prescriptionDto.substanceDrugConcentration);

            var weightUnitValue = UnitValueDto.AssembleUnitValue(null, prescriptionDto.patientWeight);
            prescription.PatientWeightUnit = weightUnitValue.Unit;
            prescription.PatientWeight = weightUnitValue.BaseValue;

            var lengthUnitValue = UnitValueDto.AssembleUnitValue(null, prescriptionDto.patientLength);
            prescription.PatientLengthUnit = lengthUnitValue.Unit;
            prescription.PatientLength = lengthUnitValue.BaseValue;

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

            prescriptionDto.prescriptionFrequency = UnitValueDto.AssembleUnitValueDto(prescription.Frequency);
            prescriptionDto.prescriptionDuration = UnitValueDto.AssembleUnitValueDto(prescription.Duration);

            prescriptionDto.substanceQuantity = UnitValueDto.AssembleUnitValueDto(prescription.Drug.Components[0].Substances[0].Quantity);
            prescriptionDto.substanceDrugConcentration = UnitValueDto.AssembleUnitValueDto(prescription.Drug.Components[0].Substances[0].DrugConcentration);

            prescriptionDto.adminQuantity = UnitValueDto.AssembleUnitValueDto(prescription.Quantity);
            prescriptionDto.adminTotal = UnitValueDto.AssembleUnitValueDto(prescription.Total);
            prescriptionDto.adminRate = UnitValueDto.AssembleUnitValueDto(prescription.Rate);

            prescriptionDto.drugQuantity = UnitValueDto.AssembleUnitValueDto(prescription.Drug.Quantity);

            prescriptionDto.doseQuantity = UnitValueDto.AssembleUnitValueDto(prescription.Doses[0].Quantity);
            prescriptionDto.doseTotal = UnitValueDto.AssembleUnitValueDto(prescription.Doses[0].Total);
            prescriptionDto.doseRate = UnitValueDto.AssembleUnitValueDto(prescription.Doses[0].Rate);

            var weightUnitValue = UnitValue.NewUnitValue(true);
            weightUnitValue.Unit = prescription.PatientWeightUnit;
            weightUnitValue.BaseValue = prescription.PatientWeight;
            prescriptionDto.patientWeight = UnitValueDto.AssembleUnitValueDto(weightUnitValue);

            var lengthUnitValue = UnitValue.NewUnitValue(true);
            lengthUnitValue.Unit = prescription.PatientLengthUnit;
            lengthUnitValue.BaseValue = prescription.PatientLength;
            prescriptionDto.patientLength = UnitValueDto.AssembleUnitValueDto(lengthUnitValue);

            prescriptionDto.PID = prescription.PID;

            prescriptionDto.verbalization = "";// PrescriptionVerbalization.Verbalize(prescription);

            prescriptionDto.AdminVolume = prescription.AdminVolume;
            prescriptionDto.DoseVolume = prescription.DoseVolume;

            var bsa = prescription.PatientBsa;
            prescriptionDto.patientBSA = new UnitValueDto()
            {
                unit = "m2",
                value = bsa,
                state = "calculated",
                canBeSet = true
            };

            return prescriptionDto;
        }
    }
}
