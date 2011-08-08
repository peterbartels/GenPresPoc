namespace GenPres.Data.Managers
{
    public interface IDataContextManager
    {
        System.Data.Linq.DataContext Context { get; }
        void Submit();
    }
}
