using System;
using System.Linq;
using GenPres.Business.Domain.Prescriptions;
using System.Collections.ObjectModel;
using GenPres.Business.Domain.Prescriptions.Medicine;
using GenPres.Business.WebService;
using GenPres.Data.DTO.GenForm;
using GenPres.Data.DTO.Prescriptions;

namespace GenPres.Service
{
    public static class PrescriptionService
    {
        public static void NewPrescription()
        {
            throw new NotImplementedException();
        }

        public static PrescriptionDto SavePrescription(PrescriptionDto prescriptionDto, string patientId)
        {
            var prescription = PrescriptionAssembler.AssemblePrescriptionBo(prescriptionDto);
            prescription.Save(patientId);
            return PrescriptionAssembler.AssemblePrescriptionDto(prescription);
        }

        public static ReadOnlyCollection<PrescriptionDto> GetPrescriptions(string patientId)
        {
            var prescriptions = Prescription.GetPrescriptions(patientId);
            var prescriptionDtos = new PrescriptionDto[prescriptions.Length];
            
            for (var i = 0; i < prescriptions.Length; i++)
                prescriptionDtos[i] = PrescriptionAssembler.AssemblePrescriptionDto(prescriptions[i]);
            
            return prescriptionDtos.ToList().AsReadOnly();
        }
        public static PrescriptionDto GetPrescriptionById(int id)
        {
            return PrescriptionAssembler.AssemblePrescriptionDto(Prescription.GetPrescriptionById(id));
        }

        public static ReadOnlyCollection<ValueDto> GetSubstanceUnits(string generic, string shape, string route)
        {
            var genFormService = new GenFormService();
            var units = genFormService.GetSubstanceUnits(generic, shape, route);
            return ValueListAssembler.AssembleValueListDto(units);
        }

        public static ReadOnlyCollection<ValueDto> GetComponentUnits(string generic, string shape, string route)
        {
            var genFormService = new GenFormService();
            var units = genFormService.GetComponentUnits(generic, shape, route);
            return ValueListAssembler.AssembleValueListDto(units);
        }
    }
}
