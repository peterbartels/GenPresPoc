using System;
using FluentNHibernate.Testing;
using GenPres.Business.Domain.Patients;
using GenPres.Business.Domain.Prescriptions;
using GenPres.Business.Domain.Units;
using GenPres.Data;
using GenPres.xTest.Base;
using GenPres.xTest.Base.Mappers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Data.NHibernateMappingsTest
{
    [TestClass]
    public class PrescriptionMapTest : BaseGenPresTest
    {
        [TestMethod]
        public void PrescriptionMapShouldCorrectlyMapPrescriptionBo()
        {
            var patient = Patient.NewPatient();
            patient.Pid = "1234567";
            
            var drug = Drug.NewDrug(Prescription.NewPrescription());
            drug.Generic = "paracetamol";
            drug.Route = "rect";
            drug.Shape = "zetp";
            drug.Quantity = GetFakeUnitValue();

            var component = Component.NewComponent();
            component.Name = "test";

            new PersistenceSpecification<Prescription>(SessionManager.SessionFactory.GetCurrentSession(), new PrescriptionEqualityComparer())
               .CheckProperty(b => b.StartDate, DateTime.Now)
               .CheckProperty(b => b.EndDate, DateTime.Now)
               .CheckProperty(b => b.CreationDate, DateTime.Now)
               .CheckProperty(b => b.PID, "1234567")
               .CheckProperty(b => b.Continuous, true)
               .CheckProperty(b => b.Infusion, true)
               .CheckProperty(b => b.OnRequest, true)
               .CheckProperty(b => b.Solution, true)
               .CheckProperty(b => b.Solution, true)
               .CheckProperty(b => b.Drug, drug)
               .CheckProperty(b => b.Frequency, GetFakeUnitValue())
               .CheckProperty(b => b.Quantity, GetFakeUnitValue())
               .CheckProperty(b => b.Total, GetFakeUnitValue())
               .CheckProperty(b => b.Rate, GetFakeUnitValue())
               .CheckReference(b => b.Patient, patient)
               .CheckList(b => b.Drug.Components, new[] {component})
               .VerifyTheMappings();
        }

        private static UnitValue GetFakeUnitValue()
        {
            var fakeUnitValue = UnitValue.NewUnitValue(true);
            fakeUnitValue.BaseValue = 2;
            fakeUnitValue.BaseValue = 2000;
            fakeUnitValue.Unit = "mg";
            fakeUnitValue.Time = "dag";
            fakeUnitValue.Adjust = "kg";
            fakeUnitValue.Total = "ml";
            return fakeUnitValue;
        }
    }



    public class PrescriptionEqualityComparer : CustomEqualityComparer
    {
        public override bool Equals(object x, object y)
        {
            if (x is Patient && y is Patient)
            {
                return ((Patient)x).Id == ((Patient)y).Id;
            }
            if (x is Drug && y is Drug)
            {
                return (
                    ((Drug)x).Generic == ((Drug)y).Generic && 
                    ((Drug)x).Route == ((Drug)y).Route && 
                    ((Drug)x).Shape == ((Drug)y).Shape &&
                    CompareUnitValue(((Drug)x).Quantity, ((Drug)y).Quantity)
                );
            }

            if (x is UnitValue && y is UnitValue)
            {
                return CompareUnitValue((UnitValue)x, (UnitValue)y);
            }
            if (x is Component && y is Component)
            {
                return (
                    ((Component)x).Name == ((Component)y).Name &&
                    ((Component)x).Id == ((Component)y).Id  
                );
            }
            return base.Equals(x, y);
        }

        private static bool CompareUnitValue(UnitValue x, UnitValue y)
        {
            return (
                x.BaseValue == y.BaseValue &&
                x.Value == y.Value &&
                x.Unit == y.Unit &&
                x.Time == y.Time &&
                x.Adjust == y.Adjust &&
                x.Total == y.Total
            );
        }
    }

}
