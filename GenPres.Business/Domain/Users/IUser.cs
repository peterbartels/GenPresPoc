namespace GenPres.Business.Domain.Users
{
    public interface IUser : ISavable, IChangeTrackable
    {
        int Id { get; set; }
        string UserName { get; set; }
        string PassCrypt { get; set; }
    }
}
