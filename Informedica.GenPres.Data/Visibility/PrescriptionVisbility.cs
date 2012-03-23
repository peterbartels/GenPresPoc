using Informedica.GenPres.Data.DTO.Prescriptions;
using Informedica.GenPres.Data.Visibility.Scenarios;
using Informedica.GenPres.Business.Domain.Prescriptions;

namespace Informedica.GenPres.Data.Visibility
{
    public class PrescriptionVisbility 
    {
        private Prescription _prescription;
        private PrescriptionDto _prescriptionDto;

        public PrescriptionVisbility(Prescription prescription, PrescriptionDto pDto)
        {
            _prescription = prescription;
            _prescriptionDto = pDto;
        }

        private void ExecuteDetermination()
        {
            var scenarios = new IScenario[]{
                new NoVolumes(_prescription, _prescriptionDto),
                new AdminVolume(_prescription, _prescriptionDto),
                new AdminVolumeDoseVolume(_prescription, _prescriptionDto),
                new NoOptions(_prescription, _prescriptionDto),
                new OnRequest(_prescription, _prescriptionDto),
                new OnRequestContinuous(_prescription, _prescriptionDto),
                new OnRequestInfusion(_prescription, _prescriptionDto),
                new OnRequestContinuousInfusion(_prescription, _prescriptionDto),
                new Continuous(_prescription, _prescriptionDto),
                new ContinuousInfusion(_prescription, _prescriptionDto),
                new Infusion(_prescription, _prescriptionDto)
            };

            for (int i = 0; i < scenarios.Length; i++)
            {
                if (scenarios[i].IsTrue())
                {
                    for (int p = 0; p < scenarios[i].PropertyVisibility.Length; p++)
                        scenarios[i].PropertyVisibility[p].SetPropertyAllowance();
                }
            }
            _prescriptionDto.substanceQuantity.IsVisible = true;
        }

        public void CheckCombinations()
        {
            if (_prescription.Drug.Generic == "" || _prescription.Drug.Route == "" || _prescription.Drug.Shape == "")
                return;

            ExecuteDetermination();
        }

        public static PrescriptionVisbility Determine(Prescription prescription, PrescriptionDto pDto)
        {
            var pa = new PrescriptionVisbility(prescription, pDto);
            pa.CheckCombinations();
            return pa;
        }
    }
}