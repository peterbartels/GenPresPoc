using System;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace GenPres.Business.ServiceProvider
{
    public abstract class ServiceProvider: IServiceProvider
    {
        
        protected ServiceProvider()
        { }

        #region IServiceProvider Members

        

        protected static T CreateInstance<T>(T instance, object lockThis) where T : class, IServiceProvider
        {
            if (instance == null)
            {
                lock (lockThis)
                {
                    // ReSharper disable ConditionIsAlwaysTrueOrFalse
                    if (instance == null)
                    // ReSharper restore ConditionIsAlwaysTrueOrFalse
                    {
                        instance = (T)Activator.CreateInstance(typeof(T), true);
                    }
                }
            }
            return instance;
        }

        public T Resolve<T>()
        {
            return (T)ObjectFactory.GetInstance(typeof(T));
        }

        #endregion

        #region IServiceProvider Members

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        #endregion

        public void RegisterInstanceOfType<T>(T instance)
        {
            var _registry = new Registry();
            
            _registry.For<T>().Use(instance);

            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry(_registry);
            });

        }
    }
}

