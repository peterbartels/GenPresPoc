namespace GenPres.Business.Domain
{
    public interface ISavable
    {
        bool IsNew { get; }
        int Id { get; set; }
    }
}
