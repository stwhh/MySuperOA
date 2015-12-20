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
        /// <param name="UserCode"></param>
        /// <param name="TripCode"></param>
        /// <param name="TripContent"></param>
        /// <param name="ApplyDate"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ResultModel<object> TripManage_Query(string UserCode, string TripCode, string TripContent, string BeginApplyDate, string EndApplyDate, int pageindex, int pagesize)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.TripManage_Query(UserCode, TripCode, TripContent, BeginApplyDate, EndApplyDate, pageindex, pagesize);
        }


        /// <summary>
        /// 新增出差申请单-保存
        /// </summary>
        /// <param name="trip"></param>
        /// <returns></returns>
        public ResultModel<object> TripManage_Add_Save(Trip trip, string userCode)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.TripManage_Add_Save(trip, userCode);
        }


        /// <summary>
        /// 查看出差单详情页面
        /// </summary>
        /// <param name="TripCode"></param>
        /// <returns></returns>
        public Trip TripManage_Detail(string TripCode)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.TripManage_Detail(TripCode);
        }
        #endregion


        #region 报销管理
        /// <summary>
        /// 报销管理-查询
        /// </summary>
        /// <param name="ReimCode"></param>
        /// <param name="RealName"></param>
        /// <param name="BeginApplyDate"></param>
        /// <param name="EndApplyDate"></param>
        /// <param name="userCode"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ResultModel<object> ReimManage_Query(string ReimCode, string ReimContent, string BeginApplyDate, string EndApplyDate, string UserCode, int pageindex, int pagesize)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.ReimManage_Query(ReimCode, ReimContent, BeginApplyDate, EndApplyDate, UserCode, pageindex, pagesize);
        }


        /// <summary>
        /// 报销管理-新增-查询已审批的出差单(只有审批通过的出差单才能报销)
        /// </summary>
        /// <param name="UserCode"></param>
        /// <param name="TripContent"></param>
        /// <param name="BeginApplyDate"></param>
        /// <param name="EndApplyDate"></param>
        /// <returns></returns>
        public ResultModel<object> ReimManage_QueryTrip(string UserCode, string TripContent, string BeginApplyDate, string EndApplyDate, int pageindex, int pagesize)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.ReimManage_QueryTrip(UserCode, TripContent, BeginApplyDate, EndApplyDate, pageindex, pagesize);
        }


        /// <summary>
        /// 报销管理-根据出差单号新增报销单||根据出差单编号查询出差信息
        /// </summary>
        /// <param name="TripCode"></param>
        /// <returns></returns>
        public Trip ReimManage_AddByTripCode(string TripCode)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.ReimManage_AddByTripCode(TripCode);
        }


        /// <summary>
        /// 确认申请报销
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="trip"></param>
        /// <param name="reim"></param>
        /// <returns></returns>
        public ResultModel<object> ReimManage_Add_Save(string UserCode, Trip trip, Reim reim)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.ReimManage_Add_Save(UserCode, trip, reim);
        }


        /// <summary>
        /// 报销管理-查看详情
        /// </summary>
        /// <param name="ReimCode"></param>
        /// <returns></returns>
        public Reim ReimManage_Detail(string ReimCode)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.ReimManage_Detail(ReimCode);
        }
        #endregion


        #region 请假管理
        /// <summary>
        /// 请假管理-查询
        /// </summary>
        /// <param name="AskForLeaveCode"></param>
        /// <param name="Reson"></param>
        /// <param name="BeginApplyDate"></param>
        /// <param name="EndApplyDate"></param>
        /// <param name="UserCode"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ResultModel<object> LeaveManage_Query(string AskForLeaveCode, string Reason, string BeginApplyDate, string EndApplyDate, string UserCode, int pageindex, int pagesize)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.LeaveManage_Query(AskForLeaveCode, Reason, BeginApplyDate, EndApplyDate, UserCode, pageindex, pagesize);
        }


        /// <summary>
        /// 请假管理-新增-保存
        /// </summary>
        /// <param name="leave"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public ResultModel<object> LeaveManage_Add_Save(AskForLeave leave, string userCode)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.LeaveManage_Add_Save(leave, userCode);
        }


        /// <summary>
        /// 请假管理-查看详细信息
        /// </summary>
        /// <param name="AskForLeaveCode"></param>
        /// <returns></returns>
        public AskForLeave LeaveManage_Detail(string AskForLeaveCode)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.LeaveManage_Detail(AskForLeaveCode);
        }

        #endregion

         
        #region 待我审批
        /// <summary>
        /// 待我审批-查询
        /// </summary>
        /// <param name="ApplyCode"></param>
        /// <param name="RealName"></param>
        /// <param name="BeginApplyDate"></param>
        /// <param name="EndApplyDate"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ResultModel<object> WaitingApprove_Query(string ApplyCode, string UserCode, string BeginApplyDate, string EndApplyDate, int pageindex, int pagesize)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.WaitingApprove_Query(ApplyCode, UserCode, BeginApplyDate, EndApplyDate, pageindex, pagesize);
        }


        /// <summary>
        /// 待我审批-审批通过
        /// </summary>
        /// <param name="ApplyCode"></param>
        /// <returns></returns>
        public ResultModel<object> WaitingApprove_Pass(string[] ApplyCodes)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.WaitingApprove_Pass(ApplyCodes);
        }


        /// <summary>
        /// 待我审批-审批拒绝
        /// </summary>
        /// <param name="ApplyCode"></param>
        /// <returns></returns>
        public ResultModel<object> WaitingApprove_Reject(string[] ApplyCodes)
        {
            ExecManageDAL dal = new ExecManageDAL();
            return dal.WaitingApprove_Reject(ApplyCodes);
        }
        #endregion
    }
}
