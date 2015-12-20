using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class User_Role
    {
        public int ID { get; set; }
        public string UserCode { get; set; }
        public string RoleCode { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreateUserCode { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public string ModifyUserCode { get; set; }
        public string Addtion1 { get; set; }
        public string Addtion2 { get; set; }
    }
}
