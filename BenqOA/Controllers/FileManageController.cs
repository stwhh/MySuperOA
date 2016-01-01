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
    public class FileManageController : Controller
    {

        /// <summary>
        /// 共享文件页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SharedFile()
        {
            User user = Session["userInfo"] as User;
            if (user != null) ViewBag.UserCode = user.UserCode;

            return View();
        }

        /// <summary>
        /// 查询共享文件
        /// </summary>
        /// <param name="fileType">文件类型</param>
        /// <param name="fileName">文件名</param>
        /// <param name="createUserCode">创建人编号</param>
        /// <param name="beginApplyDate">开始申请日期</param>
        /// <param name="endApplyDate">结束申请日期</param>
        /// <param name="pageindex">页面索引</param>
        /// <param name="pagesize">每页记录数</param>
        /// <returns></returns>
        public ActionResult FileManage_Query(string fileType, string fileName, string createUserCode, string beginApplyDate, string endApplyDate, int pageindex, int pagesize)
        {
            FileManageBLL bll = new FileManageBLL();
            return Json(bll.FileManage_Query(fileType, fileName, createUserCode, beginApplyDate, endApplyDate, pageindex, pagesize));

        }

        /// <summary>
        /// 上传文件界面
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <returns></returns>
        public ActionResult UploadFiles(string userCode)
        {
            ViewBag.UserCode = userCode;
            //获取上传文件最大值
            ViewBag.UploadFilesMaxFileSize = System.Configuration.ConfigurationManager.AppSettings["UploadFilesMaxFileSize"];
            return View();
        }

        /// <summary>
        /// 保存上次文件
        /// </summary>
        /// <param name="userCode">用户编号</param>
        /// <param name="fileType">文件类型</param>
        /// <returns></returns>
        public JsonResult UploadFiles_Save(string userCode, string fileType)
        {
            var resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            //string path = Server.MapPath("~/Uploads"); //服务器上虚拟路径

            try
            {
                //1.获取客户端上传的文件集合
                foreach (string requestFile in Request.Files)
                {
                    //提供对已上传的文件的访问
                    HttpPostedFileBase uploadFile = Request.Files[requestFile] as HttpPostedFileBase;
                    
                    //2.创建路径
                    //string filePath = file.FilePath;
                    string filePath = System.Configuration.ConfigurationManager.AppSettings["UploadFilesPath"];
                    if(!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }

                    //保存对应的记录到数据库中
                    File file = new File();
                    file.FileCode = GeneRandomNum.GetRanNum("F");
                    file.FileName = uploadFile.FileName;
                    //获取文件大小，保留两位小树ContentLength是int型
                    file.FileSize = Math.Round((Convert.ToDouble(uploadFile.ContentLength) / (1024 * 1024)), 2).ToString() +"MB";
                    file.FileType = "FT001";
                    //file.FilePath = System.IO.Path.Combine(filePath, uploadFile.FileName); //保存文件的绝对路径
                    file.FilePath = filePath;
                    file.CreateUserCode = userCode;
                    file.CreateTime = DateTime.Now;

                    bqc.Files.Add(file);
                    bqc.SaveChanges();

                    //3.保存文件到物理路径上
                    uploadFile.SaveAs(System.IO.Path.Combine(filePath,uploadFile.FileName));
                }
                resultModel.ErrorCode = "0";
            }
            catch (Exception ee)
            {
                resultModel.ErrorCode = "1";
                resultModel.Data = ee.ToString();
            }

            return Json(resultModel);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileCode">文件编号</param>
        /// <returns></returns>
        public JsonResult FileManage_Del(string fileCode)
        {
            FileManageBLL bll = new FileManageBLL();
            return Json(bll.FileManage_Del(fileCode));
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileCode">文件编号</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public FileStreamResult DownloadFile(string fileCode,string fileName)
        {
            //根据文件编号获取文件路径
            BenqOAContext bqc = new BenqOAContext();
            var filePath = bqc.Files.Where(p => p.FileCode == fileCode).Select(p => p.FilePath).First();

            //1.根据文件路径和名称获取文件绝对路径的方法
            string absolutePathName = System.IO.Path.Combine(filePath, fileName);
            //2.保存 application/octet-stream .*（ 二进制流，不知道下载文件类型）
            return File(new System.IO.FileStream(absolutePathName, System.IO.FileMode.Open), "application/octet-stream", fileName); //Server.UrlEncode(FileName)
        }

    }
}
