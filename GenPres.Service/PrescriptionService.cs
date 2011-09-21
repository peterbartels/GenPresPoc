using System;
using System.Linq;
using GenPres.Business.Allowance;
using GenPres.Business.Calculation;
using GenPres.Business.Data.IRepositories;
using GenPres.Business.Domain.Prescriptions;
using System.Collections.ObjectModel;
using GenPres.Business.Verbalization;
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
            
            PrescriptionAllowance.Determine(prescription);
            
            var pc = new PrescriptionCalculator(prescription);
            pc.Start();
            
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
        public static PrescriptionDto GetPrescriptionById(string id)
        {
            return PrescriptionAssembler.AssemblePrescriptionDto(Repository.GetPrescriptionById(Guid.Parse(id)));
        }

        public static ReadOnlyCollection<SelectionItem> GetSubstanceUnits(string generic, string route, string shape)
        {
            var genFormService = new GenFormService();
            return genFormService.GetSubstanceUnits(generic, route, shape);
        }

        public static ReadOnlyCollection<SelectionItem> GetComponentUnits(string generic, string route, string shape)
        {
            var genFormService = new GenFormService();
            return genFormService.GetComponentUnits(generic, route, shape);
        }
    }
}
