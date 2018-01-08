﻿using PatientPortal.Internal.Models;
using PatientPortal.Provider.Common;
using PatientPortal.Provider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PatientPortal.Utility.Application;
using PatientPortal.Domain.LogManager;
using Newtonsoft.Json;
using PatientPortal.Domain.Models.AUTHEN;
using Microsoft.Owin.Security;
using PatientPortal.Domain.Models.SYSTEM;
using WebMarkupMin.AspNet4.Mvc;

namespace PatientPortal.Internal.Controllers
{
    [Authorize]
    [AppHandleError]
    //[CompressContent]
    //[MinifyHtml]
    public class ModuleController : Controller
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

        public ModuleController(IUserSession userSession)
        {
            _userSession = userSession;
        }
        #endregion

        private static string controllerName = string.Empty;

        #region Get
        public async Task<ActionResult> Index(string group = "1")
        {
            var model = new ModuleModel();
            try
            {
                //Call API Provider
                controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
                var list = await APIProvider.Authorize_Get<List<ModuleViewModel>>(_userSession.BearerToken, controllerName, APIConstant.API_Resource_CORE, ARS.Get);

                model.lstModule = (list == null ? new List<ModuleViewModel>() : list.Where( m => m.Group == group).ToList());

                model.module = new ModuleViewModel();

                ///Category List
                ViewBag.Modules = model.lstModule;
                ViewBag.Group = group;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw ex;
            }
            return View(model);
        }

        public ActionResult ModuleList()
        {
            return PartialView("_List");
        }
        #endregion

        #region Create
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Create(ModuleViewModel model)
        {
            ModelState["ParentId"].Errors.Clear();

            if (ModelState.IsValid)
            {
                //Call API Provider
                string strUrl = APIProvider.APIGenerator(controllerName, APIConstant.ACTION_INSERT);
                var result = await APIProvider.Authorize_DynamicTransaction<ModuleViewModel, bool>(model, _userSession.BearerToken, strUrl, APIConstant.API_Resource_CORE, ARS.IgnoredARS);

                if (result)
                {
                    TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.SUCCESS, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_INSERT, ApplicationGenerator.TypeResult.SUCCESS));
                }
                else
                {
                    TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_INSERT, ApplicationGenerator.TypeResult.FAIL));
                }
                return RedirectToAction("Index", new {group  = model.Group});
            }
            else
            {
                var modules = new ModuleModel();
                if(TempData["Data"] != null)
                {
                    modules.lstModule = (List<ModuleViewModel>)TempData["Data"];
                }
                else
                {
                    var list = await APIProvider.Authorize_Get<List<ModuleViewModel>>(_userSession.BearerToken, controllerName, APIConstant.API_Resource_CORE, ARS.IgnoredARS);
                    modules.lstModule = list;
                }
                modules.module = model;

                ViewBag.Modules = modules.lstModule;
                TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_INSERT, ApplicationGenerator.TypeResult.FAIL));
                return View("Index", modules);
            }

        }
        #endregion

        #region Edit
        [HttpGet]
        public async Task<ActionResult> Edit(short id)
        {
            if (!await APIProvider.Authorization(_userSession.BearerToken, ARS.Edit))
            {
                TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.FuntionType.Module, APIConstant.ACTION_UPDATE);
                return RedirectToAction("Index");
            }
            else
            {
                var model = new ModuleViewModel();
                try
                {
                    //Call API Provider
                    string action = "Get";
                    var list = await APIProvider.Authorize_Get<List<ModuleViewModel>>(_userSession.BearerToken, controllerName, APIConstant.API_Resource_CORE, ARS.IgnoredARS);
                    string strUrl = controllerName + '/' + action + '/' + id;
                    model = await APIProvider.Authorize_Get<ModuleViewModel>(_userSession.BearerToken, strUrl, APIConstant.API_Resource_CORE, ARS.IgnoredARS);


                    ViewBag.Modules = list.Where(x => x.Id != id && x.Group == model.Group);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex);
                    throw ex;
                }
                return View(model);
            }
        }

        /// <summary>
        /// Save change category
        /// </summary>
        /// <param name="model"></param>
        /// <param name="fileUpload"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Edit(ModuleViewModel model)
        {
            //Ignored
            ModelState["ParentId"].Errors.Clear();

            if (ModelState.IsValid)
            {
                //Call API Provider 
                string strUrl = APIProvider.APIGenerator(controllerName, APIConstant.ACTION_UPDATE);
                var result = await APIProvider.Authorize_DynamicTransaction<ModuleViewModel, bool>(model, _userSession.BearerToken, strUrl, APIConstant.API_Resource_CORE, ARS.IgnoredARS);

                if (result)
                {
                    TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.SUCCESS, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_UPDATE, ApplicationGenerator.TypeResult.SUCCESS));
                }
                else
                {
                    TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_UPDATE, ApplicationGenerator.TypeResult.FAIL));
                }
                return RedirectToAction("Index", new { group = model.Group });
            }
            else
            {
                TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.ERROR, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_UPDATE, ApplicationGenerator.TypeResult.ERROR));
                return RedirectToAction("Edit", model.Id);

            }
            
        }

        #endregion

        #region Delete
        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(ModuleViewModel model)
        {
            if (!await APIProvider.Authorization(_userSession.BearerToken, ARS.Edit))
            {
                TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.FuntionType.Module, APIConstant.ACTION_UPDATE);
                return RedirectToAction("Index");
            }
            else
            {
                return PartialView("_Delete", model);
            }
        }

        /// <summary>
        /// Delete an object category
        /// </summary>
        /// <param name="model">object of category</param>
        /// <returns>Success: return index category - Fail: return Edit</returns>
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Delete(short id)
        {
            var model = new ModuleViewModel();
            model.Id = id;
            //Check is used
            string strUrl = controllerName + "/CheckIsUsed" + "/" + id;
            //string strUrl = APIProvider.APIGenerator(controllerName, "CheckIsUsed") + "/" + model.Id;
            var checkIsUsed = await APIProvider.Authorize_Get<bool>(_userSession.BearerToken, strUrl, APIConstant.API_Resource_CORE, ARS.IgnoredARS);

            if (!checkIsUsed)
            {
                
                //Call API Provider - Transaction
                string apiUrl = APIProvider.APIGenerator(controllerName, APIConstant.ACTION_DELETE);
                var result = await APIProvider.Authorize_DynamicTransaction<ModuleViewModel, bool>(model, _userSession.BearerToken, apiUrl, APIConstant.API_Resource_CORE, ARS.IgnoredARS);

                if (result)
                {
                    TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.SUCCESS, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_DELETE, ApplicationGenerator.TypeResult.SUCCESS));
                }
                else
                {
                    TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.ISUSED, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_DELETE, ApplicationGenerator.TypeResult.ISUSED));
                }
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.ERROR, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_DELETE, ApplicationGenerator.TypeResult.ISUSED));
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region Check Exist
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> CheckExist(string title, short id)
        {
            //Call API Provider - Get data
            string apiUrl = APIProvider.APIGenerator(controllerName,new List<string> { "id", "title"}, false, id, title.Trim());
            var isExist = await APIProvider.Authorize_Get<bool>(_userSession.BearerToken, apiUrl, APIConstant.API_Resource_CORE, ARS.IgnoredARS);

            return Json(!isExist, JsonRequestBehavior.AllowGet);
        }
        #endregion


        public ActionResult _Nav()
        {
            ModuleShared lstModel = new ModuleShared();


            string strUrl = APIProvider.APIGenerator("Module", "Initial", new List<string> { "group" }, true, APIConstant.MODULE_INTERNAL);
            var lstModule = APIProvider.Authorize_GetNonAsync<List<ModuleApplication>>(_userSession.BearerToken, strUrl, APIConstant.API_Resource_CORE, ARS.IgnoredARS);
            if (lstModule != null)
            {
                lstModel.lstModuleParent = lstModule.Where(x => x.ParentId == 0).ToList();
                lstModel.lstModuleItem = lstModule.Where(x => x.ParentId != 0).ToList();
            }

            return PartialView("_Nav", lstModel);
        }
    }
}