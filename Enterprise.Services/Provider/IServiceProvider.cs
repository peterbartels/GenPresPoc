using System;

namespace Enterprise.Services.Provider
{
    public interface IServiceProvider : System.IServiceProvider
    {
        void RegisterInstance<T>(T instance);
        T Resolve<T>();
    }
}
