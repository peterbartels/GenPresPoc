using GenPres.Business.Domain.Prescriptions;

namespace GenPres.Business.Allowance.Scenarios
{
    public class Continuous : IScenario
    {
        private readonly Prescription _prescription;

        public Continuous(Prescription prescription)
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
                    new PropertyAllowanceConfig(false, _prescription.Frequency),
                    new PropertyAllowanceConfig(false, _prescription.Duration),
                    new PropertyAllowanceConfig(false, dose.Quantity),
                    new PropertyAllowanceConfig(false, _prescription.Quantity),                 
                    new PropertyAllowanceConfig(false, dose.Total),
                    new PropertyAllowanceConfig(false, _prescription.Total),                                       
                    new PropertyAllowanceConfig(true, dose.Rate),
                    new PropertyAllowanceConfig(true, _prescription.Rate)
                };
            }
        }

        public bool IsTrue()
        {
            return (
                !_prescription.OnRequest &&
                !_prescription.Infusion &&
                _prescription.Continuous
            );
        }
    }
}
