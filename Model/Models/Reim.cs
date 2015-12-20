using System;
using System.Collections.Generic;

namespace Model.Models
{
    public partial class Reim
    {
        public int ID { get; set; }
        public string ReimCode { get; set; }
        public string ReimContent { get; set; }
        public System.DateTime ApplyDate { get; set; }
        public string AirTicket { get; set; }
        public string Train { get; set; }
        public string Bus { get; set; }
        public string Traffic { get; set; }
        public string Accommodation { get; set; }
        public string Bonus { get; set; }
        public string Other { get; set; }
        public string ReimMoney { get; set; }
        public string UserCode { get; set; }
        public string TripCode { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreateUserCode { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public string ModifyUserCode { get; set; }
        public string Addtion1 { get; set; }
        public string Addtion2 { get; set; }
    }
}
