using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Common.BaseDictionary;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Text;

namespace ZhiFang.WebLis.ApplyInput
{
    public partial class SubscriptList_Newdajia : ZhiFang.WebLis.Class.BasePage 
    {
        protected string loginID = "";  //登录用户编号
        protected string loginName = "";  //登录用户姓名     
        ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User();
        IBLL.Common.BaseDictionary.IBCLIENTELE clientele = BLLFactory<IBCLIENTELE>.GetBLL();
        Model.CLIENTELE client_m = new Model.CLIENTELE();
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ZhiFang.WebLis.Ashx.ApplyNewInput));
            IBLL.Common.BaseDictionary.IBBusinessLogicClientControl blcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
            //if (!base.CheckCookies("ZhiFangUser"))
            //{
            //    string alertStr = "未登录，请登陆后继续！";
            //    ZhiFang.Common.Public.ScriptStr.Alert(alertStr);
            //    return;
            //}
            if (!IsPostBack)
            {
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("DefaultClientFlag") == "1")
                {
                    DataSet ds = user.GetClientListByPost("", -1);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            System.Web.UI.HtmlControls.HtmlInputText hittcn = (System.Web.UI.HtmlControls.HtmlInputText)Page.FindControl("inputName");
                            hittcn.Attributes.Remove("onfocus");
                            hittcn.Attributes.Remove("onkeydown");
                            hittcn.Attributes.Remove("onpropertychange");
                            hittcn.Attributes.Remove("onblur");
                            hittcn.Attributes.Add("readonly", "readonly");
                            hittcn.Attributes.Add("disabled", "disabled");
                            hittcn.Attributes.Add("value", ds.Tables[0].Rows[0]["CName"].ToString().Trim());
                            //hiddenClient.Value = dt.Rows[0]["WebLisSourceOrgID"].ToString().Trim();
                        }
                    }
                }
                //判断是否手动输入送检单位
                if (Request["k"] != null)
                {
                    string strVal = Server.UrlDecode(Request["q"]);
                    if (strVal != null)
                    {
                        BindA(strVal);
                    }
                    else
                    { BindB(); }
                }
            }
        }
        #region  过滤送检单位两种方法
        public void BindA(string strVal)
        {
            client_m.CNAME = strVal;
            DataSet dt = clientele.GetList(client_m);
            if (dt.Tables[0].Rows.Count > 0)
            {
                string json = "{";
                json = DataTableJson(dt.Tables[0]);

                Response.ContentType = "application/json";
                Response.ContentType = "text/plain";
                Response.Write(json);
                Response.End();
            }
            else
            {
                Response.Write("{[{\"CLIENTNO\":\"0\",\"CNAME\":\"没有找到相应数据！\"}]}");
                Response.End();
            }
        }
        public void BindB()
        {
            DataSet dt = clientele.GetAllList();
            if (dt != null && dt.Tables[0].Rows.Count > 0)
            {
                string json = "{";
                json = DataTableJson(dt.Tables[0]);

                Response.ContentType = "application/json";
                Response.ContentType = "text/plain";
                Response.Write(json);
                Response.End();
            }
            else
            {
                Response.Write("{[{\"CLIENTNO\":\"0\",\"CNAME\":\"没有找到相应数据！\"}]}");
                Response.End();
            }
        }
        #endregion

        /// <summary>     
        /// dataTable转换成Json格式(后期可提取)    
        /// </summary>     
        /// <param name="dt"></param>     
        /// <returns></returns>     
        public static string DataTableJson(DataTable dt)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{");
            jsonBuilder.Append("[");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                jsonBuilder.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(dt.Columns[j].ColumnName);
                    jsonBuilder.Append("\":\"");
                    jsonBuilder.Append(dt.Rows[i][j].ToString());
                    jsonBuilder.Append("\",");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
    }
}