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

        //性别下拉框 ResultModel中Data 参数类型是SelectListItem[]类型
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

        //职位下拉框
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

        //部门下拉框
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

        //角色下拉框
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

        //获取角色为部门经理的所有用户
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

        //请假类型
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

        //公告类型
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

        //文件类型
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

        //根据请假类型ID获取请假类型名
        public string GetLeaveNameById(int Id)
        {
            var list = bqc.AskForLeaveTypes.Where(p => p.TypeId == Id).FirstOrDefault();
            return list.TypeName;
        }

        //获取当前登录的用户
        public string GetLoginUser()
        {
            User loginUser = HttpContext.Current.Session["userInfo"] as User;
            return loginUser.UserCode;
        }

        //把kendo日期控件20150213的格式转换成2015-02-13格式
        public string ExchangeDate(string StringDate)
        {
            string exchangeDate = null;
            if (!string.IsNullOrEmpty(StringDate))
            {
                exchangeDate=StringDate.Substring(0, 4) + '-' + StringDate.Substring(4, 2) + '-' + StringDate.Substring(6, 2);
            }

            return exchangeDate;
        }


    }
}
