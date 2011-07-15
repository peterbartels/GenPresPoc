using System;
using GenPres.Business.Data.Client.PrescriptionData;
using GenPres.DataAccess;
using GenPres.DataAccess.DataMapper.Mapper.PrescriptionMapper;
using GenPres.xTest.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Business.Domain.PrescriptionDomain;

namespace GenPres.Business.Test.PrescriptionTest
{
    [TestClass]
    public class PrescriptionTest : BaseGenPresTest
    {
        [TestMethod]
        public void Prescription_creates_CreationDate()
        {
            var p = Prescription.NewPrescription();
            Assert.AreEqual(p.CreationDate.ToString("dd-MM-yyyy HH:mm"), DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
        }

        [TestMethod]
        public void Prescription_creates_Drug()
        {
            var p = Prescription.NewPrescription();
            Assert.IsNotNull(p.Drug);
        }

        [TestMethod]
        public void PrescriptionDto_canMapToPrescriptionBO()
        {
            var pDto = new PrescriptionDto();
            pDto.StartDate = DateTime.Parse("2011-07-01 12:00:00").ToString();
            Prescription p = PrescriptionAssembler.AssemblePrescriptionBo(pDto);
            Assert.AreEqual(p.StartDate, DateTime.Parse("2011-07-01 12:00:00"));
        }

        [TestMethod]
        public void PrescriptionMapper_canMapPrescriptionBoToPrescriptionDAO()
        {
            Prescription p = Prescription.NewPrescription();

            p.StartDate = DateTime.Now;

            p.Drug.Generic = "paracetamol";
            p.Drug.Route = "rect";
            p.Drug.Shape = "zetp";

            //PrescriptionMapper pMapper = new PrescriptionMapper(new GenPresDataContextFactory());
            //pMapper.MapFromBoToDao(p, new Database.Prescription());
        }


        [TestMethod]
        public void PrescriptionBo_canMapToPrescriptionDto()
        {
            Prescription p = Prescription.NewPrescription();
            p.StartDate = DateTime.Parse("2011-07-01 12:00:00");
            PrescriptionDto pDto = PrescriptionAssembler.AssemblePrescriptionDto(p);
            Assert.AreEqual(pDto.StartDate, DateTime.Parse("2011-07-01 12:00:00"));
        }
    }
}
