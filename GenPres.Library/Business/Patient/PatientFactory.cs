using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace GenPres.Business
{
    public class PatientFactory
    {
        public List<LogicalUnit> logicalUnits = new List<LogicalUnit>();
        public List<Patient> patients = new List<Patient>();

        public void AddUnitByDataRow(DataRow dr)
        {
            LogicalUnit lu = new LogicalUnit();
            lu.Name = dr["Name"].ToString();
            lu.Id = dr["LogicalUnitID"].ToString();
            logicalUnits.Add(lu);
        }

        public void AddPatientByDataRow(DataRow dr)
        {
            Patient pat = new Patient();
            pat.FirstName = dr["FirstName"].ToString();
            pat.Gender = dr["GenderID"].ToString();
            pat.LastName = dr["LastName"].ToString();
            pat.id = (int) dr["PatientID"];
            pat.Unit = dr["LogicalUnitName"].ToString();
            pat.Bed = dr["BedName"].ToString();
            pat.RegisterDate = dr["AddmissionDate"].ToString();
            pat.PID = dr["HospitalNumber"].ToString();

            DateTime? addmissionDate = (DateTime?) dr["AddmissionDate"];            
            if(addmissionDate!=null) {
                TimeSpan span = DateTime.Now.Subtract(addmissionDate.Value);
                pat.DaysRegistered = span.Days.ToString();
            }
            
            decimal length = 0;
            decimal weight = 0;
            decimal.TryParse(dr["Length"].ToString(), out length);
            decimal.TryParse(dr["Weight"].ToString(), out weight);
            pat.Weight = weight;
            pat.Length = length;
            Patient patient = Patient.GetPatientByPID(pat.PID);
            if (patient != null)
            {
                if (patient.Weight > 0) pat.Weight = patient.Weight;
                if (patient.Length > 0) pat.Length = patient.Length;
            }

            patients.Add(pat);
        }
    }
}
