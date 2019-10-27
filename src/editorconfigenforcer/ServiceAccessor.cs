using System;

namespace EditorConfigEnforcer
{
    public class ServiceAccessor
    {
        private readonly Func<Type, object> _func;

        public ServiceAccessor(Func<Type, object> func)
        {
            _func = func;
        }

        public object GetService(Type serviceType) => _func(serviceType);
    }
}