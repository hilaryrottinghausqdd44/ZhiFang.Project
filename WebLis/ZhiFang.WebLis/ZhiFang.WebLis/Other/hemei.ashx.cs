using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
using System.Data;
using ZhiFang.WebLis.Class;

namespace ZhiFang.WebLis.Other
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class hemei1 : IHttpHandler
    {

        public  void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            string SEARCH_TYPE="";
            string SEARCH_NO="";
            string STARTDAY = "";
            string ENDDAY = "";
            string p = "";
            if (BaseAshx.CheckQueryStringNull_INT(context, "SEARCH_TYPE"))
            {
                SEARCH_TYPE = BaseAshx.ReadQueryString(context, "SEARCH_TYPE");
                
                //if (BaseAshx.CheckQueryStringNull_INT(context, "SEARCH_NO"))
                //{
                    SEARCH_NO = BaseAshx.ReadQueryString(context, "SEARCH_NO");
                    switch (SEARCH_TYPE)
                    {
                        case "1"://申请单号
                            ZhiFang.Model.NRequestForm nrf = new ZhiFang.Model.NRequestForm();
                            nrf.OldSerialNo = SEARCH_NO;
                            IBNRequestForm nfb = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
                            DataSet ds = nfb.GetList(nrf);
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                p += "serialno=" + ds.Tables[0].Rows[0]["SerialNo"].ToString().Trim(); 
                            }
                            else
                            {
                                context.Response.Write("查无此申请单！");
                                context.Response.End();
                            }
                            break;
                        case "2": p += "ZDY4=" + SEARCH_NO; break;//就诊号
                        case "3": p += "PatNo=" + SEARCH_NO; break;//病历号
                    }

                    if (BaseAshx.CheckQueryStringNull(context, "STARTDAY") && BaseAshx.ReadQueryString(context, "STARTDAY").Trim() != "")
                    {
                        STARTDAY = BaseAshx.ReadQueryString(context, "STARTDAY");
                        if (ZhiFang.Tools.Validate.IsDate(STARTDAY))
                        {
                            p += "&StartDate=" + STARTDAY;
                        }
                    }

                    if (BaseAshx.CheckQueryStringNull(context, "ENDDAY") && BaseAshx.ReadQueryString(context, "ENDDAY").Trim() != "")
                    {
                        ENDDAY = BaseAshx.ReadQueryString(context, "ENDDAY");
                        if (ZhiFang.Tools.Validate.IsDate(ENDDAY))
                        {
                            p += "&EndDate=" + ENDDAY;
                        }
                    }
                    context.Response.Write("hemei.aspx?" + p);
                    context.Response.Redirect("hemei.aspx?" + p);
                //}
                //else
                //{
                //    context.Response.Write("参数错误！SEARCH_NO为空！");
                //}
            }
            else
            {
                context.Response.Write("参数错误！SEARCH_TYPE为空！");
            }
            //context.Response.Redirect()
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
