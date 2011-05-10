using GenPres.Business.Domain;
using System.Data;

namespace GenPres.Business.Data.DataAccess.Mapper
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
            return patientBo;
        }
    }
}
