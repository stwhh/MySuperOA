using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;

namespace BenqOA.Models
{
    public class AsnycHandler : IHttpAsyncHandler
    {
        public AsnycHandler()
        {
        }

        #region 实现 IHttpAsyncHandler接口
        public void EndProcessRequest(IAsyncResult result)
        {

        }

        public bool IsReusable
        {
            get { return false; ; }
        }

        public void ProcessRequest(HttpContext context)
        {
        }
        #endregion

        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
        {
            //myAsynResult为实现了IAsyncResult接口的类，当不调用cb的回调函数时，该请求不会返回到给客户端，会一直处于连接状态
            myAsynResult asyncResult = new myAsynResult(context, cb, extraData);
            String content = context.Request.Params["content"];

            //向AsnyMessage类中添加该消息
            AsnycMessages.GetInstance().AddMessage(content, asyncResult);
            return asyncResult;
        }
    }


    //继承异步操作接口
    public class myAsynResult : IAsyncResult
    {
        bool _IsCompleted = false;
        private HttpContext context;
        private AsyncCallback cb;
        private object extraData;

        //构造函数
        public myAsynResult(HttpContext context, AsyncCallback cb, object extraData)
        {
            this.context = context;
            this.cb = cb;
            this.extraData = extraData;
        }

        #region 实现IAsyncResult接口中定义的字段
        public object AsyncState //用户定义的对象，它限定或包含关于异步操作的信息。
        {
            get { return null; }
        }

        public System.Threading.WaitHandle AsyncWaitHandle // 获取一个值，该值指示异步操作是否同步完成
        {
            get { return null; }
        }

        public bool CompletedSynchronously //如果异步操作同步完成，则为 true；否则为 false。
        {
            get { return false; }
        }
        public bool IsCompleted  //如果操作完成则为 true，否则为 false。
        {
            get { return _IsCompleted; }
        }
        #endregion

        private string _content;
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        //在Message类中的添加消息方法中，调用该方法，将消息输入到客户端，从而实现广播的功能
        public void Send(object data)
        {
            context.Response.Write(this.Content);
            if (cb != null)
            {
                cb(this);
            }
            _IsCompleted = true;
        }
    }
}
