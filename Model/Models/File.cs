using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class File
    {
        public int ID { get; set; }
        public string FileCode { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string FilePath { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreateUserCode { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public string ModifyUserCode { get; set; }
        public string Addtion1 { get; set; }
        public string Addtion2 { get; set; }
    }
}
