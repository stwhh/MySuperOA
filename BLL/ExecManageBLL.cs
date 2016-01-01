using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Models;
using DAL;

namespace BLL
{
    //行政管理
    public class ExecManageBLL
    {
        #region 出差管理

        /// <summary>
        /// 出差申请单查询
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <param name="tripCode">出差编号</param>
        /// <param name="tripContent">出差内容</param>
        /// <param name="beginApplyDate">出差开始日期</param>
        /// <param name="endApplyDate">出差结束日期</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public ResultModel<object> TripManage_Query(string userCode, string tripCode, string tripContent, string beginApplyDate, string endApplyDate, int pageindex, int pagesize)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.TripManage_Query(userCode, tripCode, tripContent, beginApplyDate, endApplyDate, pageindex, pagesize);
        }


        /// <summary>
        /// 新增出差申请单-保存
        /// </summary>
        /// <param name="trip">出差实体</param>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public ResultModel<object> TripManage_Add_Save(Trip trip, string userCode)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.TripManage_Add_Save(trip, userCode);
        }


        /// <summary>
        /// 查看出差单详情页面
        /// </summary>
        /// <param name="tripCode">出差编号</param>
        /// <returns></returns>
        public Trip TripManage_Detail(string tripCode)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.TripManage_Detail(tripCode);
        }
        #endregion


        #region 报销管理

        /// <summary>
        /// 报销管理-查询
        /// </summary>
        /// <param name="reimCode">报销编号</param>
        /// <param name="reimContent">报销说明</param>
        /// <param name="beginApplyDate">开始日期</param>
        /// <param name="endApplyDate">结束日期</param>
        /// <param name="userCode">用户编号</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public ResultModel<object> ReimManage_Query(string reimCode, string reimContent, string beginApplyDate, string endApplyDate, string userCode, int pageindex, int pagesize)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.ReimManage_Query(reimCode, reimContent, beginApplyDate, endApplyDate, userCode, pageindex, pagesize);
        }


        /// <summary>
        /// 报销管理-新增-查询已审批的出差单(只有审批通过的出差单才能报销)
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <param name="tripContent">出差内容</param>
        /// <param name="beginApplyDate">开始日期</param>
        /// <param name="endApplyDate">结束日期</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public ResultModel<object> ReimManage_QueryTrip(string userCode, string tripContent, string beginApplyDate, string endApplyDate, int pageindex, int pagesize)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.ReimManage_QueryTrip(userCode, tripContent, beginApplyDate, endApplyDate, pageindex, pagesize);
        }


        /// <summary>
        /// 报销管理-根据出差单号新增报销单||根据出差单编号查询出差信息
        /// </summary>
        /// <param name="tripCode">出差编号</param>
        /// <returns></returns>
        public Trip ReimManage_AddByTripCode(string tripCode)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.ReimManage_AddByTripCode(tripCode);
        }


        /// <summary>
        /// 确认申请报销
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <param name="trip">出差实体</param>
        /// <param name="reim">报销实体</param>
        /// <returns></returns>
        public ResultModel<object> ReimManage_Add_Save(string userCode, Trip trip, Reim reim)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.ReimManage_Add_Save(userCode, trip, reim);
        }


        /// <summary>
        /// 报销管理-查看详情
        /// </summary>
        /// <param name="reimCode">报销编号</param>
        /// <returns></returns>
        public Reim ReimManage_Detail(string reimCode)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.ReimManage_Detail(reimCode);
        }
        #endregion


        #region 请假管理

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
        public ResultModel<object> LeaveManage_Query(string askForLeaveCode, string reason, string beginApplyDate, string endApplyDate, string userCode, int pageindex, int pagesize)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.LeaveManage_Query(askForLeaveCode, reason, beginApplyDate, endApplyDate, userCode, pageindex, pagesize);
        }


        /// <summary>
        /// 请假管理-新增-保存
        /// </summary>
        /// <param name="leave">请假实体</param>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public ResultModel<object> LeaveManage_Add_Save(AskForLeave leave, string userCode)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.LeaveManage_Add_Save(leave, userCode);
        }


        /// <summary>
        /// 请假管理-查看详细信息
        /// </summary>
        /// <param name="askForLeaveCode">请假实体</param>
        /// <returns></returns>
        public AskForLeave LeaveManage_Detail(string askForLeaveCode)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.LeaveManage_Detail(askForLeaveCode);
        }

        #endregion

         
        #region 待我审批

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
        public ResultModel<object> WaitingApprove_Query(string applyCode, string userCode, string beginApplyDate, string endApplyDate, int pageindex, int pagesize)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.WaitingApprove_Query(applyCode, userCode, beginApplyDate, endApplyDate, pageindex, pagesize);
        }


        /// <summary>
        /// 待我审批-审批通过
        /// </summary>
        /// <param name="applyCodes">审批选中项集合</param>
        /// <returns></returns>
        public ResultModel<object> WaitingApprove_Pass(string[] applyCodes)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.WaitingApprove_Pass(applyCodes);
        }


        /// <summary>
        /// 待我审批-审批拒绝
        /// </summary>
        /// <param name="applyCodes">选中项</param>
        /// <returns></returns>
        public ResultModel<object> WaitingApprove_Reject(string[] applyCodes)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.WaitingApprove_Reject(applyCodes);
        }
        #endregion
    }
}
