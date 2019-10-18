using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileManager.Models.DTO
{
    public class FileDTO
    {
        public long ID { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string Type { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<System.DateTime> AccessedDate { get; set; }
        public Nullable<long> UserID { get; set; }
        public Nullable<double> Size { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<long> FileID { get; set; }
        public string FileType { get; set; }
        public string ParentDirect { get; set; }
        public string Username { get; set; }
    }
}