using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public interface IBEMaintenanceData : IBGenericManager<EMaintenanceData>
    {

        BaseResultDataValue AddEMaintenanceData(long templetID, string itemDate, string templetBatNo, IList<EMaintenanceData> entityList, ref IList<EMaintenanceData> resultList);

        BaseResultDataValue AddEMaintenanceData(long templetID, string itemDate, string typeCode, string templetBatNo, IList<EMaintenanceData> entityList, ref IList<EMaintenanceData> resultList);

        EntityList<EMaintenanceData> SearchEMaintenanceDataByTypeCode(long templetID, DateTime itemDate, string typeCode, string templetBatNo, int isLoadBeforeData);

        BaseResultDataValue GroupMaintenanceDataTB(long templetID, string typeCode, string beginDate, string endDate, int isLoadBeforeData);

        BaseResultDataValue JudgeTempletIsFillData(long templetID, string curDate, string beginDate, string endDate);

        BaseResultDataValue JudgeTempletIsFillData(long templetID, string typeCode, string templetDate);

        BaseResultDataValue TempletDataDelete(long templetID, bool isDelTempletData);

        BaseResultDataValue DeleteMaintenanceData(long templetID, string templetBatNo, string beginDate, string endDate);

        BaseResultDataValue DeleteMaintenanceDataTB(long templetID, string typeCode, string itemDate, string batchNumber);

        BaseResultDataValue FillMaintenanceDataToExcel(long templetID, long employeeID, string beginDate, string endDate, string templetBatNo, string checkView);

        BaseResultDataValue ExcelToPdfFile(long templetID, long employeeID, string beginDate, string endDate, string templetBatNo, bool isPreview, string checkView);

        BaseResultDataValue PreviewExcelTemplet(long templetID);

        BaseResultDataValue TempletBatNoGroupData(long templetID, string beginDate, string endDate);

        BaseResultDataValue QueryReportGroupData(long reportDataID, long templetID, string beginDate, string endDate);

    }
}