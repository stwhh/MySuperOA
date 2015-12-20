using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BenqOA.Helper
{
    public class FileHelper
    {
        /// <summary>
        /// 删除旧文件
        /// </summary>
        /// <param name="controller">调用此方法的controller</param>
        /// <param name="path">文件所在路径，文件的往上第二级目录</param>
        /// <param name="exportOrImport">导入还是导出目录，Export或者Import</param>
        /// <param name="fileName">文件名，不包含日期尾部，也不含扩展名</param>
        /// <returns></returns>
        public static bool DeleteHistoryFiles(Controller controller, string path, string exportOrImport)
        {
            string dirPath = "~/Files/ExcelFiles/" + path + "/" + exportOrImport + "/"; //文件夹路径      
            try
            {
                var mydir = new DirectoryInfo(controller.Server.MapPath(dirPath));
                var files = mydir.GetFiles();
                //前一天的文件
                var compare = DateTime.Now.AddDays(-1).ToString("yyyyMMdd");
                foreach (var info in files)
                {
                    if (info.Name.Contains(compare))
                    {
                        System.IO.File.Delete(info.FullName);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    public class ConfHelper
    {
        public const int DIGIT = 2;
    }
}