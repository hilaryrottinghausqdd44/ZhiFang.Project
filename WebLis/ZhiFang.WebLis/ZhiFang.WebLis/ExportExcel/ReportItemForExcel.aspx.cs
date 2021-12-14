using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
using System.Data;
using FastReport.Export.Mht;
using FastReport.Export.Xml;
using FastReport;
using ZhiFang.WebLis.Class;
using ZhiFang.IBLL.Common.BaseDictionary;
using Common;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace ZhiFang.WebLis.ExportExcel
{
    public partial class ReportItemForExcel : BasePage
    { 
        private readonly IBView_ReportItemFull ibvtif = BLLFactory<IBView_ReportItemFull>.GetBLL("BView_ReportItemFull");
        private ZhiFang.Model.VIEW_ReportItemFull vrif = new ZhiFang.Model.VIEW_ReportItemFull();
        private readonly IBBusinessLogicClientControl iblcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
        protected string strTable = "";
        DataSet ds;
        FastReport.Report report = new FastReport.Report();
        protected void Page_Load(object sender, EventArgs e)
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
                string url = DateTime.Now.ToString().Replace(" ", "_").Replace(":", "_").Replace("/", "_") + ".xls";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\" + url;
                ds = (DataSet)ViewState["dataset"];
                #region 方法1
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ItemShowExcel") + "\\ReportItemFullForExcel.frx");
                report.RegisterData(ds);
                DataBand data = report.FindObject("Data1") as DataBand;
                data.DataSource = report.GetDataSource("Table");
                report.Prepare();
                //FastReport.Export.Csv.CSVExport export = new FastReport.Export.Csv.CSVExport();
                FastReport.Export.Xml.XMLExport export = new FastReport.Export.Xml.XMLExport();
                report.Export(export, strPath);
                report.Dispose();
                ////将xml文件转换为标准的Excel格式 
                //Object Nothing = Missing.Value;//由于yongCOM组件很多值需要用Missing.Value代替   
                //Excel.Application ExclApp = new Excel.Application();// 初始化
                //Excel.Workbook ExclDoc = ExclApp.Workbooks.Open(strPath, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing);//打开Excl工作薄   
                //try
                //{
                //    Object format = Excel.XlFileFormat.xlWorkbookNormal;//获取Excl 2007文件格式   
                //    ExclApp.DisplayAlerts = false;
                //    ExclDoc.SaveAs(strPath, format, Nothing, Nothing, Nothing, Nothing, Excel.XlSaveAsAccessMode.xlExclusive, Nothing, Nothing, Nothing, Nothing, Nothing);//保存为Excl 2007格式   
                //}
                //catch (Exception ex) {
                //    throw ex;
                //}
                //ExclDoc.Close(Nothing, Nothing, Nothing);
                //ExclApp.Quit();
                //GC.Collect();
                #endregion
                #region 方法2
                //string[] strArray = ConfigHelper.GetConfigString("MapColumnName").Split(new char[] { ';' });

                //DataSet dsNew = ds.Copy();
                //if (ds != null && ds.Tables.Count > 0)
                //{
                //    foreach (DataColumn a in ds.Tables[0].Columns)
                //    {
                //        foreach (string str in strArray)
                //        {
                //            if ((str.Trim().Length > 0) && (str.Split(new char[] { '=' }).Length > 1))
                //            {
                //                if (a.ColumnName.Trim() == str.Split('=')[0])
                //                {
                //                    dsNew.Tables[0].Columns[a.ColumnName].ColumnName = str.Split('=')[1];
                //                }
                //            }
                //        }
                //        if (dsNew.Tables[0].Columns.Contains(a.ColumnName))
                //        {
                //            dsNew.Tables[0].Columns.Remove(dsNew.Tables[0].Columns[a.ColumnName]);
                //        }

                //    }
                //}
                //if (dsNew.Tables.Count == 0)
                //{
                //    throw new Exception("DataSet中没有任何可导出的表。");
                //}
                //Excel.Application excelApplication = new Excel.Application();
                //excelApplication.DisplayAlerts = false;
                //Excel.Workbook workbook = excelApplication.Workbooks.Add(System.Reflection.Missing.Value);
                //foreach (DataTable dt in dsNew.Tables)
                //{
                //    Excel.Worksheet lastWorksheet = (Excel.Worksheet)workbook.Worksheets.get_Item(workbook.Worksheets.Count);
                //    Excel.Worksheet newSheet = (Excel.Worksheet)workbook.Worksheets.Add(Type.Missing, lastWorksheet, Type.Missing, Type.Missing);
                //    Excel.Range range = newSheet.Range[newSheet.Cells[1, 1], newSheet.Cells[400, 400]];
                //    newSheet.Name = dt.TableName;
                //    range.NumberFormatLocal = "@";
                //    for (int col = 0; col < dsNew.Tables[0].Columns.Count; col++)
                //    {
                //        newSheet.Cells[1, col + 1] = dsNew.Tables[0].Columns[col].ColumnName;
                //    }
                //    for (int row = 0; row < dt.Rows.Count; row++)
                //    {
                //        for (int col = 0; col < dt.Columns.Count; col++)
                //        {
                //            newSheet.Cells[row + 2, col + 1] = dt.Rows[row][col].ToString();
                //        }
                //    }
                //}
                //((Excel.Worksheet)workbook.Worksheets.get_Item(1)).Delete();
                //((Excel.Worksheet)workbook.Worksheets.get_Item(1)).Delete();
                //((Excel.Worksheet)workbook.Worksheets.get_Item(1)).Delete();
                //try
                //{
                //    workbook.SaveAs(strPath, Excel.XlFileFormat.xlExcel7, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}
                //ZhiFang.Common.Log.Log.Info(strPath);
                //excelApplication.Workbooks.Close();
                //ZhiFang.Common.Log.Log.Info(strPath);
                //excelApplication.Quit();
                //GC.Collect();
                #endregion
                //KillExcel();
                ////ZhiFang.Common.Log.Log.Info(strPath);
                //FileStream fs = new FileStream(strPath, FileMode.Open);
                //ZhiFang.Common.Log.Log.Info(strPath);
                //byte[] buffer = new byte[fs.Length];
                //fs.Read(buffer, 0, buffer.Length);
                //fs.Close();
                //Response.ContentType = "application/ms-excel";
                //Response.Charset = "GB2312";
                //Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(url));
                //Response.OutputStream.Write(buffer, 0, buffer.Length);
                //Response.Flush();
                //HttpContext.Current.ApplicationInstance.CompleteRequest();
                //// ZhiFang.Common.Log.Log.Info(strPath);
                //File.Delete(strPath);
                Response.Write("<script>window.open('../" + ConfigHelper.GetConfigString("SaveExcel") + "/" + url + "', \"Weblis结果导出Excel下载\", \"width=1,height=1,toolbar=no,menubar=no,scrollbars=yes,resizable=yes,location=no,status=no,top=1,left=1\");</script>");
            }
            catch (Exception eee)
            {
                ZhiFang.Common.Log.Log.Debug(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + eee.ToString());
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
        public static void KillExcel()
        {

            Process[] excelProcesses = Process.GetProcessesByName("EXCEL");
            int processId = 0;
            for (int i = 0; i < excelProcesses.Length; i++)
            {
                try
                {
                    if (excelProcesses[processId].HasExited == false)
                    {
                        excelProcesses[i].Kill();
                    }
                }
                catch { }
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