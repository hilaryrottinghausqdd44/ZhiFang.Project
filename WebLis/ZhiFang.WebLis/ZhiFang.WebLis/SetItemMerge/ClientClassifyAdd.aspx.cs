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
    public partial class ClientClassifyAdd : BasePage
    {
        private readonly IBClientProfile ibcp = BLLFactory<IBClientProfile>.GetBLL();
        private readonly IBItemMergeRule ibimr = BLLFactory<IBItemMergeRule>.GetBLL();
        private readonly IBCLIENTELE ibc = BLLFactory<IBCLIENTELE>.GetBLL();
        Model.ItemMergeRule ItemMergeRule = new Model.ItemMergeRule();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                Model.ClientProfile ClientProfile = new Model.ClientProfile();
                Model.CLIENTELE Clientele=new Model.CLIENTELE();
                DataSet ds = ibcp.GetList(ClientProfile);
                this.dropMergeRuleName.DataSource = ibimr.GetList(ItemMergeRule);
                this.dropMergeRuleName.DataTextField = "MergeRuleName";
                this.dropMergeRuleName.DataValueField = "MergeRuleName";
                this.dropMergeRuleName.DataBind();
                this.dropMergeRuleName.Items.Insert(0, new ListItem("", ""));
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ClientList1 = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        ClientList1 += "'" + ds.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
                    }
                    if (ClientList1 != "")
                    {
                        ClientList1 = ClientList1.Remove(ClientList1.Length - 1);
                    }
                    DataSet dsClient = ibc.GetClientNo(ClientList1, "");
                    if (dsClient != null && dsClient.Tables[0].Rows.Count > 0)
                    {
                        this.ListBox1.DataSource = dsClient;
                        this.ListBox1.DataValueField = "ClIENTNO";
                        this.ListBox1.DataTextField = "CName";
                        this.ListBox1.DataBind();
                    }
                }
                if (base.CheckQueryString("ClientProfileCName"))
                {

                    Model.ClientProfile ClientProfile1 = new Model.ClientProfile();
                    ClientProfile.ClientProfileCName = base.ReadQueryString("ClientProfileCName").Trim();
                    txtClientProfileCName.Text = ClientProfile.ClientProfileCName;
                    DataSet ds1 = ibcp.GetList(ClientProfile);
                    if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                    {
                        this.ListBox2.DataSource = ds1;
                        this.ListBox2.DataValueField = "ClIENTNO";
                        this.ListBox2.DataTextField = "CName";
                        this.ListBox2.DataBind();
                        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                            this.Clientlist.Value += ds1.Tables[0].Rows[i]["ClIENTNO"].ToString() + ",";
                        }
                    }
                    this.dropMergeRuleName.SelectedValue = ds1.Tables[0].Rows[0]["MergeRuleName"].ToString();
                }
                else
                {
                  

                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
             
                if (base.CheckQueryString("ClientProfileCName"))
                {
                    List<Model.ClientProfile> l = new List<Model.ClientProfile>();
                    string ClientProfileCName = txtClientProfileCName.Text;
                    string MergeRuleName = dropMergeRuleName.Text;
                    foreach (string hh in this.Clientlist.Value.Trim().Remove(this.Clientlist.Value.Trim().Length - 1).Split(','))
                    {
                        Model.ClientProfile lcc_m = new Model.ClientProfile() { MergeRuleName = MergeRuleName,ProfileCName = base.ReadQueryString("ClientProfileCName"), ClientProfileCName = ClientProfileCName, ClientNo = Convert.ToInt32(hh.Trim()) };
                        l.Add(lcc_m);
                    }
                    if (ibcp.AddList(l))
                    {
                        Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("操作成功！", "opener.location.href=opener.location.href"));
                        Model.ClientProfile ClientProfile = new Model.ClientProfile();
                        Model.CLIENTELE Clientele = new Model.CLIENTELE();
                        DataSet ds = ibcp.GetList(ClientProfile);
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            string ClientList = "";
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ClientList += "'" + ds.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
                            }
                            if (ClientList != "")
                            {
                                ClientList = ClientList.Remove(ClientList.Length - 1);
                            }
                            DataSet dsClient = ibc.GetClientNo(ClientList,"");
                            if (dsClient != null && dsClient.Tables[0].Rows.Count > 0)
                            {
                                this.ListBox1.DataSource = dsClient;
                                this.ListBox1.DataValueField = "ClIENTNO";
                                this.ListBox1.DataTextField = "CName";
                                this.ListBox1.DataBind();
                            }
                            Model.ClientProfile ClientProfile1 = new Model.ClientProfile();
                            ClientProfile.ClientProfileCName = base.ReadQueryString("ClientProfileCName").Trim();
                            DataSet ds1 = ibcp.GetList(ClientProfile);
                            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                            {
                                this.ListBox2.DataSource = ds1;
                                this.ListBox2.DataValueField = "ClIENTNO";
                                this.ListBox2.DataTextField = "CName";
                                this.ListBox2.DataBind();
                            }
                        }
                    }
                    else
                    {
                        Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("操作失败！", "opener.location.href=opener.location.href"));
                    }
                }
                else
                {
                    string MergeRuleName = dropMergeRuleName.Text;
                    List<Model.ClientProfile> l = new List<Model.ClientProfile>();
                    string ClientProfileCName = txtClientProfileCName.Text;
                    foreach (string hh in this.Clientlist.Value.Trim().Remove(this.Clientlist.Value.Trim().Length - 1).Split(','))
                    {
                        Model.ClientProfile lcc_m = new Model.ClientProfile() {MergeRuleName=MergeRuleName,ClientProfileCName = ClientProfileCName, ClientNo = Convert.ToInt32(hh.Trim()) };
                        l.Add(lcc_m);
                    }
                    if (ibcp.Add(l))
                    {
                        Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("操作成功！", "opener.location.href=opener.location.href"));
                        Model.ClientProfile ClientProfile = new Model.ClientProfile();
                        Model.CLIENTELE Clientele = new Model.CLIENTELE();
                        DataSet ds = ibcp.GetList(ClientProfile);
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            string ClientList = "";
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                ClientList += "'" + ds.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
                            }
                            if (ClientList != "")
                            {
                                ClientList = ClientList.Remove(ClientList.Length - 1);
                            }
                            DataSet dsClient = ibc.GetClientNo(ClientList, "");
                            if (dsClient != null && ds.Tables[0].Rows.Count > 0)
                            {
                                this.ListBox1.DataSource = dsClient;
                                this.ListBox1.DataValueField = "ClIENTNO";
                                this.ListBox1.DataTextField = "CName";
                                this.ListBox1.DataBind();
                            }
                            Model.ClientProfile ClientProfile1 = new Model.ClientProfile();
                            ClientProfile.ClientProfileCName = base.ReadQueryString("ClientProfileCName").Trim();
                            DataSet ds1 = ibcp.GetList(ClientProfile);
                            if (ds1 != null && ds.Tables[0].Rows.Count > 0)
                            {
                                this.ListBox2.DataSource = ds1;
                                this.ListBox2.DataValueField = "ClIENTNO";
                                this.ListBox2.DataTextField = "CName";
                                this.ListBox2.DataBind();
                            }
                        }

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
        protected void Button2_Click(object sender, EventArgs e)
        {
            Model.CLIENTELE clientele = new Model.CLIENTELE();
            string ClientCName = txtClientName.Text.Trim();
            Model.ClientProfile ClientProfile = new Model.ClientProfile();
            Model.CLIENTELE Clientele = new Model.CLIENTELE();
            DataSet ds = ibcp.GetList(ClientProfile);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string ClientList1 = "";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ClientList1 += "'" + ds.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
                }
                if (ClientList1 != "")
                {
                    ClientList1 = ClientList1.Remove(ClientList1.Length - 1);
                }
                DataSet dsClient = ibc.GetClientNo(ClientList1, ClientCName);
                //dsClient.Tables[0].Select("CName like '%" + ClientCName + "%'");
                if (dsClient != null && dsClient.Tables[0].Rows.Count > 0)
                {
                    this.ListBox1.DataSource = dsClient;
                    this.ListBox1.DataValueField = "ClIENTNO";
                    this.ListBox1.DataTextField = "CName";
                    this.ListBox1.DataBind();
                }
                 
            }
        }

    }
}