using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;
using BenqOA.Helper;
using BLL;
using Model;
using Model.Models;

namespace BenqOA.Controllers
{
    public class SysManageController : BaseController
    {
        //
        // GET: /SysManage/

        #region 用户管理
        public ActionResult UserManage()
        {
            return View();
        }

        //查询用户
        public JsonResult UserManage_QueryUser(string UserCode, string RealName, string DepartmentCode, int pageindex, int pagesize)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.UserManage_QueryUser(UserCode, RealName, DepartmentCode, pageindex, pagesize));
        }

        //根据UserCode查询用户详细信息
        public ActionResult UserManage_Detail(string userCode, string pageAction)
        {
            BenqOAContext bqc = new BenqOAContext();
            User model = new User();
            ViewBag.pageAction = pageAction;
            try
            {
                model = bqc.Users.FirstOrDefault(p => p.UserCode == userCode) as User;
                //密码解密显示
                model.UserPwd = EncryptAndDecrypt.DecryptDES(model.UserPwd, "stwhh123");
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
            return View(model);
        }

        //添加用户页面
        public ActionResult UserManage_AddUser()
        {
            return View();
        }

        //验证用户名是否存在
        public ActionResult CheckUserCode(string UserCode)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.CheckUserCode(UserCode), JsonRequestBehavior.AllowGet);
        }

        //添加用户-保存
        public JsonResult UserManage_AddUser_Save(User User)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            using (var bqc = new BenqOAContext())
            {
                //bqc.Users.Add(user);  //方法1   --bqc.Users.Remove(user);删除
                User user = Session["userInfo"] as User; //获取session
                User.CreateUserCode = user.UserCode; //创建用户
                User.CreateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")); //创建时间
                User.ModifyUserCode = user.UserCode; //修改用户
                User.ModifyTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")); //修改时间

                //对密码进行加密
                User.UserPwd = EncryptAndDecrypt.EncryptDES(User.UserPwd, "stwhh123");

                try
                {
                    bqc.Entry(User).State = EntityState.Added; //方法2 在using System.Data命名空间下; 
                    bqc.SaveChanges();
                    resultModel.ErrorCode = "0";
                    resultModel.Message = "";

                }
                catch (Exception ee)
                {
                    resultModel.ErrorCode = "1";
                    resultModel.Message = ee.Message;
                }

            }

            return Json(resultModel);
        }

        //删除用户
        public JsonResult UserManage_DelUser(string userCode)
        {
            //string ucode = Request.Params["userCode"].ToString(); //要使用此方法，前台不能以json格式传值，
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.UserManage_DelUser(userCode));
        }

        //修改用户信息
        public JsonResult UserManage_AlterUser(User user)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            User User = bqc.Users.FirstOrDefault(p => p.UserCode == user.UserCode) as User;

            User.RealName = user.RealName;
            if (!string.IsNullOrEmpty(user.UserPwd))
            {
                User.UserPwd = EncryptAndDecrypt.EncryptDES(user.UserPwd, "stwhh123"); //密码加密保存
            }
            try
            {
                User.Sex = user.Sex;
                User.Age = user.Age;
                User.Phone = user.Phone;
                User.QQ = user.QQ;
                User.Email = user.Email;
                User.EntryDate = user.EntryDate;
                User.Address = user.Address;
                User.PositionCode = user.PositionCode;
                User.DepartmentCode = user.DepartmentCode;
                User.ModifyUserCode = user.UserCode; //修改用户
                User.ModifyTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")); //修改时间

                //bqc.Entry(User).State = EntityState.Modified; //修改用户信息 修改可以不要这句
                bqc.SaveChanges();
                resultModel.ErrorCode = "0";
            }
            catch
            {
                resultModel.ErrorCode = "1";
                resultModel.Data = "";
            }
           
            return Json(resultModel);
        }

        #endregion


        #region 角色管理
        public ActionResult RoleManage()
        {
            return View();
        }

        //查询角色
        public JsonResult RoleManage_Query(string RoleCode, string RoleName, int pageindex, int pagesize)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.RoleManage_Query(RoleCode, RoleName, pageindex, pagesize));
        }

        //根据角色编号查询所有属于这个角色的人员 User_Role-页面
        public ActionResult RoleManage_User(string RoleCode)
        {
            ViewBag.RoleCode = RoleCode; //给页面其它方法调用
            return View();
        }

        //角色组用户页面--查询
        public JsonResult Role_User_Query(string RoleCode, string UserCode, string RealName)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.Role_User_Query(RoleCode, UserCode, RealName));
        }

        //添加角色组用户页面
        public ActionResult RoleManage_User_Add(string RoleCode)
        {
            ViewBag.RoleCode = RoleCode;
            return View();
        }

        //添加角色组用户-保存
        public ActionResult Role_AddUser_Save(string RoleCode, string selects)
        {
            ViewBag.RoleCode = RoleCode;
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.Role_AddUser_Save(RoleCode, selects));
        }


        //删除角色组用户
        public ActionResult DelRole_User(string RoleCode, string selects)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.DelRole_User(RoleCode, selects));
        }


        //查询不在当前角色组的所有用户-无分页
        public JsonResult Role_QueryAllUser(string RoleCode, string UserCode, string RealName)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.Role_QueryAllUser(RoleCode, UserCode, RealName));
        }


        //删除角色
        public JsonResult RoleManage_DelRole(string RoleCode)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.RoleManage_DelRole(RoleCode));

        }

        //新增角色
        public ActionResult RoleManage_AddRole()
        {
            return View();
        }

        //编辑角色
        public ActionResult RoleManage_EditRole(string RoleCode)
        {
            ViewBag.RoleCode = RoleCode;

            BenqOAContext bqc = new BenqOAContext();
            var model = bqc.Roles.Where(p => p.RoleCode == RoleCode).First();
            return View(model);
        }

        //编辑角色—保存
        public ActionResult RoleManage_EditRole_Save(Role role)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.RoleManage_EditRole_Save(role));
        }


        // 验证角色编号是否存在
        public JsonResult CheckRoleCode(string RoleCode)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.CheckRoleCode(RoleCode), JsonRequestBehavior.AllowGet);
        }

        //新增角色—保存
        public ActionResult RoleManage_AddRole_Save(Role role)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.RoleManage_AddRole_Save(role));
        }

        //给角色赋权页面
        public ActionResult RoleManage_AddPerm(string RoleCode)
        {
            ViewBag.RoleCode = RoleCode;

            return View();
        }

        //查询所有角色权限TreeView
        public ActionResult RoleManage_AddPerm_All(string RoleCode)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.RoleManage_AddPerm_All(RoleCode), JsonRequestBehavior.AllowGet);
        }

        //查询当前角色已存在的权限信息
        public ActionResult RoleManage_AddPerm_ExistPerm(string RoleCode)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.RoleManage_AddPerm_ExistPerm(RoleCode), JsonRequestBehavior.AllowGet);
        }

        //保存当前角色选择的权限
        public ActionResult RoleManage_AddPerm_Save(string RoleCode, string perms)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.RoleManage_AddPerm_Save(RoleCode, perms));
        }

        #endregion


        #region 权限管理
        public ActionResult PermManage()
        {
            return View();
        }

        //权限管理-查询
        public JsonResult PermManage_Query(string PermCode, string PermName, int pageindex, int pagesize)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.PermManage_Query(PermCode, PermName, pageindex, pagesize));
        }

        //权限管理-新增页面
        public ActionResult PermManage_Add()
        {
            return View();
        }

        // 验证权限编号是否存在
        public JsonResult CheckPermCode(string PermCode)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.CheckPermCode(PermCode), JsonRequestBehavior.AllowGet);
        }

        //权限管理-新增 保存
        public JsonResult PermManage_Add_Save(Permisson permisson)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.PermManage_Add_Save(permisson));
        }

        //权限管理-删除
        public JsonResult PermManage_Del(string PermCode)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.PermManage_Query(PermCode));
        }

        //权限管理-编辑
        public ActionResult PermManage_Edit(string PermCode)
        {
            ViewBag.PermCode = PermCode;
            SysManageBLL bll = new SysManageBLL();
            return View(bll.PermManage_Edit(PermCode));

        }

        //权限管理-编辑-保存
        public JsonResult PermManage_Edit_Save(string PermCode, Permisson perm)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.PermManage_Edit_Save(PermCode, perm));
        }

        #endregion


        #region 部门管理
        public ActionResult DepaManage()
        {
            return View();
        }

        //部门管理-查询
        public JsonResult DepaManage_Query(string DepartmentCode, string DepartmentName, string DepartmentManageCode, int pageindex, int pagesize)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.DepaManage_Query(DepartmentCode, DepartmentName, DepartmentManageCode, pageindex, pagesize));
        }


        // 验证部门编号是否存在
        public JsonResult CheckDepaCode(string DepartmentCode)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.CheckDepaCode(DepartmentCode), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 部门管理-新增
        /// </summary>
        /// <returns></returns>
        public ActionResult DepaManage_Add(Department department)
        {
            return View();
        }


        /// <summary>
        /// 部门管理-新增-保存
        /// </summary>
        /// <returns></returns>
        public JsonResult DepaManage_Add_Save(Department department)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.DepaManage_Add_Save(department));
        }


        //部门管理-删除
        public JsonResult DepaManage_Del(string DepartmentCode)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.DepaManage_Del(DepartmentCode));
        }


        //部门管理-编辑
        public ActionResult DepaManage_Edit(string DepartmentCode)
        {
            SysManageBLL bll = new SysManageBLL();
            return View(bll.DepaManage_Edit(DepartmentCode));
        }


        //部门管理-编辑-保存
        public JsonResult DepaManage_Edit_Save(string DepartmentCode, string DepartmentName, string DepartmentManageCode)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.DepaManage_Edit_Save(DepartmentCode, DepartmentName, DepartmentManageCode));
        }
        #endregion


        #region 职位管理
        public ActionResult PosiManage()
        {
            return View();
        }

        //职位管理-查询
        public JsonResult PosiManage_Query(string PositionCode, string PositionName, int pageindex, int pagesize)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.PosiManage_Query(PositionCode, PositionName, pageindex, pagesize));
        }

        // 验证职位编号是否存在
        public JsonResult CheckPosiCode(string PositionCode)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.CheckPosiCode(PositionCode), JsonRequestBehavior.AllowGet);
        }

        //职位管理-新增
        public ActionResult PosiManage_Add()
        {
            return View();
        }

        //职位管理-新增-保存
        public JsonResult PosiManage_Add_Save(Position position)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.PosiManage_Add_Save(position));
        }

        //职位管理-删除
        public ActionResult PosiManage_Del(string PositionCode)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.PosiManage_Del(PositionCode));
        }

        //职位管理-编辑(根据职位编号查询职位信息)
        public ActionResult PosiManage_Edit(string PositionCode)
        {
            ViewBag.PositionCode = PositionCode;
            SysManageBLL bll = new SysManageBLL();
            return View(bll.PosiManage_Edit(PositionCode));
        }

        //职位管理-编辑-保存
        public JsonResult PosiManage_Edit_Save(Position position)
        {
            SysManageBLL bll = new SysManageBLL();
            return Json(bll.PosiManage_Edit_Save(position));
        }

        #endregion


        //修改密码
        public ActionResult AlterPwd()
        {
            return View();
        }

    }
}
