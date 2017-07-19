using System;
using System.Linq;
using System.Reflection;
using Manualfac.Services;

namespace Manualfac.Activators
{
    class ReflectiveActivator : IInstanceActivator
    {
        readonly Type serviceType;

        public ReflectiveActivator(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        #region Please modify the following code to pass the test

        /*
         * This method create instance via reflection. Try evaluating its parameters and
         * inject them using componentContext.
         *
         * No public members are allowed to add.
         */

        public object Activate(IComponentContext componentContext)
        {
            try
            {
                var parameters = serviceType.GetConstructors().Single()
                    .GetParameters()
                    .Select(p => componentContext.ResolveComponent(new TypedService(p.ParameterType)))
                    .ToArray();
                return Activator.CreateInstance(serviceType, parameters);
            }
            catch (Exception e)
            {
                throw new DependencyResolutionException(e.StackTrace);
            }
        }

        #endregion
    }
}