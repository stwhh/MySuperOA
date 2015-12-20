using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Model
{
    public class SelectListItem
    {
         // 摘要:
        //     初始化 System.Web.Mvc.SelectListItem 类的新实例。
        public SelectListItem()
        {

        }

        // 摘要:
        //     获取或设置一个值，该值指示是否选择此 System.Web.Mvc.SelectListItem。
        //
        // 返回结果:
        //     如果选定此项，则为 true；否则为 false。
        public bool Selected { get; set; }
        //
        // 摘要:
        //     获取或设置选定项的文本。
        //
        // 返回结果:
        //     文本。
        public string Text { get; set; }
        //
        // 摘要:
        //     获取或设置选定项的值。
        //
        // 返回结果:
        //     值。
        public string Value { get; set; }
    }
}