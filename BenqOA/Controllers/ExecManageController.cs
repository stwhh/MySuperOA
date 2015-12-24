using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using Model.Models;
using BenqOA.Helper;
using Public;

namespace BenqOA.Controllers
{
    [MyAuthorFilter(Roles = MyAuthorFilter.LoginRole)]
    public class ExecManageController : Controller
    {
        // 行政管理
        // GET: /ExecManage/

        #region 出差管理
        public ActionResult TripManage()
        {
            User user = Session["userInfo"] as User;
            ViewBag.UserCode = user.UserCode; //用户编号

            return View();
        }

        //出差申请单查询
        public JsonResult TripManage_Query(string UserCode, string TripCode, string TripContent, string BeginApplyDate, string EndApplyDate, int pageindex, int pagesize)
        {
            User user = Session["userInfo"] as User;
            string userCode = user.UserCode; //用户编号

            ExecManageBLL bll = new ExecManageBLL();
            return Json(bll.TripManage_Query(userCode, TripCode, TripContent, BeginApplyDate, EndApplyDate, pageindex, pagesize));
        }

        //新增出差申请单-页面
        public ActionResult TripManage_Add(string userCode)
        {
            ViewBag.userCode = userCode;
            //申请单号自动生成
            ViewBag.GeneRadomNum = GeneRandomNum.GetRanNum("T");
            return View();
        }

        //新增出差申请单-保存
        public JsonResult TripManage_Add_Save(string userCode, Trip trip)
        {
            ExecManageBLL bll = new ExecManageBLL();
            return Json(bll.TripManage_Add_Save(trip, userCode));
        }

        //查看出差单详情页面
        public ActionResult TripManage_Detail(string TripCode)
        {
            ExecManageBLL bll = new ExecManageBLL();
            return View(bll.TripManage_Detail(TripCode));
        }


        #endregion


        #region 报销管理
        public ActionResult ReimManage()
        {
            User user = Session["userInfo"] as User;
            ViewBag.UserCode = user.UserCode; //用户编号

            return View();
        }

        //报销管理-查询
        public JsonResult ReimManage_Query(string ReimCode, string ReimContent, string BeginApplyDate, string EndApplyDate, string UserCode, int pageindex, int pagesize)
        {
            ExecManageBLL bll = new ExecManageBLL();
            return Json(bll.ReimManage_Query(ReimCode, ReimContent, BeginApplyDate, EndApplyDate, UserCode, pageindex, pagesize));
        }

        //报销管理-新增
        public ActionResult ReimManage_Add()
        {
            User user = Session["userInfo"] as User;
            ViewBag.UserCode = user.UserCode; //用户编号

            return View();
        }

        //报销管理-新增-查询已审批的出差单(只有审批通过的出差单才能报销)
        public JsonResult ReimManage_QueryTrip(string UserCode, string TripContent, string BeginApplyDate, string EndApplyDate, int pageindex, int pagesize)
        {
            ExecManageBLL bll = new ExecManageBLL();
            return Json(bll.ReimManage_QueryTrip(UserCode, TripContent, BeginApplyDate, EndApplyDate, pageindex, pagesize));
        }

        //报销管理-根据出差单号新增报销单||根据出差单编号查询出差信息
        public ActionResult ReimManage_AddByTripCode(string TripCode,string UserCode)
        {
            ViewBag.TripCode = TripCode; //出差编号
            ViewBag.UserCode = UserCode; //用户编号
            ViewBag.GeneRadomNum = GeneRandomNum.GetRanNum("R"); //报销单号随机生成
            ExecManageBLL bll = new ExecManageBLL();
            //计算出差天数
            Trip trip = bll.ReimManage_AddByTripCode(TripCode);
            ViewBag.TripDays = Convert.ToInt32((trip.EndDate-trip.BeginDate).ToString("dd"));

            return View(trip);
        }

        //确认申请报销
        public ActionResult ReimManage_Add_Save(string UserCode, Trip trip, Reim reim)
        {
            ExecManageBLL bll=new ExecManageBLL();
            return Json(bll.ReimManage_Add_Save(UserCode, trip, reim));
        }

        //报销管理-查看详情
        public ActionResult ReimManage_Detail(string ReimCode)
        {
            ExecManageBLL bll = new ExecManageBLL();
            BenqOAContext bqc = new BenqOAContext();
            //根据报销单编号查询出差单编号
            var tripCode = bqc.Reims.Where(p => p.ReimCode == ReimCode).Select(p => new { tripCode = p.TripCode }).First();
            var TripCode = tripCode.tripCode;
            //根据出差单号查询出差信息
            var tripInfo = bqc.Trips.Where(p => p.TripCode == TripCode).First();
            ViewBag.TripCode = tripInfo.TripCode;
            ViewBag.TripContent = tripInfo.TripContent;
            ViewBag.BeginDate = tripInfo.BeginDate;
            ViewBag.EndDate = tripInfo.EndDate;
            ViewBag.Destination = tripInfo.Destination;
            //出差天数
            ViewBag.TripDays = Convert.ToInt32((ViewBag.EndDate - ViewBag.BeginDate).ToString("dd"));

            return View(bll.ReimManage_Detail(ReimCode));
        }
        #endregion


        #region 请假管理
        public ActionResult LeaveManage()
        {
            User user = Session["userInfo"] as User;
            ViewBag.UserCode = user.UserCode; //用户编号

            return View();
        }

        //请假管理-查询
        public JsonResult LeaveManage_Query(string AskForLeaveCode, string Reason, string BeginApplyDate, string EndApplyDate, string UserCode, int pageindex, int pagesize)
        {
            ExecManageBLL bll = new ExecManageBLL();
            return Json(bll.LeaveManage_Query(AskForLeaveCode, Reason, BeginApplyDate, EndApplyDate, UserCode, pageindex, pagesize));
        }

        //请假管理-新增页面
        public ActionResult LeaveManage_Add(string userCode)
        {
            ViewBag.userCode = userCode;
            //申请单号自动生成
            ViewBag.GeneRadomNum = GeneRandomNum.GetRanNum("L");
            return View();
        }

        //请假管理-新增-保存
        public JsonResult LeaveManage_Add_Save(AskForLeave leave, string userCode)
        {
            ExecManageBLL bll = new ExecManageBLL();
            return Json(bll.LeaveManage_Add_Save(leave, userCode));
        }

        //请假管理-查看详细信息
        public ActionResult LeaveManage_Detail(string AskForLeaveCode)
        {
            ExecManageBLL bll = new ExecManageBLL();
            //var list = bll.LeaveManage_Detail(AskForLeaveCode);
            //ViewBag.TypeName = GetLeaveNameById(list.AskForLeaveCode);

            return View(bll.LeaveManage_Detail(AskForLeaveCode));
        }

        #endregion


        #region 待我审批
        public ActionResult WaitingApprove()
        {
            return View();
        }

        //待我审批-查询
        public JsonResult WaitingApprove_Query(string ApplyCode, string UserCode, string BeginApplyDate, string EndApplyDate, int pageindex, int pagesize)
        {
            ExecManageBLL bll = new ExecManageBLL();
            var lists = bll.WaitingApprove_Query(ApplyCode, UserCode, BeginApplyDate, EndApplyDate, pageindex, pagesize);

            return Json(lists);
        }

        //待我审批-审批通过
        public ActionResult WaitingApprove_Pass(string selectitems)
        {
            string[] ApplyCodes = selectitems.Split(','); //接收到的字符串分割成数组形式
            ExecManageBLL bll=new ExecManageBLL();

            return Json(bll.WaitingApprove_Pass(ApplyCodes));
        }

         //待我审批-审批通过
        public ActionResult WaitingApprove_Reject(string selectitems)
        {
            string[] ApplyCodes = selectitems.Split(','); //接收到的字符串分割成数组形式
            ExecManageBLL bll=new ExecManageBLL();

            return Json(bll.WaitingApprove_Reject(ApplyCodes));
        }
        #endregion
    }
}
