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
    }
}