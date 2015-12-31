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
using Public;

namespace BenqOA.Controllers
{
    [MyAuthorFilter(Roles = "2,3")]  //2是登陆用户，3是超级管理员
    public class SysManageController : Controller
    {

        #region 用户管理
        /// <summary>
        /// 用户管理
        /// </summary>
        /// <returns></returns>
        public ActionResult UserManage()
        {
            return View();
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="departmentCode">部门编号</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public JsonResult UserManage_QueryUser(string userCode, string realName, string departmentCode, int pageindex, int pagesize)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.UserManage_QueryUser(userCode, realName, departmentCode, pageindex, pagesize));
        }

        /// <summary>
        /// 根据UserCode查询用户详细信息
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <param name="pageAction">页面属性（编辑查看）</param>
        /// <returns></returns>
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

        /// <summary>
        /// 添加用户页面
        /// </summary>
        /// <returns></returns>
        public ActionResult UserManage_AddUser()
        {
            return View();
        }

        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public ActionResult CheckUserCode(string userCode)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.CheckUserCode(userCode), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加用户-保存
        /// </summary>
        /// <param name="user">用户实体</param>
        /// <returns></returns>
        public JsonResult UserManage_AddUser_Save(User user)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            using (var bqc = new BenqOAContext())
            {
                //bqc.Users.Add(user);  //方法1   --bqc.Users.Remove(user);删除
                User userSession = Session["userInfo"] as User; //获取session
                user.CreateUserCode = userSession.UserCode; //创建用户
                user.CreateTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")); //创建时间
                user.ModifyUserCode = userSession.UserCode; //修改用户
                user.ModifyTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")); //修改时间

                //对密码进行加密
                user.UserPwd = EncryptAndDecrypt.EncryptDES(user.UserPwd, "stwhh123");
                try
                {
                    bqc.Entry(user).State = EntityState.Added; //方法2 在using System.Data命名空间下; 
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

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public JsonResult UserManage_DelUser(string userCode)
        {
            //string ucode = Request.Params["userCode"].ToString(); //要使用此方法，前台不能以json格式传值，
            SysManageBll bll = new SysManageBll();
            return Json(bll.UserManage_DelUser(userCode));
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="userNewInfo">用户实体</param>
        /// <returns></returns>
        public JsonResult UserManage_AlterUser(User userNewInfo)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            User userOldInfo = bqc.Users.FirstOrDefault(p => p.UserCode == userNewInfo.UserCode) as User;

            if (userOldInfo == null) return Json(resultModel);
            userOldInfo.RealName = userNewInfo.RealName;
            if (!string.IsNullOrEmpty(userNewInfo.UserPwd))
            {
                userOldInfo.UserPwd = EncryptAndDecrypt.EncryptDES(userNewInfo.UserPwd, "stwhh123"); //密码加密保存
            }
            try
            {
                userOldInfo.Sex = userNewInfo.Sex;
                userOldInfo.Age = userNewInfo.Age;
                userOldInfo.Phone = userNewInfo.Phone;
                userOldInfo.QQ = userNewInfo.QQ;
                userOldInfo.Email = userNewInfo.Email;
                userOldInfo.EntryDate = userNewInfo.EntryDate;
                userOldInfo.Address = userNewInfo.Address;
                userOldInfo.PositionCode = userNewInfo.PositionCode;
                userOldInfo.DepartmentCode = userNewInfo.DepartmentCode;
                userOldInfo.ModifyUserCode = userNewInfo.UserCode; //修改用户
                userOldInfo.ModifyTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")); //修改时间

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
        /// <summary>
        /// 角色管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleManage()
        {
            return View();
        }

        /// <summary>
        /// 查询角色查询
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public JsonResult RoleManage_Query(string roleCode, string roleName, int pageindex, int pagesize)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.RoleManage_Query(roleCode, roleName, pageindex, pagesize));
        }

        /// <summary>
        /// 根据角色编号查询所有属于这个角色的人员 User_Role-页面
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <returns></returns>
        public ActionResult RoleManage_User(string roleCode)
        {
            ViewBag.RoleCode = roleCode; //给页面其它方法调用
            return View();
        }

        /// <summary>
        /// 角色组用户页面--查询
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <param name="userCode">用户编号</param>
        /// <param name="realName">真实姓名</param>
        /// <returns></returns>
        public JsonResult Role_User_Query(string roleCode, string userCode, string realName)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.Role_User_Query(roleCode, userCode, realName));
        }

        /// <summary>
        /// 添加角色组用户页面
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <returns></returns>
        public ActionResult RoleManage_User_Add(string roleCode)
        {
            ViewBag.RoleCode = roleCode;
            return View();
        }

        //添加角色组用户-保存
        public ActionResult Role_AddUser_Save(string RoleCode, string selects)
        {
            ViewBag.RoleCode = RoleCode;
            SysManageBll bll = new SysManageBll();
            return Json(bll.Role_AddUser_Save(RoleCode, selects));
        }


        /// <summary>
        /// 删除角色组用户
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <param name="selects">选择的项</param>
        /// <returns></returns>
        public ActionResult DelRole_User(string roleCode, string selects)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.DelRole_User(roleCode, selects));
        }


        /// <summary>
        /// 查询不在当前角色组的所有用户-无分页
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <param name="userCode">用户编号</param>
        /// <param name="realName">真实姓名</param>
        /// <returns></returns>
        public JsonResult Role_QueryAllUser(string roleCode, string userCode, string realName)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.Role_QueryAllUser(roleCode, userCode, realName));
        }


        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <returns></returns>
        public JsonResult RoleManage_DelRole(string roleCode)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.RoleManage_DelRole(roleCode));

        }

        /// <summary>
        /// 新增角色页面
        /// </summary>
        /// <returns></returns>
        public ActionResult RoleManage_AddRole()
        {
            return View();
        }

        /// <summary>
        /// 编辑角色
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <returns></returns>
        public ActionResult RoleManage_EditRole(string roleCode)
        {
            ViewBag.RoleCode = roleCode;

            BenqOAContext bqc = new BenqOAContext();
            var model = bqc.Roles.Where(p => p.RoleCode == roleCode).First();
            return View(model);
        }

        /// <summary>
        /// 编辑角色—保存
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <returns></returns>
        public ActionResult RoleManage_EditRole_Save(Role role)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.RoleManage_EditRole_Save(role));
        }


        /// <summary>
        /// 验证角色编号是否存在
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <returns></returns>
        public JsonResult CheckRoleCode(string roleCode)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.CheckRoleCode(roleCode), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增角色—保存
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <returns></returns>
        public ActionResult RoleManage_AddRole_Save(Role role)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.RoleManage_AddRole_Save(role));
        }

        /// <summary>
        /// 给角色赋权页面
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <returns></returns>
        public ActionResult RoleManage_AddPerm(string roleCode)
        {
            ViewBag.RoleCode = roleCode;

            return View();
        }

        /// <summary>
        /// 查询所有角色权限TreeView
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <returns></returns>
        public ActionResult RoleManage_AddPerm_All(string roleCode)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.RoleManage_AddPerm_All(roleCode), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询当前角色已存在的权限信息
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <returns></returns>
        public ActionResult RoleManage_AddPerm_ExistPerm(string roleCode)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.RoleManage_AddPerm_ExistPerm(roleCode), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 保存当前角色选择的权限
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <param name="perms">权限</param>
        /// <returns></returns>
        public ActionResult RoleManage_AddPerm_Save(string roleCode, string perms)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.RoleManage_AddPerm_Save(roleCode, perms));
        }

        #endregion


        #region 权限管理
        /// <summary>
        /// 权限管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PermManage()
        {
            return View();
        }

        /// <summary>
        /// 权限管理-查询
        /// </summary>
        /// <param name="permCode">权限编号</param>
        /// <param name="permName">权限名称</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public JsonResult PermManage_Query(string permCode, string permName, int pageindex, int pagesize)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.PermManage_Query(permCode, permName, pageindex, pagesize));
        }

        /// <summary>
        /// 权限管理-新增页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PermManage_Add()
        {
            return View();
        }

        /// <summary>
        ///  验证权限编号是否存在
        /// </summary>
        /// <param name="permCode">权限编号</param>
        /// <returns></returns>
        public JsonResult CheckPermCode(string permCode)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.CheckPermCode(permCode), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 权限管理-新增 保存
        /// </summary>
        /// <param name="permisson">权限实体</param>
        /// <returns></returns>
        public JsonResult PermManage_Add_Save(Permisson permisson)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.PermManage_Add_Save(permisson));
        }

        /// <summary>
        /// 权限管理-删除
        /// </summary>
        /// <param name="permCode">权限编号</param>
        /// <returns></returns>
        public JsonResult PermManage_Del(string permCode)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.PermManage_Query(permCode));
        }

        /// <summary>
        /// 权限管理-编辑
        /// </summary>
        /// <param name="permCode">权限编号</param>
        /// <returns></returns>
        public ActionResult PermManage_Edit(string permCode)
        {
            ViewBag.PermCode = permCode;
            SysManageBll bll = new SysManageBll();
            return View(bll.PermManage_Edit(permCode));

        }

        /// <summary>
        /// 权限管理-编辑-保存
        /// </summary>
        /// <param name="permCode">权限编号</param>
        /// <param name="perm">权限实体</param>
        /// <returns></returns>
        public JsonResult PermManage_Edit_Save(string permCode, Permisson perm)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.PermManage_Edit_Save(permCode, perm));
        }

        #endregion


        #region 部门管理
        /// <summary>
        /// 部门管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DepaManage()
        {
            return View();
        }

        /// <summary>
        /// 部门管理-查询
        /// </summary>
        /// <param name="departmentCode">部门编号</param>
        /// <param name="departmentName">部门名称</param>
        /// <param name="departmentManageCode">部门经理编号</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public JsonResult DepaManage_Query(string departmentCode, string departmentName, string departmentManageCode, int pageindex, int pagesize)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.DepaManage_Query(departmentCode, departmentName, departmentManageCode, pageindex, pagesize));
        }


        /// <summary>
        /// 验证部门编号是否存在
        /// </summary>
        /// <param name="departmentCode">部门编号</param>
        /// <returns></returns>
        public JsonResult CheckDepaCode(string departmentCode)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.CheckDepaCode(departmentCode), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 部门管理-新增
        /// </summary>
        /// <param name="department">部门实体</param>
        /// <returns></returns>
        public ActionResult DepaManage_Add(Department department)
        {
            return View();
        }


        /// <summary>
        /// 部门管理-新增-保存
        /// </summary>
        /// <param name="department">部门实体</param>
        /// <returns></returns>
        public JsonResult DepaManage_Add_Save(Department department)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.DepaManage_Add_Save(department));
        }


        //部门管理-删除
        public JsonResult DepaManage_Del(string DepartmentCode)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.DepaManage_Del(DepartmentCode));
        }


        /// <summary>
        /// 部门管理-编辑
        /// </summary>
        /// <param name="departmentCode">部门编号</param>
        /// <returns></returns>
        public ActionResult DepaManage_Edit(string departmentCode)
        {
            SysManageBll bll = new SysManageBll();
            return View(bll.DepaManage_Edit(departmentCode));
        }


        /// <summary>
        /// 部门管理-编辑-保存
        /// </summary>
        /// <param name="departmentCode">部门编号</param>
        /// <param name="departmentName">部门名称</param>
        /// <param name="departmentManageCode">部门经理编号</param>
        /// <returns></returns>
        public JsonResult DepaManage_Edit_Save(string departmentCode, string departmentName, string departmentManageCode)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.DepaManage_Edit_Save(departmentCode, departmentName, departmentManageCode));
        }
        #endregion


        #region 职位管理
        /// <summary>
        /// 职位管理
        /// </summary>
        /// <returns></returns>
        public ActionResult PosiManage()
        {
            return View();
        }

        /// <summary>
        /// 职位管理-查询
        /// </summary>
        /// <param name="positionCode">职位编号</param>
        /// <param name="positionName">职位名称</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public JsonResult PosiManage_Query(string positionCode, string positionName, int pageindex, int pagesize)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.PosiManage_Query(positionCode, positionName, pageindex, pagesize));
        }

        /// <summary>
        /// 验证职位编号是否存在
        /// </summary>
        /// <param name="positionCode">职位编号</param>
        /// <returns></returns>
        public JsonResult CheckPosiCode(string positionCode)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.CheckPosiCode(positionCode), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 职位管理-新增
        /// </summary>
        /// <returns></returns>
        public ActionResult PosiManage_Add()
        {
            return View();
        }

        /// <summary>
        /// 职位管理-新增-保存
        /// </summary>
        /// <param name="position">职位实体</param>
        /// <returns></returns>
        public JsonResult PosiManage_Add_Save(Position position)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.PosiManage_Add_Save(position));
        }

        /// <summary>
        /// 职位管理-删除
        /// </summary>
        /// <param name="positionCode">职位编号</param>
        /// <returns></returns>
        public ActionResult PosiManage_Del(string positionCode)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.PosiManage_Del(positionCode));
        }

        /// <summary>
        /// 职位管理-编辑(根据职位编号查询职位信息)
        /// </summary>
        /// <param name="positionCode"></param>
        /// <returns></returns>
        public ActionResult PosiManage_Edit(string positionCode)
        {
            ViewBag.PositionCode = positionCode;
            SysManageBll bll = new SysManageBll();
            return View(bll.PosiManage_Edit(positionCode));
        }

        /// <summary>
        /// 职位管理-编辑-保存
        /// </summary>
        /// <param name="position">职位实体</param>
        /// <returns></returns>
        public JsonResult PosiManage_Edit_Save(Position position)
        {
            SysManageBll bll = new SysManageBll();
            return Json(bll.PosiManage_Edit_Save(position));
        }

        #endregion


        /// <summary>
        /// 修改密码页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AlterPwd()
        {
            return View();
        }
    }
}
