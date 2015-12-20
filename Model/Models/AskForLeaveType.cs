using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class AskForLeaveType
    {
        public int ID { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string Addtion1 { get; set; }
        public string Addtion2 { get; set; }
    }
}
