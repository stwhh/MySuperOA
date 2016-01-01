using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Model;
using Model.Models;

namespace DAL
{
    public class FileManageDAL
    {
        #region 文件管理
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
        public ResultModel<object> FileManage_Query(string fileType, string fileName, string createUserCode, string beginApplyDate, string endApplyDate, int pageindex, int pagesize)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            //基于方法的Liqn查询=>lambda表达式
            //var list = bqc.Announces.OrderBy(p=>p.ID).Skip(pageindex*pagesize).Take(pagesize).ToList(); 

            DateTime bd = Convert.ToDateTime(beginApplyDate);
            DateTime ed = Convert.ToDateTime(endApplyDate).AddHours(23.9); //加23.9小时 默认是0时，加了以后才好跟前台日期比较

            //Linq to Entity
            var list = (from a in bqc.Files
                        from b in bqc.FileTypes
                        where a.FileType == b.FilesTypeId
                        && a.FileType.Contains(fileType) //公告编号应该是=，但是用contains可以防止为空的情况
                        && a.FileName.Contains(fileName)
                        && a.CreateUserCode.Contains(createUserCode)
                        && a.CreateTime.CompareTo(bd) >= 0
                        && a.CreateTime.CompareTo(ed) <= 0
                        orderby a.CreateTime descending
                        select new
                        {
                            FileCode = a.FileCode,
                            FileName = a.FileName,
                            FileSize=a.FileSize,
                            FileType = b.FilesTypeName,
                            CreateUserCode = a.CreateUserCode,
                            CreateTime = a.CreateTime,
                        }).Skip(pageindex * pagesize).Take(pagesize);

            var count = (from a in bqc.Files
                         from b in bqc.FileTypes
                         where a.FileType == b.FilesTypeId
                         && a.FileType.Contains(fileType) //公告编号应该是=，但是用contains可以防止为空的情况
                         && a.FileName.Contains(fileName)
                         && a.CreateUserCode.Contains(createUserCode)
                         && a.CreateTime.CompareTo(bd) >= 0
                         && a.CreateTime.CompareTo(ed) <= 0
                        select a ).Count();

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
        /// 删除文件
        /// </summary>
        /// <param name="fileCode">文件编号</param>
        /// <returns></returns>
        public ResultModel<object> FileManage_Del(string fileCode)
        {
            //string ucode = Request.Params["userCode"].ToString(); //要使用此方法，前台不能以json格式传值，

            ResultModel<object> resultModel = new ResultModel<object>();
            using (var bqc = new BenqOAContext())
            {
                var file = bqc.Files.Where(p => p.FileCode == fileCode).FirstOrDefault(); //查询要删除的用户表数据
                try
                {
                    //1.先删除物理路径中文件
                    //文件路径
                    //string filePath = System.Configuration.ConfigurationManager.AppSettings["UploadFilesPath"];
                    string filePath = file.FilePath;
                    //根据文件编号查文件全名（路径+文件名）
                    string fileName = bqc.Files.Where(p => p.FileCode == fileCode).Select(p => p.FileName).First();
                    string url = System.IO.Path.Combine(filePath,fileName);
                    System.IO.File.Delete(url);

                    //2.再删除数据库中信息
                    bqc.Files.Remove(file); //删除
                    bqc.SaveChanges(); //保存更改

                    resultModel.ErrorCode = "0";
                    resultModel.Message = "";

                }
                catch (Exception ee)
                {
                    resultModel.ErrorCode = "1";
                    resultModel.Message = ee.Message;
                }
                return resultModel;
            }
        }
        #endregion
    }
}
