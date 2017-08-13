using System;
using System.Web;
using System.Web.Http;
using Serilog;

namespace Tasks2017
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var config = GlobalConfiguration.Configuration;

            new Bootstrap(config).Init();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            Log.CloseAndFlush();
        }
    }
}