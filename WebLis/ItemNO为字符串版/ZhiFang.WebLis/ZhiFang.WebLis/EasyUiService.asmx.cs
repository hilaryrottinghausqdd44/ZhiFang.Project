using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.BLLFactory;
using System.Data;

namespace ZhiFang.WebLis
{
    /// <summary>
    /// EasyUiService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://ZhiFang.WebLis/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    [System.Web.Script.Services.ScriptService]
    public class EasyUiService : System.Web.Services.WebService
    {
        private readonly IBPrintFormat pfb = BLLFactory<IBPrintFormat>.GetBLL();
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json,UseHttpGet = true)]
        public void TemplateList()
        {
            HttpContext.Current.Response.ContentType = "application/json; charset=UTF-8";
            HttpContext.Current.Request.ContentType = "application/json; charset=UTF-8";
            DataSet ds = pfb.GetAllList();
            HttpContext.Current.Response.Write("{\"total\":" + pfb.GetTotalCount() + ",\"rows\":" + ZhiFang.BLL.Common.JsonHelp.DataSetToJson(ds) + "}");
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
        public void UploadFile()
        {           
            HttpContext.Current.Response.Write("true");
        }
    }
}
