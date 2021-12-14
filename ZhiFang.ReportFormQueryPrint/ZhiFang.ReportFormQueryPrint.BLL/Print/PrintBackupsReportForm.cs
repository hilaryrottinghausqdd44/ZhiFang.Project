using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Common;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Model;
using ZhiFang.ReportFormQueryPrint.Model.VO;

namespace ZhiFang.ReportFormQueryPrint.BLL.Print
{
    public class PrintBackupsReportForm
    {
        protected readonly IDAL.IDReportDrugGene idrdruggene = DalFactory<IDReportDrugGene>.GetDal("ReportDrugGene");
        private List<ReportFormFilesVO> TemplateDataBindAndCreatReportForm(DataRow drreportform, DataTable dtri, DataTable dtrg, ReportFormTitle rft, string templatefullpath)
        {
            List<ReportFormFilesVO> reportformfileslist = new List<ReportFormFilesVO>();
            PrintBackupsReportFormCommon prfc = new PrintBackupsReportFormCommon();
            if (templatefullpath == null)
                return null;

            switch (System.IO.Path.GetExtension(templatefullpath).ToUpper())
            {
                case ".FRX":
                    reportformfileslist = prfc.CreatReportFormFilesByFRX(drreportform, dtri, dtrg, templatefullpath, rft);
                    break;
                default: break;
            }
            return reportformfileslist;
        }
        /// <summary>
        /// 填充普通生化类项目到dtri表
        /// </summary>
        /// <param name="reportformid">报告单ID</param>
        /// <param name="dtri"></param>
        private void SetReportItem(string reportformid, ref DataTable dtri)
        {
            BBackupsReportItem rifb = new BBackupsReportItem();
            Model.BackupsReportItem rif_m = new Model.BackupsReportItem();
            rif_m.FormNo = reportformid;
            DataSet dsri = rifb.GetReportItemList_DataTable(reportformid).DataSet;
            if (dsri != null && dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
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
            BBackupsReportMicro rmfb = new BBackupsReportMicro();
            DataTable dsri = rmfb.GetReportMicroList(reportformid);
            if (dsri != null && dsri.Rows.Count > 0)
            {
                dtrmicro = dsri;
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
            BBackupsReportMarrow rmarrowfb = new BBackupsReportMarrow();
            DataTable dsri = rmarrowfb.GetReportMarrowItemList(reportformid);

            if (dsri != null && dsri.Rows.Count > 0)
            {
                dtrmarrow = dsri;
            }

            dtrmarrow.TableName = "frmarrow";
        }

        /// <summary>
        /// 填充图片结果到dtg表
        /// </summary>
        /// <param name="reportformid">报告单ID</param>
        /// <param name="dtrg"></param>
        private void SetReportGraph(string reportformid, ref DataTable dtrg)
        {
            DAL.MSSQL.Backups.RFGraphData bgd = new DAL.MSSQL.Backups.RFGraphData();
            DataSet dsrg = bgd.GetListByReportPublicationID(reportformid);
            if (dsrg != null && dsrg.Tables.Count > 0 && dsrg.Tables[0].Rows.Count > 0)
            {
                dtrg = dsrg.Tables[0];
            }
            dtrg.TableName = "frgraph";
        }
        /// <summary>
        /// 生成PDF报告
        /// </summary>
        /// <param name="reportformidlist"></param>
        /// <param name="reportFormTitle"></param>
        /// <param name="reportFormFileType"></param>
        /// <param name="st">小组类型</param>
        /// <param name="flag">0:判断存在则不重新生成,1:强制重新生成.</param>
        /// <returns>List<ReportFormFilesVO></returns>
        public List<ReportFormFilesVO> CreatReportFormFiles(List<string> reportformidlist, ReportFormTitle reportFormTitle, ReportFormFileType reportFormFileType, string st, int flag = 1, int pow = -1)
        {
            ZhiFang.ReportFormQueryPrint.BLL.BBackupsReportForm brf = new BBackupsReportForm();
            Model.BackupsReportForm brf_m = new Model.BackupsReportForm();
            List<ReportFormFilesVO> reportformfileslist = new List<ReportFormFilesVO>();
            BBackupsReportMicro rmfb = new BBackupsReportMicro();
            PrintBackupsReportFormCommon prfc = new PrintBackupsReportFormCommon();
            foreach (var reportformid in reportformidlist)
            {

                brf_m.FormNo = reportformid;
                DataTable dtrf = brf.GetListByDataSource(reportformid);
                if (dtrf != null && dtrf.Rows.Count > 0)
                {
                    ReportFormFilesVO tmpreportformfilesvo = new ReportFormFilesVO();

                    if (flag == 1 || !prfc.ExistsReportForm_PDF(dtrf, ref tmpreportformfilesvo))
                    {

                        DataTable dtri = new DataTable();
                        DataTable dtrg = new DataTable();
                        dtrf.TableName = "frform";
                        #region 预处理
                        int sectiontype = Convert.ToInt32(st);
                        if (dtrf.Columns.Contains("SECTIONTYPE") && dtrf.Rows[0]["SECTIONTYPE"] != null && dtrf.Rows[0]["SECTIONTYPE"].ToString().Trim() != "")
                        {
                            sectiontype = Convert.ToInt32(dtrf.Rows[0]["SECTIONTYPE"].ToString());
                        }
                        if (sectiontype == 2 || sectiontype == 4)
                        {
                            if (dtrf.Columns.Contains("STestType") && dtrf.Rows[0]["STestType"] != null && dtrf.Rows[0]["STestType"].ToString().Trim() != "")
                            {
                                if (dtrf.Rows[0]["STestType"].ToString().Trim() == "20")
                                {
                                    sectiontype = 2;
                                }
                                if (dtrf.Rows[0]["STestType"].ToString().Trim() == "21")
                                {
                                    sectiontype = 2;
                                }
                                if (dtrf.Rows[0]["STestType"].ToString().Trim() == "22")
                                {
                                    sectiontype = 1;
                                }
                            }
                        }
                        #endregion
                        #region 数据准备
                        switch ((SectionType)Convert.ToInt32(sectiontype))
                        {
                            case SectionType.all:
                                #region Normal
                                SetReportItem(reportformid, ref dtri); break;
                            #endregion
                            case SectionType.Normal:
                                #region Normal
                                if (dtrf.Columns.Contains("SectionResultType") && dtrf.Rows[0]["SectionResultType"].ToString().Trim() == "1")
                                {
                                    dtri = idrdruggene.GetReportItemFullList(reportformid); break;
                                }
                                else
                                {
                                    SetReportItem(reportformid, ref dtri); break;
                                }
                            #endregion
                            case SectionType.Micro:
                                #region Micro
                                SetReportMicro(reportformid, ref dtri); break;
                            #endregion
                            case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroSmear:
                                {
                                    #region TestGroupMicroSmear
                                    try
                                    {
                                        //dsri = rmfb.GetList(rmf_m);
                                        dtri = rmfb.GetReportMicroGroupList(reportformid);
                                    }
                                    catch (Exception ex)
                                    {
                                        ZhiFang.Common.Log.Log.Debug("CreatReportFormFiles.TestGroupMicroSmear异常信息:" + ex.ToString());

                                    }
                                    break;
                                    #endregion
                                }
                            case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroSmearExt:
                                {
                                    #region TestGroupMicroSmearExt
                                    try
                                    {
                                        //dsri = rmfb.GetList(rmf_m);
                                        dtri = rmfb.GetReportMicroGroupList(reportformid);
                                    }
                                    catch (Exception ex)
                                    {
                                        ZhiFang.Common.Log.Log.Debug("CreatReportFormFiles.TestGroupMicroSmearExt异常信息:" + ex.ToString());

                                    }
                                    break;
                                    #endregion
                                }
                            case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroCultureAssayAntibioticSusceptibility:
                                {
                                    #region TestGroupMicroCultureAssayAntibioticSusceptibility
                                    try
                                    {
                                        //dsri = rmfb.GetList(rmf_m);
                                        dtri = rmfb.GetReportMicroGroupList(reportformid);
                                    }
                                    catch (Exception ex)
                                    {
                                        ZhiFang.Common.Log.Log.Debug("CreatReportFormFiles.TestGroupMicroCultureAssayAntibioticSusceptibility异常信息:" + ex.ToString());

                                    }
                                    break;
                                    #endregion
                                }
                            case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroCultureAssayAntibioticSusceptibilityExt:
                                {
                                    #region TestGroupMicroCultureAssayAntibioticSusceptibilityExt
                                    try
                                    {
                                        //dsri = rmfb.GetList(rmf_m);
                                        dtri = rmfb.GetReportMicroGroupList(reportformid);
                                    }
                                    catch (Exception ex)
                                    {
                                        ZhiFang.Common.Log.Log.Debug("CreatReportFormFiles.TestGroupMicroCultureAssayAntibioticSusceptibilityExt异常信息:" + ex.ToString());
                                    }
                                    break;
                                    #endregion
                                }
                            case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroOtherTest:
                                {
                                    #region Micro
                                    try
                                    {
                                        //dsri = rmfb.GetList(rmf_m);
                                        dtri = rmfb.GetReportMicroGroupList(reportformid);
                                    }
                                    catch (Exception ex)
                                    {
                                        ZhiFang.Common.Log.Log.Debug("CreatReportFormFiles.TestGroupMicroOtherTest异常信息:" + ex.ToString());

                                    }
                                    break;
                                    #endregion
                                }
                            case SectionType.NormalIncImage:
                                #region NormalIncImage

                                if (dtrf.Columns.Contains("SectionResultType") && dtrf.Rows[0]["SectionResultType"].ToString().Trim() == "1")
                                {
                                    dtri = idrdruggene.GetReportItemFullList(reportformid);
                                }
                                else
                                {
                                    SetReportItem(reportformid, ref dtri);
                                }
                                SetReportGraph(reportformid, ref dtrg);
                                break;
                            #endregion
                            case SectionType.MicroIncImage:
                                #region MicroIncImage
                                SetReportMicro(reportformid, ref dtri);
                                SetReportGraph(reportformid, ref dtrg);
                                break;
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
                        string TemplateFullPath = null;
                        string log = "";
                        TemplateFullPath = prfc.FindTemplate(dtrf.Rows[0], dtri, reportFormTitle, out log);
                        if (TemplateFullPath == null || TemplateFullPath.Trim() == "")
                        {
                            throw new Exception("未找到匹配的模版！");
                        }
                        #endregion

                        #region 数据绑定生成报告
                        List<ReportFormFilesVO> reportpath = this.TemplateDataBindAndCreatReportForm(dtrf.Rows[0], dtri, dtrg, reportFormTitle, TemplateFullPath);
                        if (reportpath.Count > 0)
                        {
                            brf.UpdatePageInfo(reportformid, reportpath[0].PageCount, reportpath[0].PageName); //更新reportformfull表
                            reportformfileslist.AddRange(reportpath);
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("CreatReportFormFiles.reportformid:" + reportformid + ";找到文件：" + tmpreportformfilesvo.ReportFormID + ";PDFPath" + tmpreportformfilesvo.PDFPath);
                        reportformfileslist.Add(tmpreportformfilesvo);
                    }
                    #endregion
                }
            }
            return reportformfileslist;
        }

    }
}
