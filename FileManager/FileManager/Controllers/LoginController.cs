using FileManager.Models.Business;
using FileManager.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace FileManager.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            UserBusiness user = new UserBusiness();
            if (ModelState.IsValid)
            {
                if (user.CheckLogin(username, password))
                {
                    Session["Login"] = user.getUserName(username);
                    return Redirect("/Home");
                }
                else
                    return Redirect("/");
            }
            return Redirect("/");
        }

        public ActionResult ForgetPass()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgetPass(string email)
        {
            var userModel = new UserBusiness();
            if (ModelState.IsValid)
            {
                var res = userModel.checkExist_Email(email);
                if (res)
                {
                    var user_ses = userModel.getUser(email);
                    Session["Forget_pass"] = user_ses.Password;
                    SendEmail_CheckPass(email);
                    return Redirect("/doi-mat-khau/" + user_ses.ID);
                }
                else
                {
                    SetAlert("Email chưa được đăng ký", "error");
                    return View();
                }
            }
            return View();
        }

        public ActionResult ChangePass(long userId)
        {

            ViewBag.UserId = userId;
            return View();
        }

        [HttpPost]
        public ActionResult ChangePass(long userId, string exPass, string newPass)
        {
            var model = new UserBusiness();
            if (ModelState.IsValid)
            {
                if (!model.checkExPass(userId, exPass))
                {
                    SetAlert("Mật khẩu cũ không trùng với mật khẩu đã gửi qua email!! Vui kiểm tra lại", "error");
                    return RedirectToAction("ChangePass", new { userId = userId });
                }
                else
                {
                    model.changePass(userId, newPass);
                    SetAlert("Đổi mật khẩu thành công", "success");
                    return Redirect("/");
                }
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User entity)
        {
            var userModel = new UserBusiness();
            if (ModelState.IsValid)
            {
                if (userModel.checkExist_Email(entity.Email))
                {
                    SetAlert("Email đã tồn tại", "error");
                    return View();
                }
                else if (userModel.CheckExits_User(entity.Username))
                {
                    SetAlert("Tài khoản đã tồn tại, vui lòng chọn tài khoản khác", "error");
                    return View();
                }
                else
                {
                    var user = new User();
                    user.Email = entity.Email;
                    user.Password = entity.Password;
                    user.Username = entity.Username;

                    //Session.Add("UserName", user);//gán session để hiện tên người dùng khi đăng ký thành công        
                    var res = userModel.addUser(user);
                    if (res)
                    {
                        Session["User"] = user;
                        SendEmail(user.Email);
                        return RedirectToAction("Register_Success", "Login");
                    }
                    else
                    {
                        SetAlert("Đã xảy ra lỗi khi đăng ký", "error");
                        return RedirectToAction("Register", "Login");
                    }
                }
            }
            return RedirectToAction("Register", "Login");
        }

        public ActionResult Register_Success()
        {
            return View();
        }


        //kích hoạt email thành công
        public ActionResult Active_User(string userSession)
        {
            new UserBusiness().updateStt(userSession);
            SetAlert("Đăng ký thành công", "success");
            return RedirectToAction("Index", "Login");
        }

        //send email
        public void SendEmail(string address)
        {
            string email = "superjunior25251325@gmail.com";
            string password = "HungCong15021996";
            var user = (User)Session["User"];
            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(address));
            msg.Subject = "Email xác nhận";
            msg.Body = "Vui lòng nhấn vào đường dẫn dưới đây để xác nhận đăng ký tại website : \n";
            msg.Body += "<a href=" + "http://localhost:59910/active/user-" + user.Username.ToString() + ">Xác nhận</a>";
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
        }

        public void SendEmail_CheckPass(string address)
        {
            string email = "superjunior25251325@gmail.com";
            string password = "HungCong15021996";

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(address));
            msg.Subject = "Quên mật khẩu đăng nhập";
            msg.Body = "Mật khẩu của bạn là <u>" + Session["Forget_pass"] + "</u>";
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
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