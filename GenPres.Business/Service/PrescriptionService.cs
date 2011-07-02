using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Data;
using GenPres.Business.Data.Client.PrescriptionData;
using GenPres.Business.DataAccess.Client;
using GenPres.Business.Domain;
using GenPres.Business.Domain.Prescription;
using System.Collections.ObjectModel;

namespace GenPres.Business.Service
{
    public static class PrescriptionService
    {
        public static void NewPrescription()
        {
            throw new NotImplementedException();
        }

        public static void SavePrescription(PrescriptionDto prescriptionDto)
        {
            var prescription = PrescriptionAssembler.AssemblePrescriptionBo(prescriptionDto);
            prescription.Save();
        }

        public static ReadOnlyCollection<PrescriptionDto> GetPrescriptions()
        {
            var prescriptions = Prescription.GetPrescriptions();
            var prescriptionDtos = new PrescriptionDto[prescriptions.Length];
            for (var i = 0; i < prescriptions.Length; i++)
            {
                prescriptionDtos[i] = PrescriptionAssembler.AssemblePrescriptionDto(prescriptions[i]);
            }
            return prescriptionDtos.ToList().AsReadOnly();
        }
    }
}
