using System;
using System.Collections.Generic;
using System.Linq;

namespace YeTi
{
    public class YeTiContainer
    {
        readonly Dictionary<Type, Type> _registrations = new Dictionary<Type, Type>();
        public void Register<TRegistration, TImplementation>()
        {
            _registrations.Add(typeof(TRegistration), typeof(TImplementation));
        }


        object Resolve(Type type)
        {
            var requestedType = type;
            Type actualType = _registrations[requestedType];
            var ctors = actualType.GetConstructors();
            var ctor = ctors.First();
            IEnumerable<Type> dependencyTypes = ctor.GetParameters().Select(p => p.ParameterType);
            var dependencies = dependencyTypes.Select(d => this.Resolve(d)).ToArray();

            var instance = Activator.CreateInstance(actualType, dependencies);
            return instance;
        }

        public T Resolve<T>()
        {
            return (T) this.Resolve(typeof(T));
        }
    }
}