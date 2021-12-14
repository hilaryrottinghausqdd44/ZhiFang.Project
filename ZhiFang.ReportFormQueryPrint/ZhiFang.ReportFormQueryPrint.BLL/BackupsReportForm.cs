using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.Common;
using ZhiFang.ReportFormQueryPrint.DAL.MSSQL.Backups;

namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BBackupsReportForm
    {
        private readonly BackupsReportForm dal = new BackupsReportForm();
        private readonly BackupsReportItem drqi = new BackupsReportItem();
        private readonly BackupsReportMicro drqmi = new BackupsReportMicro();
        protected readonly IDAL.IDReportDrugGene idrdruggene = DalFactory<IDReportDrugGene>.GetDal("ReportDrugGene");
        /// <summary>
        /// 查询有多少条数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public int GetCountFormFull(string strWhere)
        {
            return dal.GetCountFormFull(strWhere);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList_FormFull(string fields, string strWhere)
        {
            return dal.GetList_FormFull(fields, strWhere);
        }
        public DataTable GetListByDataSource(string FormNo)
        {
            return dal.GetReportFormFullList(FormNo);
        }

        internal bool UpdatePageInfo(string reportformlist, string pageCount, string pageName)
        {
            return dal.UpdatePageInfo(reportformlist, pageCount, pageName);
        }

        public bool ReportIsPrintNullValues(string ReportFormID, string st) {
            ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BReportForm();

            DataTable dtrf = brf.GetListByDataSource(ReportFormID);
            bool ds = false;
            if (dtrf != null && dtrf.Rows.Count > 0)
            {
                #region      
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
                #endregion
                #region  数据准备 
                DataTable dt1 = new DataTable();
                switch ((SectionType)Convert.ToInt32(sectiontype))
                {
                    case SectionType.Normal:
                        #region Normal                               

                        if (dtrf.Columns.Contains("SectionResultType") && dtrf.Rows[0]["SectionResultType"].ToString().Trim() == "1")
                        {
                            dt1 = idrdruggene.GetReportItemFullList(ReportFormID);
                        }
                        else
                        {
                            dt1 = drqi.GetReportItemFullList(ReportFormID);
                        }


                        break;
                    #endregion
                    case SectionType.NormalIncImage:
                        #region NormalIncImage
                        if (dtrf.Columns.Contains("SectionResultType") && dtrf.Rows[0]["SectionResultType"].ToString().Trim() == "1")
                        {
                            dt1 = idrdruggene.GetReportItemFullList(ReportFormID);
                        }
                        else
                        {
                            dt1 = drqi.GetReportItemFullList(ReportFormID);
                        }
                        break;
                    #endregion
                    case SectionType.Micro:
                        #region Micro                         
                        dt1 = drqmi.GetReportMicroList(ReportFormID);
                        break;
                    #endregion
                    case SectionType.MicroIncImage:
                        #region MicroIncImage                               
                        dt1 = drqmi.GetReportMicroList(ReportFormID);
                        break;
                    #endregion
                    default:
                        dt1 = drqi.GetReportItemFullList(ReportFormID);
                        break;
                }
                #endregion
                for (var i = 0; i < dt1.Rows.Count; i++)
                {
                    if (dt1.Rows[i]["ReportValue"] != null && dt1.Rows[i]["ReportDesc"] != null)
                    {
                        ds = true;
                    }
                }
            }
            return ds;
        }

        public bool UpdatePrintTimes(string[] reportformlist)
        {
            return dal.UpdatePrintTimes(reportformlist);
        }
        public bool UpdateClientPrintTimes(string[] reportformlist)
        {
            return dal.UpdateClientPrintTimes(reportformlist);
        }

        /// <summary>
        /// 多项目历史对比
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <param name="PatNo"></param>
        /// <param name="st"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public DataSet ResultMhistory(string ReportFormID, string PatNo, string st, string Where)
        {
            ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BReportForm();
            DataTable dtrf = brf.GetListByDataSource(ReportFormID);
            DataSet ds = dal.ResultMhistory(ReportFormID, PatNo, Where);
            if (dtrf != null && dtrf.Rows.Count > 0)
            {
                #region      
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
                #endregion
                #region  数据准备 
                switch ((SectionType)Convert.ToInt32(sectiontype))
                {
                    case SectionType.Normal:
                        #region Normal                               
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataTable dt1 = new DataTable();
                            
                            string b = ds.Tables[0].Rows[i]["ReportFormID"].ToString();
                            if (dtrf.Columns.Contains("SectionResultType") && dtrf.Rows[0]["SectionResultType"].ToString().Trim() == "1")
                            {
                                dt1 = idrdruggene.GetReportItemFullList(b);
                            }
                            else
                            {
                                dt1 = drqi.GetReportItemFullList(b);
                            }
                            dt1.TableName = "table" + i;
                            ds.Tables.Add(dt1.Copy());
                        }
                        break;
                    #endregion
                    case SectionType.NormalIncImage:
                        #region NormalIncImage
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataTable dt1 = new DataTable();
                            string b = ds.Tables[0].Rows[i]["ReportFormID"].ToString();
                            if (dtrf.Columns.Contains("SectionResultType") && dtrf.Rows[0]["SectionResultType"].ToString().Trim() == "1")
                            {
                                dt1 = idrdruggene.GetReportItemFullList(b);
                            }
                            else
                            {
                                dt1 = drqi.GetReportItemFullList(b);
                            }



                            dt1.TableName = "table" + i;
                            ds.Tables.Add(dt1.Copy());
                        }
                        break;
                    #endregion
                    case SectionType.Micro:
                        #region Micro                         
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string b = ds.Tables[0].Rows[i]["ReportFormID"].ToString();
                            DataTable dt3 = drqmi.GetReportMicroList(b);
                            dt3.TableName = "table" + i;
                            ds.Tables.Add(dt3.Copy());
                        }
                        break;
                    #endregion
                    case SectionType.MicroIncImage:
                        #region MicroIncImage                               
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string b = ds.Tables[0].Rows[i]["ReportFormID"].ToString();
                            DataTable dt4 = drqmi.GetReportMicroList(b);
                            dt4.TableName = "table" + i;
                            ds.Tables.Add(dt4.Copy());
                        }
                        break;
                    #endregion
                    //case SectionType.CellMorphology:
                    //    #region CellMorphology
                    //       DataSet ds5=dal.ResultMhistory(ReportFormID, PatNo, Where);
                    //        for (int i = 0; i < ds5.Tables[0].Rows.Count; i++)
                    //        {
                    //            Int64 rfid5 = (Int64)ds5.Tables[0].Rows[i]["ReportFormID"];
                    //            string b = rfid5 + "";
                    //            DataTable dt5 = drm.GetReportMarrowItemList(b);
                    //            dt5.TableName="table"+i;
                    //            ds.Tables.Add(dt5.Copy());      
                    //        }
                    //break;
                    //    #endregion  

                    default:
                        DataSet ds6 = dal.ResultMhistory(ReportFormID, PatNo, Where);
                        for (int i = 0; i < ds6.Tables[0].Rows.Count; i++)
                        {
                            string b = ds.Tables[0].Rows[i]["ReportFormID"].ToString();
                            DataTable dt6 = drqi.GetReportItemFullList(b);
                            dt6.TableName = "table" + i;
                            ds.Tables.Add(dt6.Copy());
                        }
                        break;
                }
                #endregion


            }
            return ds;
        }

        public DataSet ResultDataTimeMhistory(string PatNo, string Where)
        {
            var res = dal.ResultDataTimeMhistory(PatNo, Where);
            return res;
        }

        public DataSet GetReportFromByReportFormID(List<string> IdList, string fields, string strWhere)
        {
            return dal.GetReportFromByReportFormID(IdList, fields, strWhere);

        }
    }
}
