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
        /// 查询
        /// </summary>
        /// <param name="FileType"></param>
        /// <param name="FileName"></param>
        /// <param name="CreateUserCode"></param>
        /// <param name="BeginApplyDate"></param>
        /// <param name="EndApplyDate"></param>
        /// <param name="pageindex"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ResultModel<object> FileManage_Query(string FileType, string FileName, string CreateUserCode, string BeginApplyDate, string EndApplyDate, int pageindex, int pagesize)
        {
            ResultModel<object> resultModel = new ResultModel<object>();
            BenqOAContext bqc = new BenqOAContext();
            //基于方法的Liqn查询=>lambda表达式
            //var list = bqc.Announces.OrderBy(p=>p.ID).Skip(pageindex*pagesize).Take(pagesize).ToList(); 

            DateTime bd = Convert.ToDateTime(BeginApplyDate);
            DateTime ed = Convert.ToDateTime(EndApplyDate).AddHours(23.9); //加23.9小时 默认是0时，加了以后才好跟前台日期比较

            //Linq to Entity
            var list = (from a in bqc.Files
                        from b in bqc.FileTypes
                        where a.FileType == b.FilesTypeId
                        && a.FileType.Contains(FileType) //公告编号应该是=，但是用contains可以防止为空的情况
                        && a.FileName.Contains(FileName)
                        && a.CreateUserCode.Contains(CreateUserCode)
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
                         && a.FileType.Contains(FileType) //公告编号应该是=，但是用contains可以防止为空的情况
                         && a.FileName.Contains(FileName)
                         && a.CreateUserCode.Contains(CreateUserCode)
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
        /// <param name="FileCode"></param>
        /// <returns></returns>
        public ResultModel<object> FileManage_Del(string FileCode)
        {
            //string ucode = Request.Params["userCode"].ToString(); //要使用此方法，前台不能以json格式传值，

            ResultModel<object> resultModel = new ResultModel<object>();
            using (var bqc = new BenqOAContext())
            {
                var file = bqc.Files.Where(p => p.FileCode == FileCode).FirstOrDefault(); //查询要删除的用户表数据
                try
                {
                    //1.先删除物理路径中文件
                    //文件路径
                    //string filePath = System.Configuration.ConfigurationManager.AppSettings["UploadFilesPath"];
                    string filePath = file.FilePath;
                    //根据文件编号查文件全名（路径+文件名）
                    string FileName = bqc.Files.Where(p => p.FileCode == FileCode).Select(p => p.FileName).First();
                    string url = System.IO.Path.Combine(filePath,FileName);
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
