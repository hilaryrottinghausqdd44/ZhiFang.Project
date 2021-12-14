using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using Newtonsoft.Json.Linq;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BEReportData : BaseBLL<EReportData>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBEReportData
    {
        IBETemplet IBETemplet { get; set; }
        IBStoredProcedure IBStoredProcedure { get; set; }
        IBEAttachment IBEAttachment { get; set; }

        public EntityList<PReportData> QueryCheckReportData(int templetType, string templetID, string equipID, string employeeID, string beginDate, string endDate, int checkType, string otherPara)
        {
            EntityList<PReportData> listReportData = new EntityList<PReportData>();
            //otherPara = "{\"IsCheck\":\"\",\"TempletName\":\"\",\"TempletCode\":\"\",\"EquipName\":\"\",\"SectionID\":\"\",\"TempletTypeID\":\"\",\"EquipTypeID\":\"\",\"SectionCode\":\"NB001\"}";
            string para = "";
            if (!string.IsNullOrWhiteSpace(otherPara))
            {
                JObject jsonObject = JObject.Parse(otherPara);
                para = (jsonObject["IsCheck"] != null ? jsonObject["IsCheck"].ToString() : "") + "&&" +
                       (jsonObject["TempletName"] != null ? jsonObject["TempletName"].ToString() : "") + "&&" +
                       (jsonObject["TempletCode"] != null ? jsonObject["TempletCode"].ToString() : "") + "&&" +
                       (jsonObject["EquipName"] != null ? jsonObject["EquipName"].ToString() : "") + "&&" +
                       (jsonObject["SectionID"] != null ? jsonObject["SectionID"].ToString() : "") + "&&" +
                       (jsonObject["TempletTypeID"] != null ? jsonObject["TempletTypeID"].ToString() : "") + "&&" +
                       (jsonObject["EquipTypeID"] != null ? jsonObject["EquipTypeID"].ToString() : "") + "&&" +
                       (jsonObject["SectionCode"] != null ? jsonObject["SectionCode"].ToString() : "");
            }
            listReportData = IBStoredProcedure.QueryReportData(templetType, templetID, equipID, employeeID.ToString(), beginDate, endDate, checkType, para);
            return listReportData;
        }

        public BaseResultDataValue QueryCheckPdfPath(long reportDataID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EReportData reportData = this.Get(reportDataID);
            if (reportData != null)
            {
                string parentPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LabFilesPath");
                if (!string.IsNullOrEmpty(reportData.ReportFilePath))
                {
                    baseResultDataValue.ResultDataValue = parentPath + reportData.ReportFilePath;
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "审核记录文件路径为空。ID：" + reportDataID.ToString();
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "无法根据ID获取审核记录。ID：" + reportDataID.ToString();
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue AddEReportData(long templetID, DateTime reportDate, string reportFilePath, int isCheck, string checker, string checkView)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EReportData reportData = new EReportData();
            ETemplet templet = IBETemplet.Get(templetID);
            reportData.ReportName = templet.CName;
            if (templet.CheckType == 1)
                reportData.ReportDate = reportDate;
            else
                reportData.ReportDate = new DateTime(reportDate.Year, reportDate.Month, 1);
            reportData.ETemplet = templet;
            reportData.IsCheck = isCheck;
            reportData.Checker = checker;
            reportData.CheckTime = DateTime.Now;
            reportData.CheckView = checkView;
            reportData.ReportFilePath = reportFilePath;
            reportData.IsUse = true;
            reportData.ReportFileExt = "";
            this.Entity = reportData;
            baseResultDataValue.success = this.Add();
            return baseResultDataValue;
        }

        public BaseResultDataValue EditEReportDataCheckState(long reportID, string cancelCheckerID, string cancelChecker, string cancelCheckView)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EReportData report = this.Get(reportID);
            if (report != null)
            {
                report.IsUse = false;
                report.IsCheck = 0;
                report.CancelCheckerID = long.Parse(cancelCheckerID);
                report.CancelChecker = cancelChecker;
                report.CancelCheckView = cancelCheckView;
                report.CancelCheckTime = DateTime.Now;
                report.DataUpdateTime = DateTime.Now;
                this.Entity = report;
                this.Edit();
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "根据审核报告ID获取不到相关报告的信息！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QueryReportIsChecked(long templetID, DateTime reportDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string hqlSQL = "  ereportdata.IsUse=1 and ereportdata.ETemplet.Id=" + templetID.ToString() +
                " and ereportdata.ReportDate=\'" + reportDate.ToString("yyyy-MM-dd") + "\'";
            EntityList<EReportData> entityList = this.SearchListByHQL(hqlSQL, 0, 0);
            if (entityList != null && entityList.count > 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ResultDataValue = entityList.list[0].ReportFilePath;
                baseResultDataValue.ErrorInfo = "此质量记录已经审核！";
            }
            return baseResultDataValue;
        }

        public EntityList<EReportData> QueryEReportDataByHQL(string where, string sort, int page, int limit)
        {
            EntityList<EReportData> entityList = new EntityList<EReportData>();
            try
            {
                //if (!string.IsNullOrWhiteSpace(where))
                //where += " and ereportdata.IsUse=1 ";
                if ((sort != null) && (sort.Length > 0))
                    entityList = this.SearchListByHQL(where, sort, page, limit);
                else
                    entityList = this.SearchListByHQL(where, page, limit);
                if (entityList != null && entityList.count > 0)
                {
                    foreach (EReportData entity in entityList.list)
                    {
                        DateTime reportDate = (DateTime)entity.ReportDate;
                        DateTime firstDay = new DateTime(reportDate.Year, reportDate.Month, 1);
                        int dayMonth = DateTime.DaysInMonth(reportDate.Year, reportDate.Month);
                        DateTime lastDay = new DateTime(reportDate.Year, reportDate.Month, dayMonth);                       
                        if (entity.ETemplet.CheckType == 1)
                        {
                            firstDay = reportDate;
                            lastDay = reportDate;
                        }
                        string strWhere = " eattachment.ETemplet.Id=" + entity.ETemplet.Id.ToString() +
                         " and eattachment.FileUploadDate>=\'" + firstDay.ToString("yyyy-MM-dd") + "\'" +
                         " and eattachment.FileUploadDate<\'" + lastDay.AddDays(1).ToString("yyyy-MM-dd") + "\'";
                        if (entity.EEquip != null)
                            strWhere += " and (eattachment.EEquip = null or eattachment.EEquip.Id=" + entity.EEquip.Id.ToString() + ")";
                        IList<EAttachment> list = IBEAttachment.SearchListByHQL(strWhere);
                        if (list != null && list.Count > 0)
                            entity.IsAttachment = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return entityList;
        }
    }
}