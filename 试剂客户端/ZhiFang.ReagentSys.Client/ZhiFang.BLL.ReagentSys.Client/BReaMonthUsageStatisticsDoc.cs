
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using ZhiFang.ReagentSys.Client.Common;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaMonthUsageStatisticsDoc : BaseBLL<ReaMonthUsageStatisticsDoc>, ZhiFang.IBLL.ReagentSys.Client.IBReaMonthUsageStatisticsDoc
    {
        IDReaBmsOutDocDao IDReaBmsOutDocDao { get; set; }
        IDReaBmsOutDtlDao IDReaBmsOutDtlDao { get; set; }
        IBReaMonthUsageStatisticsDtl IBReaMonthUsageStatisticsDtl { get; set; }
        public BaseResultDataValue AddReaMonthUsageStatisticsDoc(ReaMonthUsageStatisticsDoc entity, long empID, string employeeName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            entity.TypeName = ReaMonthUsageStatisticsDocType.GetStatusDic()[entity.TypeID.ToString()].Name;
            entity.RoundTypeName = ReaMonthUsageStatisticsDocRoundType.GetStatusDic()[entity.TypeID.ToString()].Name;
            entity.DocNo = GetDocNo();
            entity.Visible = true;
            entity.DataAddTime = DateTime.Now;
            entity.DataUpdateTime = DateTime.Now;
            entity.CreaterID = empID;
            entity.CreaterName = employeeName;
            if (entity.StartDate.HasValue)
                entity.StartDate = DateTime.Parse(entity.StartDate.Value.ToString("yyyy-MM-dd 00:00:00"));
            if (entity.EndDate.HasValue)
                entity.EndDate = DateTime.Parse(entity.EndDate.Value.ToString("yyyy-MM-dd 23:59:59"));
            if (entity.RoundTypeId == int.Parse(ReaMonthUsageStatisticsDocRoundType.按自然月.Key))
            {
                if (entity.TypeID == int.Parse(ReaMonthUsageStatisticsDocType.按使用量.Key))
                {
                    AddOfMonthAndUsage(entity, ref baseResultDataValue);
                }
                else if (entity.TypeID == int.Parse(ReaMonthUsageStatisticsDocType.按使用部门.Key))
                {
                    AddOfMonthAndDept(entity, ref baseResultDataValue);
                }
            }
            else if (entity.RoundTypeId == int.Parse(ReaMonthUsageStatisticsDocRoundType.按日期范围.Key))
            {
                if (entity.TypeID == int.Parse(ReaMonthUsageStatisticsDocType.按使用量.Key))
                {
                    AddOfDateAreaAndUsage(entity, ref baseResultDataValue);
                }
                else if (entity.TypeID == int.Parse(ReaMonthUsageStatisticsDocType.按使用部门.Key))
                {
                    AddOfDateAreaAndDept(entity, ref baseResultDataValue);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "统计周期类型不能为空！";
                return baseResultDataValue;
            }


            return baseResultDataValue;
        }
        /// <summary>
        /// "按自然月+按使用量"新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="baseResultDataValue"></param>
        private void AddOfMonthAndUsage(ReaMonthUsageStatisticsDoc entity, ref BaseResultDataValue baseResultDataValue)
        {
            if (string.IsNullOrEmpty(entity.Round))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "统计周期不能为空！";
                return;
            }
            DateTime startDate = DateTime.Parse(entity.Round + "-01");
            if (DateTime.TryParse(entity.Round, out startDate))
                startDate = startDate.AddDays(1 - startDate.Day);
            //round = startDate.ToString("yyyy-MM");
            DateTime endDate = startDate.AddDays(1 - startDate.Day).AddMonths(1).AddDays(-1);
            entity.StartDate = startDate;
            entity.EndDate = endDate;
            if (entity.StartDate.HasValue)
                entity.StartDate = DateTime.Parse(entity.StartDate.Value.ToString("yyyy-MM-dd 00:00:00"));
            if (entity.EndDate.HasValue)
                entity.EndDate = DateTime.Parse(entity.EndDate.Value.ToString("yyyy-MM-dd 23:59:59"));

            //先判断"该自然月+使用部门"是否已经进行过统计
            string docHql = "reamonthusagestatisticsdoc.Visible=1 and reamonthusagestatisticsdoc.Round='" + entity.Round + "'";
            if (entity.DeptID.HasValue)
            {
                docHql = docHql + " and reamonthusagestatisticsdoc.DeptID = " + entity.DeptID.Value;
            }
            IList<ReaMonthUsageStatisticsDoc> dosList = this.SearchListByHQL(docHql);
            if (dosList.Count > 0)
            {
                baseResultDataValue.success = false;
                string errorInfo = "获取统计周期为：" + entity.Round;
                if (entity.DeptID.HasValue)
                {
                    errorInfo = errorInfo + ",使用部门为:" + entity.DeptName;
                }
                errorInfo = errorInfo + ",已进行过出库使用量统计！";
                baseResultDataValue.ErrorInfo = errorInfo;
                return;
            }

            //获取统计"该自然月+使用部门"的全部使用出库主单信息
            string outDocHql = string.Format("reabmsoutdoc.OutType={0} and reabmsoutdoc.DataAddTime>='{1}' and reabmsoutdoc.DataAddTime<='{2}'", ReaBmsOutDocOutType.使用出库.Key, startDate.ToString("yyyy-MM-dd HH:mm:ss"), endDate.ToString("yyyy-MM-dd HH:mm:ss"));
            if (entity.DeptID.HasValue)
            {
                outDocHql = outDocHql + " and reabmsoutdoc.DeptID = " + entity.DeptID.Value;
            }
            IList<ReaBmsOutDoc> outDocList = IDReaBmsOutDocDao.GetListByHQL(outDocHql);
            if (outDocList == null || outDocList.Count <= 0)
            {
                baseResultDataValue.success = false;
                string errorInfo = "获取统计周期为：" + entity.Round;
                if (entity.DeptID.HasValue)
                {
                    errorInfo = errorInfo + ",使用部门为:" + entity.DeptName;
                }
                errorInfo = errorInfo + ",使用出库信息为空！";
                baseResultDataValue.ErrorInfo = errorInfo;
                return;
            }

            StringBuilder outIDStr = new StringBuilder();
            IList<ReaBmsOutDtl> outDtlList = new List<ReaBmsOutDtl>();
            foreach (var outDoc in outDocList)
            {
                outIDStr.Append(outDoc.Id);
                outIDStr.Append(",");
            }
            if (outIDStr.Length > 0)
            {
                outDtlList = IDReaBmsOutDtlDao.GetListByHQL("reabmsoutdtl.OutDocID in (" + outIDStr.ToString().TrimEnd(',') + ")");
            }
            if (outDtlList != null && outDtlList.Count > 0)
            {
                this.Entity = entity;
                if (this.Add())
                {
                    baseResultDataValue = AddStatisticsDtl(entity, outDtlList);
                    baseResultDataValue.ResultDataValue = ServiceCommon.RBAC.CommonServiceMethod.GetAddMethodResultStr(this.Entity);
                }
            }
        }
        /// <summary>
        /// "按自然月+按使用部门"新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="baseResultDataValue"></param>
        private void AddOfMonthAndDept(ReaMonthUsageStatisticsDoc entity, ref BaseResultDataValue baseResultDataValue)
        {
            if (string.IsNullOrEmpty(entity.Round))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "统计周期不能为空！";
                return;
            }
            if (!entity.DeptID.HasValue)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "使用部门不能为空！";
                return;
            }
            DateTime startDate = DateTime.Parse(entity.Round + "-01");
            if (DateTime.TryParse(entity.Round, out startDate))
                startDate = startDate.AddDays(1 - startDate.Day);
            //round = startDate.ToString("yyyy-MM");
            DateTime endDate = startDate.AddDays(1 - startDate.Day).AddMonths(1).AddDays(-1);
            entity.StartDate = startDate;
            entity.EndDate = endDate;
            if (entity.StartDate.HasValue)
                entity.StartDate = DateTime.Parse(entity.StartDate.Value.ToString("yyyy-MM-dd 00:00:00"));
            if (entity.EndDate.HasValue)
                entity.EndDate = DateTime.Parse(entity.EndDate.Value.ToString("yyyy-MM-dd 23:59:59"));

            //先判断"该自然月+使用部门"是否已经进行过统计
            string docHql = "reamonthusagestatisticsdoc.Visible=1 and reamonthusagestatisticsdoc.Round='" + entity.Round + "'";
            if (entity.DeptID.HasValue)
            {
                docHql = docHql + " and reamonthusagestatisticsdoc.DeptID = " + entity.DeptID.Value;
            }
            IList<ReaMonthUsageStatisticsDoc> dosList = this.SearchListByHQL(docHql);
            if (dosList.Count > 0)
            {
                baseResultDataValue.success = false;
                string errorInfo = "获取统计周期为：" + entity.Round;
                if (entity.DeptID.HasValue)
                {
                    errorInfo = errorInfo + ",使用部门为:" + entity.DeptName;
                }
                errorInfo = errorInfo + ",已进行过出库使用量统计！";
                baseResultDataValue.ErrorInfo = errorInfo;
                return;
            }
            string outDocHql = string.Format("reabmsoutdoc.OutType={0} and reabmsoutdoc.DataAddTime>='{1}' and reabmsoutdoc.DataAddTime<='{2}'", ReaBmsOutDocOutType.使用出库.Key, startDate.ToString("yyyy-MM-dd HH:mm:ss"), endDate.ToString("yyyy-MM-dd HH:mm:ss"));
            if (entity.DeptID.HasValue)
            {
                outDocHql = outDocHql + " and reabmsoutdoc.DeptID = " + entity.DeptID.Value;
            }
            IList<ReaBmsOutDoc> outDocList = IDReaBmsOutDocDao.GetListByHQL(outDocHql);
            if (outDocList == null || outDocList.Count <= 0)
            {
                baseResultDataValue.success = false;
                string errorInfo = "获取统计周期为：" + entity.Round;
                if (entity.DeptID.HasValue)
                {
                    errorInfo = errorInfo + ",使用部门为:" + entity.DeptName;
                }
                errorInfo = errorInfo + ",使用出库信息为空！";
                baseResultDataValue.ErrorInfo = errorInfo;
                return;
            }
            AddOfGroupByDept(entity, outDocList, ref baseResultDataValue);
        }
        /// <summary>
        /// "按日期范围+按使用量"新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="baseResultDataValue"></param>
        private void AddOfDateAreaAndUsage(ReaMonthUsageStatisticsDoc entity, ref BaseResultDataValue baseResultDataValue)
        {
            if (!entity.StartDate.HasValue)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "统计开始日期不能为空！";
                return;
            }
            if (!entity.EndDate.HasValue)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "统计结束日期不能为空！";
                return;
            }
            if (entity.StartDate.HasValue)
                entity.StartDate = DateTime.Parse(entity.StartDate.Value.ToString("yyyy-MM-dd 00:00:00"));
            if (entity.EndDate.HasValue)
                entity.EndDate = DateTime.Parse(entity.EndDate.Value.ToString("yyyy-MM-dd 23:59:59"));
            string docHql = "reamonthusagestatisticsdoc.Visible=1 and reamonthusagestatisticsdoc.StartDate='" + entity.StartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' and reamonthusagestatisticsdoc.EndDate='" + entity.EndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            if (entity.DeptID.HasValue)
            {
                docHql = docHql + " and reamonthusagestatisticsdoc.DeptID = " + entity.DeptID.Value;
            }
            IList<ReaMonthUsageStatisticsDoc> dosList = this.SearchListByHQL(docHql);
            if (dosList.Count > 0)
            {
                baseResultDataValue.success = false;
                string errorInfo = "开始日期为：" + entity.StartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "，结束日期:" + entity.EndDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                if (entity.DeptID.HasValue)
                {
                    errorInfo = errorInfo + ",使用部门为:" + entity.DeptName;
                }
                errorInfo = errorInfo + ",已进行过统计！";
                return;
            }

            //获取统计自然月的全部使用出库主单信息
            string outDocHql = string.Format("reabmsoutdoc.OutType={0} and reabmsoutdoc.DataAddTime>='{1}' and reabmsoutdoc.DataAddTime<='{2}'", ReaBmsOutDocOutType.使用出库.Key, entity.StartDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), entity.EndDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            if (entity.DeptID.HasValue)
            {
                outDocHql = outDocHql + " and reabmsoutdoc.DeptID = " + entity.DeptID.Value;
            }
            IList<ReaBmsOutDoc> outDocList = IDReaBmsOutDocDao.GetListByHQL(outDocHql);
            if (outDocList == null || outDocList.Count <= 0)
            {
                baseResultDataValue.success = false;
                string errorInfo = "开始日期为：" + entity.StartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "，结束日期:" + entity.EndDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                if (entity.DeptID.HasValue)
                {
                    errorInfo = errorInfo + ",使用部门为:" + entity.DeptName;
                }
                errorInfo = errorInfo + ",使用出库信息为空！";
                return;
            }
            StringBuilder outIDStr = new StringBuilder();
            IList<ReaBmsOutDtl> outDtlList = new List<ReaBmsOutDtl>();
            foreach (var outDoc in outDocList)
            {
                outIDStr.Append(outDoc.Id);
                outIDStr.Append(",");
            }
            if (outIDStr.Length > 0)
            {
                outDtlList = IDReaBmsOutDtlDao.GetListByHQL("reabmsoutdtl.OutDocID in (" + outIDStr.ToString().TrimEnd(',') + ")");
            }
            if (outDtlList != null && outDtlList.Count > 0)
            {
                this.Entity = entity;
                if (this.Add())
                {
                    baseResultDataValue = AddStatisticsDtl(entity, outDtlList);
                    baseResultDataValue.ResultDataValue = ServiceCommon.RBAC.CommonServiceMethod.GetAddMethodResultStr(this.Entity);
                }
            }
        }
        /// <summary>
        /// "按日期范围+按使用部门"新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="baseResultDataValue"></param>
        private void AddOfDateAreaAndDept(ReaMonthUsageStatisticsDoc entity, ref BaseResultDataValue baseResultDataValue)
        {
            if (!entity.StartDate.HasValue)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "统计开始日期不能为空！";
                return;
            }
            if (!entity.EndDate.HasValue)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "统计结束日期不能为空！";
                return;
            }
            if (!entity.DeptID.HasValue)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "使用部门不能为空！";
                return;
            }
            if (entity.StartDate.HasValue)
                entity.StartDate = DateTime.Parse(entity.StartDate.Value.ToString("yyyy-MM-dd 00:00:00"));
            if (entity.EndDate.HasValue)
                entity.EndDate = DateTime.Parse(entity.EndDate.Value.ToString("yyyy-MM-dd 23:59:59"));
            string docHql = "reamonthusagestatisticsdoc.Visible=1 and reamonthusagestatisticsdoc.StartDate='" + entity.StartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' and reamonthusagestatisticsdoc.EndDate='" + entity.EndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            if (entity.DeptID.HasValue)
            {
                docHql = docHql + " and reamonthusagestatisticsdoc.DeptID = " + entity.DeptID.Value;
            }
            IList<ReaMonthUsageStatisticsDoc> dosList = this.SearchListByHQL(docHql);
            if (dosList.Count > 0)
            {
                baseResultDataValue.success = false;
                string errorInfo = "开始日期为：" + entity.StartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "，结束日期:" + entity.EndDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                if (entity.DeptID.HasValue)
                {
                    errorInfo = errorInfo + ",使用部门为:" + entity.DeptName;
                }
                errorInfo = errorInfo + ",已进行过统计！";
                return;
            }
            string outDocHql = string.Format("reabmsoutdoc.OutType={0} and reabmsoutdoc.DataAddTime>='{1}' and reabmsoutdoc.DataAddTime<='{2}'", ReaBmsOutDocOutType.使用出库.Key, entity.StartDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), entity.EndDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            if (entity.DeptID.HasValue)
            {
                outDocHql = outDocHql + " and reabmsoutdoc.DeptID = " + entity.DeptID.Value;
            }
            IList<ReaBmsOutDoc> outDocList = IDReaBmsOutDocDao.GetListByHQL(outDocHql);
            if (outDocList == null || outDocList.Count <= 0)
            {
                baseResultDataValue.success = false;
                string errorInfo = "开始日期为：" + entity.StartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "，结束日期:" + entity.EndDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                if (entity.DeptID.HasValue)
                {
                    errorInfo = errorInfo + ",使用部门为:" + entity.DeptName;
                }
                errorInfo = errorInfo + ",使用出库信息为空！";
                return;
            }
            AddOfGroupByDept(entity, outDocList, ref baseResultDataValue);
        }
        private void AddOfGroupByDept(ReaMonthUsageStatisticsDoc entity, IList<ReaBmsOutDoc> outDocList, ref BaseResultDataValue baseResultDataValue)
        {
            var deptGroupBy = outDocList.GroupBy(p => p.DeptID);
            StringBuilder outIDStr = new StringBuilder();
            IList<ReaBmsOutDtl> outDtlList = new List<ReaBmsOutDtl>();
            foreach (var groupBy in deptGroupBy)
            {
                outIDStr.Clear();
                outDtlList.Clear();
                foreach (var outDoc in groupBy)
                {
                    outIDStr.Append(outDoc.Id);
                    outIDStr.Append(",");
                }
                if (outIDStr.Length > 0)
                {
                    outDtlList = IDReaBmsOutDtlDao.GetListByHQL("reabmsoutdtl.OutDocID in (" + outIDStr.ToString().TrimEnd(',') + ")");
                }
                if (outDtlList != null && outDtlList.Count > 0)
                {
                    if (entity.EndDate.HasValue)
                        entity.EndDate = DateTime.Parse(entity.EndDate.Value.ToString("yyyy-MM-dd 23:59:59"));
                    this.Entity = entity;
                    if (this.Add())
                    {
                        baseResultDataValue = AddStatisticsDtl(entity, outDtlList);
                        baseResultDataValue.ResultDataValue = ServiceCommon.RBAC.CommonServiceMethod.GetAddMethodResultStr(this.Entity);
                    }
                }
            }
        }
        private BaseResultDataValue AddStatisticsDtl(ReaMonthUsageStatisticsDoc doc, IList<ReaBmsOutDtl> outDtlList)
        {
            BaseResultDataValue baseResultBool = new BaseResultDataValue();

            //按产品编码+包装单位+规格进行出库货品合并
            var outDtlGroupBy = outDtlList.GroupBy(p => new
            {
                p.ReaGoodsNo,
                p.GoodsUnit,
                p.UnitMemo
            });
            foreach (var groupBy in outDtlGroupBy)
            {
                ReaMonthUsageStatisticsDtl dtl = new ReaMonthUsageStatisticsDtl();
                dtl.DocID = doc.Id;
                dtl.Visible = true;
                if (doc.DeptID.HasValue)
                {
                    dtl.DeptID = doc.DeptID.Value;
                    dtl.DeptName = doc.DeptName;
                }
                dtl.GoodsName = groupBy.ElementAt(0).GoodsCName;
                dtl.ReaGoodsNo = groupBy.ElementAt(0).ReaGoodsNo;
                dtl.ProdGoodsNo = groupBy.ElementAt(0).ProdGoodsNo;
                dtl.CenOrgGoodsNo = groupBy.ElementAt(0).CenOrgGoodsNo;

                //统计的包装单位转换
                dtl.GoodsUnit = groupBy.ElementAt(0).GoodsUnit;
                dtl.UnitMemo = groupBy.ElementAt(0).UnitMemo;
                dtl.OutQty = groupBy.Sum(p => p.GoodsQty);

                dtl.DataAddTime = DateTime.Now;
                dtl.DataUpdateTime = DateTime.Now;
                IBReaMonthUsageStatisticsDtl.Entity = dtl;
                baseResultBool.success = IBReaMonthUsageStatisticsDtl.Add();
                if (baseResultBool.success == false)
                {
                    baseResultBool.ErrorInfo = "保存产品编码为:" + dtl.ReaGoodsNo + "失败!";
                    break;
                }
            }
            return baseResultBool;
        }
        private string GetDocNo()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));
            Random ran = new Random();
            int randKey = ran.Next(0, 999);
            strb.Append(randKey.ToString().PadLeft(3, '0'));//左补零
            strb.Append(DateTime.Now.ToString("HHmmssfff"));
            return strb.ToString();
        }
        public BaseResultBool RemoveDocAndDtlByDocId(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            int deleteCount = IBReaMonthUsageStatisticsDtl.DeleteByHql(string.Format(" From ReaMonthUsageStatisticsDtl reamonthusagestatisticsdtl where  reamonthusagestatisticsdtl.DocID={0}", id.ToString()));
            if (deleteCount <= 0)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：删除出库使用量明细信息失败!";
                return baseResultBool;
            }
            baseResultBool.success = this.Remove(id);
            if (!baseResultBool.success)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：删除出库使用量主单信息失败!";
            }
            return baseResultBool;
        }
        public Stream GetPdfReportOfTypeById(string reaReportClass, long id, long labID, string labCName, string breportType, string frx, ref string pdfFileName)
        {
            Stream stream = null;
            ReaMonthUsageStatisticsDoc doc = this.Get(id);
            if (doc == null)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取出库使用量统计单PDF清单数据信息为空!");
            }
            IList<ReaMonthUsageStatisticsDtl> dtlList = IBReaMonthUsageStatisticsDtl.SearchListByHQL("reamonthusagestatisticsdtl.DocID=" + id);
            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取出库使用量统计单PDF清单明细信息为空!");
            }

            pdfFileName = doc.DocNo + ".pdf";
            //string milliseconds = "";
            if (reaReportClass == ReaReportClass.Frx.Key)
            {
                stream = CreatePdfReportOfFrxById(doc, dtlList, frx);
            }
            else if (reaReportClass == ReaReportClass.Excel.Key)
            {
                string excelFileFullDir = "";
                //获取出库使用量单模板
                if (string.IsNullOrEmpty(frx))
                    frx = "出库使用量.xlsx";
                string fileExt = frx.Substring(frx.LastIndexOf("."));
                string excelFile = doc.DocNo.ToString() + fileExt;
                Stream stream2 = null;
                ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
                stream2 = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaMonthUsageStatisticsDoc, ReaMonthUsageStatisticsDtl>(doc, dtlList, excelCommand, breportType, doc.LabID, frx, excelFile, ref excelFileFullDir);
                stream2.Close();
                string pdfFullDir = "";

                bool result = ExcelToPdfReportHelp.ExcelToPDF(excelFileFullDir, breportType, doc.LabID, pdfFileName, ref pdfFullDir);
                if (result)
                {
                    stream = PdfReportHelp.GetReportPDF(pdfFullDir);
                }
            }

            return stream;
        }
        private Stream CreatePdfReportOfFrxById(ReaMonthUsageStatisticsDoc outDoc, IList<ReaMonthUsageStatisticsDtl> dtlList, string frx)
        {
            Stream stream = null;
            DataSet dataSet = new DataSet();
            dataSet.DataSetName = "ZhiFang.ReagentSys.Client";

            List<ReaMonthUsageStatisticsDoc> docList = new List<ReaMonthUsageStatisticsDoc>();
            docList.Add(outDoc);
            DataTable docDt = ReportBTemplateHelp.ToDataTable<ReaMonthUsageStatisticsDoc>(docList, null);
            docDt.TableName = "Rea_MonthUsageStatisticsDoc";
            dataSet.Tables.Add(docDt);

            DataTable dtDtl = ReportBTemplateHelp.ToDataTable<ReaMonthUsageStatisticsDtl>(dtlList, null);
            if (dtDtl != null)
            {
                dtDtl.TableName = "Rea_MonthUsageStatisticsDtl";
                dataSet.Tables.Add(dtDtl);
            }
            string pdfName = outDoc.DocNo.ToString() + ".pdf";
            //如果当前实验室还没有维护出库使用量单报表模板,默认使用公共的出库使用量单模板
            if (string.IsNullOrEmpty(frx))
                frx = "出库使用量.frx";
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            ExcelReportHelp.CreateEECDataTable(excelCommand, ref dataSet);
            stream = FrxToPdfReportHelp.SavePdfReport(dataSet, outDoc.LabID, pdfName, FrxToPdfReportHelp.PublicTemplateDir, BTemplateType.GetStatusDic()[BTemplateType.出库使用量.Key].Name, frx, false);

            return stream;
        }
        public Stream GetExcelReportOfExcelById(long id, long labID, string labCName, string breportType, string frx, ref string fileName)
        {
            Stream stream = null;
            ReaMonthUsageStatisticsDoc doc = this.Get(id);
            if (doc == null)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取出库使用量统计单数据信息为空!");
            }
            IList<ReaMonthUsageStatisticsDtl> dtlList = IBReaMonthUsageStatisticsDtl.SearchListByHQL("reamonthusagestatisticsdtl.DocID=" + id);
            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取出库使用量统计单明细信息为空!");
            }

            //获取出库使用量单模板
            if (string.IsNullOrEmpty(frx))
                frx = "出库使用量.xlsx";
            string saveFullPath = "";
            string fileExt = frx.Substring(frx.LastIndexOf("."));
            string excelFile = doc.DocNo.ToString() + fileExt;
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaMonthUsageStatisticsDoc, ReaMonthUsageStatisticsDtl>(doc, dtlList, excelCommand, breportType, doc.LabID, frx, excelFile, ref saveFullPath);
            fileName = doc.DeptName + "出库使用量统计信息" + fileExt;
            return stream;
        }

    }
}