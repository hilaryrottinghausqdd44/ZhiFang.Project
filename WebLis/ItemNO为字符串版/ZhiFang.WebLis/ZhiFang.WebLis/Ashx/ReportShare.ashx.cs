using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Data;
using System.Web.Services;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Report;
using ZhiFang.IBLL.Common.BaseDictionary;
using Newtonsoft.Json;
using System.Linq;
using Newtonsoft.Json.Converters;


namespace ZhiFang.WebLis.Ashx
{
    [WebService(Namespace = "ZhiFang.WebLis.Ashx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    /// <summary>
    /// ReportShare 的摘要说明
    /// </summary>
    public class ReportShare : IHttpHandler
    {
        private readonly IBBusinessLogicClientControl iblcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();

        /// <summary>
        /// 报告同步
        /// </summary>
        ZhiFang.Model.ReportFileInfo Model_File = new Model.ReportFileInfo();
        private readonly IReportFileInfo IbFile = BLLFactory<IReportFileInfo>.GetBLL("ReportFileInfo");
        ZhiFang.Model.CLIENTELE Model_Clientele = new Model.CLIENTELE();
        //ZhiFang.DAL.MsSql.Weblis.CLIENTELE dal_cl = new DAL.MsSql.Weblis.CLIENTELE();
        IBCLIENTELE dal_cl = ZhiFang.BLLFactory.BLLFactory<IBCLIENTELE>.GetBLL();
        public class L_item
        {//年龄，性别，身份证号，手机号
            public int id = 0;
            public string Report_Time = "";
            public string Name = "";
            public string Card_ID = "";
            public string Age = "";
            public string Sex = "";
            public string Medical_Institution_Code = "";
            public string ProjectCode = "";
            public string PageNo = "";
            public string File_Name = "";
            public string File_Url = "";
        }
        public void ProcessRequest(HttpContext context)
        {
            #region
            context.Response.ContentType = "text/plain";
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            context.Response.AddHeader("pragma", "no-cache");
            context.Response.AddHeader("cache-control", "");
            context.Response.CacheControl = "no-cache";
            #endregion
            List<L_item> ls = new List<L_item>();
            DataSet bds = IbFile.GetList(0,""," PageNo ");
            ZhiFang.Common.Log.Log.Info(bds.Tables[0].Rows.Count.ToString());
            try
            {

                if (bds != null && bds.Tables[0].Rows.Count > 0)
                {
                    //虚拟目录/报告查询页面?调用医院ID=123&报告医院ID=123&被检验者万达的PAT_ID =123&
                    //加密验证字符串=1234567890&报告开始时间=2013-12-01&报告结束时间=2013-12-01
                    DataTable dt = new DataTable();
                    dt = bds.Tables[0];
                    DataTable newdt = new DataTable();
                    newdt = dt.Clone();
                    string strWhere = "";
                    bool dtRow = false;
                    //if (!string.IsNullOrEmpty("Report_Time"))//加密验证字符串
                    //{
                    //    if (strWhere != null && strWhere != "")
                    //    {
                    //        strWhere += " and Report_Time>='" + "Report_Time" + "'";
                    //    }
                    //    else
                    //    {
                    //        strWhere += " Report_Time>='" + "Report_Time" + "'";
                    //    }
                    //    strWhere += " and Report_Time<='" + "Report_Time" + "'";
                    //    dtRow = true;
                    //}Request.Form[""]
                    //string pat_id = context.Request.Form["PAT_ID"];
                    //if (!string.IsNullOrEmpty(pat_id))//万达的PAT_ID
                    //{
                    //    if (strWhere != null && strWhere != "")
                    //    {
                    //        strWhere += " and PAT_ID like '%" + pat_id + "%'";
                    //    }
                    //    else
                    //    {
                    //        strWhere += " PAT_ID like '%" + pat_id + "%'";
                    //    }
                    //    dtRow = true;
                    //}
                    string Report_TimeStar = context.Request.Form["Report_TimeStar"];
                    string Report_TimeStop = context.Request.Form["Report_TimeStop"];
                    if (!string.IsNullOrEmpty(Report_TimeStar))//报告审核时间
                    {
                        if (strWhere != null && strWhere != "")
                        {
                            strWhere += " and Report_Time >='" + Report_TimeStar + "'";
                        }
                        else
                        {
                            strWhere += " Report_Time >='" + Report_TimeStar + "'";
                        }
                        strWhere += " and Report_Time <='" + Report_TimeStop + "'";
                        dtRow = true;
                    }
                    string Card_ID = context.Request.Form["Card_ID"];
                    if (!string.IsNullOrEmpty(Card_ID))//身份证号
                    {
                        if (strWhere != null && strWhere != "")
                        {
                            strWhere += " and Card_ID like '%" + Card_ID + "%'";
                        }
                        else
                        {
                            strWhere += " Card_ID like '%" + Card_ID + "%'";
                        }

                        dtRow = true;
                    }
                    string Name = context.Request.Form["Name"];
                    if (!string.IsNullOrEmpty(Name))//姓名
                    {
                        if (strWhere != null && strWhere != "")
                        {
                            strWhere += " and Name ='" + Name + "'";
                        }
                        else
                        {
                            strWhere += " Name ='" + Name + "'";
                        }

                        dtRow = true;
                    }
                    string Age = context.Request.Form["Age"];
                    if (!string.IsNullOrEmpty(Age))//年龄
                    {
                        if (strWhere != null && strWhere != "")
                        {
                            strWhere += " and Age ='" + Age + "'";
                        }
                        else
                        {
                            strWhere += " Age ='" + Age + "'";
                        }

                        dtRow = true;
                    }
                    string Sex = context.Request.Form["Sex"];
                    if (!string.IsNullOrEmpty(Sex) && Sex != "全部")//性别
                    {
                        if (strWhere != null && strWhere != "")
                        {
                            strWhere += " and Sex ='" + Sex + "'";
                        }
                        else
                        {
                            strWhere += " Sex ='" + Sex + "'";
                        }

                        dtRow = true;
                    }
                    //if (!string.IsNullOrEmpty("Report_Time"))//调用医院ID
                    //{
                    //    if (strWhere != null && strWhere != "")
                    //    {
                    //        strWhere += " and Report_Time>='" + "Report_Time" + "'";
                    //    }
                    //    else
                    //    {
                    //        strWhere += " Report_Time>='" + "Report_Time" + "'";
                    //    }
                    //    strWhere += " and Report_Time<='" + "Report_Time" + "'";
                    //    dtRow = true;
                    //}
                    //if (!string.IsNullOrEmpty("Report_Time"))//报告医院ID
                    //{
                    //    if (strWhere != null && strWhere != "")
                    //    {
                    //        strWhere += " and Report_Time>='" + "Report_Time" + "'";
                    //    }
                    //    else
                    //    {
                    //        strWhere += " Report_Time>='" + "Report_Time" + "'";
                    //    }
                    //    strWhere += " and Report_Time<='" + "Report_Time" + "'";
                    //    dtRow = true;
                    //}
                    //查询历时数据
                    int n = 0;
                    if (Convert.ToInt32(context.Request.Form["ChangeStatus"]) != 0)
                    {
                        n = Convert.ToInt32(context.Request.Form["ChangeStatus"]);
                    }
                    if (n == -1)
                    {

                    }
                    else
                    {
                        if (strWhere != null && strWhere != "")
                        {
                            strWhere += String.Format(" and ChangeStatus ='{0}'", n);
                        }
                        else
                        {
                            strWhere += String.Format(" ChangeStatus ='{0}'", n);
                        }
                        dtRow = true;
                    }

                    if (dtRow)
                    {
                        DataRow[] drName = dt.Select(strWhere," PageNo asc");
                        for (int i = 0; i < drName.Length; i++)
                        {
                            newdt.ImportRow((DataRow)drName[i]);
                        }
                        dt = newdt;
                    }
                    else
                    {
                        DataRow[] drName = dt.Select("", " PageNo asc");
                        for (int i = 0; i < drName.Length; i++)
                        {
                            newdt.ImportRow((DataRow)drName[i]);
                        }
                        dt = newdt;
                    }
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);

                    string ReportFile = "";
                    try
                    {
                        ReportFile = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFile");
                    }
                    catch (Exception)
                    {
                        ZhiFang.Common.Log.Log.Info("没有配置报告网络地址");
                    }
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ls.Add(new L_item
                        {

                            Report_Time = ds.Tables[0].Rows[i]["Report_Time"].ToString(),
                            Name = ds.Tables[0].Rows[i]["Name"].ToString(),
                            Card_ID = ds.Tables[0].Rows[i]["Card_ID"].ToString(),
                            Age = ds.Tables[0].Rows[i]["Age"].ToString(),
                            Sex = ds.Tables[0].Rows[i]["Sex"].ToString(),
                            Medical_Institution_Code = ds.Tables[0].Rows[i]["Medical_Institution_Code"].ToString(),
                            ProjectCode=ds.Tables[0].Rows[i]["ProjectCode"].ToString(),
                            PageNo = ds.Tables[0].Rows[i]["PageNo"].ToString(),
                            File_Name = ds.Tables[0].Rows[i]["File_Name"].ToString(),
                            File_Url = ReportFile + ds.Tables[0].Rows[i]["File_Url"].ToString()

                        });
                        ZhiFang.Common.Log.Log.Info("绑定数据"+i);
                    }
                }


                int page = Convert.ToInt32(context.Request.Form["page"]);
                int row = Convert.ToInt32(context.Request.Form["rows"]);
                List<L_item> Result = new List<L_item>();

                int rowCount = page * row;

                if (ls.Count >= rowCount)
                {
                    for (int i = rowCount - row; i < rowCount; i++)
                    {
                        ZhiFang.Common.Log.Log.Info("1");
                        ZhiFang.Common.Log.Log.Info(ls[i].ToString());
                        Result.Add(ls[i]);
                    }
                }
                else
                {
                    if (ls.Count > rowCount - row)
                    {
                        for (int i = rowCount - row; i < ls.Count; i++)
                        {
                            ZhiFang.Common.Log.Log.Info("2");
                            ZhiFang.Common.Log.Log.Info(ls[i].ToString());
                            Result.Add(ls[i]);
                        }
                    }
                }
                string jsin = JsonConvert.SerializeObject(new { total = ls.Count, rows = Result });

                context.Response.Write(jsin);
            }
            catch (Exception e)
            {

                ZhiFang.Common.Log.Log.Info(e.StackTrace + e.Message + e.ToString());
            }
            //context.Response.Write(new JavaScriptSerializer().Serialize(ls));
        }
        [AjaxPro.AjaxMethod(AjaxPro.HttpSessionStateRequirement.ReadWrite)]
        public string Lyuan(long clienteleNo)
        {
            Model_Clientele.ClIENTNO = clienteleNo.ToString();
            DataSet ds = dal_cl.GetList(Model_Clientele);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["CNAME"].ToString();
            }
            else
            {
                return "信息为空！";
            }
        }
        #region IHttpHandler 成员

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        //public void ProcessRequest(HttpContext context)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
    }
}