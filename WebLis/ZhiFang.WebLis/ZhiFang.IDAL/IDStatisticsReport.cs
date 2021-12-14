using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ZhiFang.IDAL
{
    public interface IDStatisticsReport
    {
        DataTable ReportFormGroupByClientNoAndReportDate(string startDateTime, string endDateTime, string clientNoList, int dateType);
        DataTable ReportFormGroupByClientNo(string startDateTime, string endDateTime, string clientNoList, int dateType);
        DataTable StatisticsRequestItemCenter(string startDateTime, string endDateTime, string clientNoList, int dateType = 0);
        DataTable StatisticsRequestItemClient(string startDateTime, string endDateTime, string clientNoList, int dateType = 0);
        DataTable StatisticsRequestDetailItemLab(string startDateTime, string endDateTime, string clientNoList);
        DataTable StatisticsRequestCombiItemLab(string startDateTime, string endDateTime, string clientNoList);
        DataTable StatisticsBarCodeCountGroupByClientNoAndReportDate(string startDateTime, string endDateTime, string clientNoList, int dateType);
        Dictionary<string, int> StatisticsGetTestFinishCount(string startDateTime, string endDateTime, string clientNoList);
        DataTable StatisticsGetTestFinish(string startDateTime, string endDateTime, string clientNoList, int Limit);
        DataTable StatisticsGetBarCodeDeliveryInfo(string startDateTime, string endDateTime, string clientNoList, int limit);
        DataTable StatisticsGetTestFinishCountTop(string startDateTime, string endDateTime, string clientNoList, int limit);
        DataTable StatisticsGetBarCodeSendCountTop(string startDateTime, string endDateTime, string clientNoList, int limit);
        DataTable StatisticsGetTestItemCountTop(string startDateTime, string endDateTime, string clientNoList, int limit);
        DataTable StatisticsGetTestFinishCountByYear(string startDateTime, string endDateTime, string clientNoList); 
        DataTable StatisticsGetReportCountByYear(string startDateTime, string endDateTime, string clientNoList);
        DataTable StatisticsGetReportCountTop(string startDateTime, string endDateTime, string clientNoList, int limit);
        DataTable Wuhu_StatisticsGender(string startDateTime, string endDateTime);
        DataTable Wuhu_StatisticsAge(string startDateTime, string endDateTime);
        DataTable Wuhu_StatisticsInspectionData(string startDateTime, string endDateTime);
        DataTable Wuhu_StatisticsHosptalGrade(string startDateTime, string endDateTime);
        DataTable Wuhu_StatisticsUrbanRuralGrade(string startDateTime, string endDateTime);
        DataTable Wuhu_StatisticsAreaDetectionQuantity(string startDateTime, string endDateTime);
        DataTable Wuhu_StatisticsPopInspectionFee(string startDateTime, string endDateTime);
        DataSet Wuhu_StatisticsDataAnalysis(string startDateTime, string endDateTime);
    }
}
