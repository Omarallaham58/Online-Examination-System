using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace ProjectV1
{
    public class Global : HttpApplication
    {
        public static DBHelper dbHelper;
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            dbHelper=new DBHelper();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}