using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Report;
using ZhiFang.WebLis.Class;
using ZhiFang.IBLL.Common.BaseDictionary;


namespace ZhiFang.WebLis.ReportPrint
{
    public partial class PGroupModelSet_Weblis : BasePage
    {
        private readonly IBPGroupPrint pgpb = BLLFactory<IBPGroupPrint>.GetBLL();
        protected void Page_Load(object sender, EventArgs e) 
        {
            DataList1.DataSource = pgpb.GetList_No_Name(new ZhiFang.Model.PGroupPrint());
            DataList1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int id = -1;
            try
            {
                id = Convert.ToInt32(tmpId.Text.Trim());
            } 
            catch
            {
                id = -1;
            }
            pgpb.Delete(tmpId.Text.Trim());
            DataList1.DataSource = pgpb.GetList_No_Name(new ZhiFang.Model.PGroupPrint());
            DataList1.DataBind();
        }
    }
}
