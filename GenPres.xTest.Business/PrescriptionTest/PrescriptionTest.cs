using System;
using GenPres.Data.DTO.Prescriptions;
using GenPres.xTest.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.xTest.Business.PrescriptionTest
{
    [TestClass]
    public class PrescriptionTest : BaseGenPresTest
    {   /*
        [TestMethod]
        public void PrescriptionCreatesCreationDate()
        {
            var p = Prescription.NewPrescription();
            Assert.AreEqual(p.CreationDate.ToString("dd-MM-yyyy HH:mm"), DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
        }


        [TestMethod]
        public void PrescriptionCreatesDrug()
        {
            var p = Prescription.NewPrescription();
            Assert.IsNotNull(p.Drug);
        }

        [TestMethod]
        public void PrescriptionCreatesComponent()
        {
            var p = Prescription.NewPrescription();
            Assert.IsTrue(p.Drug.Components.Count == 1);
        }

        [TestMethod]
        public void PrescriptionCreatesSubstance()
        {
            var p = Prescription.NewPrescription();
            Assert.IsTrue(p.Drug.Components[0].Substances.Count == 1);
        }

        [TestMethod]
        public void PrescriptionDtoCanMapToPrescriptionBo()
        {
            var pDto = new PrescriptionDto();
            pDto.StartDate = DateTime.Parse("2011-07-01 12:00:00").ToString();
            IPrescription p = PrescriptionAssembler.AssemblePrescriptionBo(pDto);
            Assert.AreEqual(p.StartDate, DateTime.Parse("2011-07-01 12:00:00"));
        }

        [TestMethod]
        public void PrescriptionMapperCanMapPrescriptionBoToPrescriptionDao()
        {
            IPrescription p = Prescription.NewPrescription();

            p.StartDate = DateTime.Now;

            p.Drug.Generic = "paracetamol";
            p.Drug.Route = "rect";
            p.Drug.Shape = "zetp";

            //PrescriptionMapper pMapper = new PrescriptionMapper(new GenPresDataContextFactory());
            //pMapper.MapFromBoToDao(p, new Database.Prescription());
        }

        [TestMethod]
        public void PrescriptionBoCanMapToPrescriptionDto()
        {
            IPrescription p = Prescription.NewPrescription();
            p.StartDate = DateTime.Parse("2011-07-01 12:00:00");
            PrescriptionDto pDto = PrescriptionAssembler.AssemblePrescriptionDto(p);
            Assert.AreEqual(pDto.StartDate, "1-7-2011 12:00:00");
        }

        [TestMethod]
        public void PrescriptionBoUpdatesSubstanceIncrements()
        {
            IPrescription p = Prescription.NewPrescription();
            p.Drug.Generic = "paracetamol";
            p.Drug.Route = "rect";
            p.Drug.Shape = "zetp";
            Assert.IsTrue(p.Drug.Components[0].Substances[0].SubstanceIncrements.Length > 0);
        }

        [TestMethod]
        public void PrescriptionBoUpdatesComponentIncrement()
        {
            IPrescription p = Prescription.NewPrescription();
            p.Drug.Generic = "paracetamol";
            p.Drug.Route = "rect";
            p.Drug.Shape = "zetp";
            Assert.IsTrue(p.Drug.Components[0].ComponentIncrement != 0);
        }*/
    }
}
