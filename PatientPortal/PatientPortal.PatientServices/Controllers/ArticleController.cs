﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PatientPortal.PatientServices.Common;
using PatientPortal.PatientServices.Models;
using PatientPortal.Provider.Common;
using PatientPortal.Provider.Models;
using PatientPortal.Domain.LogManager;
using PatientPortal.Domain.Models.AUTHEN;
using static PatientPortal.PatientServices.Common.ValueConstant;
using PatientPortal.Utility.Application;
using System.Web;
using static PatientPortal.Utility.Application.ApplicationGenerator;
using WebMarkupMin.AspNet4.Mvc;

namespace PatientPortal.PatientServices.Controllers
{
    [Authorize]
    [AppHandleError]
    //[CompressContent]
    //[MinifyHtml]
    public class ArticleController : Controller
    {
        #region declare variable
        private readonly IUserSession _userSession;
        private static string controllerName = string.Empty;
        #endregion

        #region Constructor
        public ArticleController(IUserSession userSession)
        {
            _userSession = userSession;
        }
        #endregion

        #region Read
        // GET: QAMedical
        public async Task<ActionResult> Index(byte type = 0)
        {
            //Set controller name
            controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            //Result
            var model = new List<ArticleViewModel>();
            try
            {
                //Call API Provider
                string patientId = _userSession.UserId;
                string apiUrl = APIProvider.APIGenerator(controllerName, new List<string> { nameof(patientId) }, true, patientId);
                var list = await APIProvider.Authorize_Get<List<ArticleViewModel>>(_userSession.BearerToken, apiUrl, APIConstant.API_Resource_CORE, ARS.Get);

                if (type == 1 && list != null)
                {
                    //Get list Unanswer
                    list = list.Where(i => i.CountComments == 0).ToList();
                }
                ViewBag.TypePage = type;
                model = list != null ? list.OrderByDescending(i=>i.Date).ToList() : model;
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

            return View(model);
        }

        #endregion

        #region Create
        public async Task<ActionResult> Create()
        {
            var token = _userSession.BearerToken;
            if (!await APIProvider.Authorization(token, ARS.Insert))
            {
                TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.FuntionType.Article, APIConstant.ACTION_INSERT);
                return RedirectToAction("Index");
            }
            else
            {
                ArticleViewModel model = new ArticleViewModel();
                return View(model);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Create(ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Set default some fields
                model.PatientId = _userSession.UserId;
                model.Status = (byte) ValueConstant.ArticleStatus.JustCreated;
                model.IsClosed = false;
                model.Date = DateTime.Now;

                //Call API Provider
                string strUrl = APIProvider.APIGenerator(controllerName, APIConstant.ACTION_INSERT);
                var result = await InvokeTransaction(model, strUrl);

                if (result > 0)
                {
                    //Successful
                    ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.SUCCESS, ApplicationGenerator.GeneralActionMessage(ValueConstant.ACTION_INSERT, ApplicationGenerator.TypeResult.SUCCESS));
                }
                else
                {
                    //Failed
                    ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL, ApplicationGenerator.GeneralActionMessage(ValueConstant.ACTION_INSERT, ApplicationGenerator.TypeResult.FAIL));
                }
                return RedirectToAction("Index");
                
            }
            else
            {
                return View(model);
            }

        }
        #endregion

        #region Edit
        public async Task<ActionResult> Edit(int id)
        {
            //Call API Provider - Get data

            if (!await APIProvider.Authorization(_userSession.BearerToken, ARS.Edit))
            {
                TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.FuntionType.Account, APIConstant.ACTION_UPDATE);
                return RedirectToAction("Index");
            }
            else
            {
                var model = await GetArticle(id);
                return View("Edit", model);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Edit(ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Date = DateTime.Now;
                model.PatientId = _userSession.UserId;

                //Call API Provider 
                var strUrl = APIProvider.APIGenerator(controllerName, APIConstant.ACTION_UPDATE);
                var result = await InvokeTransaction(model, strUrl);

                if (result > 0)
                {
                    ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.SUCCESS, ApplicationGenerator.GeneralActionMessage(ValueConstant.ACTION_UPDATE, ApplicationGenerator.TypeResult.SUCCESS));
                }
                else
                {
                    ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL, ApplicationGenerator.GeneralActionMessage(ValueConstant.ACTION_UPDATE, ApplicationGenerator.TypeResult.FAIL));
                }
                return RedirectToAction("Index");
            }
            else
            {

                ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL, ApplicationGenerator.GeneralActionMessage(ValueConstant.ACTION_UPDATE, ApplicationGenerator.TypeResult.FAIL));
                return View(model);
            }
        }
        #endregion

        #region Details
        public async Task<ActionResult> Details(int id)
        {
            if (!await APIProvider.Authorization(_userSession.BearerToken, ARS.Detail))
            {
                TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.FuntionType.Article, APIConstant.ACTION_UPDATE);
                return RedirectToAction("Index");
            }
            else
            {
                ArticlesModel model = new ArticlesModel();
                var article = await GetArticle(id);
                var comments = await GetComments(id);

                model.ArticleViewModel = article;
                model.lstArticleCommentModel = comments;

                model.ArticleCommentModel = new ArticleCommentViewModel()
                {
                    CreatedUser = _userSession.UserId,
                    ArticleId = id
                };
                return View(model);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Details(ArticlesModel model)
        {
            var comment = model.ArticleCommentModel;
            comment.Status = (byte)ArticleStatus.JustCreated;
            comment.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                string strUrl = APIProvider.APIGenerator(controllerName + "/Comment", APIConstant.ACTION_INSERT);
                var result = await InvokeTransaction(comment, strUrl);
                if (result > 0)
                {
                    return RedirectToAction("Details", new { id = comment.ArticleId });
                }
                else
                {
                    model = await SetArticleComment(comment);
                    return View("Details", model);
                }
            }
            else
            {
                model = await SetArticleComment(comment);
                return View("Details", model);
            }
        }
        #endregion

        #region Delete
        public async Task<ActionResult> Delete(ArticleViewModel model)
        {
            if (!await APIProvider.Authorization(_userSession.BearerToken, ARS.Delete))
            {
                TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.FuntionType.Article, APIConstant.ACTION_DELETE);
                return RedirectToAction("Index");
            }
            else
            {
                return PartialView("_Delete", model);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                //Call API Provider - Get data
                var apiUrl = controllerName + APIProvider.APIGenerator(this, this.RouteData.Values["action"].ToString(), id);
                var model = await GetArticle(id);

                //Call API Provider - Transaction
                apiUrl = APIProvider.APIGenerator(controllerName, APIConstant.ACTION_DELETE);
                var result = await InvokeTransaction(model, apiUrl);

                if (result > 0)
                {
                    ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.SUCCESS, ApplicationGenerator.GeneralActionMessage(ValueConstant.ACTION_DELETE, ApplicationGenerator.TypeResult.SUCCESS));
                    return RedirectToAction("Index");
                }
                else
                {
                    ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL, ApplicationGenerator.GeneralActionMessage(ValueConstant.ACTION_DELETE, ApplicationGenerator.TypeResult.FAIL));
                    return RedirectToAction("Detail", new { id = model.Id });
                }

            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL, ApplicationGenerator.GeneralActionMessage(ValueConstant.ACTION_DELETE, ApplicationGenerator.TypeResult.FAIL));
                return View("Index");
            }
        }
        #endregion

        #region Private Methods
        private async Task<ArticleViewModel> GetArticle(int id)
        {
            var apiUrl = controllerName + APIProvider.APIGenerator(this, this.RouteData.Values["action"].ToString(), id);
            var model = await APIProvider.Authorize_Get<ArticleViewModel>( _userSession.BearerToken, apiUrl, APIConstant.API_Resource_CORE, ARS.Edit);
            return model;
        }
        
        private async Task<List<ArticleCommentViewModel>> GetComments(int idArticle)
        {
            var apiUrl = controllerName + "/GetAllComment?idArticle=" + Convert.ToString(idArticle);
            var model = await APIProvider.Authorize_Get<List<ArticleCommentViewModel>>(_userSession.BearerToken, apiUrl, APIConstant.API_Resource_CORE, ARS.Edit);
            return model;
        }

        private async Task<ArticlesModel> SetArticleComment(ArticleCommentViewModel comment)
        {
            ArticlesModel articlecomment = new ArticlesModel();
            var article = await GetArticle(comment.ArticleId);
            var comments = await GetComments(comment.ArticleId);

            articlecomment.ArticleViewModel = article;
            articlecomment.lstArticleCommentModel = comments;

            articlecomment.ArticleCommentModel = comment;

            return articlecomment;
        }

        private async Task<int> InvokeTransaction(ArticleViewModel model, string strUrl)
        {
            var result =
                await APIProvider.Authorize_DynamicTransaction<ArticleViewModel, int>(model, _userSession.BearerToken, strUrl, APIConstant.API_Resource_CORE, ARS.IgnoredARS);
            return result;
        }

        private async Task<int> InvokeTransaction(ArticleCommentViewModel model, string strUrl)
        {
            var result =
                await APIProvider.Authorize_DynamicTransaction<ArticleCommentViewModel, int>(model, _userSession.BearerToken, strUrl, APIConstant.API_Resource_CORE, ARS.IgnoredARS);
            return result;
        }
        
        #endregion
    }
}