namespace GenPres.Business.ServiceProvider
{
    public interface IServiceProvider : System.IServiceProvider
    {
        T Resolve<T>();
        void RegisterInstanceOfType<T>(T instance);
    }
}
