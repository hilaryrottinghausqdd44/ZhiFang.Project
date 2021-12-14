using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.BLLFactory;
using System.Data;

namespace ZhiFang.WebLis.Ashx
{
    /// <summary>
    /// ReportSystem 的摘要说明
    /// </summary>
    [WebService(Namespace = "ZhiFang.WebLis.Ashx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ReportSystem : IHttpHandler
    {
        private readonly IBPrintFormat pfb = BLLFactory<IBPrintFormat>.GetBLL();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json; charset=UTF-8";
            context.Response.Write(TemplateList());
        }
        public string TemplateList()
        {
            DataSet ds=pfb.GetAllList();
            return "{\"total\":" + pfb.GetTotalCount() + ",\"rows\":" + ZhiFang.BLL.Common.JsonHelp.DataSetToJson(ds) + "}";
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}