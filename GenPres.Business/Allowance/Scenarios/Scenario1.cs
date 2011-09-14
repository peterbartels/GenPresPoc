using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Business.Allowance.Scenarios
{
    public class Scenario1 : IScenario
    {
        private Prescription _prescription;
        public PropertyAllowanceConfig[] PropertyAllowance
        {
            get
            {
                var drug = _prescription.Drug;
                var substance = _prescription.Drug.Components[0].Substances[0];
                var dose = _prescription.Doses[0];

                return new []
                {
                    new PropertyAllowanceConfig(false, drug.Quantity),
                    new PropertyAllowanceConfig(false, substance.DrugConcentration),
                    new PropertyAllowanceConfig(false, _prescription.Frequency),
                    new PropertyAllowanceConfig(false, _prescription.Duration),
                    new PropertyAllowanceConfig(false, dose.Quantity),
                    new PropertyAllowanceConfig(false, _prescription.Quantity),
                    new PropertyAllowanceConfig(false, dose.Total),
                    new PropertyAllowanceConfig(false, _prescription.Total),
                    new PropertyAllowanceConfig(false, dose.Rate),
                    new PropertyAllowanceConfig(false, _prescription.Rate)
                };
            }
        }

        public bool[] Criteria()
        {
            return new[] { _prescription.DoseVolume, _prescription.AdminVolume };
        }

        public Scenario1(Prescription prescription)
        {
            _prescription = prescription;
        }
    }
}
