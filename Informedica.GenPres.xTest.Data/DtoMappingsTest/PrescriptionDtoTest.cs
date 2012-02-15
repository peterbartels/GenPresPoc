using System;
using System.Globalization;
using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Data.DTO.Prescriptions;
using Informedica.GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenPres.xTest.Data.DtoMappingsTest
{
    [TestClass]
    public class PrescriptionDtoTest : BaseGenPresTest
    {   
        
        [TestMethod]
        public void PrescriptionDtoCanMapToPrescriptionBo()
        {
            var pDto = new PrescriptionDto();
            pDto.startDate = DateTime.Parse("2011-07-01 12:00:00").ToString(CultureInfo.CurrentCulture);
            Prescription p = PrescriptionAssembler.AssemblePrescriptionBo(pDto);
            Assert.AreEqual(p.StartDate, DateTime.Parse("2011-07-01 12:00:00"));
        }

        [TestMethod]
        public void PrescriptionBoCanMapToPrescriptionDto()
        {
            Prescription p = Prescription.NewPrescription();
            p.StartDate = DateTime.Parse("2011-07-01 12:00:00");
            PrescriptionDto pDto = PrescriptionAssembler.AssemblePrescriptionDto(p);
            Assert.AreEqual(pDto.startDate, "7/1/2011 12:00:00 PM");
        }

        [TestMethod]
        public void PrescriptionDtoShouldMapToDrugBo()
        {
            var pDto = new PrescriptionDto();

            pDto.drugGeneric = "paracetamol";
            pDto.drugRoute = "rect";
            pDto.drugShape = "zetp";

            Prescription p = PrescriptionAssembler.AssemblePrescriptionBo(pDto);
            Drug d = p.Drug;

            Assert.AreEqual(d.Generic, "paracetamol");
            Assert.AreEqual(d.Route, "rect");
            Assert.AreEqual(d.Shape, "zetp");

        }
    }
}
