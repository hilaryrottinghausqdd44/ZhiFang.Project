using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.WebLis.Class;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.BLLFactory;

namespace ZhiFang.WebLis.ExportExcel
{
    public partial class SearchCaiWuExcel_DaJia : BasePage
    {
        string Connstr = ConfigurationManager.ConnectionStrings["CaiWuDB"].ToString();
        private readonly IBBusinessLogicClientControl iblcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
        string ClientList = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ZhiFang.WebLis.ExportExcel.SearchCaiWuExcel_DaJia));
            if (!IsPostBack)
            {
                try
                {

                    User u = new User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                    ZhiFang.Common.Log.Log.Info("公司名称：" + u.CompanyName + "用户账号：" + u.Account);
                    System.Web.UI.HtmlControls.HtmlSelect Client = (System.Web.UI.HtmlControls.HtmlSelect)this.Page.FindControl("Client");
                    Model.BusinessLogicClientControl BusinessLogicClientControl = new Model.BusinessLogicClientControl();
                    BusinessLogicClientControl.Account = u.Account;
                    BusinessLogicClientControl.Flag = 1;
                    BusinessLogicClientControl.SelectedFlag = true;
                    DataSet ds = iblcc.GetList(BusinessLogicClientControl);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ClientList += ds.Tables[0].Rows[i]["ClientNo"].ToString() + ",";
                        }
                    }
                    ClientList = ClientList.Remove(ClientList.Length - 1);
                    BindCstatus(ClientList);
                    ZhiFang.Common.Log.Log.Info("Page_Load--" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                }
                catch (Exception ep)
                {
                    ZhiFang.Common.Log.Log.Error(ep.Message + "--" + ep.ToString() + "--" + ep.StackTrace + "--" + ep.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                }
            }
        }
        public void BindCstatus(string ClientList)
        {
            string strSql = "select*from TB_CheckClientAccount where cauditstatus='已审核' ";
            if (ClientList != "")
            {
                strSql += " and cclientno in (" + ClientList + ")";

            }
            try
            {
                DataSet ds = GetSQLtypen(strSql);
                if (ds != null)
                {
                    DataList1.DataSource = ds.Tables[0];
                    DataList1.DataBind();
                }
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    cid.Value = ds.Tables[0].Rows[0]["cid"].ToString();
                    yid.Value = ds.Tables[0].Rows[0]["yname"].ToString();
                    clientName.Value = ds.Tables[0].Rows[0]["cclientname"].ToString();
                    Selcstatus.Value = ds.Tables[0].Rows[0]["cstatus"].ToString();
                    txtcremark.Value = ds.Tables[0].Rows[0]["cremark"].ToString();
                    txtccreatedate.Value = ds.Tables[0].Rows[0]["ccreatedate"].ToString();
                }
                ZhiFang.Common.Log.Log.Info("BindCstatus--" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
            }
            catch (Exception ep)
            {
                ZhiFang.Common.Log.Log.Error(ep.Message + "--" + ep.ToString() + "--" + ep.StackTrace + "--" + ep.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
            }
        }
        public string GetUrl(string strOrderBy)
        {
            try
            {
                if (Request.Url.ToString().IndexOf("OrderField") >= 0)
                {
                    StringBuilder sb = new StringBuilder();
                    string[] arr = Request.Url.ToString().Split('&');
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (arr[i].IndexOf("OrderField") < 0)
                        {
                            sb.Append(arr[i].Trim() + "&");
                        }
                    }
                    ZhiFang.Common.Log.Log.Info("GetUrl--" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                    return sb.ToString() + "OrderField=" + strOrderBy.Trim();
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("GetUrl--" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                    return Request.Url.ToString() + "&OrderField=" + strOrderBy.Trim();
                }

            }
            catch (Exception ep)
            {
                ZhiFang.Common.Log.Log.Error(ep.Message + "--" + ep.ToString() + "--" + ep.StackTrace + "--" + ep.TargetSite + "--'" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "'");
                return null;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetSQLtypen(string sql)
        {
            DataSet dSet = new DataSet();//员工考勤记录
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Connstr;
            try
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
                sda.Fill(dSet);
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("异常信息:" + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
            return dSet;
        }
        /// <summary>
        /// 修改添加，删除
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool GetSQL(string sql)
        {
            bool boo = false;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = Connstr;
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                boo = true;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
            return boo;
        }
        //[AjaxPro.AjaxMethod]
        public string SearchData(string cid)
        {
            string strSql = "select  * from TB_CheckClientAccount where cid= '" + cid + "'";
            DataSet ds = GetSQLtypen(strSql);
            string yname = ds.Tables[0].Rows[0]["yname"].ToString();
            string cclientname = ds.Tables[0].Rows[0]["cclientname"].ToString();
            string cstatus = ds.Tables[0].Rows[0]["cstatus"].ToString();
            string cremark = ds.Tables[0].Rows[0]["cremark"].ToString();
            string ccreatedate = ds.Tables[0].Rows[0]["ccreatedate"].ToString();
            string strList = cid + ";" + yname + ";" + cclientname + ";" + cstatus + ";" + cremark + ";" + ccreatedate;
            return strList;
        }
        protected void butnSearch_Click(object sender, EventArgs e)
        {
            string cyname = AccountDate.Value;
            string ClientName = txtClient.Value;
            string status = Scstatus.Value;
            User u = new User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
            ZhiFang.Common.Log.Log.Info("公司名称：" + u.CompanyName + "用户账号：" + u.Account);
            System.Web.UI.HtmlControls.HtmlSelect Client = (System.Web.UI.HtmlControls.HtmlSelect)this.Page.FindControl("Client");
            Model.BusinessLogicClientControl BusinessLogicClientControl = new Model.BusinessLogicClientControl();
            BusinessLogicClientControl.Account = u.Account;
            BusinessLogicClientControl.Flag = 1;
            BusinessLogicClientControl.SelectedFlag = true;
            DataSet ds1 = iblcc.GetList(BusinessLogicClientControl);
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    ClientList += ds1.Tables[0].Rows[i]["ClientNo"].ToString() + ",";
                }
            }
            ClientList = ClientList.Remove(ClientList.Length - 1);
            string strSql = "select*from TB_CheckClientAccount where cauditstatus='已审核' ";
            if (ClientList != "")
            {
                strSql += " and cclientno in (" + ClientList + ")";

            }
            if (cyname != "")
            {
                strSql += " and yname='" + cyname + "'";
            }

            if (ClientName != "")
            {
                strSql += " and cclientname='" + ClientName + "' ";
            }
            if (status != "")
            {
                strSql += " and cstatus='" + status + "'";
            }
            DataSet ds = GetSQLtypen(strSql);
            if (ds != null)
            {
                DataList1.DataSource = ds.Tables[0];
                DataList1.DataBind();


                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    cid.Value = ds.Tables[0].Rows[0]["cid"].ToString();
                    yid.Value = ds.Tables[0].Rows[0]["yname"].ToString();
                    clientName.Value = ds.Tables[0].Rows[0]["cclientname"].ToString();
                    Selcstatus.Value = ds.Tables[0].Rows[0]["cstatus"].ToString();
                    txtcremark.Value = ds.Tables[0].Rows[0]["cremark"].ToString();
                    txtccreatedate.Value = ds.Tables[0].Rows[0]["ccreatedate"].ToString();
                }
                else
                {

                    cid.Value = "";
                    yid.Value = "";
                    clientName.Value = "";
                    Selcstatus.Value = "";
                    txtcremark.Value = "";
                    txtccreatedate.Value = "";
                }
            }
        }

        protected void linkDownLoadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                string id = this.cidKey.Value;
                string strSql = "select  * from TB_CheckClientAccount where cid= '" + id + "'";
                DataSet ds = GetSQLtypen(strSql);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string strPath = ds.Tables[0].Rows[0]["cfilepath"].ToString();
                    string yname = ds.Tables[0].Rows[0]["yname"].ToString();
                    string cclientname = ds.Tables[0].Rows[0]["cclientname"].ToString();
                    string ExcelPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ExcelPath").ToString();
                    string url = "";
                    string fileName = "";
                    if (ExcelType.Value == "0")
                    {
                        url = ExcelPath + strPath;
                        fileName = yname + cclientname + ".xls";
                    }
                    if (ExcelType.Value == "1")
                    {
                        url = ExcelPath + strPath.Replace(cclientname, cclientname + "项目对帐");
                        fileName = yname + cclientname + "项目对帐.xls";
                    }
                    FileStream fs = new FileStream(url, FileMode.Open);
                    ZhiFang.Common.Log.Log.Info(url);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    fs.Close();
                    Response.ContentType = "application/ms-excel";
                    Response.Charset = "GB2312";
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(fileName));
                    Response.OutputStream.Write(buffer, 0, buffer.Length);
                    Response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("下载文件异常信息:" + ex.ToString());
            }
        }
    }
}