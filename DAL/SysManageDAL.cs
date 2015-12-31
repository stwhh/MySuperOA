using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Models;

namespace DAL
{
    //系统管理
    public class SysManageDAL
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
            ResultModel<object> resultModel = new ResultModel<object>();

            using (var bqc = new BenqOAContext())
            {
                //var result = res.Users.Where(m => m.UserCode.Contains(UserCode) && m.RealName.Contains(RealName) &&
                //    m.DepartmentCode.Contains(DepartmentCode)).OrderBy(m => m.ID).Skip(pageindex * pagesize).Take(pagesize).ToList(); //所有数据
                var result = (from a in bqc.Users
                              from b in bqc.Sexes
                              where a.Sex == b.SexId
                              && a.UserCode.Contains(userCode)
                              && a.RealName.Contains(realName)
                              && a.DepartmentCode.Contains(departmentCode)
                              orderby a.ID
                              select new
                              {
                                  UserCode = a.UserCode,
                                  RealName = a.RealName,
                                  Sex = b.SexName,
                                  Age = a.Age,
                                  Phone = a.Phone,
                                  QQ=a.QQ,
                                  Email = a.Email,
                                  Address = a.Address,
                                  EntryDate = a.EntryDate
                              }).Skip(pageindex * pagesize).Take(pagesize).ToList();


                //int count = bqc.Users.Where(m => m.UserCode.Contains(UserCode) && m.RealName.Contains(RealName) &&
                //    m.DepartmentCode.Contains(DepartmentCode)).Count(); //记录数

                var count = (from a in bqc.Users
                             from b in bqc.Sexes
                             where a.Sex == b.SexId
                             && a.UserCode.Contains(userCode)
                             && a.RealName.Contains(realName)
                             && a.DepartmentCode.Contains(departmentCode)
                             select a).Count();

                resultModel.Data = result;
                resultModel.ErrorCode = "0";
                resultModel.Message = "";
                resultModel.TotalCount = count;

                return resultModel;
            }
        }


        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public bool CheckUserCode(string userCode)
        {
            bool flag = true;
            BenqOAContext bqc = new BenqOAContext();
            if (bqc.Users.Where(p => p.UserCode == userCode).FirstOrDefault() != null)
            {
                flag = false;
            }
            return flag;
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public ResultModel<object> UserManage_DelUser(string userCode)
        {

            ResultModel<object> resultModel = new ResultModel<object>();
            using (var bqc = new BenqOAContext())
            {
                var user = bqc.Users.Where(p => p.UserCode == userCode).FirstOrDefault(); //查询要删除的用户表数据
                User_Role[] urs = bqc.User_Role.Where(p => p.UserCode == userCode).ToArray() as User_Role[]; //查询要删除的用户角色表数据
                try
                {
                    bqc.Users.Remove(user); //删除
                    if (urs != null)
                    {
                        foreach (User_Role ur in urs)
                        {
                            bqc.User_Role.Remove(ur); //删除对应的用户角色表
                        }
                    }
                    bqc.SaveChanges(); //保存更改

                    resultModel.ErrorCode = "0";
                    resultModel.Message = "";
                }
                catch (Exception ee)
                {
                    resultModel.ErrorCode = "1";
                    resultModel.Message = ee.Message;
                }
                return resultModel;
            }
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
            BenqOAContext bqc = new BenqOAContext();
            List<object> list = new List<object>();
            var ur = bqc.User_Role.Where(p => p.UserCode == userCode).ToList();
            foreach (var u in ur)
            {
                list.Add(u.RoleCode);  //把一个用户对应的一个或多个角色返回
            }
            return list;
        }


        /// <summary>
        /// 登录时根据角色编号查询权限编号
        /// </summary>
        /// <param name="objs">角色编号集合</param>
        /// <returns></returns>
        public List<object> GetPermCodeByRoleCode(List<object> objs)
        {
            BenqOAContext bqc = new BenqOAContext();
            List<object> list = new List<object>();
            foreach (var obj in objs)
            {
                var rp = bqc.Role_Permisson.Where(p => p.RoleCode == obj).ToList();
                //int i = objs.Count();//角色个数=循环次数
                //var rp = bqc.Role_Permisson.Where(p => p.RoleCode == obj).ToList();

                foreach (var r in rp)
                {
                    list.Add(r.PermCode); //把权限编号放到数组里
                }
            }

            return list.Distinct().ToList(); //list.Distinct() 去重处理
        }


        /// <summary>
        /// 登录时根据权限编号查询对应的权限
        /// </summary>
        /// <param name="objs">权限编号集合</param>
        /// <returns></returns>
        public List<Permisson> GetPermInfoByRoleCode(List<object> objs)
        {
            BenqOAContext bqc = new BenqOAContext();
            List<Permisson> list = new List<Permisson>();
            for (int i = 0; i < objs.Count(); i++)  //遍历list集合用count()方法
            {
                var s1 = objs[i];
                list.Add(bqc.Permissons.Where(p => p.PermCode == s1).FirstOrDefault());
            }

            return list.OrderBy(p => p.PermSeq).ToList(); //排序后传到上一层
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
            BenqOAContext bqc = new BenqOAContext();
            var result = bqc.Roles.Where(p => p.RoleCode.Contains(roleCode) && p.RoleName.Contains(roleName))
                .OrderBy(p => p.RoleCode).Skip(pageindex * pagesize).Take(pagesize).ToList();
            ResultModel<object> resultModel = new ResultModel<object>();
            var count = bqc.Roles.Where(p => p.RoleCode.Contains(roleCode) && p.RoleName.Contains(roleName)).Count();
            try
            {
                resultModel.Data = result;
                resultModel.TotalCount = count;
                resultModel.ErrorCode = "0";
                resultModel.Message = "";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.Message;
            }
            return resultModel;
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
            //List<User> Users = new List<User>(); //查到的用户信息
            BenqOAContext bqc = new BenqOAContext();
            //根据角色编号查询属于这个角色的用户编号
            var roles = bqc.User_Role.Where(p => p.RoleCode == roleCode).ToList();
            //根据用户编号查询用户姓名
            var user = bqc.Users.SqlQuery(@"select * from [User] where UserCode in( select UserCode 
                from User_Role where RoleCode='" + roleCode + "') and UserCode like'%" + userCode + "%' and RealName like'%" + realName + "%'").ToList();

            ResultModel<List<User>> resultModel = new ResultModel<List<User>>();
            resultModel.ErrorCode = "0";
            resultModel.Data = user;
            return resultModel;
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
            ResultModel<List<User>> resultModel = new ResultModel<List<User>>();
            BenqOAContext bqc = new BenqOAContext();
            try
            {
                var result = bqc.Users.SqlQuery(@"select * from [User] where UserCode not in
                (select UserCode from User_Role where RoleCode='" + roleCode + "') and UserCode like'%" + userCode + "%' and RealName like '%" + realName + "%'").ToList();
                resultModel.Data = result;
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }

            return resultModel;
        }


        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <returns></returns>
        public ResultModel<object> RoleManage_DelRole(string roleCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            Role role = bqc.Roles.Where(p => p.RoleCode == roleCode).First() as Role;
            ResultModel<object> resultModel = new ResultModel<object>();
            try
            {
                bqc.Roles.Remove(role); //删除
                bqc.SaveChanges();
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }

            return resultModel;
        }


        /// <summary>
        /// 编辑角色—保存
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <returns></returns>
        public ResultModel<object> RoleManage_EditRole_Save(Role role)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            try
            {
                Role Role = bqc.Roles.Where(p => p.RoleCode == role.RoleCode).First() as Role;
                Role.RoleName = role.RoleName;
                Role.RoleContent = role.RoleContent;
                bqc.SaveChanges();
                resultModel.ErrorCode = "0";
            }
            catch
            {
                resultModel.ErrorCode = "1";
            }

            return resultModel;
        }


        /// <summary>
        /// 验证角色编号是否存在
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <returns></returns>
        public bool CheckRoleCode(string roleCode)
        {
            bool flag = true;
            BenqOAContext bqc = new BenqOAContext();
            if (bqc.Roles.Where(p => p.RoleCode == roleCode).FirstOrDefault() != null)
            {
                flag = false;
            }
            return flag;
        }


        /// <summary>
        /// 新增角色—保存
        /// </summary>
        /// <param name="role">角色实体</param>
        /// <returns></returns>
        public ResultModel<object> RoleManage_AddRole_Save(Role role)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            try
            {
                Role Role = new Role();
                Role.RoleCode = role.RoleCode;
                Role.RoleName = role.RoleName;
                Role.RoleContent = role.RoleContent;

                bqc.Roles.Add(Role);
                bqc.SaveChanges();
                resultModel.ErrorCode = "0";
            }
            catch
            {
                resultModel.ErrorCode = "1";
            }

            return resultModel;
        }


        /// <summary>
        /// 添加角色组用户
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <param name="selects">选中项</param>
        /// <returns></returns>
        public ResultModel<object> Role_AddUser_Save(string roleCode, string selects)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            string[] selectArray = selects.Split(',');
            try
            {
                User_Role ur = new User_Role();
                foreach (var select in selectArray)
                {
                    ur.RoleCode = roleCode;
                    ur.UserCode = select;
                    bqc.User_Role.Add(ur);
                    bqc.SaveChanges();
                }

                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }

            return resultModel;
        }


        /// <summary>
        /// 删除角色组用户
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <param name="selects">选中的项</param>
        /// <returns></returns>
        public ResultModel<object> DelRole_User(string roleCode, string selects)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            string[] selectArray = selects.Split(',');
            try
            {
                foreach (var select in selectArray)
                {
                    User_Role user = bqc.User_Role.Where(p => p.UserCode == select && p.RoleCode == roleCode).First() as User_Role;
                    bqc.User_Role.Remove(user);
                    bqc.SaveChanges();
                }

                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }

            return resultModel;
        }


        /// <summary>
        /// 角色赋权--加载所有权限信息
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <returns></returns>
        public ResultModel<List<object>> RoleManage_AddPerm_All(string roleCode)
        {
            BenqOAContext bqc = new BenqOAContext(); //上下文类
            ResultModel<List<object>> resultModel = new ResultModel<List<object>>(); //返回值resultModel.Data是list<object>类型
            List<object> listPerm = new List<object>(); //集合，存放权限信息
            Permisson[] lists = bqc.Permissons.ToArray() as Permisson[]; //权限列表

            resultModel.ErrorCode = "1"; //给个默认值为1,表示查不到数据
            foreach (var item in lists)
            {
                if (item.ParaentCode == "root")
                {
                    //foreach(var sub in lists)
                    //{
                    //    if(sub.ParaentCode==list.PermCode)
                    //    {
                    //        listperm.Add(new { PermCode = sub.PermCode, PermName=sub.PermName ,});
                    //    }
                    //}
                    var items = GetTree(item, lists);
                    listPerm.Add(new { PermCode = item.PermCode, PermName = item.PermName, items = items });
                }
                resultModel.ErrorCode = "0";
            }

            resultModel.Data = listPerm;
            return resultModel;
        }

        /// <summary>
        /// 递归-查询根节点下的子节点
        /// </summary>
        /// <param name="perm">权限实体</param>
        /// <param name="perms">权限实体集合</param>
        /// <returns></returns>
        public List<object> GetTree(Permisson perm, Permisson[] perms)
        {
            List<object> liststre = new List<object>();
            foreach (var list in perms)
            {
                if (perm.PermCode == list.ParaentCode)
                {
                    var items = GetTree(list, perms);
                    liststre.Add(new { PermCode = list.PermCode, PermName = list.PermName, items = items });
                }
            }
            return liststre;
        }


        /// <summary>
        /// 查询当前角色已存在的权限信息
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <returns></returns>
        public string RoleManage_AddPerm_ExistPerm(string roleCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            string existsPerm = string.Empty;
            var perms = bqc.Role_Permisson.SqlQuery(@"select *
                        from Role_Permisson a
                        inner join Permisson b
                        on a.PermCode=b.PermCode
                        and a.RoleCode='" + roleCode + "'").ToArray();

            foreach (var perm in perms)
            {
                existsPerm += perm.PermCode + ',';
            }

            return existsPerm;
        }


        /// <summary>
        /// 保存当前角色选择的权限
        /// </summary>
        /// <param name="roleCode">角色编号</param>
        /// <param name="perms">权限</param>
        /// <returns></returns>
        public ResultModel<object> RoleManage_AddPerm_Save(string roleCode, string perms)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();
            string[] lists = perms.Split(',');  //要添加的所有权限编号 +

            var dellist = RoleManage_AddPerm_ExistPerm(roleCode); //当前角色数据库中存在的权限 
            string[] dellists = dellist.Split(',');

            try
            {
                Role_Permisson rp = new Role_Permisson();
                foreach (var list in lists)
                {
                    //1.先查询当前选中的权限数据中是否存在，若存在，则无需保存；
                    var addlist = bqc.Role_Permisson.Where(p => p.RoleCode == roleCode && p.PermCode == list).FirstOrDefault();

                    if (addlist != null) //权限已存在
                    {
                        continue; //权限存在，跳出本次循环，继续下次循环
                    }
                    else
                    {
                        //1.增加权限
                        rp.RoleCode = roleCode;
                        rp.PermCode = list;
                        bqc.Role_Permisson.Add(rp); //权限不存在，添加
                        bqc.SaveChanges();
                    }

                }

                //2.删除被取消的权限
                if (lists != dellists)
                {
                    var excepts = dellists.Except(lists).ToList(); //前台没选择，后台数据中之前存在的数据--删除
                    foreach (var except in excepts)
                    {
                        var r = bqc.Role_Permisson.Where(p => p.RoleCode == roleCode && p.PermCode == except).FirstOrDefault();
                        if (r != null)
                        {
                            bqc.Role_Permisson.Remove(r); //权限被删除
                            bqc.SaveChanges();
                        }
                    }
                }

                resultModel.ErrorCode = "0";

            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "0";
                resultModel.Message = ee.ToString();
            }

            return resultModel;
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
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();
            try
            {
                var perm = bqc.Permissons.Where(p => p.PermCode.Contains(permCode) && p.PermName.Contains(permName))
                    .OrderBy(p => p.PermSeq).Skip(pageindex * pagesize).Take(pagesize).ToList();
                var count = bqc.Permissons.Where(p => p.PermCode.Contains(permCode) && p.PermName.Contains(permName)).Count();

                resultModel.Data = perm;
                resultModel.TotalCount = count;
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = ee.ToString();
            }
            return resultModel;
        }


        /// <summary>
        /// 验证权限编号是否存在
        /// </summary>
        /// <param name="permCode">权限编号</param>
        /// <returns></returns>
        public bool CheckPermCode(string permCode)
        {
            bool flag = true;
            BenqOAContext bqc = new BenqOAContext();
            if (bqc.Permissons.Where(p => p.PermCode == permCode).FirstOrDefault() != null)
            {
                flag = false;
            }
            return flag;
        }


        /// <summary>
        /// 权限管理-新增 保存
        /// </summary>
        /// <param name="permisson">权限实体</param>
        /// <returns></returns>
        public ResultModel<object> PermManage_Add_Save(Permisson permisson)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            try
            {
                bqc.Permissons.Add(permisson);
                bqc.SaveChanges();
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }
            return resultModel;
        }


        /// <summary>
        /// 权限管理-删除
        /// </summary>
        /// <param name="permCode">权限编号</param>
        /// <returns></returns>
        public ResultModel<object> PermManage_Query(string permCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();
            try
            {
                var perm = bqc.Permissons.Where(p => p.PermCode == permCode).First();
                bqc.Permissons.Remove(perm);
                bqc.SaveChanges();
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "0";
                resultModel.Message = ee.ToString();
            }
            return resultModel;
        }


        /// <summary>
        /// 权限管理-编辑
        /// </summary>
        /// <param name="permCode">权限编号</param>
        /// <returns></returns>
        public Permisson PermManage_Edit(string permCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();
            Permisson list = bqc.Permissons.Where(p => p.PermCode == permCode).First() as Permisson;

            return list;
        }


        /// <summary>
        /// 权限管理-编辑-保存
        /// </summary>
        /// <param name="permCode">权限编号</param>
        /// <param name="perm">权限实体</param>
        /// <returns></returns>
        public ResultModel<object> PermManage_Edit_Save(string permCode, Permisson perm)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            try
            {
                Permisson list = bqc.Permissons.Where(p => p.PermCode == permCode).First() as Permisson;
                list.PermName = perm.PermName;
                list.PermUrl = perm.PermUrl;
                list.PermSeq = perm.PermSeq;
                list.PermType = perm.PermType;
                list.ParaentCode = perm.ParaentCode;

                bqc.SaveChanges();
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }

            return resultModel;
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
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();
            try
            {
                var list = bqc.Departments.Where(p => p.DepartmentCode.Contains(departmentCode) && p.DepartmentName.Contains(departmentName)
                    &&p.DepartmentManageCode.Contains(departmentManageCode)).OrderBy(p => p.DepartmentCode).Skip(pageindex * pagesize).Take(pagesize).ToList();
                var count = bqc.Departments.Where(p => p.DepartmentCode.Contains(departmentCode) && p.DepartmentName.Contains(departmentName)
                    && p.DepartmentManageCode.Contains(departmentManageCode)).Count();
                resultModel.Data = list;
                resultModel.TotalCount = count;
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }
            return resultModel;
        }


        /// <summary>
        /// 验证部门编号是否存在
        /// </summary>
        /// <param name="departmentCode">部门编号</param>
        /// <returns></returns>
        public bool CheckDepaCode(string departmentCode)
        {
            bool flag = true;
            BenqOAContext bqc = new BenqOAContext();
            if (bqc.Departments.Where(p => p.DepartmentCode == departmentCode).FirstOrDefault() != null)
            {
                flag = false;
            }
            return flag;
        }


        /// <summary>
        /// 部门管理-新增
        /// </summary>
        /// <param name="department">部门实体</param>
        /// <returns></returns>
        public ResultModel<object> DepaManage_Add_Save(Department department)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            try
            {
                bqc.Departments.Add(department);
                bqc.SaveChanges();
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }
            return resultModel;
        }


        /// <summary>
        /// 部门管理-删除
        /// </summary>
        /// <param name="departmentCode">部门编号</param>
        /// <returns></returns>
        public ResultModel<object> DepaManage_Del(string departmentCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();
            try
            {
                var perm = bqc.Departments.Where(p => p.DepartmentCode == departmentCode).First();
                bqc.Departments.Remove(perm);
                bqc.SaveChanges();
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "0";
                resultModel.Message = ee.ToString();
            }
            return resultModel;
        }


        /// <summary>
        /// 部门管理-编辑
        /// </summary>
        /// <param name="departmentCode">部门编号</param>
        /// <returns></returns>
        public Department DepaManage_Edit(string departmentCode)
        {
            Department department = null;
            BenqOAContext bqc = new BenqOAContext();
            department = bqc.Departments.Where(p => p.DepartmentCode == departmentCode).FirstOrDefault();

            return department;
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
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            try
            {
                Department department = bqc.Departments.Where(p => p.DepartmentCode == departmentCode).First();
                department.DepartmentName = departmentName;
                department.DepartmentManageCode = departmentManageCode;
                bqc.SaveChanges();
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }

            return resultModel;
        }
        #endregion


        #region 职位管理
        ///<summary>
        /// 职位管理-查询
        /// </summary>
        /// <param name="positionCode">职位编号</param>
        /// <param name="positionName">职位名称</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public ResultModel<object> PosiManage_Query(string positionCode, string positionName, int pageindex, int pagesize)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();
            try
            {
                var list = bqc.Positions.Where(p => p.PositionCode.Contains(positionCode) && p.PositionName.Contains(positionName))
                    .OrderBy(p => p.PositionCode).Skip(pageindex * pagesize).Take(pagesize).ToList();
                var count = bqc.Positions.Where(p => p.PositionCode.Contains(positionCode) && p.PositionName.Contains(positionName)).Count();
                resultModel.Data = list;
                resultModel.TotalCount = count;
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }
            return resultModel;
        }


        /// <summary>
        /// 验证职位编号是否存在
        /// </summary>
        /// <param name="positionCode">职位编号</param>
        /// <returns></returns>
        public bool CheckPosiCode(string positionCode)
        {
            bool flag = true;
            BenqOAContext bqc = new BenqOAContext();
            if (bqc.Positions.Where(p => p.PositionCode == positionCode).FirstOrDefault() != null)
            {
                flag = false;
            }
            return flag;
        }


        /// <summary>
        /// 职位管理-新增-保存
        /// </summary>
        /// <param name="position">职位实体</param>
        /// <returns></returns>
        public ResultModel<object> PosiManage_Add_Save(Position position)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            try
            {
                bqc.Positions.Add(position);
                bqc.SaveChanges();
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }
            return resultModel;
        }


        /// <summary>
        /// 职位管理-删除
        /// </summary>
        /// <param name="positionCode">职位编号</param>
        /// <returns></returns>
        public ResultModel<object> PosiManage_Del(string positionCode)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();

            try
            {
                var list = bqc.Positions.Where(p => p.PositionCode == positionCode).FirstOrDefault();
                bqc.Positions.Remove(list);
                bqc.SaveChanges();
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "0";
                resultModel.Message = ee.ToString();
            }

            return resultModel;
        }


        /// <summary>
        /// 职位管理-编辑(根据职位编号查询职位信息)
        /// </summary>
        /// <param name="positionCode">职位编号</param>
        /// <returns></returns>
        public Position PosiManage_Edit(string positionCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            Position model = bqc.Positions.Where(p => p.PositionCode == positionCode).First() as Position;

            return model;
        }


        /// <summary>
        /// 职位管理-编辑-保存
        /// </summary>
        /// <param name="position">职位实体</param>
        /// <returns></returns>
        public ResultModel<object> PosiManage_Edit_Save(Position position)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            try
            {
                Position list = bqc.Positions.Where(p => p.PositionCode == position.PositionCode).First() as Position;
                list.PositionName = position.PositionName;

                bqc.SaveChanges();
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Message = ee.ToString();
            }
            return resultModel;
        }

        #endregion
    }
}
