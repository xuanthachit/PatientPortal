﻿using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using PatientPortal.CMS.Models;
using PatientPortal.Domain.Caching.MemCache;
using PatientPortal.Domain.LogManager;
using PatientPortal.Domain.Models.AUTHEN;
using PatientPortal.Domain.Models.CMS;
using PatientPortal.Domain.Models.CMS.Report;
using PatientPortal.Domain.Models.SYSTEM;
using PatientPortal.Provider.Common;
using PatientPortal.Provider.Models;
using PatientPortal.Utility.Files;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebMarkupMin.AspNet4.Mvc;

namespace PatientPortal.CMS.Controllers
{
    [Authorize]
    [AppHandleError]
    public class HomeController : Controller
    {
        #region Authentication Manager
        private readonly IUserSession _userSession;
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public HomeController(IUserSession userSession)
        {
            _userSession = userSession;
        }
        #endregion

        //[CompressContent]
        //[MinifyHtml]
        //[OutputCache(CacheProfile = "CacheCompressedContent5Minutes")]
        public async Task<ActionResult> Index()
        {
            //Get module list & store cookie
            try
            {
                //string strUrl = APIProvider.APIGenerator("Module", "Initial", new List<string> { "group" }, true, APIConstant.MODULE_CMS);
               // var lstModule = await APIProvider.Authorize_Get<List<ModuleApplication>>(_userSession.BearerToken, strUrl, APIConstant.API_Resource_CORE, ARS.IgnoredARS);

                //CookieStore.Create(APIConstant.COOKIE_MODULE + APIConstant.MODULE_CMS, lstModule, HttpContext.ApplicationInstance.Context);
                var strUrl = APIProvider.APIGenerator("Post", "Counter", null);
                var lstCounter = await APIProvider.Authorize_Get<List<DashboardCounter>>(_userSession.BearerToken, strUrl, APIConstant.API_Resource_CMS, ARS.IgnoredARS);
                
                return View(lstCounter);
            }
            catch (System.Exception ex)
            {
                System.Exception sa = new System.Exception(ex.Message + " - " + _userSession.BearerToken + " - " + _userSession.UserId);
                Logger.LogError(sa);
                throw ex;
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //[ChildActionOnly]
        public async Task<PartialViewResult> _DashboardCounter()
        {
            string strUrl = APIProvider.APIGenerator("Dashboard", "Counter", null);
            var lstCounter = await APIProvider.Authorize_Get<List<DashboardCounter>>(_userSession.BearerToken, strUrl, APIConstant.API_Resource_CMS, ARS.IgnoredARS);

            return PartialView("_Counter", lstCounter);
        }

        public PartialViewResult _Notification()
        {
            var result = new List<SystemNotificationViewModel>();
            string apiNotificationUrl = APIProvider.APIGenerator("SystemNotification", new List<string> { "userId", "sendFrom", "numTop" }, true, _userSession.UserId, "", 5);

            var data = APIProvider.Authorize_GetNonAsync<SystemNotificationModel>(_userSession.BearerToken, apiNotificationUrl, APIConstant.API_Resource_CORE, ARS.IgnoredARS);
            if (data != null)
            {
                result = data.lstUserNotificationViewModel;
            }

            return PartialView(result);
        }

        public ActionResult GetListNotification()
        {
            //return partial view instead of View   
            var result = new List<SystemNotificationViewModel>();
            string apiNotificationUrl = APIProvider.APIGenerator("SystemNotification", new List<string> { "userId", "sendFrom", "numTop" }, true, _userSession.UserId, string.Empty, 5);

            var data = APIProvider.Authorize_GetNonAsync<SystemNotificationModel>(_userSession.BearerToken, apiNotificationUrl, APIConstant.API_Resource_CORE, ARS.IgnoredARS);
            if (data != null)
            {
                result = data.lstUserNotificationViewModel;
            }

            return PartialView("_ListNotification", result);
        }
    }
}