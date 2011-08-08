using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using GenPres.Business.WebService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenPres.xTest.Business.GenFormTest
{
    [TestClass]
    public class GetProductInfoTest
    {
        private readonly GenFormService _genFormService = new GenFormService();

        [TestMethod]
        public void CanGetComponentIncrements()
        {
            var componentIncrements = _genFormService.GetComponentIncrements("paracetamol", "rect", "zetp");
            Assert.IsTrue(componentIncrements.Length > 0);

        }

        [TestMethod]
        public void CanGetSubstanceIncrements()
        {
            var componentIncrements = _genFormService.GetSubstanceIncrements("paracetamol", "rect", "zetp");
            Assert.IsTrue(componentIncrements.Length > 0);
        }

        [TestMethod]
        public void CanGetSubstanceUnits()
        {
            var substanceUnits = _genFormService.GetSubstanceUnits("paracetamol", "rect", "zetp");
            Assert.IsTrue(substanceUnits.Length > 0);
        }

        [TestMethod]
        public void CanGetComponentUnits()
        {
            var componentUnits = _genFormService.GetComponentUnits("paracetamol", "rect", "zetp");
            Assert.IsTrue(componentUnits.Length > 0);
        }
    }
}
