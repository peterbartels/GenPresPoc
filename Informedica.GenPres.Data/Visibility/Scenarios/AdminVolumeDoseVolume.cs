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
                _prescriptionDto.drugQuantity.visible = true;
                _prescriptionDto.substanceDrugConcentration.visible = true;
                _prescriptionDto.prescriptionSolution.visible = true;
                _prescriptionDto.prescriptionOnrequest.visible = true;
                _prescriptionDto.prescriptionContinuous.visible = true;
                _prescriptionDto.prescriptionInfusion.visible = true;
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
