using System;
using System.Linq;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Prescriptions;
using System.Collections.ObjectModel;
using GenPres.Business.WebService;
using GenPres.Data.DTO.GenForm;
using GenPres.Data.DTO.Prescriptions;
using StructureMap;

namespace GenPres.Service
{
    public static class PrescriptionService
    {
        public static void NewPrescription()
        {
            throw new NotImplementedException();
        }

        private static IPrescriptionRepository _prescriptionRepository;

        private static IPrescriptionRepository Repository
        {
            get
            {
                if (_prescriptionRepository == null) _prescriptionRepository = ObjectFactory.GetInstance<IPrescriptionRepository>();
                return _prescriptionRepository;
            }
        }

        public static PrescriptionDto UpdatePrescription(PrescriptionDto prescriptionDto, string patientId)
        {
            var prescription = PrescriptionAssembler.AssemblePrescriptionBo(prescriptionDto);
            //TEMPWEG prescription.Save(patientId);
            return PrescriptionAssembler.AssemblePrescriptionDto(prescription);
        }

        public static PrescriptionDto ClearPrescription()
        {
            return PrescriptionAssembler.AssemblePrescriptionDto(Prescription.NewPrescription());
        }

        public static PrescriptionDto SavePrescription(PrescriptionDto prescriptionDto, string patientId)
        {
            var prescription = PrescriptionAssembler.AssemblePrescriptionBo(prescriptionDto);
            Repository.SavePrescription(prescription, patientId);
            return PrescriptionAssembler.AssemblePrescriptionDto(prescription);
        }

        public static ReadOnlyCollection<PrescriptionDto> GetPrescriptions(string patientId)
        {
            var prescriptions = Repository.GetPrescriptionsByPatientId(patientId);

            var prescriptionDtos = new PrescriptionDto[prescriptions.Length];
            
            for (var i = 0; i < prescriptions.Length; i++)
                prescriptionDtos[i] = PrescriptionAssembler.AssemblePrescriptionDto(prescriptions[i]);

            return prescriptionDtos.ToList().AsReadOnly();
        }
        public static PrescriptionDto GetPrescriptionById(int id)
        {
            //TEMPWEG return PrescriptionAssembler.AssemblePrescriptionDto(Prescription.GetPrescriptionById(id));
            return PrescriptionAssembler.AssemblePrescriptionDto(Prescription.NewPrescription());
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
