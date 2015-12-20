using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Announce
    {
        public int ID { get; set; }
        public string AnnounceCode { get; set; }
        public string AnnounceTypeId { get; set; }
        public string AnnounceTitle { get; set; }
        public string AnnounceContent { get; set; }
        public string Status { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreateUserCode { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public string ModifyUserCode { get; set; }
        public string Addtion1 { get; set; }
        public string Addtion2 { get; set; }
    }
}
