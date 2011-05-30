using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.Domain.Prescription
{
    public class Prescription : IPrescription
    {
        #region Private Fields

        private DateTime _startDate;

        private DateTime _endDate;

        private DateTime _creationDate;

        #endregion

        #region Public Properties

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public DateTime CreationDate
        {
            get { return _creationDate; }
            set { _creationDate = value; }
        }

        #endregion

        public static Prescription NewPrescription()
        {
            Prescription prescription = new Prescription();
            prescription.CreationDate = DateTime.Now;
            return prescription;
        }
    }
}
