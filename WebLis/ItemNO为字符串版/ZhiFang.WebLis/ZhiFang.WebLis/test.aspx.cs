using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
using ZhiFang.WebLis.Class;
using ZhiFang.IBLL.Common.BaseDictionary;

namespace ZhiFang.WebLis
{
    public partial class test : BasePage
    {
        public string json="";
        private readonly IBShowFrom showform = BLLFactory<IBShowFrom>.GetBLL("ShowFrom");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (base.CheckQueryStringNull("PageSize", "每页记录数未填写！", 0))
            {
                if (base.CheckQueryStringNull("PageIndex", "每页记录数未填写！", 0))
                {
                    ZhiFang.IBLL.Common.BaseDictionary.IBTestItem testitem = BLLFactory<IBTestItem>.GetBLL();
                    DataSet ds = new DataSet();
                    ds = testitem.GetListByPage(new ZhiFang.Model.TestItem(), Convert.ToInt32(base.ReadQueryString("PageIndex")), Convert.ToInt32(base.ReadQueryString("PageSize")));
                    int count = testitem.GetListCount(new ZhiFang.Model.TestItem());
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (count % Convert.ToInt32(base.ReadQueryString("PageSize")) > 0)
                        {
                            json += "{'totalCount':'" + Convert.ToInt32(count / Convert.ToInt32(base.ReadQueryString("PageSize"))) + 1 + "','items':[";
                        }
                        else
                        {
                            json += "{'totalCount':'" + Convert.ToInt32(count / Convert.ToInt32(base.ReadQueryString("PageSize"))) + 1 + "','items':[";
                        }
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            json += "{'sysId':'" + i + "','ItemNo':'" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "','CName':'" + ds.Tables[0].Rows[i]["CName"].ToString().Trim() + "','EName':'" + ds.Tables[0].Rows[i]["EName"].ToString().Trim() + "','ShortName':'" + ds.Tables[0].Rows[i]["ShortName"].ToString().Trim() + "','ShortCode':'" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "'},";
                        }
                        json = json.Substring(0, json.Length - 1);
                        json += "]}";
                    }
                    
                }
            }
        }
    }
}
