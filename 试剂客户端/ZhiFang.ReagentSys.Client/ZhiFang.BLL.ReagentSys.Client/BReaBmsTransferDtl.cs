using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using ZhiFang.ReagentSys.Client.Common;
using System.Data;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Statistics;
using System;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsTransferDtl : BaseBLL<ReaBmsTransferDtl>, ZhiFang.IBLL.ReagentSys.Client.IBReaBmsTransferDtl
    {
        public override bool Add()
        {
            this.Entity.SumTotal = this.Entity.Price * this.Entity.GoodsQty;
            return base.Add();
        }
        public BaseResultDataValue EditValidTransferDtlList(IList<ReaBmsTransferDtl> dtlList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (dtlList == null || dtlList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "保存移库明细验证失败!";
                return brdv;
            }
            foreach (var transferDtl in dtlList)
            {
                if (brdv.success && !transferDtl.ReqGoodsQty.HasValue || transferDtl.ReqGoodsQty.Value <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "移库货品名称为:" + transferDtl.GoodsCName + ",申请数小于等于0!";
                    break;
                }
                if (brdv.success && transferDtl.SStorageID.HasValue && transferDtl.DStorageID.HasValue && transferDtl.SStorageID.Value == transferDtl.DStorageID.Value)
                {
                    if (brdv.success && transferDtl.SPlaceID.HasValue && transferDtl.DPlaceID.HasValue && transferDtl.SPlaceID.Value == transferDtl.DPlaceID.Value)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "移库货品名称为:" + transferDtl.GoodsCName + ",目标库房及目标货架与源库房货架相同!";
                        break;
                    }
                }
            }
            return brdv;
        }
        public BaseResultDataValue AddTransferDtlList(ReaBmsTransferDoc transferDoc, IList<ReaBmsTransferDtl> dtAddList, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (dtAddList == null || dtAddList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "不能新增移库保存,传入参数dtAddList为空!";
                return brdv;
            }
            foreach (var transferDtl in dtAddList)
            {
                if (brdv.success == false)
                {
                    brdv.ErrorInfo = "新增保存移库明细失败!";
                    break;
                }
                transferDtl.Visible = true;
                transferDtl.DataAddTime = DateTime.Now;
                transferDtl.DataUpdateTime = DateTime.Now;
                if (transferDtl.GoodsQty <= 0 && transferDtl.ReqGoodsQty.HasValue)
                    transferDtl.GoodsQty = transferDtl.ReqGoodsQty.Value;
                transferDtl.TransferDocID = transferDoc.Id;
                this.Entity = transferDtl;
                brdv.success = this.Add();
            }
            return brdv;
        }
        public BaseResultBool UpdateTransferDtlList(ReaBmsTransferDoc transferDoc, IList<ReaBmsTransferDtl> dtEditList)
        {
            BaseResultBool brdv = new BaseResultBool();
            if (dtEditList == null || dtEditList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "不能编辑保存移库明细,传入参数dtEditList为空!";
                return brdv;
            }
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            foreach (var transferDtl in dtEditList)
            {
                if (brdv.success == false)
                {
                    brdv.ErrorInfo = "编辑保存移库明细失败!";
                    break;
                }
                transferDtl.TransferDocID = transferDoc.Id;
                transferDtl.DataUpdateTime = DateTime.Now;
                if (transferDtl.GoodsQty <= 0 && transferDtl.ReqGoodsQty.HasValue)
                    transferDtl.GoodsQty = transferDtl.ReqGoodsQty.Value;
                transferDtl.DataTimeStamp = this.Get(transferDtl.Id).DataTimeStamp;
                this.Entity = transferDtl;
                brdv.success = this.Edit();
                //brdv.success = ((IDReaBmsTransferDtlDao)base.DBDao).Update(transferDtl);
            }
            return brdv;
        }
        #region 移库汇总统计
        public IList<ReaBmsTransferDtl> SearchReaBmsTransferDtlSummaryListByHQL(string docHql, string dtlHql, string reaGoodsHql, string order, int page, int limit)
        {
            IList<ReaBmsTransferDtl> dtlList = new List<ReaBmsTransferDtl>();
            if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
                return dtlList;

            dtlList = ((IDReaBmsTransferDtlDao)base.DBDao).SearchReaBmsTransferDtlSummaryByHQL(docHql, dtlHql, reaGoodsHql, order, -1, -1);
            return dtlList;
        }
        public EntityList<ReaBmsTransferDtl> SearchReaBmsTransferDtlSummaryByHQL(int groupType, string docHql, string dtlHql, string reaGoodsHql, string order, int page, int limit)
        {
            EntityList<ReaBmsTransferDtl> entityList = new EntityList<ReaBmsTransferDtl>();
            if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
                return entityList;

            IList<ReaBmsTransferDtl> dtlList = new List<ReaBmsTransferDtl>();
            dtlList = ((IDReaBmsTransferDtlDao)base.DBDao).SearchReaBmsTransferDtlSummaryByHQL(docHql, dtlHql, reaGoodsHql, order, -1, -1);
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
        /// <summary>
        /// 合并条件:领用部门+供应商+货品编码+单位+规格+批号+效期+领用人ID+领用日期
        /// </summary>
        /// <param name="dtlList"></param>
        /// <returns></returns>
        private IList<ReaBmsTransferDtl> SearchReaBmsOutDtlListOfGroupBy1(IList<ReaBmsTransferDtl> dtlList)
        {
            return dtlList.GroupBy(p => new
            {
                p.DeptID,
                p.ReaCompanyID,
                p.ReaGoodsNo,
                p.GoodsUnit,
                p.UnitMemo,
                p.LotNo,
                p.InvalidDate,
                p.TakerID,
                p.DataAddTime
            }).Select(g => new ReaBmsTransferDtl
            {
                DeptID = g.ElementAt(0).DeptID,
                DeptName = g.ElementAt(0).DeptName,
                TakerID = g.ElementAt(0).TakerID,
                TakerName = g.ElementAt(0).TakerName,
                ReaCompanyID = g.Key.ReaCompanyID,
                ReaCompanyName = g.ElementAt(0).ReaCompanyName,
                ReaGoodsNo = g.Key.ReaGoodsNo,
                ProdGoodsNo = g.ElementAt(0).ProdGoodsNo,
                CenOrgGoodsNo = g.ElementAt(0).CenOrgGoodsNo,
                GoodsID = g.ElementAt(0).GoodsID,
                GoodsCName = g.ElementAt(0).GoodsCName,

                BarCodeType = g.ElementAt(0).BarCodeType,
                GoodsUnit = g.ElementAt(0).GoodsUnit,
                UnitMemo = g.ElementAt(0).UnitMemo,
                LotNo = g.Key.LotNo,
                InvalidDate = g.ElementAt(0).InvalidDate,
                CreaterID = g.ElementAt(0).CreaterID,

                CreaterName = g.ElementAt(0).CreaterName,
                DataAddTime = g.ElementAt(0).DataAddTime,
                ReqGoodsQty = g.Sum(k => k.ReqGoodsQty),
                GoodsQty = g.Sum(k => k.GoodsQty),
                SumTotal = g.Sum(k => k.SumTotal),
                Price = g.Sum(k => k.GoodsQty) > 0 ? g.Sum(k => k.SumTotal) / g.Sum(k => k.GoodsQty) : 0,//g.ElementAt(0).Price,

                ProdDate = g.ElementAt(0).ProdDate,
                TaxRate = g.ElementAt(0).TaxRate,
                Memo = g.ElementAt(0).Memo
            }).ToList();
        }
        public Stream SearchBusinessSummaryReportOfExcelByHql(long labID, string labCName, int groupType, string docHql, string dtlHql, string reaGoodsHql, string sort, string breportType, string frx, ref string fileName, string startDate, string endDate)
        {
            if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取统计条件为空!");
            }
            Stream stream = null;
            IList<ReaBmsTransferDtl> dtlList = new List<ReaBmsTransferDtl>();
            dtlList = ((IDReaBmsTransferDtlDao)base.DBDao).SearchReaBmsTransferDtlSummaryByHQL(docHql, dtlHql, reaGoodsHql, sort, -1, -1);

            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取移库汇总明细信息为空!");
            }
            dtlList = SearchReaBmsOutDtlListOfGroupBy1(dtlList);
            if (string.IsNullOrEmpty(frx))
                frx = "移库汇总.xlsx";
            string saveFullPath = "";
            string fileExt = frx.Substring(frx.LastIndexOf("."));
            string excelFile = frx;
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand(startDate, endDate);
            stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsTransferDoc, ReaBmsTransferDtl>(null, dtlList, excelCommand, breportType, labID, frx, excelFile, ref saveFullPath);
            fileName = "移库汇总信息" + fileExt;
            return stream;
        }
        public Stream SearchBusinessSummaryReportOfPdfByHql(string reaReportClass, long labId, string labCName, int groupType, string docHql, string dtlHql, string reaGoodsHql, string sort, string breportType, string frx, ref string pdfFileName, string startDate, string endDate)
        {
            Stream stream = null;
            if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取统计条件为空!");
            }
            IList<ReaBmsTransferDtl> dtlList = ((IDReaBmsTransferDtlDao)base.DBDao).SearchReaBmsTransferDtlSummaryByHQL(docHql, dtlHql, reaGoodsHql, sort, -1, -1);

            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取移库汇总明细信息为空!");
            }
            dtlList = SearchReaBmsOutDtlListOfGroupBy1(dtlList);
            //pdfFileName = "移库汇总.pdf";
            pdfFileName = "移库汇总_" + DateTime.Now.ToString("yyMMddHHmmss") + ".pdf";
            if (reaReportClass == ReaReportClass.Frx.Key)
            {
                stream = SearchPdfReportOfFrxByHql(labId, labCName, pdfFileName, dtlList, frx, startDate, endDate);
            }
            else if (reaReportClass == ReaReportClass.Excel.Key)
            {
                string excelFileFullDir = "";
                //获取移库汇总模板
                if (string.IsNullOrEmpty(frx))
                    frx = "移库汇总.xlsx";
                string fileExt = frx.Substring(frx.LastIndexOf("."));
                string excelFile = fileExt;
                Stream stream2 = null;
                ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand(startDate, endDate);
                stream2 = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsTransferDtl, ReaBmsTransferDtl>(null, dtlList, excelCommand, breportType, labId, frx, excelFile, ref excelFileFullDir);
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
        private Stream SearchPdfReportOfFrxByHql(long labId, string labCName, string pdfFileName, IList<ReaBmsTransferDtl> dtlList, string frx, string startDate, string endDate)
        {
            Stream stream = null;
            DataSet dataSet = new DataSet();
            dataSet.DataSetName = "ZhiFang.ReagentSys.Client";
            DataTable dtDtl = ReportBTemplateHelp.ToDataTable<ReaBmsTransferDtl>(dtlList, null);
            if (dtDtl != null)
            {
                dtDtl.TableName = "Rea_BmsTransferDtl";
                dataSet.Tables.Add(dtDtl);
            }
            if (string.IsNullOrEmpty(frx))
                frx = "移库汇总.frx";
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand(startDate, endDate);
            ExcelReportHelp.CreateEECDataTable(excelCommand, ref dataSet);
            stream = FrxToPdfReportHelp.SavePdfReport(dataSet, labId, pdfFileName, FrxToPdfReportHelp.PublicTemplateDir, BTemplateType.GetStatusDic()[BTemplateType.移库汇总.Key].Name, frx, false);
            return stream;
        }

        public EntityList<ReaTransferAndOutDtlVO> SearchReaTransferAndOutDtlVOListByHQL(int groupType, string hqlStr, string order, int page, int limit)
        {
            EntityList<ReaTransferAndOutDtlVO> entityVOList = new EntityList<ReaTransferAndOutDtlVO>();

            if (string.IsNullOrWhiteSpace(hqlStr) && string.IsNullOrWhiteSpace(hqlStr))
                return entityVOList;
            string docHql = "", dtlHql = "";
            IList<ReaBmsTransferDtl> dtlList = ((IDReaBmsTransferDtlDao)base.DBDao).SearchReaBmsTransferDtlSummaryByHQL(docHql, dtlHql, "", order, -1, -1);
            return entityVOList;
        }
        public Stream SearchReaTransferAndOutDtlVOOfExcelByHql(long labID, string labCName, int groupType, string hqlStr, string sort, string breportType, string frx, ref string fileName)
        {
            if (string.IsNullOrWhiteSpace(hqlStr))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取统计条件为空!");
            }
            Stream stream = null;
            IList<ReaTransferAndOutDtlVO> dtlVOList = new List<ReaTransferAndOutDtlVO>();
            IList<ReaBmsTransferDtl> dtlList = new List<ReaBmsTransferDtl>();
            string docHql = "", dtlHql = "";
            dtlList = ((IDReaBmsTransferDtlDao)base.DBDao).SearchReaBmsTransferDtlSummaryByHQL(docHql, dtlHql, "", sort, -1, -1);

            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取移库明细信息为空!");
            }
            dtlList = SearchReaBmsOutDtlListOfGroupBy1(dtlList);
            if (string.IsNullOrEmpty(frx))
                frx = "移库及使用.xlsx";
            string saveFullPath = "";
            string fileExt = frx.Substring(frx.LastIndexOf("."));
            string excelFile = frx;
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaTransferAndOutDtlVO, ReaTransferAndOutDtlVO>(null, dtlVOList, excelCommand, breportType, labID, frx, excelFile, ref saveFullPath);
            fileName = "移库及使用信息" + fileExt;
            return stream;
        }
        public Stream SearchReaTransferAndOutDtlVOOfPdfByHql(string reaReportClass, long labId, string labCName, int groupType, string hqlStr, string sort, string breportType, string frx, ref string pdfFileName)
        {
            Stream stream = null;
            if (string.IsNullOrWhiteSpace(hqlStr))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取统计条件为空!");
            }

            IList<ReaTransferAndOutDtlVO> dtlVOList = new List<ReaTransferAndOutDtlVO>();
            string docHql = "", dtlHql = "";
            IList<ReaBmsTransferDtl> dtlList = ((IDReaBmsTransferDtlDao)base.DBDao).SearchReaBmsTransferDtlSummaryByHQL(docHql, dtlHql, "", sort, -1, -1);

            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取移库明细信息为空!");
            }
            dtlList = SearchReaBmsOutDtlListOfGroupBy1(dtlList);
            pdfFileName = "移库及使用.pdf";
            if (reaReportClass == ReaReportClass.Frx.Key)
            {
                stream = SearchPdfReportOfFrxByHql(labId, labCName, pdfFileName, dtlList, frx, "", "");
            }
            else if (reaReportClass == ReaReportClass.Excel.Key)
            {
                string excelFileFullDir = "";
                //获取移库及使用模板
                if (string.IsNullOrEmpty(frx))
                    frx = "移库及使用.xlsx";
                string fileExt = frx.Substring(frx.LastIndexOf("."));
                string excelFile = fileExt;
                Stream stream2 = null;
                ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
                stream2 = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaTransferAndOutDtlVO, ReaTransferAndOutDtlVO>(null, dtlVOList, excelCommand, breportType, labId, frx, excelFile, ref excelFileFullDir);
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
        #endregion

    }
}