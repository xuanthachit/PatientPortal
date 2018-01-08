﻿using System.Web;
using System.Web.Optimization;

namespace PatientPortal.SPA.CMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Scripts/jquery.js",
                        "~/Scripts/owl.carousel.min.js",
                        "~/Scripts/mousescroll.js",
                        "~/Scripts/smoothscroll.js",
                        "~/Scripts/jquery.prettyPhoto.js",
                        "~/Scripts/jquery.isotope.min.js",
                        "~/Scripts/jquery.inview.min.js",
                        "~/Scripts/wow.min.js",
                        "~/Scripts/main.js"));
            //bundles.Add(new ScriptBundle("~/bundles/app").Include(
            //            "~/Scripts/main.js"));
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                         "~/Scripts/angular.js", 
                         "~/Scripts/angular-mocks.js",
                         "~/Scripts/angular-route.js",
                         "~/Scripts/angular-resource.js",
                         "~/Scripts/angular-cookies.js",
                         "~/Scripts/angular-aria.min.js",
                         "~/Scripts/angular-sanitize.min.js",
                         "~/Scripts/angular-animate.min.js",
                         "~/Scripts/angular-ui/ui-bootstrap-tpls.min.js"
                         ));
            bundles.Add(new ScriptBundle("~/bundles/src").Include(
                         "~/www/app/app.js",
                         "~/www/app/service/params.service.js",
                         "~/www/app/service/postdetail.service.js",
                         "~/www/app/controller/postdetail.controller.js",
                         "~/www/app/service/index.service.js",
                         "~/www/app/controller/index.controller.js",
                         "~/www/app/service/appointment.service.js",
                         "~/www/app/controller/appointment.controller.js",
                         "~/www/app/service/function.array.js"
                         ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/animate.min.css",
                      "~/Content/owl.carousel.css",
                      "~/Content/owl.transitions.css",
                      "~/Content/prettyPhoto.css",
                      "~/Content/main.css",
                      "~/Content/responsive.css"));
        }
    }
}