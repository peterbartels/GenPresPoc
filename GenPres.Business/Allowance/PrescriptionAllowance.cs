using GenPres.Business.Domain.Prescriptions;
using System.Collections.Generic;
using GenPres.Business.Allowance.Scenarios;

namespace GenPres.Business.Allowance
{
    public class PrescriptionAllowance : IPrescriptionAllowance
    {
        private Prescription _prescription;
        
        private PrescriptionAllowance(Prescription prescription)
        {
            _prescription = prescription;
        }

        public void CheckCombinations()
        {
            if (_prescription.Drug.Generic == "" && _prescription.Drug.Route == "" && _prescription.Drug.Shape == "")
                return;

            var scenarios = new IScenario[]{
                new NoVolumes(_prescription),
                new AdminVolume(_prescription),
                new AdminVolumeDoseVolume(_prescription),
                new NoOptions(_prescription),
                new OnRequest(_prescription),
                new OnRequestContinuous(_prescription),
                new OnRequestInfusion(_prescription),
                new OnRequestContinuousInfusion(_prescription),
                new Continuous(_prescription),
                new ContinuousInfusion(_prescription),
                new Infusion(_prescription)
            };

            for (int i = 0; i < scenarios.Length; i++)
            {
                if (scenarios[i].IsTrue())
                {
                    for (int p = 0; p < scenarios[i].PropertyAllowance.Length; p++)
                        scenarios[i].PropertyAllowance[p].SetPropertyAllowance();
                }
            }
        }

        public static PrescriptionAllowance Determine(Prescription prescription)
        {
            var pa = new PrescriptionAllowance(prescription);
            pa.CheckCombinations();
            return pa;
        }
    }

    internal interface IPrescriptionAllowance
    {
        void CheckCombinations();
    }
}
