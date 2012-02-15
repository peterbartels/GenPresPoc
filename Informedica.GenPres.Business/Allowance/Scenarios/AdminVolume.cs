using Informedica.GenPres.Business.Domain.Prescriptions;

namespace Informedica.GenPres.Business.Allowance.Scenarios
{
    public class AdminVolume : IScenario
    {
        private readonly Prescription _prescription;

        public AdminVolume(Prescription prescription)
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
                !_prescription.DoseVolume && 
                _prescription.AdminVolume 
            );
        }
    }
}
