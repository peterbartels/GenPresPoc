using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Data.DTO.Prescriptions;

namespace Informedica.GenPres.Data.Visibility.Scenarios
{
    public class AdminVolumeDoseVolume : IScenario
    {
        public void SetVisibilities(Prescription prescription, PrescriptionDto _prescriptionDto)
        {
            if (_scenarioIsTrue(prescription))
            {
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.drugQuantity, true);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.substanceDrugConcentration, true);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.prescriptionSolution, true);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.prescriptionOnrequest, true);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.prescriptionContinuous, true);
                PropertyVisibilityConfig.SetPropertyAllowance(_prescriptionDto.prescriptionInfusion, true);
            }
        }


        private bool _scenarioIsTrue(Prescription prescription)
        {
            return (
                prescription.DoseVolume &&
                prescription.AdminVolume
            );
        }
    }
}
