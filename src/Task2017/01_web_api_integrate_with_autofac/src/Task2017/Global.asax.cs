using System;
using System.Web;
using System.Web.Http;

namespace Task2017
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var config = GlobalConfiguration.Configuration;

            Bootstrap.Init(config);
        }
    }
}