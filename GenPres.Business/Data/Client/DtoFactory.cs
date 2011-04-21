namespace GenPres.Business.DataAccess.Client
{
    public class DtoFactory
    {
        public static T Create<T>() where T : IDto, new()
        {
            return new T();
        }
    }
}
