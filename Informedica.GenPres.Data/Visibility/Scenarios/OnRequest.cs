using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Data.DTO.Prescriptions;

namespace Informedica.GenPres.Data.Visibility.Scenarios
{
    public class OnRequest : IScenario
    {
        private readonly Prescription _prescription;
        private readonly PrescriptionDto _prescriptionDto;

        public OnRequest(Prescription prescription, PrescriptionDto prescriptionDto)
        {
            _prescription = prescription;
            _prescriptionDto = prescriptionDto;
        }

        public PropertyVisibilityConfig[] PropertyVisibility
        {
            get
            {
                var drug = _prescription.Drug;
                var dose = _prescription.Doses[0];

                return new[]
                {
                    new PropertyVisibilityConfig(true, _prescriptionDto.prescriptionFrequency),
                    new PropertyVisibilityConfig(false, _prescriptionDto.prescriptionDuration),
                    new PropertyVisibilityConfig(true, _prescriptionDto.adminQuantity),
                    new PropertyVisibilityConfig(true, _prescriptionDto.doseQuantity),
                    new PropertyVisibilityConfig(false, _prescriptionDto.adminTotal),
                    new PropertyVisibilityConfig(false, _prescriptionDto.doseTotal),
                    new PropertyVisibilityConfig(false, _prescriptionDto.adminRate),
                    new PropertyVisibilityConfig(false, _prescriptionDto.doseRate)
                };
            }
        }

        public bool IsTrue()
        {
            return (
                _prescription.OnRequest &&
                !_prescription.Infusion &&
                !_prescription.Continuous
            );
        }
    }
}
