namespace GenPres.Data.DTO
{
    public class DtoFactory
    {
        public static T Create<T>() where T : IDto, new()
        {
            return new T();
        }
    }
}
