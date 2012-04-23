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
                _prescriptionDto.drugQuantity.visible = false;
                _prescriptionDto.substanceDrugConcentration.visible = false;
                _prescriptionDto.prescriptionSolution.visible = false;
                _prescriptionDto.prescriptionOnrequest.visible = true;
                _prescriptionDto.prescriptionContinuous.visible = false;
                _prescriptionDto.prescriptionInfusion.visible = false;
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
