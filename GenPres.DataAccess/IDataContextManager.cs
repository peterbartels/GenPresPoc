namespace GenPres.DataAccess
{
    public interface IDataContextManager
    {
        System.Data.Linq.DataContext Context { get; }
        void Submit();
    }
}
