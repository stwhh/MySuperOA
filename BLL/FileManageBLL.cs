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
    public class FileManageBLL
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
            FileManageDAL dal = new FileManageDAL();
            return dal.FileManage_Query(FileType, FileName, CreateUserCode, BeginApplyDate, EndApplyDate, pageindex, pagesize);
        }


        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="FileCode"></param>
        /// <returns></returns>
        public ResultModel<object> FileManage_Del(string FileCode)
        {
            FileManageDAL dal = new FileManageDAL();
            return dal.FileManage_Del(FileCode);

        }
        #endregion
    }
}
