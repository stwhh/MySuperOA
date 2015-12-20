using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Model.Models;
using System.Data;

namespace DAL
{
    //公告管理
    public class AnnoManageDAL
    {
        #region 发布公告
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public ResultModel<object> PublishAnno_Query(string AnnounceTypeId, string AnnounceTitle, string BeginApplyDate, string EndApplyDate, int pageindex, int pagesize)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            //基于方法的Liqn查询=>lambda表达式
            //var list = bqc.Announces.OrderBy(p=>p.ID).Skip(pageindex*pagesize).Take(pagesize).ToList(); 

            DateTime bd = Convert.ToDateTime(BeginApplyDate);
            DateTime ed = Convert.ToDateTime(EndApplyDate).AddHours(23.9); //加23.9小时 默认是0时，加了以后才好跟前台日期比较

            //Linq to Entity
            var list = (from a in bqc.Announces
                       from b in bqc.AnnounceTypes
                       where a.AnnounceTypeId == b.AnnounceTypeId
                       && a.AnnounceTypeId.Contains(AnnounceTypeId) //公告编号应该是=，但是用contains可以防止为空的情况
                       && a.AnnounceTitle.Contains(AnnounceTitle)
                       && a.CreateTime.CompareTo(bd) >= 0
                       && a.CreateTime.CompareTo(ed) <= 0
                       orderby a.CreateTime descending
                       select new
                       {
                           AnnounceCode = a.AnnounceCode,
                           AnnounceTypeName = b.AnnounceTypeName,
                           AnnounceTitle = a.AnnounceTitle,
                           AnnounceContent = a.AnnounceContent,
                           CreateTime = a.CreateTime,
                           CreateUserCode = a.CreateUserCode,
                           Status = a.Status
                       }).Skip(pageindex*pagesize).Take(pagesize);

            var count = (from a in bqc.Announces
                         from b in bqc.AnnounceTypes
                         where a.AnnounceTypeId == b.AnnounceTypeId
                         && a.AnnounceTypeId.Contains(AnnounceTypeId) //公告编号应该是=，但是用contains可以防止为空的情况
                         && a.AnnounceTitle.Contains(AnnounceTitle)
                         && a.CreateTime.CompareTo(bd) >= 0
                         && a.CreateTime.CompareTo(ed) <= 0
                         select a).Count();

            ;
            try
            {
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
        /// 新增-保存
        /// </summary>
        /// <param name="anno"></param>
        /// <param name="AnnounceTitle"></param>
        /// <param name="AnnounceTypeId"></param>
        /// <param name="AnnounceContent"></param>
        /// <returns></returns>
        public ResultModel<object> PublishAnno_Add_Save(Announce Anno)
        {
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();

            try
            {
                Anno.CreateTime = DateTime.Now;
                Anno.Status = "未发布";
                bqc.Announces.Add(Anno);
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
        /// 删除
        /// </summary>
        /// <param name="Anno"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public ResultModel<object> PublishAnno_Del(string selectitems)
        {
            string[] selects = selectitems.Split(',');
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();
            try
            {
                foreach (var select in selects)
                {
                    Announce list = bqc.Announces.Where(p => p.AnnounceCode == select).First();
                    bqc.Announces.Remove(list);
                }
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
        /// 查看公告详细信息
        /// </summary>
        /// <param name="AnnounceCode"></param>
        /// <returns></returns>
        public Announce PublishAnno_Detail(string AnnounceCode)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            Announce model = bqc.Announces.Where(p => p.AnnounceCode == AnnounceCode).First();

            return model;
        }


        /// <summary>
        /// 编辑-保存
        /// </summary>
        /// <param name="UserCode"></param>
        /// <param name="AnnounceCode"></param>
        /// <returns></returns>
        public ResultModel<object> PublishAnno_Edit_Save(Announce Anno) //Anno是新对象
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            var status = bqc.Announces.Where(p => p.AnnounceCode == Anno.AnnounceCode).Select(p => p.Status).First();

            try
            {
                //var list = bqc.Announces.Where(p => p.AnnounceCode == Anno.AnnounceCode).First();
                Announce anno = bqc.Announces.Where(p => p.AnnounceCode == Anno.AnnounceCode).First(); //原来的对象
                anno.AnnounceTitle = Anno.AnnounceTitle;
                anno.AnnounceTypeId = Anno.AnnounceTypeId;
                anno.AnnounceContent = Anno.AnnounceContent;
                anno.Status = status;

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
        /// 发布
        /// </summary>
        /// <param name="Anno"></param>
        /// <param name="UserCode"></param>
        /// <returns></returns>
        public ResultModel<object> PublishAnno_Ok(string selectitems)
        {
            string[] selects = selectitems.Split(',');
            BenqOAContext bqc = new BenqOAContext();
            ResultModel<object> resultModel = new ResultModel<object>();
            try
            {
                foreach (var select in selects)
                {
                    Announce list = bqc.Announces.Where(p => p.AnnounceCode == select).First();
                    list.Status = "已发布";
                }
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
