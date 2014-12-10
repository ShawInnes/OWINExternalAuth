﻿using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Serilog;
using Serilog.Extras.Web.Enrichers;

namespace MiniApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Seq("http://localhost:5341/")
                .Enrich.WithProperty("System", "Api")
                .Enrich.With<HttpRequestIdEnricher>()
                .Enrich.With<UserNameEnricher>()
                .Enrich.FromLogContext()
                .Destructure.UsingAttributes()
                .CreateLogger();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}