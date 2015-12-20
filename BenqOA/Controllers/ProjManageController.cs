using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model.Models;

namespace BenqOA.Controllers
{
    public class ProjManageController : BaseController
    {
        // 项目管理
        // GET: /ProjManage/

        public ActionResult ProjDiscuss()
        {
            User user = Session["userInfo"] as User;
            ViewBag.userCode = user.UserCode; //用户编号

            return View();
        }

        //项目查询
        public JsonResult ProjDiscuss_Query(string ProjCode, string ProjName, int pageindex, int pagesize)
        {
            ProjManageBLL bll = new ProjManageBLL();
            return Json(bll.ProjDiscuss_Query(ProjCode, ProjName, pageindex, pagesize));
        }

        //新增项目
        public ActionResult ProjDiscuss_Add(string UserCode)
        {
            ViewBag.userCode = UserCode; //用户编号
            return View();
        }

        //新增项目-保存
        public JsonResult ProjDiscuss_Add_Save(string UserCode, Project Project)
        {
            ProjManageBLL bll = new ProjManageBLL();
            return Json(bll.ProjDiscuss_Add_Save(UserCode, Project));
        }

        //删除项目
        public JsonResult ProjDiscuss_Del(string UserCode, string ProjCode)
        {
            ProjManageBLL bll = new ProjManageBLL();
            return Json(bll.ProjDiscuss_Del(UserCode, ProjCode));
        }

        //编辑项目-页面
        public ActionResult ProjDiscuss_Edit(string ProjCode, string ProjName)
        {
            ViewBag.ProjCode = ProjCode;
            ViewBag.ProjName = ProjName;
            return View();
        }

        //编辑项目-保存
        public JsonResult ProjDiscuss_Edit_Save(Project project)
        {
            ProjManageBLL bll = new ProjManageBLL();
            return Json(bll.ProjDiscuss_Edit_Save(project));
        }

        //项目讨论页面
        public ActionResult ProjDiscuss_Comment(string ProjCode, string UserCode)
        {
            ViewBag.projCode = ProjCode; //参与评论的项目编号
            ViewBag.userCode = UserCode; //用户编号

            ProjManageBLL bll = new ProjManageBLL();
            ViewBag.model = bll.ProjDiscuss_Comment(ProjCode);
            return View();
        }

        //发送讨论信息 
        public JsonResult ProjDiscuss_sendComment(string ProjCode, string UserCode, string Comment)
        {
            ProjManageBLL bll = new ProjManageBLL();
            return Json(bll.ProjDiscuss_sendComment(ProjCode, UserCode, Comment));
        }

        //判断是否刷新 
        public ActionResult ProjDiscuss_ifRefresh()
        {
            ProjManageBLL bll = new ProjManageBLL();
            bool flag = bll.ProjDiscuss_ifRefresh();
            return Json(new { flag=flag});
        }

        //#region comet发送消息
        ////LongPolling Action 1 - 处理客户端发起的请求
        //public void LongPollingAsync()
        //{
        //    //计时器，5秒种触发一次Elapsed事件
        //    System.Timers.Timer timer = new System.Timers.Timer(30000);
        //    //告诉ASP.NET接下来将进行一个异步操作
        //    AsyncManager.OutstandingOperations.Increment(); //增量
        //    //订阅计时器的Elapsed事件
        //    timer.Elapsed += (sender, e) =>
        //    {
        //        //保存将要传递给LongPollingCompleted的参数
        //        AsyncManager.Parameters["now"] = e.SignalTime;
        //        //告诉ASP.NET异步操作已完成，进行LongPollingCompleted方法的调用
        //        AsyncManager.OutstandingOperations.Decrement(); //减量
        //    };
        //    //启动计时器
        //    timer.Start();
        //}

        ////LongPolling Action 2 - 异步处理完成，向客户端发送响应
        //public ActionResult LongPollingCompleted(DateTime now)
        //{
        //    string contents = Request.Params["contents"].ToString();
        //    return Json(new
        //    {
        //        d = now.ToString("MM-dd HH:mm:ss ") +
        //            "-- Welcome to cnblogs.com!"+contents
        //    },
        //        JsonRequestBehavior.AllowGet);
        //}

        //#endregion
    }
}
