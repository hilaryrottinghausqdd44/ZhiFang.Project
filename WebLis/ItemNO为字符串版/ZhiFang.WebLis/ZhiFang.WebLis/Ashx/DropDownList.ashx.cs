using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Common.BaseDictionary;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Web.Script.Serialization;
using ZhiFang.IBLL.Report;
using ZhiFang.WebLis.Class;
using System.Web.UI.WebControls;

namespace ZhiFang.WebLis.Ashx
{
    /// <summary>
    /// DropDownList 的摘要说明
    /// </summary>
    public class DropDownList : BasePage, IHttpHandler
    {
        IBNRequestForm ibnrf = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
        Model.NRequestForm NRequestForm = new Model.NRequestForm();
        Model.BarCodeForm BarCodeForm = new Model.BarCodeForm();
        Model.ReportFormFull ReportFormFull = new Model.ReportFormFull();
        Model.BusinessLogicClientControl BusinessLogicClientControl = new Model.BusinessLogicClientControl();
        private readonly IBBusinessLogicClientControl iblcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
        private readonly IBReportFormFull ibff = BLLFactory<IBReportFormFull>.GetBLL();
        private readonly IBNRequestItem rib = BLLFactory<IBNRequestItem>.GetBLL("NRequestItem");
        string weblisOrgID = "";

        public void ProcessRequest(HttpContext context)
        {
            IBLL.Common.BaseDictionary.IBCLIENTELE clientele = BLLFactory<IBCLIENTELE>.GetBLL();
            Model.CLIENTELE CLIENTELE = new Model.CLIENTELE();
            string type = context.Request.QueryString["type"];

            ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User();
            try
            {
                string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                user = new ZhiFang.WebLis.Class.User(UserId);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(e.Message + e.TargetSite);
            }
               
            //BusinessLogicClientControl.Account = user.Account;
            //BusinessLogicClientControl.Flag = 1;
            //BusinessLogicClientControl.SelectedFlag = true;
            //DataSet dsLogicClient = iblcc.GetList(BusinessLogicClientControl);
            //NRequestForm.ClientNo = dsLogicClient.Tables[0].Rows[0]["ClientNo"].ToString();
            //weblisOrgID = dsLogicClient.Tables[0].Rows[0]["ClientNo"].ToString();
            DataSet cl_ds = null;
            if (base.CheckCookies("ZhiFangUser"))
            {
                User u = new User(base.ReadCookies("ZhiFangUser"));
                ZhiFang.Common.Log.Log.Info("公司名称：" + u.CompanyName + "用户账号：" + u.Account);
                System.Web.UI.HtmlControls.HtmlSelect Client = (System.Web.UI.HtmlControls.HtmlSelect)this.Page.FindControl("Client");
                cl_ds = u.GetClientListByPost("", -1);
                if (cl_ds != null && cl_ds.Tables.Count > 0 && cl_ds.Tables[0].Rows.Count > 0)
                {
                    
                }
            }

            if (type == "TypeCli")
            {
               
                //cl_ds = clientele.GetAllList();
                if (cl_ds != null && cl_ds.Tables.Count > 0 && cl_ds.Tables[0].Rows.Count > 0)
                {
                    DataTable newdt = new DataTable();
                    DataTable dt = new DataTable();
                    dt = cl_ds.Tables[0];
                    newdt = dt.Clone();
                    DataRow[] drName = dt.Select("", " ClIENTNO asc");
                    for (int j = 0; j < drName.Length; j++)
                    {
                        newdt.ImportRow((DataRow)drName[j]);
                    }
                    dt = newdt;
                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    List<DropDownlist> ls = new List<DropDownlist>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ls.Add(new DropDownlist
                        {
                            id = ds.Tables[0].Rows[i]["ClIENTNO"].ToString(),
                            text = ds.Tables[0].Rows[i]["CName"].ToString()
                        });
                    }
                    context.Response.Write(new JavaScriptSerializer().Serialize(ls));
                }
            }
            else if (type == "TypeList")
            {

                DataTable dt = ReadFormFull(context);
                List<SampleList> ls = new List<SampleList>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ls.Add(new SampleList
                    {
                        BarCode = dt.Rows[i]["BarCode"].ToString(),
                        CName = dt.Rows[i]["CName"].ToString(),
                        Sex = dt.Rows[i]["Sex"].ToString(),
                        TestItemCName = dt.Rows[i]["TestItemCName"].ToString(),
                        DoctorName = dt.Rows[i]["DoctorName"].ToString(),
                        ClientName = dt.Rows[i]["ClientName"].ToString(),
                        SampleName = dt.Rows[i]["SampleName"].ToString(),
                        WebLisSourceOrgName = dt.Rows[i]["WebLisSourceOrgName"].ToString(),
                    });
                }
                List<SampleList> Result = Pagination<SampleList>(context, ls);
                string jsin = JsonConvert.SerializeObject(new { total = ls.Count, rows = Result });
                context.Response.Write(jsin);
            }
            else if (type == "TypeTime")
            {

                //NRequestForm.OperDateStart = inptime1.Value;
                //NRequestForm.OperDateEnd = inptime2.Value;
                BarCodeForm.BarCode = context.Request.Form["BarCode"];
                DataSet dsri = ibnrf.GetListByModel(NRequestForm, BarCodeForm);
                if (dsri != null && dsri.Tables[0] != null && dsri.Tables[0].Rows.Count > 0)
                {
                    NRequestForm.NRequestFormNo = Convert.ToInt64(dsri.Tables[0].Rows[0]["NRequestFormNo"].ToString());
                }
                DataTable dt = ibnrf.GetListBy(NRequestForm).Tables[0];
                List<SampleTime> ls = new List<SampleTime>();
                if (!string.IsNullOrEmpty(BarCodeForm.BarCode) && BarCodeForm.BarCode != "0")
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //var a =new { aaa = "vvv" };
                        
                        ReportFormFull.SERIALNO = BarCodeForm.BarCode;
                        DataSet dsReport = ibff.GetList(ReportFormFull);
                        string ReportTESTTIME = "";
                        string ReportCHECKDATE = "";
                        if (dsReport != null && dsReport.Tables.Count > 0 && dsReport.Tables[0].Rows.Count > 0)
                        {
                            ReportTESTTIME = dsReport.Tables[0].Rows[0]["TESTTIME"].ToString();
                            ReportCHECKDATE = Convert.ToDateTime(dsReport.Tables[0].Rows[0]["CHECKDATE"]).ToShortDateString() + " " + Convert.ToDateTime(dsReport.Tables[0].Rows[0]["CHECKTIME"]).ToLongTimeString();
                        }
                        ls.Add(new SampleTime
                        {
                            OperTime = dt.Rows[0]["OperTime"].ToString(),
                            CollectTime = dt.Rows[0]["CollectTime"].ToString(),
                            incepttime = dt.Rows[0]["incepttime"].ToString(),
                            TESTTIME = ReportTESTTIME,
                            CHECKDATE = ReportCHECKDATE
                        });

                        context.Response.Write(new JavaScriptSerializer().Serialize(ls));
                    }
                    else
                    {
                        context.Response.Write(new JavaScriptSerializer().Serialize(ls));
                    }
                }
                else {
                    ZhiFang.Common.Log.Log.Info("不存在条码号,无法提取样本流转时间！");
                    context.Response.Write(new JavaScriptSerializer().Serialize(ls)); }
            }

        }
        /// <summary>
        /// ReadForm
        /// </summary>
        /// <returns></returns>
        private DataTable ReadFormFull(HttpContext context)
        {
            DataTable dt = new DataTable();
            NRequestForm.ClientNo = context.Request.Form["txtClientNo"];
            //没选择单位就查询全部
            if (string.IsNullOrEmpty(NRequestForm.ClientNo))
            {
                //NRequestForm.ClientNo = weblisOrgID;
            }
            BarCodeForm.BarCode = context.Request.Form["txtBarCode"];
            NRequestForm.CName = context.Request.Form["txtCName"];
            NRequestForm.PatNo = context.Request.Form["txtPatNo"];
            NRequestForm.OperDateStart = context.Request.Form["txtStartDate"];
            NRequestForm.OperDateEnd = context.Request.Form["txtEndDate"];
            DataSet dsri1 = ibnrf.GetListByModel(NRequestForm, BarCodeForm);
            if (!string.IsNullOrEmpty(BarCodeForm.BarCode))
            {
                if (dsri1 != null && dsri1.Tables[0] != null && dsri1.Tables[0].Rows.Count > 0)
                {
                    NRequestForm.NRequestFormNo = Convert.ToInt64(dsri1.Tables[0].Rows[0]["NRequestFormNo"].ToString());

                }
                else
                {
                    return dt;
                }
            }
            ZhiFang.Common.Log.Log.Info("WebLisSourceOrgID:" + NRequestForm.WebLisSourceOrgID + "txtBarCode:" + BarCodeForm.BarCode + "txtCName:" +
                NRequestForm.CName + "txtPatNo:" + NRequestForm.PatNo + "txtStartDate" + NRequestForm.OperDateStart + "txtEndDate:" +
                NRequestForm.OperDateEnd + "NRequestFormNo:" + NRequestForm.NRequestFormNo);
            dt = ibnrf.GetListBy(NRequestForm).Tables[0];
            dt.Columns.Add("TestItemCName");
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    string ItemStr = "";
                    DataSet dsri = rib.GetList(new ZhiFang.Model.NRequestItem() { BarCodeFormNo = long.Parse(dr["BarCodeFormNo"].ToString().Trim()) });
                    if (dsri != null && dsri.Tables[0] != null && dsri.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsri.Tables[0].Rows.Count; i++)
                        {
                            ItemStr += dsri.Tables[0].Rows[i]["CName"].ToString() + ";";
                        }
                        ItemStr = ItemStr.Remove(ItemStr.Length - 1);
                        dr["TestItemCName"] = ItemStr;
                    }
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Info(DateTime.Now.ToString() + ex.Message + ex.StackTrace);
                }
            }
            return dt;
        }

        /// <summary>
        /// List分页
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ls"></param>
        /// <returns></returns>
        private static List<T> Pagination<T>(HttpContext context, List<T> ls)
        {
            int page = Convert.ToInt32(context.Request.Form["page"]);
            int row = Convert.ToInt32(context.Request.Form["rows"]);
            ZhiFang.Common.Log.Log.Info("Page:" + page + "Row:" + row);
            List<T> Result = new List<T>();
            int rowCount = page * row;
            if (ls.Count >= rowCount)
            {
                for (int i = rowCount - row; i < rowCount; i++)
                {
                    ZhiFang.Common.Log.Log.Info("1：" + ls[i].ToString());
                    Result.Add(ls[i]);
                }
            }
            else
            {
                if (ls.Count > rowCount - row)
                {
                    for (int i = rowCount - row; i < ls.Count; i++)
                    {
                        ZhiFang.Common.Log.Log.Info("2：" + ls[i].ToString());
                        Result.Add(ls[i]);
                    }
                }
            }
            return Result;
        }

        public class DropDownlist
        {
            public string id = "";
            public string text = "";

        }
        public class SampleList
        {
            public string BarCode;
            public string CName;
            public string Sex;
            public string TestItemCName;
            public string DoctorName;
            public string WebLisSourceOrgName;
            public string SampleName;
            public string ClientName;
        }
        public class SampleTime
        {
            public string OperTime;
            public string CollectTime;
            public string incepttime;
            public string TESTTIME;
            public string CHECKDATE;
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