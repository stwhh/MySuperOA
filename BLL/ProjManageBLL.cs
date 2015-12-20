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
        /// 查询项目
        /// </summary>
        /// <param name="ProjCode"></param>
        /// <param name="ProjName"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ResultModel<object> ProjDiscuss_Query(string ProjCode, string ProjName, int pageindex, int pagesize)
        {
            ProjManageDAL dal = new ProjManageDAL();
            return dal.ProjDiscuss_Query(ProjCode, ProjName, pageindex, pagesize);
        }


        /// <summary>
        /// 新增项目-保存
        /// </summary>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public ResultModel<object> ProjDiscuss_Add_Save(string UserCode, Project Project)
        {
            ProjManageDAL dal = new ProjManageDAL();
            return dal.ProjDiscuss_Add_Save(UserCode, Project);
        }


        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="ProjCode"></param>
        /// <returns></returns>
        public ResultModel<object> ProjDiscuss_Del(string UserCode, string ProjCode)
        {
            ProjManageDAL dal = new ProjManageDAL();
            return dal.ProjDiscuss_Del(UserCode, ProjCode);
        }


        /// <summary>
        /// 编辑项目-保存
        /// </summary>
        /// <param name="ProjCode"></param>
        /// <returns></returns>
        public ResultModel<object> ProjDiscuss_Edit_Save(Project project)
        {
            ProjManageDAL dal = new ProjManageDAL();
            return dal.ProjDiscuss_Edit_Save(project);
        }


        /// <summary>
        /// 项目讨论页面
        /// </summary>
        /// <param name="ProjCode"></param>
        /// <returns></returns>
        public List<Project_Discuss> ProjDiscuss_Comment(string ProjCode)
        {
            ProjManageDAL dal = new ProjManageDAL();
            return dal.ProjDiscuss_Comment(ProjCode);
        }


        /// <summary>
        /// 发送讨论信息
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        public ResultModel<object> ProjDiscuss_sendComment(string ProjCode, string UserCode, string Comment)
        {
            ProjManageDAL dal = new ProjManageDAL();
            return dal.ProjDiscuss_sendComment(ProjCode, UserCode, Comment);
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
