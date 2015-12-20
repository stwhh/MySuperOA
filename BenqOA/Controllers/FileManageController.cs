using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Model;
using Model.Models;
using BenqOA.Helper;

namespace BenqOA.Controllers
{
    public class FileManageController : BaseController
    {
        //
        // GET: /File/

        //public ActionResult FileManage()
        //{
        //    return View();
        //}

        //共享文件
        public ActionResult SharedFile()
        {
            User user = Session["userInfo"] as User;
            ViewBag.UserCode = user.UserCode;

            return View();
        }

        //查询
        public ActionResult FileManage_Query(string FileType, string FileName, string CreateUserCode, string BeginApplyDate, string EndApplyDate, int pageindex, int pagesize)
        {
            FileManageBLL bll = new FileManageBLL();
            return Json(bll.FileManage_Query(FileType, FileName, CreateUserCode, BeginApplyDate, EndApplyDate, pageindex, pagesize));

        }

        //上传文件界面
        public ActionResult UploadFiles(string UserCode)
        {
            ViewBag.UserCode = UserCode;
            //获取上传文件最大值
            ViewBag.UploadFilesMaxFileSize = System.Configuration.ConfigurationManager.AppSettings["UploadFilesMaxFileSize"];
            return View();
        }

        //保存上传文件
        public JsonResult UploadFiles_Save(string UserCode, string FileType)
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
                    file.CreateUserCode = UserCode;
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

        //删除文件
        public JsonResult FileManage_Del(string FileCode)
        {
            FileManageBLL bll = new FileManageBLL();
            return Json(bll.FileManage_Del(FileCode));
        }

        //下载文件
        public FileStreamResult DownloadFile(string FileCode,string FileName)
        {
            //根据文件编号获取文件路径
            BenqOAContext bqc = new BenqOAContext();
            var FilePath = bqc.Files.Where(p => p.FileCode == FileCode).Select(p => p.FilePath).First();

            //1.根据文件路径和名称获取文件绝对路径的方法
            string absolutePathName = System.IO.Path.Combine(FilePath, FileName);
            //2.保存 application/octet-stream .*（ 二进制流，不知道下载文件类型）
            return File(new System.IO.FileStream(absolutePathName, System.IO.FileMode.Open), "application/octet-stream", FileName); //Server.UrlEncode(FileName)
        }

    }
}
