using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model.Models;
using Microsoft.Reporting.WebForms; //报表命名空间

using System.Data.Objects.SqlClient;//用sql自带的函数命名空间
using System.Data.Objects; //用ef函数，EntityFunctions

namespace BenqOA.Reports
{
    public partial class ReimInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack) //第一次加载
            {
                string reimCode = Request.QueryString["ReimCode"].ToString();

                BenqOAContext bqc = new BenqOAContext();
                var list = (from r in bqc.Reims
                            from t in bqc.Trips
                            where r.TripCode == t.TripCode
                            && r.ReimCode == reimCode
                            select new
                            {
                                ReimCode = r.ReimCode,
                                ReimContent = r.ReimContent,
                                AirTicket = r.AirTicket,
                                Train = r.Train,
                                Bus = r.Bus,
                                Traffic = r.Traffic,
                                Accommodation = r.Accommodation,
                                //tripDays =(SqlFunctions.DatePart("day",(t.EndDate-t.BeginDate))), //出差天数
                                tripDays =EntityFunctions.DiffDays(t.BeginDate,t.EndDate), //出差天数
                                Bonus = r.Bonus,
                                Other = r.Other,
                                ReimMoney = r.ReimMoney,
                                TripCode = r.TripCode,
                                CreateUserCode = r.CreateUserCode,
                                BeginDate = t.BeginDate,
                                EndDate = t.EndDate,
                                Destination = t.Destination,
                                TripContent = t.TripContent,
                                UserCode=t.UserCode
                            }).ToList();

                ReportDataSource rds = new ReportDataSource("DataSet1", list); //报表数据集
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RtForReimInfo.rdlc"); //报表路径
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.DisplayName = "报销单"; //报表名称
                ReportViewer1.LocalReport.Refresh();
               
            }
        }
    }
}