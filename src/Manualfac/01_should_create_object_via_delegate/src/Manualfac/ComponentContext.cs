using System;
using System.Collections.Generic;

namespace Manualfac
{
    public class ComponentContext : IComponentContext
    {
        #region Please modify the following code to pass the test

        /*
         * A ComponentContext is used to resolve a component. Since the component
         * is created by the ContainerBuilder, it brings all the registration
         * information.
         *
         * You can add non-public member functions or member variables as you like.
         */
        readonly Dictionary<Type, Func<IComponentContext, object>> registry;

        internal ComponentContext(Dictionary<Type, Func<IComponentContext, object>> store)
        {
            if (store == null) throw new ArgumentNullException(nameof(store));

            registry = store;
        }

        public object ResolveComponent(Type type)
        {
            if (registry.ContainsKey(type))
            {
                return registry[type](this);
            }
            throw new DependencyResolutionException();
        }

        #endregion
    }
}