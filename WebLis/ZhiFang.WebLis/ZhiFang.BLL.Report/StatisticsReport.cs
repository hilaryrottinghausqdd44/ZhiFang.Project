using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.DALFactory;
using ZhiFang.IDAL;
using ZhiFang.Model.VO;

namespace ZhiFang.BLL.Report
{
    public class StatisticsReport
    {
        private readonly IDStatisticsReport dal = DalFactory<IDStatisticsReport>.GetDalByClassName("StatisticsReport");
        public DataTable ReportFormGroupByClientNoAndReportDate(string startDateTime, string endDateTime, string clientNoList, int dateType)
        {
            return dal.ReportFormGroupByClientNoAndReportDate(startDateTime, endDateTime, clientNoList, dateType);
        }

        public DataTable ReportFormGroupByClientNo(string startDateTime, string endDateTime, string clientNoList, int dateType)
        {
            return dal.ReportFormGroupByClientNo(startDateTime, endDateTime, clientNoList, dateType);
        }

        public DataTable StatisticsRequestItemCenter(string startDateTime, string endDateTime, string clientNoList)
        {
            return dal.StatisticsRequestItemCenter(startDateTime, endDateTime, clientNoList);
        }

        public DataTable StatisticsRequestItemClient(string startDateTime, string endDateTime, string clientNoList)
        {
            return dal.StatisticsRequestItemClient(startDateTime, endDateTime, clientNoList);
        }

        public DataTable StatisticsRequestDetailItemLab(string startDateTime, string endDateTime, string clientNoList)
        {
            return dal.StatisticsRequestDetailItemLab(startDateTime, endDateTime, clientNoList);
        }

        public DataTable StatisticsRequestCombiItemLab(string startDateTime, string endDateTime, string clientNoList)
        {
            return dal.StatisticsRequestCombiItemLab(startDateTime, endDateTime, clientNoList);
        }

        public DataTable StatisticsBarCodeCountGroupByClientNoAndReportDate(string startDateTime, string endDateTime, string clientNoList, int dateType)
        {
            return dal.StatisticsBarCodeCountGroupByClientNoAndReportDate(startDateTime, endDateTime, clientNoList, dateType);
        }
        public Dictionary<string, int> StatisticsGetTestFinishCount(string startDateTime, string endDateTime, string clientNoList)
        {
            return dal.StatisticsGetTestFinishCount(startDateTime, endDateTime, clientNoList);
        }
        public DataTable StatisticsGetTestFinish(string startDateTime, string endDateTime, string clientNoList, int Limit)
        {
            return dal.StatisticsGetTestFinish(startDateTime, endDateTime, clientNoList, Limit);
        }

        public DataTable StatisticsGetBarCodeDeliveryInfo(string startDateTime, string endDateTime, string clientNoList, int Limit)
        {
            return dal.StatisticsGetBarCodeDeliveryInfo(startDateTime, endDateTime, clientNoList, Limit);
        }
        public DataTable StatisticsGetTestFinishCountTop(string startDateTime, string endDateTime, string clientNoList, int limit)
        {
            return dal.StatisticsGetTestFinishCountTop(startDateTime, endDateTime, clientNoList, limit);
        }

        public DataTable StatisticsGetBarCodeSendCountTop(string startDateTime, string endDateTime, string clientNoList, int limit)
        {
            return dal.StatisticsGetBarCodeSendCountTop(startDateTime, endDateTime, clientNoList, limit);
        }

        public DataTable StatisticsGetTestItemCountTop(string startDateTime, string endDateTime, string clientNoList, int limit)
        {
            return dal.StatisticsGetTestItemCountTop(startDateTime, endDateTime, clientNoList, limit);
        }
        public DataTable StatisticsGetTestFinishCountByYear(string startDateTime, string endDateTime, string clientNoList)
        {
            return dal.StatisticsGetTestFinishCountByYear(startDateTime, endDateTime, clientNoList);
        }
        public DataTable StatisticsGetReportCountByYear(string startDateTime, string endDateTime, string clientNoList)
        {
            return dal.StatisticsGetReportCountByYear(startDateTime, endDateTime, clientNoList);
        }
        public DataTable StatisticsGetReportCountTop(string startDateTime, string endDateTime, string clientNoList, int limit)
        {
            return dal.StatisticsGetReportCountTop(startDateTime, endDateTime, clientNoList, limit);
        }

        public Dictionary<string, string> Wuhu_StatisticsAge(string startDateTime, string endDateTime)
        {
            var table = dal.Wuhu_StatisticsAge(startDateTime, endDateTime);
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("age5", "0");
            keyValuePairs.Add("age5_14", "0");
            keyValuePairs.Add("age15_44", "0");
            keyValuePairs.Add("age45_59", "0");
            keyValuePairs.Add("age60", "0");
            double agecount = 0;
            if (double.TryParse(table.Rows[0]["age5"].ToString(), out agecount))
            {
                keyValuePairs["age5"] = (Math.Round(agecount)).ToString();
            }
            if (double.TryParse(table.Rows[0]["age5_14"].ToString(), out agecount))
            {
                keyValuePairs["age5_14"] = (Math.Round(agecount)).ToString();
            }
            if (double.TryParse(table.Rows[0]["age15_44"].ToString(), out agecount))
            {
                keyValuePairs["age15_44"] = (Math.Round(agecount)).ToString();
            }
            if (double.TryParse(table.Rows[0]["age45_59"].ToString(), out agecount))
            {
                keyValuePairs["age45_59"] = (Math.Round(agecount)).ToString();
            }
            if (double.TryParse(table.Rows[0]["age60"].ToString(), out agecount))
            {
                keyValuePairs["age60"] = (Math.Round(agecount)).ToString();
            }
            return keyValuePairs; 
            
        }

        public Dictionary<string, string> Wuhu_StatisticsGender(string startDateTime, string endDateTime)
        {
            var table = dal.Wuhu_StatisticsGender(startDateTime, endDateTime);
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("man", "0");
            keyValuePairs.Add("wuman", "0");
            double manc = 0;
            if (double.TryParse(table.Rows[0]["man"].ToString(), out manc))
            {
                keyValuePairs["man"] = (Math.Round(manc)).ToString();
            }
            double wmanc = 0;
            if (double.TryParse(table.Rows[0]["wuman"].ToString(), out wmanc))
            {
                keyValuePairs["wuman"] = (Math.Round(wmanc)).ToString();
            }
            return keyValuePairs;
        }

        public DataTable Wuhu_StatisticsInspectionData(string startDateTime, string endDateTime)
        {
            return dal.Wuhu_StatisticsInspectionData(startDateTime, endDateTime);
           
        }

        public DataTable Wuhu_StatisticsHosptalGrade(string startDateTime, string endDateTime)
        {
            return dal.Wuhu_StatisticsHosptalGrade(startDateTime, endDateTime);
        }

        public DataTable Wuhu_StatisticsUrbanRuralGrade(string startDateTime, string endDateTime)
        {
            return dal.Wuhu_StatisticsUrbanRuralGrade(startDateTime, endDateTime);
        }

        public DataTable Wuhu_StatisticsAreaDetectionQuantity(string startDateTime, string endDateTime)
        {
            return dal.Wuhu_StatisticsAreaDetectionQuantity(startDateTime, endDateTime);
        }

        public DataTable Wuhu_StatisticsPopInspectionFee(string startDateTime, string endDateTime)
        {
            return dal.Wuhu_StatisticsPopInspectionFee(startDateTime, endDateTime);
        }

        public Wuhu_ReportFormFullVo Wuhu_StatisticsDataAnalysis(string startDateTime, string endDateTime)
        {
            DataSet ds = dal.Wuhu_StatisticsDataAnalysis(startDateTime, endDateTime);
            Wuhu_ReportFormFullVo wuhu_ReportFormFullVo = new Wuhu_ReportFormFullVo();
            DataTable dataTable = new DataTable();
            #region 门诊体检等数量
            dataTable = ds.Tables[0];
            List<Wuhu_SickTypeSample> WHSickTypeSample = new List<Wuhu_SickTypeSample>();
            foreach (DataRow item in dataTable.Rows)
            {
                Wuhu_SickTypeSample wuhu_SickTypeSample = new Wuhu_SickTypeSample();
                wuhu_SickTypeSample.Name = item["SICKTYPENAME"].ToString();
                wuhu_SickTypeSample.Count = item["sicktypecount"].ToString();
                wuhu_SickTypeSample.ALLCount = item["allcount"].ToString(); 
                WHSickTypeSample.Add(wuhu_SickTypeSample);
            }
            wuhu_ReportFormFullVo.WHSickTypeSample = WHSickTypeSample;
            #endregion

            #region 人次 总数
            wuhu_ReportFormFullVo.SampleCount = ds.Tables[1].Rows[0]["sampleall"].ToString();
            wuhu_ReportFormFullVo.SickTypeCount= ds.Tables[2].Rows[0]["sickcount"].ToString();
            #endregion

            #region 检测标本图
            dataTable = ds.Tables[3];
            List<Wuhu_SampleFigure> whsamplefigure = new List<Wuhu_SampleFigure>();
            foreach (DataRow item in dataTable.Rows)
            {
                Wuhu_SampleFigure wuhu_SampleFigure = new Wuhu_SampleFigure();
                wuhu_SampleFigure.Times = item["Times"].ToString();
                wuhu_SampleFigure.Count = item["count"].ToString();
                whsamplefigure.Add(wuhu_SampleFigure);
            }
            wuhu_ReportFormFullVo.WHSampleFigure = whsamplefigure;
            #endregion

            #region 人次图
            dataTable = ds.Tables[4];
            List<Wuhu_SickTypeFigure> Wuhu_SickTypeFigure = new List<Wuhu_SickTypeFigure>();
            foreach (DataRow item in dataTable.Rows)
            {
                Wuhu_SickTypeFigure wuhu_SickTypeFigure = new Wuhu_SickTypeFigure();
                wuhu_SickTypeFigure.Times = item["Times"].ToString();
                wuhu_SickTypeFigure.Count = item["count"].ToString();
                Wuhu_SickTypeFigure.Add(wuhu_SickTypeFigure);
            }
            wuhu_ReportFormFullVo.WHSickTypeFigure = Wuhu_SickTypeFigure;
            #endregion
            return wuhu_ReportFormFullVo;
        }
    }
}
