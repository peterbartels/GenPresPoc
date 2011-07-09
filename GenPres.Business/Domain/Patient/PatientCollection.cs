using System.Collections.Generic;
using System.Collections.ObjectModel;
using GenPres.Business.Data.DataAccess.Repositories;
using GenPres.Business.ServiceProvider;

namespace GenPres.Business.Domain.Patient
{
    public class PatientCollection : IPatientCollection
    {
        private static IPdsmRepository Repository
        {
            get { return DalServiceProvider.Instance.Resolve<IPdsmRepository>(); }
        }

        private List<IPatient> _patients = new List<IPatient>();

        public static ReadOnlyCollection<IPatient> GetPatientsByLogicalUnit(int logicalUnitId)
        {
            var pc = PatientCollection.NewPatientCollection();
            pc._patients = Repository.GetPatientsByLogicalUnitId(logicalUnitId);
            return pc._patients.AsReadOnly();
        }

        public static PatientCollection NewPatientCollection()
        {
            return new PatientCollection();
        }
    }
}
