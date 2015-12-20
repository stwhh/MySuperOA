using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
using Model.Models;

namespace BLL
{
    //公告管理
    public class AnnoManageBLL
    {
        #region 发布公告
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public ResultModel<object> PublishAnno_Query(string AnnounceTypeId, string AnnounceTitle, string BeginApplyDate, string EndApplyDate, int pageindex, int pagesize)
        {
            AnnoManageDAL dal = new AnnoManageDAL();
            return dal.PublishAnno_Query(AnnounceTypeId, AnnounceTitle, BeginApplyDate, EndApplyDate,pageindex, pagesize);
        }


        /// <summary>
        /// 新增-保存
        /// </summary>
        /// <param name="anno"></param>
        /// <param name="AnnounceTitle"></param>
        /// <param name="AnnounceTypeId"></param>
        /// <param name="AnnounceContent"></param>
        /// <returns></returns>
        public ResultModel<object> PublishAnno_Add_Save(Announce Anno)
        {
            AnnoManageDAL dal = new AnnoManageDAL();
            return dal.PublishAnno_Add_Save(Anno);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Anno"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public ResultModel<object> PublishAnno_Del(string selectitems)
        {
            AnnoManageDAL dal = new AnnoManageDAL();
            return dal.PublishAnno_Del(selectitems);
        }


        /// <summary>
        /// 查看公告详细信息
        /// </summary>
        /// <param name="AnnounceCode"></param>
        /// <returns></returns>
        public Announce PublishAnno_Detail(string AnnounceCode)
        {
            AnnoManageDAL dal = new AnnoManageDAL();
            return dal.PublishAnno_Detail(AnnounceCode);
        }


        /// <summary>
        /// 编辑-保存
        /// </summary>
        /// <param name="UserCode"></param>
        /// <param name="AnnounceCode"></param>
        /// <returns></returns>
        public ResultModel<object> PublishAnno_Edit_Save(Announce Anno)
        {

            AnnoManageDAL dal = new AnnoManageDAL();
            return dal.PublishAnno_Edit_Save(Anno);
        }


        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="Anno"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public ResultModel<object> PublishAnno_Ok(string selectitems)
        {
            AnnoManageDAL dal = new AnnoManageDAL();
            return dal.PublishAnno_Ok(selectitems);
        }
        #endregion
    }
}
