using FileManager.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileManager.Models.Business
{
    public class UserBusiness
    {
        private FileManagerEntities db = new FileManagerEntities();

        public bool CheckLogin(string username, string password)
        {
            var user = db.Users.Where(x => x.Username == username && x.Password == password).ToList();
            if (user.Count > 0)
                return true;
            return false;
        }
        //Thay đổi mật khẩu
        public void changePass(long userId, string newPass)
        {
            var user = db.Users.Find(userId);
            user.Password = newPass;
            db.SaveChanges();
        }

        //kiểm tra tài khoản đã tồn tại chưa?
        public bool CheckExits_User(string username)
        {
            var res = db.Users.Count(x => x.Username == username);
            if (res > 0)
                return true;
            return false;
        }

        //kiểm tra mật khẩu cũ
        public bool checkExPass(long userId, string exPass)
        {
            var user = db.Users.Find(userId);
            if (user.Password.Trim() != exPass)
                return false;
            else
                return true;
        }

        //Kiểm tra email đăng ký có tồn tại?
        public bool checkExist_Email(string email)
        {
            var res = db.Users.Count(x => x.Email == email);
            if (res > 0)
                return true;
            return false;
        }

        //get dispaly_name from email
        public User getUser(string email)
        {
            return db.Users.Single(x => x.Email == email);
        }

        //get user from username
        public User getUserName(string username)
        {
            return db.Users.Single(x => x.Username == username);
        }

        //Thêm mới user
        public bool addUser(User entity)
        {
            try
            {
                var user = new User();
                user.Email = entity.Email;
                user.Password = entity.Password;
                user.Username = entity.Username;
                user.status = false;

                db.Users.Add(user);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //cập nhật trạng thái user
        public void updateStt(string userSession)
        {
            var user = db.Users.Single(x => x.Username == userSession);
            user.status = true;
            db.SaveChanges();
        }
    }
}