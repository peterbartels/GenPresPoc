using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.Domain
{
    public class Patient : IPatient
    {
        #region Private Properties
        
        private int _id;
        private string _name;
        private int _logicalId;

        #endregion 

        #region Public Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PID { get; set; }

        public int Id { get; set; }
        public int LogicalUnitId { get; set; }

        public string FullName { 
            get { return FirstName + " " + LastName; }
        }

        #endregion

        public static Patient NewPatient()
        {
            return new Patient();
        }
    }

}
