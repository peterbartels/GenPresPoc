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
            if(DateTime.TryParse(prescriptionDto.StartDate, out dt))
            {
                prescription.StartDate = dt;
            }
             

            /*TEMPWEG var drug = prescription.Drug;
            if (drug != null)
            {
                drug.Generic = prescriptionDto.drugGeneric;
                drug.Route = prescriptionDto.drugRoute;
                drug.Shape = prescriptionDto.drugShape;
            }
            prescription.PID = prescriptionDto.PID;
             * */
            return prescription;
        }

        public static PrescriptionDto AssemblePrescriptionDto(Prescription prescription)
        {
            var prescriptionDto = new PrescriptionDto();
            prescriptionDto.StartDate = prescription.StartDate.ToString();
            prescriptionDto.Id = prescription.Id.ToString();

            /*TEMPWEG var drug = prescription.Drug;
            if(drug!=null)
            {
                prescriptionDto.drugGeneric = drug.Generic;
                prescriptionDto.drugRoute = drug.Route;
                prescriptionDto.drugShape = drug.Shape;    
            }*/

            //TEMPWEG prescriptionDto.PID = prescription.PID;
            return prescriptionDto;
        }
    }
}
