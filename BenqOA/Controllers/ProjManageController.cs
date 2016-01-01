using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model.Models;
using Public;

namespace BenqOA.Controllers
{
    [MyAuthorFilter(Roles = MyAuthorFilter.LoginRole)]
    public class ProjManageController : Controller
    {
        // 项目管理
        /// <summary>
        /// 项目讨论页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjDiscuss()
        {
            User user = Session["userInfo"] as User;
            ViewBag.userCode = user.UserCode; //用户编号

            return View();
        }

        /// <summary>
        /// 项目查询
        /// </summary>
        /// <param name="projCode">项目编号</param>
        /// <param name="projName">项目名称</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public JsonResult ProjDiscuss_Query(string projCode, string projName, int pageindex, int pagesize)
        {
            ProjManageBLL bll = new ProjManageBLL();
            return Json(bll.ProjDiscuss_Query(projCode, projName, pageindex, pagesize));
        }

        /// <summary>
        /// 新增项目页面
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public ActionResult ProjDiscuss_Add(string userCode)
        {
            ViewBag.userCode = userCode; //用户编号
            return View();
        }

        /// <summary>
        /// 新增项目-保存
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <param name="project">项目实体</param>
        /// <returns></returns>
        public JsonResult ProjDiscuss_Add_Save(string userCode, Project project)
        {
            ProjManageBLL bll = new ProjManageBLL();
            return Json(bll.ProjDiscuss_Add_Save(userCode, project));
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <param name="projCode">项目编号</param>
        /// <returns></returns>
        public JsonResult ProjDiscuss_Del(string userCode, string projCode)
        {
            ProjManageBLL bll = new ProjManageBLL();
            return Json(bll.ProjDiscuss_Del(userCode, projCode));
        }

        /// <summary>
        /// 编辑项目-页面
        /// </summary>
        /// <param name="projCode">项目编号</param>
        /// <param name="projName">项目名称</param>
        /// <returns></returns>
        public ActionResult ProjDiscuss_Edit(string projCode, string projName)
        {
            ViewBag.ProjCode = projCode;
            ViewBag.ProjName = projName;
            return View();
        }

        /// <summary>
        /// 编辑项目-保存
        /// </summary>
        /// <param name="project">项目实体</param>
        /// <returns></returns>
        public JsonResult ProjDiscuss_Edit_Save(Project project)
        {
            ProjManageBLL bll = new ProjManageBLL();
            return Json(bll.ProjDiscuss_Edit_Save(project));
        }

        /// <summary>
        /// 项目讨论页面
        /// </summary>
        /// <param name="projCode">项目编号</param>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public ActionResult ProjDiscuss_Comment(string projCode, string userCode)
        {
            ViewBag.projCode = projCode; //参与评论的项目编号
            ViewBag.userCode = userCode; //用户编号

            ProjManageBLL bll = new ProjManageBLL();
            ViewBag.model = bll.ProjDiscuss_Comment(projCode);
            return View();
        }

        /// <summary>
        /// 发送讨论信息 
        /// </summary>
        /// <param name="projCode">项目编号</param>
        /// <param name="userCode">用户编号</param>
        /// <param name="comment">评论信息</param>
        /// <returns></returns>
        public JsonResult ProjDiscuss_sendComment(string projCode, string userCode, string comment)
        {
            ProjManageBLL bll = new ProjManageBLL();
            return Json(bll.ProjDiscuss_sendComment(projCode, userCode, comment));
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
