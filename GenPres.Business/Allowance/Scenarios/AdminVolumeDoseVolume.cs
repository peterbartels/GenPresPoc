using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Business.Allowance.Scenarios
{
    public class AdminVolumeDoseVolume : IScenario
    {
        private readonly Prescription _prescription;

        public AdminVolumeDoseVolume(Prescription prescription)
        {
            _prescription = prescription;
        }

        public PropertyAllowanceConfig[] PropertyAllowance
        {
            get
            {
                var drug = _prescription.Drug;
                var substance = _prescription.Drug.Components[0].Substances[0];

                return new []
                {
                    new PropertyAllowanceConfig(true, drug.Quantity),
                    new PropertyAllowanceConfig(true, substance.DrugConcentration)
                };
            }
        }

        public bool IsTrue()
        {
            return (
                _prescription.DoseVolume && 
                _prescription.AdminVolume 
            );
        }
    }
}
