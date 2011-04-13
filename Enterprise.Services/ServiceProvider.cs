using System;
using Microsoft.Practices.Unity;

namespace Enterprise.Service
{
    public abstract class ServiceProvider: IServiceProvider
    {
        private readonly IUnityContainer _container;

        protected ServiceProvider()
        { _container = new UnityContainer(); }

        #region IServiceProvider Members

        public void RegisterInstance<T>(T instance)
        {
            _container.RegisterInstance<T>(instance);
        }

        protected static T CreateInstance<T>(T instance, object lockThis) where T : class, IServiceProvider
        {
            if (instance == null)
            {
                lock (lockThis)
                {
                    if (instance == null)
                    {
                        instance = (T)Activator.CreateInstance(typeof(T), true);
                    }
                }
            }
            return instance;
        }

        public T Resolve<T>()
        {
            var result = default(T);
            try
            {
                result = _container.Resolve<T>();

            }
            catch 
            {
                
            }
            return result;
        }

        #endregion

        #region IServiceProvider Members

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
