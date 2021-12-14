using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ZhiFang.Common.Public;
using ZhiFang.IBLL.Common;
using ZhiFang.BLLFactory;
using ZhiFang.Common.Log;
using System.Text;
using System.Xml;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using ZhiFang.IBLL.Common.BaseDictionary;

namespace ZhiFang.WebLis.Ashx
{
    /// <summary>
    /// ApplyDownload 的摘要说明
    /// </summary>
    public class ApplyDownload : IHttpHandler
    {
        IBReportData ibr = BLLFactory<IBReportData>.GetBLL("ReportData");
        ZhiFang.Model.Lab_TestItem Model_ti = new Model.Lab_TestItem();
        IBCLIENTELE client = ZhiFang.BLLFactory.BLLFactory<IBCLIENTELE>.GetBLL();
        //ZhiFang.DAL.MsSql.Weblis.B_Lab_TestItem dal_ti = new DAL.MsSql.Weblis.B_Lab_TestItem();
        public class item
        {
            public string id = "";
            public string text = "";
        }
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string strObj = context.Request.Form["strObj"];
                if (strObj != "1")
                {
                    DataSet ds_ti = new DataSet();
                    ds_ti = ibr.Getapply(null);
                    DataTable dt = new DataTable();
                    dt = ds_ti.Tables[0].DefaultView.ToTable(true, "ParItemNo", "ParItemName");
                    ds_ti.Tables.Clear();
                    ds_ti.Tables.Add(dt);
                    List<item> ls_ti = new List<item>();

                    if (ds_ti.Tables != null && ds_ti.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds_ti.Tables[0].Rows.Count; i++)
                        {
                            ls_ti.Add(new item
                            {
                                id = ds_ti.Tables[0].Rows[i]["ParItemNo"].ToString(),
                                text = ds_ti.Tables[0].Rows[i]["ParItemName"].ToString(),

                            });
                        }

                        //string jsin = JsonConvert.SerializeObject(new { children = ls_ti.Count, rows = ls_ti });
                        string jsin = JsonConvert.SerializeObject(ls_ti);
                        //string strJsin = "[{\"id\":1,\"text\":\"City\",\"children\":[{\"id\":11,\"text\":\"Wyoming\",\"children\":" + jsin + " }]}]";
                        context.Response.ContentType = "text/json;charset=UTF-8;";
                        context.Response.Write(jsin);
                    }

                }
                else
                {
                    string SendUnit = context.Request.Form["SendUnit"]; //送
                    string PickUnit = context.Request.Form["PickUnit"]; //接
                    string Star = context.Request.Form["Star"];         //起始
                    string Stop = context.Request.Form["Stop"];         //截止
                    string ParItemNo = context.Request.Form["ParItemNo"];     //项目编码
                    DataSet ds = new DataSet();
                    string strWhere = "";

                    if (!string.IsNullOrEmpty(SendUnit))
                    {
                        if (strWhere != null && strWhere != "")
                        {
                            strWhere += " and ClientNo ='" + SendUnit + "'";
                        }
                        else
                        {
                            strWhere += " ClientNo ='" + SendUnit + "'";
                        }
                    }
                    string time = "";
                    if (!string.IsNullOrEmpty(Star))
                    {
                        if (strWhere != null && strWhere != "")
                        {
                            time += " and OperDate between '" + Star + "'";
                        }
                        else
                        {
                            strWhere += " OperDate between '" + Star + "'";
                        }
                    }
                    if (!string.IsNullOrEmpty(Stop))
                    {
                        time += " and  '" + Stop + "' ";

                    }
                    else
                    {
                        time = null;
                    }
                    if (!string.IsNullOrEmpty(ParItemNo))
                    {
                        ParItemNo = ParItemNo.Replace(",", "','");
                        if (strWhere != null && strWhere != "")
                        {
                            strWhere += " and ParItemNo in ('" + ParItemNo + "')";
                        }
                        else
                        {
                            strWhere += " ParItemNo in ('" + ParItemNo + "')";
                        }
                    }
                    strWhere = strWhere + time;
                    if (strWhere.Length > 0)
                    {
                        strWhere = " where " + strWhere;
                    }
                    Log.Info("sql:" + strWhere + time);
                    ds = ibr.Getapply(strWhere);
                    List<apply> ls = new List<apply>();

                    if (ds.Tables != null && ds.Tables.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ls.Add(new apply
                            {
                                OldSerialNo = ds.Tables[0].Rows[i]["OldSerialNo"].ToString(),
                                SerialNo = ds.Tables[0].Rows[i]["SerialNo"].ToString(),
                                SampleTypeName = ds.Tables[0].Rows[i]["SampleTypeName"].ToString(),
                                CName = ds.Tables[0].Rows[i]["CName"].ToString(),
                                GenderName = ds.Tables[0].Rows[i]["GenderName"].ToString(),
                                Age = ds.Tables[0].Rows[i]["Age"].ToString(),
                                CollectDate = ds.Tables[0].Rows[i]["CollectDate"].ToString(),
                                ClientName = ds.Tables[0].Rows[i]["ClientName"].ToString(),
                                OperDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["OperDate"]).ToString("yyyy-MM-dd"),
                                jzName = ds.Tables[0].Rows[i]["jzName"].ToString(),
                                ParItemName = ds.Tables[0].Rows[i]["ParItemName"].ToString(),
                            });
                        }

                        int page = Convert.ToInt32(context.Request.Form["page"]);
                        int row = Convert.ToInt32(context.Request.Form["rows"]);
                        //List<apply> Result = new List<apply>();

                        //int rowCount = page * row;

                        //if (ls.Count >= rowCount)
                        //{
                        //    for (int i = rowCount - row; i < rowCount; i++)
                        //    {
                        //        ZhiFang.Common.Log.Log.Info("1");
                        //        ZhiFang.Common.Log.Log.Info(ls[i].ToString());
                        //        Result.Add(ls[i]);
                        //    }
                        //}
                        //else
                        //{
                        //    if (ls.Count > rowCount - row)
                        //    {
                        //        for (int i = rowCount - row; i < ls.Count; i++)
                        //        {
                        //            ZhiFang.Common.Log.Log.Info("2");
                        //            ZhiFang.Common.Log.Log.Info(ls[i].ToString());
                        //            Result.Add(ls[i]);
                        //        }
                        //    }
                        //}
                        string jsin = JsonConvert.SerializeObject(new { total = ls.Count, rows = ls });
                        context.Response.Write(jsin);
                        XmlDocument xmlRead = new XmlDocument();
                        xmlRead.LoadXml(ds.GetXml());
                    }
                    //xmlRead.DocumentElement;
                }
            }
            catch (Exception ex)
            {
                GetExceptionMsg(ex, ex.ToString());
            }
            //where OperDate between '2014-4-20' and '2014-5-1' 
            //and ItemNo in ('150030','150020') 
            //and ClientNo='12' "

        }
        public class apply
        {
            public string OldSerialNo;
            public string SerialNo;
            public string SampleTypeNo;
            public string SampleTypeName;
            public string CName;
            public int GenderNo;
            public string GenderName;
            public string Age;
            public string CollectDate;
            public string ClientNo;
            public string ClientName;
            public string OperDate;
            public int jztype;
            public string jzName;
            public int ParItemNo;
            public string ParItemName;
        }

        private DataSet MatchClientNo(DataSet ds, string SourceOrgID)
        {
            string[] strArray = ConfigHelper.GetConfigString("TransCodField").Split(new char[] { ';' });
            foreach (string str in strArray)
            {
                List<string> ListStr = new List<string>();
                List<string> ListStrName = new List<string>();
                string B_Lab_controlTableName = "";
                switch (str)
                {
                    case "SAMPLETYPENO":
                        B_Lab_controlTableName = "B_SampleTypeControl";
                        break;
                    case "GENDERNO":
                        B_Lab_controlTableName = "B_GenderTypeControl";
                        break;
                    case "FOLKNO":
                        B_Lab_controlTableName = "B_FolkTypeControl";
                        break;
                    case "ITEMNO":
                        B_Lab_controlTableName = "B_TestItemControl";
                        break;
                    case "SUPERGROUPNO":
                        B_Lab_controlTableName = "B_SuperGroupControl";
                        break;
                }
                if (ds.Tables[0].Columns.Contains(str))
                {
                    for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
                    {
                        if (ds.Tables[0].Rows[count][str].ToString() != "")
                        {
                            ListStr.Add(ds.Tables[0].Rows[count][str].ToString());
                        }
                    }
                    if (ListStr.Count != 0)
                    {
                        DataSet labNo = ibr.GetLabControlNo(B_Lab_controlTableName, ListStr, SourceOrgID, str);
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            foreach (DataRow dritem in labNo.Tables[0].Rows)
                            {
                                if (dr[str].ToString() == dritem[str].ToString() || dr[str].ToString() == "")
                                {
                                    dr[str] = dritem[str].ToString();
                                }
                            }
                        }
                    }
                }
            }
            return ds;
        }
        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }
            else
            {
                sb.AppendLine("【未处理异常】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
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