using FileManager.Models.DTO;
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

        public List<FileDTO> getFile(long UserId)
        {
            var query = from f in db.Files
                        join fd in db.FileDescriptions on f.ID equals fd.FileID
                        where f.UserID == UserId
                        select new FileDTO()
                        {
                            ID = f.ID,
                            FileName = f.FileName,
                            CreatedDate = f.CreatedDate,
                            Extension = f.Extension,
                            ParentDirect = fd.ParentDirect
                        };
            query.OrderByDescending(x => x.ID);
            return query.ToList();
        }

        //lấy thông tin file chia sẻ
        public FileDTO getFileShare(long id)
        {
            var query = from f in db.Files
                        join user in db.Users on f.UserID equals user.ID
                        where f.ID == id
                        select new FileDTO()
                        {
                            ID = f.ID,
                            FileName = f.FileName,
                            CreatedDate = f.CreatedDate,
                            Extension = f.Extension,
                            Username = user.Username
                        };
            return query.Single(x => x.ID == id);
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

        //Lấy ID cao nhất
        public long getIDMax()
        {
            return db.Files.Select(x => x.ID).Max();
        }

        //thêm file trong file description
        public bool AddFileDescription(FileDescription entity)
        {
            try
            {
                FileDescription file = new FileDescription();
                file.FileID = entity.FileID;
                file.FileType = "file";
                file.ParentDirect = entity.ParentDirect;
                db.FileDescriptions.Add(file);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Xóa file trong bảng file descript
        public bool DeleteFileDescription(long fileId)
        {
            try
            {
                var file = db.FileDescriptions.Single(x => x.FileID == fileId);
                db.FileDescriptions.Remove(file);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}