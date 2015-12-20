using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Project_Discuss
    {
        public int ID { get; set; }
        public string ProjCode { get; set; }
        public string ProjComConent { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreateUserCode { get; set; }
        public string CreateUserImage { get; set; }
        public string Addtion1 { get; set; }
        public string Addtion2 { get; set; }
    }
}
