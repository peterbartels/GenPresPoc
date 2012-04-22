using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Data.DTO.Prescriptions;

namespace Informedica.GenPres.Data.Visibility.Scenarios
{
    public class NoVolumes : IScenario
    {
        public void SetVisibilities(Prescription prescription, PrescriptionDto _prescriptionDto)
        {
            if (_scenarioIsTrue(prescription))
            {
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.drugQuantity, false);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.substanceDrugConcentration, false);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.prescriptionSolution, false);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.prescriptionOnrequest, true);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.prescriptionContinuous, false);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.prescriptionInfusion, false);
            }
        }

        private bool _scenarioIsTrue(Prescription prescription)
        {
            return (
                !prescription.DoseVolume && 
                !prescription.AdminVolume 
            );
        }
    }
}
