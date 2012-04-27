using Informedica.GenPres.Data.DTO.Prescriptions;
using Informedica.GenPres.Data.Visibility.Scenarios;
using Informedica.GenPres.Business.Domain.Prescriptions;

namespace Informedica.GenPres.Data.Visibility
{
    public class PrescriptionVisibility 
    {
        private static IScenario[] _scenarios = new IScenario[]{
            new NoVolumes(),
            new AdminVolume(),
            new AdminVolumeDoseVolume(),
            new NoOptions(),
            new OnRequest(),
            new OnRequestContinuous(),
            new OnRequestInfusion(),
            new OnRequestContinuousInfusion(),
            new Continuous(),
            new ContinuousInfusion(),
            new Infusion()
        };

        private static void _setScenariosVisibility(Prescription prescription, PrescriptionDto pDto)
        {
            for (int i = 0; i < _scenarios.Length; i++)
            {
                _scenarios[i].SetVisibilities(prescription, pDto);
            }
            _setSubstanceQuantityVisibility(pDto);
        }

        private static void _setSubstanceQuantityVisibility(PrescriptionDto pDto)
        {
            pDto.substanceQuantity.visible = true;
        }

        public static void Execute(Prescription prescription, PrescriptionDto pDto)
        {
            if (prescription.Drug.Generic == "" || prescription.Drug.Route == "" || prescription.Drug.Shape == "")
                return;

            _setScenariosVisibility(prescription, pDto);
        }
    }
}