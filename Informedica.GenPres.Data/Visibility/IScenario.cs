namespace Informedica.GenPres.Data.Visibility
{
    public interface IScenario
    {
        PropertyVisibilityConfig[] PropertyVisibility { get; }
        bool IsTrue();
    }
}
