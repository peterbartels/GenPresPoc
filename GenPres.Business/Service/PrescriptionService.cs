using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Data;
using GenPres.Business.Domain;

namespace GenPres.Business.Service
{
    public static class PrescriptionService
    {
        public static NewPrescriptionDto NewPrescription()
        {
            IPrescription prescription = Prescription.NewPrescription();
            throw new NotImplementedException();
        }
    }
}
