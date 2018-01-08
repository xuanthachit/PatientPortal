﻿using Microsoft.Owin.Security;
using PatientPortal.Domain.Models.AUTHEN;
using PatientPortal.Internal.Models;
using PatientPortal.Provider.Common;
using PatientPortal.Provider.Models;
using PatientPortal.Utility.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PatientPortal.Internal.Controllers
{
    public class NotificationController : Controller
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

        public NotificationController(IUserSession userSession)
        {
            _userSession = userSession;
        }
        #endregion

        // GET: Notification
        public ActionResult Index()
        {
            return View(new SystemNotificationEditModel());
        }
        [HttpPost]
        public async Task<JsonResult> Push(SystemNotificationEditModel model)
        {
            if (ModelState.IsValid)
            {
                //Call API Provider
                string strUrl = APIProvider.APIGenerator("SystemNotification", APIConstant.ACTION_INSERT);
                model.SendFrom = _userSession.UserId;
                var result = await APIProvider.Authorize_DynamicTransaction<SystemNotificationEditModel, bool>(model, _userSession.BearerToken, strUrl, APIConstant.API_Resource_CORE, ARS.Insert);
                if (result)
                {
                    //Call SignalR
                    //Return message
                    var alert = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.SUCCESS, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_INSERT, ApplicationGenerator.TypeResult.SUCCESS));
                    return Json(new { IsSuccess = true,Message = model.Detail, Data = alert.ToHtmlString() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var alert = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_INSERT, ApplicationGenerator.TypeResult.FAIL));
                    return Json(new { IsSuccess = false, Data = alert.ToHtmlString() }, JsonRequestBehavior.AllowGet);
                }

                
            }
            else
                return Json(new { IsResult = false, Data = model }, JsonRequestBehavior.AllowGet);
        }
    }
}