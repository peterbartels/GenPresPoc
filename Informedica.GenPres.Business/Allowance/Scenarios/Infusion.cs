using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Business.Allowance.Scenarios
{
    public class Infusion : IScenario
    {
        private readonly Prescription _prescription;

        public Infusion(Prescription prescription)
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
                    new PropertyAllowanceConfig(true, dose.Total),
                    new PropertyAllowanceConfig(true, _prescription.Total),                                       
                    new PropertyAllowanceConfig(false, dose.Rate),
                    new PropertyAllowanceConfig(true, _prescription.Rate)
                };
            }
        }

        public bool IsTrue()
        {
            return (
                !_prescription.OnRequest &&
                _prescription.Infusion &&
                !_prescription.Continuous
            );
        }
    }
}
