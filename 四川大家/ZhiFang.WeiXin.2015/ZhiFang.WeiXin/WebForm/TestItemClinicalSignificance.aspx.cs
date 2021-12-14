using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.WeiXin.IBLL;
using ZhiFang.WeiXin.Entity;

namespace ZhiFang.WeiXin
{
    public partial class TestItemClinicalSignificance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (Request.QueryString["ItemShortCode"] == null || Request.QueryString["ItemShortCode"].Trim() == "")
            {
                Response.Write("参数错误！请重新加载！");
                Response.End();
            }
            Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
            var bbtics = context.GetObject("BBTestItemClinicalSignificance") as IBBTestItemClinicalSignificance;
            var bbticslist=bbtics.SearchListByHQL("  StandCode='" + Request.QueryString["ItemShortCode"].Trim() + "' ");
            if (bbticslist != null && bbticslist.Count > 0)
            {
                BTestItemClinicalSignificance btics = bbticslist[0];
                if (btics.CName != null && btics.CName.Trim() != "")
                {
                    this.labItemName.Text = btics.CName.Trim() + "(" + Request.QueryString["ItemShortCode"].Trim() + ")";
                }
                if (btics.BaseSignificance != null && btics.BaseSignificance.Trim() != "")
                {
                    this.LabBaseSignificance.Text = btics.BaseSignificance.Trim() ;
                }
                if (btics.HighSignificance != null && btics.HighSignificance.Trim() != "")
                {
                    this.LabHighSignificance.Text = btics.HighSignificance.Trim();
                }
                if (btics.LowSignificance != null && btics.LowSignificance.Trim() != "")
                {
                    this.LabLowSignificance.Text = btics.LowSignificance.Trim() ;
                }
                if (btics.SuperHighSignificance != null && btics.SuperHighSignificance.Trim() != "")
                {
                    this.LabSuperHighSignificance.Text = btics.SuperHighSignificance.Trim();
                }
                if (btics.SuperLowSignificance != null && btics.SuperLowSignificance.Trim() != "")
                {
                    this.LabSuperLowSignificance.Text = btics.SuperLowSignificance.Trim();
                }
                if (btics.OtherSignificance != null && btics.OtherSignificance.Trim() != "")
                {
                    this.LabOtherSignificance.Text = btics.OtherSignificance.Trim();
                }
                if (btics.MorphologySignificance != null && btics.MorphologySignificance.Trim() != "")
                {
                    this.LabMorphologySignificance.Text = btics.MorphologySignificance.Trim();
                }
                if (btics.Comment != null && btics.Comment.Trim() != "")
                {
                    this.LabComment.Text = btics.Comment.Trim();
                }
            }
        }
    }
}