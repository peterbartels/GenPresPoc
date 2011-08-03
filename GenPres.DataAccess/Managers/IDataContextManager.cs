namespace GenPres.DataAccess.Managers
{
    public interface IDataContextManager
    {
        System.Data.Linq.DataContext Context { get; }
        void Submit();
    }
}
