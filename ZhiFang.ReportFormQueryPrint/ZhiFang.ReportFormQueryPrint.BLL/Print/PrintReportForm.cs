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
    public class PrintReportForm
    {
        #region IBPrintReportForm 成员     
        protected readonly IDAL.IDReportDrugGene idrdruggene = DalFactory<IDReportDrugGene>.GetDal("ReportDrugGene");
        private List<ReportFormFilesVO> TemplateDataBindAndCreatReportForm(DataRow drreportform, DataTable dtri, DataTable dtrg, ReportFormTitle rft, string templatefullpath)
        {
            List<ReportFormFilesVO> reportformfileslist = new List<ReportFormFilesVO>();
            PrintReportFormCommon prfc = new PrintReportFormCommon();
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
            BReportItem rifb = new BReportItem();
            Model.ReportItem rif_m = new Model.ReportItem();
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
            BReportMicro rmfb = new BReportMicro();
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
            BReportMarrow rmarrowfb = new BReportMarrow();
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
            BRFGraphData bgd = new BRFGraphData();
            DataSet dsrg = bgd.GetListByReportPublicationID(reportformid);
            if (dsrg != null && dsrg.Tables.Count > 0 && dsrg.Tables[0].Rows.Count > 0)
            {
                dtrg = dsrg.Tables[0];
            }
            dtrg.TableName = "frgraph";
        }
        #endregion

        /// <summary>
        /// 填充图片结果到dtg表
        /// </summary>
        /// <param name="reportformid">报告单ID</param>
        /// <param name="dtrg"></param>
        private void SetS_RequestVItemReportGraph(string reportformid, ref DataTable dtrg)
        {
            BS_RequestVItem bgd = new BS_RequestVItem();
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
        public List<ReportFormFilesVO> CreatReportFormFiles(List<string> reportformidlist, ReportFormTitle reportFormTitle, ReportFormFileType reportFormFileType, string st, int flag = 0, int pow = -1)
        {
            ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BReportForm();
            Model.ReportForm brf_m = new Model.ReportForm();
            List<ReportFormFilesVO> reportformfileslist = new List<ReportFormFilesVO>();
            BReportMicro rmfb = new BReportMicro();
            PrintReportFormCommon prfc = new PrintReportFormCommon();
            foreach (var reportformid in reportformidlist)
            {
                ZhiFang.Common.Log.Log.Debug("CreatReportFormFiles.ReportFormID:"+ reportformid);
                brf_m.FormNo = reportformid;
                DataTable dtrf = brf.GetListByDataSource(reportformid);
                if (dtrf.Columns.Contains("bRevised") && dtrf.Rows[0]["bRevised"].ToString() == "1")
                {
                    flag = 1;
                }
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
                                SetReportMarrow(reportformid, ref dtri);
                                SetReportGraph(reportformid, ref dtrg);
                                break;
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
                        #region 保密等级
                        //默认不使用
                        if (pow > -1)
                        {
                            //prfc.secrecyGrade(pow, ref dtri);
                        }
                        #endregion

                        #region 查找模版
                        string TemplateFullPath = null;
                        string log = "";
                        string IsRFGraphdataPDf = "False";
                        TemplateFullPath = prfc.FindTemplate(dtrf.Rows[0], dtri, reportFormTitle, out log, out IsRFGraphdataPDf);
                        if (IsRFGraphdataPDf.Equals("True"))
                        {
                            ZhiFang.Common.Log.Log.Debug("第三方外送项目不需要通过FRX模板生成，将获取数据库外送PDF");
                        }
                        else {
                            if (TemplateFullPath == null || TemplateFullPath.Trim() == "")
                            {
                                throw new Exception("未找到匹配的模版！");
                            }
                        }
                        #endregion

                        #region 数据绑定生成报告
                        List<ReportFormFilesVO> reportpath = new List<ReportFormFilesVO>();
                        //判断是否从第三方获取PDF
                        if (IsRFGraphdataPDf.Equals("True"))
                        {
                            BRFGraphData bgd = new BRFGraphData();
                            string[] p = reportformid.Split(';');
                            if (p.Length >= 4)
                            {
                                DataSet dsrg = bgd.GetList(" ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' ");
                                dtrg = dsrg.Tables[0];
                                
                            }
                            else
                            {
                                string IsUseMergePDF = ConfigHelper.GetConfigString("IsUseMergePDF");
                                if (IsUseMergePDF!=null && IsUseMergePDF.Equals("1"))
                                {
                                    DataSet dsrg = bgd.GetList(" ReportPublicationID='" + reportformid + "' ");
                                    dtrg = dsrg.Tables[0];
                                }
                                else
                                {
                                    throw new Exception("查找第三方报告失败报告ID错误.reportformid:"+ reportformid);
                                }
                            }
                            PrintReportFormCommon prfcommon = new PrintReportFormCommon();
                            reportpath = prfcommon.CreatReportFormFilesBybyte(dtrf.Rows[0], dtri, dtrg, TemplateFullPath, reportFormTitle);
                        }
                        else
                        {
                            reportpath = this.TemplateDataBindAndCreatReportForm(dtrf.Rows[0], dtri, dtrg, reportFormTitle, TemplateFullPath);

                        }
                        if (reportpath.Count > 0)
                        {
                            brf.UpdatePageInfo(reportformid, reportpath[0].PageCount, reportpath[0].PageName); //更新reportformfull表
                            reportformfileslist.AddRange(reportpath);
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("CreatReportFormFiles.reportformid:" + reportformid + ";找到文件：" + tmpreportformfilesvo.ReportFormID + ";PDFPath:" + tmpreportformfilesvo.PDFPath);
                        reportformfileslist.Add(tmpreportformfilesvo);
                    }

                    #endregion
                }
            }
            return reportformfileslist;
        }
        public List<ReportFormFilesVO> CreatReportFormFilesGlucoseToleranceZhongRi(List<string> reportformidlist, ReportFormTitle reportFormTitle, ReportFormFileType reportFormFileType, string st, string SerialNos, int flag = 0)
        {
            ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BReportForm();
            
            List<ReportFormFilesVO> reportformfileslist = new List<ReportFormFilesVO>();
            PrintReportFormCommon prfc = new PrintReportFormCommon();
            foreach (var reportformid in reportformidlist)
            {
                ZhiFang.Common.Log.Log.Debug("CreatReportFormFilesGlucoseToleranceZhongRi.ReportFormID:" + reportformid);
                //本报告单数据
                DataTable dtrf = brf.GetListByDataSource(reportformid);
                if (dtrf.Columns.Contains("bRevised") && dtrf.Rows[0]["bRevised"].ToString() == "1")
                {
                    flag = 1;
                }
                if (dtrf != null && dtrf.Rows.Count > 0)
                {
                    ReportFormFilesVO tmpreportformfilesvo = new ReportFormFilesVO();

                    if (flag == 1 || !prfc.ExistsReportForm_PDF(dtrf, ref tmpreportformfilesvo))
                    {
                        
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
                        string[] SerialNosstr = SerialNos.Split(',');//NrequestItem表中特殊项目的条码号
                        //所有特殊项目的对应完成的报告单
                        DataSet formDataset = brf.GetListTopByWhereAndSort(100, "SerialNo in ('" + string.Join("','", SerialNosstr) + "')", "ReceiveTime");
                        DataTable dtri = new DataTable();//所有reportItem
                        dtri.TableName = "fritem";
                        List<string> reportFormIds = new List<string>();

                        if (formDataset != null && formDataset.Tables.Count > 0 && formDataset.Tables[0].Rows.Count > 0)
                        {

                            BReportItem rifb = new BReportItem();
                            for (int i = 0; i < formDataset.Tables[0].Rows.Count; i++)
                            {
                                reportFormIds.Add(formDataset.Tables[0].Rows[i]["ReportFormID"].ToString());
                            }
                            //查询所有reportItem
                            DataSet dsri = rifb.GetList(100, "ReportFormID in ("+ string.Join(",",reportFormIds) + ")" , "ParitemNo");
                            if (dsri != null && dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                            {
                                dtri = dsri.Tables[0];
                                dtri.TableName = "fritem";
                            }
                        }

                        #region
                        //switch ((SectionType)Convert.ToInt32(sectiontype))
                        //{
                        //    case SectionType.all:
                        //        #region Normal
                        //        SetReportItem(reportformid, ref dtri); break;
                        //    #endregion
                        //    case SectionType.Normal:
                        //        #region Normal
                        //        if (dtrf.Columns.Contains("SectionResultType") && dtrf.Rows[0]["SectionResultType"].ToString().Trim() == "1")
                        //        {
                        //            dtri = idrdruggene.GetReportItemFullList(reportformid); break;
                        //        }
                        //        else
                        //        {
                        //            SetReportItem(reportformid, ref dtri); break;
                        //        }
                        //    #endregion

                        //    case SectionType.NormalIncImage:
                        //        #region NormalIncImage

                        //        if (dtrf.Columns.Contains("SectionResultType") && dtrf.Rows[0]["SectionResultType"].ToString().Trim() == "1")
                        //        {
                        //            dtri = idrdruggene.GetReportItemFullList(reportformid);
                        //        }
                        //        else
                        //        {
                        //            SetReportItem(reportformid, ref dtri);
                        //        }
                        //        SetReportGraph(reportformid, ref dtrg);
                        //        break;
                        //    #endregion

                        //    default:
                        //        SetReportItem(reportformid, ref dtri); break;
                        //}
                        #endregion
                        #endregion


                        #region 查找模版
                        string TemplateFullPath = null;
                        string log = "";
                        string IsRFGraphdataPDf = "False";
                        TemplateFullPath = prfc.FindTemplate(dtrf.Rows[0], dtri, reportFormTitle, out log, out IsRFGraphdataPDf);
                        
                        if (TemplateFullPath == null || TemplateFullPath.Trim() == "")
                        {
                            throw new Exception("未找到匹配的模版！");
                        }
                        
                        #endregion

                        #region 数据绑定生成报告
                        List<ReportFormFilesVO> reportpath = new List<ReportFormFilesVO>();
                        //判断是否从第三方获取PDF
                        reportpath = this.TemplateDataBindAndCreatReportForm(dtrf.Rows[0], dtri, dtrg, reportFormTitle, TemplateFullPath);
                        if (reportpath.Count > 0)
                        {
                            brf.UpdatePageInfo(reportformid, reportpath[0].PageCount, reportpath[0].PageName); //更新reportformfull表
                            reportformfileslist.AddRange(reportpath);
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("CreatReportFormFilesGlucoseToleranceZhongRi.reportformid:" + reportformid + ";找到文件：" + tmpreportformfilesvo.ReportFormID + ";PDFPath:" + tmpreportformfilesvo.PDFPath);
                        reportformfileslist.Add(tmpreportformfilesvo);
                    }

                    #endregion
                }
            }
            return reportformfileslist;
        }
        public BaseResultDataValue CreatReportFormFilesTest(List<string> reportformidlist, ReportFormTitle reportFormTitle, ReportFormFileType reportFormFileType, string st, int nextIndex, int flag = 0)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = false;
            ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BReportForm();
            Model.ReportForm brf_m = new Model.ReportForm();
            List<ReportFormFilesVO> reportformfileslist = new List<ReportFormFilesVO>();
            BReportMicro rmfb = new BReportMicro();
            foreach (var reportformid in reportformidlist)
            {
                brf_m.FormNo = reportformid;
                DataTable dtrf = brf.GetListByDataSource(reportformid);
                if (dtrf != null && dtrf.Rows.Count > 0)
                {
                    ReportFormFilesVO tmpreportformfilesvo = new ReportFormFilesVO();
                    PrintReportFormCommon prfc = new PrintReportFormCommon();
                    if (flag == 1 || !prfc.ExistsReportForm_PDF(dtrf, ref tmpreportformfilesvo))
                    {
                        #region NextIndex 1
                        if (nextIndex == 1)
                        {
                            brdv.success = true;
                            brdv.ResultDataValue = "小组：" + dtrf.Rows[0]["SectionName"].ToString() + ",ReportFormID:" + reportformid + "的报告开始生成.";
                            return brdv;
                        }
                        #endregion
                        DataTable dtri = new DataTable();
                        DataTable dtrg = new DataTable();
                        dtrf.TableName = "frform";
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


                        #region NextIndex2 数据准备 
                        switch ((SectionType)Convert.ToInt32(sectiontype))
                        {
                            case SectionType.all:
                                #region Normal
                                SetReportItem(reportformid, ref dtri);
                                if (nextIndex == 2)
                                {
                                    brdv.success = true;
                                    brdv.ResultDataValue = "报告单项目查询完成，数据来源:普通生化";
                                    return brdv;
                                }
                                break;
                            #endregion
                            case SectionType.Normal:
                                #region Normal
                                if (dtrf.Columns.Contains("SectionResultType") && dtrf.Rows[0]["SectionResultType"].ToString().Trim() == "1")
                                {
                                    dtri = idrdruggene.GetReportItemFullList(reportformid);
                                    if (nextIndex == 2)
                                    {
                                        brdv.success = true;
                                        brdv.ResultDataValue = "报告单项目查询完成，数据来源:药物基因";
                                        return brdv;
                                    }
                                    break;
                                }
                                else
                                {
                                    SetReportItem(reportformid, ref dtri);
                                    if (nextIndex == 2)
                                    {
                                        brdv.success = true;
                                        brdv.ResultDataValue = "报告单项目查询完成，数据来源:普通生化";
                                        return brdv;
                                    }
                                    break;
                                }
                            #endregion
                            case SectionType.Micro:
                                #region Micro
                                SetReportMicro(reportformid, ref dtri);
                                if (nextIndex == 2)
                                {
                                    brdv.success = true;
                                    brdv.ResultDataValue = "报告单项目查询完成，数据来源:微生物";
                                    return brdv;
                                }
                                break;
                            #endregion
                            case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroSmear:
                                {
                                    #region TestGroupMicroSmear
                                    try
                                    {
                                        //dsri = rmfb.GetList(rmf_m);
                                        dtri = rmfb.GetReportMicroGroupList(reportformid);
                                        if (nextIndex == 2)
                                        {
                                            brdv.success = true;
                                            brdv.ResultDataValue = "报告单项目查询完成，数据来源:TestGroupMicroSmear";
                                            return brdv;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ZhiFang.Common.Log.Log.Debug("Micro异常信息:" + ex.ToString());
                                        if (nextIndex == 2)
                                        {
                                            brdv.success = false;
                                            brdv.ErrorInfo = "报告单项目查询失败，数据来源：TestGroupMicroSmear;异常信息:" + ex.ToString();
                                            return brdv;
                                        }
                                    }
                                    break;
                                    #endregion
                                }
                            case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroSmearExt:
                                {
                                    #region TestGroupMicroSmearExt
                                    try
                                    {
                                        dtri = rmfb.GetReportMicroGroupList(reportformid);
                                        if (nextIndex == 2)
                                        {
                                            brdv.success = true;
                                            brdv.ResultDataValue = "报告单项目查询完成，数据来源:TestGroupMicroSmearExt";
                                            return brdv;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ZhiFang.Common.Log.Log.Debug("Micro异常信息:" + ex.ToString());
                                        if (nextIndex == 2)
                                        {
                                            brdv.success = false;
                                            brdv.ErrorInfo = "报告单项目查询失败，数据来源：TestGroupMicroSmearExt;异常信息:" + ex.ToString();
                                            return brdv;
                                        }
                                    }
                                    break;
                                    #endregion
                                }
                            case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroCultureAssayAntibioticSusceptibility:
                                {
                                    #region TestGroupMicroCultureAssayAntibioticSusceptibility
                                    try
                                    {
                                        dtri = rmfb.GetReportMicroGroupList(reportformid);
                                        if (nextIndex == 2)
                                        {
                                            brdv.success = true;
                                            brdv.ResultDataValue = "报告单项目查询完成，数据来源:TestGroupMicroCultureAssayAntibioticSusceptibility";
                                            return brdv;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ZhiFang.Common.Log.Log.Debug("Micro异常信息:" + ex.ToString());
                                        if (nextIndex == 2)
                                        {
                                            brdv.success = false;
                                            brdv.ErrorInfo = "报告单项目查询失败，数据来源：TestGroupMicroCultureAssayAntibioticSusceptibility;异常信息:" + ex.ToString();
                                            return brdv;
                                        }
                                    }
                                    break;
                                    #endregion
                                }
                            case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroCultureAssayAntibioticSusceptibilityExt:
                                {
                                    #region TestGroupMicroCultureAssayAntibioticSusceptibilityExt
                                    try
                                    {
                                        dtri = rmfb.GetReportMicroGroupList(reportformid);
                                        if (nextIndex == 2)
                                        {
                                            brdv.success = true;
                                            brdv.ResultDataValue = "报告单项目查询完成，数据来源:TestGroupMicroCultureAssayAntibioticSusceptibilityExt";
                                            return brdv;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ZhiFang.Common.Log.Log.Debug("Micro异常信息:" + ex.ToString());
                                        if (nextIndex == 2)
                                        {
                                            brdv.success = false;
                                            brdv.ErrorInfo = "报告单项目查询失败，数据来源：TestGroupMicroCultureAssayAntibioticSusceptibilityExt;异常信息:" + ex.ToString();
                                            return brdv;
                                        }
                                    }
                                    break;
                                    #endregion
                                }
                            case ZhiFang.ReportFormQueryPrint.Common.SectionType.TestGroupMicroOtherTest:
                                {
                                    #region Micro
                                    try
                                    {
                                        dtri = rmfb.GetReportMicroGroupList(reportformid);
                                        if (nextIndex == 2)
                                        {
                                            brdv.success = true;
                                            brdv.ResultDataValue = "报告单项目查询完成，数据来源:TestGroupMicroOtherTest";
                                            return brdv;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ZhiFang.Common.Log.Log.Debug("Micro异常信息:" + ex.ToString());
                                        if (nextIndex == 2)
                                        {
                                            brdv.success = false;
                                            brdv.ErrorInfo = "报告单项目查询失败，数据来源：TestGroupMicroOtherTest;异常信息:" + ex.ToString();
                                            return brdv;
                                        }
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
                                if (nextIndex == 2)
                                {
                                    brdv.success = true;
                                    brdv.ResultDataValue = "报告单图片结果查询完成，数据来源:生化类（图）";
                                    return brdv;
                                }
                                break;
                            #endregion
                            case SectionType.MicroIncImage:
                                #region MicroIncImage
                                SetReportMicro(reportformid, ref dtri);
                                SetReportGraph(reportformid, ref dtrg);
                                if (nextIndex == 2)
                                {
                                    brdv.success = true;
                                    brdv.ResultDataValue = "报告单图片结果查询完成，数据来源:微生物（图）";
                                    return brdv;
                                }
                                break;
                            #endregion
                            case SectionType.CellMorphology:
                                #region CellMorphology:
                                SetReportMarrow(reportformid, ref dtri);
                                if (nextIndex == 2)
                                {
                                    brdv.success = true;
                                    brdv.ResultDataValue = "报告单结果查询完成，数据来源:骨髓、病例、细胞学";
                                    return brdv;
                                }
                                break;
                            #endregion
                            case SectionType.FishCheck:
                                #region FishCheck
                                SetReportMarrow(reportformid, ref dtri);
                                if (nextIndex == 2)
                                {
                                    brdv.success = true;
                                    brdv.ResultDataValue = "报告单结果查询完成，数据来源:骨髓、病例、细胞学";
                                    return brdv;
                                }
                                break;
                            #endregion
                            case SectionType.SensorCheck:
                                #region SensorCheck
                                SetReportItem(reportformid, ref dtri);
                                if (nextIndex == 2)
                                {
                                    brdv.success = true;
                                    brdv.ResultDataValue = "报告单结果查询完成，数据来源:普通生化";
                                    return brdv;
                                }
                                break;
                            #endregion
                            case SectionType.ChromosomeCheck:
                                #region ChromosomeCheck
                                SetReportMarrow(reportformid, ref dtri);
                                if (nextIndex == 2)
                                {
                                    brdv.success = true;
                                    brdv.ResultDataValue = "报告单结果查询完成，数据来源:骨髓、病例、细胞学";
                                    return brdv;
                                }
                                break;
                            #endregion
                            case SectionType.PathologyCheck:
                                #region PathologyCheck
                                SetReportMarrow(reportformid, ref dtri);
                                if (nextIndex == 2)
                                {
                                    brdv.success = true;
                                    brdv.ResultDataValue = "报告单结果查询完成，数据来源:骨髓、病例、细胞学";
                                    return brdv;
                                }
                                break;
                            #endregion
                            default:
                                SetReportItem(reportformid, ref dtri);
                                if (nextIndex == 2)
                                {
                                    brdv.success = true;
                                    brdv.ResultDataValue = "报告单结果查询完成，数据来源:普通生化";
                                    return brdv;
                                }
                                break;
                        }
                        #endregion
                        #region NextIndex3 查找模版
                        string TemplateFullPath = null;
                        string log = "";
                        string IsRFGraphdataPDf = "False";
                        TemplateFullPath = prfc.FindTemplate(dtrf.Rows[0], dtri, reportFormTitle, out log,out IsRFGraphdataPDf);

                        if (TemplateFullPath == null || TemplateFullPath.Trim() == "")
                        {
                            if (nextIndex == 3)
                            {
                                brdv.success = false;
                                brdv.ErrorInfo = log + "查找模板错误,未找到匹配的模版！";
                                return brdv;
                            }
                            throw new Exception("未找到匹配的模版！");
                        }
                        else
                        {
                            if (nextIndex == 3)
                            {
                                brdv.success = true;
                                brdv.ResultDataValue = log + "找到模板路径为：" + TemplateFullPath;
                                return brdv;
                            }
                        }

                        #endregion
                        #region NextIndex4 数据绑定生成报告
                        List<ReportFormFilesVO> reportpath = this.TemplateDataBindAndCreatReportForm(dtrf.Rows[0], dtri, dtrg, reportFormTitle, TemplateFullPath);
                        if (reportpath.Count > 0)
                        {
                            brf.UpdatePageInfo(reportformid, reportpath[0].PageCount, reportpath[0].PageName); //更新reportformfull表
                            reportformfileslist.AddRange(reportpath);
                            if (nextIndex == 4)
                            {
                                brdv.success = true;
                                brdv.ResultDataValue = "报告生成成功:" + reportformfileslist[0].PDFPath;
                                return brdv;
                            }
                        }
                    }
                    else
                    {
                        reportformfileslist.Add(tmpreportformfilesvo);
                        #region NextIndex 1
                        if (nextIndex == 1)
                        {
                            brdv.success = true;
                            brdv.ResultDataValue = "小组：" + dtrf.Rows[0]["SectionName"].ToString() + ",ReportFormID:" + reportformid + "的报告已经生成，并且不重新生成。";
                            return brdv;
                        }
                        #endregion
                    }
                    #endregion
                }
            }
            return brdv;
        }

        public List<ReportFormFilesVO> GetMergReportFromByReportFormIdList(string reportFormIdList, string sectionType)
        {
            ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BReportForm();
            Model.ReportForm brf_m = new Model.ReportForm();
            List<ReportFormFilesVO> reportformfileslist = new List<ReportFormFilesVO>();
            BReportMicro rmfb = new BReportMicro();
            PrintReportFormCommon prfc = new PrintReportFormCommon();
            string templatepath = System.AppDomain.CurrentDomain.BaseDirectory + "Template/NormalMerge.frx";
            switch (sectionType)
            {
                case "1":
                    List<string> idlist = reportFormIdList.Split(',').ToList();
                    #region 查找模版
                    if (!(System.IO.File.Exists(templatepath)))
                    {
                        throw new Exception("未找到生化类合并模版路径：Template/NormalMerge.frx。");
                    }
                    FastReport.Report report = new FastReport.Report();
                    ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".GetMergReportFromByReportFormIdList.读取模版开始");
                    report.Load(templatepath);
                    #endregion
                    #region 准备ReportFormAll数据

                    ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".GetMergReportFromByReportFormIdList.准备ReportFormAll数据开始");
                    DataSet dsreportformall = brf.GetReporFormAllByReportFormIdList(idlist, null, "1=1");
                    dsreportformall.Tables[0].TableName = "reportformall";
                    if (dsreportformall == null || dsreportformall.Tables.Count <= 0 || dsreportformall.Tables[0].Rows.Count <= 0)
                    {
                        throw new Exception("未找到报告数据：reportFormIdList：" + reportFormIdList);
                    }
                    #endregion
                    #region 添加电子签名图片路径列
                    if (!dsreportformall.Tables[0].Columns.Contains("CollecterImageFilePath"))//报告库视图存在电子签名路径跳过此步骤
                    {
                        ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".GetMergReportFromByReportFormIdList.准备电子签名数据开始");
                        List<string> PUserList = new List<string>();
                        List<string> tmp = new List<string>() { "Collecter", "Incepter", "Technician", "Operator", "Checker" };
                        foreach (var t in tmp)
                        {
                            dsreportformall.Tables[0].Columns.Add(t+"ImageFilePath");
                        }

                        for (int i = 0; i < dsreportformall.Tables[0].Rows.Count; i++)
                        {
                            foreach (var t in tmp)
                            {
                                if (!PUserList.Contains(dsreportformall.Tables[0].Rows[i][t].ToString().Trim()))
                                {
                                    PUserList.Add(dsreportformall.Tables[0].Rows[i][t].ToString().Trim());
                                }
                            }                            
                        }
                        BPUser bpuser = new BPUser();
                        DataSet dsbpuser = bpuser.GetListByPUserNameList(PUserList);
                        if (dsbpuser == null || dsbpuser.Tables.Count <= 0 || dsbpuser.Tables[0].Rows.Count <= 0)
                        {
                            throw new Exception("未找到报告内各个操作者数据：PUserList：" + string.Join(",", PUserList));
                        }
                        for (int i = 0; i < dsreportformall.Tables[0].Rows.Count; i++)
                        {
                            foreach (var t in tmp)
                            {
                                if (dsbpuser.Tables[0].Select(" CName= '" + dsreportformall.Tables[0].Rows[i][t].ToString().Trim() + "'").Count() > 0)
                                    dsreportformall.Tables[0].Rows[i][t+"ImageFilePath"] = dsbpuser.Tables[0].Select(" CName= '" + dsreportformall.Tables[0].Rows[i][t].ToString().Trim() + "'").ElementAt(0)["FilePath"].ToString();
                            }
                        }

                    }

                    #endregion

                    #region 绑定数据
                    ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".GetMergReportFromByReportFormIdList.注册数据开始");
                    report.RegisterData(dsreportformall);
                    #endregion
                    PrintReportFormCommon.eSet.ReportSettings.ShowProgress = false;
                    report.Prepare();
                    string reportformfiletype = "PDF";

                    #region 生成报告
                    ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".CreatReportFormFilesByFRX.生成报告文件开始");
                    ReportFormFilesVO tmpvo = new ReportFormFilesVO();
                    ReportFormPDFExport tmppdf = new ReportFormPDFExport();

                    string reportformfileallpath = System.AppDomain.CurrentDomain.BaseDirectory+ SysContractPara.ReportFormFilePath+DateTime.Now.ToString("yyyy-MM-dd")+@"\";
                    
                    string filename= +GUIDHelp.GetGUIDLong() + "_" + dsreportformall.Tables[0].Rows[0]["CName"].ToString() + "." + reportformfiletype;

                    if (!System.IO.Directory.Exists(reportformfileallpath))
                    {
                        System.IO.Directory.CreateDirectory(reportformfileallpath);
                    }
                    report.Export(tmppdf, reportformfileallpath+ filename);

                    tmpvo.PDFPath = SysContractPara.ReportFormFilePath + DateTime.Now.ToString("yyyy-MM-dd") + @"\" + filename;
                    tmpvo.PageCount = tmppdf.PageCount.ToString();
                    float h = ((FastReport.ReportPage)report.FindObject("Page1")).PaperHeight;
                    float w = ((FastReport.ReportPage)report.FindObject("Page1")).PaperWidth;
                    //tmpvo.PageName = PageTypeCheck(h, w);
                    //tmpvo.ReportFormID = reportform["ReportFormID"].ToString();
                    reportformfileslist.Add(tmpvo);
                    ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".CreatReportFormFilesByFRX.释放报告模版开始");
                    report.Dispose();
                    tmppdf.Dispose();
                    #endregion
                    break;
            }
            return reportformfileslist;
        }

        public List<ReportFormFilesVO> CreatReportFormFilesBYSpid(List<string> reportformidlist, ReportFormTitle reportFormTitle, ReportFormFileType reportFormFileType, string st, int flag , string Template, int pow = -1)
        {
            ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BReportForm();
            Model.ReportForm brf_m = new Model.ReportForm();
            List<ReportFormFilesVO> reportformfileslist = new List<ReportFormFilesVO>();
            BReportMicro rmfb = new BReportMicro();
            PrintReportFormCommon prfc = new PrintReportFormCommon();
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
                        #region 保密等级
                        //默认不使用
                        if (pow > -1)
                        {
                            //prfc.secrecyGrade(pow, ref dtri);
                        }
                        #endregion

                        #region 查找模版
                        string TemplateFullPath = null;
                        //string log = "";
                        //TemplateFullPath = prfc.FindTemplate(dtrf.Rows[0], dtri, reportFormTitle, out log);
                        TemplateFullPath = Template;
                        TemplateFullPath = TemplateFullPath.Substring(TemplateFullPath.IndexOf(".exe") + 4);
                        TemplateFullPath = TemplateFullPath.Replace("modelprint.exe ", "").Replace("printrmf.exe ", "").Replace(".frf", "").Replace(".fr3", "").Replace(".rmf", "");
                        TemplateFullPath = System.AppDomain.CurrentDomain.BaseDirectory + SysContractPara.TemplatePath + TemplateFullPath.Trim() + SysContractPara.PrintTemplatextension;
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

        public List<ReportFormFilesVO> GetSampleReportFromByReportFormIdList(string reportFormIdList, string sectionType) {


            ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BReportForm();
            Model.ReportForm brf_m = new Model.ReportForm();
            List<ReportFormFilesVO> reportformfileslist = new List<ReportFormFilesVO>();
            BReportMicro rmfb = new BReportMicro();
            PrintReportFormCommon prfc = new PrintReportFormCommon();
            string templatepath = System.AppDomain.CurrentDomain.BaseDirectory + "Template/样本清单打印.frx";
            
            List<string> idlist = reportFormIdList.Split(',').ToList();
            #region 查找模版
            if (!(System.IO.File.Exists(templatepath)))
            {
                throw new Exception("未找到样本清单打印模版路径：Template/样本清单打印.frx");
            }
            FastReport.Report report = new FastReport.Report();
            ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".GetSampleReportFromByReportFormIdList.读取模版开始");
            report.Load(templatepath);
            #endregion
            #region 准备ReportForm数据

            ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".GetSampleReportFromByReportFormIdList.准备ReportForm数据开始");
            DataSet dsreportformall = brf.GetSampleReportFromByReportFormID(idlist, null, "1=1");
            dsreportformall.Tables[0].TableName = "reportformall";
            if (dsreportformall == null || dsreportformall.Tables.Count <= 0 || dsreportformall.Tables[0].Rows.Count <= 0)
            {
                throw new Exception("未找到报告数据：reportFormIdList：" + reportFormIdList);
            }
            #endregion
            

            #region 绑定数据
            ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".GetSampleReportFromByReportFormIdList.注册数据开始");
            report.RegisterData(dsreportformall);
            #endregion
            PrintReportFormCommon.eSet.ReportSettings.ShowProgress = false;
            report.Prepare();
            string reportformfiletype = "PDF";

            #region 生成报告
            ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".CreatReportFormFilesByFRX.生成报告文件开始");
            ReportFormFilesVO tmpvo = new ReportFormFilesVO();
            ReportFormPDFExport tmppdf = new ReportFormPDFExport();

            string reportformfileallpath = System.AppDomain.CurrentDomain.BaseDirectory + SysContractPara.ReportFormFilePath+ "sampleDetailedList"+@"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\";

            string filename = "";
            if (dsreportformall.Tables[0].Rows[0]["client"].ToString() != null && dsreportformall.Tables[0].Rows[0]["client"].ToString() != "")
            {
                filename = +GUIDHelp.GetGUIDLong() + "_" + dsreportformall.Tables[0].Rows[0]["client"].ToString() + "." + reportformfiletype;
            }
            else {
                filename = +GUIDHelp.GetGUIDLong() + "." + reportformfiletype;
            }

            

            if (!System.IO.Directory.Exists(reportformfileallpath))
            {
                System.IO.Directory.CreateDirectory(reportformfileallpath);
            }
            report.Export(tmppdf, reportformfileallpath + filename);

            tmpvo.PDFPath = SysContractPara.ReportFormFilePath + "sampleDetailedList" + @"\"  + DateTime.Now.ToString("yyyy-MM-dd") + @"\" + filename;
            tmpvo.PageCount = tmppdf.PageCount.ToString();
            float h = ((FastReport.ReportPage)report.FindObject("Page1")).PaperHeight;
            float w = ((FastReport.ReportPage)report.FindObject("Page1")).PaperWidth;
            //tmpvo.PageName = PageTypeCheck(h, w);
            //tmpvo.ReportFormID = reportform["ReportFormID"].ToString();
            reportformfileslist.Add(tmpvo);
            ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".CreatReportFormFilesByFRX.释放报告模版开始");
            report.Dispose();
            tmppdf.Dispose();
            #endregion
            
            return reportformfileslist;

        }

        /// <summary>
        /// 生成PDF报告(天津血研所定制)
        /// </summary>
        /// <param name="reportformidlist"></param>
        /// <param name="reportFormTitle"></param>
        /// <param name="reportFormFileType"></param>
        /// <param name="st">小组类型</param>
        /// <param name="flag">0:判断存在则不重新生成,1:强制重新生成.</param>
        /// <returns>List<ReportFormFilesVO></returns>
        public List<ReportFormFilesVO> TianJinXueYanSuoCreatReportFormFiles(List<string> reportformidlist, ReportFormTitle reportFormTitle, ReportFormFileType reportFormFileType, string st, int flag = 0, int pow = -1)
        {
            ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BReportForm();
            Model.ReportForm brf_m = new Model.ReportForm();
            List<ReportFormFilesVO> reportformfileslist = new List<ReportFormFilesVO>();
            BReportMicro rmfb = new BReportMicro();
            PrintReportFormCommon prfc = new PrintReportFormCommon();
            foreach (var reportformid in reportformidlist)
            {

                brf_m.FormNo = reportformid;
                DataTable dtrf = brf.GetListByDataSource(reportformid);
                if (dtrf != null && dtrf.Rows.Count > 0)
                {
                    if (dtrf.Rows[0]["zdy15"] != null && dtrf.Rows[0]["zdy15"].ToString().Trim() != "")
                    {
                        ReportFormFilesVO tmpreportformfilesvo = new ReportFormFilesVO();
                        tmpreportformfilesvo = tianjinxueyansuoGetReportFromGeneByReportFormIdList(reportformid, dtrf.Rows[0]["SectionType"].ToString());
                        reportformfileslist.Add(tmpreportformfilesvo);
                    }
                    else
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
                                    SetReportMarrow(reportformid, ref dtri);
                                    SetS_RequestVItemReportGraph(reportformid, ref dtrg);
                                    break;
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
                            #region 保密等级
                            //默认不使用
                            if (pow > -1)
                            {
                                //prfc.secrecyGrade(pow, ref dtri);
                            }
                            #endregion

                            #region 查找模版
                            string TemplateFullPath = null;
                            string log = "";
                            string IsRFGraphdataPDf = "False";
                            TemplateFullPath = prfc.FindTemplate(dtrf.Rows[0], dtri, reportFormTitle, out log,out IsRFGraphdataPDf);
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
                            ZhiFang.Common.Log.Log.Debug("CreatReportFormFiles.reportformid:" + reportformid + ";找到文件：" + tmpreportformfilesvo.ReportFormID + ";PDFPath:" + tmpreportformfilesvo.PDFPath);
                            reportformfileslist.Add(tmpreportformfilesvo);
                        }

                    }

                    #endregion
                }
            }
            return reportformfileslist;
        }

        /// <summary>
        /// 天津血研所定制服务打印基因报告
        /// </summary>
        /// <param name="reportFormIdList"></param>
        /// <param name="sectionType"></param>
        /// <returns></returns>
        public ReportFormFilesVO tianjinxueyansuoGetReportFromGeneByReportFormIdList(string reportFormId, string sectionType)
        {
            ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BReportForm();
            ZhiFang.ReportFormQueryPrint.BLL.BReportItem bri = new BReportItem();
            Model.ReportForm brf_m = new Model.ReportForm();
            ReportFormFilesVO tmpvo = new ReportFormFilesVO();
            PrintReportFormCommon prfc = new PrintReportFormCommon();

            string templatepath = "";
            switch (sectionType)
            {
                case "1":


                    #region 准备数据

                    ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".tianjinxueyansuoGetReportFromGeneByReportFormIdList.准备数据开始");
                    //获取本人的信息
                    DataTable reportformall = brf.GetListByDataSource(reportFormId);
                    //获取本人以及亲属的ReportFormID
                    //DataSet dsreportform = brf.GetRepotFormByReportFormIDGroupByZdy15(reportformall.Rows[0]["PatNo"].ToString(), reportformall.Rows[0]["zdy15"].ToString());
                    DataSet dsreportform = brf.GetRepotFormByReportFormIDAndZdy15AndReceiveDate(reportformall.Rows[0]["PatNo"].ToString(), reportformall.Rows[0]["zdy15"].ToString(), reportformall.Rows[0]["ReceiveDate"].ToString());
                    List<string> idlist = new List<string>();
                    for (var i = 0; i < dsreportform.Tables[0].Rows.Count; i++)
                    {
                        idlist.Add(dsreportform.Tables[0].Rows[i]["FormNo"].ToString());
                    }
                    //DataSet reportitemall = bri.GetReportItemList_DataSet(idlist);
                    DataSet reportitemall = bri.GetReportItemListSort_DataSet(idlist, "disporder");
                    if (reportitemall == null || reportitemall.Tables.Count <= 0 || reportitemall.Tables[0].Rows.Count <= 0)
                    {
                        throw new Exception("未找到报告数据：reportFormIdList：" + reportFormId);
                    }
                    
                    //创建新的DataSet
                    DataTable fritemTable = new DataTable();
                    fritemTable.TableName = "fritem";
                    fritemTable.Columns.Add("SampleNo");
                    fritemTable.Columns.Add("ZDY6");
                    fritemTable.Columns.Add("ZDY7");
                    fritemTable.Columns.Add("ZDY8");
                    fritemTable.Columns.Add("Formcomment2");
                    fritemTable.Columns.Add("ItemCname1");
                    fritemTable.Columns.Add("ItemCname2");
                    fritemTable.Columns.Add("ItemCname3");
                    fritemTable.Columns.Add("ItemCname4");
                    fritemTable.Columns.Add("ItemCname5");
                    fritemTable.Columns.Add("ItemCname6");
                    fritemTable.Columns.Add("ReportDesc1");
                    fritemTable.Columns.Add("ReportDesc2");
                    fritemTable.Columns.Add("ReportDesc3");
                    fritemTable.Columns.Add("ReportDesc4");
                    fritemTable.Columns.Add("ReportDesc5");
                    fritemTable.Columns.Add("ReportDesc6");
                    fritemTable.Columns.Add("ReportDesc7");
                    fritemTable.Columns.Add("ReportDesc8");
                    fritemTable.Columns.Add("ReportDesc9");
                    fritemTable.Columns.Add("ReportDesc10");
                    fritemTable.Columns.Add("ReportDesc11");
                    fritemTable.Columns.Add("ReportDesc12");

                    //判断用哪个模板
                    if (reportitemall.Tables[0].Rows[0]["ParItemNo"].ToString() == "90008713")
                    {
                        templatepath = System.AppDomain.CurrentDomain.BaseDirectory + "Template/GeneMergeGaoFen.frx";
                        for (var i = 0; i < dsreportform.Tables[0].Rows.Count; i++)
                        {
                            DataRow NewRow = fritemTable.NewRow();
                            NewRow["SampleNo"] = dsreportform.Tables[0].Rows[i]["SampleNo"];
                            NewRow["ZDY6"] = dsreportform.Tables[0].Rows[i]["ZDY6"];
                            NewRow["ZDY7"] = dsreportform.Tables[0].Rows[i]["ZDY7"];
                            NewRow["ZDY8"] = dsreportform.Tables[0].Rows[i]["ZDY8"];
                            NewRow["Formcomment2"] = dsreportform.Tables[0].Rows[i]["Formcomment2"];

                            DataRow[] itemtable = new DataRow[6];
                            itemtable = reportitemall.Tables[0].Select("SampleNo = '" + dsreportform.Tables[0].Rows[i]["SampleNo"].ToString() + "'");
                            if(itemtable != null)
                            {
                                for (var a = 0; a < 6; a++)
                                {
                                    if (a == 0)
                                    {
                                        NewRow["ItemCname1"] = itemtable[a]["ItemCname"];
                                        NewRow["ReportDesc1"] = itemtable[a]["ReportDesc"].ToString().Split(';')[0];
                                        NewRow["ReportDesc2"] = itemtable[a]["ReportDesc"].ToString().Split(';')[1];
                                    }
                                    else if (a == 1)
                                    {
                                        NewRow["ItemCname2"] = itemtable[a]["ItemCname"];
                                        NewRow["ReportDesc3"] = itemtable[a]["ReportDesc"].ToString().Split(';')[0];
                                        NewRow["ReportDesc4"] = itemtable[a]["ReportDesc"].ToString().Split(';')[1];
                                    }
                                    else if (a == 2)
                                    {
                                        NewRow["ItemCname3"] = itemtable[a]["ItemCname"];
                                        NewRow["ReportDesc5"] = itemtable[a]["ReportDesc"].ToString().Split(';')[0];
                                        NewRow["ReportDesc6"] = itemtable[a]["ReportDesc"].ToString().Split(';')[1];
                                    }
                                    else if (a == 3)
                                    {
                                        NewRow["ItemCname4"] = itemtable[a]["ItemCname"];
                                        NewRow["ReportDesc7"] = itemtable[a]["ReportDesc"].ToString().Split(';')[0];
                                        NewRow["ReportDesc8"] = itemtable[a]["ReportDesc"].ToString().Split(';')[1];
                                    }
                                    else if (a == 4)
                                    {
                                        NewRow["ItemCname5"] = itemtable[a]["ItemCname"];
                                        NewRow["ReportDesc9"] = itemtable[a]["ReportDesc"].ToString().Split(';')[0];
                                        NewRow["ReportDesc10"] = itemtable[a]["ReportDesc"].ToString().Split(';')[1];
                                    }
                                    else if (a == 5)
                                    {
                                        NewRow["ItemCname6"] = itemtable[a]["ItemCname"];
                                        NewRow["ReportDesc11"] = itemtable[a]["ReportDesc"].ToString().Split(';')[0];
                                        NewRow["ReportDesc12"] = itemtable[a]["ReportDesc"].ToString().Split(';')[1];
                                    }
                                }
                            }                            
                            fritemTable.Rows.Add(NewRow);
                        }
                    }
                    else if (reportitemall.Tables[0].Rows[0]["ParItemNo"].ToString() == "90008712")
                    {
                        templatepath = System.AppDomain.CurrentDomain.BaseDirectory + "Template/GeneMergeDiFen.frx";
                        for (var i = 0; i < dsreportform.Tables[0].Rows.Count; i++)
                        {
                            DataRow NewRow = fritemTable.NewRow();
                            NewRow["SampleNo"] = dsreportform.Tables[0].Rows[i]["SampleNo"];
                            NewRow["ZDY6"] = dsreportform.Tables[0].Rows[i]["ZDY6"];
                            NewRow["ZDY7"] = dsreportform.Tables[0].Rows[i]["ZDY7"];
                            NewRow["ZDY8"] = dsreportform.Tables[0].Rows[i]["ZDY8"];
                            NewRow["Formcomment2"] = dsreportform.Tables[0].Rows[i]["Formcomment2"];
                            DataRow[] itemtable = new DataRow[3];
                            itemtable = reportitemall.Tables[0].Select("SampleNo = '" + dsreportform.Tables[0].Rows[i]["SampleNo"].ToString() + "'");
                            if (itemtable != null)
                            {
                                for (var a = 0; a < 3; a++)
                                {
                                    if (a == 0)
                                    {
                                        NewRow["ItemCname1"] = itemtable[a]["ItemCname"];
                                        NewRow["ReportDesc1"] = itemtable[a]["ReportDesc"].ToString().Split(';')[0];
                                        NewRow["ReportDesc2"] = itemtable[a]["ReportDesc"].ToString().Split(';')[1];
                                    }
                                    else if (a == 1)
                                    {
                                        NewRow["ItemCname2"] = itemtable[a]["ItemCname"];
                                        NewRow["ReportDesc3"] = itemtable[a]["ReportDesc"].ToString().Split(';')[0];
                                        NewRow["ReportDesc4"] = itemtable[a]["ReportDesc"].ToString().Split(';')[1];
                                    }
                                    else if (a == 2)
                                    {
                                        NewRow["ItemCname3"] = itemtable[a]["ItemCname"];
                                        NewRow["ReportDesc5"] = itemtable[a]["ReportDesc"].ToString().Split(';')[0];
                                        NewRow["ReportDesc6"] = itemtable[a]["ReportDesc"].ToString().Split(';')[1];
                                    }
                                }
                            }
                            fritemTable.Rows.Add(NewRow);
                        }
                    }
                    else {
                        throw new Exception("未找到正确的医嘱项目ID：ParItemNo" );
                    }

                    DataSet fritemSet = new DataSet();
                    fritemSet.Tables.Add(fritemTable);
                    reportformall.TableName = "frform";
                    #endregion



                    #region 查找模版
                    if (!(System.IO.File.Exists(templatepath)))
                    {
                        throw new Exception("未找到基因合并模版路径。");
                    }
                    FastReport.Report report = new FastReport.Report();
                    ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".tianjinxueyansuoGetReportFromGeneByReportFormIdList.读取模版开始");
                    report.Load(templatepath);
                    #endregion

                    #region 添加电子签名图片路径列
                    if (!reportformall.Columns.Contains("CollecterImageFilePath"))//报告库视图存在电子签名路径跳过此步骤
                    {
                        ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".tianjinxueyansuoGetReportFromGeneByReportFormIdList.准备电子签名数据开始");
                        List<string> PUserList = new List<string>();
                        List<string> tmp = new List<string>() { "Collecter", "Incepter", "Technician", "Operator", "Checker" };
                        foreach (var t in tmp)
                        {
                            reportformall.Columns.Add(t + "ImageFilePath");
                        }

                        for (int i = 0; i < reportformall.Rows.Count; i++)
                        {
                            foreach (var t in tmp)
                            {
                                if (!PUserList.Contains(reportformall.Rows[i][t].ToString().Trim()))
                                {
                                    PUserList.Add(reportformall.Rows[i][t].ToString().Trim());
                                }
                            }
                        }
                        BPUser bpuser = new BPUser();
                        DataSet dsbpuser = bpuser.GetListByPUserNameList(PUserList);
                        if (dsbpuser == null || dsbpuser.Tables.Count <= 0 || dsbpuser.Tables[0].Rows.Count <= 0)
                        {
                            throw new Exception("未找到报告内各个操作者数据：PUserList：" + string.Join(",", PUserList));
                        }
                        for (int i = 0; i < reportformall.Rows.Count; i++)
                        {
                            foreach (var t in tmp)
                            {
                                if (dsbpuser.Tables[0].Select(" CName= '" + reportformall.Rows[i][t].ToString().Trim() + "'").Count() > 0)
                                    reportformall.Rows[i][t + "ImageFilePath"] = dsbpuser.Tables[0].Select(" CName= '" + reportformall.Rows[i][t].ToString().Trim() + "'").ElementAt(0)["FilePath"].ToString();
                            }
                        }

                    }

                    #endregion

                    #region 绑定数据
                    ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".tianjinxueyansuoGetReportFromGeneByReportFormIdList.注册数据开始");
                    
                    report.RegisterData(reportformall.DataSet);
                    report.RegisterData(fritemSet);
                    #endregion
                    PrintReportFormCommon.eSet.ReportSettings.ShowProgress = false;
                    report.Prepare();
                    string reportformfiletype = "PDF";

                    #region 生成报告
                    ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".tianjinxueyansuoGetReportFromGeneByReportFormIdList.生成报告文件开始");
                    
                    ReportFormPDFExport tmppdf = new ReportFormPDFExport();

                    string reportformfileallpath = System.AppDomain.CurrentDomain.BaseDirectory + SysContractPara.ReportFormFilePath + DateTime.Now.ToString("yyyy-MM-dd") + @"\";

                    string filename = +GUIDHelp.GetGUIDLong() + "_" + reportformall.Rows[0]["CName"].ToString() + "." + reportformfiletype;

                    if (!System.IO.Directory.Exists(reportformfileallpath))
                    {
                        System.IO.Directory.CreateDirectory(reportformfileallpath);
                    }
                    report.Export(tmppdf, reportformfileallpath + filename);

                    tmpvo.PDFPath = SysContractPara.ReportFormFilePath + DateTime.Now.ToString("yyyy-MM-dd") + @"\" + filename;
                    tmpvo.PageCount = tmppdf.PageCount.ToString();
                    float h = ((FastReport.ReportPage)report.FindObject("Page1")).PaperHeight;
                    float w = ((FastReport.ReportPage)report.FindObject("Page1")).PaperWidth;
                    //tmpvo.PageName = PageTypeCheck(h, w);
                    //tmpvo.ReportFormID = reportform["ReportFormID"].ToString();
                   
                    ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".tianjinxueyansuoGetReportFromGeneByReportFormIdList.释放报告模版开始");
                    report.Dispose();
                    tmppdf.Dispose();
                    #endregion
                    break;
            }
            return tmpvo;
        }

        public List<ReportFormFilesVO> LabStarCreatReportFormFiles(List<string> reportformidlist, ReportFormTitle reportFormTitle, ReportFormFileType reportFormFileType, string st, long LabId, int flag = 0, int pow = -1)
        {
            ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BReportForm();
            Model.ReportForm brf_m = new Model.ReportForm();
            List<ReportFormFilesVO> reportformfileslist = new List<ReportFormFilesVO>();
            BReportMicro rmfb = new BReportMicro();
            PrintReportFormCommon prfc = new PrintReportFormCommon();
            foreach (var reportformid in reportformidlist)
            {
                ZhiFang.Common.Log.Log.Debug("LabStarCreatReportFormFiles.ReportFormID:" + reportformid);
                brf_m.FormNo = reportformid;
                DataTable dtrf = brf.GetListByDataSource(reportformid);
                if (dtrf.Columns.Contains("bRevised") && dtrf.Rows[0]["bRevised"].ToString() == "1")
                {
                    flag = 1;
                }
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
                                SetReportMarrow(reportformid, ref dtri);
                                SetReportGraph(reportformid, ref dtrg);
                                break;
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
                        #region 保密等级
                        //默认不使用
                        if (pow > -1)
                        {
                            //prfc.secrecyGrade(pow, ref dtri);
                        }
                        #endregion

                        #region 查找模版
                        string TemplateFullPath = null;
                        string log = "";
                        string IsRFGraphdataPDf = "False";
                        TemplateFullPath = prfc.FindTemplate(dtrf.Rows[0], dtri, null, reportFormTitle, out log, out IsRFGraphdataPDf, LabId);
                        if (IsRFGraphdataPDf.Equals("True"))
                        {
                            ZhiFang.Common.Log.Log.Debug("第三方外送项目不需要通过FRX模板生成，将获取数据库外送PDF");
                        }
                        else
                        {
                            if (TemplateFullPath == null || TemplateFullPath.Trim() == "")
                            {
                                throw new Exception("未找到匹配的模版！");
                            }
                        }
                        #endregion

                        #region 数据绑定生成报告
                        List<ReportFormFilesVO> reportpath = new List<ReportFormFilesVO>();
                        //判断是否从第三方获取PDF
                        if (IsRFGraphdataPDf.Equals("True"))
                        {
                            BRFGraphData bgd = new BRFGraphData();
                            string[] p = reportformid.Split(';');
                            if (p.Length >= 4)
                            {
                                DataSet dsrg = bgd.GetList(" ReceiveDate='" + p[0] + "' and SectionNo=" + p[1] + " and TestTypeNo=" + p[2] + " and SampleNo='" + p[3] + "' ");
                                dtrg = dsrg.Tables[0];

                            }
                            else
                            {
                                string IsUseMergePDF = ConfigHelper.GetConfigString("IsUseMergePDF");
                                if (IsUseMergePDF != null && IsUseMergePDF.Equals("1"))
                                {
                                    DataSet dsrg = bgd.GetList(" ReportPublicationID='" + reportformid + "' ");
                                    dtrg = dsrg.Tables[0];
                                }
                                else
                                {
                                    throw new Exception("查找第三方报告失败报告ID错误.reportformid:" + reportformid);
                                }
                            }
                            PrintReportFormCommon prfcommon = new PrintReportFormCommon();
                            reportpath = prfcommon.CreatReportFormFilesBybyte(dtrf.Rows[0], dtri, dtrg, TemplateFullPath, reportFormTitle);
                        }
                        else
                        {
                            reportpath = this.TemplateDataBindAndCreatReportForm(dtrf.Rows[0], dtri, dtrg, reportFormTitle, TemplateFullPath);

                        }
                        if (reportpath.Count > 0)
                        {
                            brf.UpdatePageInfo(reportformid, reportpath[0].PageCount, reportpath[0].PageName); //更新reportformfull表
                            reportformfileslist.AddRange(reportpath);
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("LabStarCreatReportFormFiles.reportformid:" + reportformid + ";找到文件：" + tmpreportformfilesvo.ReportFormID + ";PDFPath:" + tmpreportformfilesvo.PDFPath);
                        reportformfileslist.Add(tmpreportformfilesvo);
                    }

                    #endregion
                }
            }
            return reportformfileslist;
        }
    }
}
