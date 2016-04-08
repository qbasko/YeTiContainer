using System;
using System.Collections.Generic;

namespace YeTi
{
    public class YeTiContainer
    {
        readonly Dictionary<Type, Type> _registrations = new Dictionary<Type, Type>();
        public void Register<TRegistration, TImplementation>()
        {
            _registrations.Add(typeof(TRegistration), typeof(TImplementation));
        }

        public T Resolve<T>()
        {
            var requestedType = typeof(T);
            Type actualType = _registrations[requestedType];
            var instance = Activator.CreateInstance(actualType);
            return (T)instance;
        }
    }
}