using System;

namespace Enterprise.Service
{
    public interface IServiceProvider: System.IServiceProvider
    {
        T Resolve<T>();
    }
}
