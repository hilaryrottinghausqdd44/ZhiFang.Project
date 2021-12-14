using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.BLL.Base;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaConfirm;
using ZhiFang.Entity.ReagentSys.Client.ViewObject;
using ZhiFang.ServiceCommon.RBAC;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ZhiFang.ReagentSys.Client.Common;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsCenOrderDtl : BaseBLL<ReaBmsCenOrderDtl>, IBReaBmsCenOrderDtl
    {
        IDReaBmsCenSaleDtlConfirmDao IDReaBmsCenSaleDtlConfirmDao { get; set; }
        IDReaBmsCenSaleDocConfirmDao IDReaBmsCenSaleDocConfirmDao { get; set; }
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IDReaGoodsBarcodeOperationDao IDReaGoodsBarcodeOperationDao { get; set; }
        IDReaGoodsOrgLinkDao IDReaGoodsOrgLinkDao { get; set; }
        IDReaBmsCenOrderDocDao IDReaBmsCenOrderDocDao { get; set; }

        #region 客户端订单明细处理
        public BaseResultBool EditDtlListCheck(IList<ReaBmsCenOrderDtl> dtEditList)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (dtEditList != null && dtEditList.Count > 0)
            {
                foreach (var entity in dtEditList)
                {
                    if (tempBaseResultBool.success == false) break;

                    ReaGoodsOrgLink reaGoodsOrgLink = IDReaGoodsOrgLinkDao.Get(entity.CompGoodsLinkID.Value);
                    if (reaGoodsOrgLink == null)
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "货品为:" + entity.ReaGoodsName + ",供货商货品关系ID为:" + entity.CompGoodsLinkID.Value + ",已从供货商货品维护里删除,请从货品明细里移除该货品再保存!建议删除原订货货品模板重新维护新的订货货品模板!";
                    }
                    else if (reaGoodsOrgLink.Visible == 0)
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "货品为:" + entity.ReaGoodsName + ",供货商货品关系ID为:" + entity.CompGoodsLinkID.Value + ",已在供货商货品维护里禁用,请从订货明细里移除该货品再保存!建议删除原订货货品模板重新维护新的订货货品模板!";
                    }
                }
            }
            return tempBaseResultBool;
        }
        public BaseResultBool AddDtList(IList<ReaBmsCenOrderDtl> dtAddList, ReaBmsCenOrderDoc reaBmsOrderDoc, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            tempBaseResultBool = EditDtlListCheck(dtAddList);
            if (tempBaseResultBool.success == false) return tempBaseResultBool;

            if (dtAddList != null && dtAddList.Count > 0)
            {
                foreach (var entity in dtAddList)
                {
                    if (tempBaseResultBool.success == false) break;

                    if (!entity.LabOrderDtlID.HasValue)
                        entity.LabOrderDtlID = entity.Id;
                    entity.LabID = reaBmsOrderDoc.LabID;
                    entity.OrderDocID = reaBmsOrderDoc.Id;
                    entity.OrderDocNo = reaBmsOrderDoc.OrderDocNo;
                    if (string.IsNullOrEmpty(entity.OrderDtlNo))
                        entity.OrderDtlNo = this.GetOrderDtlNo();
                    entity.DataUpdateTime = DateTime.Now;

                    entity.SuppliedQty = 0;//已供数量，默认=0
                    entity.UnSupplyQty = entity.GoodsQty.Value;//未供数量，默认=订货审批数量

                    this.Entity = entity;
                    tempBaseResultBool.success = this.Add();

                    if (tempBaseResultBool.success == false)
                    {
                        tempBaseResultBool.ErrorInfo = "货品为:" + entity.ReaGoodsName + ",新增保存失败!";
                    }
                }
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditDtList(IList<ReaBmsCenOrderDtl> dtEditList, ReaBmsCenOrderDoc orderDoc)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            tempBaseResultBool = EditDtlListCheck(dtEditList);
            if (tempBaseResultBool.success == false) return tempBaseResultBool;

            if (dtEditList != null && dtEditList.Count > 0)
            {
                List<string> tmpa = new List<string>();
                foreach (var entity in dtEditList)
                {
                    tmpa.Clear();
                    if (tempBaseResultBool.success == false) break;

                    if (!entity.ReqGoodsQty.HasValue)
                        entity.ReqGoodsQty = 0;
                    if (!entity.GoodsQty.HasValue)
                        entity.GoodsQty = 0;
                    if (!entity.Price.HasValue)
                        entity.Price = 0;
                    if (!entity.MonthlyUsage.HasValue)
                        entity.MonthlyUsage = 0;
                    if (!entity.CurrentQty.HasValue)
                        entity.CurrentQty = 0;
                    if (!entity.ExpectedStock.HasValue)
                        entity.ExpectedStock = 0;
                    entity.SumTotal = entity.GoodsQty.Value * entity.Price.Value;
                    tmpa.Add("Id=" + entity.Id + " ");
                    tmpa.Add("MonthlyUsage=" + entity.MonthlyUsage + " ");
                    tmpa.Add("CurrentQty=" + entity.CurrentQty + " ");
                    tmpa.Add("ExpectedStock=" + entity.ExpectedStock + " ");

                    tmpa.Add("ReqGoodsQty=" + entity.ReqGoodsQty + " ");
                    tmpa.Add("GoodsQty=" + entity.GoodsQty + " ");
                    if (entity.Price.HasValue)
                        tmpa.Add("Price=" + entity.Price + " ");
                    else
                        tmpa.Add("Price=null ");

                    tmpa.Add("ProdOrgName='" + entity.ProdOrgName + "' ");

                    if (entity.ArrivalTime.HasValue)
                        tmpa.Add("ArrivalTime='" + entity.ArrivalTime + "' ");
                    else
                        tmpa.Add("ArrivalTime=null ");

                    if (entity.SumTotal.HasValue)
                        tmpa.Add("SumTotal=" + entity.SumTotal.Value + " ");
                    else
                        tmpa.Add("SumTotal=null ");

                    if (orderDoc.IOFlag.HasValue)
                        tmpa.Add("IOFlag=" + orderDoc.IOFlag + " ");

                    entity.SuppliedQty = 0;//已供数量，默认=0
                    entity.UnSupplyQty = entity.GoodsQty.Value;//未供数量，默认=订货审批数量
                    tmpa.Add("SuppliedQty=" + entity.SuppliedQty + " ");
                    tmpa.Add("UnSupplyQty=" + entity.UnSupplyQty + " ");

                    tempBaseResultBool.success = this.Update(tmpa.ToArray());

                    if (tempBaseResultBool.success == false)
                    {
                        tempBaseResultBool.ErrorInfo = "货品为:" + entity.ReaGoodsName + ",保存失败!";
                    }
                }
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 客户端订单验收,获取某一订单的待验收订单明细VO集合信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<ReaOrderDtlOfConfirmVO> SearchReaOrderDtlOfConfirmVOListByHQL(string strHqlWhere, string order, int page, int limit)
        {
            EntityList<ReaOrderDtlOfConfirmVO> entityVOList = new EntityList<ReaOrderDtlOfConfirmVO>();
            entityVOList.list = new List<ReaOrderDtlOfConfirmVO>();

            EntityList<ReaBmsCenOrderDtl> el = this.SearchListByHQL(strHqlWhere, order, -1, -1);
            if (el.count <= 0)
                return entityVOList;

            ReaBmsCenOrderDoc orderDoc = null;
            if (el.count > 0) orderDoc = IDReaBmsCenOrderDocDao.Get(el.list[0].OrderDocID);

            if (orderDoc == null)
                return entityVOList;

            if (orderDoc.Status == int.Parse(ReaBmsOrderDocStatus.全部验收.Key))
                return entityVOList;
            IList<ReaOrderDtlOfConfirmVO> tempList = new List<ReaOrderDtlOfConfirmVO>();

            IList<ReaBmsCenSaleDtlConfirm> dtlConfirmList = IDReaBmsCenSaleDtlConfirmDao.GetListByHQL(String.Format("reabmscensaledtlconfirm.OrderDocID={0}", orderDoc.Id));

            foreach (var model in el.list)
            {
                ReaOrderDtlOfConfirmVO vo = ClassMapperHelp.GetMapper<ReaOrderDtlOfConfirmVO, ReaBmsCenOrderDtl>(model);
                changeReaOrderDtlVO(dtlConfirmList, vo);
                if (model.GoodsQty.HasValue)
                    vo.DtlGoodsQty = model.GoodsQty.Value;
                tempList.Add(vo);
            }
            //过滤已验收完的订单明细(可验收数大于0)
            tempList = tempList.Where(p => p.ConfirmCount > 0).ToList();

            entityVOList.count = tempList.Count;
            //分页处理
            if (limit > 0 && limit < tempList.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = tempList.Skip(startIndex).Take(endIndex);
                if (list != null)
                    tempList = list.ToList();
            }
            //订单货品明细条码类型赋值处理
            for (int i = 0; i < tempList.Count; i++)
            {
                ReaGoods reaGoods = IDReaGoodsDao.Get(tempList[i].ReaGoodsID.Value);
                tempList[i].ReaGoods = reaGoods;
            }
            entityVOList.list = tempList;
            return entityVOList;
        }
        /// <summary>
        /// 某一订单明细的验收信息处理
        /// </summary>
        /// <param name="dtlConfirmList"></param>
        /// <param name="vo"></param>
        private void changeReaOrderDtlVO(IList<ReaBmsCenSaleDtlConfirm> dtlConfirmList, ReaOrderDtlOfConfirmVO vo)
        {
            if (dtlConfirmList != null)
            {
                //某一订单明细的所有验收明细信息
                var dtlList = dtlConfirmList.Where(p => p.OrderDtlID.Value == vo.Id).ToList();
                if (dtlList != null && dtlList.Count() > 0)
                {
                    StringBuilder strbIDS = new StringBuilder();
                    foreach (var item in dtlList)
                    {
                        strbIDS.Append(item.Id + ",");
                    }
                    IList<ReaGoodsBarcodeOperation> dtlBarCodeList = IDReaGoodsBarcodeOperationDao.GetListByHQL(string.Format("reagoodsbarcodeoperation.BDtlID in({0}) and (reagoodsbarcodeoperation.OperTypeID={1} or reagoodsbarcodeoperation.OperTypeID={2})", strbIDS.ToString().TrimEnd(','), ReaGoodsBarcodeOperType.验货接收.Key, ReaGoodsBarcodeOperType.验货拒收.Key));
                    IList<ReaGoodsBarcodeOperationVO> dtVOList = new List<ReaGoodsBarcodeOperationVO>();
                    //获取每一盒条码(供货验收或供货拒收)的最后操作记录
                    Dictionary<string, ReaGoodsBarcodeOperation> tempDictionary = new Dictionary<string, ReaGoodsBarcodeOperation>();
                    //按验收明细ID进行分组
                    var dtlIDGroupBy = dtlBarCodeList.GroupBy(p => p.BDtlID);
                    foreach (var model in dtlIDGroupBy)
                    {
                        //某一验收明细的所有验收盒条码信息按使用盒条码进行分组
                        var serialGroupBy = dtlBarCodeList.Where(p => p.BDtlID.Value == model.Key.Value).GroupBy(p => p.UsePackSerial);
                        tempDictionary.Clear();
                        foreach (var item in serialGroupBy)
                        {
                            var tempList = item.OrderByDescending(p => p.DataAddTime).ToList();
                            if (tempDictionary.Keys.Contains(item.Key))
                                tempDictionary.Add(item.Key, tempList[0]);
                        }
                        foreach (var item in tempDictionary)
                        {
                            var operation = item.Value;
                            ReaGoodsBarcodeOperationVO operationVO = new ReaGoodsBarcodeOperationVO();

                            operationVO.Id = item.Value.Id;
                            operationVO.BDocNo = item.Value.BDocNo;
                            operationVO.BDocID = item.Value.BDocID;
                            operationVO.BDtlID = item.Value.BDtlID;
                            operationVO.QtyDtlID = item.Value.QtyDtlID;
                            operationVO.OperTypeID = item.Value.OperTypeID;

                            //签收标志：2接收、3拒收
                            operationVO.ReceiveFlag = operationVO.OperTypeID.Value;
                            operationVO.SysPackSerial = item.Value.SysPackSerial;
                            operationVO.OtherPackSerial = item.Value.OtherPackSerial;
                            operationVO.UsePackSerial = item.Value.UsePackSerial;
                            operationVO.UsePackQRCode = item.Value.UsePackQRCode;

                            operationVO.LotNo = item.Value.LotNo;
                            operationVO.ReaGoodsNo = item.Value.ReaGoodsNo;
                            operationVO.CenOrgGoodsNo = item.Value.CenOrgGoodsNo;
                            operationVO.ProdGoodsNo = item.Value.ProdGoodsNo;
                            operationVO.GoodsNo = item.Value.GoodsNo;

                            operationVO.ReaCompCode = item.Value.ReaCompCode;
                            operationVO.GoodsSort = item.Value.GoodsSort;
                            operationVO.CompGoodsLinkID = item.Value.CompGoodsLinkID;
                            operationVO.BarCodeType = item.Value.BarCodeType;

                            dtVOList.Add(operationVO);
                        }
                    }

                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                    vo.ReaBmsCenSaleDtlConfirmLinkVOListStr = tempParseObjectProperty.GetObjectPropertyNoPlanish(dtVOList);
                    vo.ReceivedCount = dtlList.Where(p => p.Status.Value != int.Parse(ReaBmsCenSaleDocConfirmStatus.待继续验收.Key)).Sum(p => p.AcceptCount);
                    vo.RejectedCount = dtlList.Where(p => p.Status.Value != int.Parse(ReaBmsCenSaleDocConfirmStatus.待继续验收.Key)).Sum(p => p.RefuseCount);
                }
                var goodsQty = vo.ReceivedCount + vo.RejectedCount;
                if (vo.GoodsQty.HasValue)
                    vo.ConfirmCount = vo.GoodsQty.Value - goodsQty;
                if (vo.ConfirmCount < 0) vo.ConfirmCount = 0;
                if (goodsQty >= vo.GoodsQty) vo.AcceptFlag = true; else vo.AcceptFlag = false;
            }
        }
        /// <summary>
        /// 获取订单明细单号
        /// </summary>
        /// <returns></returns>
        private string GetOrderDtlNo()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));
            Random ran = new Random();
            int randKey = ran.Next(0, 999);
            strb.Append(randKey.ToString().PadLeft(3, '0'));//左补零
            strb.Append(DateTime.Now.ToString("HHmmssfff"));
            return strb.ToString();
        }
        #endregion

        #region 统计报表/Excel导出/PDF预览
        public IList<ReaBmsCenOrderDtl> SearchReaBmsCenOrderDtlListByHQL(string docHql, string dtlHql, string reaGoodsHql, string order, int page, int limit)
        {
            IList<ReaBmsCenOrderDtl> dtlList = ((IDReaBmsCenOrderDtlDao)base.DBDao).SearchReaBmsCenOrderDtlSummaryByHQL(docHql, dtlHql, reaGoodsHql, order, page, limit);
            //dtlList = SearchReaBmsCenOrderDtlListOfGroupBy1(dtlList);

            return dtlList;
        }
        public EntityList<ReaBmsCenOrderDtl> SearchReaBmsCenOrderDtlSummaryByHQL(int groupType, string docHql, string dtlHql, string reaGoodsHql, string order, int page, int limit)
        {
            EntityList<ReaBmsCenOrderDtl> entityList = new EntityList<ReaBmsCenOrderDtl>();
            if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
                return entityList;

            IList<ReaBmsCenOrderDtl> dtlList = new List<ReaBmsCenOrderDtl>();
            //if (groupType.ToString() == ReaBmsCenOrderDtlGroupType.按订货品明细.Key)
            //{
            //    dtlList = ((IDReaBmsCenOrderDtlDao)base.DBDao).SearchReaBmsCenOrderDtlSummaryByHQL(docHql, dtlHql, reaGoodsHql, order, page, limit);
            //    entityList.list = dtlList;
            //    entityList.count = dtlList.Count;
            //    return entityList;
            //}
            dtlList = ((IDReaBmsCenOrderDtlDao)base.DBDao).SearchReaBmsCenOrderDtlSummaryByHQL(docHql, dtlHql, reaGoodsHql, order, -1, -1);

            if (groupType.ToString() == ReaBmsCenOrderDtlStatisticalType.订单明细汇总.Key)
            {
                dtlList = SearchReaBmsCenOrderDtlListOfGroupBy1(dtlList);
            }
            else if (groupType.ToString() == ReaBmsCenOrderDtlStatisticalType.订单明细.Key)
            {
                //不需要合并，直接返回dtlList
            }
            else if (groupType.ToString() == ReaBmsCenOrderDtlStatisticalType.订单号汇总.Key)
            {
                dtlList = SearchReaBmsCenOrderDtlListOfGroupBy3(dtlList);
            }

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
        /// 合并条件:部门ID+供应商+货品产品编码+包装单位+规格
        /// </summary>
        /// <param name="outDtlList"></param>
        /// <returns></returns>
        private IList<ReaBmsCenOrderDtl> SearchReaBmsCenOrderDtlListOfGroupBy1(IList<ReaBmsCenOrderDtl> outDtlList)
        {
            return outDtlList.GroupBy(p => new
            {
                p.DeptID,
                p.ReaCompID,
                p.ReaGoodsNo,
                p.CenOrgGoodsNo,
                p.GoodsUnit,
                p.UnitMemo
            }).Select(g => new ReaBmsCenOrderDtl
            {
                Id = g.ElementAt(0).Id,
                LabID = g.ElementAt(0).LabID,
                DeptID = g.ElementAt(0).DeptID,
                DeptName = g.ElementAt(0).DeptName,
                ReaCompID = g.ElementAt(0).ReaCompID,
                CompanyName = g.ElementAt(0).CompanyName,

                LabcID = g.ElementAt(0).LabcID,
                LabcName = g.ElementAt(0).LabcName,//订货方
                ReaServerLabcCode = g.ElementAt(0).ReaServerLabcCode,
                ReaGoodsNo = g.Key.ReaGoodsNo,
                CenOrgGoodsNo = g.Key.CenOrgGoodsNo,
                GoodsName = g.ElementAt(0).GoodsName,

                ReaGoodsID = g.ElementAt(0).ReaGoodsID,
                ReaGoodsName = g.ElementAt(0).ReaGoodsName,
                GoodsUnit = g.ElementAt(0).GoodsUnit,
                UnitMemo = g.ElementAt(0).UnitMemo,
                ExpectedStock = g.ElementAt(0).ExpectedStock,
                MonthlyUsage = g.ElementAt(0).MonthlyUsage,

                LastMonthlyUsage = g.ElementAt(0).LastMonthlyUsage,
                ReqGoodsQty = g.Sum(k => k.ReqGoodsQty), //g.ElementAt(0).ReqGoodsQty,
                GoodsQty = g.Sum(k => k.GoodsQty),
                SumTotal = g.Sum(k => k.SumTotal),
                Price = g.Sum(k => k.GoodsQty) > 0 ? g.Sum(k => k.SumTotal) / g.Sum(k => k.GoodsQty) : 0,//g.ElementAt(0).Price,

                CurrentQty = g.ElementAt(0).CurrentQty,
                ProdID = g.ElementAt(0).ProdID,
                ProdOrgName = g.ElementAt(0).ProdOrgName,
                BarCodeType = g.ElementAt(0).BarCodeType,

                OrderDocID = g.ElementAt(0).OrderDocID,
                DataAddTime = g.ElementAt(0).DataAddTime,
                ArrivalTime = g.ElementAt(0).ArrivalTime,
                Memo = g.ElementAt(0).Memo,
                GoodSName = g.ElementAt(0).GoodSName
            }).OrderBy(p => p.DeptID).ThenBy(p => p.CompanyName).ThenBy(p => p.CenOrgGoodsNo).ThenBy(p => p.GoodsUnit).ToList();
        }
        /// <summary>
        /// 订单号汇总
        /// 供应商+订货单号+订单时间
        /// </summary>
        /// <param name="outDtlList"></param>
        /// <returns></returns>
        private IList<ReaBmsCenOrderDtl> SearchReaBmsCenOrderDtlListOfGroupBy3(IList<ReaBmsCenOrderDtl> outDtlList)
        {
            return outDtlList.GroupBy(p => new
            {
                p.ReaCompID,
                p.OrderDocNo,
                p.DataAddTime
            }).Select(g => new ReaBmsCenOrderDtl
            {
                Id = g.ElementAt(0).Id,
                LabID = g.ElementAt(0).LabID,
                //DeptID = g.ElementAt(0).DeptID,
                //DeptName = g.ElementAt(0).DeptName,
                ReaCompID = g.ElementAt(0).ReaCompID,
                CompanyName = g.ElementAt(0).CompanyName,

                DataAddTime = g.ElementAt(0).DataAddTime,
                OrderDocNo = g.ElementAt(0).OrderDocNo,
                SumTotal = g.Sum(k => k.SumTotal)

            }).ToList();
        }
        public Stream SearchBusinessSummaryReportOfExcelByHql(long labID, string labCName, int groupType, string docHql, string dtlHql, string reaGoodsHql, string sort, string breportType, string frx, ref string fileName, string startDate, string endDate)
        {
            if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取统计条件为空!");
            }
            Stream stream = null;
            IList<ReaBmsCenOrderDtl> dtlList = ((IDReaBmsCenOrderDtlDao)base.DBDao).SearchReaBmsCenOrderDtlSummaryByHQL(docHql, dtlHql, reaGoodsHql, sort, -1, -1);

            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取订单汇总明细信息为空!");
            }
            
            if (groupType.ToString() == ReaBmsCenOrderDtlStatisticalType.订单明细汇总.Key)
            {
                dtlList = SearchReaBmsCenOrderDtlListOfGroupBy1(dtlList);
            }
            else if (groupType.ToString() == ReaBmsCenOrderDtlStatisticalType.订单明细.Key)
            {
                //不需要合并，直接返回dtlList
            }
            else if (groupType.ToString() == ReaBmsCenOrderDtlStatisticalType.订单号汇总.Key)
            {
                dtlList = SearchReaBmsCenOrderDtlListOfGroupBy3(dtlList);
            }

            //获取订单汇总模板
            if (string.IsNullOrEmpty(frx))
                frx = "订单汇总.xlsx";
            string saveFullPath = "";
            string fileExt = frx.Substring(frx.LastIndexOf("."));
            string excelFile = frx;
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand(startDate, endDate);
            stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsCenOrderDoc, ReaBmsCenOrderDtl>(null, dtlList, excelCommand, breportType, labID, frx, excelFile, ref saveFullPath);
            fileName = "订单汇总信息" + fileExt;
            return stream;
        }
        public Stream SearchBusinessSummaryReportOfPdfByHql(string reaReportClass, long labId, string labCName, int groupType, string docHql, string dtlHql, string reaGoodsHql, string sort, string breportType, string frx, ref string pdfFileName, string startDate, string endDate)
        {
            Stream stream = null;
            if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取统计条件为空!");
            }
            IList<ReaBmsCenOrderDtl> dtlList = ((IDReaBmsCenOrderDtlDao)base.DBDao).SearchReaBmsCenOrderDtlSummaryByHQL(docHql, dtlHql, reaGoodsHql, sort, -1, -1);

            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取订单汇总明细信息为空!");
            }
            //dtlList = SearchReaBmsCenOrderDtlListOfGroupBy1(dtlList);

            if (groupType.ToString() == ReaBmsCenOrderDtlStatisticalType.订单明细汇总.Key)
            {
                dtlList = SearchReaBmsCenOrderDtlListOfGroupBy1(dtlList);
            }
            else if (groupType.ToString() == ReaBmsCenOrderDtlStatisticalType.订单明细.Key)
            {
                //不需要合并，直接返回dtlList
            }
            else if (groupType.ToString() == ReaBmsCenOrderDtlStatisticalType.订单号汇总.Key)
            {
                dtlList = SearchReaBmsCenOrderDtlListOfGroupBy3(dtlList);
            }

            //pdfFileName = "订单汇总.pdf";
            pdfFileName = "订单汇总_" + DateTime.Now.ToString("yyMMddHHmmss") + ".pdf";
            if (reaReportClass == ReaReportClass.Frx.Key)
            {
                stream = SearchPdfReportOfFrxByHql(labId, labCName, pdfFileName, dtlList, frx, startDate, endDate);
            }
            else if (reaReportClass == ReaReportClass.Excel.Key)
            {
                string excelFileFullDir = "";
                //获取订货单模板
                if (string.IsNullOrEmpty(frx))
                    frx = "订单汇总.xlsx";
                string fileExt = frx.Substring(frx.LastIndexOf("."));
                string excelFile = fileExt;
                Stream stream2 = null;
                ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand(startDate, endDate);

                stream2 = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsCenOrderDoc, ReaBmsCenOrderDtl>(null, dtlList, excelCommand, breportType, labId, frx, excelFile, ref excelFileFullDir);
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
        private Stream SearchPdfReportOfFrxByHql(long labId, string labCName, string pdfFileName, IList<ReaBmsCenOrderDtl> dtlList, string frx, string startDate, string endDate)
        {
            Stream stream = null;
            DataSet dataSet = new DataSet();
            dataSet.DataSetName = "ZhiFang.ReagentSys.Client";
            DataTable dtDtl = ReportBTemplateHelp.ToDataTable<ReaBmsCenOrderDtl>(dtlList, null);
            if (dtDtl != null)
            {
                dtDtl.TableName = "Rea_BmsCenOrderDtl";
                dataSet.Tables.Add(dtDtl);
            }
            //如果当前实验室还没有维护订单汇总报表模板,默认使用公共的订单汇总模板
            if (string.IsNullOrEmpty(frx))
                frx = "订单汇总.frx";
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand(startDate, endDate);
            ExcelReportHelp.CreateEECDataTable(excelCommand, ref dataSet);
            stream = FrxToPdfReportHelp.SavePdfReport(dataSet, labId, pdfFileName, FrxToPdfReportHelp.PublicTemplateDir, BTemplateType.GetStatusDic()[BTemplateType.订单汇总.Key].Name, frx, false);
            return stream;
        }
        #endregion

        /// <summary>
        /// 根据订货单ID查询订单明细
        /// </summary>
        /// <param name="orderDocId">订货总单ID</param>
        /// <returns></returns>
        public IList<ReaBmsCenOrderDtl> GetReaBmsCenOrderDtlListByDocId(long orderDocId)
        {
            return ((IDReaBmsCenOrderDtlDao)base.DBDao).GetReaBmsCenOrderDtlListByDocId(orderDocId);
        }

        public EntityList<ReaBmsCenOrderDtl> GetReaBmsCenOrderDtlListByHQL(string strHqlWhere, string Order, int start, int count, long orderDocId)
        {
            EntityList<ReaBmsCenOrderDtl> entityList = ((IDReaBmsCenOrderDtlDao)base.DBDao).GetReaBmsCenOrderDtlListByHQL(strHqlWhere, Order, start, count);
            if (orderDocId > 0)
            {
                //根据订单主键ID，获取验收单
                IList<ReaBmsCenSaleDocConfirm> docConfirmList = IDReaBmsCenSaleDocConfirmDao.GetListByHQL(string.Format("OrderDocID={0}", orderDocId));
                if (docConfirmList.Count == 0)
                {
                    return entityList;
                }
                //获取验收单所有的验收明细单(Status=3已入库的数据)
                string saleDocConfirmIDs = string.Join(",", docConfirmList.Select(p => p.Id).ToArray());
                IList<ReaBmsCenSaleDtlConfirm> allDtlConfirmList = IDReaBmsCenSaleDtlConfirmDao.GetListByHQL(string.Format("SaleDocConfirmID in ({0}) and Status in (2,3)", saleDocConfirmIDs));
                if (allDtlConfirmList.Count == 0)
                {
                    return entityList;
                }

                //遍历订单明细，给字段赋值
                foreach (var orderDtl in entityList.list)
                {
                    var tmpList = allDtlConfirmList.Where(p => p.ReaGoodsID == orderDtl.ReaGoodsID).ToList();
                    if (tmpList.Count == 0)
                    {
                        orderDtl.InStorageQty = 0;
                        orderDtl.NotInStorageQty = orderDtl.GoodsQty.Value;
                    }
                    else
                    {
                        orderDtl.InStorageQty = tmpList.Sum(p => p.InCount);//已入库数
                        orderDtl.NotInStorageQty = (orderDtl.GoodsQty.Value - orderDtl.InStorageQty);//未入库数=审批数-已入库数
                    }
                }

            }
            return entityList;
        }
    }
}