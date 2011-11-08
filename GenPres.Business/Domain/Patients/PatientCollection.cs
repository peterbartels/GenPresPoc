using System.Collections.Generic;
using System.Collections.ObjectModel;
using GenPres.Business.Data.IRepositories;

namespace GenPres.Business.Domain.Patients
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
