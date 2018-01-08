﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using PatientPortal.Internal.Models;
using PatientPortal.Provider.Common;
using PatientPortal.Provider.Models;
using PatientPortal.Domain.LogManager;
using PatientPortal.Domain.Models.AUTHEN;
using PatientPortal.Utility.Application;
using PatientPortal.Utility.Common;
using static PatientPortal.Utility.Application.ApplicationGenerator;
using System.Web;
using WebMarkupMin.AspNet4.Mvc;

namespace PatientPortal.Internal.Controllers
{
    [Authorize]
    [AppHandleError]
    //[CompressContent]
    //[MinifyHtml]
    public class EmailMarketingController : Controller
    {
        private const string ControllerName = "EmailMarketing";
        private readonly IUserSession _userSession;

        public EmailMarketingController(IUserSession userSession)
        {
            _userSession = userSession;
        }

        // GET: EmailMarketing
        public async Task<ActionResult> Index()
        {
            try
            {
                var model = new EmailMarketingViewModel();

                var strUrl = ControllerName + APIProvider.APIGenerator(this, this.RouteData.Values["action"].ToString());
                var templates = await APIProvider.Authorize_Get<List<EmailMarketingViewModel>>(_userSession.BearerToken, strUrl, APIConstant.API_Resource_CORE, ARS.Get);

                ViewBag.Templates = templates;

                return View(model);
            }
            catch (HttpException ex)
            {
                Logger.LogError(ex);
                int statusCode = ex.GetHttpCode();
                if (statusCode == 401)
                {
                    TempData["Alert"] = ApplicationGenerator.RenderResult(FuntionType.Department, APIConstant.ACTION_ACCESS);
                    return new HttpUnauthorizedResult();
                }

                throw ex;
            }
        }

        /// <summary>
        /// Send email to group
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Process(EmailMarketingViewModel model)
        {
            model.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
            model.CreatedUser = _userSession.UserName;
            model.ModifiedDate = DateTime.Now.ToString("yyyy-MM-dd");
            model.ModifiedUser = _userSession.UserName;
            if (ModelState.IsValid)
            {
                try
                {
                    //Send email
                    var strUrl = APIProvider.APIGenerator(ControllerName, APIConstant.ACTION_SEND);
                    
                    //Save template
                    if (model.Type == "2")
                    {
                        strUrl = APIProvider.APIGenerator(ControllerName, APIConstant.ACTION_INSERT);
                    }
                    var result = await APIProvider.Authorize_DynamicTransaction<EmailMarketingViewModel, int>(model, _userSession.BearerToken, strUrl, APIConstant.API_Resource_CORE, ARS.Edit);

                    if (result > 0)
                    {
                        TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.SUCCESS, ApplicationGenerator.GeneralActionMessage(ValueConstant.ACTION_SEND, ApplicationGenerator.TypeResult.SUCCESS));
                        if (model.Type == "2")
                            TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.SUCCESS, ApplicationGenerator.GeneralActionMessage(ValueConstant.ACTION_INSERT, ApplicationGenerator.TypeResult.SUCCESS));
                    }
                    else
                    {
                        TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL, ApplicationGenerator.GeneralActionMessage(ValueConstant.ACTION_SEND, ApplicationGenerator.TypeResult.FAIL));
                        if (model.Type == "2")
                            TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL, ApplicationGenerator.GeneralActionMessage(ValueConstant.ACTION_INSERT, ApplicationGenerator.TypeResult.FAIL));
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                    TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.ERROR, ApplicationGenerator.GeneralActionMessage(ValueConstant.ACTION_READ, ApplicationGenerator.TypeResult.ERROR));
                }
                return RedirectToAction("Index");
            }

            var apiUrl = ControllerName + APIProvider.APIGenerator(this, this.RouteData.Values["action"].ToString());
            var templates = await APIProvider.Authorize_Get<List<EmailMarketingViewModel>>(apiUrl, APIConstant.API_Resource_CORE);

            ViewBag.Templates = templates;
            return View("Index", model);
        }

        public async Task<ActionResult> LoadTemplate(int id)
        {
            //Call API Provider get template by id
            var strUrl = ControllerName + APIProvider.APIGenerator(this, this.RouteData.Values["action"].ToString(), id);
            var model = await APIProvider.Authorize_Get<EmailMarketingViewModel>(_userSession.BearerToken, strUrl, APIConstant.API_Resource_CORE, ARS.Get);

            //Call API Provider get all template
            var strUrl2 = ControllerName + APIProvider.APIGenerator(this, this.RouteData.Values["action"].ToString());
            var templates = await APIProvider.Authorize_Get<List<EmailMarketingViewModel>>(_userSession.BearerToken, strUrl2, APIConstant.API_Resource_CORE, ARS.Get);
            ViewBag.Templates = templates;
            
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}