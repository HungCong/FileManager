using FileManager.Models.Business;
using FileManager.Models.DTO;
using FileManager.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var user = (User)Session["Login"];
            if (user == null)
            {
                return RedirectToAction("Index", "Login");
            }else
            {
                var lstFile = new FileBusiness().getFile(user.ID);
                foreach(var item in lstFile)
                {
                    item.ParentDirect = new MD5().Encrypt_MD5(item.ID.ToString());
                }
                ViewBag.ListFile = lstFile;
                return View();
            }
        }

        //Tìm kiếm file
        [HttpPost]
        public JsonResult SearchFile(string q)
        {
            var user = (User)Session["Login"];
            var data = new FileBusiness().searchFile(q, user);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string Keyword)
        {
            string[] key = Keyword.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            FileManagerEntities db = new FileManagerEntities();

            var query = new List<FileDTO>();
            foreach(var item in key)
            {
                query =    (from f in db.Files
                            join fd in db.FileDescriptions on f.ID equals fd.FileID
                            where f.FileName.Contains(Keyword)
                            select new FileDTO()
                            {
                                ID = f.ID,
                                FileName = f.FileName,
                                CreatedDate = f.CreatedDate,
                                Extension = f.Extension,
                                ParentDirect = fd.ParentDirect
                            }).ToList();
            }
            
            ViewBag.FileSearch = query;
            return View();
        }
    }
}