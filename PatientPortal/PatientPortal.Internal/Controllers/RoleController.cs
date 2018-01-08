﻿using PatientPortal.Domain.LogManager;
using PatientPortal.Domain.Models.AUTHEN;
using PatientPortal.Internal.Models;
using PatientPortal.Provider.Common;
using PatientPortal.Provider.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using PatientPortal.Internal.Common;
using PatientPortal.Utility.Application;
using static PatientPortal.Utility.Application.ApplicationGenerator;
using System.Web;
using WebMarkupMin.AspNet4.Mvc;

namespace PatientPortal.Internal.Controllers
{
    [Authorize]
    [AppHandleError]
    //[CompressContent]
    //[MinifyHtml]
    public class RoleController : Controller
    {
        private readonly IUserSession _userSession;
        private static string controllerName = "Role";

        public RoleController(IUserSession userSession)
        {
            _userSession = userSession;
        }

        // GET: Role
        public async Task<ActionResult> Index()
        {
            var results = new RoleModel();
            try
            {
                //Call API Provider

                //IOwinContext ctx = Request.GetOwinContext();
                //ClaimsPrincipal user = ctx.Authentication.User;
                //IEnumerable<Claim> claims = user.Claims;
                //var claimsIdentity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

                //var a = Request.Cookies["AspnetAuthenticationCookie"];
                //a = Request.Cookies["AspnetAuthenticationCookie"].Value;
                //a = Request.Cookies[".AspNet.ApplicationCookie"].Value;
                //FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(a.Value);


                var token = _userSession.BearerToken;
                var list = await APIProvider.Authorize_Get<List<RoleViewModel>>(token, controllerName, "GetAll", null, APIConstant.API_Resource_Authorize);
                if (list == null)
                    list = new List<RoleViewModel>();

                var role = new RoleViewModel();

                results.lstRoleViewModel = list;
                results.RoleViewModel = role;

                TempData["Data"] = list;
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
            return View(results);
        }

        #region Create
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var strUrl = APIProvider.APIGenerator(controllerName + "/SetRole", APIConstant.ACTION_INSERT);
                var token = _userSession.BearerToken;
                var result = await APIProvider.Authorize_DynamicTransaction<RoleViewModel, string>(model, token, "Role/SetRole", APIConstant.API_Resource_Authorize);

                if (result  == null)
                {
                    TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.SUCCESS, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_INSERT, ApplicationGenerator.TypeResult.SUCCESS));
                }
                else
                {
                    TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_INSERT, ApplicationGenerator.TypeResult.FAIL));
                    Exception ex = new Exception(result);
                    Logger.LogError(ex);
                }

                return RedirectToAction("Index");
            }
            else
            {
                var role = new RoleModel();
                if (TempData["Data"] == null)
                {
                    role.lstRoleViewModel = await APIProvider.Authorize_Get<List<RoleViewModel>>(_userSession.BearerToken, controllerName, APIConstant.API_Resource_Authorize);
                    TempData["Data"] = role.lstRoleViewModel;
                }
                else
                {
                    role.lstRoleViewModel = (List<RoleViewModel>)TempData["Data"];
                }

                role.RoleViewModel = model;

                //TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL, ApplicationGenerator.GeneralActionMessage(ValueConstant.ACTION_INSERT, ApplicationGenerator.TypeResult.FAIL));
                return View("Index", role);
            }
        }
        #endregion

        #region Edit

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            RoleViewModel model;
            try
            {
                //Call API Provider
                var token = _userSession.BearerToken;
                model = await APIProvider.Authorize_Get<RoleViewModel>(token, controllerName, $"Info?id={id}", null, APIConstant.API_Resource_Authorize);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw ex.GetBaseException();
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Edit(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Call API Provider 
                var token = _userSession.BearerToken;
                var result = await APIProvider.Authorize_DynamicTransaction<RoleViewModel, bool>(model, token, "Role/UpdateRole", APIConstant.API_Resource_Authorize);
                if (Response.StatusCode == 200)
                {
                    TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.SUCCESS, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_UPDATE, ApplicationGenerator.TypeResult.SUCCESS));
                }
                else
                {
                    TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_UPDATE, ApplicationGenerator.TypeResult.FAIL));
                }
                return RedirectToAction("Index");
            }
            else
            {
               // TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL, ApplicationGenerator.GeneralActionMessage(ValueConstant.ACTION_UPDATE, ApplicationGenerator.TypeResult.FAIL));
                return View(model);
            }
        }
        #endregion

        #region Delete
        public async Task<ActionResult> Delete(RoleViewModel model)
        {
            if (!await APIProvider.Authorization(_userSession.BearerToken, ARS.Delete))
            {
                TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.FuntionType.Role, APIConstant.ACTION_DELETE);
                return RedirectToAction("Index");
            }
            else
            {
                return PartialView("_Delete", model);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            RoleViewModel model = new RoleViewModel();
            model.Id = id;
            try
            {
                var token = _userSession.BearerToken;
                //Call API Provider - data
                //var model = await APIProvider.Authorize_Get<RoleViewModel>(token, controllerName, $"Info?id={id}", null, APIConstant.API_Resource_Authorize);
                //Call API Provider - Transaction
                var result = await APIProvider.Authorize_DynamicTransaction<RoleViewModel, bool>(model, token, "Role/RemoveRole", APIConstant.API_Resource_Authorize, ARS.IgnoredARS);

                if (result)
                {
                    TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.SUCCESS, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_DELETE, ApplicationGenerator.TypeResult.SUCCESS));
                }
                else
                {
                    TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_DELETE, ApplicationGenerator.TypeResult.FAIL));
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return View("Index");
            }
        }
        #endregion

        #region PermissionRole
        public async Task<ActionResult> Permission(string id)
        {
            List<RolePermissionViewModel> model;
            try
            {
                //Call API Provider
                var token = _userSession.BearerToken;
                model = await APIProvider.Authorize_Get<List<RolePermissionViewModel>>(token, controllerName, $"Permissions?id={id}", null, APIConstant.API_Resource_Authorize, ARS.Get);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw ex.GetBaseException();
            }
            return PartialView("_Detail", model);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePermission(List<RolePermissionViewModel> permissions)
        {
            if (permissions != null)
            {
                try
                {
                    // Filter permission with status is true
                    var model = new List<RolePermission>();
                    foreach (var item in permissions)
                    {
                        if (item.IsUsed)
                        {
                            var tmp = new RolePermission
                            {
                                PermissionId = item.Id,
                                RoleId = item.RoleId
                            };
                            model.Add(tmp);
                        }
                            
                    }

                    //Call API Provider
                    var token = _userSession.BearerToken;
                    var ret = await APIProvider.Authorize_DynamicTransaction<List<RolePermission>, string>(model, token, "Role/UpdatePermission", APIConstant.API_Resource_Authorize);
                    if(ret == null) TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.SUCCESS, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_UPDATE, ApplicationGenerator.TypeResult.SUCCESS));
                    else TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_UPDATE, ApplicationGenerator.TypeResult.FAIL));

                }
                catch (Exception ex)
                {

                    TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_UPDATE, ApplicationGenerator.TypeResult.FAIL));
                    Logger.LogError(ex);
                    throw ex.GetBaseException();
                }
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}