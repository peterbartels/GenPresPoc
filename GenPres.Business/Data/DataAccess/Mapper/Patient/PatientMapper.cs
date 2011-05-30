using System;
using GenPres.Business.Domain;
using System.Data;
using GenPres.Business.Domain.Patient;

namespace GenPres.Business.Data.DataAccess.Mapper.Patient
{
    public class PatientMapper
    {
        public IPatient MapDaoToBusinessObject(object daoObj, IPatient patientBo)
        {
            var patientDao = (DataRow)daoObj;
            patientBo.Id = int.Parse(patientDao["PatientID"].ToString());
            patientBo.LastName = patientDao["LastName"].ToString();
            patientBo.FirstName = patientDao["FirstName"].ToString();
            patientBo.PID = patientDao["HospitalNumber"].ToString();
            patientBo.LogicalUnitId = int.Parse(patientDao["LOGICALUNITID"].ToString());

            decimal length = 0;
            decimal weight = 0;
            decimal.TryParse(patientDao["Length"].ToString(), out length);
            decimal.TryParse(patientDao["Weight"].ToString(), out weight);
            patientBo.Weight = weight;
            patientBo.Length = length;

            DateTime? addmissionDate = (DateTime?)patientDao["AddmissionDate"];

            if (addmissionDate != null)
            {
                TimeSpan span = DateTime.Now.Subtract(addmissionDate.Value);
                patientBo.DaysRegistered = span.Days;
                patientBo.RegisterDate = addmissionDate.Value;
            }

            patientBo.Unit = patientDao["LogicalUnitName"].ToString();
            patientBo.Bed = patientDao["BedName"].ToString();

            patientBo.Gender = patientDao["GenderID"].ToString();
            

            return patientBo;
        }
    }
}
