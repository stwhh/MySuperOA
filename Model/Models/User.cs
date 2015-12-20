using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class User
    {
        public int ID { get; set; }
        public string UserCode { get; set; }
        public string RealName { get; set; }
        public string UserPwd { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string QQ { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public System.DateTime EntryDate { get; set; }
        public string PositionCode { get; set; }
        public string DepartmentCode { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreateUserCode { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public string ModifyUserCode { get; set; }
        public string Addtion1 { get; set; }
        public string Addtion2 { get; set; }
    }
}
