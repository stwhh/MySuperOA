﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using Model.Models;

namespace Public
{
    public class MyAuthorFilter : AuthorizeAttribute
    {
        /// <summary>公用,不需要登录即可访问,如登录页面</summary>
        public const string PublicRole = "1";
        /// <summary>要登录才可以访问</summary>
        public const string LoginRole = "2";
        ///// <summary>管理员才能查看的页面</summary>
        //public const string AdminRole = "3";

        List<string> roles = new List<string>(); 


        /// <summary>
        /// 请求授权时执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //获得url请求里的controller和action：
            string controllerName = filterContext.RouteData.Values["controller"].ToString().ToLower();
            string actionName = filterContext.RouteData.Values["action"].ToString().ToLower();
           
            #region 暂时不用的
            //string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //string actionName = filterContext.ActionDescriptor.ActionName;

            //根据请求过来的controller和action去查询可以被哪些角色操作:
            //Models.RoleWithControllerAction roleWithControllerAction =
            //  base.SampleData.roleWithControllerAndAction.Find(r => r.ControllerName.ToLower() == controllerName &&
            //  tionName.ToLower() == actionName);
            //if (roleWithControllerAction != null)
            //{
            //    this.Roles = roleWithControllerAction.RoleIds;     //有权限操作当前控制器和Action的角色id
            //}
            #endregion

            roles = Roles.Split(',').ToList();
            if (roles.Contains(PublicRole)) //公共用户，不用登陆就无需验证
            {
                return;//不执行AuthorizeCore,即不检查授权
            }
            if (roles.Contains(LoginRole))  //登陆用户，需要验证
            {
                base.OnAuthorization(filterContext);  //进入AuthorizeCore
            }

            //base.OnAuthorization(filterContext);   //进入AuthorizeCore
        }
        

        /// <summary>
        /// 自定义授权检查（返回False则授权失败）
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //return base.AuthorizeCore(httpContext);
            string redirectUrl = "/Home/Login?ReturnUrl=" + httpContext.Server.UrlEncode(
                  httpContext.Request.Url == null ? "" : httpContext.Request.Url.AbsoluteUri);

            if (httpContext.Session["userInfo"] != null)
            {
                User user = httpContext.Session["userInfo"] as User;
                string userName = user.UserCode;
                BenqOAContext bqc = new BenqOAContext();
                if (roles.Count==0)
                {
                    return false;
                }
                if (httpContext.Request.Url.AbsoluteUri.Contains("/Home/Index"))
                {
                    return true; //首页不要权限控制
                }
                //根据用户编号查询用户所有的权限
                var userPermissonList = bqc.User_Role.Where(x => x.UserCode == userName)
                    .Join(bqc.Roles, p => p.RoleCode, q => q.RoleCode, (a, b) => new { uRoleCode = a.RoleCode, b.RoleCode })
                    .Join(bqc.Role_Permisson, p => p.RoleCode, q => q.RoleCode, (a, b) => new { b.PermCode })
                    .Join(bqc.Permissons, p => p.PermCode, q => q.PermCode, (a, b) => new { b.PermCode, b.PermUrl })
                    .Select(x => x.PermUrl).Distinct().ToList();
                foreach (var item in userPermissonList)
                {
                    if (httpContext.Request.Url.AbsoluteUri.Contains(item))
                    {
                        return true;
                    }
                }
                RedirectOrShow(httpContext, redirectUrl, "您没有权限");
                //return false;
            }

            return false;
        }


        /// <summary>
        /// 处理授权失败的HTTP请求
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.Redirect("/Home/Login");

            //base.HandleUnauthorizedRequest(filterContext);
        }


        /// <summary>
        /// 重定向或显示信息
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="redirectUrl"></param>
        /// <param name="message"></param>
        static void RedirectOrShow(HttpContextBase filterContext, string redirectUrl, string message)
        {
            if (filterContext.Request.IsAjaxRequest()) //如果是Ajax提交
            {
                filterContext.Response.Write(message);
            }
            else
            {
                filterContext.Response.Redirect(redirectUrl
                    + (redirectUrl.IndexOf('?') > 0 ? "&" : "?") + "message=" + filterContext.Server.UrlEncode(message));
            }
            filterContext.Response.End();
        }
    }
}
