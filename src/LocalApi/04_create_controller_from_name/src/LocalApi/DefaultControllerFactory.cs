using System;
using System.Collections.Generic;
using System.Linq;

namespace LocalApi
{
    class DefaultControllerFactory : IControllerFactory
    {
        public HttpController CreateController(
            string controllerName,
            ICollection<Type> controllerTypes,
            IDependencyResolver resolver)
        {
            #region Please modify the following code to pass the test.

            /*
             * The controller factory will create controller by its name. It will search
             * form the controllerTypes collection to get the correct controller type,
             * then create instance from resolver.
             */

            throw new NotImplementedException();

            var count = controllerTypes.Count(e => e.Name == controllerName);
            if (count > 1) throw new ArgumentException();

            var type = controllerTypes.SingleOrDefault(e => e.Name == controllerName);
            if (type == null) return null;

            return resolver.GetService(type) == null ? null : (HttpController) Activator.CreateInstance(type);

            #endregion
        }
    }
}
