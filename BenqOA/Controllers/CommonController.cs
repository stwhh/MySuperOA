using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using Model.Models;

namespace BenqOA.Controllers
{
    public class CommonController : BaseController
    {
        //公有方法
        // GET: /Common/

        CommonDataBLL bll = new CommonDataBLL();
        //获取性别
        public JsonResult GetSexInfo()
        {
            return Json(bll.GetSexInfo());
        }

        //获取职位
        public JsonResult GetPosiInfo()
        {
            return Json(bll.GetPosiInfo());
        }

        //获取部门
        public JsonResult GetDepInfo()
        {
            return Json(bll.GetDepInfo());
        }

        //获取角色
        public JsonResult GetRoleInfo()
        {
            return Json(bll.GetRoleInfo());
        }

        //获取角色为部门经理的所有用户
        public JsonResult GetPMUserCode()
        {
            CommonDataBLL bll = new CommonDataBLL();
            return Json(bll.GetPMUserCode());
        }

        //获取请假类型
        public JsonResult GetLeaveType()
        {
            return Json(bll.GetLeaveType());
        }

        //获取公告类型
        public JsonResult GetAnnoType()
        {
            return Json(bll.GetAnnoType());
        }

        //文件类型
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
