﻿using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Data.DTO.Prescriptions;

namespace Informedica.GenPres.Data.Visibility.Scenarios
{
    public class OnRequest : IScenario
    {
        public void SetVisibilities(Prescription prescription, PrescriptionDto _prescriptionDto)
        {
            if (_scenarioIsTrue(prescription))
            {
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.prescriptionFrequency, true);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.prescriptionDuration, false);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.doseQuantity, true);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.adminQuantity, true);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.adminTotal, false);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.doseTotal, false);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.adminRate, false);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.doseRate, false);
            }
        }

        private bool _scenarioIsTrue(Prescription prescription)
        {
            return (
                prescription.OnRequest &&
                !prescription.Infusion &&
                !prescription.Continuous
            );
        }
    }
}