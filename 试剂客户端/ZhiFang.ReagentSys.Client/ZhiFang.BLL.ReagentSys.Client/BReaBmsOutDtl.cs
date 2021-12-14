using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using System.Text;
using System.Collections.Generic;
using System;
using System.Linq;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.ServiceCommon.RBAC;
using System.IO;
using ZhiFang.ReagentSys.Client.Common;
using System.Data;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Statistics;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Response;
using Newtonsoft.Json.Linq;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.RBAC;
using ZhiFang.BLL.ReagentSys.Client.OutDtlListGroupBy;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class BReaBmsOutDtl : BaseBLL<ReaBmsOutDtl>, ZhiFang.IBLL.ReagentSys.Client.IBReaBmsOutDtl
    {
        IDReaCenOrgDao IDReaCenOrgDao { get; set; }
        IDReaStorageDao IDReaStorageDao { get; set; }
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IDHRDeptDao IDHRDeptDao { get; set; }
        IDReaTestEquipLabDao IDReaTestEquipLabDao { get; set; }
        IBBDict IBBDict { get; set; }

        public BaseResultDataValue AddOutDtlList(ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> dtAddList, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (dtAddList == null || dtAddList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "不能新增出库保存,传入参数dtAddList为空!";
                return brdv;
            }
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach (var outDtl in dtAddList)
            {
                if (brdv.success == false)
                {
                    brdv.ErrorInfo = "新增保存出库明细失败!";
                    break;
                }
                if (outDoc.Status.ToString() == ReaBmsOutDocStatus.出库完成.Key && outDtl.GoodsQty <= 0 && outDtl.ReqGoodsQty.HasValue)
                    outDtl.GoodsQty = outDtl.ReqGoodsQty.Value;
                outDtl.OutDocID = outDoc.Id;
                outDtl.Visible = true;
                outDtl.DataAddTime = DateTime.Now;
                outDtl.DataUpdateTime = DateTime.Now;
                outDtl.DataTimeStamp = dataTimeStamp;
                outDtl.SumTotal = outDtl.GoodsQty * outDtl.Price;
                ZhiFang.Common.Log.Log.Debug("AddOutDtlList.outDtl.Id:" + outDtl.Id + ",outDoc.Id:" + outDoc.Id + ",outDtl.GoodsQty:" + outDtl.GoodsQty + ",outDtl.Price:" + outDtl.Price + ",outDtl.SumTotal:" + outDtl.SumTotal);
                this.Entity = outDtl;
                brdv.success = this.Add();
            }
            return brdv;
        }
        public BaseResultBool UpdateOutDtlList(ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> dtEditList)
        {
            BaseResultBool brdv = new BaseResultBool();
            if (dtEditList == null || dtEditList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "不能编辑保存出库明细,传入参数dtEditList为空!";
                return brdv;
            }
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach (var outDtl in dtEditList)
            {
                if (brdv.success == false)
                {
                    brdv.ErrorInfo = "编辑保存出库明细失败!";
                    break;
                }
                outDtl.OutDocID = outDoc.Id;
                outDtl.DataUpdateTime = DateTime.Now;
                if (outDoc.Status.ToString() == ReaBmsOutDocStatus.出库完成.Key && outDtl.GoodsQty <= 0 && outDtl.ReqGoodsQty.HasValue)
                    outDtl.GoodsQty = outDtl.ReqGoodsQty.Value;
                outDtl.DataTimeStamp = this.Get(outDtl.Id).DataTimeStamp;
                outDtl.SumTotal = outDtl.GoodsQty * outDtl.Price;
                ZhiFang.Common.Log.Log.Debug("UpdateOutDtlList.outDtl.Id:" + outDtl.Id + ",outDoc.Id:" + outDoc.Id + ",outDtl.GoodsQty:" + outDtl.GoodsQty + ",outDtl.Price:" + outDtl.Price + ",outDtl.SumTotal:" + outDtl.SumTotal);
                //brdv.success = ((IDReaBmsOutDtlDao)base.DBDao).Update(outDtl);
                this.Entity = outDtl;
                brdv.success = this.Edit();
            }
            return brdv;
        }
        #region 统计报表/Excel导出/PDF预览
        public EntityList<ReaBmsOutDtl> SearchReaBmsOutDtlSummaryByHQL(int groupType, string companyId, string deptId, string testEquipId, string startDate, string endDate, string sort, int page, int limit)
        {
            EntityList<ReaBmsOutDtl> entityList = new EntityList<ReaBmsOutDtl>();
            StringBuilder outDocHql = new StringBuilder();
            StringBuilder outDtlHql = new StringBuilder();

            long tempDeptId = -1;
            if (!string.IsNullOrEmpty(deptId) && long.TryParse(deptId, out tempDeptId))
            {
                outDocHql.Append("reabmsoutdoc.DeptID=" + tempDeptId);
                outDocHql.Append(" and ");
            }
            DateTime startDate1;
            if (!string.IsNullOrEmpty(startDate) && DateTime.TryParse(startDate, out startDate1))
            {
                outDocHql.Append("reabmsoutdoc.DataAddTime>='" + startDate1.ToString("yyyy-MM-dd 00:00:00") + "'");
                outDocHql.Append(" and ");
            }
            DateTime endDate1;
            if (!string.IsNullOrEmpty(endDate) && DateTime.TryParse(endDate, out endDate1))
            {
                outDocHql.Append("reabmsoutdoc.DataAddTime<='" + endDate1.ToString("yyyy-MM-dd 23:59:59") + "'");
                outDocHql.Append(" and ");
            }

            long tempCompanyId = -1;
            if (!string.IsNullOrEmpty(companyId) && long.TryParse(companyId, out tempCompanyId))
            {
                outDtlHql.Append("reabmsoutdtl.ReaCompanyID=" + tempCompanyId);
                outDtlHql.Append(" and ");
            }
            long tempTestEquipId = -1;
            if (!string.IsNullOrEmpty(testEquipId) && long.TryParse(testEquipId, out tempTestEquipId))
            {
                outDtlHql.Append("reabmsoutdtl.TestEquipID=" + tempTestEquipId);
                outDtlHql.Append(" and ");
            }

            if (outDocHql.Length <= 0 && outDtlHql.Length <= 0)
                return entityList;
            //去除最后的 and 
            if (outDocHql.Length > 0)
                outDocHql = outDocHql.Remove(outDocHql.Length - 5, 5);
            if (outDtlHql.Length > 0)
                outDtlHql = outDtlHql.Remove(outDocHql.Length - 5, 5);
            IList<ReaBmsOutDtl> dtlList = ((IDReaBmsOutDtlDao)base.DBDao).SearchReaBmsOutDtlSummaryByHQL(outDocHql.ToString(), outDtlHql.ToString(), "", sort, page, limit);
            dtlList = SearchReaBmsOutDtlListOfGroupBy1(dtlList);
            entityList.count = dtlList.Count;
            //分页处理
            if (limit > 0 && limit < dtlList.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = dtlList.Skip(startIndex).Take(endIndex);
                if (list != null)
                    dtlList = list.ToList();
            }
            entityList.list = dtlList;
            return entityList;
        }
        public IList<ReaBmsOutDtl> SearchReaBmsOutDtlSummaryListByHQL(string docHql, string dtlHql, string reaGoodsHql, string order, int page, int limit)
        {
            IList<ReaBmsOutDtl> dtlList = new List<ReaBmsOutDtl>();
            if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
                return dtlList;
            dtlList = ((IDReaBmsOutDtlDao)base.DBDao).SearchReaBmsOutDtlSummaryByHQL(docHql, dtlHql, reaGoodsHql, order, page, limit);
            return dtlList;
        }
        public EntityList<ReaBmsOutDtl> SearchReaBmsOutDtlSummaryByHQL(int groupType, string docHql, string dtlHql, string reaGoodsHql, string order, int page, int limit)
        {
            EntityList<ReaBmsOutDtl> entityList = new EntityList<ReaBmsOutDtl>();
            if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
                return entityList;

            IList<ReaBmsOutDtl> dtlList = ((IDReaBmsOutDtlDao)base.DBDao).SearchReaBmsOutDtlSummaryByHQL(docHql, dtlHql, reaGoodsHql, order, -1, -1);

            //if (groupType.ToString() == ReaBmsOutDtlStatisticalType.按货品规格.Key)
            //{
            //    dtlList = SearchReaBmsOutDtlListOfGroupBy1(dtlList);
            //}
            //else if (groupType.ToString() == ReaBmsOutDtlStatisticalType.按货品批号.Key)
            //{
            //    dtlList = SearchReaBmsOutDtlListOfGroupBy1(dtlList);
            //}
            //else if(groupType.ToString() == ReaBmsOutDtlStatisticalType.按出库明细常规合并.Key)
            //{
            //    dtlList = SearchReaBmsOutDtlListOfGroupBy1(dtlList);
            //}
            //else
            //{
            //    dtlList = SearchReaBmsOutDtlListOfGroupBy1(dtlList);
            //}

            OutDtlListGroupByStrategy outDtlListGroupByStrategy = null;
            if (groupType == int.Parse(ReaBmsOutDtlStatisticalType.按货品规格.Key))
            {
                outDtlListGroupByStrategy = new OutDtlGroupByReaGoodsNoGoodsUnit();
            }
            else if (groupType == int.Parse(ReaBmsOutDtlStatisticalType.按供应商加批号及货品.Key))
            {
                outDtlListGroupByStrategy = new OutDtlGroupByCompReaGoodsNo();
            }
            else if (groupType == int.Parse(ReaBmsOutDtlStatisticalType.按出库明细常规合并.Key))
            {
                outDtlListGroupByStrategy = new OutDtlGroupByOfGroupBy12();
            }
            else if (groupType == int.Parse(ReaBmsOutDtlStatisticalType.按出库单汇总.Key))
            {
                outDtlListGroupByStrategy = new OutDtlGroupByOfGroupBy13();
            }
            IList<ReaBmsOutDtl> tempDtlList = new List<ReaBmsOutDtl>();
            if (outDtlListGroupByStrategy != null)
            {
                OutDtlListGroupByContext context = new OutDtlListGroupByContext(IDReaGoodsDao, dtlList, outDtlListGroupByStrategy);
                tempDtlList = context.SearchReaDtlListOfGroupBy();
            }

            entityList.count = tempDtlList.Count;
            //分页处理
            if (limit > 0 && limit < tempDtlList.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = tempDtlList.Skip(startIndex).Take(endIndex);
                if (list != null)
                    tempDtlList = list.ToList();
            }
            entityList.list = tempDtlList;
            return entityList;
        }

        public Stream SearchBusinessSummaryReportOfExcelByHql(long labID, string labCName, int groupType, string docHql, string dtlHql, string reaGoodsHql, string sort, string breportType, string frx, ref string fileName, string startDate, string endDate)
        {
            if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取统计条件为空!");
            }
            Stream stream = null;
            IList<ReaBmsOutDtl> dtlList = new List<ReaBmsOutDtl>();
            dtlList = ((IDReaBmsOutDtlDao)base.DBDao).SearchReaBmsOutDtlSummaryByHQL(docHql, dtlHql, reaGoodsHql, sort, -1, -1);
            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取出库单明细信息为空!");
            }
            //dtlList = SearchReaBmsOutDtlListOfGroupBy1(dtlList);
            
            OutDtlListGroupByStrategy outDtlListGroupByStrategy = null;
            if (groupType == int.Parse(ReaBmsOutDtlStatisticalType.按货品规格.Key))
            {
                outDtlListGroupByStrategy = new OutDtlGroupByReaGoodsNoGoodsUnit();
            }
            else if (groupType == int.Parse(ReaBmsOutDtlStatisticalType.按供应商加批号及货品.Key))
            {
                outDtlListGroupByStrategy = new OutDtlGroupByCompReaGoodsNo();
            }
            else if (groupType == int.Parse(ReaBmsOutDtlStatisticalType.按出库明细常规合并.Key))
            {
                outDtlListGroupByStrategy = new OutDtlGroupByOfGroupBy12();
            }
            else if (groupType == int.Parse(ReaBmsOutDtlStatisticalType.按出库单汇总.Key))
            {
                outDtlListGroupByStrategy = new OutDtlGroupByOfGroupBy13();
            }

            IList<ReaBmsOutDtl> tempDtlList = new List<ReaBmsOutDtl>();
            if (outDtlListGroupByStrategy != null)
            {
                OutDtlListGroupByContext context = new OutDtlListGroupByContext(IDReaGoodsDao, dtlList, outDtlListGroupByStrategy);
                tempDtlList = context.SearchReaDtlListOfGroupBy();
            }
            
            //获取出库汇总模板
            if (string.IsNullOrEmpty(frx))
                frx = "出库汇总.xlsx";
            string saveFullPath = "";
            string fileExt = frx.Substring(frx.LastIndexOf("."));
            string excelFile = frx;
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand(startDate, endDate);
            stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsOutDoc, ReaBmsOutDtl>(null, tempDtlList, excelCommand, breportType, labID, frx, excelFile, ref saveFullPath);
            fileName = "出库汇总信息" + fileExt;
            return stream;
        }
        public Stream SearchBusinessSummaryReportOfPdfByHql(string reaReportClass, long labId, string labCName, int groupType, string docHql, string dtlHql, string reaGoodsHql, string sort, string breportType, string frx, ref string pdfFileName, string startDate, string endDate)
        {
            Stream stream = null;
            if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取统计条件为空!");
            }
            IList<ReaBmsOutDtl> dtlList = ((IDReaBmsOutDtlDao)base.DBDao).SearchReaBmsOutDtlSummaryByHQL(docHql, dtlHql, reaGoodsHql, sort, -1, -1);

            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取出库汇总明细信息为空!");
            }
            dtlList = SearchReaBmsOutDtlListOfGroupBy1(dtlList);
            pdfFileName = "出库汇总_" + DateTime.Now.ToString("yyMMddHHmmss") + ".pdf";
            if (reaReportClass == ReaReportClass.Frx.Key)
            {
                stream = SearchPdfReportOfFrxByHql(labId, labCName, pdfFileName, dtlList, frx, startDate, endDate);
            }
            else if (reaReportClass == ReaReportClass.Excel.Key)
            {
                string excelFileFullDir = "";
                //获取出库汇总模板
                if (string.IsNullOrEmpty(frx))
                    frx = "出库汇总.xlsx";
                string fileExt = frx.Substring(frx.LastIndexOf("."));
                string excelFile = fileExt;
                Stream stream2 = null;
                ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand(startDate, endDate);
                stream2 = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsOutDoc, ReaBmsOutDtl>(null, dtlList, excelCommand, breportType, labId, frx, excelFile, ref excelFileFullDir);
                stream2.Close();
                string pdfFullDir = "";
                bool result = ExcelToPdfReportHelp.ExcelToPDF(excelFileFullDir, breportType, labId, pdfFileName, ref pdfFullDir);
                if (result)
                {
                    stream = PdfReportHelp.GetReportPDF(pdfFullDir);
                }
            }
            return stream;
        }
        private Stream SearchPdfReportOfFrxByHql(long labId, string labCName, string pdfFileName, IList<ReaBmsOutDtl> dtlList, string frx, string startDate, string endDate)
        {
            Stream stream = null;
            DataSet dataSet = new DataSet();
            dataSet.DataSetName = "ZhiFang.ReagentSys.Client";
            DataTable dtDtl = ReportBTemplateHelp.ToDataTable<ReaBmsOutDtl>(dtlList, null);
            if (dtDtl != null)
            {
                dtDtl.TableName = "Rea_BmsOutDtl";
                dataSet.Tables.Add(dtDtl);
            }
            //如果当前实验室还没有维护出库汇总报表模板,默认使用公共的出库汇总模板
            if (string.IsNullOrEmpty(frx))
                frx = "出库汇总.frx";
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand(startDate, endDate);
            ExcelReportHelp.CreateEECDataTable(excelCommand, ref dataSet);
            stream = FrxToPdfReportHelp.SavePdfReport(dataSet, labId, pdfFileName, FrxToPdfReportHelp.PublicTemplateDir, BTemplateType.GetStatusDic()[BTemplateType.出库汇总.Key].Name, frx, false);
            return stream;
        }
        public EntityList<ReaBmsOutDtl> SearchReaBmsOutDtEntityListByJoinHql(int groupType, string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, int page, int limit, string sort)
        {
            EntityList<ReaBmsOutDtl> entityList = new EntityList<ReaBmsOutDtl>();
            IList<ReaBmsOutDtl> tempOutDtlList = ((IDReaBmsOutDtlDao)base.DBDao).SearchReaBmsOutDtlListByJoinHql(docHql, dtlHql, deptGoodsHql, reaGoodsHql, -1, -1, sort);
            //按使用仪器+货品ID分组
            tempOutDtlList = SearchReaBmsOutDtlListOfGroupBy2(tempOutDtlList);
            entityList.count = tempOutDtlList.Count();
            //分页处理
            if (limit > 0 && limit < tempOutDtlList.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = tempOutDtlList.Skip(startIndex).Take(endIndex);
                if (list != null)
                    tempOutDtlList = list.ToList();
            }
            entityList.list = tempOutDtlList;
            return entityList;
        }
        /// <summary>
        /// 使用出库明细:领用部门ID+供应商ID+使用仪器ID+货品产品编码+包装单位+规格+批号+效期+出库人ID+使用时间
        /// </summary>
        /// <param name="outDtlList"></param>
        /// <returns></returns>
        private IList<ReaBmsOutDtl> SearchReaBmsOutDtlListOfGroupBy1(IList<ReaBmsOutDtl> outDtlList)
        {
            return outDtlList.GroupBy(p => new
            {
                p.DeptID,
                p.ReaCompanyID,
                p.TestEquipID,
                p.ReaGoodsNo,
                p.GoodsUnit,
                p.UnitMemo,
                p.LotNo,
                p.InvalidDate,
                p.CreaterID,
                p.DataAddTime,
                p.TransportNo,
                p.OutDocNo,
                p.SName,
                p.TestCount
            }).Select(g => new ReaBmsOutDtl
            {
                DeptID = g.Key.DeptID,
                DeptName = g.ElementAt(0).DeptName,
                ReaCompanyID = g.Key.ReaCompanyID,
                CompGoodsLinkID = g.ElementAt(0).CompGoodsLinkID,
                CompanyName = g.ElementAt(0).CompanyName,
                ReaGoodsNo = g.Key.ReaGoodsNo,
                ProdGoodsNo = g.ElementAt(0).ProdGoodsNo,
                GoodsNo = g.ElementAt(0).GoodsNo,
                CenOrgGoodsNo = g.ElementAt(0).CenOrgGoodsNo,

                GoodsID = g.ElementAt(0).GoodsID,
                GoodsCName = g.ElementAt(0).GoodsCName,
                GoodsUnit = g.ElementAt(0).GoodsUnit,
                UnitMemo = g.ElementAt(0).UnitMemo,
                LotNo = g.Key.LotNo,
                InvalidDate = g.ElementAt(0).InvalidDate,
                BarCodeType = g.ElementAt(0).BarCodeType,
                TestEquipID = g.ElementAt(0).TestEquipID,
                TestEquipName = g.ElementAt(0).TestEquipName,
                CreaterID = g.ElementAt(0).CreaterID,
                CreaterName = g.ElementAt(0).CreaterName,
                DataAddTime = g.ElementAt(0).DataAddTime,
                ReqGoodsQty = g.Sum(k => k.ReqGoodsQty),
                GoodsQty = g.Sum(k => k.GoodsQty),
                SumTotal = g.Sum(k => k.SumTotal),
                Price = g.Sum(k => k.GoodsQty) > 0 ? g.Sum(k => k.SumTotal) / g.Sum(k => k.GoodsQty) : 0,//g.ElementAt(0).Price,
                ProdDate = g.ElementAt(0).ProdDate,
                TaxRate = g.ElementAt(0).TaxRate,
                Memo = g.ElementAt(0).Memo,
                TransportNo = g.ElementAt(0).TransportNo,
                OutDocNo = g.ElementAt(0).OutDocNo,
                SName = g.ElementAt(0).SName,
                TestCount = g.ElementAt(0).TestCount
            }).ToList();
        }
        /// <summary>
        /// 获取仪器试剂使用量:按使用仪器+货品ID分组
        /// </summary>
        /// <param name="tempOutDtlList"></param>
        /// <returns></returns>
        private IList<ReaBmsOutDtl> SearchReaBmsOutDtlListOfGroupBy2(IList<ReaBmsOutDtl> tempOutDtlList)
        {
            IList<ReaBmsOutDtl> outDtlList = new List<ReaBmsOutDtl>();
            if (tempOutDtlList != null && tempOutDtlList.Count > 0)
            {
                var groupBy = tempOutDtlList.GroupBy(p => new
                {
                    p.TestEquipID,
                    p.GoodsID
                });
                Dictionary<long, ReaGoods> goodsList = new Dictionary<long, ReaGoods>();
                foreach (var model in groupBy)
                {
                    ReaBmsOutDtl outDtl = ClassMapperHelp.GetMapper<ReaBmsOutDtl, ReaBmsOutDtl>(model.ElementAt(0));
                    outDtl.GoodsQty = model.Sum(k => k.GoodsQty);
                    outDtl.SumTotal = model.Sum(k => k.SumTotal);
                    //平均价格
                    if (outDtl.GoodsQty > 0)
                        outDtl.Price = outDtl.SumTotal / outDtl.GoodsQty;
                    long goodId = model.ElementAt(0).GoodsID.Value;
                    ReaGoods goods = null;
                    if (!goodsList.ContainsKey(goodId))
                    {
                        goods = IDReaGoodsDao.Get(goodId, false);
                        if (goods != null)
                            goodsList.Add(goodId, goods);
                    }
                    else
                    {
                        goods = goodsList[goodId];
                    }
                    if (goods != null)
                    {
                        outDtl.EName = goods.EName;
                        outDtl.SName = goods.SName;
                        outDtl.Purpose = goods.Purpose;
                        outDtl.StorageType = goods.StorageType;
                    }
                    outDtlList.Add(outDtl);
                }
            }
            return outDtlList;
        }
        public EntityList<EChartsVO> SearchEquipReagUsageEChartsVOListByHql(int statisticType, bool showZero, string startDate, string endDate, string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, string sort)
        {
            EntityList<EChartsVO> entityList = new EntityList<EChartsVO>();
            entityList.list = new List<EChartsVO>();
            IList<ReaBmsOutDtl> dtlList = ((IDReaBmsOutDtlDao)base.DBDao).SearchOutDocAndDtlListByAllJoinHql(docHql, dtlHql, deptGoodsHql, reaGoodsHql, -1, -1, sort);
            switch (statisticType)
            {
                case 1://仪器试剂使用量
                    entityList.list = SearchOutEChartsVOListOfGroupByEquipReagUsage(dtlList, showZero);
                    break;
                default:

                    break;
            }
            entityList.count = entityList.list.Count();
            return entityList;
        }
        private IList<EChartsVO> SearchOutEChartsVOListOfGroupByEquipReagUsage(IList<ReaBmsOutDtl> dtlList, bool showZero)
        {
            IList<EChartsVO> voList = new List<EChartsVO>();
            var groupBy = dtlList.GroupBy(p => new
            {
                p.TestEquipID,
                p.GoodsID
            });
            var allGoodsQty = dtlList.Sum(k => k.GoodsQty);
            var allSumTotal = dtlList.Sum(k => k.SumTotal);
            StringBuilder hqlStr = new StringBuilder();
            bool hasNull = false;
            if (groupBy != null && groupBy.Count() > 0)
            {
                voList = groupBy.Select(g => new EChartsVO
                {
                    TestEquipID = !string.IsNullOrEmpty(g.Key.TestEquipID.ToString()) == true ? g.Key.TestEquipID.ToString() : "",
                    TestEquipName = !string.IsNullOrEmpty(g.ElementAt(0).TestEquipName) == true ? g.ElementAt(0).TestEquipName.ToString() : "未知",
                    ReaGoodsNo = g.ElementAt(0).ReaGoodsNo,
                    GoodsCName = g.ElementAt(0).GoodsCName,
                    GoodsUnit = g.ElementAt(0).GoodsUnit,
                    GoodsQty = g.Sum(k => k.GoodsQty),
                    AllGoodsQty = allGoodsQty,
                    GoodsQtyPercent = (allGoodsQty > 0 ? g.Sum(k => k.GoodsQty) / allGoodsQty * 100 : 0),
                    SumTotal = g.Sum(k => k.SumTotal),
                    AllSumTotal = allSumTotal,
                    SumTotalPercent = (allSumTotal > 0 ? g.Sum(k => k.SumTotal) / allSumTotal * 100 : 0)
                }).ToList();
                foreach (var item in groupBy)
                {
                    if (item.Key.TestEquipID.HasValue)
                    {
                        hqlStr.Append(item.Key.TestEquipID);
                        hqlStr.Append(",");
                    }
                    else if (hasNull == false)
                    {
                        hasNull = true;
                    }
                }
            }
            if (!showZero) return voList;

            //没有使用出库记录的仪器信息
            string hql2 = "reatestequiplab.Visible=1";
            if (hqlStr.Length > 0)
            {
                hql2 = hql2 + " and reatestequiplab.Id not in(" + hqlStr.ToString().TrimEnd(',') + ")";
            }
            IList<ReaTestEquipLab> equipLabList = IDReaTestEquipLabDao.GetListByHQL(hql2);
            foreach (ReaTestEquipLab entity in equipLabList)
            {
                EChartsVO vo = new EChartsVO();
                vo.TestEquipID = entity.Id.ToString();
                vo.TestEquipName = entity.CName;
                vo.AllGoodsQty = allGoodsQty;
                vo.GoodsQty = 0;
                vo.GoodsQtyPercent = 0;
                vo.AllSumTotal = allSumTotal;
                vo.SumTotal = 0;
                vo.SumTotalPercent = 0;
                voList.Add(vo);
            }
            return voList;
        }
        public IList<ReaBmsOutDtl> SearchOutDocAndDtlListByAllJoinHql(string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, int page, int limit, string sort)
        {
            IList<ReaBmsOutDtl> entityList = new List<ReaBmsOutDtl>();
            entityList = ((IDReaBmsOutDtlDao)base.DBDao).SearchOutDocAndDtlListByAllJoinHql(docHql, dtlHql, deptGoodsHql, reaGoodsHql, page, limit, sort);
            return entityList;
        }

        #region 出库变更台账-导出Excel/PDF预览
        public Stream SearchReaBmsOutDtlLotNoAndTransportNoChangeOfExcelPdfByHQL(string reaReportClass, long labId, string labCName, string docHql, string dtlHql, string reaGoodsHql, string sort, string breportType, string frx, ref string fileName, string startDate, string endDate)
        {
            Stream stream = null;
            if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取统计条件为空!");
            }
            IList<ReaBmsOutDtl> dtlList = ((IDReaBmsOutDtlDao)base.DBDao).SearchReaBmsOutDtlSummaryByHQL(docHql, dtlHql, reaGoodsHql, sort, -1, -1);
            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取出库变更台账信息为空!");
            }

            foreach (var l in dtlList)
            {
                //批号是否改变
                if (!string.IsNullOrWhiteSpace(l.LastLotNo) && ("^" + l.LastLotNo + "^").IndexOf("^" + l.LotNo + "^") >= 0)
                {
                    l.LotNoIsChange = "否";
                }
                else
                {
                    l.LotNoIsChange = "是";
                }
                //货运单是否改变
                if (!string.IsNullOrWhiteSpace(l.LastTransportNo) && ("^" + l.LastTransportNo + "^").IndexOf("^" + l.TransportNo + "^") >= 0)
                {
                    l.TransportNoIsChange = "否";
                }
                else
                {
                    l.TransportNoIsChange = "是";
                }
            }
                       
            if (reaReportClass == ReaReportClass.Frx.Key)
            {
                fileName = "出库变更台账_" + DateTime.Now.ToString("yyMMddHHmmss") + ".pdf";
                DataSet dataSet = new DataSet();
                dataSet.DataSetName = "ZhiFang.ReagentSys.Client";
                DataTable dtDtl = ReportBTemplateHelp.ToDataTable<ReaBmsOutDtl>(dtlList, null);
                if (dtDtl != null)
                {
                    dtDtl.TableName = "Rea_BmsOutDtl";
                    dataSet.Tables.Add(dtDtl);
                }
                //如果当前实验室还没有维护出库汇总报表模板,默认使用公共的出库汇总模板
                if (string.IsNullOrEmpty(frx))
                    frx = "出库变更台账.frx";
                ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand(startDate, endDate);
                ExcelReportHelp.CreateEECDataTable(excelCommand, ref dataSet);
                stream = FrxToPdfReportHelp.SavePdfReport(dataSet, labId, fileName, FrxToPdfReportHelp.PublicTemplateDir, BTemplateType.GetStatusDic()[BTemplateType.出库变更台账.Key].Name, frx, false);
            }
            else if (reaReportClass == ReaReportClass.Excel.Key)
            {
                if (string.IsNullOrEmpty(frx))
                    frx = "出库变更台账.xlsx";
                string saveFullPath = "";
                string excelFile = frx;
                ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand(startDate, endDate);
                stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsOutDoc, ReaBmsOutDtl>(null, dtlList, excelCommand, breportType, labId, frx, excelFile, ref saveFullPath);
                fileName = "出库变更台账.xlsx";          
            }
            return stream;
        }
        

        #endregion

        #endregion
        #region EChart图表统计
        public EntityList<EChartsVO> SearchOutEChartsVOListByHql(int statisticType, bool showZero, string startDate, string endDate, string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, string sort)
        {
            EntityList<EChartsVO> entityList = new EntityList<EChartsVO>();
            entityList.list = new List<EChartsVO>();
            IList<ReaBmsOutDtl> dtlList = ((IDReaBmsOutDtlDao)base.DBDao).SearchOutDocAndDtlListByAllJoinHql(docHql, dtlHql, deptGoodsHql, reaGoodsHql, -1, -1, sort);
            switch (statisticType)
            {
                case 1://按库房
                    entityList.list = SearchOutEChartsVOListOfGroupByStorage(dtlList, showZero);
                    break;
                case 2://按供货商
                    entityList.list = SearchOutEChartsVOListOfGroupByComp(dtlList, showZero);
                    break;
                case 3://按品牌
                    //SearchOutEChartsVOListBySetGoodsInfo(ref dtlList);
                    entityList.list = SearchOutEChartsVOListOfGroupByProdOrg(dtlList, showZero);
                    break;
                case 4://按货品一级分类
                    //SearchOutEChartsVOListBySetGoodsInfo(ref dtlList);
                    entityList.list = SearchOutEChartsVOListOfGroupByGoodsClass(dtlList, showZero);
                    break;
                case 5://按货品二级分类
                       // SearchOutEChartsVOListBySetGoodsInfo(ref dtlList);
                    entityList.list = SearchOutEChartsVOListOfGroupByProdOrg(dtlList, showZero);
                    break;
                case 6://按仪器
                    entityList.list = SearchOutEChartsVOListOfGroupByEquip(dtlList, showZero);
                    break;
                case 7://按部门
                    //SearchOutEChartsVOListBySetGoodsInfo(ref dtlList);
                    entityList.list = SearchOutEChartsVOListOfGroupByDept(dtlList, showZero);
                    break;
                default:

                    break;
            }
            if (entityList.list != null && entityList.list.Count > 0) entityList.list = entityList.list.OrderByDescending(p => p.SumTotal).ToList();
            entityList.count = entityList.list.Count();
            return entityList;
        }
        private void SearchOutEChartsVOListBySetGoodsInfo(ref IList<ReaBmsOutDtl> dtlList)
        {
            if (dtlList == null || dtlList.Count <= 0) return;

            IList<ReaGoods> allReaGoodsList = IDReaGoodsDao.LoadAll();//false
            for (int i = 0; i < dtlList.Count; i++)
            {
                if (!dtlList[i].GoodsID.HasValue) continue;

                long goodsID = dtlList[i].GoodsID.Value;
                var tempList = allReaGoodsList.Where(p => p.Id == goodsID);
                if (tempList != null && tempList.Count() > 0)
                {
                    dtlList[i].GoodsClass = tempList.ElementAt(0).GoodsClass;
                    dtlList[i].GoodsClassType = tempList.ElementAt(0).GoodsClassType;
                    dtlList[i].ProdOrgName = tempList.ElementAt(0).ProdOrgName;
                }
            }
        }
        private IList<EChartsVO> SearchOutEChartsVOListOfGroupByStorage(IList<ReaBmsOutDtl> dtlList, bool showZero)
        {
            IList<EChartsVO> voList = new List<EChartsVO>();
            var groupBy = dtlList.GroupBy(p => new
            {
                p.StorageID
            });
            var allSumTotal = dtlList.Sum(k => k.SumTotal);
            StringBuilder hqlStr = new StringBuilder();
            if (groupBy != null && groupBy.Count() > 0)
            {
                voList = groupBy.Select(g => new EChartsVO
                {
                    StorageID = g.Key.StorageID.ToString(),
                    StorageName = g.ElementAt(0).StorageName,
                    SumTotal = Math.Round(g.Sum(k => k.SumTotal), 2),
                    AllSumTotal = allSumTotal,
                    SumTotalPercent = (allSumTotal > 0 ? g.Sum(k => k.SumTotal) / allSumTotal * 100 : 0)
                }).ToList();
                foreach (var item in groupBy)
                {
                    hqlStr.Append(item.Key.StorageID);
                    hqlStr.Append(",");
                }
            }
            if (!showZero) return voList;

            //没有出库记录的库房处理
            string hql2 = "reastorage.Visible=1";
            if (hqlStr.Length > 0)
            {
                hql2 = hql2 + " and reastorage.Id not in(" + hqlStr.ToString().TrimEnd(',') + ")";
            }
            IList<ReaStorage> cenOrgList = IDReaStorageDao.GetListByHQL(hql2);
            foreach (ReaStorage entity in cenOrgList)
            {
                EChartsVO vo = new EChartsVO();
                vo.StorageID = entity.Id.ToString();
                vo.StorageName = entity.CName;
                vo.AllSumTotal = allSumTotal;
                vo.SumTotal = 0;
                vo.SumTotalPercent = 0;
                voList.Add(vo);
            }
            return voList;
        }
        private IList<EChartsVO> SearchOutEChartsVOListOfGroupByComp(IList<ReaBmsOutDtl> dtlList, bool showZero)
        {
            IList<EChartsVO> voList = new List<EChartsVO>();
            var groupBy = dtlList.GroupBy(p => new
            {
                p.ReaCompanyID
            });
            var allSumTotal = dtlList.Sum(k => k.SumTotal);
            StringBuilder hqlStr = new StringBuilder();
            if (groupBy != null && groupBy.Count() > 0)
            {
                voList = groupBy.Select(g => new EChartsVO
                {
                    ReaCompanyID = g.Key.ReaCompanyID.ToString(),
                    ReaCompCode = g.ElementAt(0).ReaCompCode,
                    ReaCompanyName = g.ElementAt(0).CompanyName,
                    SumTotal = Math.Round(g.Sum(k => k.SumTotal), 2),
                    AllSumTotal = allSumTotal,
                    SumTotalPercent = (allSumTotal > 0 ? g.Sum(k => k.SumTotal) / allSumTotal * 100 : 0)
                }).ToList();
                foreach (var item in groupBy)
                {
                    hqlStr.Append(item.Key.ReaCompanyID);
                    hqlStr.Append(",");
                }
            }
            if (!showZero) return voList;

            //没有出库记录的供货商处理
            string hql2 = "reacenorg.Visible=1 and reacenorg.OrgType=" + ReaCenOrgType.供货方.Key;
            if (hqlStr.Length > 0)
            {
                hql2 = hql2 + " and reacenorg.Id not in(" + hqlStr.ToString().TrimEnd(',') + ")";
            }
            IList<ReaCenOrg> cenOrgList = IDReaCenOrgDao.GetListByHQL(hql2);
            foreach (ReaCenOrg cenOrg in cenOrgList)
            {
                EChartsVO vo = new EChartsVO();
                vo.ReaCompanyID = cenOrg.Id.ToString();
                vo.ReaCompCode = cenOrg.OrgNo.ToString();
                vo.ReaCompanyName = cenOrg.CName;
                vo.AllSumTotal = allSumTotal;
                vo.SumTotal = 0;
                vo.SumTotalPercent = 0;
                voList.Add(vo);
            }
            return voList;
        }
        private IList<EChartsVO> SearchOutEChartsVOListOfGroupByGoodsClass(IList<ReaBmsOutDtl> dtlList, bool showZero)
        {
            IList<EChartsVO> voList = new List<EChartsVO>();
            var groupBy = dtlList.GroupBy(p => new
            {
                p.GoodsClass
            });
            var allSumTotal = dtlList.Sum(k => k.SumTotal);
            StringBuilder hqlStr = new StringBuilder();
            bool hasNull = false;
            if (groupBy != null && groupBy.Count() > 0)
            {
                voList = groupBy.Select(g => new EChartsVO
                {
                    GoodsClass = !string.IsNullOrEmpty(g.Key.GoodsClass) == true ? g.Key.GoodsClass : "未知",
                    SumTotal = Math.Round(g.Sum(k => k.SumTotal), 2),
                    AllSumTotal = allSumTotal,
                    SumTotalPercent = (allSumTotal > 0 ? g.Sum(k => k.SumTotal) / allSumTotal * 100 : 0)
                }).ToList();
                foreach (var item in groupBy)
                {
                    if (!string.IsNullOrEmpty(item.Key.GoodsClass))
                    {
                        hqlStr.Append("'" + item.Key.GoodsClass + "'");
                        hqlStr.Append(",");
                    }
                    else if (hasNull == false)
                    {
                        hasNull = true;
                    }
                }
            }
            if (!showZero) return voList;

            //没有出库记录的一级分类处理
            string hql2 = "reagoods.Visible=1";
            if (hqlStr.Length > 0)
            {
                hql2 = hql2 + " and reagoods.GoodsClass not in(" + hqlStr.ToString().TrimEnd(',') + ")";
            }
            IList<ReaGoodsClassVO> goodsclassList = IDReaGoodsDao.SearchGoodsClassListByClassTypeAndHQL("goodsclass", false, hql2, "", -1, -1);
            foreach (ReaGoodsClassVO entity in goodsclassList)
            {
                EChartsVO vo = new EChartsVO();
                vo.GoodsClass = entity.CName;
                vo.AllSumTotal = allSumTotal;
                vo.SumTotal = 0;
                vo.SumTotalPercent = 0;
                voList.Add(vo);
            }
            return voList;
        }
        private IList<EChartsVO> SearchOutEChartsVOListOfGroupByGoodsClassType(IList<ReaBmsOutDtl> dtlList, bool showZero)
        {
            IList<EChartsVO> voList = new List<EChartsVO>();
            var groupBy = dtlList.GroupBy(p => new
            {
                p.GoodsClassType
            });
            var allSumTotal = dtlList.Sum(k => k.SumTotal);
            StringBuilder hqlStr = new StringBuilder();
            bool hasNull = false;
            if (groupBy != null && groupBy.Count() > 0)
            {
                voList = groupBy.Select(g => new EChartsVO
                {
                    GoodsClassType = !string.IsNullOrEmpty(g.Key.GoodsClassType) == true ? g.Key.GoodsClassType : "未知",
                    SumTotal = Math.Round(g.Sum(k => k.SumTotal), 2),
                    AllSumTotal = allSumTotal,
                    SumTotalPercent = (allSumTotal > 0 ? g.Sum(k => k.SumTotal) / allSumTotal * 100 : 0)
                }).ToList();
                foreach (var item in groupBy)
                {
                    if (!string.IsNullOrEmpty(item.Key.GoodsClassType))
                    {
                        hqlStr.Append("'" + item.Key.GoodsClassType + "'");
                        hqlStr.Append(",");
                    }
                    else if (hasNull == false)
                    {
                        hasNull = true;
                    }
                }
            }
            if (!showZero) return voList;

            //没有出库记录的二级分类处理
            string hql2 = "reagoods.Visible=1";
            if (hqlStr.Length > 0)
            {
                hql2 = hql2 + " and reagoods.GoodsClassType not in(" + hqlStr.ToString().TrimEnd(',') + ")";
            }
            IList<ReaGoodsClassVO> coodsClassTypeList = IDReaGoodsDao.SearchGoodsClassListByClassTypeAndHQL("goodsclasstype", false, hql2, "", -1, -1);
            foreach (ReaGoodsClassVO entity in coodsClassTypeList)
            {
                EChartsVO vo = new EChartsVO();
                vo.GoodsClassType = entity.CName;
                vo.AllSumTotal = allSumTotal;
                vo.SumTotal = 0;
                vo.SumTotalPercent = 0;
                voList.Add(vo);
            }
            return voList;
        }
        private IList<EChartsVO> SearchOutEChartsVOListOfGroupByProdOrg(IList<ReaBmsOutDtl> dtlList, bool showZero)
        {
            IList<EChartsVO> voList = new List<EChartsVO>();
            var groupBy = dtlList.GroupBy(p => new
            {
                p.ProdOrgName
            });
            var allSumTotal = dtlList.Sum(k => k.SumTotal);
            StringBuilder hqlStr = new StringBuilder();
            bool hasNull = false;
            if (groupBy != null && groupBy.Count() > 0)
            {
                voList = groupBy.Select(g => new EChartsVO
                {
                    ProdOrgName = !string.IsNullOrEmpty(g.Key.ProdOrgName) == true ? g.Key.ProdOrgName : "未知",
                    SumTotal = Math.Round(g.Sum(k => k.SumTotal), 2),
                    AllSumTotal = allSumTotal,
                    SumTotalPercent = (allSumTotal > 0 ? g.Sum(k => k.SumTotal) / allSumTotal * 100 : 0)
                }).ToList();
                foreach (var item in groupBy)
                {
                    if (!string.IsNullOrEmpty(item.Key.ProdOrgName))
                    {
                        hqlStr.Append("'" + item.Key.ProdOrgName + "'");
                        hqlStr.Append(",");
                    }
                    else if (hasNull == false)
                    {
                        hasNull = true;
                    }
                }
            }
            if (!showZero) return voList;

            //没有出库记录的库房处理
            string hql2 = "bdict.IsUse=1 and bdict.BDictType.DictTypeCode='ProdOrg'";
            if (hqlStr.Length > 0)
            {
                hql2 = hql2 + " and bdict.CName not in(" + hqlStr.ToString().TrimEnd(',') + ")";
            }
            IList<BDict> entityList = IBBDict.SearchListByHQL(hql2);
            foreach (BDict entity in entityList)
            {
                EChartsVO vo = new EChartsVO();
                vo.ProdOrgName = entity.CName;
                vo.AllSumTotal = allSumTotal;
                vo.SumTotal = 0;
                vo.SumTotalPercent = 0;
                voList.Add(vo);
            }
            return voList;
        }
        private IList<EChartsVO> SearchOutEChartsVOListOfGroupByEquip(IList<ReaBmsOutDtl> dtlList, bool showZero)
        {
            IList<EChartsVO> voList = new List<EChartsVO>();
            var groupBy = dtlList.GroupBy(p => new
            {
                p.TestEquipID
            });
            var allSumTotal = dtlList.Sum(k => k.SumTotal);
            StringBuilder hqlStr = new StringBuilder();
            bool hasNull = false;
            if (groupBy != null && groupBy.Count() > 0)
            {
                voList = groupBy.Select(g => new EChartsVO
                {
                    TestEquipID = g.Key.TestEquipID.ToString(),
                    TestEquipName = !string.IsNullOrEmpty(g.ElementAt(0).TestEquipName) == true ? g.ElementAt(0).TestEquipName : "未知",
                    SumTotal = Math.Round(g.Sum(k => k.SumTotal), 2),
                    AllSumTotal = allSumTotal,
                    SumTotalPercent = (allSumTotal > 0 ? g.Sum(k => k.SumTotal) / allSumTotal * 100 : 0)
                }).ToList();
                foreach (var item in groupBy)
                {
                    if (item.Key.TestEquipID.HasValue)
                    {
                        hqlStr.Append("" + item.Key.TestEquipID + "");
                        hqlStr.Append(",");
                    }
                    else if (hasNull == false)
                    {
                        hasNull = true;
                    }
                }
            }
            if (!showZero) return voList;

            //没有出库记录的仪器处理
            string hql2 = "reatestequiplab.Visible=1";
            if (hqlStr.Length > 0)
            {
                hql2 = hql2 + " and reatestequiplab.Id not in(" + hqlStr.ToString().TrimEnd(',') + ")";
            }
            IList<ReaTestEquipLab> entityList = IDReaTestEquipLabDao.GetListByHQL(hql2);
            foreach (ReaTestEquipLab entity in entityList)
            {
                EChartsVO vo = new EChartsVO();
                vo.TestEquipID = entity.Id.ToString();
                vo.TestEquipName = entity.CName;
                vo.AllSumTotal = allSumTotal;
                vo.SumTotal = 0;
                vo.SumTotalPercent = 0;
                voList.Add(vo);
            }
            return voList;
        }
        private IList<EChartsVO> SearchOutEChartsVOListOfGroupByDept(IList<ReaBmsOutDtl> dtlList, bool showZero)
        {
            IList<EChartsVO> voList = new List<EChartsVO>();
            var groupBy = dtlList.GroupBy(p => new
            {
                p.DeptID
            });
            var allSumTotal = dtlList.Sum(k => k.SumTotal);
            StringBuilder hqlStr = new StringBuilder();
            bool hasNull = false;
            if (groupBy != null && groupBy.Count() > 0)
            {
                voList = groupBy.Select(g => new EChartsVO
                {
                    DeptID = g.Key.DeptID.ToString(),
                    DeptName = !string.IsNullOrEmpty(g.ElementAt(0).DeptName) == true ? g.ElementAt(0).DeptName : "未知",
                    SumTotal = Math.Round(g.Sum(k => k.SumTotal), 2),
                    AllSumTotal = allSumTotal,
                    SumTotalPercent = (allSumTotal > 0 ? g.Sum(k => k.SumTotal) / allSumTotal * 100 : 0)
                }).ToList();
                foreach (var item in groupBy)
                {
                    if (item.Key.DeptID > 0)
                    {
                        hqlStr.Append("" + item.Key.DeptID + "");
                        hqlStr.Append(",");
                    }
                    else if (hasNull == false)
                    {
                        hasNull = true;
                    }
                }
            }
            if (!showZero) return voList;

            //没有出库记录的部门处理
            string hql2 = "hrdept.IsUse=1";
            if (hqlStr.Length > 0)
            {
                hql2 = hql2 + " and hrdept.Id not in(" + hqlStr.ToString().TrimEnd(',') + ")";
            }
            IList<HRDept> entityList = IDHRDeptDao.GetListByHQL(hql2);
            foreach (HRDept entity in entityList)
            {
                EChartsVO vo = new EChartsVO();
                vo.DeptID = entity.Id.ToString();
                vo.DeptName = entity.CName;
                vo.AllSumTotal = allSumTotal;
                vo.SumTotal = 0;
                vo.SumTotalPercent = 0;
                voList.Add(vo);
            }
            return voList;
        }
        public BaseResultDataValue SearchStackOutEChartsVOListByHql(int statisticType, string startDate, string endDate, string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            IList<ReaBmsOutDtl> dtlList = ((IDReaBmsOutDtlDao)base.DBDao).SearchReaBmsOutDtlListByJoinHql(docHql, dtlHql, deptGoodsHql, reaGoodsHql, -1, -1, "");
            //SearchOutEChartsVOListBySetGoodsInfo(ref dtlList);
            string goodsClassHql = "reagoods.Visible=1";
            IList<ReaGoodsClassVO> goodsclassList = IDReaGoodsDao.SearchGoodsClassListByClassTypeAndHQL("goodsclass", true, goodsClassHql, "", -1, -1);
            IList<ReaGoodsClassVO> goodsClassTypeList = IDReaGoodsDao.SearchGoodsClassListByClassTypeAndHQL("goodsclasstype", true, goodsClassHql, "", -1, -1);
            var groupByGoodsClass = goodsclassList.GroupBy(p => new
            {
                p.CName
            });
            var groupByGoodsClassType = goodsClassTypeList.GroupBy(p => new
            {
                p.CName
            });
            JObject jresult = new JObject();
            JObject jAxis = new JObject();
            JArray axisData = new JArray();
            JObject jLegend = new JObject();
            JArray legendData = new JArray();
            JArray seriesData = new JArray();
            var allSumTotal = dtlList.Sum(k => k.SumTotal);
            jresult.Add("allSumTotal", Math.Round(allSumTotal, 2));

            bool isHasNull = false;
            foreach (var vo in groupByGoodsClass)
            {
                if (!string.IsNullOrEmpty(vo.Key.CName))
                {
                    axisData.Add(vo.Key.CName);
                }
                else if (isHasNull == false)
                {
                    isHasNull = true;
                    axisData.Add("未知");
                }
            }
            jAxis.Add("data", axisData);
            jresult.Add("axis", jAxis);

            isHasNull = false;
            foreach (var vo in groupByGoodsClassType)
            {
                if (!string.IsNullOrEmpty(vo.Key.CName))
                {
                    legendData.Add(vo.Key.CName);
                }
                else if (isHasNull == false)
                {
                    isHasNull = true;
                    legendData.Add("未知");
                }
            }
            jLegend.Add("data", legendData);
            jresult.Add("legend", jLegend);

            switch (statisticType)
            {
                case 21:
                    //一级分类金额及金额占比处理
                    JArray goodsClassList = new JArray();
                    var groupBy = dtlList.GroupBy(p => new
                    {
                        p.GoodsClass
                    });
                    if (groupBy != null && groupBy.Count() > 0)
                    {
                        foreach (var g in groupBy)
                        {
                            JObject jseries = new JObject();
                            jseries.Add("GoodsClass", !string.IsNullOrEmpty(g.Key.GoodsClass) == true ? g.Key.GoodsClass : "未知");
                            jseries.Add("SumTotal", Math.Round(g.Sum(k => k.SumTotal), 2));
                            jseries.Add("SumTotalPercent", (allSumTotal > 0 ? g.Sum(k => k.SumTotal) / allSumTotal * 100 : 0));
                            goodsClassList.Add(jseries);
                        }
                        jresult.Add("goodsClassList", goodsClassList);
                    }
                    //按货品一级分类(堆叠为二级分类)
                    foreach (var voGoodsClassType in groupByGoodsClassType)
                    {
                        JObject jseries = new JObject();
                        jseries.Add("name", !string.IsNullOrEmpty(voGoodsClassType.Key.CName) == true ? voGoodsClassType.Key.CName : "未知");
                        jseries.Add("type", "bar");
                        jseries.Add("stack", "总量");
                        JArray seriesData2 = new JArray();
                        foreach (var voGoodsclass in groupByGoodsClass)
                        {
                            double? sumTotal = 0;
                            var tempList = dtlList.Where(p => p.GoodsClass == voGoodsclass.Key.CName && p.GoodsClassType == voGoodsClassType.Key.CName);
                            if (tempList != null) sumTotal = tempList.Sum(k => k.SumTotal);
                            seriesData2.Add(Math.Round(sumTotal.Value, 2));
                        }
                        jseries.Add("data", seriesData2);
                        seriesData.Add(jseries);
                    }
                    jresult.Add("series", seriesData);
                    break;
                default:

                    break;
            }
            //ZhiFang.Common.Log.Log.Debug("jresult:" + jresult.ToString());
            baseResultDataValue.ResultDataValue = jresult.ToString();
            return baseResultDataValue;
        }

        #endregion

        /// <summary>
        /// 智方试剂平台查询使用，根据出库主单查询明细。
        /// hasLabId传false，不增加LabID的默认条件
        /// </summary>
        public EntityList<ReaBmsOutDtl> GetListByHQL(string strHqlWhere, string Order, int start, int count, bool hasLabId)
        {
            return ((IDReaBmsOutDtlDao)base.DBDao).GetListByHQL(strHqlWhere, Order, start, count, hasLabId);
        }
    }
}