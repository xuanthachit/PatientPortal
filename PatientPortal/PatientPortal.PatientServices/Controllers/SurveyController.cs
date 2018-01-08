﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using PatientPortal.PatientServices.Models.Survey;
using PatientPortal.Provider.Common;
using PatientPortal.Provider.Models;
using PatientPortal.Utility.Application;
using System.Web;
using PatientPortal.Domain.LogManager;
using static PatientPortal.Utility.Application.ApplicationGenerator;
using WebMarkupMin.AspNet4.Mvc;
using PatientPortal.PatientServices.Models;

namespace PatientPortal.PatientServices.Controllers
{
    [Authorize]
    [AppHandleError]
    //[CompressContent]
    //[MinifyHtml]
    public class SurveyController : Controller
    {
        private const string ControllerName = "Survey";
        // GET: Survey
        public async Task<ActionResult> Index()
        {
            try
            {
                var model = await APIProvider.Get<List<SurveyModel>>(ControllerName, APIConstant.API_Resource_CORE);
                if (model == null) model = ApplicationGenerator.GetObject<List<SurveyModel>>();
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

        [HttpPost]
        public ActionResult Send(SurveyModel model)
        {
            return RedirectToAction("Index");
        }
    }
}