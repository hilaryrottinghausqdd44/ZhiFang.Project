using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.IBLL.Common;
using ZhiFang.DALFactory;
using ZhiFang.Common.Dictionary;
using System.Runtime.InteropServices;
using FastReport;
using System.Data;
using System.IO;
using System.Configuration;
using System.Web.UI;
using System.Web;
using ZhiFang.Common.Public;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.BLLFactory;
using ZhiFang.Model;
using ZhiFang.IBLL.Report;
using System.Collections;

namespace ZhiFang.BLL.Report
{
    public class PrintUseDeliph : ShowFrom, ZhiFang.IBLL.Report.IBPrintFrom_Weblis
    {
        
        ZhiFang.BLL.Report.ReportFormFull rffb = new ReportFormFull();
        ZhiFang.BLL.Report.ReportItemFull rifb = new ReportItemFull();
        ZhiFang.BLL.Report.ReportMicroFull rmfb = new ReportMicroFull();
        ZhiFang.BLL.Report.ReportMarrowFull rmarrowfb = new ReportMarrowFull();
        private readonly IBPGroup ibpg = BLLFactory<IBPGroup>.GetBLL();
        private readonly IBItemMergeRule ibim = BLLFactory<IBItemMergeRule>.GetBLL();
        private readonly IBClientProfile ibcp = BLLFactory<IBClientProfile>.GetBLL();
        private ZhiFang.Common.Dictionary.ReportFormTitle titelflag = ZhiFang.Common.Dictionary.ReportFormTitle.center;
        private readonly IDReportFormMerge dalmerge = DalFactory<IDReportFormMerge>.GetDalByClassName("ReportFormMerge");
        ZhiFang.IBLL.Common.BaseDictionary.IBPGroupPrint PGroupPrint = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBPGroupPrint>.GetBLL();
        ZhiFang.IBLL.Common.BaseDictionary.IBPrintFormat pfb = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBPrintFormat>.GetBLL();

        public ReportFormTitle TitleFlag
        {
            set { titelflag = value; }
            get { return titelflag; }
        }

        
        public List<string> PrintHtml1(string FormNo, ReportFormTitle Flag,string whereStr)
        {
            try
            {
                Model.ReportFormFull rff_m = new Model.ReportFormFull();
                Model.ReportItemFull rif_m = new Model.ReportItemFull();
                Model.ReportMicroFull rmf_m = new Model.ReportMicroFull();
                Model.ReportMarrowFull rmarrowf_m = new Model.ReportMarrowFull();
                rff_m.ReportFormID = FormNo;
                DataSet dsrf = rffb.GetList(rff_m);
                DataTable dtri = new DataTable();
                DataSet dsri = new DataSet();
                string modelType = "";
                string modelname = "";
                bool result = false;
                switch ((SectionType)Convert.ToInt32(dsrf.Tables[0].Rows[0]["SECTIONTYPE"].ToString()))
                {
                    case SectionType.all:
                        #region Normal
                        rff_m.ReportFormID = FormNo;
                        dsri = rifb.GetList(rif_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "CHAM";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                        }
                        break;
                        #endregion
                    case SectionType.Normal:
                        #region Normal
                        rif_m.ReportFormID = FormNo;
                        dsri = rifb.GetList(rif_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "CHAM";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        string sub = System.IO.Path.GetFileName(modelname);
                        string str1 = System.IO.Path.GetDirectoryName(modelname);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                            //return "暂无匹配模板！";
                        }
                        break;
                        #endregion
                    case SectionType.Micro:
                        #region Micro
                        rmf_m.ReportFormID = FormNo;
                        dsri = rmfb.GetList(rmf_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "MICROBE";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                            //return "暂无匹配模板！";
                        }
                        break;
                        #endregion
                    case SectionType.NormalIncImage:
                        #region NormalIncImage
                        rif_m.ReportFormID = FormNo;
                        dsri = rifb.GetList(rif_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "CHAM";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                            //return "暂无匹配模板！";
                        }
                        break;
                        #endregion
                    case SectionType.MicroIncImage:
                        #region MicroIncImage
                        rmf_m.ReportFormID = FormNo;
                        dsri = rmfb.GetList(rmf_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "MICROBE";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                            //return "暂无匹配模板！";
                        }
                        break;
                        #endregion
                    case SectionType.CellMorphology:
                        #region CellMorphology
                        rmarrowf_m.ReportFormID = FormNo;
                        dsri = rmarrowfb.GetList(rmarrowf_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "MARROW";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                            //return "暂无匹配模板！";
                        }
                        break;
                        #endregion
                    case SectionType.FishCheck:
                        #region FishCheck
                        rmarrowf_m.ReportFormID = FormNo;
                        dsri = rmarrowfb.GetList(rmarrowf_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "MARROW";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                            //return "暂无匹配模板！";
                        }
                        break;
                        #endregion
                    case SectionType.SensorCheck:
                        #region SensorCheck
                        rmarrowf_m.ReportFormID = FormNo;
                        dsri = rmarrowfb.GetList(rmarrowf_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "MARROW";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                            //return "暂无匹配模板！";
                        }
                        break;
                        #endregion
                    case SectionType.ChromosomeCheck:
                        #region ChromosomeCheck
                        rmarrowf_m.ReportFormID = FormNo;
                        dsri = rmarrowfb.GetList(rmarrowf_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "MARROW";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                            //return "暂无匹配模板！";
                        }
                        break;
                        #endregion
                    case SectionType.PathologyCheck:
                        #region PathologyCheck
                        rmarrowf_m.ReportFormID = FormNo;
                        dsri = rmarrowfb.GetList(rmarrowf_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "MARROW";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                            //return "暂无匹配模板！";
                        }
                        break;
                        #endregion
                }
                // string SaveType = "pdf";
                string SaveType = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFileType");
                IntPtr ConnectionString = Marshal.StringToHGlobalAnsi(ConfigurationManager.ConnectionStrings["WebLisDB"].ToString());
                IntPtr modelNameDeliph = Marshal.StringToHGlobalAnsi(modelname);
                IntPtr modelTypeDeliph = Marshal.StringToHGlobalAnsi(modelType);
                IntPtr PrintID = Marshal.StringToHGlobalAnsi(FormNo);
                IntPtr saveType = Marshal.StringToHGlobalAnsi(SaveType);
                IntPtr SavePath = Marshal.StringToHGlobalAnsi(System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"));
                IntPtr Path = Marshal.StringToHGlobalAnsi(GetImagePath(dsrf.Tables[0].Rows[0]));
                IntPtr LogPath = Marshal.StringToHGlobalAnsi(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\");
                IntPtr where = Marshal.StringToHGlobalAnsi(whereStr);
                ZhiFang.Common.Log.Log.Info("开始调用ModelPrint.dll");
                result = PrintReport(ConnectionString, modelNameDeliph, modelTypeDeliph, PrintID, saveType, SavePath, Path, LogPath, where);
                if (result)
                {string aaa="";
                    ZhiFang.Common.Log.Log.Info("生成报告成功");
                    if (SaveType=="jpg")
                    {
                    aaa = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + FormNo.Replace(" 00:00:00", "000000").Replace(":", "") + "/1.1." + SaveType + "";
                    }else
                    {
                     aaa = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + FormNo.Replace(" 00:00:00", "000000").Replace(":", "") + "." + SaveType + "";
                    }
                    List<string> l = new List<string>();
                    if (SaveType == "jpg")
                    {
                        l.Add(ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + FormNo.Replace(" 00:00:00", "00:00:00").Replace(":", "") + "/1.1." + SaveType + "");
                    }
                    else
                    {
                        l.Add(ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + FormNo.Replace(" 00:00:00", "00:00:00").Replace(":", "") + "." + SaveType + "");
                    }
                        return l;
                }
                return null;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString() + "---------" + e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                return null;
            }
        }
        public List<string> PrintHtml(string FormNo, ReportFormTitle Flag, string ResultFlag)
        {
            try
            {
                Model.ReportFormFull rff_m = new Model.ReportFormFull();
                Model.ReportItemFull rif_m = new Model.ReportItemFull();
                Model.ReportMicroFull rmf_m = new Model.ReportMicroFull();
                Model.ReportMarrowFull rmarrowf_m = new Model.ReportMarrowFull();
                rff_m.ReportFormID = FormNo;
                DataSet dsrf = rffb.GetList(rff_m);
                DataTable dtri = new DataTable();
                DataSet dsri = new DataSet();
                string modelType = "";
                string modelname = "";
                bool result = false;
                List<string> l = new List<string>();
                switch ((SectionType)Convert.ToInt32(dsrf.Tables[0].Rows[0]["SECTIONTYPE"].ToString()))
                {
                    case SectionType.all:
                        #region Normal
                        rff_m.ReportFormID = FormNo;
                        dsri = rifb.GetList(rif_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "CHAM";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                            //return "暂无匹配模板！";
                        }
                        break;
                        #endregion
                    case SectionType.Normal:
                        #region Normal
                        rif_m.ReportFormID = FormNo;
                        dsri = rifb.GetList(rif_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "CHAM";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                            //return "暂无匹配模板！";
                        }
                        break;
                        #endregion
                    case SectionType.Micro:
                        #region Micro
                        rff_m.ReportFormID = FormNo;
                        dsri = rifb.GetList(rif_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "MICROBE";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                            //return "暂无匹配模板！";
                        }
                        break;
                        #endregion
                    case SectionType.NormalIncImage:
                        #region NormalIncImage
                        rff_m.ReportFormID = FormNo;
                        dsri = rifb.GetList(rif_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "CHAM";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                            //return "暂无匹配模板！";
                        }
                        break;
                        #endregion
                    case SectionType.MicroIncImage:
                        #region MicroIncImage
                        rff_m.ReportFormID = FormNo;
                        dsri = rifb.GetList(rif_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "MICROBE";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                            //return "暂无匹配模板！";
                        }
                        break;
                        #endregion
                    case SectionType.CellMorphology:
                        #region CellMorphology
                        rff_m.ReportFormID = FormNo;
                        dsri = rifb.GetList(rif_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "MARROW";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                            //return "暂无匹配模板！";
                        }
                        break;
                        #endregion
                    case SectionType.FishCheck:
                        #region FishCheck
                        rff_m.ReportFormID = FormNo;
                        dsri = rifb.GetList(rif_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "CHAM";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                            //return "暂无匹配模板！";
                        }
                        break;
                        #endregion
                    case SectionType.SensorCheck:
                        #region SensorCheck
                        rff_m.ReportFormID = FormNo;
                        dsri = rifb.GetList(rif_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "PIC";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                            //return "暂无匹配模板！";
                        }
                        break;
                        #endregion
                    case SectionType.ChromosomeCheck:
                        #region ChromosomeCheck
                        rff_m.ReportFormID = FormNo;
                        dsri = rifb.GetList(rif_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "MARROW";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                            //return "暂无匹配模板！";
                        }
                        break;
                        #endregion
                    case SectionType.PathologyCheck:
                        #region PathologyCheck
                        rff_m.ReportFormID = FormNo;
                        dsri = rifb.GetList(rif_m);
                        if (dsri.Tables.Count > 0 && dsri.Tables[0].Rows.Count > 0)
                        {
                            dtri = dsri.Tables[0];
                        }
                        modelType = "MARROW";
                        modelname = this.FindMode(dsrf.Tables[0].Rows[0], dtri, Flag);
                        if (modelname.Trim().Length < 0)
                        {
                            return null;
                            //return "暂无匹配模板！";
                        }
                        break;
                        #endregion
                }
                string SaveType = "pdf";
                IntPtr ConnectionString = Marshal.StringToHGlobalAnsi(ConfigurationManager.ConnectionStrings["WebLisDB"].ToString());
                IntPtr modelNameDeliph = Marshal.StringToHGlobalAnsi(modelname);
                IntPtr modelTypeDeliph = Marshal.StringToHGlobalAnsi(modelType);
                IntPtr PrintID = Marshal.StringToHGlobalAnsi(FormNo);
                IntPtr saveType = Marshal.StringToHGlobalAnsi(SaveType);
                IntPtr SavePath = Marshal.StringToHGlobalAnsi(System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"));
                IntPtr Path = Marshal.StringToHGlobalAnsi(GetImagePath(dsrf.Tables[0].Rows[0]));
                IntPtr LogPath = Marshal.StringToHGlobalAnsi(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\");
                string whereStr = "";
                IntPtr where = Marshal.StringToHGlobalAnsi(whereStr);
                result = PrintReport(ConnectionString, modelNameDeliph, modelTypeDeliph, PrintID, saveType, SavePath, Path, LogPath, where);
                if (result)
                {
                    ZhiFang.Common.Log.Log.Info("生成报告成功");
                    string aaa = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + FormNo.Replace(" 00:00:00", "00:00:00").Replace(":", "") + ".pdf";
                    l.Add(ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + FormNo.Replace(" 00:00:00", "00:00:00").Replace(":", "") + ".pdf");
                    return l;
                }
                return null;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString() + "---------" + e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                return null;
            }
        }

        public List<string> PrintMergeEnHtml(string FormNo, ReportFormTitle Flag, string ResultFlag)
        {
            try
            {
                Model.ReportFormFull rff_m = new Model.ReportFormFull();
                Model.ReportItemFull rif_m = new Model.ReportItemFull();
                Model.ReportMicroFull rmf_m = new Model.ReportMicroFull();
                Model.ReportMarrowFull rmarrowf_m = new Model.ReportMarrowFull();
                DataTable dtri = new DataTable();
                string modelType = "";
                string modelname = "";
                string SaveType = "pdf";
                bool result = false;
                DataSet dataSet;
                dataSet = this.dalmerge.GetModelDataFrFormAll(FormNo.Split(',')).DataSet;
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {

                    dtri = dataSet.Tables[0];
                    modelType = "HB";
                    if (Flag == ReportFormTitle.BatchPrint)
                    {
                        modelname = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\套打合并英文.fr3";
                    }
                    else
                    {
                        modelname = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\合并英文.fr3";
                    }
                    IntPtr ConnectionString = Marshal.StringToHGlobalAnsi(ConfigurationManager.ConnectionStrings["WebLisDB"].ToString());
                    IntPtr modelNameDeliph = Marshal.StringToHGlobalAnsi(modelname);
                    IntPtr modelTypeDeliph = Marshal.StringToHGlobalAnsi(modelType);
                    IntPtr PrintID = Marshal.StringToHGlobalAnsi(FormNo);
                    IntPtr saveType = Marshal.StringToHGlobalAnsi(SaveType);
                    IntPtr SavePath = Marshal.StringToHGlobalAnsi(System.AppDomain.CurrentDomain.BaseDirectory +ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"));
                    IntPtr Path = Marshal.StringToHGlobalAnsi(GetImagePath(dtri.Rows[0]));
                    IntPtr LogPath = Marshal.StringToHGlobalAnsi(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\");
                    string whereStr = "";
                    IntPtr where = Marshal.StringToHGlobalAnsi(whereStr);
                    result = PrintReport(ConnectionString, modelNameDeliph, modelTypeDeliph, PrintID, saveType, SavePath, Path, LogPath, where);
                    if (result)
                    {
                        string[] formNoa = FormNo.Split(',');
                        string str = "";
                        str = formNoa[0].ToString();
                        List<string> l = new List<string>();
                        string aaa = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + str.Replace(" 00:00:00", "000000").Replace(":", "") + ".pdf";
                        l.Add(ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + str.Replace(" 00:00:00", "00:00:00").Replace(":", "") + ".pdf");
                        return l;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                return null;
            }
        }

        public List<string> PrintMergeHtml(string FormNo, ReportFormTitle Flag, string ResultFlag)
        {
            try
            {
                Model.ReportFormFull rff_m = new Model.ReportFormFull();
                Model.ReportItemFull rif_m = new Model.ReportItemFull();
                Model.ReportMicroFull rmf_m = new Model.ReportMicroFull();
                Model.ReportMarrowFull rmarrowf_m = new Model.ReportMarrowFull();
                DataTable dtri = new DataTable();
                string modelType = "";
                string modelname = "";
                bool result = false;
                string SaveType = "pdf";
                DataSet dataSet;
                dataSet = this.dalmerge.GetModelDataFrFormAll(FormNo.Split(',')).DataSet;
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {

                    dtri = dataSet.Tables[0];
                    modelType = "HB";
                    if (Flag == ReportFormTitle.BatchPrint)
                    {
                        modelname = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\套打合并.fr3";
                    }
                    else
                    {
                        modelname = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\合并.fr3";
                    }
                    IntPtr ConnectionString = Marshal.StringToHGlobalAnsi(ConfigurationManager.ConnectionStrings["WebLisDB"].ToString());
                    IntPtr modelNameDeliph = Marshal.StringToHGlobalAnsi(modelname);
                    IntPtr modelTypeDeliph = Marshal.StringToHGlobalAnsi(modelType);
                    IntPtr PrintID = Marshal.StringToHGlobalAnsi(FormNo);
                    IntPtr saveType = Marshal.StringToHGlobalAnsi(SaveType);
                    IntPtr SavePath = Marshal.StringToHGlobalAnsi(System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"));
                    IntPtr Path = Marshal.StringToHGlobalAnsi(GetImagePath(dtri.Rows[0]));
                    IntPtr LogPath = Marshal.StringToHGlobalAnsi(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\");
                    string whereStr = "";
                    IntPtr where = Marshal.StringToHGlobalAnsi(whereStr);
                    result = PrintReport(ConnectionString, modelNameDeliph, modelTypeDeliph, PrintID, saveType, SavePath, Path, LogPath, where);
                    if (result)
                    {
                        string[] formNoa = FormNo.Split(',');
                        string str = "";
                        str = formNoa[0].ToString();
                        List<string> l = new List<string>();
                        string aaa = System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + str.Replace(" 00:00:00", "000000").Replace(":", "") + ".pdf";
                        l.Add(ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + "\\" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + str.Replace(" 00:00:00", "00:00:00").Replace(":", "") + ".pdf");
                        return l;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                return null;
            }
        }

        
        public List<string> PrintHtml(string FormNo)
        {
            return PrintHtml(FormNo, this.titelflag);
        }

        public List<string> PrintMergeHtml(string FormNo)
        {
            return PrintMergeHtml(FormNo, this.titelflag);
        }

        public List<string> PrintMergeEnHtml(string FormNo)
        {
            return PrintMergeEnHtml(FormNo, this.titelflag);
        }
        public Model.PrintFormat GetPrintModelInfo(string FormNo)
        {
            throw new NotImplementedException();
        }
        private string FindMode(DataRow dr, DataTable table, ReportFormTitle flag)
        {
            return FindMode(dr, table, null, flag);
        }
        private string FindMode(DataRow dr, DataTable table, DataTable table1, ReportFormTitle flag)
        {
            try
            {
                string path = "";
                int h = 0;
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage").Trim() != "")
                {
                    if (dr != null && dr.Table.Columns.Contains("ReceiveDate") && dr["ReceiveDate"] != null && dr["ReceiveDate"].ToString().Trim() != "")
                    {
                        DateTime datetime = Convert.ToDateTime(dr["ReceiveDate"].ToString().Trim());
                        path = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage").Trim() + "\\" + datetime.Year + "\\" + datetime.Month + "\\" + datetime.Day + "\\" + dr["FormNo"].ToString() + "\\";
                        if (ZhiFang.Common.Public.FilesHelper.CheckDirectory(path))
                        {
                            string[] files = Directory.GetFiles(path);

                            for (int i = 0; i < files.Length; i++)
                            {
                                if (files[i].ToString().IndexOf("S_RequestVItem") > 0)
                                {
                                    h = h + 1;
                                }
                            }
                        }
                    }
                }
                DataTable table4;
                string str5;
                string formatPrint = "";
                Model.PGroupPrint t = new Model.PGroupPrint();
                try
                {
                    t.SectionNo = int.Parse(dr["SectionNo"].ToString());
                }
                catch
                {
                    t.SectionNo = -1;
                }
                t.UseFlag = 1;
                if (flag == ReportFormTitle.BatchPrint)
                {
                    t.BatchPrint = 1;
                }
                if (flag == ReportFormTitle.center)
                {
                    t.ModelTitleType = 1;
                }
                if (flag == ReportFormTitle.client)
                {
                    t.ModelTitleType = 0;
                }
                if (Convert.ToInt32(flag) > 2)
                {
                    t.SickTypeNo = Convert.ToInt32(flag) - 2;
                }
                table4 = this.PGroupPrint.GetList(t).Tables[0];
                if (table4.Rows.Count <= 0)
                {
                    return null;
                }
                DataTable tmp = new DataTable();
                if (h > 0)
                {
                    if (table4 != null && table4.Rows.Count > 0)
                    {
                        DataRow[] dra = table4.Select();
                        formatPrint = PGroupPrint.PrintFormatFilter_Weblis(dra, h);
                    }
                    DataRow[] dra1 = table4.Select("PrintFormatNo=" + formatPrint);
                    tmp = table4.Clone();
                    DataRow dr1 = dra1[0];
                    tmp.ImportRow(dr1);
                }
                else
                {
                    tmp = table4;
                }
                if (tmp.Select("SpecialtyItemNo is null", "Sort").Count<DataRow>() != tmp.Rows.Count)
                {
                    if (tmp.Select("SpecialtyItemNo is null and ItemMaxNumber is null and ItemMinNumber is null ", "Sort").Count<DataRow>() != tmp.Rows.Count)
                    {
                        str5 = "";
                        if (table != null && table.Rows.Count > 0)
                        {
                            for (int num = 0; num < table.Rows.Count; num++)
                            {
                                str5 = str5 + table.Rows[num]["itemno"].ToString() + ",";
                            }
                        }
                        if (table1 != null && table1.Rows.Count > 0)
                        {
                            for (int num = 0; num < table1.Rows.Count; num++)
                            {
                                str5 = str5 + table1.Rows[num]["itemno"].ToString() + ",";
                            }
                        }
                        if (str5 != "")
                        {
                            str5 = str5.Substring(0, str5.LastIndexOf(","));
                        }
                        if ((tmp.Select("clientno is null ", "Sort").Count<DataRow>() == tmp.Rows.Count) || (dr["clientno"].ToString().Trim().Length < 0))
                        {
                            formatPrint = this.PGroupPrint.GetFormatPrint(str5, tmp);
                        }
                        else
                        {
                            formatPrint = this.PGroupPrint.GetFormatPrint(Convert.ToInt32(dr["clientno"].ToString()), str5, tmp);
                        }
                    }
                }
                else
                {

                    if ((tmp.Select("clientno is null ", "Sort").Count<DataRow>() != tmp.Rows.Count) && (dr["clientno"].ToString().Trim().Length >= 0))
                    {
                        formatPrint = tmp.Select(" 1=1 ", " Sort asc,Id desc ")[0]["PrintFormatNo"].ToString().Trim();
                    }
                    if (dr["clientno"] != DBNull.Value)
                    {

                        formatPrint = this.PGroupPrint.GetFormatPrint(new int?(Convert.ToInt32(dr["clientno"].ToString())), tmp);
                    }
                    else
                    {
                        int? clientno = null;
                        formatPrint = this.PGroupPrint.GetFormatPrint(clientno, tmp);
                    }
                }
                string format = "";
                try
                {
                    Model.PrintFormat pf_m = pfb.GetModel(formatPrint);
                    string showmodel = pf_m.PintFormatAddress.ToString().Trim() + "\\" + pf_m.Id + "\\" + pf_m.Id + ".fr3";
                    formatPrint = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\" + showmodel);
                    format = formatPrint.Replace("//", "").ToString();
                    ZhiFang.Common.Log.Log.Info("模板路径：" + format);
                }
                catch (Exception e)
                {
                    ZhiFang.Common.Log.Log.Debug(e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                    return "";
                }
                return format;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                return "";
            }
            //return "手工免疫.fr3";
        }
        public string GetImagePath(DataRow dataRow)
        {
            try
            {
                string path = "";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage").Trim() != "")
                {
                    if (dataRow != null && dataRow.Table.Columns.Contains("ReceiveDate") && dataRow["ReceiveDate"] != null && dataRow["ReceiveDate"].ToString().Trim() != "")
                    {
                        DateTime datetime = Convert.ToDateTime(dataRow["ReceiveDate"].ToString().Trim());
                        path = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage").Trim() + "\\" + datetime.Year + "\\" + datetime.Month + "\\" + datetime.Day + "\\" + dataRow["FormNo"].ToString() + "\\";
                    }
                }
                return path;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString() + "---------" + e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                return null;
            }
        }

        public DataTable GerReportFormAndItemData(string fromNo, int sectiontype, out DataTable dsrf_Out)
        {
            dsrf_Out = null;
            return null;
        }

        public string GetTemplatePath(DataTable dtrf, DataTable dtri, string pageName, int showType, int sectiontype)
        {
            return "";
        }


        public List<string> PrintMergePdf(string FormNo, ReportFormTitle Flag)
        {
            throw new NotImplementedException();
        }
    }
}
