using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Models;

namespace DAL
{
    public class ProjManageDAL
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
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            try
            {
                var result = bqc.Projects.Where(p => p.ProjCode.Contains(projCode) && p.ProjName.Contains(projName))
                .OrderByDescending(p => p.CreateTime).Skip(pageindex * pagesize).Take(pagesize).ToList();

                var count = bqc.Projects.Where(p => p.ProjCode.Contains(projCode) && p.ProjName.Contains(projName))
                .OrderBy(p => p.ID).Count();

                resultModel.Data = result;
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
        /// 新增项目-保存
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <param name="project">项目实体</param>
        /// <returns></returns>
        public ResultModel<object> ProjDiscuss_Add_Save(string userCode, Project project)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            try
            {
                project.CreateUserCode = userCode;
                project.CreateTime = DateTime.Now;

                bqc.Projects.Add(project);
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
        /// 删除项目
        /// </summary>
        /// <param name="userCode">用户</param>
        /// <param name="projCode">项目编号</param>
        /// <returns></returns>
        public ResultModel<object> ProjDiscuss_Del(string userCode, string projCode)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            try
            {
                var project = bqc.Projects.Where(p => p.ProjCode == projCode).First();
                bqc.Projects.Remove(project);
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
        /// 编辑项目-保存
        /// </summary>
        /// <param name="project">项目实体</param>
        /// <returns></returns>
        public ResultModel<object> ProjDiscuss_Edit_Save(Project project)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            try
            {
                Project projectModel = bqc.Projects.Where(p => p.ProjCode == project.ProjCode).First();
                projectModel.ProjName = project.ProjName;
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
        /// 项目讨论页面
        /// </summary>
        /// <param name="projCode">项目编号</param>
        /// <returns></returns>
        public List<Project_Discuss> ProjDiscuss_Comment(string projCode)
        {
            BenqOAContext bqc = new BenqOAContext();
            List<Project_Discuss> model = bqc.Project_Discuss.Where(p => p.ProjCode == projCode).ToList();
            return model;
        }


        /// <summary>
        /// 发送讨论信息
        /// </summary>
        /// <param name="comment">评论</param>
        /// <param name="userCode">用户编号</param>
        /// <param name="projCode">项目编号</param>
        /// <returns></returns>
        public ResultModel<object> ProjDiscuss_sendComment(string projCode, string userCode, string comment)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> reusltModel = new ResultModel<object>();
            Project_Discuss pro = new Project_Discuss();
            try
            {
                pro.ProjCode = projCode;
                pro.ProjComConent = comment;
                pro.CreateTime = DateTime.Now;
                pro.CreateUserCode = userCode;

                //如果该用户从未发表过评论，随机分配头像；否则用之前的头像
                var list = bqc.Project_Discuss.Where(p => p.CreateUserCode == userCode).FirstOrDefault();
                if (list == null)
                {
                    Random ran = new Random();
                    int i = ran.Next(1, 8); //一共7张头像
                    pro.CreateUserImage = "/Content/images/users/user" + i + ".png";
                }
                else
                {
                    pro.CreateUserImage = list.CreateUserImage;
                }

                bqc.Project_Discuss.Add(pro);
                bqc.SaveChanges();
                reusltModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                reusltModel.ErrorCode = "1";
                reusltModel.Message = ee.ToString();
            }

            return reusltModel;
        }


        /// <summary>
        /// 判断是否刷新 
        /// </summary>
        /// <returns></returns>
        static BenqOAContext bqc = new BenqOAContext();
        static int initCount =bqc.Project_Discuss.Count(); //初始化时数据库中的消息记录条数
        public bool ProjDiscuss_ifRefresh()
        {
            int initCount2 = bqc.Project_Discuss.Count();
            if (initCount2 > initCount)
            {
                initCount = initCount2; //把新的记录数赋给initCount变量
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
