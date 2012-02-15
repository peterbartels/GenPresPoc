using System;
using System.Data;
using System.Linq;
using System.Xml.Linq;
using GenPres.Business.Domain.Patients;

namespace GenPres.Data.DAO.Mapper.Patients
{
    public class XmlPatientMapper
    {
        public Patient MapDaoToBusinessObject(XElement xmlDaoObj, Patient patientBo)
        {
            patientBo.LastName = xmlDaoObj.Descendants("lastname").ElementAt(0).Value;
            patientBo.FirstName = xmlDaoObj.Descendants("firstname").ElementAt(0).Value;
            patientBo.Pid = xmlDaoObj.Descendants("hospitalnumber").ElementAt(0).Value;
            patientBo.RegisterDate = DateTime.Parse(xmlDaoObj.Descendants("admissiondate").ElementAt(0).Value);
            patientBo.Weight.BaseValue = decimal.Parse(xmlDaoObj.Descendants("weight").ElementAt(0).Value);
            patientBo.Height.BaseValue = decimal.Parse(xmlDaoObj.Descendants("length").ElementAt(0).Value);
            patientBo.Unit = xmlDaoObj.Descendants("unit").ElementAt(0).Value;
            patientBo.Bed = xmlDaoObj.Descendants("bedname").ElementAt(0).Value;
            return patientBo;
        }
    }
}
