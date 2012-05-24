namespace Informedica.GenPres.Business.Domain.Prescriptions.Scenarios
{
    public class OptionScenario : IScenario
    {
        public bool Solution { get; set; }
        public bool Continuous { get; set; }
        public bool Infusion { get; set; }
        public bool OnRequest { get; set; }

        public bool GenericIsVisible;
        public bool ShapeIsVisible;
        public bool RouteIsVisible;

        public bool AppliesTo(Prescription prescription)
        {
            return prescription.Solution == Solution && 
                   prescription.Continuous == Continuous &&
                   prescription.Infusion == Infusion &&
                   prescription.OnRequest == OnRequest;
        }
    }
}
