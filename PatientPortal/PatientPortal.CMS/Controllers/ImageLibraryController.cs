﻿using PatientPortal.CMS.Common;
using PatientPortal.CMS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PatientPortal.Utility.Files;
using PatientPortal.Utility.Application;
using PatientPortal.Domain.LogManager;
using PatientPortal.Domain.Models.AUTHEN;
using PatientPortal.Provider.Common;
//using WebMarkupMin.AspNet4.Mvc;

namespace PatientPortal.CMS.Controllers
{
    [Authorize]
    [AppHandleError]
    //[CompressContent]
    //[MinifyHtml]
    public class ImageLibraryController : Controller
    {
        private static string folderUpload = string.Empty;
        private readonly IUserSession _userSession;

        public ImageLibraryController(IUserSession userSession)
        {
            _userSession = userSession;
        }

        // GET: Gallery
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MenuFileManagerPartial()
        {
            return PartialView();
        }

        public ActionResult GalleryPartial(string fullPath)
        {
            if (!string.IsNullOrEmpty(fullPath))
            {
                string path = ValueConstant.FOLDER_SEPARATOR;
                string[] arr = fullPath.Split('\\');
                int index = Array.IndexOf(arr, ValueConstant.ROOT_FOLDER);
                for (int i = index; i < arr.Length; i++)
                {
                    path += arr[i].ToString() + ValueConstant.FOLDER_SEPARATOR;
                }

                var appData = Server.MapPath(path);
                folderUpload = fullPath;
                var image = Directory.GetFiles(appData).Select(x => new FileViewModel
                {
                    Url = Url.Content(fullPath + Path.GetFileName(x)),
                });
                return PartialView(image);
            }
            else
            {
                var mode = new FileViewModel();
                return PartialView(mode);
            }
        }

        public void PopulateTree(string dir, JsTreeModel node)
        {
            if (node.children == null)
            {
                node.children = new List<JsTreeModel>();
            }
            
            DirectoryInfo directory = new DirectoryInfo(dir);
            
            foreach (DirectoryInfo d in directory.GetDirectories())
            {
                JsTreeModel t = new JsTreeModel();

                string path = ValueConstant.FOLDER_SEPARATOR;
                string[] arr = d.FullName.Split('\\');
                int index = Array.IndexOf(arr, ValueConstant.ROOT_FOLDER);
                for(int i = index; i < arr.Length; i++)
                {
                    path += arr[i].ToString() + ValueConstant.FOLDER_SEPARATOR;
                }

                t.id = path;
                t.text = d.Name.ToString();
                t.state = new StateNode();
                t.state.opened = true;
                
                PopulateTree(d.FullName, t);
                node.children.Add(t);
            }
        }

        public bool AlreadyPopulated
        {
            get
            {
                return (Session["AlreadyPopulated"] == null ? false : (bool)Session["AlreadyPopulated"]);
            }
            set
            {
                Session["AlreadyPopulated"] = (bool)value;
            }

        }

        [HttpPost]
        public ActionResult CreateFolder(string path, string newname)
        {
            try
            {
                string mapPath = path + ValueConstant.FOLDER_SEPARATOR + newname;
                Directory.CreateDirectory(mapPath);
                AlreadyPopulated = false;
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult RenameFolder(string path, string newname, string oldname)
        {
            try
            {
                if (!string.IsNullOrEmpty(newname))
                {
                    string oldDirectory = Server.MapPath(path + ValueConstant.FOLDER_SEPARATOR + oldname);
                    string newDirectory = Server.MapPath(path + ValueConstant.FOLDER_SEPARATOR + newname);
                    Directory.Move(oldDirectory, newDirectory);
                    AlreadyPopulated = false;
                    return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult GetTreeData()
        {
            JsTreeModel rootNode = new JsTreeModel();
            rootNode.text = "images";
            rootNode.state = new StateNode();
            rootNode.state.opened = true;

            string rootPath = Request.MapPath(ValueConstant.IMAGE_PATH);
            rootNode.id = rootPath;
            PopulateTree(rootPath, rootNode);
            AlreadyPopulated = true;
            return Json(rootNode, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase fileUpload)
        {
            try
            {
                if (!string.IsNullOrEmpty(folderUpload))
                {
                    if (fileUpload != null)
                    {
                        string name = "";
                        FileManagement.UploadImage(fileUpload, folderUpload, ref name);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_INSERT, ApplicationGenerator.TypeResult.FAIL));
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.FAIL,"Vui lòng chọn thư mục cần tải hình lên");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                TempData["Alert"] = ApplicationGenerator.RenderResult(ApplicationGenerator.TypeResult.ERROR, ApplicationGenerator.GeneralActionMessage(APIConstant.ACTION_INSERT, ApplicationGenerator.TypeResult.ERROR));
                return View();
            }
        }
    }
}
