
namespace GenPres.Business
{
    public class GenPresServiceProvider : ServiceProvider.ServiceProvider
    {
        private static GenPresServiceProvider _instance;
        private static readonly object _lock = new object();

        public static GenPresServiceProvider Create()
        {
            return _instance ?? (_instance =  CreateInstance(_instance, _lock));
        }
    }
}
