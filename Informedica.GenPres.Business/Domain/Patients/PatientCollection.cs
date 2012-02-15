using System.Collections.Generic;

namespace Informedica.GenPres.Business.Domain.Patients
{
    public class PatientCollection
    {
       
        private List<Patient> _patients = new List<Patient>();

        public static PatientCollection NewPatientCollection()
        {
            return new PatientCollection();
        }
    }
}
