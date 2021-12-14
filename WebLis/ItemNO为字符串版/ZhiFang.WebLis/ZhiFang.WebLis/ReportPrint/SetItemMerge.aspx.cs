using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.WebLis.Class;
using System.Data;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.Model;

namespace ZhiFang.WebLis.ReportPrint
{
    public partial class SetItemMerge : BasePage
    {
        private readonly IBItemMergeRule iblcc = BLLFactory<IBItemMergeRule>.GetBLL();
        private readonly IBPGroup ibpg = BLLFactory<IBPGroup>.GetBLL();
        DataSet dsClient = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ListBox1.Attributes.Add("ondblclick", "Selected(document.getElementById('ListBox1'),document.getElementById('ListBox2'),'One');");
            this.ListBox2.Attributes.Add("ondblclick", "Selected(document.getElementById('ListBox2'),document.getElementById('ListBox1'),'One');");
            AjaxPro.Utility.RegisterTypeForAjax(typeof(SetItemMerge));
            if (!IsPostBack)
            {
                #region 送检单位
                if (base.CheckCookies("ZhiFangUser"))
                {


                    User u = new User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                    dsClient = u.GetClientListByPost("", -1);
                    if (dsClient != null && dsClient.Tables.Count > 0 && dsClient.Tables[0].Rows.Count > 0)
                    {
                        DropDownList1.DataSource = dsClient;
                        DropDownList1.DataTextField = "CName";
                        DropDownList1.DataValueField = "ClientNo";
                        DropDownList1.DataBind();
                        DropDownList1.Items.Insert(0, new ListItem("", ""));
                        SelectSetValueByQuery("ClientNo");
                    }
                }
                #endregion
                #region 小    组
                DropDownList2.DataSource = ibpg.GetAllList().Tables[0];
                DropDownList2.DataTextField = "CName";
                DropDownList2.DataValueField = "SuperGroupNo";
                DropDownList2.DataBind();
                #endregion
                string ClientList = "";
                string ItemList = "";
                for (int i = 0; i < dsClient.Tables[0].Rows.Count; i++)
                {
                    ClientList += " '" + dsClient.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
                }
                if (ClientList != "")
                {
                    ClientList = ClientList.Remove(ClientList.Length - 1);
                }
                DataSet dsItem = iblcc.GetList(DropDownList2.SelectedValue, ClientList);
                for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                {
                    ItemList += " '" + dsItem.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "',";
                }
                if (ItemList != "")
                {
                    ItemList = ItemList.Remove(ItemList.Length - 1);
                }
                DataSet dsSelected = iblcc.GetList(new ZhiFang.Model.ItemMergeRule() {  SelectedFlag = true });
                DataSet dsUnSelected = iblcc.GetList(new ZhiFang.Model.ItemMergeRule() {ItemNo=ItemList, SelectedFlag = false });
                if (dsUnSelected != null && dsUnSelected.Tables.Count > 0 && dsUnSelected.Tables[0].Rows.Count > 0)
                {
                    this.ListBox1.DataSource = dsUnSelected;
                    this.ListBox1.DataValueField = "ItemNo";
                    this.ListBox1.DataTextField = "CName";
                    this.ListBox1.DataBind();
                }
                if (dsSelected != null && dsSelected.Tables.Count > 0 && dsSelected.Tables[0].Rows.Count > 0)
                {
                    this.ListBox2.DataSource = dsSelected;
                    this.ListBox2.DataValueField = "ItemNo";
                    this.ListBox2.DataTextField = "CName";
                    this.ListBox2.DataBind();
                    for (int i = 0; i < dsSelected.Tables[0].Rows.Count; i++)
                    {
                        this.Clientlist.Value += dsSelected.Tables[0].Rows[i]["ItemNo"].ToString() + ",";
                    } 
                }
               
            }
             
        }
        private void SelectSetValueByQuery(string a)
        {
            if (base.CheckQueryString(a))
            {
                try
                {
                    foreach (ListItem l in DropDownList1.Items)
                    {
                        l.Selected = false;
                        if (l.Value == base.ReadQueryString(a).Trim())
                        {
                            l.Selected = true;
                            break;
                        }
                    }
                }
                catch
                {

                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string ClientList = "";
                User u = new User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
                dsClient = u.GetClientListByPost("", -1);
                for (int i = 0; i < dsClient.Tables[0].Rows.Count; i++)
                {
                    ClientList +=  "'"+dsClient.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
                }
                ClientList = ClientList.Remove(ClientList.Length - 1);
                DataSet tmpds = iblcc.GetList(DropDownList2.SelectedValue,ClientList);
                for (int i = 0; i < tmpds.Tables[0].Rows.Count; i++)
                {
                    iblcc.Delete(tmpds.Tables[0].Rows[i]["Id"].ToString().Trim());
                }
                List<Model.ItemMergeRule> l = new List<Model.ItemMergeRule>();
               
                    foreach (string li in ClientList.Split(','))
                    {
                        if (this.Clientlist.Value.Trim() != "")
                        {
                            foreach (string hh in this.Clientlist.Value.Trim().Remove(this.Clientlist.Value.Trim().Length - 1).Split(','))
                            {
                                Model.ItemMergeRule lcc_m = new Model.ItemMergeRule() {ItemNo = hh.Trim() };
                                l.Add(lcc_m);
                            }
                        }
                        else
                        {
                            Model.ItemMergeRule lcc_m = new Model.ItemMergeRule() {};
                            l.Add(lcc_m);
                        }
                    }
                  
                    if (iblcc.Add(l))
                    {
                        Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndClose("操作成功！"));
                        string ItemList = "";
                        DataSet dsItem = iblcc.GetList(DropDownList2.SelectedValue, ClientList);
                        for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                        {
                            ItemList += " '" + dsItem.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "',";
                        }
                        if (ItemList != "")
                        {
                            ItemList = ItemList.Remove(ItemList.Length - 1);
                        }
                        DataSet dsSelected = iblcc.GetList(new ZhiFang.Model.ItemMergeRule() {  SelectedFlag = true });
                        DataSet dsUnSelected = iblcc.GetList(new ZhiFang.Model.ItemMergeRule() {  ItemNo = ItemList, SelectedFlag = false });
                        if (dsUnSelected != null && dsUnSelected.Tables.Count > 0 && dsUnSelected.Tables[0].Rows.Count > 0)
                        {
                            this.ListBox1.DataSource = dsUnSelected;
                            this.ListBox1.DataValueField = "ItemNo";
                            this.ListBox1.DataTextField = "CName";
                            this.ListBox1.DataBind();
                        }
                        if (dsSelected != null && dsSelected.Tables.Count > 0 && dsSelected.Tables[0].Rows.Count > 0)
                        {
                            this.ListBox2.DataSource = dsSelected;
                            this.ListBox2.DataValueField = "ItemNo";
                            this.ListBox2.DataTextField = "CName";
                            this.ListBox2.DataBind();
                        }
               
                    }
                    else
                    {
                        Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndClose("操作失败！"));
                    }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("异常信息" + ex.ToString());
            }
        }
        [AjaxPro.AjaxMethod]
        public string  SearchClient(string key)
        {
            string ClientList = "";
            string ItemList = "";
            User u = new User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
            dsClient = u.GetClientListByPost("", -1);
            for (int i = 0; i < dsClient.Tables[0].Rows.Count; i++)
            {
                ClientList += " '" + dsClient.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
            }
            if (ClientList != "")
            {
                ClientList = ClientList.Remove(ClientList.Length - 1);
            }
            DataSet dsItem = iblcc.GetList(key, ClientList);
            for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
            {
                ItemList += " '" + dsItem.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "',";
            }
            if (ItemList != "")
            {
                ItemList = ItemList.Remove(ItemList.Length - 1);
            }
            DataSet dsSelected = iblcc.GetList(new ZhiFang.Model.ItemMergeRule() { SelectedFlag = true });
            DataSet dsUnSelected = iblcc.GetList(new ZhiFang.Model.ItemMergeRule() { ItemNo = ItemList, SelectedFlag = false });
            //for (int i = 0; i < dsSelected.Tables[0].Rows.Count; i++)
            //{
            //    this.Clientlist.Value += dsSelected.Tables[0].Rows[i]["ItemNo"].ToString() + ",";
            //} 
            string aa="";
            string b = "";
            for (var i = 0; i < dsSelected.Tables[0].Rows.Count; i++)
            {
                aa += dsSelected.Tables[0].Rows[i]["ItemNo"].ToString() + "," + dsSelected.Tables[0].Rows[i]["CName"].ToString()+";";
            }
            for (var i = 0; i < dsUnSelected.Tables[0].Rows.Count; i++)
            {
                b += dsUnSelected.Tables[0].Rows[i]["ItemNo"].ToString() + "," + dsUnSelected.Tables[0].Rows[i]["CName"].ToString()+";"; ;
            }
            if (aa != "")
            {
                aa.Substring(0, aa.Length - 1);
            }
            if (b != "")
            {
                b.Substring(0, b.Length - 1);
            }
            string hh = "";
            hh=b + "@" + aa;
            return hh;
        }
    }
}