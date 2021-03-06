﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using Informedica.GenPres.Business.Data.IRepositories;
using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Business.Domain.Units;

namespace Informedica.GenPres.Business.Domain.Patients
{
    public class Patient
    {
        #region Public Properties

        public virtual Guid Id { get; set; }

        public virtual string LastName { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string FullName
        { 
            get { return FirstName + " " + LastName; }
        }

        public virtual string Pid { get; set; }

        public virtual int LogicalUnitId { get; set; }

        public virtual UnitValue Height { get; set; }

        public virtual UnitValue Weight { get; set; }

        public virtual string Gender { get; set; }

        public virtual string Unit { get; set; }

        public virtual string Bed { get; set; }

        public virtual string Birthdate { get; set; }

        public virtual string Age { get; set; }

        public virtual DateTime RegisterDate { get; set; }

        public virtual int DaysRegistered { get; set; }

        public virtual IList<Prescription> Prescriptions { get; set; }

        #endregion

        public virtual bool IsNew { get { return (Id == Guid.Empty); } }

        protected Patient()
        {
            
        }

        private static IPdsmPatientRepository Repository
        {
            get { return StructureMap.ObjectFactory.GetInstance<IPdsmPatientRepository>(); }
        }

        public static Patient NewPatient()
        {
            var patient = new Patient();
            patient.Height = UnitValue.NewUnitValue();
            patient.Height.Unit = "cm";

            patient.Weight = UnitValue.NewUnitValue();
            patient.Weight.Unit = "kg";

            patient.Prescriptions = new EntitySet<Prescription>();

            return patient;
        }


        public static ReadOnlyCollection<Patient> GetPatientsByLogicalUnit(int logicalUnitId)
        {
            return Repository.GetPatientsByLogicalUnitId(logicalUnitId);
        }


    }
}
