using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class AskForLeave
    {
        public int ID { get; set; }
        public string AskForLeaveCode { get; set; }
        public System.DateTime ApplyDate { get; set; }
        public System.DateTime BeginDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public int TypeId { get; set; }
        public string Reason { get; set; }
        public Nullable<System.DateTime> ApprovalTime { get; set; }
        public string Status { get; set; }
        public string UserCode { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreateUserCode { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public string ModifyUserCode { get; set; }
        public string Addtion1 { get; set; }
        public string Addtion2 { get; set; }
    }
}
