using System;
using System.Linq;
using System.Collections.ObjectModel;
using Informedica.GenPres.Business.Calculation;
using Informedica.GenPres.Business.Data.IRepositories;
using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Business.Exceptions;
using Informedica.GenPres.Business.WebService;
using Informedica.GenPres.Data.DTO.Prescriptions;
using StructureMap;

namespace Informedica.GenPres.Service
{
    public static class PrescriptionService
    {
        
        private static IPrescriptionRepository Repository
        {
            get
            {
                return ObjectFactory.GetInstance<IPrescriptionRepository>();;
            }
        }

        private static IGenFormWebServices GenFormWebServices
        {
            get
            {
                return ObjectFactory.GetInstance<IGenFormWebServices>(); ;
            }
        }

        public static PrescriptionDto UpdatePrescription(PrescriptionDto prescriptionDto, string patientId)
        {
            var prescription = PrescriptionAssembler.AssemblePrescriptionBo(prescriptionDto);

            var pc = new PrescriptionCalculator(prescription);
            pc.Calculate();
            
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
            if (patientId == "") 
                throw new InvalidIdException();

            var prescriptions = Repository.GetPrescriptionsByPatientId(patientId);

            var prescriptionDtos = new PrescriptionDto[prescriptions.Length];
            
            for (var i = 0; i < prescriptions.Length; i++)
                prescriptionDtos[i] = PrescriptionAssembler.AssemblePrescriptionDto(prescriptions[i]);

            return prescriptionDtos.ToList().AsReadOnly();
        }
        public static PrescriptionDto GetPrescriptionById(string id)
        {
            var guid = new Guid();
            
            if (id == Guid.Empty.ToString() || !Guid.TryParse(id, out guid)) 
                throw new InvalidIdException();

            var prescription = Repository.GetPrescriptionById(guid);
            if(prescription == null) throw new UnknownIdException();
            return PrescriptionAssembler.AssemblePrescriptionDto(prescription);
        }

        public static ReadOnlyCollection<SelectionItem> GetSubstanceUnits(string generic, string route, string shape)
        {
            return GenFormWebServices.GetSubstanceUnits(generic, route, shape);
        }

        public static ReadOnlyCollection<SelectionItem> GetComponentUnits(string generic, string route, string shape)
        {
            return GenFormWebServices.GetComponentUnits(generic, route, shape);
        }
    }
}
