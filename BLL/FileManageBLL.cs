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
            FileManageDAL dal = new FileManageDAL();
            return dal.FileManage_Query(fileType, fileName, createUserCode, beginApplyDate, endApplyDate, pageindex, pagesize);
        }


        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileCode">文件编号</param>
        /// <returns></returns>
        public ResultModel<object> FileManage_Del(string fileCode)
        {
            FileManageDAL dal = new FileManageDAL();
            return dal.FileManage_Del(fileCode);

        }
        #endregion
    }
}
