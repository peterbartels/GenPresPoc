using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Csla;
using Csla.Data;
using Csla.Security;
using GenPres.Database;

namespace GenPres.Business
{
    [Serializable]
    public class Patient : DataBusinessBase<Patient>
    {
        public override object GetDataAccessObject()
        {
            if (_cachedAccessObject == null) _cachedAccessObject = new GenPres.Database.Patient();
            return _cachedAccessObject;
        }

        internal static PropertyInfo<int> IDProperty = RegisterProperty(typeof(Patient), new PropertyInfo<int>("id"));
        public int id
        {
            get
            {
                int value = GetProperty(IDProperty);
                return value;
            }
            set
            {
                SetProperty(IDProperty, value);
            }
        }

        internal static PropertyInfo<string> FirstNameProperty = RegisterProperty(typeof(Patient), new PropertyInfo<string>("FirstName"));
        public string FirstName
        {
            get
            {
                string value = GetProperty(FirstNameProperty);
                return value;
            }
            set
            {
                SetProperty(FirstNameProperty, value);
            }
        }

        internal static PropertyInfo<decimal> LengthProperty = RegisterProperty(typeof(Patient), new PropertyInfo<decimal>("Length"));
        public decimal Length
        {
            get
            {
                decimal value = GetProperty(LengthProperty);
                return value;
            }
            set
            {
                SetProperty(LengthProperty, value);
            }
        }

        internal static PropertyInfo<decimal> WeightProperty = RegisterProperty(typeof(Patient), new PropertyInfo<decimal>("Weight"));
        public decimal Weight
        {
            get
            {
                decimal value = GetProperty(WeightProperty);
                return value;
            }
            set
            {
                SetProperty(WeightProperty, value);
            }
        }

        internal static PropertyInfo<string> LastNameProperty = RegisterProperty(typeof(Patient), new PropertyInfo<string>("LastName"));
        public string LastName
        {
            get
            {
                string value = GetProperty(LastNameProperty);
                return value;
            }
            set
            {
                SetProperty(LastNameProperty, value);
            }
        }


        internal static PropertyInfo<string> GenderProperty = RegisterProperty(typeof(Patient), new PropertyInfo<string>("Gender"));
        public string Gender
        {
            get
            {
                string value = GetProperty(GenderProperty);
                return value;
            }
            set
            {
                SetProperty(GenderProperty, value);
            }
        }

        internal static PropertyInfo<string> UnitProperty = RegisterProperty(typeof(Patient), new PropertyInfo<string>("Unit"));
        public string Unit
        {
            get
            {
                string value = GetProperty(UnitProperty);
                return value;
            }
            set
            {
                SetProperty(UnitProperty, value);
            }
        }


        internal static PropertyInfo<string> BedProperty = RegisterProperty(typeof(Patient), new PropertyInfo<string>("Bed"));
        public string Bed
        {
            get
            {
                string value = GetProperty(BedProperty);
                return value;
            }
            set
            {
                SetProperty(BedProperty, value);
            }
        }


        internal static PropertyInfo<string> BirthdateProperty = RegisterProperty(typeof(Patient), new PropertyInfo<string>("Birthdate"));
        public string Birthdate
        {
            get
            {
                string value = GetProperty(BirthdateProperty);
                return value;
            }
            set
            {
                SetProperty(BirthdateProperty, value);
            }
        }


        internal static PropertyInfo<string> AgeProperty = RegisterProperty(typeof(Patient), new PropertyInfo<string>("Age"));
        public string Age
        {
            get
            {
                string value = GetProperty(AgeProperty);
                return value;
            }
            set
            {
                SetProperty(AgeProperty, value);
            }
        }

        internal static PropertyInfo<string> MedicationWeightProperty = RegisterProperty(typeof(Patient), new PropertyInfo<string>("MedicationWeight"));
        public string MedicationWeight
        {
            get
            {
                string value = GetProperty(MedicationWeightProperty);
                return value;
            }
            set
            {
                SetProperty(MedicationWeightProperty, value);
            }
        }

        internal static PropertyInfo<string> RegisterWeightProperty = RegisterProperty(typeof(Patient), new PropertyInfo<string>("RegisterWeight"));
        public string RegisterWeight
        {
            get
            {
                string value = GetProperty(RegisterWeightProperty);
                return value;
            }
            set
            {
                SetProperty(RegisterWeightProperty, value);
            }
        }

        internal static PropertyInfo<string> GuessedWeightProperty = RegisterProperty(typeof(Patient), new PropertyInfo<string>("GuessedWeight"));
        public string GuessedWeight
        {
            get
            {
                string value = GetProperty(GuessedWeightProperty);
                return value;
            }
            set
            {
                SetProperty(GuessedWeightProperty, value);
            }
        }

        internal static PropertyInfo<string> BirthWeightProperty = RegisterProperty(typeof(Patient), new PropertyInfo<string>("BirthWeight"));
        public string BirthWeight
        {
            get
            {
                string value = GetProperty(BirthWeightProperty);
                return value;
            }
            set
            {
                SetProperty(BirthWeightProperty, value);
            }
        }

        internal static PropertyInfo<string> CurrentWeightProperty = RegisterProperty(typeof(Patient), new PropertyInfo<string>("CurrentWeight"));
        public string CurrentWeight
        {
            get
            {
                string value = GetProperty(CurrentWeightProperty);
                return value;
            }
            set
            {
                SetProperty(CurrentWeightProperty, value);
            }
        }

        internal static PropertyInfo<string> GuessedLengthProperty = RegisterProperty(typeof(Patient), new PropertyInfo<string>("GuessedLength"));
        public string GuessedLength
        {
            get
            {
                string value = GetProperty(GuessedLengthProperty);
                return value;
            }
            set
            {
                SetProperty(GuessedLengthProperty, value);
            }
        }

        internal static PropertyInfo<string> CurrentLengthProperty = RegisterProperty(typeof(Patient), new PropertyInfo<string>("CurrentLength"));
        public string CurrentLength
        {
            get
            {
                string value = GetProperty(CurrentLengthProperty);
                return value;
            }
            set
            {
                SetProperty(CurrentLengthProperty, value);
            }
        }

        internal static PropertyInfo<string> RegisterDateProperty = RegisterProperty(typeof(Patient), new PropertyInfo<string>("RegisterDate"));
        public string RegisterDate
        {
            get
            {
                string value = GetProperty(RegisterDateProperty);
                return value;
            }
            set
            {
                SetProperty(RegisterDateProperty, value);
            }
        }

        internal static PropertyInfo<string> DaysRegisteredProperty = RegisterProperty(typeof(Patient), new PropertyInfo<string>("DaysRegistered"));
        public string DaysRegistered
        {
            get
            {
                string value = GetProperty(DaysRegisteredProperty);
                return value;
            }
            set
            {
                SetProperty(DaysRegisteredProperty, value);
            }
        }
        
        internal static PropertyInfo<string> PIDProperty = RegisterProperty(typeof(Patient), new PropertyInfo<string>("PID"));
        public string PID
        {
            get
            {
                string value = GetProperty(PIDProperty);
                return value;
            }
            set
            {
                SetProperty(PIDProperty, value);
            }
        }

        #region DataAccess


        private void ApplyMapping(object source, object destination)
        {
            Csla.Data.DataMap mapping = new DataMap(source.GetType(), destination.GetType());
            mapping.AddPropertyMapping("PID", "PID");
            Csla.Data.DataMapper.Map(source, destination, mapping);
        }

        [RunLocal]
        protected override void DataPortal_Create()
        {
            // TODO: load default values
            // omit this override if you have no defaults to set
            base.DataPortal_Create();
        }
        /*
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var ctx = ContextManager<PrescriptionDataContext>.GetManager(
                DatabaseConnection.GetConnectionString(
                DatabaseConnection.DatabaseName.GENPRES), false))
            {
                using (BypassPropertyChecks)
                {
                    Database.Patient patientData = new Database.Patient();
                    ApplyMapping(this, patientData);
                    patientData.MedicationWeight = (double)this.Weight;
                    patientData.Height = (double)this.Length;
                    this.PatientData = patientData;
                    //DataCache.Instance.patientData = patientData;
                    ctx.DataContext.Patients.InsertOnSubmit(patientData);
                }
            }
        }*/
        /*
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var ctx = ContextManager<PrescriptionDataContext>.GetManager(
                DatabaseConnection.GetConnectionString(
                DatabaseConnection.DatabaseName.GENPRES), false))
            {
                using (BypassPropertyChecks)
                {
                    Database.Patient data = ctx.DataContext.Patients.Single<Database.Patient>(c => c.Id == this.id);
                    data.PID = this.PID;
                    data.MedicationWeight = (double)this.Weight;
                    data.Height = (double)this.Length;
                    //DataCache.Instance.patientData = data;
                    
                    ctx.DataContext.SubmitChanges();
                }
            }
        }*/
        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(new SingleCriteria<Patient, int>(this.id));
        }
        [Transactional(TransactionalTypes.TransactionScope)]
        private void DataPortal_Delete(SingleCriteria<Patient, int> criteria)
        {
        // TODO: delete values
        }
        /*

        private void DataPortal_Fetch(Database.Patient data)
        {
            this.id = data.Id;
            this.PID = data.PID;
            if(data.Height != null) this.Length = (decimal)data.Height.Value;
            if (data.MedicationWeight != null) this.Weight = (decimal)data.MedicationWeight.Value;
        }*/
        #endregion

        #region Factory Methods

        public static Patient NewPatient()
        {
            return DataPortal.Create<Patient>();
        }

        public static Patient GetPatient(object data)
        {
            return DataPortal.Fetch<Patient>(data);
        }

        public Patient()
        { /* Require use of factory methods */ }

        #endregion


        public static Patient GetPatientByPID(string pid)
        {
            Patient p = null;
            
            using (var ctx = Csla.Data.ContextManager<PrescriptionDataContext>.GetManager(
                DatabaseConnection.GetConnectionString(
                DatabaseConnection.DatabaseName.GENPRES), false))
            {
                Database.Patient data = ctx.DataContext.Patients.SingleOrDefault<Database.Patient>(c => c.PID == pid);
                if(data != null) p = DataPortal.Fetch<Patient>(data);
            }

            return p;
        }
    }
}

                    