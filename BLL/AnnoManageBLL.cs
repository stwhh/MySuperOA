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
        /// <param name="announceTypeId">公告类型id</param>
        /// <param name="announceTitle">公告标题</param>
        /// <param name="beginApplyDate">开始日期</param>
        /// <param name="endApplyDate">结束日期</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public ResultModel<object> PublishAnno_Query(string announceTypeId, string announceTitle, string beginApplyDate, string endApplyDate, int pageindex, int pagesize)
        {
            AnnoManageDAL dal = new AnnoManageDAL();
            return dal.PublishAnno_Query(announceTypeId, announceTitle, beginApplyDate, endApplyDate,pageindex, pagesize);
        }


        /// <summary>
        /// 新增-保存
        /// </summary>
        /// <param name="anno">公告实体</param>
        /// <returns></returns>
        public ResultModel<object> PublishAnno_Add_Save(Announce anno)
        {
            AnnoManageDAL dal = new AnnoManageDAL();
            return dal.PublishAnno_Add_Save(anno);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="selectItems">选中的项</param>
        /// <returns></returns>
        public ResultModel<object> PublishAnno_Del(string selectItems)
        {
            AnnoManageDAL dal = new AnnoManageDAL();
            return dal.PublishAnno_Del(selectItems);
        }


        /// <summary>
        /// 查看公告详细信息
        /// </summary>
        /// <param name="announceCode">公告编号</param>
        /// <returns></returns>
        public Announce PublishAnno_Detail(string announceCode)
        {
            AnnoManageDAL dal = new AnnoManageDAL();
            return dal.PublishAnno_Detail(announceCode);
        }


        /// <summary>
        /// 编辑-保存
        /// </summary>
        /// <param name="anno">公告实体</param>
        /// <returns></returns>
        public ResultModel<object> PublishAnno_Edit_Save(Announce anno)
        {

            AnnoManageDAL dal = new AnnoManageDAL();
            return dal.PublishAnno_Edit_Save(anno);
        }


        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="selectItems">要发布的项</param>
        /// <returns></returns>
        public ResultModel<object> PublishAnno_Ok(string selectItems)
        {
            AnnoManageDAL dal = new AnnoManageDAL();
            return dal.PublishAnno_Ok(selectItems);
        }
        #endregion
    }
}
