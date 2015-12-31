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
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();
            ////1.把数字形式的时间截取拼接成2015-05-17
            //CommonDataDAL dal = new CommonDataDAL();
            //string beginApplyDate = dal.ExchangeDate(BeginApplyDate);
            //string endApplyDate = dal.ExchangeDate(EndApplyDate);

            DateTime bd = Convert.ToDateTime(beginApplyDate);
            DateTime ed = Convert.ToDateTime(endApplyDate);

            try
            {
                var list = bqc.Trips.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.UserCode == userCode && p.TripCode.Contains(tripCode) && p.TripContent.Contains(tripContent))
                .OrderByDescending(p => p.ApplyDate).Skip(pageindex * pagesize).Take(pagesize).ToList();

                var count = bqc.Trips.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.UserCode == userCode && p.TripCode.Contains(tripCode) && p.TripContent.Contains(tripContent)).Count();

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
        /// <param name="trip">出差实体</param>
        /// <param name="userCode">用户编号</param>
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
        /// <param name="tripCode">出差编号</param>
        /// <returns></returns>
        public Trip TripManage_Detail(string tripCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            Trip list = bqc.Trips.Where(p => p.TripCode == tripCode).FirstOrDefault();
            //修改时间显示格式为2015-08-19

            return list; ;
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
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            DateTime bd = Convert.ToDateTime(beginApplyDate);
            DateTime ed = Convert.ToDateTime(endApplyDate);

            try
            {
                var list = bqc.Reims.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.UserCode == userCode && p.ReimCode.Contains(reimCode) && p.ReimContent.Contains(reimContent))
                .OrderByDescending(p => p.ApplyDate).Skip(pageindex * pagesize).Take(pagesize).ToList();

                var count = bqc.Reims.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.UserCode == userCode && p.ReimCode.Contains(reimCode) && p.ReimContent.Contains(reimContent))
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
        /// <param name="userCode">用户编号</param>
        /// <param name="tripContent">出差内容</param>
        /// <param name="beginApplyDate">开始日期</param>
        /// <param name="endApplyDate">结束日期</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public ResultModel<object> ReimManage_QueryTrip(string userCode, string tripContent, string beginApplyDate, string endApplyDate, int pageindex, int pagesize)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            DateTime bd = Convert.ToDateTime(beginApplyDate);
            DateTime ed = Convert.ToDateTime(endApplyDate);

            try
            {
                var list = bqc.Trips.Where(p => p.BeginDate.CompareTo(bd) >= 0 && p.BeginDate.CompareTo(ed) <= 0
                && p.UserCode == userCode && p.TripContent.Contains(tripContent) && p.Status == "审批通过" && p.IsReim == "N")
                .OrderBy(p => p.ApplyDate).Skip(pageindex * pagesize).Take(pagesize).ToList();

                var count = bqc.Trips.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.UserCode == userCode && p.TripContent.Contains(tripContent) && p.Status == "审批通过" && p.IsReim == "N").Count();

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
        /// <param name="tripCode">出差编号</param>
        /// <returns></returns>
        public Trip ReimManage_AddByTripCode(string tripCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            Trip list = bqc.Trips.Where(p => p.TripCode == tripCode).FirstOrDefault();

            return list; ;

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
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            try
            {
                //保存报销单
                reim.UserCode = userCode;
                reim.TripCode = trip.TripCode;
                reim.ApplyDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                reim.Status = "待审批";
                bqc.Reims.Add(reim);

                //把出差单中 是否报销 改为Y
                Trip tripMdoel = bqc.Trips.Where(p => p.TripCode == trip.TripCode).First();
                tripMdoel.IsReim = "Y";

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
        /// <param name="reimCode">报销编号</param>
        /// <returns></returns>
        public Reim ReimManage_Detail(string reimCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            Reim list = bqc.Reims.Where(p => p.ReimCode == reimCode).FirstOrDefault();

            return list; ;
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
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            DateTime bd = Convert.ToDateTime(beginApplyDate);
            DateTime ed = Convert.ToDateTime(endApplyDate);

            try
            {
                var list = bqc.AskForLeaves.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.UserCode == userCode && p.AskForLeaveCode.Contains(askForLeaveCode) && p.Reason.Contains(reason))
                .OrderByDescending(p => p.ApplyDate).Skip(pageindex * pagesize).Take(pagesize).ToList();

                var count = bqc.AskForLeaves.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.UserCode == userCode && p.AskForLeaveCode.Contains(askForLeaveCode) && p.Reason.Contains(reason)).Count();

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
        /// <param name="leave">请假实体</param>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public ResultModel<object> LeaveManage_Add_Save(AskForLeave leave, string userCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            try
            {
                AskForLeave askForLeave = new AskForLeave();
                askForLeave.AskForLeaveCode = leave.AskForLeaveCode;
                askForLeave.ApplyDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                askForLeave.BeginDate = leave.BeginDate;
                askForLeave.EndDate = leave.EndDate;
                askForLeave.Reason = leave.Reason;
                askForLeave.TypeId = leave.TypeId;
                askForLeave.Status = "待审批";
                askForLeave.UserCode = userCode;

                bqc.AskForLeaves.Add(askForLeave);
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
        /// <param name="askForLeaveCode">请假编号</param>
        /// <returns></returns>
        public AskForLeave LeaveManage_Detail(string askForLeaveCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            AskForLeave list = bqc.AskForLeaves.Where(p => p.AskForLeaveCode == askForLeaveCode).FirstOrDefault();
            //AskForLeave list = bqc.AskForLeaves.Join(bqc.AskForLeaveTypes.Where()).Where(p=>p.AskForLeaveCode==AskForLeaveCode);

            return list; ;
        }
        #endregion


        #region 待我审批

        /// <summary>
        /// 待我审批-查询
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <param name="beginApplyDate">开始审批时间</param>
        /// <param name="endApplyDate">结束申请时间</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public ResultModel<object> WaitingApprove_Query(string userCode, string beginApplyDate, string endApplyDate, int pageindex, int pagesize)
        {
            if (userCode == null) throw new ArgumentNullException("userCode");
            return WaitingApprove_Query(null, userCode, beginApplyDate, endApplyDate, pageindex, pagesize);
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
        public ResultModel<object> WaitingApprove_Query(string applyCode, string userCode, string beginApplyDate, string endApplyDate, int pageindex, int pagesize)
        {
            if (applyCode == null) throw new ArgumentNullException("applyCode");
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            DateTime bd = Convert.ToDateTime(beginApplyDate);
            DateTime ed = Convert.ToDateTime(endApplyDate);

            try
            {
                //1.查询出差申请单
                var list1 = bqc.Trips.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.TripCode.Contains(applyCode) && p.UserCode.Contains(userCode)).Select(p => new
                {
                    ApplyCode = p.TripCode,
                    ApplyContent = p.TripContent,
                    ApplyDate = p.ApplyDate,
                    UserCode = p.UserCode,
                    Status = p.Status,
                    ApplyType = "出差单"
                }).OrderBy(p => p.Status).Skip(pageindex * pagesize).Take(pagesize).ToList();

                var count1 = bqc.Trips.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.TripCode.Contains(applyCode) && p.UserCode.Contains(userCode)).Count();
               
                //2.查询报销申请单
                var list2 = bqc.Reims.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.ReimCode.Contains(applyCode) && p.UserCode.Contains(userCode)).Select(p => new
                {
                    ApplyCode = p.ReimCode,
                    ApplyContent = p.ReimContent,
                    ApplyDate = p.ApplyDate,
                    UserCode = p.UserCode,
                    Status = p.Status,
                    ApplyType = "报销单"
                }).OrderBy(p => p.Status).Skip(pageindex * pagesize).Take(pagesize).ToList();

                var count2 = bqc.Reims.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.ReimCode.Contains(applyCode) && p.UserCode.Contains(userCode)).Count();

                //3.查询请假单
                var list3 = bqc.AskForLeaves.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.AskForLeaveCode.Contains(applyCode) && p.UserCode.Contains(userCode)).Select(p => new
                {
                    ApplyCode = p.AskForLeaveCode,
                    ApplyContent = p.Reason,
                    ApplyDate = p.ApplyDate,
                    UserCode = p.UserCode,
                    Status = p.Status,
                    ApplyType="请假单"
                }).OrderBy(p => p.Status).Skip(pageindex * pagesize).Take(pagesize).ToList();

                var count3 = bqc.AskForLeaves.Where(p => p.ApplyDate.CompareTo(bd) >= 0 && p.ApplyDate.CompareTo(ed) <= 0
                && p.AskForLeaveCode.Contains(applyCode) && p.UserCode.Contains(userCode)).Count();

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
        /// <param name="applyCodes">选中的项</param>
        /// <returns></returns>
        public ResultModel<object> WaitingApprove_Pass(string[] applyCodes)
        {
            BenqOAContext bqc=new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            try
            {
                foreach (string applyCode in applyCodes)
                {
                    if (applyCode.IndexOf("T") == 0) //出差单
                    {
                        Trip trip = bqc.Trips.Where(p => p.TripCode == applyCode).First();
                        trip.Status = "审批通过";
                    }
                    else if (applyCode.IndexOf("L") == 0) //请假单
                    {
                        AskForLeave leave = bqc.AskForLeaves.Where(p => p.AskForLeaveCode == applyCode).First();
                        leave.Status = "审批通过";
                    }
                    else if (applyCode.IndexOf("R") == 0) //报销单
                    {
                        Reim reim = bqc.Reims.Where(p => p.ReimCode == applyCode).First();
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
        /// <param name="applyCodes">选中的项</param>
        /// <returns></returns>
        public ResultModel<object> WaitingApprove_Reject(string[] applyCodes)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            try
            {
                foreach (string applyCode in applyCodes)
                {
                    if (applyCode.IndexOf("T") == 0) //出差单
                    {
                        Trip trip = bqc.Trips.Where(p => p.TripCode == applyCode).First();
                        trip.Status = "审批不通过";
                    }
                    else if (applyCode.IndexOf("L") == 0) //请假单
                    {
                        AskForLeave leave = bqc.AskForLeaves.Where(p => p.AskForLeaveCode == applyCode).First();
                        leave.Status = "审批不通过";
                    }
                    else if (applyCode.IndexOf("R") == 0) //报销单
                    {
                        Reim reim = bqc.Reims.Where(p => p.ReimCode == applyCode).First();
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
