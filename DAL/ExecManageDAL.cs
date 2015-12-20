using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Models;

namespace DAL
{
    //行政管理
    public class ExecManageDAL
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
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();
            ////1.把数字形式的时间截取拼接成2015-05-17
            //CommonDataDAL dal = new CommonDataDAL();
            //string beginApplyDate = dal.ExchangeDate(BeginApplyDate);
            //string endApplyDate = dal.ExchangeDate(EndApplyDate);

            DateTime bd = Convert.ToDateTime(BeginApplyDate);
            DateTime ed = Convert.ToDateTime(EndApplyDate);

            try
            {
                var list = bqc.Trips.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.UserCode == UserCode && p.TripCode.Contains(TripCode) && p.TripContent.Contains(TripContent))
                .OrderByDescending(p => p.ApplyDate).Skip(pageindex * pagesize).Take(pagesize).ToList();

                var count = bqc.Trips.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.UserCode == UserCode && p.TripCode.Contains(TripCode) && p.TripContent.Contains(TripContent)).Count();

                resultModel.Data = list;
                resultModel.TotalCount = count;
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }
            return resultModel;
        }


        /// <summary>
        /// 新增出差申请单-保存
        /// </summary>
        /// <param name="trip"></param>
        /// <returns></returns>
        public ResultModel<object> TripManage_Add_Save(Trip trip, string userCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            try
            {
                Trip trips = new Trip();
                trips.TripCode = trip.TripCode;
                trips.Destination = trip.Destination;
                trips.ApplyDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                trips.BeginDate = trip.BeginDate;
                trips.EndDate = trip.EndDate;
                trips.TripContent = trip.TripContent;
                trips.UserCode = userCode;
                trips.Status = "待审批";
                trips.IsReim = "N"; //开始还没申请报销

                bqc.Trips.Add(trips);
                bqc.SaveChanges();
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }

            return resultModel;
        }


        /// <summary>
        /// 查看出差单详情页面
        /// </summary>
        /// <param name="TripCode"></param>
        /// <returns></returns>
        public Trip TripManage_Detail(string TripCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            Trip list = bqc.Trips.Where(p => p.TripCode == TripCode).FirstOrDefault();
            //修改时间显示格式为2015-08-19

            return list; ;
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
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            DateTime bd = Convert.ToDateTime(BeginApplyDate);
            DateTime ed = Convert.ToDateTime(EndApplyDate);

            try
            {
                var list = bqc.Reims.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.UserCode == UserCode && p.ReimCode.Contains(ReimCode) && p.ReimContent.Contains(ReimContent))
                .OrderByDescending(p => p.ApplyDate).Skip(pageindex * pagesize).Take(pagesize).ToList();

                var count = bqc.Reims.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.UserCode == UserCode && p.ReimCode.Contains(ReimCode) && p.ReimContent.Contains(ReimContent))
                .Count();

                resultModel.Data = list;
                resultModel.TotalCount = count;
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }
            return resultModel;
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
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            DateTime bd = Convert.ToDateTime(BeginApplyDate);
            DateTime ed = Convert.ToDateTime(EndApplyDate);

            try
            {
                var list = bqc.Trips.Where(p => p.BeginDate.CompareTo(bd) >= 0 && p.BeginDate.CompareTo(ed) <= 0
                && p.UserCode == UserCode && p.TripContent.Contains(TripContent) && p.Status == "审批通过" && p.IsReim == "N")
                .OrderBy(p => p.ApplyDate).Skip(pageindex * pagesize).Take(pagesize).ToList();

                var count = bqc.Trips.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.UserCode == UserCode && p.TripContent.Contains(TripContent) && p.Status == "审批通过" && p.IsReim == "N").Count();

                resultModel.Data = list;
                resultModel.TotalCount = count;
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }
            return resultModel;
        }


        /// <summary>
        /// 报销管理-根据出差单号新增报销单||根据出差单编号查询出差信息
        /// </summary>
        /// <param name="TripCode"></param>
        /// <returns></returns>
        public Trip ReimManage_AddByTripCode(string TripCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            Trip list = bqc.Trips.Where(p => p.TripCode == TripCode).FirstOrDefault();

            return list; ;

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
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            try
            {
                //保存报销单
                reim.UserCode = UserCode;
                reim.TripCode = trip.TripCode;
                reim.ApplyDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                reim.Status = "待审批";
                bqc.Reims.Add(reim);

                //把出差单中 是否报销 改为Y
                Trip Trip = bqc.Trips.Where(p => p.TripCode == trip.TripCode).First();
                Trip.IsReim = "Y";

                bqc.SaveChanges();
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }

            return resultModel;
        }


        /// <summary>
        /// 报销管理-查看详情
        /// </summary>
        /// <param name="ReimCode"></param>
        /// <returns></returns>
        public Reim ReimManage_Detail(string ReimCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            Reim list = bqc.Reims.Where(p => p.ReimCode == ReimCode).FirstOrDefault();

            return list; ;
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
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            DateTime bd = Convert.ToDateTime(BeginApplyDate);
            DateTime ed = Convert.ToDateTime(EndApplyDate);

            try
            {
                var list = bqc.AskForLeaves.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.UserCode == UserCode && p.AskForLeaveCode.Contains(AskForLeaveCode) && p.Reason.Contains(Reason))
                .OrderByDescending(p => p.ApplyDate).Skip(pageindex * pagesize).Take(pagesize).ToList();

                var count = bqc.AskForLeaves.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.UserCode == UserCode && p.AskForLeaveCode.Contains(AskForLeaveCode) && p.Reason.Contains(Reason)).Count();

                resultModel.Data = list;
                resultModel.TotalCount = count;
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }
            return resultModel;
        }


        /// <summary>
        /// 请假管理-新增-保存
        /// </summary>
        /// <param name="leave"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public ResultModel<object> LeaveManage_Add_Save(AskForLeave leave, string userCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            try
            {
                AskForLeave Leave = new AskForLeave();
                Leave.AskForLeaveCode = leave.AskForLeaveCode;
                Leave.ApplyDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                Leave.BeginDate = leave.BeginDate;
                Leave.EndDate = leave.EndDate;
                Leave.Reason = leave.Reason;
                Leave.TypeId = leave.TypeId;
                Leave.Status = "待审批";
                Leave.UserCode = userCode;

                bqc.AskForLeaves.Add(Leave);
                bqc.SaveChanges();
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }

            return resultModel;
        }


        /// <summary>
        /// 请假管理-查看详细信息
        /// </summary>
        /// <param name="AskForLeaveCode"></param>
        /// <returns></returns>
        public AskForLeave LeaveManage_Detail(string AskForLeaveCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            AskForLeave list = bqc.AskForLeaves.Where(p => p.AskForLeaveCode == AskForLeaveCode).FirstOrDefault();
            //AskForLeave list = bqc.AskForLeaves.Join(bqc.AskForLeaveTypes.Where()).Where(p=>p.AskForLeaveCode==AskForLeaveCode);

            return list; ;
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
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            DateTime bd = Convert.ToDateTime(BeginApplyDate);
            DateTime ed = Convert.ToDateTime(EndApplyDate);

            try
            {
                //1.查询出差申请单
                var list1 = bqc.Trips.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.TripCode.Contains(ApplyCode) && p.UserCode.Contains(UserCode)).Select(p => new
                {
                    ApplyCode = p.TripCode,
                    ApplyContent = p.TripContent,
                    ApplyDate = p.ApplyDate,
                    UserCode = p.UserCode,
                    Status = p.Status,
                    ApplyType = "出差单"
                }).OrderBy(p => p.Status).Skip(pageindex * pagesize).Take(pagesize).ToList();

                var count1 = bqc.Trips.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.TripCode.Contains(ApplyCode) && p.UserCode.Contains(UserCode)).Count();
               
                //2.查询报销申请单
                var list2 = bqc.Reims.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.ReimCode.Contains(ApplyCode) && p.UserCode.Contains(UserCode)).Select(p => new
                {
                    ApplyCode = p.ReimCode,
                    ApplyContent = p.ReimContent,
                    ApplyDate = p.ApplyDate,
                    UserCode = p.UserCode,
                    Status = p.Status,
                    ApplyType = "报销单"
                }).OrderBy(p => p.Status).Skip(pageindex * pagesize).Take(pagesize).ToList();

                var count2 = bqc.Reims.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.ReimCode.Contains(ApplyCode) && p.UserCode.Contains(UserCode)).Count();

                //3.查询请假单
                var list3 = bqc.AskForLeaves.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.AskForLeaveCode.Contains(ApplyCode) && p.UserCode.Contains(UserCode)).Select(p => new
                {
                    ApplyCode = p.AskForLeaveCode,
                    ApplyContent = p.Reason,
                    ApplyDate = p.ApplyDate,
                    UserCode = p.UserCode,
                    Status = p.Status,
                    ApplyType="请假单"
                }).OrderBy(p => p.Status).Skip(pageindex * pagesize).Take(pagesize).ToList();

                var count3 = bqc.AskForLeaves.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.AskForLeaveCode.Contains(ApplyCode) && p.UserCode.Contains(UserCode)).Count();

                resultModel.Data = list1.Concat(list2).Concat(list3).OrderBy(p => p.Status).ThenByDescending(P => P.ApplyDate); //先按状态排序，再用ThenByDescending对申请日期降序排序
                resultModel.TotalCount = count1+count2+count3;
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }
            return resultModel;
        }


        /// <summary>
        /// 待我审批-审批通过
        /// </summary>
        /// <param name="ApplyCode"></param>
        /// <returns></returns>
        public ResultModel<object> WaitingApprove_Pass(string[] ApplyCodes)
        {
            BenqOAContext bqc=new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            try
            {
                foreach (string ApplyCode in ApplyCodes)
                {
                    if (ApplyCode.IndexOf("T") == 0) //出差单
                    {
                        Trip trip = bqc.Trips.Where(p => p.TripCode == ApplyCode).First();
                        trip.Status = "审批通过";
                    }
                    else if (ApplyCode.IndexOf("L") == 0) //请假单
                    {
                        AskForLeave leave = bqc.AskForLeaves.Where(p => p.AskForLeaveCode == ApplyCode).First();
                        leave.Status = "审批通过";
                    }
                    else if (ApplyCode.IndexOf("R") == 0) //报销单
                    {
                        Reim reim = bqc.Reims.Where(p => p.ReimCode == ApplyCode).First();
                        reim.Status = "审批通过";
                    }
                }

                bqc.SaveChanges(); //保存修改
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }

            return resultModel;
        }


        /// <summary>
        /// 待我审批-审批拒绝
        /// </summary>
        /// <param name="ApplyCode"></param>
        /// <returns></returns>
        public ResultModel<object> WaitingApprove_Reject(string[] ApplyCodes)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            try
            {
                foreach (string ApplyCode in ApplyCodes)
                {
                    if (ApplyCode.IndexOf("T") == 0) //出差单
                    {
                        Trip trip = bqc.Trips.Where(p => p.TripCode == ApplyCode).First();
                        trip.Status = "审批不通过";
                    }
                    else if (ApplyCode.IndexOf("L") == 0) //请假单
                    {
                        AskForLeave leave = bqc.AskForLeaves.Where(p => p.AskForLeaveCode == ApplyCode).First();
                        leave.Status = "审批不通过";
                    }
                    else if (ApplyCode.IndexOf("R") == 0) //报销单
                    {
                        Reim reim = bqc.Reims.Where(p => p.ReimCode == ApplyCode).First();
                        reim.Status = "审批不通过";
                    }
                }

                bqc.SaveChanges(); //保存修改
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }

            return resultModel;
        }
        #endregion
    }
}
