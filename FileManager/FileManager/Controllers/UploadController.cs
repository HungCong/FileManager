using FileManager.Models.Business;
using FileManager.Models.DTO;
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
            if( file != null)
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
                    //add data in fileDescription table
                    FileDescription fd = new FileDescription();
                    fd.FileID = new FileBusiness().getIDMax();
                    fd.ParentDirect = "/UploadedFiles/" + _FileName;

                    var resFileDs = new FileBusiness().AddFileDescription(fd);
                    if (resFileDs)
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
                               
            }
            else
            {
                SetAlert("Bạn chưa chọn file upload!!", "error");
                return RedirectToAction("Index", "Home");
            }

        }

        //Xóa File 
        public JsonResult deleteFile(long fileID)
        {
            var file = new FileBusiness().FindFile(fileID);
            var path = Server.MapPath("~/UploadedFiles/" + file.FileName + file.Extension.Trim());
            FileInfo FILE = new FileInfo(path);
            if (FILE.Exists)
            {
                FILE.Delete();
                var res = new FileBusiness().deleteFile(fileID);
                var resFileD = new FileBusiness().DeleteFileDescription(fileID);
                if(res && resFileD)
                    return Json(new
                    {
                        status = true
                    });
                else
                    return Json(new
                    {
                        status = false
                    });

            }else
            {
                return Json(new
                {
                    status = false
                });
            }
          
        }


        //Tải về 
        [HttpGet]
        public ActionResult downloadFile(long fileID)
        {
            var file = new FileBusiness().FindFile(fileID);

            //string path = Path.Combine(Server.MapPath("~/UploadedFiles/"), file.FileName + file.Extension.Trim());
            string path = Server.MapPath(@"~/UploadedFiles/" + file.FileName + file.Extension.Trim());

            byte[] fileBytes = GetBytesFromFile(path);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file.FileName + file.Extension.Trim());
        }

        //Chia sẻ file
        public ActionResult shareFile(string path)
        {
            var user = (User)Session["Login"];
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                long fileID = long.Parse(new MD5().Decrypt_MD5(path));
                ViewBag.FileShare = new FileBusiness().getFileShare(fileID);
                return View();
            }
        }

        public byte[] GetBytesFromFile(string fullFilePath)
        {
            // this method is limited to 2^32 byte files (4.2 GB)
            FileStream fs = null;
            try
            {
                fs = System.IO.File.OpenRead(fullFilePath);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
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