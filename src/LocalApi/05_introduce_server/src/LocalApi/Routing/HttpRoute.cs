using System;
using System.CodeDom.Compiler;
using System.Net.Http;
using Microsoft.CSharp;

namespace LocalApi.Routing
{
    public class HttpRoute
    {
        public HttpRoute(string controllerName, string actionName, HttpMethod methodConstraint) :
            this(controllerName, actionName, methodConstraint, null)
        {
        }

        #region Please modifies the following code to pass the test

        /*
         * You can add non-public helper method for help, but you cannot change public
         * interfaces.
         */

        static readonly CodeDomProvider CodeProvider = new CSharpCodeProvider();

        public HttpRoute(string controllerName, string actionName, HttpMethod methodConstraint, string uriTemplate)
        {
            CheckIdentifier(controllerName, nameof(controllerName));
            CheckIdentifier(actionName, nameof(actionName));
            CheckNull(methodConstraint, nameof(methodConstraint));

            ControllerName = controllerName;
            ActionName = actionName;
            MethodConstraint = methodConstraint;
            UriTemplate = uriTemplate;
        }

        static void CheckIdentifier(string parameter, string name)
        {
            CheckNull(parameter, name);

            if (!CodeProvider.IsValidIdentifier(parameter)) throw new ArgumentException(name);
        }

        static void CheckNull(object parameter, string name)
        {
            if (parameter == null) throw new ArgumentNullException(name);
        }

        #endregion

        public string ControllerName { get; }
        public string ActionName { get; }
        public HttpMethod MethodConstraint { get; }
        public string UriTemplate { get; }

        public bool IsMatch(Uri uri, HttpMethod method)
        {
            if (uri == null) { throw new ArgumentNullException(nameof(uri)); }
            if (method == null) { throw new ArgumentNullException(nameof(method)); }
            string path = uri.AbsolutePath.TrimStart('/');
            return path.Equals(UriTemplate, StringComparison.OrdinalIgnoreCase) &&
                   method == MethodConstraint;
        }
    }
}