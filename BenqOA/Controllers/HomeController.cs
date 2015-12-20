using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BenqOA.Helper;
using BLL;
using Model;
using Model.Models;
using System.Data; //sqlhelper

namespace BenqOA.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //主页显示公告列表
            BenqOAContext bqc = new BenqOAContext();
            var model = bqc.Announces.Where(p => p.Status == "已发布").OrderByDescending(p => p.CreateTime).Take(10).ToList();
            ViewBag.data = model;

            return View(model);
        }

        //查看公告信息页面
        public ActionResult GetAnnoInfo(string Annocode)
        {
            BenqOAContext bqc = new BenqOAContext();
            var model = bqc.Announces.Where(p => p.AnnounceCode == Annocode).ToList();
            ViewBag.data = model;

            return View();
        }

        //登录页面
        public ActionResult Login(string LoginOff)
        {
            //判断是否是注销
            if (!string.IsNullOrEmpty(LoginOff))
            {
                Session.Abandon();//清除缓存
            }
            return View();
        }

        //获取验证码
        public FileResult ValidationCode()
        {
            ValidateCodeHelper helper = new ValidateCodeHelper();
            var code = helper.CreateValidateCode(4); //生成验证码
            Session["ValidationCode"] = code;
            var bytes = helper.CreateValidateGraphic(code); //生成验证码图片
            return File(bytes, @"image/jpeg");
        }

        #region
        ////根绝用户名验证密码 登录
        //public JsonResult CheckUserPwd(string UserCode, string userPwd)
        //{
        //    SysManageBLL bll = new SysManageBLL();
        //    BenqOAContext bqc = new BenqOAContext();  //实例化上下文类
        //    var flag = false;

        //    //查询该用户解密后的密码与填写密码比较
        //    var pwd = EncryptAndDecrypt.DecryptDES(bqc.Users.First(p => p.UserCode == UserCode).UserPwd, "stwhh123");
        //    if (userPwd == pwd.ToString())
        //    {
        //        flag = true;

        //        var userInfo = bqc.Users.FirstOrDefault(p => p.UserCode == UserCode);
        //        Session["userInfo"] = userInfo;  //登录后保存session
        //        //User u = Session["userInfo"] as User;
        //        //string userName= u.UserName;

        //        //根据用户编号查询角色编号
        //        List<object> RoleCodes = bll.GetUserRoleByUserCode(UserCode);

        //        //根绝角色编号查权限编号
        //        List<object> PermCodes = bll.GetPermCodeByRoleCode(RoleCodes);

        //        //根据权限编号查权限信息
        //        List<Permisson> PermInfo = bll.GetPermInfoByRoleCode(PermCodes);

        //        //保存菜单权限信息  //Session["NavInfo"] = bqc.Permissons.OrderBy(m => m.PermSeq).ToList();//保存菜单权限信息
        //        Session["NavInfo"] = PermInfo;
        //    }

        //    return Json(flag,JsonRequestBehavior.AllowGet);
        //}
        #endregion

        //登录
        public ActionResult btnLogin(string UserCode, string userPwd, string CheckCode)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            if (string.IsNullOrEmpty(CheckCode))
            {
                resultModel.ErrorCode = "00"; //00表示验证码为空
            }
            else if (Session["ValidationCode"].ToString() != CheckCode)
            {
                resultModel.ErrorCode = "11"; //11表示验证码错误
            }
            else
            {
               
                BenqOAContext bqc = new BenqOAContext();  //实例化上下文类
                SysManageBLL bll = new SysManageBLL();
                ViewBag.UserCode = UserCode;

                //查询该用户解密后的密码与填写密码比较
                if (bqc.Users.FirstOrDefault(p => p.UserCode == UserCode) == null)
                {
                    resultModel.ErrorCode = "1"; //0表示没错
                }
                else
                {
                    try
                    {
                        var pwd = EncryptAndDecrypt.DecryptDES(bqc.Users.First(p => p.UserCode == UserCode).UserPwd, "stwhh123");
                        if (userPwd == pwd.ToString())
                        {
                            resultModel.ErrorCode = "0"; //0表示没错
                            resultModel.Message = "";
                            var userInfo = bqc.Users.FirstOrDefault(p => p.UserCode == UserCode);
                            Session["userInfo"] = userInfo;  //登录后保存session
                            //User u = Session["userInfo"] as User;
                            //string userName= u.UserName;

                            //根据用户编号查询角色编号
                            List<object> RoleCodes = bll.GetUserRoleByUserCode(UserCode);

                            //根绝角色编号查权限编号
                            List<object> PermCodes = bll.GetPermCodeByRoleCode(RoleCodes);

                            //根据权限编号查权限信息
                            List<Permisson> PermInfo = bll.GetPermInfoByRoleCode(PermCodes);

                            //保存菜单权限信息  //Session["NavInfo"] = bqc.Permissons.OrderBy(m => m.PermSeq).ToList();//保存菜单权限信息
                            Session["NavInfo"] = PermInfo;
                        }
                        else
                        {
                            resultModel.ErrorCode = "1"; //0表示没错
                            resultModel.Message = "";
                        }
                    }
                    catch (Exception ee)
                    {
                        resultModel.ErrorCode = "1"; //0表示没错
                        resultModel.Message = ee.Message;
                    }
                }
            }
            return Json(resultModel);
        }


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns>页面</returns>
        public ActionResult ChangePassword()
        {
            User user = Session["userInfo"] as User;
            ViewBag.UserCode = user.UserCode;

            return View();
        }

        //修改密码-保存
        public ActionResult ChangePassword_Save(string UserCode, string OldPassword, string NewPassword)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            User user = bqc.Users.Where(p => p.UserCode == UserCode).FirstOrDefault();
            //把用户输入的旧密码加密后和原密码比较
            var inputUserOldPwd = EncryptAndDecrypt.EncryptDES(OldPassword, "stwhh123");
            if (user.UserPwd == inputUserOldPwd)
            {
                //修改原密码
                user.UserPwd = EncryptAndDecrypt.EncryptDES(NewPassword, "stwhh123");
                bqc.SaveChanges();
                resultModel.ErrorCode = "0";
            }
            else if (user.UserPwd != inputUserOldPwd)
            {
                resultModel.ErrorCode = "1";
            }
            else
            {
                resultModel.ErrorCode = "2";
            }
            return Json(resultModel);
        }
    }

}
