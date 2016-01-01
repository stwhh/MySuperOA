using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using Model.Models;
using BenqOA.Helper;
using Public;

namespace BenqOA.Controllers
{
    [MyAuthorFilter(Roles = MyAuthorFilter.LoginRole)]
    public class AnnoManageController : Controller
    {
        #region 公告管理

        /// <summary>
        /// 发布公告页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PublishAnno()
        {
            User user = Session["userInfo"] as User;
            if (user != null) ViewBag.UserCode = user.UserCode;

            return View();
        }

        /// <summary>
        /// 查询公告
        /// </summary>
        /// <param name="announceTypeId">公告类型id</param>
        /// <param name="announceTitle">公告标题</param>
        /// <param name="beginApplyDate">发布开始时间</param>
        /// <param name="endApplyDate">发布结束时间</param>
        /// <param name="pageindex">页索引</param>
        /// <param name="pagesize">页大小</param>
        /// <returns></returns>
        public ActionResult PublishAnno_Query(string announceTypeId,string announceTitle,string beginApplyDate,string endApplyDate,int pageindex, int pagesize)
        {
            AnnoManageBLL bll = new AnnoManageBLL();
            return Json(bll.PublishAnno_Query(announceTypeId, announceTitle, beginApplyDate, endApplyDate, pageindex, pagesize));
        }

        /// <summary>
        /// 新增页面
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public ActionResult PublishAnno_Add(string userCode)
        {
            if (!string.IsNullOrWhiteSpace(userCode))
            {
                ViewBag.UserCode = userCode;
            }

            return View();
        }

        /// <summary>
        /// 新增公告-保存
        /// </summary>
        /// <param name="anno">公告实体</param>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)] //设置传数据时不验证格式
        public JsonResult PublishAnno_Add_Save(Announce anno, string userCode)
        {
            //自动生成发布编号
            anno.AnnounceCode = GeneRandomNum.GetRanNum("A");
            anno.CreateUserCode = userCode;
            AnnoManageBLL bll = new AnnoManageBLL();

            return Json(bll.PublishAnno_Add_Save(anno));
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        /// <param name="selectItems">选择的项</param>
        /// <returns></returns>
        public JsonResult PublishAnno_Del(string selectItems)
        {
            AnnoManageBLL bll = new AnnoManageBLL();
            return Json(bll.PublishAnno_Del(selectItems));
        }

        /// <summary>
        /// 详细信息/编辑-页面
        /// </summary>
        /// <param name="announceCode">公告编号</param>
        /// <param name="pageAction">类型（查看，编辑）</param>
        /// <returns></returns>
        public ActionResult PublishAnno_Detail(string announceCode, string pageAction)
        {
            AnnoManageBLL bll = new AnnoManageBLL();
            ViewBag.pageAction = pageAction;
            ViewBag.announceCode = announceCode;

            var model = bll.PublishAnno_Detail(announceCode);
            ViewBag.AnnounceContent = model.AnnounceContent;

            return View(model);
        }


        /// <summary>
        /// 编辑-保存
        /// </summary>
        /// <param name="announceCode">公告编号</param>
        /// <param name="anno">公告实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)] //设置传数据时不验证格式
        public JsonResult PublishAnno_Edit_Save(string announceCode, Announce anno)
        {
            //Anno.ModifyUserCode = UserCode; //修改者
            anno.AnnounceCode = announceCode;

            AnnoManageBLL bll = new AnnoManageBLL();
            return Json(bll.PublishAnno_Edit_Save(anno));
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="selectItems">选择的项</param>
        /// <returns></returns>
        public JsonResult PublishAnno_Ok(string selectItems)
        {
            AnnoManageBLL bll = new AnnoManageBLL();
            return Json(bll.PublishAnno_Ok(selectItems));
        }

       
        #endregion
    }
}
