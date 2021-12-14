
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
using Newtonsoft.Json;
using System.Reflection;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsQtyMonthBalanceDoc : BaseBLL<ReaBmsQtyMonthBalanceDoc>, ZhiFang.IBLL.ReagentSys.Client.IBReaBmsQtyMonthBalanceDoc
    {
        IDReaBmsQtyDtlOperationDao IDReaBmsQtyDtlOperationDao { get; set; }
        IDReaBmsQtyBalanceDtlDao IDReaBmsQtyBalanceDtlDao { get; set; }
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IDReaBmsInDtlDao IDReaBmsInDtlDao { get; set; }
        IDReaBmsOutDocDao IDReaBmsOutDocDao { get; set; }
        IDReaBmsOutDtlDao IDReaBmsOutDtlDao { get; set; }
        IBBReport IBBReport { get; set; }

        #region 库存结转报表
        public BaseResultDataValue SaveOfQtyBalanceDtlList(ReaBmsQtyMonthBalanceDoc entity, string labCName, long empID, string empName)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            IList<ReaBmsQtyBalanceDtl> qtyBalanceDtlList = new List<ReaBmsQtyBalanceDtl>();
            tempBaseResultDataValue = SaveBeforeVerify(entity, ref qtyBalanceDtlList);
            if (tempBaseResultDataValue.success == false) return tempBaseResultDataValue;

            //获取库存结转报表所属的库存结转信息
            string errorInfo = "";
            qtyBalanceDtlList = GetQtyBalanceDtlList(entity, ref errorInfo);
            if (qtyBalanceDtlList == null || qtyBalanceDtlList.Count <= 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = errorInfo + ",库存结转数据为空!";
                return tempBaseResultDataValue;
            }
            entity.QtyMonthBalanceDocNo = this.GetQtyMonthBalanceDocNo();
            entity.OperID = empID;
            entity.OperName = empName;
            entity.OperDate = DateTime.Now;
            entity.Visible = true;
            entity.CreaterID = empID;
            entity.CreaterName = empName;
            entity.DataUpdateTime = DateTime.Now;
            if (string.IsNullOrEmpty(entity.TypeName))
                entity.TypeName = ReaBmsQtyMonthBalanceDocType.GetStatusDic()[entity.TypeID.ToString()].Name;
            if (string.IsNullOrEmpty(entity.StatisticalTypeName))
                entity.StatisticalTypeName = ReaBmsQtyMonthBalanceDocStatisticalType.GetStatusDic()[entity.StatisticalTypeID.ToString()].Name;
            this.Entity = entity;
            if (this.Add())
            {
                //按合并方式生成库存结转报表明细数据
                IList<ReaBmsQtyMonthBalanceDtl> qtyMonthBalanceDtlList = new List<ReaBmsQtyMonthBalanceDtl>();
                tempBaseResultDataValue = GetQtyMonthBalanceDtlListOfQtyBalanceDtlList(entity, qtyBalanceDtlList, empID, empName, ref qtyMonthBalanceDtlList);
                if (tempBaseResultDataValue.success == false) return tempBaseResultDataValue;

                //生成PDF
                entity.LabCName = labCName;
                Stream stream = CreateQtyMonthBalanceOfPdf(entity, qtyMonthBalanceDtlList, "");
                if (stream == null)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "生成库存结转报表PDF文件失败!";
                }
                else
                {
                    tempBaseResultDataValue = SaveBReport(entity);
                }
            }
            else
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo += ",新增失败!";
            }
            return tempBaseResultDataValue;
        }
        private BaseResultDataValue SaveBReport(ReaBmsQtyMonthBalanceDoc entity)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            string reportSubDir = BTemplateType.GetStatusDic()[BTemplateType.库存结转报表.Key].Name;
            string pdfFilePath = PdfReportHelp.GetSavePDFSubDir(entity.LabID, reportSubDir);
            pdfFilePath = pdfFilePath + "\\" + entity.Id + ".pdf" + "." + FileExt.zf;

            BReport report = new BReport();
            report.IsUse = true;
            report.Status = long.Parse(BReportStatus.报表生成.Key);
            report.CreatorID = entity.CreaterID;
            report.CreatorName = entity.CreaterName;
            report.BobjectID = entity.Id;

            report.StatisticsBeginDateTime = entity.StartDate;
            report.StatisticsEndDateTime = entity.EndDate;
            report.BusinessModuleCode = "ReaBmsQtyMonthBalanceDoc";
            report.TypeID = long.Parse(BReportType.库存结转报表.Key);
            if (string.IsNullOrEmpty(report.TypeName))
                report.TypeName = BReportType.GetStatusDic()[report.TypeID.ToString()].Name;
            string pdfName = entity.Id.ToString() + ".pdf" + "." + FileExt.zf;
            Stream stream = PdfReportHelp.GetReportPDF(entity.LabID, pdfName, reportSubDir);
            report.FilePath = pdfFilePath;
            report.FileExt = ".pdf";
            report.ContentType = "application/pdf";
            report.FileSize = stream.Length;
            IBBReport.Entity = report;
            tempBaseResultDataValue.success = IBBReport.Add();
            stream.Close();

            if (tempBaseResultDataValue.success == false)
            {
                tempBaseResultDataValue.ErrorInfo = "保存报表管理信息失败!";
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 获取库存结转信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        private IList<ReaBmsQtyBalanceDtl> GetQtyBalanceDtlList(ReaBmsQtyMonthBalanceDoc entity, ref string errorInfo)
        {
            IList<ReaBmsQtyBalanceDtl> qtyBalanceDtlList = new List<ReaBmsQtyBalanceDtl>();
            DateTime endDate = entity.EndDate.Value;
            if (string.IsNullOrEmpty(entity.Round))
                entity.Round = endDate.ToString("yyyy-MM");
            //库存结转明细信息
            string qtyBalanceHql = "reabmsqtybalancedtl.GoodsQty>=0 ";
            if (entity.QtyBalanceDocID.HasValue)
            {
                qtyBalanceHql = qtyBalanceHql + "and reabmsqtybalancedtl.QtyBalanceDocID=" + entity.QtyBalanceDocID.Value;
            }
            errorInfo = "";
            if (entity.TypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocType.按全部.Key))
            {
                errorInfo = "库存结转报表周期为" + entity.Round + ",库存结转报表类型为【按全部】!";
            }
            else if (entity.TypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocType.按库房.Key))
            {
                qtyBalanceHql = qtyBalanceHql + " and reabmsqtybalancedtl.StorageID=" + entity.StorageID.Value;
                errorInfo = "库存结转报表周期为" + entity.Round + ",库存结转报表类型为【按库房】,选择的库房为【" + entity.StorageName + "】";
            }
            else if (entity.TypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocType.按库房货架.Key))
            {
                qtyBalanceHql = qtyBalanceHql + " and reabmsqtybalancedtl.StorageID=" + entity.StorageID.Value;
                if (entity.PlaceID.HasValue)
                    qtyBalanceHql = qtyBalanceHql + " and reabmsqtybalancedtl.PlaceID=" + entity.PlaceID;
                errorInfo = "库存结转报表周期为" + entity.Round + ",库存结转报表类型为【按库房货架】,选择的库房为【" + entity.StorageName + "】,选择的货架为【" + entity.PlaceName + "】";
            }

            string reaGoodHql = "";
            if (!string.IsNullOrEmpty(entity.GoodsClass))
            {
                reaGoodHql = reaGoodHql + " reagoods.GoodsClass='" + entity.GoodsClass + "' and ";
            }
            if (!string.IsNullOrEmpty(entity.GoodsClassType))
            {
                reaGoodHql = reaGoodHql + " reagoods.GoodsClassType='" + entity.GoodsClassType + "' and ";
            }

            if (string.IsNullOrEmpty(reaGoodHql))
            {
                qtyBalanceDtlList = IDReaBmsQtyBalanceDtlDao.GetListByHQL(qtyBalanceHql);
            }
            else
            {
                char[] trimChars = new char[] { ' ', 'a', 'n', 'd' };
                reaGoodHql = reaGoodHql.TrimEnd(trimChars);
                qtyBalanceDtlList = IDReaBmsQtyBalanceDtlDao.SearchListByReaGoodHQL(qtyBalanceHql, reaGoodHql, "", -1, -1);
            }
            return qtyBalanceDtlList;
        }
        /// <summary>
        /// 按库存结转明细记录生成库存结转报表明细数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="qtyBalanceDtlList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <param name="qtyMonthBalanceDtlList"></param>
        /// <returns></returns>
        private BaseResultDataValue GetQtyMonthBalanceDtlListOfQtyBalanceDtlList(ReaBmsQtyMonthBalanceDoc entity, IList<ReaBmsQtyBalanceDtl> qtyBalanceDtlList, long empID, string empName, ref IList<ReaBmsQtyMonthBalanceDtl> qtyMonthBalanceDtlList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            string errorInfo = "";
            //获取库存结转报表条件的库存结转信息
            if (qtyBalanceDtlList == null)
                qtyBalanceDtlList = GetQtyBalanceDtlList(entity, ref errorInfo);
            if (qtyBalanceDtlList == null || qtyBalanceDtlList.Count <= 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = errorInfo + ",库存结转数据为空!";
                return tempBaseResultDataValue;
            }

            //获取当前统计条件的库存变化记录集合
            IList<ReaBmsQtyDtlOperation> curQtyDtlOperList = new List<ReaBmsQtyDtlOperation>();
            curQtyDtlOperList = GetCurQtyDtlOperationList(entity, ref errorInfo);
            //if (curQtyDtlOperList == null || curQtyDtlOperList.Count <= 0)
            //{
            //    tempBaseResultDataValue.success = false;
            //    tempBaseResultDataValue.ErrorInfo = errorInfo + "获取库存变化操作记录为空!";
            //    return tempBaseResultDataValue;
            //}
            if (entity.StatisticalTypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocStatisticalType.按货品批号供应商.Key))
            {
                tempBaseResultDataValue = GetQtyMonthBalanceDtlOfGroupByGoodsIDAndReaComAndLotNo(entity, qtyBalanceDtlList, curQtyDtlOperList, empID, empName, ref qtyMonthBalanceDtlList);
            }
            else if (entity.StatisticalTypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocStatisticalType.按货品批号供应商库房.Key))
            {
                tempBaseResultDataValue = GetQtyMonthBalanceDtlListOfGroupByGoodsIDAndReaComAndLotNoAndStorage(entity, qtyBalanceDtlList, curQtyDtlOperList, empID, empName, ref qtyMonthBalanceDtlList);
            }
            else if (entity.StatisticalTypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocStatisticalType.按货品批号供应商库房货架.Key))
            {
                tempBaseResultDataValue = GetQtyMonthBalanceDtlListOfGroupByGoodsIDAndReaComAndLotNoAndStorageAndPlace(entity, qtyBalanceDtlList, curQtyDtlOperList, empID, empName, ref qtyMonthBalanceDtlList);
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 入库数据进行大小包装单位转换(将大包装单位转换为小包装单位)
        /// </summary>
        /// <param name="reaGoodsList"></param>
        /// <param name="inDtlList"></param>
        /// <returns></returns>
        private IList<ReaBmsInDtl> GetInDtlListConvertMinUnit(IList<ReaGoods> reaGoodsList, IList<ReaBmsInDtl> inDtlList)
        {
            IList<ReaBmsInDtl> inDtlTempList = new List<ReaBmsInDtl>();
            double goodsQty = 0, price = 0;
            foreach (var inDtl in inDtlList)
            {
                //IsMinUnit == true
                if (inDtl.ReaGoods.GonvertQty == 1)
                {
                    inDtlTempList.Add(inDtl);
                    //continue;
                }
                else
                {
                    //是否存在最小单位并且换算比率值>0
                    var tempGoodsList = reaGoodsList.Where(p => p.ReaGoodsNo == inDtl.ReaGoodsNo && p.GonvertQty == 1);
                    double gonvertQty = inDtl.ReaGoods.GonvertQty;
                    if (gonvertQty > 0 && tempGoodsList != null && tempGoodsList.Count() > 0)
                    {
                        var reaGoods = tempGoodsList.ElementAt(0);
                        ReaBmsInDtl inDtlTemp = ClassMapperHelp.GetMapper<ReaBmsInDtl, ReaBmsInDtl>(inDtl);
                        goodsQty = 0; price = 0;
                        if (inDtlTemp.GoodsQty.HasValue)
                            goodsQty = inDtlTemp.GoodsQty.Value;
                        if (inDtlTemp.Price.HasValue)
                            price = inDtlTemp.Price.Value;
                        inDtlTemp.GoodsQty = goodsQty * gonvertQty;
                        inDtlTemp.Price = price / gonvertQty;
                        inDtlTemp.SumTotal = goodsQty * price;
                        inDtlTemp.GoodsUnit = reaGoods.UnitName;
                        inDtlTemp.UnitMemo = reaGoods.UnitMemo;
                        inDtlTemp.ReaGoodsNo = reaGoods.ReaGoodsNo;
                        inDtlTemp.GoodsNo = reaGoods.GoodsNo;
                        inDtlTempList.Add(inDtlTemp);
                    }
                    else
                    {
                        inDtlTempList.Add(inDtl);
                    }
                }
            }
            return inDtlTempList;
        }
        /// <summary>
        /// 出库数据进行大小包装单位转换(将大包装单位转换为小包装单位)
        /// </summary>
        /// <param name="reaGoodsList"></param>
        /// <param name="outDtlList"></param>
        /// <returns></returns>
        private IList<ReaBmsOutDtl> GetOutDtlListConvertMinUnit(IList<ReaGoods> reaGoodsList, IList<ReaBmsOutDtl> outDtlList)
        {
            IList<ReaBmsOutDtl> outDtlTempList = new List<ReaBmsOutDtl>();

            foreach (var outDtl in outDtlList)
            {
                var tempGoodsList = reaGoodsList.Where(p => p.ReaGoodsNo == outDtl.ReaGoodsNo && p.GonvertQty == 1);
                var outReaGoods = reaGoodsList.Where(p => p.Id == outDtl.GoodsID.Value);
                double gonvertQty = outReaGoods.ElementAt(0).GonvertQty;
                //如果存在最小单位并且换算比率值>0
                if (gonvertQty > 0 && tempGoodsList != null && tempGoodsList.Count() > 0)
                {
                    var reaGoods = tempGoodsList.ElementAt(0);
                    ReaBmsOutDtl outDtlTemp = ClassMapperHelp.GetMapper<ReaBmsOutDtl, ReaBmsOutDtl>(outDtl);

                    outDtlTemp.GoodsQty = outDtl.GoodsQty * gonvertQty;
                    outDtlTemp.Price = outDtl.Price / gonvertQty;
                    outDtlTemp.SumTotal = outDtlTemp.GoodsQty * outDtlTemp.Price;
                    outDtlTemp.GoodsUnit = reaGoods.UnitName;
                    outDtlTemp.GoodsNo = reaGoods.GoodsNo;
                    outDtlTemp.ReaGoodsNo = reaGoods.ReaGoodsNo;
                    outDtlTempList.Add(outDtlTemp);
                }
                else
                {
                    outDtlTempList.Add(outDtl);
                }
            }
            return outDtlTempList;
        }
        /// <summary>
        /// 按货品编码+供应商ID+货品批号
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="qtyList"></param>
        /// <param name="inDtlList"></param>
        /// <param name="outDtlList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <param name="qtyMonthBalanceDtlList"></param>
        /// <returns></returns>
        private BaseResultDataValue GetQtyMonthBalanceDtlOfGroupByGoodsIDAndReaComAndLotNo(ReaBmsQtyMonthBalanceDoc entity, IList<ReaBmsQtyBalanceDtl> qtyBalanceDtlList, IList<ReaBmsQtyDtlOperation> curQtyDtlOperList, long empID, string empName, ref IList<ReaBmsQtyMonthBalanceDtl> qtyMonthBalanceDtlList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            var qtyBalanceGroupBy = qtyBalanceDtlList.GroupBy(p => new { p.ReaGoodsNo, p.ReaCompanyID, p.LotNo, p.GoodsUnit });

            foreach (var qtyBalanceItem in qtyBalanceGroupBy)
            {
                ReaBmsQtyMonthBalanceDtl qtyMonthBalanceDtl = new ReaBmsQtyMonthBalanceDtl();
                qtyMonthBalanceDtl.QtyMonthBalanceDocID = entity.Id;
                GetQtyMonthBalanceDtlOneOfQtyBalanceDtl(qtyBalanceItem.ElementAt(0), ref qtyMonthBalanceDtl, empID, empName);

                //初始库存数量及初始库存金额
                qtyMonthBalanceDtl.PreMonthQty = qtyBalanceItem.ElementAt(0).PreGoodsQty;
                qtyMonthBalanceDtl.PreMonthQtyPrice = qtyBalanceItem.ElementAt(0).PreSumTotal;
                //剩余库存数
                qtyMonthBalanceDtl.MonthQty = qtyBalanceItem.Sum(p => p.GoodsQty);
                //剩余库存金额
                qtyMonthBalanceDtl.MonthQtyPrice = qtyBalanceItem.Sum(p => p.SumTotal);
                var qtyBalance = qtyBalanceItem.ElementAt(0);
                var curQtyDtlOperList1 = curQtyDtlOperList.Where(p => p.ReaGoodsNo == qtyBalance.ReaGoodsNo && p.ReaCompanyID == qtyBalance.ReaCompanyID && p.LotNo == qtyBalance.LotNo && p.GoodsUnit == qtyBalance.GoodsUnit).ToList();
                GetReaBmsQtyDtlOperationOfQtyOper(curQtyDtlOperList1, qtyBalance, ref qtyMonthBalanceDtl);
                qtyMonthBalanceDtlList.Add(qtyMonthBalanceDtl);
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 按货品编码+供应商ID+货品批号+库房
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="qtyList"></param>
        /// <param name="inDtlList"></param>
        /// <param name="outDtlList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <param name="qtyMonthBalanceDtlList"></param>
        /// <returns></returns>
        private BaseResultDataValue GetQtyMonthBalanceDtlListOfGroupByGoodsIDAndReaComAndLotNoAndStorage(ReaBmsQtyMonthBalanceDoc entity, IList<ReaBmsQtyBalanceDtl> qtyBalanceDtlList, IList<ReaBmsQtyDtlOperation> curQtyDtlOperList, long empID, string empName, ref IList<ReaBmsQtyMonthBalanceDtl> qtyMonthBalanceDtlList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            var qtyGroupBy = qtyBalanceDtlList.GroupBy(p => new { p.ReaGoodsNo, p.ReaCompanyID, p.LotNo, p.StorageID, p.GoodsUnit });

            foreach (var qtyBalanceItem in qtyGroupBy)
            {
                ReaBmsQtyMonthBalanceDtl qtyMonthBalanceDtl = new ReaBmsQtyMonthBalanceDtl();
                qtyMonthBalanceDtl.QtyMonthBalanceDocID = entity.Id;
                GetQtyMonthBalanceDtlOneOfQtyBalanceDtl(qtyBalanceItem.ElementAt(0), ref qtyMonthBalanceDtl, empID, empName);

                //初始库存数量及初始库存金额
                qtyMonthBalanceDtl.PreMonthQty = qtyBalanceItem.ElementAt(0).PreGoodsQty;
                qtyMonthBalanceDtl.PreMonthQtyPrice = qtyBalanceItem.ElementAt(0).PreSumTotal;
                //剩余库存数
                qtyMonthBalanceDtl.MonthQty = qtyBalanceItem.Sum(p => p.GoodsQty);
                //剩余库存金额
                qtyMonthBalanceDtl.MonthQtyPrice = qtyBalanceItem.Sum(p => p.SumTotal);
                var qtyBalance = qtyBalanceItem.ElementAt(0);
                var curQtyDtlOperList1 = curQtyDtlOperList.Where(p => p.ReaGoodsNo == qtyBalance.ReaGoodsNo && p.ReaCompanyID == qtyBalance.ReaCompanyID && p.LotNo == qtyBalance.LotNo && p.StorageID == qtyBalance.StorageID && p.GoodsUnit == qtyBalance.GoodsUnit).ToList();
                GetReaBmsQtyDtlOperationOfQtyOper(curQtyDtlOperList1, qtyBalance, ref qtyMonthBalanceDtl);
                qtyMonthBalanceDtlList.Add(qtyMonthBalanceDtl);
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 按货品编码+供应商ID+货品批号+库房+货架
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="qtyList"></param>
        /// <param name="inDtlList"></param>
        /// <param name="outDtlList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <param name="qtyMonthBalanceDtlList"></param>
        /// <returns></returns>
        private BaseResultDataValue GetQtyMonthBalanceDtlListOfGroupByGoodsIDAndReaComAndLotNoAndStorageAndPlace(ReaBmsQtyMonthBalanceDoc entity, IList<ReaBmsQtyBalanceDtl> qtyBalanceDtlList, IList<ReaBmsQtyDtlOperation> curQtyDtlOperList, long empID, string empName, ref IList<ReaBmsQtyMonthBalanceDtl> qtyMonthBalanceDtlList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            var qtyGroupBy = qtyBalanceDtlList.GroupBy(p => new { p.ReaGoodsNo, p.ReaCompanyID, p.LotNo, p.StorageID, p.PlaceID, p.GoodsUnit });

            foreach (var qtyBalanceItem in qtyGroupBy)
            {
                var qtyBalance = qtyBalanceItem.ElementAt(0);
                ReaBmsQtyMonthBalanceDtl qtyMonthBalanceDtl = new ReaBmsQtyMonthBalanceDtl();
                qtyMonthBalanceDtl.QtyMonthBalanceDocID = entity.Id;
                GetQtyMonthBalanceDtlOneOfQtyBalanceDtl(qtyBalance, ref qtyMonthBalanceDtl, empID, empName);

                //初始库存数量及初始库存金额
                qtyMonthBalanceDtl.PreMonthQty = qtyBalance.PreGoodsQty;
                qtyMonthBalanceDtl.PreMonthQtyPrice = qtyBalance.PreSumTotal;
                //剩余库存数
                qtyMonthBalanceDtl.MonthQty = qtyBalanceItem.Sum(p => p.GoodsQty);
                //剩余库存金额
                qtyMonthBalanceDtl.MonthQtyPrice = qtyBalanceItem.Sum(p => p.SumTotal);

                var curQtyDtlOperList1 = curQtyDtlOperList.Where(p => p.ReaGoodsNo == qtyBalance.ReaGoodsNo && p.ReaCompanyID == qtyBalance.ReaCompanyID && p.LotNo == qtyBalance.LotNo && p.StorageID == qtyBalance.StorageID && p.PlaceID == qtyBalance.PlaceID && p.GoodsUnit == qtyBalance.GoodsUnit).ToList();
                GetReaBmsQtyDtlOperationOfQtyOper(curQtyDtlOperList1, qtyBalance, ref qtyMonthBalanceDtl);
                qtyMonthBalanceDtlList.Add(qtyMonthBalanceDtl);
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 新增时,库存结转明细转库存结转报表明细处理
        /// </summary>
        /// <param name="qty"></param>
        /// <param name="qtyMonthBalanceDtl"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        private void GetQtyMonthBalanceDtlOneOfQtyBalanceDtl(ReaBmsQtyBalanceDtl qty, ref ReaBmsQtyMonthBalanceDtl qtyMonthBalanceDtl, long empID, string empName)
        {
            qtyMonthBalanceDtl.ReaCompanyID = qty.ReaCompanyID;
            qtyMonthBalanceDtl.CompanyName = qty.CompanyName;
            qtyMonthBalanceDtl.ReaServerCompCode = qty.ReaServerCompCode;
            qtyMonthBalanceDtl.ReaGoodsNo = qty.ReaGoodsNo;

            qtyMonthBalanceDtl.CompGoodsLinkID = qty.CompGoodsLinkID;
            qtyMonthBalanceDtl.GoodsID = qty.GoodsID;
            qtyMonthBalanceDtl.GoodsName = qty.GoodsName;
            qtyMonthBalanceDtl.LotNo = qty.LotNo;
            qtyMonthBalanceDtl.ProdDate = qty.ProdDate;
            qtyMonthBalanceDtl.RegisterNo = qty.RegisterNo;

            qtyMonthBalanceDtl.InvalidDate = qty.InvalidDate;
            qtyMonthBalanceDtl.InvalidWarningDate = qty.InvalidWarningDate;
            qtyMonthBalanceDtl.GoodsUnitID = qty.GoodsUnitID;
            qtyMonthBalanceDtl.GoodsUnit = qty.GoodsUnit;
            qtyMonthBalanceDtl.UnitMemo = qty.UnitMemo;

            qtyMonthBalanceDtl.Price = qty.Price;
            qtyMonthBalanceDtl.Visible = true;
            qtyMonthBalanceDtl.CreaterID = empID;
            qtyMonthBalanceDtl.CreaterName = empName;
            qtyMonthBalanceDtl.DataUpdateTime = DateTime.Now;
        }
        #endregion

        #region 库存变化统计报表
        public BaseResultDataValue SaveQtyBalanceReportOfQtyDtlOperList(ReaBmsQtyMonthBalanceDoc entity, string labCName, long empID, string empName)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            IList<ReaBmsQtyBalanceDtl> qtyBalanceDtlList = new List<ReaBmsQtyBalanceDtl>();
            tempBaseResultDataValue = SaveBeforeVerify(entity, ref qtyBalanceDtlList);
            if (tempBaseResultDataValue.success == false) return tempBaseResultDataValue;

            entity.QtyMonthBalanceDocNo = this.GetQtyMonthBalanceDocNo();
            entity.OperID = empID;
            entity.OperName = empName;
            entity.OperDate = DateTime.Now;
            entity.Visible = true;
            entity.CreaterID = empID;
            entity.CreaterName = empName;
            entity.DataUpdateTime = DateTime.Now;
            if (string.IsNullOrEmpty(entity.TypeName))
                entity.TypeName = ReaBmsQtyMonthBalanceDocType.GetStatusDic()[entity.TypeID.ToString()].Name;
            if (string.IsNullOrEmpty(entity.StatisticalTypeName))
                entity.StatisticalTypeName = ReaBmsQtyMonthBalanceDocStatisticalType.GetStatusDic()[entity.StatisticalTypeID.ToString()].Name;
            this.Entity = entity;
            if (this.Add())
            {
                //按合并方式生成库存结转报表明细数据
                IList<ReaBmsQtyMonthBalanceDtl> qtyMonthBalanceDtlList = new List<ReaBmsQtyMonthBalanceDtl>();
                tempBaseResultDataValue = GetQtyMonthBalanceDtlListOfQtyDtlOperList(entity, empID, empName, ref qtyMonthBalanceDtlList);
                if (tempBaseResultDataValue.success == false) return tempBaseResultDataValue;

                //获取货品，将货品的排序赋值到DispOrder里
                var allGoodsList = IDReaGoodsDao.GetListByHQL("");
                foreach (var tmp in qtyMonthBalanceDtlList)
                {
                    var l = allGoodsList.Where(p => p.Id == tmp.GoodsID.Value).ToList();
                    if (l.Count > 0)
                    {
                        tmp.DispOrder = l[0].DispOrder;
                    }
                }
                //按照DispOrder升序
                qtyMonthBalanceDtlList = qtyMonthBalanceDtlList.OrderBy(p => p.DispOrder).ToList();

                //生成PDF
                entity.LabCName = labCName;
                Stream stream = CreateQtyMonthBalanceOfPdf(entity, qtyMonthBalanceDtlList, "");
                if (stream == null)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "生成库存结转报表PDF文件失败!";
                }
                else
                {
                    tempBaseResultDataValue = SaveBReport(entity);
                }
            }
            else
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo += ",新增失败!";
            }
            return tempBaseResultDataValue;
        }
        private BaseResultDataValue GetQtyMonthBalanceDtlListOfQtyDtlOperList(ReaBmsQtyMonthBalanceDoc entity, long empID, string empName, ref IList<ReaBmsQtyMonthBalanceDtl> qtyMonthBalanceDtlList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //获取起始日期之前的库存数变化记录集合
            IList<ReaBmsQtyDtlOperation> beforeQtyDtlOperList = new List<ReaBmsQtyDtlOperation>();
            string errorInfo = "";
            //获取当前统计条件的库存变化记录集合
            IList<ReaBmsQtyDtlOperation> curQtyDtlOperList = new List<ReaBmsQtyDtlOperation>();
            curQtyDtlOperList = GetCurQtyDtlOperationList(entity, ref errorInfo);
            if (curQtyDtlOperList == null || curQtyDtlOperList.Count <= 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = errorInfo + "获取库存变化操作记录为空!";
                return tempBaseResultDataValue;
            }
            beforeQtyDtlOperList = GetBeforeQtyDtlOperationList(entity, ref errorInfo);
            if (entity.StatisticalTypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocStatisticalType.按货品批号供应商.Key))
            {
                tempBaseResultDataValue = GetQtyMonthBalanceDtlOfGroupByGoodsIDAndReaComAndLotNo(entity, beforeQtyDtlOperList, curQtyDtlOperList, empID, empName, ref qtyMonthBalanceDtlList);
            }
            else if (entity.StatisticalTypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocStatisticalType.按货品批号供应商库房.Key))
            {
                tempBaseResultDataValue = GetQtyMonthBalanceDtlListOfGroupByGoodsIDAndReaComAndLotNoAndStorage(entity, beforeQtyDtlOperList, curQtyDtlOperList, empID, empName, ref qtyMonthBalanceDtlList);
            }
            else if (entity.StatisticalTypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocStatisticalType.按货品批号供应商库房货架.Key))
            {
                tempBaseResultDataValue = GetQtyMonthBalanceDtlListOfGroupByGoodsIDAndReaComAndLotNoAndStorageAndPlace(entity, beforeQtyDtlOperList, curQtyDtlOperList, empID, empName, ref qtyMonthBalanceDtlList);
            }
            return tempBaseResultDataValue;
        }
        private IList<ReaBmsQtyDtlOperation> GetBeforeQtyDtlOperationList(ReaBmsQtyMonthBalanceDoc entity, ref string errorInfo)
        {
            IList<ReaBmsQtyDtlOperation> qtyDtlOperList = new List<ReaBmsQtyDtlOperation>();
            DateTime startDate = DateTime.MinValue;
            if (entity.StartDate.HasValue)
                startDate = entity.StartDate.Value;
            DateTime endDate = entity.EndDate.Value;
            if (string.IsNullOrEmpty(entity.Round))
                entity.Round = endDate.ToString("yyyy-MM");
            string qtyDtlOperHql = " reabmsqtydtloperation.DataAddTime<='" + startDate.ToString("yyyy-MM-dd HH:mm:ss") + "'";//
            errorInfo = "";
            if (entity.TypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocType.按全部.Key))
            {
                errorInfo = "库存结转报表周期为" + entity.Round + ",库存结转报表类型为【按全部】!";
            }
            else if (entity.TypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocType.按库房.Key))
            {
                qtyDtlOperHql = qtyDtlOperHql + " and reabmsqtydtloperation.StorageID=" + entity.StorageID.Value;
                errorInfo = "库存结转报表周期为" + entity.Round + ",库存结转报表类型为【按库房】,选择的库房为【" + entity.StorageName + "】";
            }
            else if (entity.TypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocType.按库房货架.Key))
            {
                qtyDtlOperHql = qtyDtlOperHql + " and reabmsqtydtloperation.StorageID=" + entity.StorageID.Value;
                if (entity.PlaceID.HasValue)
                    qtyDtlOperHql = qtyDtlOperHql + " and reabmsqtydtloperation.PlaceID=" + entity.PlaceID;
                errorInfo = "库存结转报表周期为" + entity.Round + ",库存结转报表类型为【按库房货架】,选择的库房为【" + entity.StorageName + "】,选择的货架为【" + entity.PlaceName + "】";
            }
            string reaGoodHql = "";
            if (!string.IsNullOrEmpty(entity.GoodsClass))
            {
                reaGoodHql = reaGoodHql + " reagoods.GoodsClass='" + entity.GoodsClass + "' and ";
            }
            if (!string.IsNullOrEmpty(entity.GoodsClassType))
            {
                reaGoodHql = reaGoodHql + " reagoods.GoodsClassType='" + entity.GoodsClassType + "' and ";
            }
            if (string.IsNullOrEmpty(reaGoodHql))
            {
                qtyDtlOperList = IDReaBmsQtyDtlOperationDao.GetListByHQL(qtyDtlOperHql);
            }
            else
            {
                char[] trimChars = new char[] { ' ', 'a', 'n', 'd' };
                reaGoodHql = reaGoodHql.TrimEnd(trimChars);
                qtyDtlOperList = IDReaBmsQtyDtlOperationDao.SearchReaBmsQtyDtlOperationListByAllJoinHql(qtyDtlOperHql, reaGoodHql, "", -1, -1);
            }
            return qtyDtlOperList;
        }
        private IList<ReaBmsQtyDtlOperation> GetCurQtyDtlOperationList(ReaBmsQtyMonthBalanceDoc entity, ref string errorInfo)
        {
            IList<ReaBmsQtyDtlOperation> qtyDtlOperList = new List<ReaBmsQtyDtlOperation>();

            DateTime endDate = entity.EndDate.Value;
            if (string.IsNullOrEmpty(entity.Round))
                entity.Round = endDate.ToString("yyyy-MM");//
            string qtyDtlOperHql = " reabmsqtydtloperation.DataAddTime<='" + endDate.ToString("yyyy-MM-dd HH:mm:ss") + "'";//HH:mm:ss
            if (entity.StartDate.HasValue)
            {
                qtyDtlOperHql = qtyDtlOperHql + " and reabmsqtydtloperation.DataAddTime>='" + entity.StartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            errorInfo = "";
            if (entity.TypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocType.按全部.Key))
            {
                errorInfo = "库存结转报表周期为" + entity.Round + ",库存结转报表类型为【按全部】!";
            }
            else if (entity.TypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocType.按库房.Key))
            {
                qtyDtlOperHql = qtyDtlOperHql + " and reabmsqtydtloperation.StorageID=" + entity.StorageID.Value;
                errorInfo = "库存结转报表周期为" + entity.Round + ",库存结转报表类型为【按库房】,选择的库房为【" + entity.StorageName + "】";
            }
            else if (entity.TypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocType.按库房货架.Key))
            {
                qtyDtlOperHql = qtyDtlOperHql + " and reabmsqtydtloperation.StorageID=" + entity.StorageID.Value;
                if (entity.PlaceID.HasValue)
                    qtyDtlOperHql = qtyDtlOperHql + " and reabmsqtydtloperation.PlaceID=" + entity.PlaceID;
                errorInfo = "库存结转报表周期为" + entity.Round + ",库存结转报表类型为【按库房货架】,选择的库房为【" + entity.StorageName + "】,选择的货架为【" + entity.PlaceName + "】";
            }
            string reaGoodHql = "";
            if (!string.IsNullOrEmpty(entity.GoodsClass))
            {
                reaGoodHql = reaGoodHql + " reagoods.GoodsClass='" + entity.GoodsClass + "' and ";
            }
            if (!string.IsNullOrEmpty(entity.GoodsClassType))
            {
                reaGoodHql = reaGoodHql + " reagoods.GoodsClassType='" + entity.GoodsClassType + "' and ";
            }
            if (string.IsNullOrEmpty(reaGoodHql))
            {
                qtyDtlOperList = IDReaBmsQtyDtlOperationDao.GetListByHQL(qtyDtlOperHql);
            }
            else
            {
                char[] trimChars = new char[] { ' ', 'a', 'n', 'd' };
                reaGoodHql = reaGoodHql.TrimEnd(trimChars);
                qtyDtlOperList = IDReaBmsQtyDtlOperationDao.SearchReaBmsQtyDtlOperationListByAllJoinHql(qtyDtlOperHql, reaGoodHql, "", -1, -1);
            }
            return qtyDtlOperList;
        }
        private BaseResultDataValue GetQtyMonthBalanceDtlOfGroupByGoodsIDAndReaComAndLotNo(ReaBmsQtyMonthBalanceDoc entity, IList<ReaBmsQtyDtlOperation> beforeQtyDtlOperList, IList<ReaBmsQtyDtlOperation> curQtyDtlOperList, long empID, string empName, ref IList<ReaBmsQtyMonthBalanceDtl> qtyMonthBalanceDtlList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            var curQtyDtlOperGroupBy = curQtyDtlOperList.GroupBy(p => new { p.ReaGoodsNo, p.ReaCompanyID, p.LotNo, p.GoodsUnit });
            foreach (var qtyDtlOperItem in curQtyDtlOperGroupBy)
            {
                ReaBmsQtyMonthBalanceDtl qtyMonthBalanceDtl = new ReaBmsQtyMonthBalanceDtl();
                //初始库存数:指起始日期之前的库存数(某一库存货品)
                ReaBmsQtyDtlOperation qtyDtlOper = qtyDtlOperItem.ElementAt(0);
                var beforeQtyDtlOper = beforeQtyDtlOperList.Where(p => p.ReaGoodsNo == qtyDtlOper.ReaGoodsNo && p.ReaCompanyID == qtyDtlOper.ReaCompanyID && p.LotNo == qtyDtlOper.LotNo && p.GoodsUnit == qtyDtlOper.GoodsUnit);
                qtyMonthBalanceDtl.PreMonthQty = beforeQtyDtlOper.Sum(k => k.GoodsQty);
                qtyMonthBalanceDtl.PreMonthQtyPrice = beforeQtyDtlOper.Sum(k => k.SumTotal);
                if (qtyDtlOperItem.Count() > 0)
                {
                    GetReaBmsQtyDtlOperationOfQtyOper(qtyDtlOperItem.ToList(), null, ref qtyMonthBalanceDtl);
                    qtyMonthBalanceDtlList.Add(qtyMonthBalanceDtl);
                }
            }
            return tempBaseResultDataValue;
        }
        private BaseResultDataValue GetQtyMonthBalanceDtlListOfGroupByGoodsIDAndReaComAndLotNoAndStorage(ReaBmsQtyMonthBalanceDoc entity, IList<ReaBmsQtyDtlOperation> beforeQtyDtlOperList, IList<ReaBmsQtyDtlOperation> curQtyDtlOperList, long empID, string empName, ref IList<ReaBmsQtyMonthBalanceDtl> qtyMonthBalanceDtlList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            var curQtyDtlOperGroupBy = curQtyDtlOperList.GroupBy(p => new { p.ReaGoodsNo, p.ReaCompanyID, p.LotNo, p.StorageID, p.GoodsUnit });
            //var beforeQtyDtlOperGroupBy = beforeQtyDtlOperList.GroupBy(p => new { p.ReaGoodsNo, p.ReaCompanyID, p.LotNo, p.StorageID, p.GoodsUnit });
            foreach (var qtyDtlOperItem in curQtyDtlOperGroupBy)
            {
                ReaBmsQtyMonthBalanceDtl qtyMonthBalanceDtl = new ReaBmsQtyMonthBalanceDtl();
                //初始库存数:指起始日期之前的库存数(某一库存货品)
                ReaBmsQtyDtlOperation qtyDtlOper = qtyDtlOperItem.ElementAt(0);
                var beforeQtyDtlOper = beforeQtyDtlOperList.Where(p => p.ReaGoodsNo == qtyDtlOper.ReaGoodsNo && p.ReaCompanyID == qtyDtlOper.ReaCompanyID && p.LotNo == qtyDtlOper.LotNo && p.StorageID == qtyDtlOper.StorageID && p.GoodsUnit == qtyDtlOper.GoodsUnit);
                qtyMonthBalanceDtl.PreMonthQty = beforeQtyDtlOper.Sum(k => k.GoodsQty);
                qtyMonthBalanceDtl.PreMonthQtyPrice = beforeQtyDtlOper.Sum(k => k.SumTotal);
                if (qtyDtlOperItem.Count() > 0)
                {
                    GetReaBmsQtyDtlOperationOfQtyOper(qtyDtlOperItem.ToList(), null, ref qtyMonthBalanceDtl);
                    qtyMonthBalanceDtlList.Add(qtyMonthBalanceDtl);
                }
            }
            return tempBaseResultDataValue;
        }
        private BaseResultDataValue GetQtyMonthBalanceDtlListOfGroupByGoodsIDAndReaComAndLotNoAndStorageAndPlace(ReaBmsQtyMonthBalanceDoc entity, IList<ReaBmsQtyDtlOperation> beforeQtyDtlOperList, IList<ReaBmsQtyDtlOperation> curQtyDtlOperList, long empID, string empName, ref IList<ReaBmsQtyMonthBalanceDtl> qtyMonthBalanceDtlList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            var curQtyDtlOperGroupBy = curQtyDtlOperList.GroupBy(p => new { p.ReaGoodsNo, p.ReaCompanyID, p.LotNo, p.StorageID, p.PlaceID, p.GoodsUnit });
            foreach (var qtyDtlOperItem in curQtyDtlOperGroupBy)
            {
                ReaBmsQtyMonthBalanceDtl qtyMonthBalanceDtl = new ReaBmsQtyMonthBalanceDtl();
                //初始库存数:指起始日期之前的库存数(某一库存货品)
                ReaBmsQtyDtlOperation qtyDtlOper = qtyDtlOperItem.ElementAt(0);
                var beforeQtyDtlOper = beforeQtyDtlOperList.Where(p => p.ReaGoodsNo == qtyDtlOper.ReaGoodsNo && p.ReaCompanyID == qtyDtlOper.ReaCompanyID && p.LotNo == qtyDtlOper.LotNo && p.StorageID == qtyDtlOper.StorageID && p.PlaceID == qtyDtlOper.PlaceID && p.GoodsUnit == qtyDtlOper.GoodsUnit);
                qtyMonthBalanceDtl.PreMonthQty = beforeQtyDtlOper.Sum(k => k.GoodsQty);
                qtyMonthBalanceDtl.PreMonthQtyPrice = beforeQtyDtlOper.Sum(k => k.SumTotal);
                if (qtyDtlOperItem.Count() > 0)
                {
                    GetReaBmsQtyDtlOperationOfQtyOper(qtyDtlOperItem.ToList(), null, ref qtyMonthBalanceDtl);
                    qtyMonthBalanceDtlList.Add(qtyMonthBalanceDtl);
                }
            }
            return tempBaseResultDataValue;
        }
        private void GetReaBmsQtyDtlOperationOfQtyOper(IList<ReaBmsQtyDtlOperation> qtyDtlOperItem, ReaBmsQtyBalanceDtl qtyBalanceDtl, ref ReaBmsQtyMonthBalanceDtl qtyMonthBalanceDtl)
        {
            if (qtyDtlOperItem.Count > 0)
            {
                var qtyDtlOper = qtyDtlOperItem.ElementAt(0);
                qtyMonthBalanceDtl.GoodsID = qtyDtlOper.GoodsID;
                qtyMonthBalanceDtl.GoodsName = qtyDtlOper.GoodsName;
                qtyMonthBalanceDtl.GoodsUnit = qtyDtlOper.GoodsUnit;
                qtyMonthBalanceDtl.UnitMemo = qtyDtlOper.UnitMemo;
                qtyMonthBalanceDtl.Price = qtyDtlOper.Price;
                qtyMonthBalanceDtl.LotNo = qtyDtlOper.LotNo;
                qtyMonthBalanceDtl.ReaGoodsNo = qtyDtlOper.ReaGoodsNo;
                qtyMonthBalanceDtl.ReaCompanyID = qtyDtlOper.ReaCompanyID;
                qtyMonthBalanceDtl.ReaServerCompCode = qtyDtlOper.ReaServerCompCode;
                qtyMonthBalanceDtl.CompanyName = qtyDtlOper.CompanyName;
            }
            else if (qtyBalanceDtl != null)
            {
                qtyMonthBalanceDtl.GoodsID = qtyBalanceDtl.GoodsID;
                qtyMonthBalanceDtl.GoodsName = qtyBalanceDtl.GoodsName;
                qtyMonthBalanceDtl.GoodsUnit = qtyBalanceDtl.GoodsUnit;
                qtyMonthBalanceDtl.UnitMemo = qtyBalanceDtl.UnitMemo;
                qtyMonthBalanceDtl.Price = qtyBalanceDtl.Price;
                qtyMonthBalanceDtl.LotNo = qtyBalanceDtl.LotNo;
                qtyMonthBalanceDtl.ReaGoodsNo = qtyBalanceDtl.ReaGoodsNo;
                qtyMonthBalanceDtl.ReaCompanyID = qtyBalanceDtl.ReaCompanyID;
                qtyMonthBalanceDtl.ReaServerCompCode = qtyBalanceDtl.ReaServerCompCode;
                qtyMonthBalanceDtl.CompanyName = qtyBalanceDtl.CompanyName;
            }
            if (qtyMonthBalanceDtl.PreMonthQty <= 0) qtyMonthBalanceDtl.PreMonthQty = 0;
            if (qtyMonthBalanceDtl.PreMonthQtyPrice <= 0) qtyMonthBalanceDtl.PreMonthQtyPrice = 0;
            //入库总数
            var inAllList = qtyDtlOperItem.Where(p => p.OperTypeID == long.Parse(ReaBmsQtyDtlOperationOperType.库存初始化.Key) || p.OperTypeID == long.Parse(ReaBmsQtyDtlOperationOperType.验货入库.Key) || p.OperTypeID == long.Parse(ReaBmsQtyDtlOperationOperType.退库入库.Key) || p.OperTypeID == long.Parse(ReaBmsQtyDtlOperationOperType.借调入库.Key) || p.OperTypeID == long.Parse(ReaBmsQtyDtlOperationOperType.借入入库.Key) || p.OperTypeID == long.Parse(ReaBmsQtyDtlOperationOperType.盘盈入库.Key) || p.OperTypeID == long.Parse(ReaBmsQtyDtlOperationOperType.移库入库.Key));
            qtyMonthBalanceDtl.InQty = inAllList.Sum(p => p.ChangeCount);
            qtyMonthBalanceDtl.InQtyPrice = inAllList.Sum(p => p.SumTotal);
            //库存初始化的入库
            var availabilityList = qtyDtlOperItem.Where(p => p.OperTypeID == long.Parse(ReaBmsQtyDtlOperationOperType.库存初始化.Key));
            qtyMonthBalanceDtl.AvailabilityQty = availabilityList.Sum(p => p.ChangeCount);
            qtyMonthBalanceDtl.AvailabilityPrice = availabilityList.Sum(p => p.SumTotal);

            //验货入库
            var comfirmInList = qtyDtlOperItem.Where(p => p.OperTypeID == long.Parse(ReaBmsQtyDtlOperationOperType.验货入库.Key));
            qtyMonthBalanceDtl.ComfirmInQty = comfirmInList.Sum(p => p.ChangeCount);
            qtyMonthBalanceDtl.ComfirmInPrice = comfirmInList.Sum(p => p.SumTotal);
            //移库入库
            var transferInList = qtyDtlOperItem.Where(p => p.OperTypeID == long.Parse(ReaBmsQtyDtlOperationOperType.移库入库.Key));
            qtyMonthBalanceDtl.TransferInQty = transferInList.Sum(p => p.ChangeCount);
            qtyMonthBalanceDtl.TransferInPrice = transferInList.Sum(p => p.SumTotal);
            //退库入库
            var outOfInList = qtyDtlOperItem.Where(p => p.OperTypeID == long.Parse(ReaBmsQtyDtlOperationOperType.退库入库.Key));
            qtyMonthBalanceDtl.OutOfInQty = outOfInList.Sum(p => p.ChangeCount);
            qtyMonthBalanceDtl.OutOfInPrice = outOfInList.Sum(p => p.SumTotal);
            //盘盈入库
            var surplusInList = qtyDtlOperItem.Where(p => p.OperTypeID == long.Parse(ReaBmsQtyDtlOperationOperType.盘盈入库.Key));
            qtyMonthBalanceDtl.SurplusInQty = surplusInList.Sum(p => p.ChangeCount);
            qtyMonthBalanceDtl.SurplusInPrice = surplusInList.Sum(p => p.SumTotal);

            //仪器使用数处理
            var equipDtlList = qtyDtlOperItem.Where(p => p.OperTypeID == long.Parse(ReaBmsQtyDtlOperationOperType.使用出库.Key));
            qtyMonthBalanceDtl.EquipQty = equipDtlList.Sum(p => p.ChangeCount);
            qtyMonthBalanceDtl.EquipPrice = equipDtlList.Sum(p => p.SumTotal);
            if (qtyMonthBalanceDtl.EquipQty.HasValue) qtyMonthBalanceDtl.EquipQty = System.Math.Abs(qtyMonthBalanceDtl.EquipQty.Value);
            if (qtyMonthBalanceDtl.EquipPrice.HasValue) qtyMonthBalanceDtl.EquipPrice = System.Math.Abs(qtyMonthBalanceDtl.EquipPrice.Value);
            //退供应商数处理
            var returnQtyDtlList = qtyDtlOperItem.Where(p => p.OperTypeID == long.Parse(ReaBmsQtyDtlOperationOperType.退供应商.Key));
            qtyMonthBalanceDtl.ReturnQty = returnQtyDtlList.Sum(p => p.ChangeCount);
            qtyMonthBalanceDtl.ReturnPrice = returnQtyDtlList.Sum(p => p.SumTotal);
            if (qtyMonthBalanceDtl.ReturnQty.HasValue) qtyMonthBalanceDtl.ReturnQty = System.Math.Abs(qtyMonthBalanceDtl.ReturnQty.Value);
            if (qtyMonthBalanceDtl.ReturnPrice.HasValue) qtyMonthBalanceDtl.ReturnPrice = System.Math.Abs(qtyMonthBalanceDtl.ReturnPrice.Value);
            //报损出库数处理
            var lossQtyDtlList = qtyDtlOperItem.Where(p => p.OperTypeID == long.Parse(ReaBmsQtyDtlOperationOperType.报损出库.Key));
            qtyMonthBalanceDtl.LossQty = lossQtyDtlList.Sum(p => p.ChangeCount);
            qtyMonthBalanceDtl.LossQtyPrice = lossQtyDtlList.Sum(p => p.SumTotal);
            if (qtyMonthBalanceDtl.LossQty.HasValue) qtyMonthBalanceDtl.LossQty = System.Math.Abs(qtyMonthBalanceDtl.LossQty.Value);
            if (qtyMonthBalanceDtl.LossQtyPrice.HasValue) qtyMonthBalanceDtl.LossQtyPrice = System.Math.Abs(qtyMonthBalanceDtl.LossQtyPrice.Value);
            //盘亏出库数处理
            var diskLossQtyDtlList = qtyDtlOperItem.Where(p => p.OperTypeID == long.Parse(ReaBmsQtyDtlOperationOperType.盘亏出库.Key));
            qtyMonthBalanceDtl.DiskLossQty = diskLossQtyDtlList.Sum(p => p.ChangeCount);
            qtyMonthBalanceDtl.DiskLossQtyPrice = diskLossQtyDtlList.Sum(p => p.SumTotal);
            if (qtyMonthBalanceDtl.LossQty.HasValue) qtyMonthBalanceDtl.DiskLossQty = System.Math.Abs(qtyMonthBalanceDtl.DiskLossQty.Value);
            if (qtyMonthBalanceDtl.DiskLossQtyPrice.HasValue) qtyMonthBalanceDtl.DiskLossQtyPrice = System.Math.Abs(qtyMonthBalanceDtl.DiskLossQtyPrice.Value);
            //调账出库数处理
            //var adjustmentOutQtyDtlList = qtyDtlOperItem.Where(p => p.OperTypeID == long.Parse(ReaBmsQtyDtlOperationOperType.调账出库.Key));
            //qtyMonthBalanceDtl.AdjustmentOutQty = adjustmentOutQtyDtlList.Sum(p => p.ChangeCount);
            //qtyMonthBalanceDtl.AdjustmentOutQtyPrice = adjustmentOutQtyDtlList.Sum(p => p.SumTotal);
            if (qtyMonthBalanceDtl.AdjustmentOutQty.HasValue) qtyMonthBalanceDtl.AdjustmentOutQty = System.Math.Abs(qtyMonthBalanceDtl.AdjustmentOutQty.Value);
            if (qtyMonthBalanceDtl.AdjustmentOutQtyPrice.HasValue) qtyMonthBalanceDtl.AdjustmentOutQtyPrice = System.Math.Abs(qtyMonthBalanceDtl.AdjustmentOutQtyPrice.Value);

            //移库出库数处理
            var transferOutDtlList = qtyDtlOperItem.Where(p => p.OperTypeID == long.Parse(ReaBmsQtyDtlOperationOperType.移库出库.Key));
            qtyMonthBalanceDtl.TransferOutQty = transferOutDtlList.Sum(p => p.ChangeCount);
            qtyMonthBalanceDtl.TransferOutPrice = transferOutDtlList.Sum(p => p.SumTotal);
            if (qtyMonthBalanceDtl.TransferOutQty.HasValue) qtyMonthBalanceDtl.TransferOutQty = System.Math.Abs(qtyMonthBalanceDtl.TransferOutQty.Value);
            if (qtyMonthBalanceDtl.TransferOutPrice.HasValue) qtyMonthBalanceDtl.TransferOutPrice = System.Math.Abs(qtyMonthBalanceDtl.TransferOutPrice.Value);

            if (!qtyMonthBalanceDtl.InQty.HasValue) qtyMonthBalanceDtl.InQty = 0;
            if (!qtyMonthBalanceDtl.InQtyPrice.HasValue) qtyMonthBalanceDtl.InQtyPrice = 0;

            //变化库存数
            var changeQty = qtyDtlOperItem.Sum(p => p.GoodsQty);
            if (!changeQty.HasValue) changeQty = 0;
            //变化库存金额
            var changePrice = qtyDtlOperItem.Sum(p => p.SumTotal);
            if (!changePrice.HasValue) changePrice = 0;
            if (!qtyMonthBalanceDtl.MonthQty.HasValue || qtyMonthBalanceDtl.MonthQty <= 0)
            {
                //剩余库存数=初始库存数+变化库存数
                qtyMonthBalanceDtl.MonthQty = qtyMonthBalanceDtl.PreMonthQty + changeQty;
                //剩余库存金额=初始库存金额+变化库存金额
                qtyMonthBalanceDtl.MonthQtyPrice = qtyMonthBalanceDtl.PreMonthQtyPrice + changePrice;
            }
            if (qtyMonthBalanceDtl.MonthQty <= 0) qtyMonthBalanceDtl.MonthQty = 0;
            if (qtyMonthBalanceDtl.MonthQtyPrice <= 0) qtyMonthBalanceDtl.MonthQtyPrice = 0;
            //计算库存数=初始库存数+变化库存数
            qtyMonthBalanceDtl.CalcGoodsQty = qtyMonthBalanceDtl.PreMonthQty + changeQty;
            //计算库存金额=初始库存金额+变化库存金额
            qtyMonthBalanceDtl.CalcQtyPrice = qtyMonthBalanceDtl.PreMonthQtyPrice + changePrice;
        }
        #endregion

        #region 公共部分
        private BaseResultDataValue SaveBeforeVerify(ReaBmsQtyMonthBalanceDoc entity, ref IList<ReaBmsQtyBalanceDtl> qtyBalanceDtlList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (entity == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "entity为空!";
                return tempBaseResultDataValue;
            }
            //库存结转报表允许为空,库存变化报表不能为空
            if (!entity.QtyBalanceDocID.HasValue && !entity.StartDate.HasValue)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "库存结转报表起始日期不能为空!";
                return tempBaseResultDataValue;
            }
            if (!entity.EndDate.HasValue)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "库存结转报表结束日期不能为空!";
                return tempBaseResultDataValue;
            }
            if (!entity.TypeID.HasValue)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "库存结转报表的结转类型为空!";
                return tempBaseResultDataValue;
            }
            if (!entity.StatisticalTypeID.HasValue)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "库存结转报表的统计类型为空!";
                return tempBaseResultDataValue;
            }

            DateTime endDate = entity.EndDate.Value;
            if (string.IsNullOrEmpty(entity.Round))
                entity.Round = endDate.ToString("yyyy-MM");
            //判断是否已进行过库存结转报表条件00:00:00
            string hqlWhere = " reabmsqtymonthbalancedoc.Visible=1 and reabmsqtymonthbalancedoc.TypeID=" + entity.TypeID.Value + " and reabmsqtymonthbalancedoc.EndDate<='" + endDate.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            if (entity.StartDate.HasValue)
            {
                hqlWhere = hqlWhere + " and reabmsqtymonthbalancedoc.StartDate>='" + entity.StartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            }
            if (entity.QtyBalanceDocID.HasValue)
            {
                hqlWhere = hqlWhere + " and reabmsqtymonthbalancedoc.QtyBalanceDocID=" + entity.QtyBalanceDocID;
            }
            else
            {
                hqlWhere = hqlWhere + " and reabmsqtymonthbalancedoc.QtyBalanceDocID is null";
            }

            string errorInfo = "";
            if (entity.TypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocType.按全部.Key))
            {
                errorInfo = "库存结转报表周期为" + entity.Round + ",库存结转报表类型为【按全部】!";
            }
            else if (entity.TypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocType.按库房.Key))
            {
                if (!entity.StorageID.HasValue)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "库存结转报表类型为【按库房】时,库房选择不能为空!";
                    return tempBaseResultDataValue;
                }
                hqlWhere = hqlWhere + " and reabmsqtymonthbalancedoc.StorageID=" + entity.StorageID.Value;
                errorInfo = "库存结转报表周期为" + entity.Round + ",库存结转报表类型为【按库房】,选择的库房为【" + entity.StorageName + "】";
            }
            else if (entity.TypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocType.按库房货架.Key))
            {
                if (!entity.StorageID.HasValue)// || !entity.PlaceID.HasValue
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "库存结转报表类型为【按库房货架】时,库房选择及货架选择都不能为空!";
                    return tempBaseResultDataValue;
                }
                hqlWhere = hqlWhere + " and reabmsqtymonthbalancedoc.StorageID=" + entity.StorageID.Value;
                if (entity.PlaceID.HasValue)
                    hqlWhere = hqlWhere + " and reabmsqtymonthbalancedoc.PlaceID=" + entity.PlaceID;
                errorInfo = "库存结转报表周期为" + entity.Round + ",库存结转报表类型为【按库房货架】,选择的库房为【" + entity.StorageName + "】,选择的货架为【" + entity.PlaceName + "】";
            }
            //机构货品
            if (!string.IsNullOrEmpty(entity.GoodsClass))
            {
                hqlWhere = hqlWhere + " and reabmsqtymonthbalancedoc.GoodsClass='" + entity.GoodsClass + "'";
            }
            if (!string.IsNullOrEmpty(entity.GoodsClassType))
            {
                hqlWhere = hqlWhere + " and reabmsqtymonthbalancedoc.GoodsClassType='" + entity.GoodsClassType + "'";
            }
            IList<ReaBmsQtyMonthBalanceDoc> tempList = this.SearchListByHQL(hqlWhere);
            if (tempList.Count > 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = errorInfo + ",已进行过库存结转报表!";
                return tempBaseResultDataValue;
            }

            return tempBaseResultDataValue;
        }
        public BaseResultBool UpdateCancelReaBmsQtyMonthBalanceDocById(long id, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            ReaBmsQtyMonthBalanceDoc entity = this.Get(id);

            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "库存结转报表ID为:" + id + ",不存在数据库中!";
                return tempBaseResultBool;
            }
            if (entity.Visible == false)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "库存结转报表ID为:" + id + ",已被禁用!";
                return tempBaseResultBool;
            }
            entity.OperID = empID;
            entity.OperName = empName;
            entity.OperDate = DateTime.Now;
            entity.Visible = false;
            this.Entity = entity;

            IList<string> tmpa = new List<string>();
            tmpa.Add("Id=" + entity.Id);
            tmpa.Add("Visible=0 ");
            tmpa.Add("OperID=" + empID + " ");
            tmpa.Add("OperName='" + empName + "'");
            if (!entity.OperDate.HasValue) tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

            string[] tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                tempBaseResultBool.ErrorInfo = "库存结转报表单取消成功!";
            }
            else
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "库存结转报表单取消失败!";
            }
            return tempBaseResultBool;
        }
        public EntityList<ReaBmsQtyMonthBalanceDtl> SearchQtyMonthBalanceDtlListById(long id, int page, int limit, ref BaseResultDataValue tempBaseResultDataValue)
        {
            ReaBmsQtyMonthBalanceDoc entity = this.Get(id);
            EntityList<ReaBmsQtyMonthBalanceDtl> qtyMonthBalanceDtlList = new EntityList<ReaBmsQtyMonthBalanceDtl>();
            if (entity == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "库存结转报表ID为:" + id + ",不存在!";
                return qtyMonthBalanceDtlList;
            }

            qtyMonthBalanceDtlList = SearchQtyMonthBalanceDtlListByDoc(entity, page, limit, ref tempBaseResultDataValue);
            return qtyMonthBalanceDtlList;
        }
        private EntityList<ReaBmsQtyMonthBalanceDtl> SearchQtyMonthBalanceDtlListByDoc(ReaBmsQtyMonthBalanceDoc entity, int page, int limit, ref BaseResultDataValue tempBaseResultDataValue)
        {
            EntityList<ReaBmsQtyMonthBalanceDtl> qtyMonthBalanceDtlList = new EntityList<ReaBmsQtyMonthBalanceDtl>();
            //按合并方式生成库存结转报表明细数据
            IList<ReaBmsQtyMonthBalanceDtl> tempList = new List<ReaBmsQtyMonthBalanceDtl>();
            long empID = entity.CreaterID;
            string empName = entity.CreaterName;
            string errorInfo = "";
            if (entity.QtyBalanceDocID.HasValue)
            {
                //获取库存结转报表条件的库存结转信息
                IList<ReaBmsQtyBalanceDtl> qtyBalanceDtlList = GetQtyBalanceDtlList(entity, ref errorInfo);
                if (qtyBalanceDtlList == null || qtyBalanceDtlList.Count <= 0)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = errorInfo + ",库存结转数据为空!";
                    return qtyMonthBalanceDtlList;
                }
                tempBaseResultDataValue = GetQtyMonthBalanceDtlListOfQtyBalanceDtlList(entity, qtyBalanceDtlList, empID, empName, ref tempList);
            }
            else
            {
                tempBaseResultDataValue = GetQtyMonthBalanceDtlListOfQtyDtlOperList(entity, empID, empName, ref tempList);
            }
                        
            if (tempBaseResultDataValue.success == true)
            {
                //获取货品，将货品的排序赋值到DispOrder里
                var allGoodsList = IDReaGoodsDao.GetListByHQL("");
                foreach (var tmp in tempList)
                {
                    var l = allGoodsList.Where(p => p.Id == tmp.GoodsID.Value).ToList();
                    if (l.Count > 0)
                    {
                        tmp.DispOrder = l[0].DispOrder;
                    }
                }

                qtyMonthBalanceDtlList.count = tempList.Count;
                //排序
                tempList = tempList.OrderBy(p => p.DispOrder).ToList();
                //分页处理
                if (limit > 0 && limit < tempList.Count)
                {
                    int startIndex = limit * (page - 1);
                    int endIndex = limit;
                    var list = tempList.Skip(startIndex).Take(endIndex);
                    if (list != null)
                        tempList = list.ToList();
                }
                qtyMonthBalanceDtlList.list = tempList;
            }
            return qtyMonthBalanceDtlList;
        }
        public Stream GetQtyMonthBalanceAndDtlOfPdf(long id, string frx, string labCName)
        {
            Stream stream = null;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            ReaBmsQtyMonthBalanceDoc entity = this.Get(id);
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "库存结转报表单ID：" + id + "不存在数据库中！";
                return stream;
            }
            //通过库存结转报表主单ID查找出原来生成的PDF
            string pdfName = entity.Id.ToString() + ".pdf" + "." + FileExt.zf;
            stream = PdfReportHelp.GetReportPDF(entity.LabID, pdfName, "库存结转报表");
            if (stream == null || stream.Length <= 0)
            {
                ZhiFang.Common.Log.Log.Error("库存结转报表ID为:" + entity.Id + ",PDF不存在,重新生成!");
                BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
                EntityList<ReaBmsQtyMonthBalanceDtl> qtyMonthBalanceDtlList = SearchQtyMonthBalanceDtlListByDoc(entity, -1, -1, ref tempBaseResultDataValue);
                stream = CreateQtyMonthBalanceOfPdf(entity, qtyMonthBalanceDtlList.list, "");
            }
            return stream;
        }
        private Stream CreateQtyMonthBalanceOfPdf(ReaBmsQtyMonthBalanceDoc entity, IList<ReaBmsQtyMonthBalanceDtl> qtyMonthBalanceDtlList, string frx)
        {
            Stream stream = null;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            DataSet dataSet = new DataSet();
            dataSet.DataSetName = "ZhiFang.ReagentSys.Client";
            try
            {
                List<ReaBmsQtyMonthBalanceDoc> docList = new List<ReaBmsQtyMonthBalanceDoc>();
                docList.Add(entity);

                DataTable docDt = ReportBTemplateHelp.ToDataTable<ReaBmsQtyMonthBalanceDoc>(docList, null);
                if (docDt != null)
                {
                    docDt.TableName = "ReaBmsQtyMonthBalanceDoc";
                    dataSet.Tables.Add(docDt);
                }
                //qtyMonthBalanceDtlList = qtyMonthBalanceDtlList.OrderBy(p => p.LotNo).ToList();

                DataTable dtDtl = ReportBTemplateHelp.ToDataTable<ReaBmsQtyMonthBalanceDtl>(qtyMonthBalanceDtlList, null);
                if (dtDtl != null)
                {
                    dtDtl.TableName = "DtlList";
                    dataSet.Tables.Add(dtDtl);
                }
                ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
                ExcelReportHelp.CreateEECDataTable(excelCommand, ref dataSet);
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("库存结转报表数据转换为DataTable出错:" + ex.Message);
                throw ex;
            }

            if (string.IsNullOrEmpty(frx))
            {
                if (entity.StatisticalTypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocStatisticalType.按货品批号供应商.Key))
                {
                    frx = "按货品批号供应商.frx";
                }
                else if (entity.StatisticalTypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocStatisticalType.按货品批号供应商库房.Key))
                {
                    frx = "按货品批号供应商库房.frx";
                }
                else if (entity.StatisticalTypeID.Value == long.Parse(ReaBmsQtyMonthBalanceDocStatisticalType.按货品批号供应商库房货架.Key))
                {
                    frx = "按货品批号供应商库房货架.frx";
                }
                else
                {
                    frx = "按货品批号供应商库房货架.frx"; //"库存结转报表单.frx";
                }
            }
            string pdfName = entity.Id.ToString() + ".pdf." + FileExt.zf;
            string reportSubDir = BTemplateType.GetStatusDic()[BTemplateType.库存结转报表.Key].Name;
            stream = FrxToPdfReportHelp.SavePdfReport(dataSet, entity.LabID, pdfName, FrxToPdfReportHelp.PublicTemplateDir, reportSubDir, frx, true);
            return stream;
        }
        private string GetQtyMonthBalanceDocNo()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));
            Random ran = new Random();
            int randKey = ran.Next(0, 999);
            strb.Append(randKey.ToString().PadLeft(3, '0'));
            strb.Append(DateTime.Now.ToString("HHmmssfff"));
            return strb.ToString();
        }
        #endregion
    }
}