using Informedica.GenPres.Business.Domain.Prescriptions;
using Informedica.GenPres.Data.DTO.Prescriptions;

namespace Informedica.GenPres.Data.Visibility.Scenarios
{
    public class NoVolumes : IScenario
    {
        private readonly Prescription _prescription;
        private readonly PrescriptionDto _prescriptionDto;

        public NoVolumes(Prescription prescription, PrescriptionDto prescriptionDto)
        {
            _prescription = prescription;
            _prescriptionDto = prescriptionDto;
        }

        public PropertyVisibilityConfig[] PropertyVisibility
        {
            get
            {
                var drug = _prescription.Drug;
                var substance = _prescription.Drug.Components[0].Substances[0];

                return new []
                {
                    new PropertyVisibilityConfig(false, _prescriptionDto.drugQuantity),
                    new PropertyVisibilityConfig(false, _prescriptionDto.substanceDrugConcentration),
                    new PropertyVisibilityConfig(false, _prescriptionDto.prescriptionSolution),
                    new PropertyVisibilityConfig(true, _prescriptionDto.prescriptionOnrequest),
                    new PropertyVisibilityConfig(false, _prescriptionDto.prescriptionContinuous),
                    new PropertyVisibilityConfig(false, _prescriptionDto.prescriptionInfusion)
                };
            }
        }

        public bool IsTrue()
        {
            return (
                !_prescription.DoseVolume && 
                !_prescription.AdminVolume 
            );
        }
    }
}
