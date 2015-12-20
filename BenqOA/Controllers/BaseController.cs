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
    public class BaseController : Controller
    {

        //当前登陆用户属性
        public User currentUserInfo { get; set; }  //表示对类的读写
        /// <summary>
        /// 重写控制器执行前的方法
        /// </summary>
        /// <param name="filterContext">重写方法的参数</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            currentUserInfo = Session["userInfo"] as User;
            if (currentUserInfo == null)
            {
                Response.Redirect("/Home/Login");
            }
        }
        

    }
}
