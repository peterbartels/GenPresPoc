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
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.prescriptionFrequency, true);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.prescriptionDuration, false);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.doseQuantity, false);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.adminQuantity, false);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.adminTotal, false);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.doseTotal, false);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.adminRate, true);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.doseRate, true);
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

