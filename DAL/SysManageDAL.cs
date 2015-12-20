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
        /// <param name="UserCode"></param>
        /// <param name="RealName"></param>
        /// <param name="DepartmentCode"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ResultModel<object> UserManage_QueryUser(string UserCode, string RealName, string DepartmentCode, int pageindex, int pagesize)
        {
            ResultModel<object> resultModel = new ResultModel<object>();

            using (var bqc = new BenqOAContext())
            {
                //var result = res.Users.Where(m => m.UserCode.Contains(UserCode) && m.RealName.Contains(RealName) &&
                //    m.DepartmentCode.Contains(DepartmentCode)).OrderBy(m => m.ID).Skip(pageindex * pagesize).Take(pagesize).ToList(); //所有数据
                var result = (from a in bqc.Users
                              from b in bqc.Sexes
                              where a.Sex == b.SexId
                              && a.UserCode.Contains(UserCode)
                              && a.RealName.Contains(RealName)
                              && a.DepartmentCode.Contains(DepartmentCode)
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
                             && a.UserCode.Contains(UserCode)
                             && a.RealName.Contains(RealName)
                             && a.DepartmentCode.Contains(DepartmentCode)
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
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public bool CheckUserCode(string UserCode)
        {
            bool flag = true;
            BenqOAContext bqc = new BenqOAContext();
            if (bqc.Users.Where(p => p.UserCode == UserCode).FirstOrDefault() != null)
            {
                flag = false;
            }
            return flag;
        }


        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userCode"></param>
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
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public List<object> GetUserRoleByUserCode(string UserCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            List<object> list = new List<object>();
            var ur = bqc.User_Role.Where(p => p.UserCode == UserCode).ToList();
            foreach (var u in ur)
            {
                list.Add(u.RoleCode);  //把一个用户对应的一个或多个角色返回
            }
            return list;
        }


        /// <summary>
        /// 登录时根据角色编号查询权限编号
        /// </summary>
        /// <param name="objs"></param>
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
        /// <param name="objs"></param>
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
        /// 查询角色
        /// </summary>
        /// <param name="RoleCode"></param>
        /// <param name="RoleName"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ResultModel<object> RoleManage_Query(string RoleCode, string RoleName, int pageindex, int pagesize)
        {
            BenqOAContext bqc = new BenqOAContext();
            var result = bqc.Roles.Where(p => p.RoleCode.Contains(RoleCode) && p.RoleName.Contains(RoleName))
                .OrderBy(p => p.RoleCode).Skip(pageindex * pagesize).Take(pagesize).ToList();
            ResultModel<object> resultModel = new ResultModel<object>();
            var count = bqc.Roles.Where(p => p.RoleCode.Contains(RoleCode) && p.RoleName.Contains(RoleName)).Count();
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
        /// <param name="RoleCode">角色编号</param>
        /// <param name="UserCode">用户编号</param>
        /// <param name="RealName">真实姓名</param>
        /// <returns></returns>
        public ResultModel<List<User>> Role_User_Query(string RoleCode, string UserCode, string RealName)
        {
            //List<User> Users = new List<User>(); //查到的用户信息
            BenqOAContext bqc = new BenqOAContext();
            //根据角色编号查询属于这个角色的用户编号
            var roles = bqc.User_Role.Where(p => p.RoleCode == RoleCode).ToList();
            //根据用户编号查询用户姓名
            var User = bqc.Users.SqlQuery(@"select * from [User] where UserCode in( select UserCode 
                from User_Role where RoleCode='" + RoleCode + "') and UserCode like'%" + UserCode + "%' and RealName like'%" + RealName + "%'").ToList();

            ResultModel<List<User>> resultModel = new ResultModel<List<User>>();
            resultModel.ErrorCode = "0";
            resultModel.Data = User;
            return resultModel;
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
            ResultModel<List<User>> resultModel = new ResultModel<List<User>>();
            BenqOAContext bqc = new BenqOAContext();
            try
            {
                var result = bqc.Users.SqlQuery(@"select * from [User] where UserCode not in
                (select UserCode from User_Role where RoleCode='" + RoleCode + "') and UserCode like'%" + UserCode + "%' and RealName like '%" + RealName + "%'").ToList();
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
        /// <param name="RoleCode"></param>
        /// <returns></returns>
        public ResultModel<object> RoleManage_DelRole(string RoleCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            Role role = bqc.Roles.Where(p => p.RoleCode == RoleCode).First() as Role;
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
        /// <param name="role"></param>
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
        /// <param name="RoleCode"></param>
        /// <returns></returns>
        public bool CheckRoleCode(string RoleCode)
        {
            bool flag = true;
            BenqOAContext bqc = new BenqOAContext();
            if (bqc.Roles.Where(p => p.RoleCode == RoleCode).FirstOrDefault() != null)
            {
                flag = false;
            }
            return flag;
        }


        /// <summary>
        /// 新增角色—保存
        /// </summary>
        /// <param name="role"></param>
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
        /// <param name="RoleCode"></param>
        /// <returns></returns>
        public ResultModel<object> Role_AddUser_Save(string RoleCode, string selects)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            string[] Selects = selects.Split(',');
            try
            {
                User_Role ur = new User_Role();
                foreach (var select in Selects)
                {
                    ur.RoleCode = RoleCode;
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
        /// <param name="role"></param>
        /// <returns></returns>
        public ResultModel<object> DelRole_User(string RoleCode, string selects)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            string[] Selects = selects.Split(',');
            try
            {
                foreach (var select in Selects)
                {
                    User_Role user = bqc.User_Role.Where(p => p.UserCode == select && p.RoleCode == RoleCode).First() as User_Role;
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
        /// <param name="RoleCode">此参数没用</param>
        /// <returns></returns>
        public ResultModel<List<object>> RoleManage_AddPerm_All(string RoleCode)
        {
            BenqOAContext bqc = new BenqOAContext(); //上下文类
            ResultModel<List<object>> resultModel = new ResultModel<List<object>>(); //返回值resultModel.Data是list<object>类型
            List<object> listperm = new List<object>(); //集合，存放权限信息
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
                    listperm.Add(new { PermCode = item.PermCode, PermName = item.PermName, items = items });
                }
                resultModel.ErrorCode = "0";
            }

            resultModel.Data = listperm;
            return resultModel;
        }

        //递归-查询根节点下的子节点
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
        /// <param name="RoleCode"></param>
        /// <returns></returns>
        public string RoleManage_AddPerm_ExistPerm(string RoleCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            string existsperm = string.Empty;
            var perms = bqc.Role_Permisson.SqlQuery(@"select *
                        from Role_Permisson a
                        inner join Permisson b
                        on a.PermCode=b.PermCode
                        and a.RoleCode='" + RoleCode + "'").ToArray();

            foreach (var perm in perms)
            {
                existsperm += perm.PermCode + ',';
            }

            return existsperm;
        }


        /// <summary>
        /// 保存当前角色选择的权限
        /// </summary>
        /// <param name="RoleCode"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public ResultModel<object> RoleManage_AddPerm_Save(string RoleCode, string perms)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();
            string[] lists = perms.Split(',');  //要添加的所有权限编号 +

            var dellist = RoleManage_AddPerm_ExistPerm(RoleCode); //当前角色数据库中存在的权限 
            string[] dellists = dellist.Split(',');

            try
            {
                Role_Permisson rp = new Role_Permisson();
                foreach (var list in lists)
                {
                    //1.先查询当前选中的权限数据中是否存在，若存在，则无需保存；
                    var addlist = bqc.Role_Permisson.Where(p => p.RoleCode == RoleCode && p.PermCode == list).FirstOrDefault();

                    if (addlist != null) //权限已存在
                    {
                        continue; //权限存在，跳出本次循环，继续下次循环
                    }
                    else
                    {
                        //1.增加权限
                        rp.RoleCode = RoleCode;
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
                        var r = bqc.Role_Permisson.Where(p => p.RoleCode == RoleCode && p.PermCode == except).FirstOrDefault();
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
        /// <param name="PermCode"></param>
        /// <param name="PermName"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ResultModel<object> PermManage_Query(string PermCode, string PermName, int pageindex, int pagesize)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();
            try
            {
                var perm = bqc.Permissons.Where(p => p.PermCode.Contains(PermCode) && p.PermName.Contains(PermName))
                    .OrderBy(p => p.PermSeq).Skip(pageindex * pagesize).Take(pagesize).ToList();
                var count = bqc.Permissons.Where(p => p.PermCode.Contains(PermCode) && p.PermName.Contains(PermName)).Count();

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
        /// <param name="PermCode"></param>
        /// <returns></returns>
        public bool CheckPermCode(string PermCode)
        {
            bool flag = true;
            BenqOAContext bqc = new BenqOAContext();
            if (bqc.Permissons.Where(p => p.PermCode == PermCode).FirstOrDefault() != null)
            {
                flag = false;
            }
            return flag;
        }


        /// <summary>
        /// 权限管理-新增 保存
        /// </summary>
        /// <param name="PermCode"></param>
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
        /// <param name="PermCode"></param>
        /// <returns></returns>
        public ResultModel<object> PermManage_Query(string PermCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();
            try
            {
                var perm = bqc.Permissons.Where(p => p.PermCode == PermCode).First();
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
        /// <param name="PermCode"></param>
        /// <returns></returns>
        public Permisson PermManage_Edit(string PermCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();
            Permisson list = bqc.Permissons.Where(p => p.PermCode == PermCode).First() as Permisson;

            return list;
        }


        /// <summary>
        /// 权限管理-编辑-保存
        /// </summary>
        /// <param name="PermCode"></param>
        /// <returns></returns>
        public ResultModel<object> PermManage_Edit_Save(string PermCode, Permisson perm)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            try
            {
                Permisson list = bqc.Permissons.Where(p => p.PermCode == PermCode).First() as Permisson;
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
        /// <returns></returns>
        public ResultModel<object> DepaManage_Query(string DepartmentCode, string DepartmentName,string DepartmentManageCode, int pageindex, int pagesize)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();
            try
            {
                var list = bqc.Departments.Where(p => p.DepartmentCode.Contains(DepartmentCode) && p.DepartmentName.Contains(DepartmentName)
                    &&p.DepartmentManageCode.Contains(DepartmentManageCode)).OrderBy(p => p.DepartmentCode).Skip(pageindex * pagesize).Take(pagesize).ToList();
                var count = bqc.Departments.Where(p => p.DepartmentCode.Contains(DepartmentCode) && p.DepartmentName.Contains(DepartmentName)
                    && p.DepartmentManageCode.Contains(DepartmentManageCode)).Count();
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
        /// <param name="DepaCode"></param>
        /// <returns></returns>
        public bool CheckDepaCode(string DepartmentCode)
        {
            bool flag = true;
            BenqOAContext bqc = new BenqOAContext();
            if (bqc.Departments.Where(p => p.DepartmentCode == DepartmentCode).FirstOrDefault() != null)
            {
                flag = false;
            }
            return flag;
        }


        /// <summary>
        /// 部门管理-新增
        /// </summary>
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
        /// <param name="DepartmentCode"></param>
        /// <returns></returns>
        public ResultModel<object> DepaManage_Del(string DepartmentCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();
            try
            {
                var perm = bqc.Departments.Where(p => p.DepartmentCode == DepartmentCode).First();
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
        /// <param name="DepartmentCode"></param>
        /// <returns></returns>
        public Department DepaManage_Edit(string DepartmentCode)
        {
            Department department = null;
            BenqOAContext bqc = new BenqOAContext();
            department = bqc.Departments.Where(p => p.DepartmentCode == DepartmentCode).FirstOrDefault();

            return department;
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
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            try
            {
                Department department = bqc.Departments.Where(p => p.DepartmentCode == DepartmentCode).First();
                department.DepartmentName = DepartmentName;
                department.DepartmentManageCode = DepartmentManageCode;
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
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();
            try
            {
                var list = bqc.Positions.Where(p => p.PositionCode.Contains(PositionCode) && p.PositionName.Contains(PositionName))
                    .OrderBy(p => p.PositionCode).Skip(pageindex * pagesize).Take(pagesize).ToList();
                var count = bqc.Positions.Where(p => p.PositionCode.Contains(PositionCode) && p.PositionName.Contains(PositionName)).Count();
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
        /// <param name="PositionCode"></param>
        /// <returns></returns>
        public bool CheckPosiCode(string PositionCode)
        {
            bool flag = true;
            BenqOAContext bqc = new BenqOAContext();
            if (bqc.Positions.Where(p => p.PositionCode == PositionCode).FirstOrDefault() != null)
            {
                flag = false;
            }
            return flag;
        }


        /// <summary>
        /// 职位管理-新增-保存
        /// </summary>
        /// <param name="PositionCode"></param>
        /// <param name="PositionName"></param>
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
        /// <param name="PositionCode"></param>
        /// <returns></returns>
        public ResultModel<object> PosiManage_Del(string PositionCode)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();

            try
            {
                var list = bqc.Positions.Where(p => p.PositionCode == PositionCode).FirstOrDefault();
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
        /// <param name="PositionCode"></param>
        /// <returns></returns>
        public Position PosiManage_Edit(string PositionCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            Position model = bqc.Positions.Where(p => p.PositionCode == PositionCode).First() as Position;

            return model;
        }


        /// <summary>
        /// 职位管理-编辑-保存
        /// </summary>
        /// <param name="PositionCode"></param>
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
