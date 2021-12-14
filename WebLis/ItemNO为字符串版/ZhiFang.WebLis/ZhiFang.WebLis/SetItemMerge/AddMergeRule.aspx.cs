using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.WebLis.Class;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.BLLFactory;
using System.Data;
using ZhiFang.IBLL.Report;

namespace ZhiFang.WebLis.SetItemMerge
{
    public partial class AddMergeRule : BasePage
    {
        private readonly IBPGroup ibimr = BLLFactory<IBPGroup>.GetBLL();
        private readonly IBItemMergeRule ibim = BLLFactory<IBItemMergeRule>.GetBLL();
        private readonly IBTestItem ibt = BLLFactory<IBTestItem>.GetBLL();
        Model.PGroup pgroup = new Model.PGroup();
        Model.TestItem testItem = new Model.TestItem();
        Model.ItemMergeRule ItemMergeRule = new Model.ItemMergeRule();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet dsSection = ibimr.GetAllList();
                dropSectionType.DataSource = dsSection;
                dropSectionType.DataTextField = "CName";
                dropSectionType.DataValueField = "SectionNo";
                dropSectionType.DataBind();
                if (base.CheckQueryString("MergeRuleName"))
                {
                    ItemMergeRule.MergeRuleName = base.ReadQueryString("MergeRuleName").Trim();
                    txtItemMergeCName.Text = ItemMergeRule.MergeRuleName;
                    DataSet ds1 = ibim.GetList(ItemMergeRule);
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        string ItemList1 = "";
                        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                            ItemList1 += "'" + ds1.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "',";
                        }
                        if (ItemList1 != "")
                        {
                            ItemList1 = ItemList1.Remove(ItemList1.Length - 1);
                            DataSet dsItem1 = ibt.getItemCName(ItemList1);
                            if (dsItem1 != null && dsItem1.Tables[0].Rows.Count > 0)
                            {
                                this.ListBox2.DataSource = dsItem1;
                                this.ListBox2.DataValueField = "ItemNo";
                                this.ListBox2.DataTextField = "CName";
                                this.ListBox2.DataBind();
                                for (int i = 0; i < dsItem1.Tables[0].Rows.Count; i++)
                                {
                                    this.Itemlist.Value += dsItem1.Tables[0].Rows[i]["ItemNo"].ToString() + ",";
                                }
                            }
                            string SectionNo = ds1.Tables[0].Rows[0]["SectionNo"].ToString();
                            dropSectionType.SelectedValue = SectionNo;
                            testItem.SuperGroupNo = Convert.ToInt32(dsSection.Tables[0].Rows[0]["SuperGroupNo"].ToString());
                            testItem.ItemList = ItemList1;
                            DataSet dsItem = ibt.GetList(testItem);
                            if (dsItem != null && dsItem.Tables[0].Rows.Count>0)
                            {
                                this.ListBox1.DataSource = dsItem;
                                this.ListBox1.DataValueField = "ItemNo";
                                this.ListBox1.DataTextField = "CName";
                                this.ListBox1.DataBind();
                            }
                        }
                    }
                }
                else
                {
                   
                    testItem.SuperGroupNo = Convert.ToInt32(dsSection.Tables[0].Rows[0]["SuperGroupNo"].ToString());
                    DataSet dsItem = ibt.GetList(testItem);
                    if (dsItem != null && dsItem.Tables[0].Rows.Count > 0)
                    {
                        this.ListBox1.DataSource = dsItem;
                        this.ListBox1.DataValueField = "ItemNo";
                        this.ListBox1.DataTextField = "CName";
                        this.ListBox1.DataBind();
                         
                    }
                }
            }
        }

        protected void dropSectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sectionNo = dropSectionType.Text;
            pgroup.SectionNo = Convert.ToInt32(sectionNo);
            DataSet dsGropNo = ibimr.GetList(pgroup);
            testItem.SuperGroupNo = Convert.ToInt32(dsGropNo.Tables[0].Rows[0]["SuperGroupNo"].ToString());
            DataSet dsItem = ibt.GetList(testItem);
            if (dsItem != null && dsItem.Tables[0].Rows.Count > 0)
            {
                this.ListBox1.DataSource = dsItem;
                this.ListBox1.DataValueField = "ItemNo";
                this.ListBox1.DataTextField = "CName";
                this.ListBox1.DataBind();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string sectionNo = dropSectionType.Text;
            pgroup.SectionNo = Convert.ToInt32(sectionNo);
            DataSet dsGropNo = ibimr.GetList(pgroup);
            testItem.SuperGroupNo = Convert.ToInt32(dsGropNo.Tables[0].Rows[0]["SuperGroupNo"].ToString());
            testItem.CName = txtItemCName.Text;
            DataSet dsItem = ibt.GetList(testItem);
            if (dsItem != null && dsItem.Tables[0].Rows.Count > 0)
            {
                this.ListBox1.DataSource = dsItem;
                this.ListBox1.DataValueField = "ItemNo";
                this.ListBox1.DataTextField = "CName";
                this.ListBox1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
          
            try
            {

                if (base.CheckQueryString("MergeRuleName"))
                {
                    List<Model.ItemMergeRule> l = new List<Model.ItemMergeRule>();
                    string itemMergeCName = txtItemMergeCName.Text;
                    string sectionNo = dropSectionType.Text;
                    pgroup.SectionNo = Convert.ToInt32(sectionNo);
                    DataSet dsGropNo = ibimr.GetList(pgroup);
                    string SuperGroupNo = dsGropNo.Tables[0].Rows[0]["SuperGroupNo"].ToString();
                    foreach (string hh in this.Itemlist.Value.Trim().Remove(this.Itemlist.Value.Trim().Length - 1).Split(','))
                    {
                        Model.ItemMergeRule lcc_m = new Model.ItemMergeRule() { itemMergeCName = base.ReadQueryString("MergeRuleName"), MergeRuleName = itemMergeCName, ItemNo = hh.Trim(), SectionNo = Convert.ToInt32(pgroup.SectionNo) };
                        l.Add(lcc_m);
                    }
                    if (ibim.AddList(l))
                    {
                        Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("操作成功！", "opener.location.href=opener.location.href"));
                   
                    }
                    else
                    {
                        Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("操作失败！", "opener.location.href=opener.location.href"));
                    }
                }
                else
                {
                    List<Model.ItemMergeRule> l = new List<Model.ItemMergeRule>();
                    string itemMergeCName = txtItemMergeCName.Text;
                    string sectionNo = dropSectionType.Text;
                    pgroup.SectionNo = Convert.ToInt32(sectionNo);
                    DataSet dsGropNo = ibimr.GetList(pgroup);
                    string SuperGroupNo = dsGropNo.Tables[0].Rows[0]["SuperGroupNo"].ToString();
                    foreach (string hh in this.Itemlist.Value.Trim().Remove(this.Itemlist.Value.Trim().Length - 1).Split(','))
                    {
                        Model.ItemMergeRule lcc_m = new Model.ItemMergeRule() { MergeRuleName = itemMergeCName, ItemNo = hh.Trim(), SectionNo = Convert.ToInt32(sectionNo) };
                        l.Add(lcc_m);
                    }
                    if (ibim.Add(l))
                    {
                           Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("操作成功！", "opener.location.href=opener.location.href"));
                           ItemMergeRule.MergeRuleName = itemMergeCName;
                           DataSet ds = ibim.GetList(ItemMergeRule);
                           if (ds != null && ds.Tables[0].Rows.Count > 0)
                           {
                               string ItemList1 = "";
                               for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                               {
                                   ItemList1 += "'" + ds.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "',";
                               }
                               if (ItemList1 != "")
                               {
                                   ItemList1 = ItemList1.Remove(ItemList1.Length - 1);
                                   DataSet dsItem = ibt.getItemCName(ItemList1);
                                   if (dsItem.Tables.Count > 0 && dsItem.Tables[0].Rows.Count > 0)
                                   {
                                       this.ListBox2.DataSource = dsItem;
                                       this.ListBox2.DataValueField = "ItemNo";
                                       this.ListBox2.DataTextField = "CName";
                                       this.ListBox2.DataBind();
                                   }
                               }
                           }
                        //Model.ClientProfile ClientProfile = new Model.ClientProfile();
                        //Model.CLIENTELE Clientele = new Model.CLIENTELE();
                        //DataSet ds = ibcp.GetList(ClientProfile);
                        //if (ds != null && ds.Tables[0].Rows.Count > 0)
                        //{
                        //    string ClientList = "";
                        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        //    {
                        //        ClientList += "'" + ds.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
                        //    }
                        //    if (ClientList != "")
                        //    {
                        //        ClientList = ClientList.Remove(ClientList.Length - 1);
                        //    }
                        //    DataSet dsClient = ibc.GetClientNo(ClientList, "");
                        //    if (dsClient != null && ds.Tables[0].Rows.Count > 0)
                        //    {
                        //        this.ListBox1.DataSource = dsClient;
                        //        this.ListBox1.DataValueField = "ClIENTNO";
                        //        this.ListBox1.DataTextField = "CName";
                        //        this.ListBox1.DataBind();
                        //    }
                        //    Model.ClientProfile ClientProfile1 = new Model.ClientProfile();
                        //    ClientProfile.ClientProfileCName = base.ReadQueryString("ClientProfileCName").Trim();
                        //    DataSet ds1 = ibcp.GetList(ClientProfile);
                        //    if (ds1 != null && ds.Tables[0].Rows.Count > 0)
                        //    {
                        //        this.ListBox2.DataSource = ds1;
                        //        this.ListBox2.DataValueField = "ClIENTNO";
                        //        this.ListBox2.DataTextField = "CName";
                        //        this.ListBox2.DataBind();
                        //    }
                        //}

                    }
                    else
                    {
                        Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("操作失败！", "opener.location.href=opener.location.href"));
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("异常信息" + ex.ToString());
            }

        }
        //protected void dropSectionType_TextChanged(object sender, EventArgs e)
        //{
        //    string sectionNo = dropSectionType.Text;
        //    pgroup.SectionNo = Convert.ToInt32(sectionNo);
        //    DataSet dsGropNo = ibimr.GetList(pgroup);
        //    testItem.SuperGroupNo = Convert.ToInt32(dsGropNo.Tables[0].Rows[0]["SuperGroupNo"].ToString());
        //    DataSet dsItem = ibt.GetList(testItem);
        //    if (dsItem != null && dsItem.Tables[0].Rows.Count > 0)
        //    {
        //        this.ListBox1.DataSource = dsItem;
        //        this.ListBox1.DataValueField = "ItemNo";
        //        this.ListBox1.DataTextField = "CName";
        //        this.ListBox1.DataBind();
        //    }
        //}
    }
}