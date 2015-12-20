using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Permisson
    {
        public int ID { get; set; }
        public string PermCode { get; set; }
        public string PermName { get; set; }
        public string PermUrl { get; set; }
        public int PermSeq { get; set; }
        public string PermType { get; set; }
        public string ParaentCode { get; set; }
        public string PermIcon { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreateUserCode { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public string ModifyUserCode { get; set; }
        public string Addtion1 { get; set; }
        public string Addtion2 { get; set; }
    }
}
