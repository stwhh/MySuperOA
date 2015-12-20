using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
        public class ResultModel<T>
        {
            //构造函数
            public ResultModel()
            {
                ErrorCode = "0";
                Message = "";
                PageIndex = 0;
                PageSize = 0;
                TotalCount = 0;
            }

            public T Data { get; set; }
            public string ErrorCode { get; set; }
            public string Message { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int TotalCount { get; set; }
        }
}