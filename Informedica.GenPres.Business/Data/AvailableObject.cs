namespace GenPres.Business.Data
{
    public class AvailableObject<T>
    {
        public static AvailableObject<T> Create(bool isAvailable, T obj)
        {
            AvailableObject<T> availableObject = new AvailableObject<T>();
            availableObject.IsAvailable = isAvailable;
            availableObject.Object = obj;
            return availableObject;
        }

        public bool IsAvailable { get; private set; }
        public T Object { get; private set; }
    }
}
