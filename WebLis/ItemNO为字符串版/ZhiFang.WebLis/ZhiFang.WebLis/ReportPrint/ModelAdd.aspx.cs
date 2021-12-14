using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Report;
using System.Data;
using ZhiFang.Common;
using ZhiFang.WebLis.Class;
using ZhiFang.IBLL.Common.BaseDictionary;

namespace ZhiFang.WebLis.ReportPrint
{
    public partial class ModelAdd : BasePage
    {
        private readonly IBPrintFormat pfb = BLLFactory<IBPrintFormat>.GetBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (base.CheckQueryStringNull_INT("Id"))
                {
                    ZhiFang.Model.PrintFormat pf_m = pfb.GetModel(base.ReadQueryString("Id").Trim());
                    Label1.Text = "修改";
                    if (pf_m != null)
                    {
                        this.TxtBox_ModelName.Text = pf_m.PrintFormatName;
                        this.TxtBox_ModelAddress.Text = pf_m.PintFormatAddress;
                        this.TxtBox_ModelItemCount.Text = pf_m.ItemParaLineNum.Value.ToString();
                        this.TxtBox_ModelPaperSize.Text = pf_m.PaperSize;
                        foreach(ListItem i in this.DDL_ModelBatchFlag.Items)
                        {
                            i.Selected = false;
                            if(i.Value==pf_m.BatchPrint.Value.ToString())
                            {
                                i.Selected=true;
                                break;
                            }
                        }
                        if (pf_m.ImageFlag != null)
                        {
                            foreach (ListItem i in this.DDL_ImageFlag.Items)
                            {
                                i.Selected = false;
                                if (i.Value == pf_m.ImageFlag.Value.ToString())
                                {
                                    i.Selected = true;
                                    break;
                                }
                            }
                        }
                        if (pf_m.AntiFlag != null)
                        {
                            foreach (ListItem i in this.DDL_AntiFlag.Items)
                            {
                                i.Selected = false;
                                if (i.Value == pf_m.AntiFlag.Value.ToString())
                                {
                                    i.Selected = true;
                                    break;
                                }
                            }
                        }
                        this.TxtBox_ModelDesc.Text = pf_m.PrintFormatDesc;
                        this.TxtBox_ModelAddress.Enabled = false;
                        this.FileUpload1.Visible = false;
                        this.FileUpload1.Enabled = false;
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Response.Write("aaa");
            try
            {
                ZhiFang.Model.PrintFormat pf_m = new ZhiFang.Model.PrintFormat();
                pf_m.PrintFormatName = this.TxtBox_ModelName.Text.Trim();
                if (pfb.GetList(pf_m).Tables.Count > 0 && pfb.GetList(pf_m).Tables[0].Rows.Count > 0)
                {
                    Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndFunction("添加模板失败！模板名重名！", "history.go(-1);"));
                    return;
                }
                pf_m.PaperSize = this.TxtBox_ModelPaperSize.Text.Trim();
                pf_m.PrintFormatDesc = this.TxtBox_ModelDesc.Text.Trim();
                try
                {
                    pf_m.ItemParaLineNum = Convert.ToInt32(this.TxtBox_ModelItemCount.Text.Trim());
                }
                catch
                {
                    Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndFunction("请输入数字！", "history.go(-1);"));
                    pf_m.ItemParaLineNum = 0;
                }
                try
                {
                    pf_m.BatchPrint = Convert.ToInt32(this.DDL_ModelBatchFlag.SelectedValue.Trim());
                }
                catch
                {
                    Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndFunction("请输入数字！", "history.go(-1);"));
                    pf_m.BatchPrint = 0;
                }
                //Response.Write("bbb");
                pf_m.PintFormatAddress = this.TxtBox_ModelAddress.Text.Trim();
                string tmpfilename = this.FileUpload1.FileName.ToString();
                string tmpfiledir = ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL");
                pf_m.ImageFlag = Convert.ToInt32(this.DDL_ImageFlag.SelectedValue.Trim());
                pf_m.AntiFlag = Convert.ToInt32(this.DDL_AntiFlag.SelectedValue.Trim());
                if (base.CheckQueryStringNull_INT("Id"))
                {
                    try
                    {
                        pf_m.Id = Convert.ToInt32(base.ReadQueryString("Id"));
                        if (pfb.Update(pf_m) > 0)
                        {
                            Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("修改模板成功！", "opener.location.href=opener.location.href"));
                        }
                        else
                        {
                            Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("修改模板失败！", "opener.location.href=opener.location.href"));
                        }
                    }
                    catch
                    {
                        Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("修改模板失败！", "opener.location.href=opener.location.href"));
                    }
                }
                else
                {
                    if (tmpfilename.Length > 0 && this.FileUpload1.FileBytes.Count() > 0 && (tmpfilename.Substring(tmpfilename.LastIndexOf('.'), tmpfilename.Length - tmpfilename.LastIndexOf('.')).ToUpper() == ".XSL" || tmpfilename.Substring(tmpfilename.LastIndexOf('.'), tmpfilename.Length - tmpfilename.LastIndexOf('.')).ToUpper() == ".XSLT" || (tmpfilename.Substring(tmpfilename.LastIndexOf('.'), tmpfilename.Length - tmpfilename.LastIndexOf('.')).ToUpper() == ".FRX" || tmpfilename.Substring(tmpfilename.LastIndexOf('.'), tmpfilename.Length - tmpfilename.LastIndexOf('.')).ToUpper() == ".FR3")))
                    {
                        pf_m.PintFormatFileName = this.FileUpload1.FileName.ToString();
                        int id = pfb.Add(pf_m);
                        if (this.TxtBox_ModelAddress.Text.Trim().Length > 0)
                        {
                            tmpfiledir = tmpfiledir + "//" + this.TxtBox_ModelAddress.Text.Trim();
                        }
                        //Response.Write("ccc");                    
                        if (id > 0)
                        {

                            if (ZhiFang.Common.Public.FilesHelper.CheckAndCreatDir(Request.ServerVariables.Get("appl_physical_path") + tmpfiledir + "//" + id.ToString().Trim()))
                            {
                                try
                                {
                                    this.FileUpload1.SaveAs(Request.ServerVariables.Get("appl_physical_path") + tmpfiledir + "//" + id.ToString().Trim() + "//" + id.ToString().Trim() + "" + tmpfilename.Substring(tmpfilename.LastIndexOf('.'), tmpfilename.Length - tmpfilename.LastIndexOf('.')));

                                    Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("添加模板成功！", "opener.location.href=opener.location.href"));

                                }
                                catch
                                {
                                    pfb.Delete(id.ToString());
                                    Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("添加模板失败！", "opener.location.href=opener.location.href"));
                                }
                            } 
                            else
                            {
                                pfb.Delete(id.ToString());
                                Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("添加模板失败！", "opener.location.href=opener.location.href"));
                            }

                        }
                        else
                        {
                            Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("添加模板失败！", "opener.location.href=opener.location.href"));
                        }
                    }
                    else
                    {
                        Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndFunction("文件不正确！", "history.go(-1);"));
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
    }
}
