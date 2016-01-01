using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Models;
using System.Web;
//using System.Web.SessionState;
//using System.Web.HttpContext; //调用Session

namespace DAL
{
    //公用方法
    public class CommonDataDAL
    {
        //引用上下文类
        BenqOAContext bqc = new BenqOAContext();

        /// <summary>
        /// 性别下拉框 ResultModel中Data 参数类型是SelectListItem[]类型
        /// </summary>
        /// <returns></returns>
        public ResultModel<SelectListItem[]> GetSexInfo()
        {
            //必须引用EF程序集
            var list = bqc.Sexes.ToList(); //查询性别表
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            foreach (var li in list)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = li.SexName.ToString(),
                    Value = li.SexId.ToString()
                });
            }
            ResultModel<SelectListItem[]> resultModel = new ResultModel<SelectListItem[]>();
            try
            {
                resultModel.Data = selectListItem.ToArray(); //转为数组
                resultModel.ErrorCode = "0";
                resultModel.Message = "";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.Message;
            }
           
            return resultModel;
        }

        /// <summary>
        /// 职位下拉框
        /// </summary>
        /// <returns></returns>
        public ResultModel<SelectListItem[]> GetPosiInfo()
        {
            var list = bqc.Positions.ToList();
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            foreach(var li in list)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = li.PositionName,
                    Value = li.PositionCode
                });

            };
            ResultModel<SelectListItem[]> resultModel = new ResultModel<SelectListItem[]>();

            try
            {
                resultModel.Data = selectListItem.ToArray(); //转为数组
                resultModel.ErrorCode = "0";
                resultModel.Message = "";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.Message;
            }

            return resultModel;
        }

        /// <summary>
        /// 部门下拉框
        /// </summary>
        /// <returns></returns>
        public ResultModel<SelectListItem[]> GetDepInfo()
        {
            var list = bqc.Departments.ToList();
            List<SelectListItem> selectListItem = new List<SelectListItem>();

            foreach(var li in list)
            {
                selectListItem.Add(new SelectListItem  
                { 
                    Text=li.DepartmentName,
                    Value=li.DepartmentCode
                });     
            }

            ResultModel<SelectListItem[]> resultModel = new ResultModel<SelectListItem[]>();
            try
            {
                resultModel.Data = selectListItem.ToArray();
                resultModel.ErrorCode = "0";
                resultModel.Message = "";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "0";
                resultModel.Message = ee.Message;
            }

            return resultModel;
        }

        /// <summary>
        /// 角色下拉框
        /// </summary>
        /// <returns></returns>
        public ResultModel<SelectListItem[]> GetRoleInfo()
        {
            var list = bqc.Roles.ToList();
            List<SelectListItem> selectListItem = new List<SelectListItem>();

            foreach (var li in list)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = li.RoleName,
                    Value = li.RoleCode
                });
            }

            ResultModel<SelectListItem[]> resultModel = new ResultModel<SelectListItem[]>();
            try
            {
                resultModel.Data = selectListItem.ToArray();
                resultModel.ErrorCode = "0";
                resultModel.Message = "";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "0";
                resultModel.Message = ee.Message;
            }

            return resultModel;
        }

        /// <summary>
        /// 获取角色为部门经理的所有用户
        /// </summary>
        /// <returns></returns>
        public ResultModel<SelectListItem[]> GetPMUserCode()
        {

            var list = bqc.User_Role.Where(p => p.RoleCode == "R003").Select(p => p.UserCode).ToList(); //R012是部门经理角色
            List<SelectListItem> selectListItem = new List<SelectListItem>();

            foreach (var li in list)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = li,
                    Value = li
                });
            }

            ResultModel<SelectListItem[]> resultModel = new ResultModel<SelectListItem[]>();
            try
            {
                resultModel.Data = selectListItem.ToArray();
                resultModel.ErrorCode = "0";
                resultModel.Message = "";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.Message;
            }

            return resultModel;
        }

        /// <summary>
        /// 请假类型
        /// </summary>
        /// <returns></returns>
        public ResultModel<SelectListItem[]> GetLeaveType()
        {
            var list = bqc.AskForLeaveTypes.ToList();
            List<SelectListItem> selectListItem = new List<SelectListItem>();

            foreach (var li in list)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = li.TypeName,
                    Value = li.TypeId.ToString()
                });
            }

            ResultModel<SelectListItem[]> resultModel = new ResultModel<SelectListItem[]>();
            try
            {
                resultModel.Data = selectListItem.ToArray();
                resultModel.ErrorCode = "0";
                resultModel.Message = "";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "0";
                resultModel.Message = ee.Message;
            }

            return resultModel;
        }

        /// <summary>
        /// 公告类型
        /// </summary>
        /// <returns></returns>
        public ResultModel<SelectListItem[]> GetAnnoType()
        {
            var lists = bqc.AnnounceTypes.ToList();
            List<SelectListItem> selectListItem = new List<SelectListItem>();

            foreach(var list in lists)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text=list.AnnounceTypeName,
                    Value=list.AnnounceTypeId.ToString()

                });
            }

            ResultModel<SelectListItem[]> resultModel = new ResultModel<SelectListItem[]>();
            try
            {
                resultModel.Data = selectListItem.ToArray();
                resultModel.ErrorCode = "0";
                resultModel.Message = "";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "0";
                resultModel.Message = ee.Message;
            }

            return resultModel;
        }

        /// <summary>
        /// 文件类型
        /// </summary>
        /// <returns></returns>
        public ResultModel<SelectListItem[]> GetFileType()
        {
            var lists = bqc.FileTypes.ToList();
            List<SelectListItem> selectListItem = new List<SelectListItem>();

            foreach (var list in lists)
            {
                selectListItem.Add(new SelectListItem
                {
                    Text = list.FilesTypeName,
                    Value = list.FilesTypeId

                });
            }

            ResultModel<SelectListItem[]> resultModel = new ResultModel<SelectListItem[]>();
            try
            {
                resultModel.Data = selectListItem.ToArray();
                resultModel.ErrorCode = "0";
                resultModel.Message = "";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "0";
                resultModel.Message = ee.Message;
            }

            return resultModel;
        }

        /// <summary>
        /// 根据请假类型ID获取请假类型名
        /// </summary>
        /// <param name="id">请假类型ID</param>
        /// <returns></returns>
        public string GetLeaveNameById(int id)
        {
            var list = bqc.AskForLeaveTypes.Where(p => p.TypeId == id).FirstOrDefault();
            return list.TypeName;
        }

        /// <summary>
        /// 获取当前登录的用户
        /// </summary>
        /// <returns></returns>
        public string GetLoginUser()
        {
            User loginUser = HttpContext.Current.Session["userInfo"] as User;
            return loginUser.UserCode;
        }

        /// <summary>
        /// 把kendo日期控件20150213的格式转换成2015-02-13格式
        /// </summary>
        /// <param name="stringDate">日期</param>
        /// <returns></returns>
        public string ExchangeDate(string stringDate)
        {
            string exchangeDate = null;
            if (!string.IsNullOrEmpty(stringDate))
            {
                exchangeDate=stringDate.Substring(0, 4) + '-' + stringDate.Substring(4, 2) + '-' + stringDate.Substring(6, 2);
            }

            return exchangeDate;
        }


    }
}
