using FileManager.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace FileManager.Models.Business
{
    public class FileBusiness
    {
        private FileManagerEntities db = new FileManagerEntities();

        //Thêm mới file
        public bool addFile(File entity, long userId)
        {
            try
            {
                File file = new File();
                file.FileName = entity.FileName;
                file.Extension = entity.Extension.Trim();
                file.Type = entity.Type;
                file.CreatedDate = DateTime.Now;
                file.UserID = userId;
                file.Size = entity.Size;
                file.ParentDirect = entity.ParentDirect;
                file.Status = true;

                db.Files.Add(file);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<File> getFile(long UserId)
        {
            return db.Files.Where(x => x.UserID == UserId).OrderByDescending(x => x.ID).ToList();
        }
        
        //Xóa File
        public bool deleteFile(long ID)
        {
            try
            {
                //Xóa trong csdl
                var file = db.Files.Find(ID);
                db.Files.Remove(file);
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public File FindFile(long ID)
        {
            return db.Files.Find(ID);
        }
    }
}