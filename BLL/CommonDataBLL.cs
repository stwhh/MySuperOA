using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
using Model.Models;

namespace BLL
{
    //公用方法
    public class CommonDataBLL
    {
        //实例化DAL层
        CommonDataDAL dal = new CommonDataDAL();

        //性别下拉框
        public ResultModel<SelectListItem[]> GetSexInfo()
        {
            return dal.GetSexInfo();
        }

        //职位下拉框
        public ResultModel<SelectListItem[]> GetPosiInfo()
        {
            return dal.GetPosiInfo();
        }

        //部门下拉框
        public ResultModel<SelectListItem[]> GetDepInfo()
        {
            return dal.GetDepInfo();
        }

        //角色下拉框
        public ResultModel<SelectListItem[]> GetRoleInfo()
        {
            return dal.GetRoleInfo();
        }

        //获取角色为部门经理的所有用户
        public ResultModel<SelectListItem[]> GetPMUserCode()
        {
            CommonDataDAL dal = new CommonDataDAL();
            return dal.GetPMUserCode();
        }

        //请假类型
        public ResultModel<SelectListItem[]> GetLeaveType()
        {
            return dal.GetLeaveType();
        }

         //公告类型
        public ResultModel<SelectListItem[]> GetAnnoType()
        {
            CommonDataDAL dal = new CommonDataDAL();
            return dal.GetAnnoType();
        }

        //文件类型
        public ResultModel<SelectListItem[]> GetFileType()
        {
            CommonDataDAL dal = new CommonDataDAL();
            return dal.GetFileType();
        }

        //根据请假类型ID获取请假类型名
        public string GetLeaveNameById(int Id)
        {
            return dal.GetLeaveNameById(Id);
        }

        //获取当前登录的用户
        public string GetLoginUser()
        {
            return dal.GetLoginUser();
        }
    }
}
