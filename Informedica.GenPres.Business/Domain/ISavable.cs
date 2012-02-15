namespace Informedica.GenPres.Business.Domain
{
    public interface ISavable
    {
        bool IsNew { get; }
        int Id { get; set; }
    }
}
