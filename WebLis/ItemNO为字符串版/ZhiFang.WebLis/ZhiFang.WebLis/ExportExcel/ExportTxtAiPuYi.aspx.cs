using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.WebLis.Class;
using System.Data;
using ZhiFang.IBLL.Report;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.BLLFactory;
using FastReport;
using FastReport.Export.Mht;
using Common;
using System.IO;
using System.Text;

namespace ZhiFang.WebLis.ExportExcel
{
    public partial class ExportTxtAiPuYi : BasePage
    {
        private readonly IBView_ReportItemFull ibvtif = BLLFactory<IBView_ReportItemFull>.GetBLL("BView_ReportItemFull");
        private readonly IBReportItemFull rfif = BLLFactory<IBReportItemFull>.GetBLL("ReportItemFull");
        private ZhiFang.Model.VIEW_ReportItemFull vrif = new ZhiFang.Model.VIEW_ReportItemFull();
        private readonly IBBusinessLogicClientControl iblcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
        protected string strTable = "";
        DataSet ds;
        FastReport.Report report = new FastReport.Report();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    #region 报告开始时间
                    if (base.CheckPageControlNull("CheckStartDate"))
                    {
                        System.Web.UI.HtmlControls.HtmlInputText CheckStartDate = (System.Web.UI.HtmlControls.HtmlInputText)this.Page.FindControl("CheckStartDate");
                        if (ConfigHelper.GetConfigInt("SearchBeforeDayNum") == null)
                        {
                            CheckStartDate.Value = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            CheckStartDate.Value = DateTime.Now.AddDays(ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value).ToString("yyyy-MM-dd");
                        }
                        TextBoxSetValueByQuery("CheckStartDate", CheckStartDate);
                    }
                    #endregion
                    #region 报告结束时间
                    if (base.CheckPageControlNull("CheckEndDate"))
                    {
                        System.Web.UI.HtmlControls.HtmlInputText CheckEndDate = (System.Web.UI.HtmlControls.HtmlInputText)this.Page.FindControl("CheckEndDate");
                        CheckEndDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
                        TextBoxSetValueByQuery("CheckEndDate", CheckEndDate);
                    }
                    #endregion
                    #region 送检单位
                    if (base.CheckPageControlNull("Client"))
                    {
                        if (base.CheckCookies("ZhiFangUser"))
                        {
                            User u = new User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                            ZhiFang.Common.Log.Log.Info("公司名称：" + u.CompanyName + "用户账号：" + u.Account);
                            System.Web.UI.HtmlControls.HtmlSelect Client = (System.Web.UI.HtmlControls.HtmlSelect)this.Page.FindControl("Client");
                            DataSet ds = u.GetClientListByPost("", -1);
                            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                Client.DataSource = ds;
                                Client.DataTextField = "CName";
                                Client.DataValueField = "ClientNo";
                                Client.DataBind();
                                Client.Items.Insert(0, new ListItem("", ""));
                                SelectSetValueByQuery("ClientNo", Client);
                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("异常信息:" + ex.ToString());
            }
        }
        protected void linkGetAllItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (base.CheckPageControlNull("CheckStartDate"))
                {
                    vrif.Checkstartdate = Convert.ToDateTime(base.ReadForm("CheckStartDate"));
                }
                else
                {
                    vrif.Checkstartdate = null;
                }
                if (base.CheckPageControlNull("CheckEndDate"))
                {
                    vrif.Checkenddate = Convert.ToDateTime(base.ReadForm("CheckEndDate") + " 23:59:59");
                }
                else
                {
                    vrif.Checkenddate = null;
                }
                if (base.CheckFormNullAndValue("CName"))
                {
                    vrif.CNAME = base.ReadForm("CName");
                }
                if (base.CheckFormNullAndValue("Bed"))
                {
                    vrif.BED = base.ReadForm("Bed");
                }
                if (base.CheckFormNullAndValue("SerialNo"))
                {
                    vrif.SERIALNO = base.ReadForm("SerialNo");
                }
                if (base.CheckFormNullAndValue("SampleNo"))
                {
                    try
                    {
                        vrif.SAMPLENO = base.ReadForm("SampleNo");
                    }
                    catch
                    {

                    }
                }
                if (base.CheckPageControlNull("Client"))
                {
                    if (!base.CheckFormNullAndValue("Client"))
                    {
                        User u = new User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                        u.GetPostList();
                        DataSet dsc = new DataSet();
                        if (u.PostList.Exists(l => { if (l.ToUpper() == "LOGISTICSOFFICER")return true; else return false; }))
                        {
                            dsc = iblcc.GetClientList_DataSet(new ZhiFang.Model.BusinessLogicClientControl() { Account = u.Account.Trim(), SelectedFlag = true });
                            ZhiFang.Common.Log.Log.Info(dsc.Tables[0].Rows.Count.ToString());
                            for (int i = 0; i < dsc.Tables[0].Rows.Count; i++)
                            {
                                vrif.ClientList += " '" + dsc.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
                            }
                            vrif.ClientList = vrif.ClientList.Remove(vrif.ClientList.Length - 1);
                        }
                    }
                    else
                    {
                        vrif.CLIENTNO = base.ReadForm("Client");
                    }
                }
                ds = ibvtif.GetViewItemFull(vrif);
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel") + "\\ReportItemFullShow.frx");
                DataTable dt = ds.Tables[0];
                dt.TableName = "Table";
                ViewState["dataset"] = ds;
                GC.Collect();
                report.RegisterData(dt.DataSet);
                DataBand data = report.FindObject("Data1") as DataBand;
                data.DataSource = report.GetDataSource("Table");
                report.Prepare();
                MHTExport export = new MHTExport();
                string url = DateTime.Now.ToString().Replace(" ", "_").Replace(":", "_").Replace("/", "_") + ".mht";
                report.Export(export, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + url);
                report.Dispose();
                strTable = "../" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + url;
                ViewState["fielPath"] = strTable;
            }
            catch (Exception eee)
            {
                ZhiFang.Common.Log.Log.Debug(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + eee.ToString());
            }
        }
        protected void ReportItemFullExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (strTable == "")
                {
                    strTable = ViewState["fielPath"].ToString();
                    ZhiFang.Common.Log.Log.Info(strTable);
                }
                ds = (DataSet)ViewState["dataset"];
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + DateTime.Now.ToString().Replace(" ", "_").Replace(":", "_").Replace("/", "_") + ".txt";
                System.IO.FileInfo file = new System.IO.FileInfo(path);
                StreamWriter sw = null;
                if (!file.Exists)
                {
                    sw = file.CreateText();
                }
                //fs = new FileStream(path, FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
                //sw = new StreamWriter(fs, Encoding.UTF8);
                string CName = "";
                string PatNo = "";
                string OPERDATE = "";
                string ItemCode = "";
                string ReportValue = "";
                string Unit = "";
                string REFRANGE = "";
                string ReportDesc = "";
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string ReportFormId = ds.Tables[0].Rows[i]["ReportFormId"].ToString();
                        if (i == 0)
                        {
                            string content = "STX";
                            sw.WriteLine(content);
                            CName = ds.Tables[0].Rows[i]["Cname"].ToString();
                            PatNo = ds.Tables[0].Rows[i]["PatNo"].ToString();
                            OPERDATE = (Convert.ToDateTime(ds.Tables[0].Rows[i]["OPERDATE"])).ToString("yyyy-MM-dd");
                            sw.WriteLine("1," + CName + "," + PatNo + "," + PatNo + "," + OPERDATE);
                            Model.ReportItemFull ReportItemFull = new Model.ReportItemFull();
                            ReportItemFull.ReportFormID = ds.Tables[0].Rows[i]["ReportFormID"].ToString();
                            DataSet dsItem = rfif.GetList(ReportItemFull);
                            if (dsItem != null && dsItem.Tables[0].Rows.Count > 0)
                            {
                                for (int h = 0; h < dsItem.Tables[0].Rows.Count; h++)
                                {
                                    ItemCode = ds.Tables[0].Rows[h]["TESTITEMSNAME"].ToString();
                                    ReportValue = ds.Tables[0].Rows[h]["ReportValue"].ToString();
                                    ReportDesc = ds.Tables[0].Rows[h]["ReportDesc"].ToString();
                                    Unit = ds.Tables[0].Rows[h]["UNIT"].ToString();
                                    REFRANGE = ds.Tables[0].Rows[h]["REFRANGE"].ToString();
                                    sw.WriteLine("2," + ItemCode + "," + ItemCode + "," + ReportValue + ReportDesc + "," + Unit + "," + REFRANGE);
                                }
                            }
                            sw.WriteLine("ETX");

                        }
                        if (i > 0)
                        {
                            if (ReportFormId != ds.Tables[0].Rows[i - 1]["ReportFormId"].ToString())
                            {
                                string content = "STX";
                                sw.WriteLine(content);
                                CName = ds.Tables[0].Rows[i]["Cname"].ToString();
                                PatNo = ds.Tables[0].Rows[i]["PatNo"].ToString();
                                OPERDATE = (Convert.ToDateTime(ds.Tables[0].Rows[i]["OPERDATE"])).ToString("yyyy-MM-dd");
                                sw.WriteLine("1," + CName + "," + PatNo + "," + PatNo + "," + OPERDATE);
                                Model.ReportItemFull ReportItemFull = new Model.ReportItemFull();
                                ReportItemFull.ReportFormID = ds.Tables[0].Rows[i]["ReportFormID"].ToString();
                                DataSet dsItem = rfif.GetList(ReportItemFull);
                                if (dsItem != null && dsItem.Tables[0].Rows.Count > 0)
                                {
                                    for (int h = 0; h < dsItem.Tables[0].Rows.Count; h++)
                                    {
                                        ItemCode = ds.Tables[0].Rows[h]["TESTITEMSNAME"].ToString();
                                        ReportValue = ds.Tables[0].Rows[h]["ReportValue"].ToString();
                                        ReportDesc = ds.Tables[0].Rows[h]["ReportDesc"].ToString();
                                        Unit = ds.Tables[0].Rows[h]["UNIT"].ToString();
                                        REFRANGE = ds.Tables[0].Rows[h]["REFRANGE"].ToString();
                                        sw.WriteLine("2," + ItemCode + "," + ItemCode + "," + ReportValue + ReportDesc + "," + Unit + "," + REFRANGE);
                                    }
                                }
                                sw.WriteLine("ETX");

                            }

                        }
                    }
                }
                sw.Flush();
                sw.Close();
                System.IO.FileInfo file1 = new System.IO.FileInfo(path);
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment;   filename=" + Server.UrlEncode(file1.Name));//文件名称   
                Response.AddHeader("Content-Length", file1.Length.ToString());//文件长度   
                Response.ContentType = "application/octet-stream";//获取或设置HTTP类型   
                Response.ContentEncoding = System.Text.Encoding.Default;
                Response.WriteFile(path);//将文件内容作为文件块直接写入HTTP响应输出流
                Response.Write("<script>alert('操作成功！');window.close();</script>");
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('操作失败！');window.close();</script>");
                ZhiFang.Common.Log.Log.Error("异常信息:" + ex.ToString());
            }

        }
        private void TextBoxSetValueByQuery(string a, System.Web.UI.HtmlControls.HtmlInputText h)
        {
            if (base.CheckQueryString(a) && base.ReadQueryString(a).Split(',').Length == 2)
            {
                try
                {
                    if (base.ReadQueryString(a).Split(',')[1] == "0")
                    {
                        h.Value = base.ReadQueryString(a).Split(',')[0].Trim();
                        h.Disabled = true;
                    }
                    else
                    {
                        if (base.ReadQueryString(a).Split(',')[1] == "1")
                        {
                            h.Value = base.ReadQueryString(a).Split(',')[0].Trim();
                        }
                    }
                }
                catch
                {

                }
            }
        }
        private void SelectSetValueByQuery(string a, System.Web.UI.HtmlControls.HtmlSelect h)
        {
            if (base.CheckQueryString(a))
            {
                try
                {
                    foreach (ListItem l in h.Items)
                    {
                        l.Selected = false;
                        if (l.Value == base.ReadQueryString(a).Trim())
                        {
                            l.Selected = true;
                            break;
                        }
                    }
                    h.Disabled = true;
                }
                catch
                {

                }
            }
        }
    }
}