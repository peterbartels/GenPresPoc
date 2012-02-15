namespace Informedica.GenPres.Business.Domain
{
    public enum CalculationState
    {
        NotSet = 0,
        Calculated = 1
    }

    interface ICalculationStateTrackable
    {
        CalculationState CalculationState { get; set; }
    }
}
