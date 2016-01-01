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

        #region 出差管理
        /// <summary>
        /// 出差管理
        /// </summary>
        /// <returns></returns>
        public ActionResult TripManage()
        {
            User user = Session["userInfo"] as User;
            if (user != null) ViewBag.UserCode = user.UserCode; //用户编号

            return View();
        }

        /// <summary>
        /// 出差申请单查询
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <param name="tripCode">出差编号</param>
        /// <param name="tripContent">出差内容</param>
        /// <param name="beginApplyDate">出差开始时间</param>
        /// <param name="endApplyDate">出差结束时间</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public JsonResult TripManage_Query(string userCode, string tripCode, string tripContent, string beginApplyDate, string endApplyDate, int pageindex, int pagesize)
        {
            User user = Session["userInfo"] as User;
            string tripUserCode = user.UserCode; //用户编号

            ExecManageBLL bll = new ExecManageBLL();
            return Json(bll.TripManage_Query(tripUserCode, tripCode, tripContent, beginApplyDate, endApplyDate, pageindex, pagesize));
        }

        /// <summary>
        /// 新增出差申请单-页面
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public ActionResult TripManage_Add(string userCode)
        {
            ViewBag.userCode = userCode;
            //申请单号自动生成
            ViewBag.GeneRadomNum = GeneRandomNum.GetRanNum("T");
            return View();
        }

        /// <summary>
        /// 新增出差申请单-保存
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <param name="trip">出差实体</param>
        /// <returns></returns>
        public JsonResult TripManage_Add_Save(string userCode, Trip trip)
        {
            ExecManageBLL bll = new ExecManageBLL();
            return Json(bll.TripManage_Add_Save(trip, userCode));
        }

        /// <summary>
        /// 查看出差单详情页面
        /// </summary>
        /// <param name="tripCode">出差编号</param>
        /// <returns></returns>
        public ActionResult TripManage_Detail(string tripCode)
        {
            ExecManageBLL bll = new ExecManageBLL();
            return View(bll.TripManage_Detail(tripCode));
        }


        #endregion


        #region 报销管理
        /// <summary>
        /// 报销管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ReimManage()
        {
            User user = Session["userInfo"] as User;
            ViewBag.UserCode = user.UserCode; //用户编号

            return View();
        }

        /// <summary>
        /// 报销管理-查询
        /// </summary>
        /// <param name="reimCode">报销编号</param>
        /// <param name="reimContent">报销内容</param>
        /// <param name="beginApplyDate">开始申请时间</param>
        /// <param name="endApplyDate">结束申请时间</param>
        /// <param name="userCode">用户编号</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public JsonResult ReimManage_Query(string reimCode, string reimContent, string beginApplyDate, string endApplyDate, string userCode, int pageindex, int pagesize)
        {
            ExecManageBLL bll = new ExecManageBLL();
            return Json(bll.ReimManage_Query(reimCode, reimContent, beginApplyDate, endApplyDate, userCode, pageindex, pagesize));
        }

        /// <summary>
        /// 报销管理-新增
        /// </summary>
        /// <returns></returns>
        public ActionResult ReimManage_Add()
        {
            User user = Session["userInfo"] as User;
            ViewBag.UserCode = user.UserCode; //用户编号

            return View();
        }

        /// <summary>
        /// 报销管理-新增-查询已审批的出差单(只有审批通过的出差单才能报销)
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <param name="tripContent">出差内容</param>
        /// <param name="beginApplyDate">开始申请时间</param>
        /// <param name="endApplyDate">结束申请时间</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public JsonResult ReimManage_QueryTrip(string userCode, string tripContent, string beginApplyDate, string endApplyDate, int pageindex, int pagesize)
        {
            ExecManageBLL bll = new ExecManageBLL();
            return Json(bll.ReimManage_QueryTrip(userCode, tripContent, beginApplyDate, endApplyDate, pageindex, pagesize));
        }

        /// <summary>
        /// 报销管理-根据出差单号新增报销单||根据出差单编号查询出差信息
        /// </summary>
        /// <param name="tripCode">出差编号</param>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public ActionResult ReimManage_AddByTripCode(string tripCode,string userCode)
        {
            ViewBag.TripCode = tripCode; //出差编号
            ViewBag.UserCode = userCode; //用户编号
            ViewBag.GeneRadomNum = GeneRandomNum.GetRanNum("R"); //报销单号随机生成
            ExecManageBLL bll = new ExecManageBLL();
            //计算出差天数
            Trip trip = bll.ReimManage_AddByTripCode(tripCode);
            ViewBag.TripDays = Convert.ToInt32((trip.EndDate-trip.BeginDate).ToString("dd"));

            return View(trip);
        }

        /// <summary>
        /// 确认申请报销
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <param name="trip">出差实体</param>
        /// <param name="reim">报销实体</param>
        /// <returns></returns>
        public ActionResult ReimManage_Add_Save(string userCode, Trip trip, Reim reim)
        {
            ExecManageBLL bll=new ExecManageBLL();
            return Json(bll.ReimManage_Add_Save(userCode, trip, reim));
        }

        /// <summary>
        /// 报销管理-查看详情
        /// </summary>
        /// <param name="reimCode">报销编号</param>
        /// <returns></returns>
        public ActionResult ReimManage_Detail(string reimCode)
        {
            ExecManageBLL bll = new ExecManageBLL();
            BenqOAContext bqc = new BenqOAContext();
            //根据报销单编号查询出差单编号
            var tripCode = bqc.Reims.Where(p => p.ReimCode == reimCode).Select(p => new { tripCode = p.TripCode }).First();
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

            return View(bll.ReimManage_Detail(reimCode));
        }
        #endregion


        #region 请假管理
        /// <summary>
        /// 请假管理
        /// </summary>
        /// <returns></returns>
        public ActionResult LeaveManage()
        {
            User user = Session["userInfo"] as User;
            ViewBag.UserCode = user.UserCode; //用户编号

            return View();
        }

        /// <summary>
        /// 请假管理-查询
        /// </summary>
        /// <param name="askForLeaveCode">请假编号</param>
        /// <param name="reason">请假原因</param>
        /// <param name="beginApplyDate">开始申请时间</param>
        /// <param name="endApplyDate">结束申请时间</param>
        /// <param name="userCode">用户编号</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public JsonResult LeaveManage_Query(string askForLeaveCode, string reason, string beginApplyDate, string endApplyDate, string userCode, int pageindex, int pagesize)
        {
            ExecManageBLL bll = new ExecManageBLL();
            return Json(bll.LeaveManage_Query(askForLeaveCode, reason, beginApplyDate, endApplyDate, userCode, pageindex, pagesize));
        }

        /// <summary>
        /// 请假管理-新增页面
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public ActionResult LeaveManage_Add(string userCode)
        {
            ViewBag.userCode = userCode;
            //申请单号自动生成
            ViewBag.GeneRadomNum = GeneRandomNum.GetRanNum("L");
            return View();
        }

        /// <summary>
        /// 请假管理-新增-保存
        /// </summary>
        /// <param name="leave">请假实体</param>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public JsonResult LeaveManage_Add_Save(AskForLeave leave, string userCode)
        {
            ExecManageBLL bll = new ExecManageBLL();
            return Json(bll.LeaveManage_Add_Save(leave, userCode));
        }

        /// <summary>
        /// 请假管理-查看详细信息
        /// </summary>
        /// <param name="askForLeaveCode">请假编号</param>
        /// <returns></returns>
        public ActionResult LeaveManage_Detail(string askForLeaveCode)
        {
            ExecManageBLL bll = new ExecManageBLL();
            //var list = bll.LeaveManage_Detail(AskForLeaveCode);
            //ViewBag.TypeName = GetLeaveNameById(list.AskForLeaveCode);

            return View(bll.LeaveManage_Detail(askForLeaveCode));
        }

        #endregion


        #region 待我审批
        /// <summary>
        /// 待我审批
        /// </summary>
        /// <returns></returns>
        public ActionResult WaitingApprove()
        {
            return View();
        }

        /// <summary>
        /// 待我审批-查询
        /// </summary>
        /// <param name="applyCode">申请编号</param>
        /// <param name="userCode">用户编号</param>
        /// <param name="beginApplyDate">开始审批时间</param>
        /// <param name="endApplyDate">结束申请时间</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public JsonResult WaitingApprove_Query(string applyCode, string userCode, string beginApplyDate, string endApplyDate, int pageindex, int pagesize)
        {
            ExecManageBLL bll = new ExecManageBLL();
            var lists = bll.WaitingApprove_Query(applyCode, userCode, beginApplyDate, endApplyDate, pageindex, pagesize);

            return Json(lists);
        }

        /// <summary>
        /// 待我审批-审批通过
        /// </summary>
        /// <param name="selectItems"></param>
        /// <returns></returns>
        public ActionResult WaitingApprove_Pass(string selectItems)
        {
            string[] applyCodes = selectItems.Split(','); //接收到的字符串分割成数组形式
            ExecManageBLL bll=new ExecManageBLL();

            return Json(bll.WaitingApprove_Pass(applyCodes));
        }

         /// <summary>
         /// 待我审批-审批通过
         /// </summary>
         /// <param name="selectItems">选择的项</param>
         /// <returns></returns>
        public ActionResult WaitingApprove_Reject(string selectItems)
        {
            string[] applyCodes = selectItems.Split(','); //接收到的字符串分割成数组形式
            ExecManageBLL bll=new ExecManageBLL();

            return Json(bll.WaitingApprove_Reject(applyCodes));
        }
        #endregion
    }
}
