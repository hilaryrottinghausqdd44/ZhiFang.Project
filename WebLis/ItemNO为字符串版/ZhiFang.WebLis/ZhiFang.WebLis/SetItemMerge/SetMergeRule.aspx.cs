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

namespace ZhiFang.WebLis.SetItemMerge
{
    public partial class SetMergeRule : BasePage
    {
        private readonly IBItemMergeRule ibimr = BLLFactory<IBItemMergeRule>.GetBLL();
        private readonly IBTestItem ibti = BLLFactory<IBTestItem>.GetBLL();

        ItemMergeRule itemMerge = new ItemMergeRule();
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ZhiFang.WebLis.SetItemMerge.SetMergeRule));
            if (!IsPostBack)
            {
                this.DataList1.DataSource = ibimr.GetList(itemMerge);
                this.DataList1.DataBind();
            }    
        }
        [AjaxPro.AjaxMethod()]
        public string[] ShowItemList(string MergeRuleName, int PageIndex, int PageSize, int PageCol)
        {
            try
            {
                itemMerge.MergeRuleName = MergeRuleName;
                string[] aaa = new string[2];
                if (PageIndex < 0)
                {
                    return new string[2] { "", "" };
                }
                if (PageSize <= 0)
                {
                    return new string[2] { "", "" };
                }
                if (PageCol <= 0)
                {
                    return new string[2] { "", "" };
                }
                DataSet ds = ibimr.GetList(itemMerge);
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
                        DataSet dsItem = ibti.getItemCName(ItemList1);
                        string tr = "";
                        string td = "";
                        if (dsItem.Tables.Count > 0 && dsItem.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                            {
                                if (i % PageCol != 0)
                                {
                                    string cssstr = "border-top:#0099cc solid 0px;border-bottom:#0099cc solid 1px; ";
                                    if ((i + 1) % PageCol != 0)
                                    {
                                        cssstr += "border-right:#0099cc solid 1px;";
                                    }
                                    else
                                    {
                                        cssstr += "border-right:#0099cc solid 0px;";
                                    }
                                    td += "<td align='center' style=\"background-color:#ffffff;width:" + Convert.ToInt32(100 / PageCol) + "%;" + cssstr + "\">" + dsItem.Tables[0].Rows[i]["CName"].ToString().Trim() + "</td>";
                                }
                                else
                                {
                                    tr += "<tr height=\"25\">" + td + "</tr>";
                                    td = "<td align='center'  style=\"background-color:#ffffff;width:" + Convert.ToInt32(100 / PageCol) + "%;border-right:#0099cc solid 1px;border-bottom:#0099cc solid 1px;\">" + dsItem.Tables[0].Rows[i]["CName"].ToString().Trim() + "</td>";
                                }
                            }
                            tr += "<tr height=\"25\">" + td + "</tr>";
                            aaa[0] = "<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"background-color:#0099cc\">" + tr + "</table>";
                        }
                    }
                }
                return aaa;         
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                return new string[2] { "程序运行错误！", "" };
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            DataList1.DataSource = ibimr.GetList(itemMerge);
            DataList1.DataBind();
        }
        //private void SelectSetValueByQuery(string a)
        //{
        //    if (base.CheckQueryString(a))
        //    {
        //        try
        //        {
        //            foreach (ListItem l in DropDownList1.Items)
        //            {
        //                l.Selected = false;
        //                if (l.Value == base.ReadQueryString(a).Trim())
        //                {
        //                    l.Selected = true;
        //                    break;
        //                }
        //            }
        //        }
        //        catch
        //        {

        //        }
        //    }
        //}

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string ClientList = "";
        //        User u = new User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
        //        dsClient = u.GetClientListByPost("", -1);
        //        for (int i = 0; i < dsClient.Tables[0].Rows.Count; i++)
        //        {
        //            ClientList +=  "'"+dsClient.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
        //        }
        //        ClientList = ClientList.Remove(ClientList.Length - 1);
        //        DataSet tmpds = iblcc.GetList(DropDownList2.SelectedValue,ClientList);
        //        for (int i = 0; i < tmpds.Tables[0].Rows.Count; i++)
        //        {
        //            iblcc.Delete(tmpds.Tables[0].Rows[i]["Id"].ToString().Trim());
        //        }
        //        List<Model.ItemMerge> l = new List<Model.ItemMerge>();
               
        //            foreach (string li in ClientList.Split(','))
        //            {
        //                if (this.Clientlist.Value.Trim() != "")
        //                {
        //                    foreach (string hh in this.Clientlist.Value.Trim().Remove(this.Clientlist.Value.Trim().Length - 1).Split(','))
        //                    {
        //                        Model.ItemMerge lcc_m = new Model.ItemMerge() { SuperGroupNo = Convert.ToInt32(DropDownList2.SelectedValue), ClientNo = li.Trim(), ItemNo = hh.Trim() };
        //                        l.Add(lcc_m);
        //                    }
        //                }
        //                else
        //                {
        //                    Model.ItemMerge lcc_m = new Model.ItemMerge() { SuperGroupNo = Convert.ToInt32(DropDownList2.SelectedValue), ClientNo = li.Trim()};
        //                    l.Add(lcc_m);
        //                }
        //            }
                  
        //            if (iblcc.Add(l))
        //            {
        //                Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndClose("操作成功！"));
        //                string ItemList = "";
        //                DataSet dsItem = iblcc.GetList(DropDownList2.SelectedValue, ClientList);
        //                for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
        //                {
        //                    ItemList += " '" + dsItem.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "',";
        //                }
        //                if (ItemList != "")
        //                {
        //                    ItemList = ItemList.Remove(ItemList.Length - 1);
        //                }
        //                DataSet dsSelected = iblcc.GetList(new ZhiFang.Model.ItemMerge() { SuperGroupNo = Convert.ToInt32(DropDownList2.SelectedValue), SelectedFlag = true });
        //                DataSet dsUnSelected = iblcc.GetList(new ZhiFang.Model.ItemMerge() { SuperGroupNo = Convert.ToInt32(DropDownList2.SelectedValue), ItemNo = ItemList, SelectedFlag = false });
        //                if (dsUnSelected != null && dsUnSelected.Tables.Count > 0 && dsUnSelected.Tables[0].Rows.Count > 0)
        //                {
        //                    this.ListBox1.DataSource = dsUnSelected;
        //                    this.ListBox1.DataValueField = "ItemNo";
        //                    this.ListBox1.DataTextField = "CName";
        //                    this.ListBox1.DataBind();
        //                }
        //                if (dsSelected != null && dsSelected.Tables.Count > 0 && dsSelected.Tables[0].Rows.Count > 0)
        //                {
        //                    this.ListBox2.DataSource = dsSelected;
        //                    this.ListBox2.DataValueField = "ItemNo";
        //                    this.ListBox2.DataTextField = "CName";
        //                    this.ListBox2.DataBind();
        //                }
               
        //            }
        //            else
        //            {
        //                Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndClose("操作失败！"));
        //            }
        //    }
        //    catch (Exception ex)
        //    {
        //        ZhiFang.Common.Log.Log.Debug("异常信息" + ex.ToString());
        //    }
        //}
        //[AjaxPro.AjaxMethod]
        //public string  SearchClient(string key)
        //{
        //    string ClientList = "";
        //    string ItemList = "";
        //    User u = new User(ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser"));
        //    dsClient = u.GetClientListByPost("", -1);
        //    for (int i = 0; i < dsClient.Tables[0].Rows.Count; i++)
        //    {
        //        ClientList += " '" + dsClient.Tables[0].Rows[i]["ClientNo"].ToString().Trim() + "',";
        //    }
        //    if (ClientList != "")
        //    {
        //        ClientList = ClientList.Remove(ClientList.Length - 1);
        //    }
        //    DataSet dsItem = iblcc.GetList(key, ClientList);
        //    for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
        //    {
        //        ItemList += " '" + dsItem.Tables[0].Rows[i]["ItemNo"].ToString().Trim() + "',";
        //    }
        //    if (ItemList != "")
        //    {
        //        ItemList = ItemList.Remove(ItemList.Length - 1);
        //    }
        //    DataSet dsSelected = iblcc.GetList(new ZhiFang.Model.ItemMerge() { SuperGroupNo = Convert.ToInt32(key), SelectedFlag = true });
        //    DataSet dsUnSelected = iblcc.GetList(new ZhiFang.Model.ItemMerge() { SuperGroupNo = Convert.ToInt32(key), ItemNo = ItemList, SelectedFlag = false });
        //    //for (int i = 0; i < dsSelected.Tables[0].Rows.Count; i++)
        //    //{
        //    //    this.Clientlist.Value += dsSelected.Tables[0].Rows[i]["ItemNo"].ToString() + ",";
        //    //} 
        //    string aa="";
        //    string b = "";
        //    for (var i = 0; i < dsSelected.Tables[0].Rows.Count; i++)
        //    {
        //        aa += dsSelected.Tables[0].Rows[i]["ItemNo"].ToString() + "," + dsSelected.Tables[0].Rows[i]["CName"].ToString()+";";
        //    }
        //    for (var i = 0; i < dsUnSelected.Tables[0].Rows.Count; i++)
        //    {
        //        b += dsUnSelected.Tables[0].Rows[i]["ItemNo"].ToString() + "," + dsUnSelected.Tables[0].Rows[i]["CName"].ToString()+";"; ;
        //    }
        //    if (aa != "")
        //    {
        //        aa.Substring(0, aa.Length - 1);
        //    }
        //    if (b != "")
        //    {
        //        b.Substring(0, b.Length - 1);
        //    }
        //    string hh = "";
        //    hh=b + "@" + aa;
        //    return hh;
        //}
    }
}