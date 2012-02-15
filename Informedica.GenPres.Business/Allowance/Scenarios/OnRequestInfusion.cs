using Informedica.GenPres.Business.Domain.Prescriptions;

namespace Informedica.GenPres.Business.Allowance.Scenarios
{
    public class OnRequestInfusion : IScenario
    {
        private readonly Prescription _prescription;

        public OnRequestInfusion(Prescription prescription)
        {
            _prescription = prescription;
        }

        public PropertyAllowanceConfig[] PropertyAllowance
        {
            get
            {
                var drug = _prescription.Drug;
                var dose = _prescription.Doses[0];

                return new[]
                {
                    new PropertyAllowanceConfig(true, _prescription.Frequency),
                    new PropertyAllowanceConfig(true, _prescription.Duration),
                    new PropertyAllowanceConfig(true, dose.Quantity),
                    new PropertyAllowanceConfig(true, _prescription.Quantity),                 
                    new PropertyAllowanceConfig(false, dose.Total),
                    new PropertyAllowanceConfig(false, _prescription.Total),                                       
                    new PropertyAllowanceConfig(false, dose.Rate),
                    new PropertyAllowanceConfig(true, _prescription.Rate)
                };
            }
        }

        public bool IsTrue()
        {
            return (
                _prescription.OnRequest &&
                _prescription.Infusion &&
                !_prescription.Continuous
            );
        }
    }
}
