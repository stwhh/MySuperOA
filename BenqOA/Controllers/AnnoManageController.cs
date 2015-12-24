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
        // GET: /AnnoManage/
        #region 公告管理

        //发布公告页面
        public ActionResult PublishAnno()
        {
            User user = Session["userInfo"] as User;
            ViewBag.UserCode = user.UserCode;

            return View();
        }

        //查询
        public ActionResult PublishAnno_Query(string AnnounceTypeId,string AnnounceTitle,string BeginApplyDate,string EndApplyDate,int pageindex, int pagesize)
        {
            AnnoManageBLL bll = new AnnoManageBLL();
            return Json(bll.PublishAnno_Query(AnnounceTypeId, AnnounceTitle, BeginApplyDate, EndApplyDate, pageindex, pagesize));
        }

        //新增页面
        public ActionResult PublishAnno_Add(string UserCode)
        {
            ViewBag.UserCode = UserCode;

            return View();
        }

        //新增-保存
        [HttpPost]
        [ValidateInput(false)] //设置传数据时不验证格式
        public JsonResult PublishAnno_Add_Save(Announce Anno, string UserCode)
        {
            //自动生成发布编号
            Anno.AnnounceCode = GeneRandomNum.GetRanNum("A");
            Anno.CreateUserCode = UserCode;
            AnnoManageBLL bll = new AnnoManageBLL();

            return Json(bll.PublishAnno_Add_Save(Anno));
        }

        //删除
        public JsonResult PublishAnno_Del(string selectitems)
        {
            AnnoManageBLL bll = new AnnoManageBLL();
            return Json(bll.PublishAnno_Del(selectitems));
        }

        //详细信息/编辑-页面
        public ActionResult PublishAnno_Detail(string AnnounceCode, string pageaction)
        {
            AnnoManageBLL bll = new AnnoManageBLL();
            ViewBag.pageaction = pageaction;
            ViewBag.AnnounceCode = AnnounceCode;

            var model = bll.PublishAnno_Detail(AnnounceCode);
            ViewBag.AnnounceContent = model.AnnounceContent;

            return View(model);
        }


        //编辑-保存
        [HttpPost]
        [ValidateInput(false)] //设置传数据时不验证格式
        public JsonResult PublishAnno_Edit_Save(string AnnounceCode, Announce Anno)
        {
            //Anno.ModifyUserCode = UserCode; //修改者
            Anno.AnnounceCode = AnnounceCode;

            AnnoManageBLL bll = new AnnoManageBLL();
            return Json(bll.PublishAnno_Edit_Save(Anno));
        }

        //发布
        public JsonResult PublishAnno_Ok(string selectitems)
        {
            AnnoManageBLL bll = new AnnoManageBLL();
            return Json(bll.PublishAnno_Ok(selectitems));
        }

       
        #endregion
    }
}
