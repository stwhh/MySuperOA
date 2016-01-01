using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using Model.Models;
using Public;

namespace BenqOA.Controllers
{
    [MyAuthorFilter(Roles = MyAuthorFilter.LoginRole)]
    public class CommonController:Controller
    {
        //公有方法

        CommonDataBLL bll = new CommonDataBLL();

        /// <summary>
        /// 获取性别
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSexInfo()
        {
            return Json(bll.GetSexInfo());
        }

        /// <summary>
        /// 获取职位
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPosiInfo()
        {
            return Json(bll.GetPosiInfo());
        }

        /// <summary>
        /// 获取部门
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDepInfo()
        {
            return Json(bll.GetDepInfo());
        }

        /// <summary>
        /// 获取角色
        /// </summary>
        /// <returns></returns>
        public JsonResult GetRoleInfo()
        {
            return Json(bll.GetRoleInfo());
        }

        /// <summary>
        /// 获取角色为部门经理的所有用户
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPmUserCode()
        {
            CommonDataBLL bll = new CommonDataBLL();
            return Json(bll.GetPMUserCode());
        }

        /// <summary>
        /// 获取请假类型
        /// </summary>
        /// <returns></returns>
        public JsonResult GetLeaveType()
        {
            return Json(bll.GetLeaveType());
        }

        /// <summary>
        /// 获取公告类型
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAnnoType()
        {
            return Json(bll.GetAnnoType());
        }

        /// <summary>
        /// 文件类型
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFileType()
        {
            CommonDataBLL bll = new CommonDataBLL();
            return Json(bll.GetFileType());
        }

        //根据请假类型ID获取请假类型名
        public ActionResult GetLeaveNameById(int Id)
        {
            //content 返回字符串
            ResultModel<object> resultModel = new ResultModel<object>();
            resultModel.Data = bll.GetLeaveNameById(Id);
            return Json(resultModel);
        }

        ////刚登录时把用户信息保存在session里
        //public ActionResult GetNav()
        //{
        //    User user = Session["userInfo"] as User;
        //    List<Permisson> list = null;
        //    using(var bqc=new BenqOAContext())
        //    {
        //        list = bqc.Permissons.OrderBy(m => m.PermSeq).ToList();
        //    }
        //    Session["NavInfo"] = list;
        //    return View();
        //}
    }
}
