using System.Collections.Generic;
using System.Collections.ObjectModel;
using GenPres.Business.Data.IRepositories;

namespace GenPres.Business.Domain.Patients
{
    public class PatientCollection
    {
        private static IPdsmRepository Repository
        {
            get { return StructureMap.ObjectFactory.GetInstance<IPdsmRepository>(); }
        }

        private List<Patient> _patients = new List<Patient>();

        public static ReadOnlyCollection<Patient> GetPatientsByLogicalUnit(int logicalUnitId)
        {
            var pc = NewPatientCollection();
            pc._patients = Repository.GetPatientsByLogicalUnitId(logicalUnitId);
            return pc._patients.AsReadOnly();
        }

        public static PatientCollection NewPatientCollection()
        {
            return new PatientCollection();
        }
    }
}
