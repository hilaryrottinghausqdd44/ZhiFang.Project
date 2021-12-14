using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;
using ZhiFang.Common.Public;
using System.Data;
using System.IO;
using ZhiFang.Model;
using ZhiFang.IBLL.Report;
using System.Collections;
using ZhiFang.Common.Dictionary;

namespace ZhiFang.BLL.Report.Print
{
    //public class PrintFrom : ShowFrom, ZhiFang.IBLL.Report.IBPrintFrom
    public class PrintReportForm : ZhiFang.IBLL.Report.IBPrintReportForm
    {
        #region IBPrintReportForm 成员
        /// <summary>
        /// 生成报告文件并返回Html内容标签
        /// </summary>
        /// <param name="reportformidlist">报告ID(主键)列表</param>
        /// <param name="rft">报告抬头类型</param>
        /// <param name="rfft">报告文件类型</param>
        /// <returns></returns>
        public List<string> PrintReportFormHtml(List<string> reportformidlist, ZhiFang.Common.Dictionary.ReportFormTitle rft, ZhiFang.Common.Dictionary.ReportFormFileType rfft)
        {
            ZhiFang.BLL.Report.ReportFormFull rffb = new ReportFormFull();
            Model.ReportFormFull rff_m = new Model.ReportFormFull();
            List<string> htmlurllist = new List<string>();
            foreach (var reportformid in reportformidlist)
            {
                string htmlurl = "";
                rff_m.ReportFormID = reportformid;
                DataSet dsrf = rffb.GetList(rff_m);
                if (dsrf.Tables.Count > 0 && dsrf.Tables[0].Rows.Count > 0)
                {
                    DataTable dtrf = new DataTable("frform");
                    DataTable dtri = new DataTable();
                    dtrf = dsrf.Tables[0];
                    dtrf.TableName = "frform";
                    string sectiontype = "0";
                    if (dsrf.Tables[0].Rows[0]["SECTIONTYPE"] != null && dsrf.Tables[0].Rows[0]["SECTIONTYPE"].ToString().Trim() != "")
                    {
                        sectiontype = dsrf.Tables[0].Rows[0]["SECTIONTYPE"].ToString();
                    }
                 
                    #region 数据准备
                    switch ((SectionType)Convert.ToInt32(sectiontype))
                    {
                        case SectionType.all:
                            #region Normal
                            SetReportItem(reportformid, ref dtri); break;
                            #endregion
                        case SectionType.Normal:
                            #region Normal
                            SetReportItem(reportformid, ref dtri); break;
                            #endregion
                        case SectionType.Micro:
                            #region Micro
                            SetReportMicro(reportformid, ref dtri); break;
                            #endregion
                        case SectionType.NormalIncImage:
                            #region NormalIncImage
                            SetReportItem(reportformid, ref dtri); break;
                            #endregion
                        case SectionType.MicroIncImage:
                            #region MicroIncImage
                            SetReportMicro(reportformid, ref dtri); break;
                            #endregion
                        case SectionType.CellMorphology:
                            #region CellMorphology:
                            SetReportMarrow(reportformid, ref dtri); break;
                            #endregion
                        case SectionType.FishCheck:
                            #region FishCheck
                            SetReportMarrow(reportformid, ref dtri); break;
                            #endregion
                        case SectionType.SensorCheck:
                            #region SensorCheck
                            SetReportItem(reportformid, ref dtri); break;
                            #endregion
                        case SectionType.ChromosomeCheck:
                            #region ChromosomeCheck
                            SetReportMarrow(reportformid, ref dtri); break;
                            #endregion
                        case SectionType.PathologyCheck:
                            #region PathologyCheck
                            SetReportMarrow(reportformid, ref dtri); break;
                            #endregion
                        default:
                            SetReportItem(reportformid, ref dtri); break;
                    }
                    #endregion

                    #region 查找模版
                    string modelname = null;
                    modelname = PrintReportFormCommon.FindModel(dtrf.Rows[0], dtri, rft);
                    if (modelname == null || modelname.Trim() == "")
                    {
                        throw new Exception("未找到匹配的模版！");
                    }
                    #endregion

                    #region 数据绑定生成报告
                    List<string> reportpath = this.ModelDataBindAndCreatReportForm(dtrf.Rows[0], dtri, rft, modelname);
                    if (reportpath.Count > 0)
                    {
                        htmlurllist.AddRange(reportpath);
                    }
                    #endregion
                }
            }
            return htmlurllist;
        }

        private List<string> ModelDataBindAndCreatReportForm(DataRow drreportform, DataTable dtri, ReportFormTitle rft, string modelname)
        {
            List<string> reportformfileurl = new List<string>();
            if (modelname == null)
                return null;

            switch (System.IO.Path.GetExtension(modelname).ToUpper())
            {
                case ".FRX":
                    reportformfileurl = PrintReportFormCommon.CreatReportFormFilesFRX(drreportform, dtri, modelname, rft);
                    break;
                case ".FR3":
                    reportformfileurl = PrintReportFormCommon.CreatReportFormFilesFR3(drreportform, dtri, modelname, rft);
                    break;
                default: break;
            }
            return reportformfileurl;
        }
        /// <summary>
        /// 填充普通生化类项目到dtri表
        /// </summary>
        /// <param name="reportformid">报告单ID</param>
        /// <param name="dtri"></param>
        private void SetReportItem(string reportformid, ref DataTable dtri)
        {
            ZhiFang.BLL.Report.ReportItemFull rifb = new ReportItemFull();
            Model.ReportItemFull rif_m = new Model.ReportItemFull();
            rif_m.ReportFormID = reportformid;
            DataSet dsri = rifb.GetList(rif_m);
            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
            {
                dtri = dsri.Tables[0];
            }
            dtri.TableName = "fritem";
        }

        /// <summary>
        /// 填充微生物类项目到dtrmicro表
        /// </summary>
        /// <param name="reportformid">报告单ID</param>
        /// <param name="dtrmicro"></param>
        private void SetReportMicro(string reportformid, ref DataTable dtrmicro)
        {
            ZhiFang.BLL.Report.ReportMicroFull rmfb = new ReportMicroFull();
            Model.ReportMicroFull rmf_m = new Model.ReportMicroFull();
            rmf_m.ReportFormID = reportformid;
            DataSet dsri = rmfb.GetList(rmf_m);
            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
            {
                dtrmicro = dsri.Tables[0];
            }
            dtrmicro.TableName = "frmicro";
        }

        /// <summary>
        /// 填充骨髓、病理、细胞学、Fish检查类项目到dtrmarrow表
        /// </summary>
        /// <param name="reportformid">报告单ID</param>
        /// <param name="dtrmarrow"></param>
        private void SetReportMarrow(string reportformid, ref DataTable dtrmarrow)
        {
            ZhiFang.BLL.Report.ReportMarrowFull rmarrowfb = new ReportMarrowFull();
            Model.ReportMarrowFull rmarrowf_m = new Model.ReportMarrowFull();
            rmarrowf_m.ReportFormID = reportformid;
            DataSet dsri = rmarrowfb.GetList(rmarrowf_m);
            if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
            {
                dtrmarrow = dsri.Tables[0];
            }
        }
        #endregion
    }
}
