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
    public class SysManageBll
    {
        #region 用户管理
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="departmentCode">部门编号</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public ResultModel<object> UserManage_QueryUser(string userCode, string realName, string departmentCode, int pageindex, int pagesize)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.UserManage_QueryUser(userCode, realName, departmentCode, pageindex, pagesize);
        }


        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public bool CheckUserCode(string userCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.CheckUserCode(userCode);
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userCode">用户编号</param>
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
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public List<object> GetUserRoleByUserCode(string userCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.GetUserRoleByUserCode(userCode);
        }


        /// <summary>
        /// 登录时根据角色编号查询权限编号
        /// </summary>
        /// <param name="objs">角色集合</param>
        /// <returns></returns>
        public List<object> GetPermCodeByRoleCode(List<object> objs)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.GetPermCodeByRoleCode(objs);
        }


        /// <summary>
        /// 登录时根据权限编号查询对应的权限
        /// </summary>
        /// <param name="objs">权限编号</param>
        /// <returns></returns>
        public List<Permisson> GetPermInfoByRoleCode(List<object> objs)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.GetPermInfoByRoleCode(objs);
        }


        /// <summary>
        /// 查询角色查询
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <param name="roleName">角色名称</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public ResultModel<object> RoleManage_Query(string roleCode, string roleName, int pageindex, int pagesize)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.RoleManage_Query(roleCode, roleName, pageindex, pagesize);
        }

        /// <summary>
        /// 角色组用户页面--查询
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <param name="userCode">用户编号</param>
        /// <param name="realName">真实姓名</param>
        /// <returns></returns>
        public ResultModel<List<User>> Role_User_Query(string roleCode, string userCode, string realName)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.Role_User_Query(roleCode, userCode, realName);
        }


        /// <summary>
        /// 查询不在当前角色组的所有用户--无分页
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <param name="userCode">用户编号</param>
        /// <param name="realName">真实姓名</param>
        /// <returns></returns>
        public ResultModel<List<User>> Role_QueryAllUser(string roleCode, string userCode, string realName)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.Role_QueryAllUser(roleCode, userCode, realName);
        }


        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <returns></returns>
        public ResultModel<object> RoleManage_DelRole(string roleCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.RoleManage_DelRole(roleCode);
        }


        /// <summary>
        /// 编辑角色—保存
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <returns></returns>
        public ResultModel<object> RoleManage_EditRole_Save(Role role)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.RoleManage_EditRole_Save(role);
        }



        /// <summary>
        /// 验证角色编号是否存在
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <returns></returns>
        public bool CheckRoleCode(string roleCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.CheckRoleCode(roleCode);
        }


        /// <summary>
        /// 新增角色—保存
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <returns></returns>
        public ResultModel<object> RoleManage_AddRole_Save(Role role)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.RoleManage_AddRole_Save(role);
        }


        /// <summary>
        /// 添加角色组用户
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <param name="selects">选中的项</param>
        /// <returns></returns>
        public ResultModel<object> Role_AddUser_Save(string roleCode, string selects)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.Role_AddUser_Save(roleCode, selects);
        }


        /// <summary>
        /// 删除角色组用户
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <param name="selects">选中的项</param>
        /// <returns></returns>
        public ResultModel<object> DelRole_User(string roleCode, string selects)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.DelRole_User(roleCode, selects);
        }


        /// <summary>
        /// 角色赋权--加载所有权限信息
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <returns></returns>
        public ResultModel<List<object>> RoleManage_AddPerm_All(string roleCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.RoleManage_AddPerm_All(roleCode);
        }


        /// <summary>
        /// 查询当前角色已存在的权限信息
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <returns></returns>
        public string RoleManage_AddPerm_ExistPerm(string roleCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.RoleManage_AddPerm_ExistPerm(roleCode);
        }


        /// <summary>
        /// 保存当前角色选择的权限
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <param name="perms">权限</param>
        /// <returns></returns>
        public ResultModel<object> RoleManage_AddPerm_Save(string roleCode, string perms)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.RoleManage_AddPerm_Save(roleCode, perms);
        }
        #endregion


        #region 权限管理
        /// <summary>
        /// 权限管理-查询
        /// </summary>
        /// <param name="permCode">权限编号</param>
        /// <param name="permName">权限名称</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public ResultModel<object> PermManage_Query(string permCode, string permName, int pageindex, int pagesize)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PermManage_Query(permCode, permName, pageindex, pagesize);
        }


        /// <summary>
        /// 验证权限编号是否存在
        /// </summary>
        /// <param name="permCode">权限编号</param>
        /// <returns></returns>
        public bool CheckPermCode(string permCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.CheckPermCode(permCode);
        }


        /// <summary>
        /// 权限管理-新增 保存
        /// </summary>
        /// <param name="permisson">权限实体</param>
        /// <returns></returns>
        public ResultModel<object> PermManage_Add_Save(Permisson permisson)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PermManage_Add_Save(permisson);
        }


        /// <summary>
        /// 权限管理-删除
        /// </summary>
        /// <param name="permCode">权限编号</param>
        /// <returns></returns>
        public ResultModel<object> PermManage_Query(string permCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PermManage_Query(permCode);
        }


        /// <summary>
        /// 权限管理-编辑
        /// </summary>
        /// <param name="permCode">权限编号</param>
        /// <returns></returns>
        public Permisson PermManage_Edit(string permCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PermManage_Edit(permCode);
        }


        /// <summary>
        /// 权限管理-编辑-保存
        /// </summary>
        /// <param name="permCode">权限编号</param>
        /// <param name="perm">权限实体</param>
        /// <returns></returns>
        public ResultModel<object> PermManage_Edit_Save(string permCode, Permisson perm)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PermManage_Edit_Save(permCode, perm);
        }
        #endregion


        #region 部门管理

        /// <summary>
        /// 部门管理-查询
        /// </summary>
        /// <param name="departmentCode">部门编号</param>
        /// <param name="departmentName">部门名称</param>
        /// <param name="departmentManageCode">部门经理编号</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public ResultModel<object> DepaManage_Query(string departmentCode, string departmentName,string departmentManageCode, int pageindex, int pagesize)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.DepaManage_Query(departmentCode, departmentName,departmentManageCode, pageindex, pagesize);
        }


        /// <summary>
        /// 验证部门编号是否存在
        /// </summary>
        /// <param name="departmentCode">部门编号</param>
        /// <returns></returns>
        public bool CheckDepaCode(string departmentCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.CheckDepaCode(departmentCode);
        }


        /// <summary>
        /// 部门管理-新增-保存
        /// </summary>
        /// <param name="department">部门实体</param>
        /// <returns></returns>
        public ResultModel<object> DepaManage_Add_Save(Department department)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.DepaManage_Add_Save(department);
        }


        /// <summary>
        /// 部门管理-删除
        /// </summary>
        /// <param name="departmentCode">部门编号</param>
        /// <returns></returns>
        public ResultModel<object> DepaManage_Del(string departmentCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.DepaManage_Del(departmentCode);
        }


        /// <summary>
        /// 部门管理-编辑
        /// </summary>
        /// <param name="departmentCode">部门编号</param>
        /// <returns></returns>
        public Department DepaManage_Edit(string departmentCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.DepaManage_Edit(departmentCode);
        }


        /// <summary>
        /// 部门管理-编辑-保存
        /// </summary>
        /// <param name="departmentCode">部门编号</param>
        /// <param name="departmentName">部门名称</param>
        /// <param name="departmentManageCode">部门经理编号</param>
        /// <returns></returns>
        public ResultModel<object> DepaManage_Edit_Save(string departmentCode, string departmentName, string departmentManageCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.DepaManage_Edit_Save(departmentCode,departmentName,departmentManageCode);
        }
        #endregion


        #region 职位管理
        /// <summary>
        /// 职位管理-查询
        /// </summary>
        /// <param name="positionCode">职位编号</param>
        /// <param name="positionName">职位名称</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public ResultModel<object> PosiManage_Query(string positionCode, string positionName, int pageindex, int pagesize)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PosiManage_Query(positionCode, positionName, pageindex, pagesize);
        }


        /// <summary>
        /// 验证职位编号是否存在
        /// </summary>
        /// <param name="positionCode">职位编号</param>
        /// <returns></returns>
        public bool CheckPosiCode(string positionCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.CheckPosiCode(positionCode);
        }


        /// <summary>
        /// 职位管理-新增-保存
        /// </summary>
        /// <param name="position">职位实体</param>
        /// <returns></returns>
        public ResultModel<object> PosiManage_Add_Save(Position position)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PosiManage_Add_Save(position);
        }


        /// <summary>
        /// 职位管理-删除
        /// </summary>
        /// <param name="positionCode">职位编号</param>
        /// <returns></returns>
        public ResultModel<object> PosiManage_Del(string positionCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PosiManage_Del(positionCode);
        }


        /// <summary>
        /// 职位管理-编辑(根据职位编号查询职位信息)
        /// </summary>
        /// <param name="positionCode">职位编号</param>
        /// <returns></returns>
        public Position PosiManage_Edit(string positionCode)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PosiManage_Edit(positionCode);
        }


        /// <summary>
        /// 职位管理-编辑-保存
        /// </summary>
        /// <param name="position">职位实体</param>
        /// <returns></returns>
        public ResultModel<object> PosiManage_Edit_Save(Position position)
        {
            SysManageDAL dal = new SysManageDAL();
            return dal.PosiManage_Edit_Save(position);
        }
        #endregion
    }
}
