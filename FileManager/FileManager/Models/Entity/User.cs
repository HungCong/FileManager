//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FileManager.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public long ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Nullable<int> Permission { get; set; }
        public Nullable<bool> status { get; set; }
    }
}
