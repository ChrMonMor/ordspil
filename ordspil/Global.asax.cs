using ordspil.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ordspil
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public SecretWordModel Game = new SecretWordModel(4, 4);
        protected void Application_Start()
        {
            Stream stream = File.Open("Game.dat",FileMode.OpenOrCreate);
            stream.Close();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
