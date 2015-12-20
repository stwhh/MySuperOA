using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Models;
using DAL;

namespace BLL
{
    //系统管理
    public class SysManageBLL
    {
        #region 用户管理
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="UserCode"></param>
        /// <param name="RealName"></param>
        /// <param name="DepartmentCode"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ResultModel<object> UserManage_QueryUser(string UserCode, string RealName, string DepartmentCode, int pageindex, int pagesize)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.UserManage_QueryUser(UserCode, RealName, DepartmentCode, pageindex, pagesize);
        }


        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public bool CheckUserCode(string UserCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.CheckUserCode(UserCode);
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public ResultModel<object> UserManage_DelUser(string userCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.UserManage_DelUser(userCode);
        }
        #endregion


        #region 角色管理
        /// <summary>
        /// 登录时根据用户编号查询角色编号
        /// </summary>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public List<object> GetUserRoleByUserCode(string UserCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.GetUserRoleByUserCode(UserCode);
        }


        /// <summary>
        /// 登录时根据角色编号查询权限编号
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public List<object> GetPermCodeByRoleCode(List<object> objs)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.GetPermCodeByRoleCode(objs);
        }


        /// <summary>
        /// 登录时根据权限编号查询对应的权限
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public List<Permisson> GetPermInfoByRoleCode(List<object> objs)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.GetPermInfoByRoleCode(objs);
        }


        /// <summary>
        /// 查询角色
        /// </summary>
        /// <param name="RoleCode"></param>
        /// <param name="RoleName"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ResultModel<object> RoleManage_Query(string RoleCode, string RoleName, int pageindex, int pagesize)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.RoleManage_Query(RoleCode, RoleName, pageindex, pagesize);
        }

        /// <summary>
        /// 角色组用户页面--查询
        /// </summary>
        /// <param name="RoleCode">角色编号</param>
        /// <param name="UserCode">用户编号</param>
        /// <param name="RealName">真实姓名</param>
        /// <returns></returns>
        public ResultModel<List<User>> Role_User_Query(string RoleCode, string UserCode, string RealName)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.Role_User_Query(RoleCode, UserCode, RealName);
        }


        /// <summary>
        /// 查询不在当前角色组的所有用户--无分页
        /// </summary>
        /// <param name="RoleCode">角色编号</param>
        /// <param name="UserCode">用户编号</param>
        /// <param name="RealName">真实姓名</param>
        /// <returns></returns>
        public ResultModel<List<User>> Role_QueryAllUser(string RoleCode, string UserCode, string RealName)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.Role_QueryAllUser(RoleCode, UserCode, RealName);
        }


        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="RoleCode"></param>
        /// <returns></returns>
        public ResultModel<object> RoleManage_DelRole(string RoleCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.RoleManage_DelRole(RoleCode);
        }


        /// <summary>
        /// 编辑角色—保存
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public ResultModel<object> RoleManage_EditRole_Save(Role role)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.RoleManage_EditRole_Save(role);
        }



        /// <summary>
        /// 验证角色编号是否存在
        /// </summary>
        /// <param name="RoleCode"></param>
        /// <returns></returns>
        public bool CheckRoleCode(string RoleCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.CheckRoleCode(RoleCode);
        }


        /// <summary>
        /// 新增角色—保存
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public ResultModel<object> RoleManage_AddRole_Save(Role role)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.RoleManage_AddRole_Save(role);
        }


        /// <summary>
        /// 添加角色组用户
        /// </summary>
        /// <param name="RoleCode"></param>
        /// <returns></returns>
        public ResultModel<object> Role_AddUser_Save(string RoleCode, string selects)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.Role_AddUser_Save(RoleCode, selects);
        }


        /// <summary>
        /// 删除角色组用户
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public ResultModel<object> DelRole_User(string RoleCode, string selects)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.DelRole_User(RoleCode, selects);
        }


        /// <summary>
        /// 角色赋权--加载所有权限信息
        /// </summary>
        /// <returns></returns>
        public ResultModel<List<object>> RoleManage_AddPerm_All(string RoleCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.RoleManage_AddPerm_All(RoleCode);
        }


        /// <summary>
        /// 查询当前角色已存在的权限信息
        /// </summary>
        /// <param name="RoleCode"></param>
        /// <returns></returns>
        public string RoleManage_AddPerm_ExistPerm(string RoleCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.RoleManage_AddPerm_ExistPerm(RoleCode);
        }


        /// <summary>
        /// 保存当前角色选择的权限
        /// </summary>
        /// <param name="RoleCode"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ResultModel<object> RoleManage_AddPerm_Save(string RoleCode, string perms)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.RoleManage_AddPerm_Save(RoleCode, perms);
        }
        #endregion


        #region 权限管理
        /// <summary>
        /// 权限管理-查询
        /// </summary>
        /// <param name="PermCode"></param>
        /// <param name="PermName"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ResultModel<object> PermManage_Query(string PermCode, string PermName, int pageindex, int pagesize)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PermManage_Query(PermCode, PermName, pageindex, pagesize);
        }


        /// <summary>
        /// 验证权限编号是否存在
        /// </summary>
        /// <param name="PermCode"></param>
        /// <returns></returns>
        public bool CheckPermCode(string PermCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.CheckPermCode(PermCode);
        }


        /// <summary>
        /// 权限管理-新增 保存
        /// </summary>
        /// <param name="PermCode"></param>
        /// <returns></returns>
        public ResultModel<object> PermManage_Add_Save(Permisson permisson)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PermManage_Add_Save(permisson);
        }


        /// <summary>
        /// 权限管理-删除
        /// </summary>
        /// <param name="PermCode"></param>
        /// <returns></returns>
        public ResultModel<object> PermManage_Query(string PermCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PermManage_Query(PermCode);
        }


        /// <summary>
        /// 权限管理-编辑
        /// </summary>
        /// <param name="PermCode"></param>
        /// <returns></returns>
        public Permisson PermManage_Edit(string PermCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PermManage_Edit(PermCode);
        }


        /// <summary>
        /// 权限管理-编辑-保存
        /// </summary>
        /// <param name="PermCode"></param>
        /// <returns></returns>
        public ResultModel<object> PermManage_Edit_Save(string PermCode, Permisson perm)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PermManage_Edit_Save(PermCode, perm);
        }
        #endregion


        #region 部门管理
        /// <summary>
        /// 部门管理-查询
        /// </summary>
        /// <param name="DepartmentCode"></param>
        /// <param name="DepartmentName"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ResultModel<object> DepaManage_Query(string DepartmentCode, string DepartmentName,string DepartmentManageCode, int pageindex, int pagesize)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.DepaManage_Query(DepartmentCode, DepartmentName,DepartmentManageCode, pageindex, pagesize);
        }


        /// <summary>
        /// 验证部门编号是否存在
        /// </summary>
        /// <param name="DepaCode"></param>
        /// <returns></returns>
        public bool CheckDepaCode(string DepartmentCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.CheckDepaCode(DepartmentCode);
        }


        /// <summary>
        /// 部门管理-新增-保存
        /// </summary>
        /// <returns></returns>
        public ResultModel<object> DepaManage_Add_Save(Department department)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.DepaManage_Add_Save(department);
        }


        /// <summary>
        /// 部门管理-删除
        /// </summary>
        /// <param name="DepartmentCode"></param>
        /// <returns></returns>
        public ResultModel<object> DepaManage_Del(string DepartmentCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.DepaManage_Del(DepartmentCode);
        }


        /// <summary>
        /// 部门管理-编辑
        /// </summary>
        /// <param name="DepartmentCode"></param>
        /// <returns></returns>
        public Department DepaManage_Edit(string DepartmentCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.DepaManage_Edit(DepartmentCode);
        }


        /// <summary>
        /// 部门管理-编辑-保存
        /// </summary>
        /// <param name="DepartmentCode"></param>
        /// <param name="DepartmentName"></param>
        /// <param name="DepartmentManageCode"></param>
        /// <returns></returns>
        public ResultModel<object> DepaManage_Edit_Save(string DepartmentCode, string DepartmentName, string DepartmentManageCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.DepaManage_Edit_Save(DepartmentCode,DepartmentName,DepartmentManageCode);
        }
        #endregion


        #region 职位管理
        /// <summary>
        /// 职位管理-查询
        /// </summary>
        /// <param name="PositionCode"></param>
        /// <param name="PositionName"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ResultModel<object> PosiManage_Query(string PositionCode, string PositionName, int pageindex, int pagesize)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PosiManage_Query(PositionCode, PositionName, pageindex, pagesize);
        }


        /// <summary>
        /// 验证职位编号是否存在
        /// </summary>
        /// <param name="PositionCode"></param>
        /// <returns></returns>
        public bool CheckPosiCode(string PositionCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.CheckPosiCode(PositionCode);
        }


        /// <summary>
        /// 职位管理-新增-保存
        /// </summary>
        /// <param name="PositionCode"></param>
        /// <param name="PositionName"></param>
        /// <returns></returns>
        public ResultModel<object> PosiManage_Add_Save(Position position)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PosiManage_Add_Save(position);
        }


        /// <summary>
        /// 职位管理-删除
        /// </summary>
        /// <param name="PositionCode"></param>
        /// <returns></returns>
        public ResultModel<object> PosiManage_Del(string PositionCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PosiManage_Del(PositionCode);
        }


        /// <summary>
        /// 职位管理-编辑(根据职位编号查询职位信息)
        /// </summary>
        /// <param name="PositionCode"></param>
        /// <returns></returns>
        public Position PosiManage_Edit(string PositionCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PosiManage_Edit(PositionCode);
        }


        /// <summary>
        /// 职位管理-编辑-保存
        /// </summary>
        /// <param name="PositionCode"></param>
        /// <returns></returns>
        public ResultModel<object> PosiManage_Edit_Save(Position position)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PosiManage_Edit_Save(position);
        }
        #endregion
    }
}
