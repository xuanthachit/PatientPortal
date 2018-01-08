﻿using PatientPortal.Domain.LogManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using PatientPortal.PatientServices.Binders;
using System.Web.Http;
using System.Web.Helpers;
using System.Security.Claims;
using System.IO.Compression;

namespace PatientPortal.PatientServices
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            // anti-forgery token support with claims-based authentication
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimsIdentity.DefaultNameClaimType;

            //Log4Net
            log4net.Config.XmlConfigurator.Configure();

            //ModelBinder
            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new DateTimeModelBinder());

            //Autofac
            AutofacConfig.RegisterDependencies();
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            if (ex is ThreadAbortException)
                return;
            Logger.LogError(ex);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

            //// Implement HTTP compression
            //HttpApplication app = (HttpApplication)sender;


            //// Retrieve accepted encodings
            //string encodings = app.Request.Headers.Get("Accept-Encoding");
            //if (encodings != null)
            //{
            //    // Check the browser accepts deflate or gzip (deflate takes preference)
            //    encodings = encodings.ToLower();
            //    if (encodings.Contains("deflate"))
            //    {
            //        app.Response.Filter = new DeflateStream(app.Response.Filter, CompressionMode.Compress);
            //        app.Response.AppendHeader("Content-Encoding", "deflate");
            //    }
            //    else if (encodings.Contains("gzip"))
            //    {
            //        app.Response.Filter = new GZipStream(app.Response.Filter, CompressionMode.Compress);
            //        app.Response.AppendHeader("Content-Encoding", "gzip");
            //    }
            //}
        }
    }
}