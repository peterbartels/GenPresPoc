namespace Informedica.GenPres.Business.Domain.Prescriptions.Scenarios
{
    public class VolumeScenario : IScenario
    {
        public bool AdminVolume { get; set;  }
        public bool DoseVolume { get; set; }

        public bool AppliesTo(Prescription prescription)
        {
            return prescription.AdminVolume == AdminVolume &&
                   prescription.DoseVolume == DoseVolume;
        }
    }
}