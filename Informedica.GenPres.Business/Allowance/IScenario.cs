namespace Informedica.GenPres.Business.Allowance
{
    public interface IScenario
    {
        PropertyAllowanceConfig[] PropertyAllowance { get; }
        bool IsTrue();
    }
}
