using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Data.DTO.Prescriptions;

namespace Informedica.GenPres.Data.Visibility.Scenarios
{
    public class OnRequestContinuous : IScenario
    {
        public void SetVisibilities(Prescription prescription, PrescriptionDto _prescriptionDto)
        {
            if (_scenarioIsTrue(prescription))
            {
                _prescriptionDto.prescriptionFrequency.visible = true;
                _prescriptionDto.prescriptionDuration.visible = false;
                _prescriptionDto.doseQuantity.visible = false;
                _prescriptionDto.adminQuantity.visible = false;
                _prescriptionDto.adminTotal.visible = false;
                _prescriptionDto.doseTotal.visible = false;
                _prescriptionDto.adminRate.visible = true;
                _prescriptionDto.doseRate.visible = true;
            }
        }

        private bool _scenarioIsTrue(Prescription prescription)
        {
            return (
                prescription.OnRequest &&
                !prescription.Infusion &&
                prescription.Continuous
            );
        }
    }
}

