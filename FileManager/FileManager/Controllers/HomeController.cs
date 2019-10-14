using FileManager.Models.Business;
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
            ViewBag.ListFile = new FileBusiness().getFile(user.ID);
            return View();
        }
    }
}