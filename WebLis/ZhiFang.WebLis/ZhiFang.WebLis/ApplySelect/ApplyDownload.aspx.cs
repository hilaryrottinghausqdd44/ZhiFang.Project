using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.IO;
using System.Text;
using ZhiFang.Common.Public;
using ZhiFang.IBLL.Common;
using ZhiFang.BLLFactory;
using ZhiFang.Common.Log;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.WebLis.ApplySelect
{
    public partial class ApplyDownload : System.Web.UI.Page
    {
        DataSet ds = new DataSet();
        IBReportData ibr = BLLFactory<IBReportData>.GetBLL("ReportData");
        ZhiFang.Model.CLIENTELE Model_Clientele = new Model.CLIENTELE();
        //ZhiFang.DAL.MsSql.Weblis.CLIENTELE dal_cl = new DAL.MsSql.Weblis.CLIENTELE();
        ZhiFang.BLL.Common.BaseDictionary.CLIENTELE b_client = new BLL.Common.BaseDictionary.CLIENTELE();
        IBCLIENTELE client = ZhiFang.BLLFactory.BLLFactory<IBCLIENTELE>.GetBLL();
        public class apply
        {
            public string OldSerialNo;
            public string SerialNo;
            public string SampleTypeNo;
            public string CName;
            public int GenderNo;
            public float Age;
            public string CollectDate;
            public string ClientNo;
            public string OperDate;
            public int jztype;
            public int ParItemNo;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet unit = new DataSet();
                //unit = dal_cl.GetAllList();
                unit = b_client.GetAllList();
                DataTable dt = new DataTable();
                dt = unit.Tables[0];
                DataTable newdt = new DataTable();
                newdt = dt.Clone();
                DataRow[] drName = dt.Select("", " ClIENTNO asc");
                for (int i = 0; i < drName.Length; i++)
                {
                    newdt.ImportRow((DataRow)drName[i]);
                }
                dt = newdt;
                DataSet dss = new DataSet();
                dss.Tables.Add(dt);

                //unit.Tables[0].Select("", " ClIENTNO asc");
                this.SendUnit.Items.Add(new ListItem());
                this.SendUnit.DataSource = dss;
                this.SendUnit.DataValueField = "ClIENTNO";
                this.SendUnit.DataTextField = "cname";
                this.SendUnit.DataBind();

                this.PickUnit.Items.Add(new ListItem());
                this.PickUnit.DataSource = dss;
                this.PickUnit.DataValueField = "ClIENTNO";
                this.PickUnit.DataTextField = "cname";
                this.PickUnit.DataBind();
            }
        }

        private DataSet MatchClientNo(DataSet ds, string SourceOrgID)
        {
            string[] strArray = ConfigHelper.GetConfigString("TransCodField").Split(new char[] { ';' });
            foreach (string str in strArray)
            {
                ZhiFang.Common.Log.Log.Info("需转码TransCodField:" + str);
                List<string> ListStr = new List<string>();
                List<string> ListStrName = new List<string>();
                string B_Lab_controlTableName = "";
                switch (str.ToUpper())
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
                    case "PARITEMNO":
                        B_Lab_controlTableName = "B_TestItemControl";
                        break;
                    case "JZTYPE":
                        B_Lab_controlTableName = "B_SickTypeControl";
                        break;
                    case "SUPERGROUPNO":
                        B_Lab_controlTableName = "B_SuperGroupControl";
                        break;
                }
                string newStr = str;

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
                        if (str == "PARITEMNO")
                        {
                            newStr = "ITEMNO";
                        }
                        if (str == "JZTYPE")
                        {
                            newStr = "SICKTYPENO";
                        }
                        DataSet labNo = ibr.GetLabControlNo(B_Lab_controlTableName, ListStr, SourceOrgID, newStr);
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            foreach (DataRow dritem in labNo.Tables[0].Rows)
                            {
                                if (dr[str].ToString() == dritem[newStr].ToString() || dr[str].ToString() == "")
                                {
                                    if (str == "PARITEMNO")
                                    {
                                        //dr[str] = dritem["ControlItemNo"].ToString();
                                        dr[newStr] = dritem["ControlItemNo"].ToString();//ganwh edit 2016-1-14
                                    }
                                    else if (str == "JZTYPE")
                                    {
                                        dr[str] = dritem["ControlSickTypeNo"].ToString();
                                    }
                                    else if (str == "SAMPLETYPENO")
                                    {
                                        dr[str] = dritem["ControlSampleTypeNo"].ToString();
                                    }
                                    else
                                    {
                                        dr[str] = dritem[str].ToString();
                                    }
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


        protected void dowload_Click(object sender, EventArgs e)
        {
            try
            {
                string SendUnit = this.SendUnit.SelectedValue; //送
                string PickUnit = this.PickUnit.SelectedValue; //接
                string Star = this.star.Value;        //起始
                string Stop = this.stop.Value;         //截止
                string ItemNo = this.txt.Value;     //项目编码
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
                if (!string.IsNullOrEmpty(ItemNo) && ItemNo != "null")
                {
                    ItemNo = ItemNo.Replace(",", "','");
                    if (strWhere != null && strWhere != "")
                    {
                        strWhere += " and ParItemNo in ('" + ItemNo + "')";
                    }
                    else
                    {
                        strWhere += " ParItemNo in ('" + ItemNo + "')";
                    }
                }
                strWhere = strWhere + time;
                if (strWhere.Length > 0)
                {
                    strWhere = " where " + strWhere;
                }
                Log.Info("sql:" + strWhere + time);
                ds = ibr.Getapply(strWhere);
            }
            catch (Exception ex)
            {
                GetExceptionMsg(ex, ex.ToString());
            }
            //ds = MatchClientNo(ds, this.PickUnit.SelectedValue);
            ds = MatchClientNo(ds, this.SendUnit.SelectedValue);//ganwh edit 2016-1-14
            //ds.Tables[0];
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {


                XmlWriterSettings settings = new XmlWriterSettings();

                settings.Indent = false;
                StringBuilder sb = new StringBuilder();

                XmlWriter writer = XmlWriter.Create(sb, settings);

                writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"");

                ds.WriteXml(writer);

                HttpContext.Current.Response.Charset = "UTF-8";

                HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;

                HttpContext.Current.Response.ContentType = "application/xml";

                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=WHT.xml");

                HttpContext.Current.Response.Write(sb.ToString());

                HttpContext.Current.Response.End();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "commit", "alert('无法导出！')", true);
            }
        }

    }
}