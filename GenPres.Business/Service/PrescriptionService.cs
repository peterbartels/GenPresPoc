using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Data;
using GenPres.Business.Data.Client.PrescriptionData;
using GenPres.Business.DataAccess.Client;
using GenPres.Business.Domain;
using GenPres.Business.Domain.PrescriptionDomain;
using System.Collections.ObjectModel;

namespace GenPres.Business.Service
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
    }
}
