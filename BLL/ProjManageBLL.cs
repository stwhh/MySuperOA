using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
using Model.Models;

namespace BLL
{
    public class ProjManageBLL
    {
        #region 项目讨论
        /// <summary>
        /// 项目查询
        /// </summary>
        /// <param name="projCode">项目编号</param>
        /// <param name="projName">项目名称</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public ResultModel<object> ProjDiscuss_Query(string projCode, string projName, int pageindex, int pagesize)
        {
            ProjManageDAL dal = new ProjManageDAL();
            return dal.ProjDiscuss_Query(projCode, projName, pageindex, pagesize);
        }


        /// <summary>
        /// 新增项目-保存
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <param name="project">项目实体</param>
        /// <returns></returns>
        public ResultModel<object> ProjDiscuss_Add_Save(string userCode, Project project)
        {
            ProjManageDAL dal = new ProjManageDAL();
            return dal.ProjDiscuss_Add_Save(userCode, project);
        }


        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <param name="projCode">项目编号</param>
        /// <returns></returns>
        public ResultModel<object> ProjDiscuss_Del(string userCode, string projCode)
        {
            ProjManageDAL dal = new ProjManageDAL();
            return dal.ProjDiscuss_Del(userCode, projCode);
        }


        /// <summary>
        /// 编辑项目-保存
        /// </summary>
        /// <param name="project">项目实体</param>
        /// <returns></returns>
        public ResultModel<object> ProjDiscuss_Edit_Save(Project project)
        {
            ProjManageDAL dal = new ProjManageDAL();
            return dal.ProjDiscuss_Edit_Save(project);
        }


        /// <summary>
        /// 项目讨论页面
        /// </summary>
        /// <param name="projCode">项目编号</param>
        /// <returns></returns>
        public List<Project_Discuss> ProjDiscuss_Comment(string projCode)
        {
            ProjManageDAL dal = new ProjManageDAL();
            return dal.ProjDiscuss_Comment(projCode);
        }


        /// <summary>
        /// 发送讨论信息
        /// </summary>
        /// <param name="projCode">项目编号</param>
        /// <param name="userCode">用户编号</param>
        /// <param name="comment">评论</param>
        /// <returns></returns>
        public ResultModel<object> ProjDiscuss_sendComment(string projCode, string userCode, string comment)
        {
            ProjManageDAL dal = new ProjManageDAL();
            return dal.ProjDiscuss_sendComment(projCode, userCode, comment);
        }

        /// <summary>
        /// 判断是否刷新 
        /// </summary>
        /// <returns></returns>
        public bool ProjDiscuss_ifRefresh()
        {
            ProjManageDAL dal = new ProjManageDAL();
            return dal.ProjDiscuss_ifRefresh();
        }
        #endregion
    }
}
