﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LocalApi
{
    class DefaultDependencyResolver : IDependencyResolver
    {
        #region Please modify the following code to pass the test

        /*
         * The dependency resolver is kind of a IoC service to create instances for
         * specified types as well as managing their lifetime. In this practice, it
         * it only used as a simple factory. No lifetime management is provided.
         *
         * Since the controller is currently the entry point for handling request,
         * The default dependency resolver implementation will scan all the public
         * non-abstract classes which implement HttpController interface (using
         * DefaultHttpControllerTypeResolver).
         *
         * The GetService method will try creating instance for specified types by
         * directly calling its default constructor. So any controller without a
         * default constructor will not be supported.
         */

        HashSet<Type> controllerTypes;

        internal DefaultDependencyResolver(IEnumerable<Type> controllerTypes)
        {
            this.controllerTypes = new HashSet<Type>(controllerTypes);
        }

        public void Dispose()
        {
            controllerTypes = null;
        }

        public object GetService(Type type)
        {
            return controllerTypes.Any(e => e == type) ? Activator.CreateInstance(type) : null;
        }

        #endregion
    }
}