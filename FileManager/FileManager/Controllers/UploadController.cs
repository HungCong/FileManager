using FileManager.Models.Business;
using FileManager.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileManager.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            //try
            //{
                if (file.ContentLength > 0)
                {
                    var user = (User)Session["Login"];
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles/"), _FileName);
                    file.SaveAs(_path);

                    Models.Entity.File model = new Models.Entity.File();
                    model.FileName = Path.GetFileNameWithoutExtension(_path);
                    model.Extension = Path.GetExtension(file.FileName);
                    model.Type = "file";
                    model.UserID = user.ID;
                    model.Size = file.ContentLength;
                    model.ParentDirect = 1;
                    var res = new FileBusiness().addFile(model, user.ID);
                    if (res)
                    {
                        SetAlert("File uploaded thành công", "success");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        SetAlert("File upload bị lỗi, Kiểm tra lại!!", "error");
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    SetAlert("File upload bị lỗi, Kiểm tra lại!!", "error");
                    return RedirectToAction("Index", "Home");
                }

            //}
            //catch
            //{
            //    SetAlert("File upload bị lỗi, Kiểm tra lại!!", "error");
            //    return RedirectToAction("Index", "Home");
            //}
        }

        public void SetAlert(string message, string type)
        {
            //Giống ViewBag
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}