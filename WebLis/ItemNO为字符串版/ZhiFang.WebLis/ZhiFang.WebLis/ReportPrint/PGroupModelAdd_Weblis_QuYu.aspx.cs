using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Report;
using System.Data;
using ZhiFang.WebLis.Class;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.Common.Public;
using ZhiFang.IBLL.Common;

namespace ZhiFang.WebLis.ReportPrint
{
    public partial class PGroupModelAdd_Weblis_QuYu : BasePage
    {
        IBPGroup pg = ZhiFang.BLLFactory.BLLFactory<IBPGroup>.GetBLL();
        IBLab_PGroup lpg = ZhiFang.BLLFactory.BLLFactory<IBLab_PGroup>.GetBLL();
        IBPrintFormat pgf = ZhiFang.BLLFactory.BLLFactory<IBPrintFormat>.GetBLL();
        IBPGroupControl ibpgc = ZhiFang.BLLFactory.BLLFactory<IBPGroupControl>.GetBLL();
        IBCLIENTELE client = ZhiFang.BLLFactory.BLLFactory<IBCLIENTELE>.GetBLL();
        IBPGroupPrint pgp = ZhiFang.BLLFactory.BLLFactory<IBPGroupPrint>.GetBLL();
        IBSickType sicktype = ZhiFang.BLLFactory.BLLFactory<IBSickType>.GetBLL();
        IBTestItem ti = ZhiFang.BLLFactory.BLLFactory<IBTestItem>.GetBLL();
        Model.Lab_PGroup Lab_PGroup = new Model.Lab_PGroup();
        Model.PGroup PGroup = new Model.PGroup();
        private readonly IBReportData ibrd = BLLFactory<IBReportData>.GetBLL("ReportData");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DictionaryBind();
                if (base.CheckQueryStringNull_INT("Id"))
                {
                    ZhiFang.Model.PGroupPrint pgp_m = pgp.GetModel(base.ReadQueryString("Id").Trim());
                    if (pgp_m != null)
                    {
                        this.TxtBox_Sort.Text = pgp_m.Sort.ToString();
                        this.TxtBox_Min.Text = pgp_m.ItemMinNumber.ToString();
                        this.TxtBox_Max.Text = pgp_m.ItemMaxNumber.ToString();
                        if (pgp_m.ClientNo != null)
                        {
                            foreach (ListItem i in this.DDL_Client.Items)
                            {
                                i.Selected = false;
                                if (i.Value == pgp_m.ClientNo)
                                {
                                    i.Selected = true;
                                    Lab_PGroup.LabCode = this.DDL_Client.SelectedValue.Trim();
                                    DataSet ds = new DataSet();
                                    if (ConfigHelper.GetConfigString("SectionNo") == null && ConfigHelper.GetConfigString("SectionNo").Trim() == "" || ConfigHelper.GetConfigString("SectionNo") == "0")
                                    {
                                        ds = lpg.GetList(Lab_PGroup);
                                        this.DDL_Section.DataSource = ds;
                                        this.DDL_Section.DataTextField = "CName";
                                        this.DDL_Section.DataValueField = "LabSectionNo";
                                    }
                                    else
                                    {
                                        ds = pg.GetAllList();
                                        this.DDL_Section.DataSource = ds;
                                        this.DDL_Section.DataTextField = "CName";
                                        this.DDL_Section.DataValueField = "SectionNo";
                                    }
                          
                                    this.DDL_Section.DataBind();
                                    break;
                                }
                            }
                        }
                        foreach (ListItem i in this.DDL_Section.Items)
                        {
                            i.Selected = false;
                            if (i.Value == pgp_m.SectionNo.Value.ToString())
                            {
                                i.Selected = true;
                                break;
                            }
                        }
                        foreach (ListItem i in this.DDL_Model.Items)
                        {
                            i.Selected = false;
                            if (i.Value == pgp_m.PrintFormatNo.Value.ToString())
                            {
                                i.Selected = true;
                                break;
                            }
                        }

                        foreach (ListItem i in this.DDL_UserFlag.Items)
                        {
                            i.Selected = false;
                            if (i.Value == pgp_m.UseFlag.Value.ToString())
                            {
                                i.Selected = true;
                                break;
                            }
                        }
                        if (pgp_m.ImageFlag != null)
                        {
                            foreach (ListItem i in this.DDL_ImageFlag.Items)
                            {
                                i.Selected = false;
                                if (i.Value == pgp_m.ImageFlag.Value.ToString())
                                {
                                    i.Selected = true;
                                    break;
                                }
                            }
                        }
                        if (pgp_m.AntiFlag != null)
                        {
                            foreach (ListItem i in this.DDL_AntiFlag.Items)
                            {
                                i.Selected = false;
                                if (i.Value == pgp_m.AntiFlag.Value.ToString())
                                {
                                    i.Selected = true;
                                    break;
                                }
                            }
                        }
                        if (pgp_m.BatchPrint != null)
                        {
                            foreach (ListItem i in this.DDL_BatchPrintFlag.Items)
                            {
                                i.Selected = false;
                                if (i.Value == pgp_m.BatchPrint.Value.ToString())
                                {
                                    i.Selected = true;
                                    break;
                                }
                            }
                        }
                        if (pgp_m.SickTypeNo != null)
                        {
                            foreach (ListItem i in this.DDL_SickTypeNo.Items)
                            {
                                i.Selected = false;
                                if (i.Value == pgp_m.SickTypeNo.Value.ToString())
                                {
                                    i.Selected = true;
                                    break;
                                }
                            }
                        }
                        if (pgp_m.SpecialtyItemNo != null)
                        {
                            foreach (ListItem i in this.DDL_SpecialtyItem.Items)
                            {
                                i.Selected = false;
                                if (i.Value == pgp_m.SpecialtyItemNo.Value.ToString())
                                {
                                    i.Selected = true;
                                    break;
                                }
                            }
                        }
                        if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
                        {
                            if (pgp_m.ModelTitleType != null)
                            {
                                foreach (ListItem i in this.DDL_ModelTitleType.Items)
                                {
                                    i.Selected = false;
                                    if (i.Value == pgp_m.ModelTitleType.Value.ToString())
                                    {
                                        i.Selected = true;
                                        break;
                                    }
                                }
                            }
                        }

                    }
                }

            }
        }
        protected void DictionaryBind()
        {

            ListItem li = new ListItem("无", "");
            DataTable newDt = new DataTable();
            DataSet dsClient = client.GetAllList();
            DataTable dt = new DataTable();
            dt = dsClient.Tables[0];//.Select("", " ClIENTNO asc ")
            newDt = dt.Clone();
            DataRow[] dr = dt.Select("", " ClIENTNO asc ");
            for (int i = 0; i < dr.Length; i++)
            {
                newDt.ImportRow((DataRow)dr[i]);
            }
            dt = newDt;
            dsClient.Clone();
            DataSet nds = new DataSet();
            nds.Tables.Add(dt);
            this.DDL_Client.DataSource = nds;
            this.DDL_Client.DataTextField = "CNAME";
            this.DDL_Client.DataValueField = "ClIENTNO";
            this.DDL_Client.DataBind();
            if (nds.Tables[0] != null && nds.Tables[0].Rows.Count > 0)
            {
                Lab_PGroup.LabCode = nds.Tables[0].Rows[0]["ClIENTNO"].ToString();
            }
            //this.DDL_Client.Items.Insert(0, li);
            Lab_PGroup.LabCode = this.DDL_Client.SelectedValue.Trim();
            DataSet ds = new DataSet();
            if (ConfigHelper.GetConfigString("SectionNo") == null && ConfigHelper.GetConfigString("SectionNo").Trim() == "" || ConfigHelper.GetConfigString("SectionNo") == "0")
            {
                 ds = lpg.GetList(Lab_PGroup);
                 if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                 {
                     ds.Tables[0].Columns.Add("CName_Client", typeof(string));
                     //for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                     //{
                     //    if(ds.Tables[0].Rows[i]["CName_Client"].ToString().Trim()!="")
                     //    {
                     //    ds.Tables[0].Rows[i]["CName_Client"]=ds.Tables[0].Rows[i]["CName_Client"]
                     //    }
                     //    else
                     //    {

                     //    }
                     //}
                 }
                 this.DDL_Section.DataSource = ds;
                 this.DDL_Section.DataTextField = "CName";
                 this.DDL_Section.DataValueField = "LabSectionNo";
                 this.DDL_Section.DataBind();
            }
            else
            {
                 ds = pg.GetAllList();
                 if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                 {
                     ds.Tables[0].Columns.Add("CName_Client", typeof(string));
                     //for(int i=0;i<ds.Tables[0].Rows.Count;i++)
                     //{
                     //    if(ds.Tables[0].Rows[i]["CName_Client"].ToString().Trim()!="")
                     //    {
                     //    ds.Tables[0].Rows[i]["CName_Client"]=ds.Tables[0].Rows[i]["CName_Client"]
                     //    }
                     //    else
                     //    {

                     //    }
                     //}
                 }
                 this.DDL_Section.DataSource = ds;
                 this.DDL_Section.DataTextField = "CName";
                 this.DDL_Section.DataValueField = "SectionNo";
                 this.DDL_Section.DataBind();
            }
           
            this.DDL_Model.DataSource = pgf.GetAllList();
            this.DDL_Model.DataTextField = "PrintFormatName";
            this.DDL_Model.DataValueField = "Id";
            this.DDL_Model.DataBind();
            try
            {
                ListItem li1 = new ListItem("无", "");
                this.DDL_SpecialtyItem.DataSource = ti.GetAllList();
                this.DDL_SpecialtyItem.DataTextField = "CName";
                this.DDL_SpecialtyItem.DataValueField = "ItemNo";
                this.DDL_SpecialtyItem.DataBind();
                this.DDL_SpecialtyItem.Items.Insert(0, li1);
            }
            catch (Exception eee)
            {
                ZhiFang.Common.Log.Log.Info(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "获取数据字典错误CName_ItemNo" + eee.ToString());
            }


            this.DDL_SickTypeNo.DataSource = sicktype.GetAllList();
            this.DDL_SickTypeNo.DataTextField = "CNAME";
            this.DDL_SickTypeNo.DataValueField = "SickTypeNo";
            this.DDL_SickTypeNo.DataBind();
            this.DDL_SickTypeNo.Items.Insert(0, li);
        }
        /// <summary>
        /// 添加小组模版确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            ZhiFang.Model.PGroupPrint pgp_m = new ZhiFang.Model.PGroupPrint();
            ZhiFang.Model.PGroupControl pgc_m = new Model.PGroupControl();
            try
            {
                //string sectionno = this.DDL_Section.SelectedValue.Trim();
                pgc_m.ControlSectionNo = Convert.ToInt32(this.DDL_Section.SelectedValue.Trim());
                pgp_m.SectionNo = Convert.ToInt32(this.DDL_Section.SelectedValue.Trim());
                pgc_m.LabCode = this.DDL_Client.SelectedValue.Trim();
                ZhiFang.Common.Log.Log.Info("ControlSectionNo :" + pgc_m.ControlSectionNo + "LabCode:" + pgc_m.LabCode);
                DataSet dsPGroupControl = ibpgc.GetList(pgc_m);
                //因为是中心编码 所以不取对照关系
                //if (dsPGroupControl.Tables != null && dsPGroupControl.Tables[0].Rows.Count > 0)
                //{
                //    ZhiFang.Common.Log.Log.Info("存在SectionNo");
                //    pgp_m.SectionNo = Convert.ToInt32(dsPGroupControl.Tables[0].Rows[0]["SectionNo"]);
                //}

                //pgc_m. = Convert.ToInt32(sectionno);
                //ibpgc.GetList();
                //pgp_m.SectionNo = Convert.ToInt32(this.DDL_Section.SelectedValue.Trim());
                pgp_m.PrintFormatNo = Convert.ToInt32(this.DDL_Model.SelectedValue.Trim());
                try
                {
                    pgp_m.ClientNo = this.DDL_Client.SelectedValue.Trim();
                }
                catch
                {
                    pgp_m.ClientNo = null;
                }
                try
                {
                    if (ZhiFang.Common.Public.PageValidate.IsNumber(this.TxtBox_Sort.Text.Trim()))
                    {
                        pgp_m.Sort = Convert.ToInt32(this.TxtBox_Sort.Text.Trim());
                    }
                    else
                    {
                        Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndFunction("“优先级”请输入数字！", "history.go(-1);"));
                        return;
                    }
                }
                catch
                {
                    pgp_m.Sort = 0;
                }
                try
                {
                    if (ZhiFang.Common.Public.PageValidate.IsNumber(this.TxtBox_Min.Text.Trim()))
                    {
                        pgp_m.ItemMinNumber = Convert.ToInt32(this.TxtBox_Min.Text.Trim());
                    }
                    else
                    {
                        if (this.TxtBox_Min.Text.Trim().Length == 0)
                        {
                            pgp_m.ItemMinNumber = null;
                        }
                        else
                        {
                            Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndFunction("“最小项目数”请输入数字！", "history.go(-1);"));
                            return;
                        }
                    }
                }
                catch
                {
                    pgp_m.Sort = 0;
                }
                try
                {
                    if (ZhiFang.Common.Public.PageValidate.IsNumber(this.TxtBox_Max.Text.Trim()))
                    {
                        pgp_m.ItemMaxNumber = Convert.ToInt32(this.TxtBox_Max.Text.Trim());
                    }
                    else
                    {
                        if (this.TxtBox_Min.Text.Trim().Length == 0)
                        {
                            pgp_m.ItemMaxNumber = null;
                        }
                        else
                        {
                            Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndFunction("“最大项目数”请输入数字！", "history.go(-1);"));
                            return;
                        }
                    }
                }
                catch
                {
                    pgp_m.Sort = 0;
                }
                pgp_m.UseFlag = Convert.ToInt32(this.DDL_UserFlag.SelectedValue.Trim());
                pgp_m.ImageFlag = Convert.ToInt32(this.DDL_ImageFlag.SelectedValue.Trim());
                pgp_m.AntiFlag = Convert.ToInt32(this.DDL_AntiFlag.SelectedValue.Trim());
                pgp_m.BatchPrint = Convert.ToInt32(this.DDL_BatchPrintFlag.SelectedValue.Trim());
                try
                {
                    pgp_m.SpecialtyItemNo = Convert.ToInt32(this.DDL_SpecialtyItem.SelectedValue.Trim());
                }
                catch
                {
                    pgp_m.SpecialtyItemNo = null;
                }
                if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
                {
                    pgp_m.ModelTitleType = Convert.ToInt32(this.DDL_ModelTitleType.SelectedValue.Trim());
                }

                if (Tools.Validate.IsInt(this.DDL_SickTypeNo.SelectedValue.Trim()))
                {
                    pgp_m.SickTypeNo = Convert.ToInt32(this.DDL_SickTypeNo.SelectedValue.Trim());
                }
                if (base.CheckQueryStringNull_INT("Id"))
                {
                    pgp_m.Id = Convert.ToInt32(base.ReadQueryString("Id"));
                    if (pgp.Update(pgp_m) > 0)
                    {
                        Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("修改小组模板成功！", "opener.location.href=opener.location.href"));
                    }
                    else
                    {
                        Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("修改小组模板失败！", "opener.location.href=opener.location.href"));
                    }
                }
                else
                {
                    if (pgp.Add(pgp_m) > 0)
                    {
                        Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("添加小组模板成功！", "opener.location.href=opener.location.href"));
                    }
                    else
                    {
                        Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("添加小组模板失败！", "opener.location.href=opener.location.href"));
                    }
                }

            }
            catch
            {
                Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("操作小组模板失败！", "opener.location.href=opener.location.href"));
            }
        }

        protected void btnSearchSJDW_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            string UserInput = hiddenUserInput.Value;

            if (UserInput.Trim().Length > 0)
            {
                ZhiFang.Model.TestItem ti_m = new ZhiFang.Model.TestItem();
                ti_m.CName = UserInput.Trim();
                ti_m.EName = UserInput.Trim();
                ti_m.ShortCode = UserInput.Trim();
                ti_m.ShortName = UserInput.Trim();
                dt = ZhiFang.BLLFactory.BLLFactory<IBTestItem>.GetBLL("TestItem").GetListLike(ti_m).Tables[0];
            }
            else
            {
                dt = ZhiFang.BLLFactory.BLLFactory<IBTestItem>.GetBLL("TestItem").GetAllList().Tables[0];
            }
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string c = "#a3f1f5";
                    if (i == 0)
                    {
                        c = "#999999";
                    }
                    System.Web.UI.HtmlControls.HtmlTableRow tr = new System.Web.UI.HtmlControls.HtmlTableRow();
                    System.Web.UI.HtmlControls.HtmlTableCell td = new System.Web.UI.HtmlControls.HtmlTableCell();
                    System.Web.UI.HtmlControls.HtmlTableCell td1 = new System.Web.UI.HtmlControls.HtmlTableCell();
                    td.Width = "20px";
                    td1.Width = "20px";
                    td.InnerHtml = dt.Rows[i]["CNAME"].ToString() + "&nbsp;";
                    td.Style.Add("padding", "2px");
                    td.Style.Add("font-size", "12px");
                    td1.InnerHtml = dt.Rows[i]["ShortCode"].ToString() + "&nbsp;";
                    td1.Style.Add("padding", "2px");
                    td1.Style.Add("font-size", "12px");
                    tr.Cells.Add(td);
                    tr.Cells.Add(td1);
                    tr.BgColor = c;
                    tr.Attributes.Add("onmouseover", "this.style.backgroundColor='#AAA';");
                    tr.Attributes.Add("onmousedown", "this.style.backgroundColor='" + c + "';GetTmpClient('" + dt.Rows[i]["ItemNo"].ToString() + "','" + dt.Rows[i]["CNAME"].ToString() + "')");
                    //
                    tr.Attributes.Add("onmouseout", "this.style.backgroundColor='#a3f1f5';");
                    tr.Attributes.Add("onclick", "GetTmpClient('" + dt.Rows[i]["ItemNo"].ToString() + "','" + dt.Rows[i]["CNAME"].ToString() + "')");
                    //tr.Attributes.Add("onclick", "alert('test');");
                    if (i <= 9)
                    {
                        tableUserInputSJDWList.Rows.Add(tr);
                    }
                }
            }
            Div_Input.Style["display"] = "";
            Div_Input.Style.Add("left", hiddenLeft.Value);
            Div_Input.Style.Add("top", hiddenTop.Value);
        }
        protected void DDL_Client_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ConfigHelper.GetConfigString("SectionNo") == null && ConfigHelper.GetConfigString("SectionNo").Trim() == "" || ConfigHelper.GetConfigString("SectionNo") == "0")
                {
                    ZhiFang.Common.Log.Log.Info("[B_PGroupControl]");
                    Lab_PGroup.LabCode = this.DDL_Client.SelectedValue.Trim();
                    //DataSet ds = lpg.GetList(Lab_PGroup);
                    DataSet ds = pg.GetAllList();
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        this.DDL_Section.DataSource = ds;
                        this.DDL_Section.DataTextField = "CName";
                        this.DDL_Section.DataValueField = "LabSectionNo";
                        this.DDL_Section.DataBind();
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("[PGroup]");
                    DataSet ds = ibrd.GetPGroup(this.DDL_Client.SelectedValue.Trim());
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        this.DDL_Section.DataSource = ds;
                        this.DDL_Section.DataTextField = "CName";
                        this.DDL_Section.DataValueField = "SectionNo";
                        this.DDL_Section.DataBind();
                    }
                }
            }
            catch (Exception)
            {
                ZhiFang.Common.Log.Log.Info("[B_PGroupControl]");
                Lab_PGroup.LabCode = this.DDL_Client.SelectedValue.Trim();
                DataSet ds = lpg.GetList(Lab_PGroup);
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    this.DDL_Section.DataSource = ds;
                    this.DDL_Section.DataTextField = "CName";
                    this.DDL_Section.DataValueField = "LabSectionNo";
                    this.DDL_Section.DataBind();
                }
            }



        }
    }
}
