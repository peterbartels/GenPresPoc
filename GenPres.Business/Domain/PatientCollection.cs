using System.Collections.Generic;
using System.Collections.ObjectModel;
using GenPres.Business.Data.DataAccess.Repository;
using GenPres.Business.ServiceProvider;
using GenPres.Business.Aspect;

namespace GenPres.Business.Domain
{
    public class PatientCollection : IPatientCollection
    {
        private static IPatientRepository Repository
        {
            get { return DalServiceProvider.Instance.Resolve<IPatientRepository>(); }
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
