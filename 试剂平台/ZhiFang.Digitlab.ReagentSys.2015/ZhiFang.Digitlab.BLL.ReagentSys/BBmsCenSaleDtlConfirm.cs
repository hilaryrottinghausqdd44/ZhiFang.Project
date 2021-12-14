using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IBLL.ReagentSys;
using ZhiFang.Digitlab.IDAO.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.Response;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaConfirm;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaGoodsScanCode;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BBmsCenSaleDtlConfirm : BaseBLL<BmsCenSaleDtlConfirm>, ZhiFang.Digitlab.IBLL.ReagentSys.IBBmsCenSaleDtlConfirm
    {
        IDAO.ReagentSys.IDBmsCenOrderDtlDao IDBmsCenOrderDtlDao { get; set; }
        IDReaBmsInDtlDao IDReaBmsInDtlDao { get; set; }
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IBReaGoodsBarcodeOperation IBReaGoodsBarcodeOperation { get; set; }
        IBBmsCenOrderDoc IBBmsCenOrderDoc { get; set; }

        #region 客户端验收处理
        private void GetReaBmsCenSaleDtlConfirmLinkVO(long dtlConfirmID, BmsCenSaleDtlConfirmVO vo, IList<ReaGoodsBarcodeOperation> inDtlLink)
        {
            IList<ReaGoodsBarcodeOperation> dtlBarCodeList = IBReaGoodsBarcodeOperation.SearchListByHQL(string.Format("reagoodsbarcodeoperation.BDtlID={0} and (reagoodsbarcodeoperation.OperTypeID={1} or reagoodsbarcodeoperation.OperTypeID={2})", dtlConfirmID, ReaGoodsBarcodeOperType.验货接收.Key, ReaGoodsBarcodeOperType.验货拒收.Key));
            if (dtlBarCodeList != null && dtlBarCodeList.Count > 0)
            {
                //按使用盒条码进行分组
                var serialGroupBy = dtlBarCodeList.GroupBy(p => p.UsePackSerial);
                //获取每一盒条码(供货验收或供货拒收)的最后操作记录
                Dictionary<string, ReaGoodsBarcodeOperation> tempDictionary = new Dictionary<string, ReaGoodsBarcodeOperation>();
                foreach (var item in serialGroupBy)
                {
                    var tempList = item.OrderByDescending(p => p.DataAddTime).ToList();
                    if (!tempDictionary.Keys.Contains(item.Key))
                        tempDictionary.Add(item.Key, tempList[0]);
                }

                IList<ReaGoodsBarcodeOperationVO> dtVOList = new List<ReaGoodsBarcodeOperationVO>();
                foreach (var item in tempDictionary)
                {
                    var operation = item.Value;
                    ReaGoodsBarcodeOperationVO operationVO = new ReaGoodsBarcodeOperationVO();

                    operationVO.Id = item.Value.Id;
                    operationVO.BDocNo = item.Value.BDocNo;
                    operationVO.BDocID = item.Value.BDocID;
                    operationVO.BDtlID = item.Value.BDtlID;
                    operationVO.OperTypeID = item.Value.OperTypeID;
                    //签收标志：2接收、3拒收
                    operationVO.ReceiveFlag = operationVO.OperTypeID.Value;

                    operationVO.SysPackSerial = item.Value.SysPackSerial;
                    operationVO.OtherPackSerial = item.Value.OtherPackSerial;
                    operationVO.UsePackSerial = item.Value.UsePackSerial;
                    operationVO.LotNo = item.Value.LotNo;

                    //if (inDtlLink != null)
                    //{
                    //    var tempList = inDtlLink.Where(p => p.UsePackSerial == item.Value.UsePackSerial).ToList();
                    //    operationVO.IsStoreIn = (tempList.Count > 0 ? true : false);
                    //}
                    dtVOList.Add(operationVO);
                }
                ServiceCommon.ParseObjectProperty tempParseObjectProperty = new ServiceCommon.ParseObjectProperty();
                vo.ReaBmsCenSaleDtlConfirmLinkVOListStr = tempParseObjectProperty.GetObjectPropertyNoPlanish(dtVOList);
            }
        }
        /// <summary>
        /// (验收)获取客户端验收明细及验收条码明细信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public EntityList<BmsCenSaleDtlConfirmVO> SearchBmsCenSaleDtlConfirmVOOfConfirmByHQL(string strHqlWhere, string order, int page, int limit)
        {
            EntityList<BmsCenSaleDtlConfirmVO> entityVOList = new EntityList<BmsCenSaleDtlConfirmVO>();
            entityVOList.list = new List<BmsCenSaleDtlConfirmVO>();
            EntityList<BmsCenSaleDtlConfirm> el = this.SearchListByHQL(strHqlWhere, order, page, limit);

            IList<BmsCenSaleDtlConfirmVO> tempList = new List<BmsCenSaleDtlConfirmVO>();
            foreach (var model in el.list)
            {
                BmsCenSaleDtlConfirmVO vo = new BmsCenSaleDtlConfirmVO();
                vo.BmsCenSaleDtlConfirm = model;
                ReaGoods reaGoods = IDReaGoodsDao.Get(model.ReaGoodsID.Value);
                vo.ReaGoodsSName = reaGoods.SName;
                vo.ReaGoodsEName = reaGoods.EName;

                GetReaBmsCenSaleDtlConfirmLinkVO(model.Id, vo, null);
                tempList.Add(vo);
            }
            entityVOList.count = tempList.Count;
            entityVOList.list = tempList;
            return entityVOList;
        }

        /// <summary>
        /// 客户端订单验收(过滤已验收的验收货品明细)
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="confirmType">订单验收:order;供货单验收:sale</param>
        /// <returns></returns>
        public EntityList<BmsCenSaleDtlConfirmVO> SearchBmsCenSaleDtlConfirmVOOfOrderByHQL(string strHqlWhere, string order, int page, int limit, string confirmType)
        {
            EntityList<BmsCenSaleDtlConfirmVO> entityVOList = new EntityList<BmsCenSaleDtlConfirmVO>();
            entityVOList.list = new List<BmsCenSaleDtlConfirmVO>();

            if (!string.IsNullOrEmpty(strHqlWhere))
            {
                strHqlWhere = strHqlWhere.Replace("bmscensaledtlconfirmvo.", "");
                strHqlWhere = strHqlWhere.Replace("BmsCenSaleDtlConfirm", "bmscensaledtlconfirm");
            }

            if (!string.IsNullOrEmpty(order))
            {
                order = order.Replace("bmscensaledtlconfirmvo.", "");
                order = order.Replace("BmsCenSaleDtlConfirm", "bmscensaledtlconfirm");
            }
            EntityList<BmsCenSaleDtlConfirm> el = this.SearchListByHQL(strHqlWhere, order, -1, -1);

            IList<BmsCenSaleDtlConfirmVO> tempList = new List<BmsCenSaleDtlConfirmVO>();
            foreach (var model in el.list)
            {
                BmsCenSaleDtlConfirmVO vo = new BmsCenSaleDtlConfirmVO();
                vo.BmsCenSaleDtlConfirm = model;
                ReaGoods reaGoods = IDReaGoodsDao.Get(model.ReaGoodsID.Value);
                vo.ReaGoodsSName = reaGoods.SName;
                vo.ReaGoodsEName = reaGoods.EName;
                switch (confirmType)
                {
                    case "order"://订单验收
                        changeBmsCenSaleDtlConfirmVOOfOrderDtl(el.list, vo);
                        break;
                    case "sale"://供货单验收
                        //changeBmsCenSaleDtlConfirmVOOfOrderSale(el.list, vo);
                        break;
                    default:
                        changeBmsCenSaleDtlConfirmVO(el.list, vo);
                        break;
                }
                GetReaBmsCenSaleDtlConfirmLinkVO(model.Id, vo, null);
                tempList.Add(vo);
            }
            entityVOList.count = tempList.Count;
            //过滤已验收的验收明细(可验收数大于0)
            tempList = tempList.Where(p => p.ConfirmCount > 0).ToList();

            //分页处理
            if (limit > 0 && limit < tempList.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = tempList.Skip(startIndex).Take(endIndex);
                if (list != null)
                {
                    tempList = list.ToList();
                }
            }

            entityVOList.list = tempList;
            return entityVOList;
        }
        private void changeBmsCenSaleDtlConfirmVO(IList<BmsCenSaleDtlConfirm> dtlConfirmList, BmsCenSaleDtlConfirmVO vo)
        {
            if (dtlConfirmList != null)
            {
                var dtlList = dtlConfirmList.Where(p => p.Id == vo.Id).ToList();
                if (dtlList != null && dtlList.Count() > 0)
                {
                    vo.BmsCenSaleDtlConfirm.AcceptCount = dtlList.Where(p => p.Status.Value != int.Parse(BmsCenSaleDtlConfirmStatus.待继续验收.Key)).Sum(p => p.AcceptCount);
                    vo.BmsCenSaleDtlConfirm.RefuseCount = dtlList.Where(p => p.Status.Value != int.Parse(BmsCenSaleDtlConfirmStatus.待继续验收.Key)).Sum(p => p.RefuseCount);
                }
                var goodsQty = vo.BmsCenSaleDtlConfirm.AcceptCount + vo.BmsCenSaleDtlConfirm.RefuseCount;
                vo.ConfirmCount = vo.BmsCenSaleDtlConfirm.GoodsQty - goodsQty;
                if (vo.BmsCenSaleDtlConfirm.Status == int.Parse(BmsCenSaleDtlConfirmStatus.待继续验收.Key))
                {
                    vo.ConfirmCount = vo.BmsCenSaleDtlConfirm.GoodsQty;
                }
                if (vo.ConfirmCount < 0) vo.ConfirmCount = 0;
            }
        }
        private void changeBmsCenSaleDtlConfirmVOOfOrderDtl(IList<BmsCenSaleDtlConfirm> dtlConfirmList, BmsCenSaleDtlConfirmVO vo)
        {
            if (dtlConfirmList != null)
            {
                var dtlConfirmList2 = dtlConfirmList.Where(p => p.BmsCenOrderDtl.Id == vo.BmsCenSaleDtlConfirm.BmsCenOrderDtl.Id).ToList();
                //已接收总数及已拒收总数处理
                if (dtlConfirmList2 != null && dtlConfirmList2.Count() > 0)
                {
                    vo.ReceivedCount = dtlConfirmList2.Where(p => p.Status.Value != int.Parse(BmsCenSaleDtlConfirmStatus.待继续验收.Key)).Sum(p => p.AcceptCount);
                    vo.RejectedCount = dtlConfirmList2.Where(p => p.Status.Value != int.Parse(BmsCenSaleDtlConfirmStatus.待继续验收.Key)).Sum(p => p.RefuseCount);
                }

                vo.OrderGoodsQty = dtlConfirmList2[0].BmsCenOrderDtl.GoodsQty;
                var goodsQty = vo.ReceivedCount + vo.RejectedCount;
                //某一订单货品明细的可验收数=该订单货品明细的购进数-(该订单货品明细的已接收总数+该订单货品明细的已拒收总数)
                vo.ConfirmCount = vo.OrderGoodsQty - goodsQty;
                if (vo.BmsCenSaleDtlConfirm.Status == int.Parse(BmsCenSaleDtlConfirmStatus.待继续验收.Key))
                {
                    vo.ConfirmCount = vo.BmsCenSaleDtlConfirm.GoodsQty;
                }
                if (vo.ConfirmCount < 0) vo.ConfirmCount = 0;
            }
        }
        private void changeBmsCenSaleDtlConfirmVOOfOrderSale(IList<BmsCenSaleDtlConfirm> dtlConfirmList, BmsCenSaleDtlConfirmVO vo)
        {
            if (dtlConfirmList != null)
            {
                var dtlList = dtlConfirmList.Where(p => p.BmsCenSaleDtl.Id == vo.BmsCenSaleDtlConfirm.BmsCenSaleDtl.Id).ToList();
                if (dtlList != null && dtlList.Count() > 0)
                {
                    vo.BmsCenSaleDtlConfirm.AcceptCount = dtlList.Where(p => p.Status.Value != int.Parse(BmsCenSaleDocConfirmStatus.待继续验收.Key)).Sum(p => p.AcceptCount);
                    vo.BmsCenSaleDtlConfirm.RefuseCount = dtlList.Where(p => p.Status.Value != int.Parse(BmsCenSaleDocConfirmStatus.待继续验收.Key)).Sum(p => p.RefuseCount);
                }
                vo.OrderGoodsQty = dtlList[0].BmsCenOrderDtl.GoodsQty;
                var goodsQty = vo.BmsCenSaleDtlConfirm.AcceptCount + vo.BmsCenSaleDtlConfirm.RefuseCount;
                vo.ConfirmCount = vo.OrderGoodsQty - goodsQty;
                if (vo.BmsCenSaleDtlConfirm.Status == int.Parse(BmsCenSaleDtlConfirmStatus.待继续验收.Key))
                {
                    vo.ConfirmCount = vo.BmsCenSaleDtlConfirm.GoodsQty;
                }
                if (vo.ConfirmCount < 0) vo.ConfirmCount = 0;
            }
        }
        public BaseResultBool AddDtlConfirmOfList(BmsCenSaleDocConfirm entity, IList<BmsCenSaleDtlConfirmVO> dtAddList, string codeScanningMode, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.success = true;
            tempBaseResultBool = EditBmsCenSaleDtlConfirmValid(dtAddList, codeScanningMode);
            if (tempBaseResultBool.success == false) return tempBaseResultBool;

            foreach (BmsCenSaleDtlConfirmVO vo in dtAddList)
            {
                BmsCenSaleDtlConfirm saleDtlConfirm = vo.BmsCenSaleDtlConfirm;
                saleDtlConfirm.SaleDocConfirmNo = entity.SaleDocConfirmNo;
                if (string.IsNullOrEmpty(saleDtlConfirm.SaleDtlConfirmNo))
                    saleDtlConfirm.SaleDtlConfirmNo = this.GetReqDocNo();
                saleDtlConfirm.BmsCenSaleDocConfirm = entity;
                if (saleDtlConfirm.BmsCenSaleDocConfirm.DataTimeStamp == null)
                {
                    byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
                    saleDtlConfirm.BmsCenSaleDocConfirm.DataTimeStamp = dataTimeStamp;
                }
                if (!saleDtlConfirm.Status.HasValue)
                    saleDtlConfirm.Status = int.Parse(BmsCenSaleDtlConfirmStatus.待继续验收.Key);
                saleDtlConfirm.StatusName = BmsCenSaleDtlConfirmStatus.GetStatusDic()[saleDtlConfirm.Status.ToString()].Name;

                if (tempBaseResultBool.success == true)
                {
                    if (saleDtlConfirm.RefuseCount > 0)
                        entity.IsAcceptError = true;
                    this.Entity = saleDtlConfirm;
                    if (tempBaseResultBool.success == true)
                    {
                        tempBaseResultBool.success = this.Add();
                        //如果货品的条码类型为盒条码,需要添加货品的条码明细关系信息
                        if (vo.BmsCenSaleDtlConfirm.BarCodeMgr == int.Parse(ReaGoodsBarCodeMgr.盒条码.Key))
                        {
                            tempBaseResultBool = AddBarcodeOperationOfList(saleDtlConfirm, vo.ReaGoodsBarcodeOperationList, empID, empName);
                        }
                    }
                    if (tempBaseResultBool.success == false)
                        tempBaseResultBool.ErrorInfo = "新增验收明细单失败！";
                }
                else
                {
                    break;
                }
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditDtlConfirmOfList(BmsCenSaleDocConfirm entity, IList<BmsCenSaleDtlConfirmVO> dtEditList, string fieldsDtl, string codeScanningMode, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.success = true;
            tempBaseResultBool = EditBmsCenSaleDtlConfirmValid(dtEditList, codeScanningMode);
            if (tempBaseResultBool.success == false) return tempBaseResultBool;

            foreach (BmsCenSaleDtlConfirmVO vo in dtEditList)
            {
                BmsCenSaleDtlConfirm saleDtlConfirm = vo.BmsCenSaleDtlConfirm;
                saleDtlConfirm.BmsCenSaleDocConfirm = entity;
                if (saleDtlConfirm.BmsCenSaleDocConfirm.DataTimeStamp == null)
                {
                    byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
                    saleDtlConfirm.BmsCenSaleDocConfirm.DataTimeStamp = dataTimeStamp;
                }
                if (!saleDtlConfirm.Status.HasValue)
                    saleDtlConfirm.Status = int.Parse(BmsCenSaleDtlConfirmStatus.待继续验收.Key);
                saleDtlConfirm.StatusName = BmsCenSaleDtlConfirmStatus.GetStatusDic()[saleDtlConfirm.Status.ToString()].Name;

                if (tempBaseResultBool.success == true)
                {
                    if (saleDtlConfirm.RefuseCount > 0)
                        entity.IsAcceptError = true;
                    this.Entity = saleDtlConfirm;
                    if (tempBaseResultBool.success == true)
                    {
                        this.Entity = saleDtlConfirm;
                        string[] tempDtlArray = ServiceCommon.CommonServiceMethod.GetUpdateFieldValueStr(this.Entity, fieldsDtl);
                        tempBaseResultBool.success = this.Update(tempDtlArray);
                        //如果货品的条码类型为盒条码,需要添加货品的条码明细关系信息
                        if (vo.BmsCenSaleDtlConfirm.BarCodeMgr == int.Parse(ReaGoodsBarCodeMgr.盒条码.Key))
                        {
                            tempBaseResultBool = AddBarcodeOperationOfList(saleDtlConfirm, vo.ReaGoodsBarcodeOperationList, empID, empName);
                        }
                    }
                    if (tempBaseResultBool.success == false)
                        tempBaseResultBool.ErrorInfo = "新增验收明细单失败！";
                }
                else
                {
                    break;
                }
            }
            return tempBaseResultBool;
        }
        public BaseResultBool AddBarcodeOperationOfList(BmsCenSaleDtlConfirm model, IList<ReaGoodsBarcodeOperation> dtAddList, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (dtAddList == null || dtAddList.Count <= 0) return tempBaseResultBool;

            foreach (ReaGoodsBarcodeOperation operation in dtAddList)
            {
                operation.BDocID = model.BmsCenSaleDocConfirm.Id;
                operation.BDocNo = model.SaleDocConfirmNo;//BmsCenSaleDocConfirm.SaleDocConfirmNo;
                operation.BDtlID = model.Id;
                operation.ReaCompanyID = model.BmsCenSaleDocConfirm.ReaCompID;
                operation.CompanyName = model.BmsCenSaleDocConfirm.ReaCompName;
                operation.LotNo = model.LotNo;
                operation.GoodsID = model.ReaGoodsID;
                operation.GoodsCName = model.ReaGoodsName;
                operation.GoodsUnit = model.GoodsUnit;
                operation.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[operation.OperTypeID.ToString()].Name;
            }
            tempBaseResultBool = IBReaGoodsBarcodeOperation.AddBarcodeOperationOfList(dtAddList, 0, empID, empName);
            return tempBaseResultBool;
        }
        /// <summary>
        /// 验货明细的保存验证判断
        /// </summary>
        /// <param name="voDtlList"></param>
        /// <param name="codeScanningMode">扫码模式(严格模式:strict,混合模式：mixing)</param>
        /// <returns></returns>
        public BaseResultBool EditBmsCenSaleDtlConfirmValid(IList<BmsCenSaleDtlConfirmVO> voDtlList, string codeScanningMode)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            foreach (var vo in voDtlList)
            {
                BmsCenSaleDtlConfirm model = vo.BmsCenSaleDtlConfirm;
                #region 数量判断比较处理
                if (model.GoodsQty <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("试剂【{0}】的购进数为零，不能验收！", model.ReaGoodsName);
                }
                else if (string.IsNullOrEmpty(model.LotNo))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("试剂【{0}】的批号信息为空，不能验收！", model.ReaGoodsName);
                }
                else if (model.AcceptCount <= 0 && model.RefuseCount <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("试剂【{ 0}】的验收数量和拒收数量都为零，不能验收！", model.ReaGoodsName);
                }
                else if (model.GoodsQty < model.AcceptCount)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("试剂【{0}】的验收数量大于总量，不能验收！", model.ReaGoodsName);
                }
                else if (model.GoodsQty < model.RefuseCount)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("试剂【{0}】的拒收数量大于总量，不能验收！", model.ReaGoodsName);
                }
                else if (model.GoodsQty < (model.AcceptCount + model.RefuseCount))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("试剂【{0}】的验收和拒收数量大于总量，不能验收！", model.ReaGoodsName);
                }
                #endregion
                else if (vo.BmsCenSaleDtlConfirm.BarCodeMgr == int.Parse(ReaGoodsBarCodeMgr.盒条码.Key))
                {
                    if (vo.ReaGoodsBarcodeOperationList != null && vo.ReaGoodsBarcodeOperationList.Count() > 0)
                    {
                        var acceptCountList = vo.ReaGoodsBarcodeOperationList.Where(p => p.OperTypeID == int.Parse(ReaGoodsBarcodeOperType.验货接收.Key));
                        var refuseCountList = vo.ReaGoodsBarcodeOperationList.Where(p => p.OperTypeID == int.Parse(ReaGoodsBarcodeOperType.验货拒收.Key));
                        if (model.AcceptCount < acceptCountList.Count())
                        {
                            tempBaseResultBool.success = false;
                            tempBaseResultBool.ErrorInfo = string.Format("试剂【{0}】的接收数{1}小于扫码接收操作记录数{2},下不能验收！", model.ReaGoodsName, model.AcceptCount, acceptCountList.Count());
                        }
                        else if (model.RefuseCount < refuseCountList.Count())
                        {
                            tempBaseResultBool.success = false;
                            tempBaseResultBool.ErrorInfo = string.Format("试剂【{0}】的拒收数{1}小于扫码拒收操作记录数{2},下不能验收！", model.ReaGoodsName, model.AcceptCount, refuseCountList.Count());
                        }
                    }
                    else if (codeScanningMode == "strict")
                    {
                        if (vo.ReaGoodsBarcodeOperationList == null || vo.ReaGoodsBarcodeOperationList.Count() <= 0)
                        {
                            tempBaseResultBool.success = false;
                            tempBaseResultBool.ErrorInfo = string.Format("试剂【{0}】的扫码操作记录为空,在严格扫码模式下不能验收！", model.ReaGoodsName);
                        }
                    }
                }
                if (tempBaseResultBool.success == false) break;
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 订单验收保存前验证处理
        /// </summary>
        /// <param name="saleDtlConfirm"></param>
        /// <returns></returns>
        public BaseResultBool EditBmsCenSaleDtlConfirmValidOfOrder(IList<BmsCenSaleDtlConfirmVO> voDtlList, string codeScanningMode)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            if (voDtlList == null || voDtlList.Count == 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：验收货品明细为空！";
                return tempBaseResultBool;
            }
            tempBaseResultBool = EditBmsCenSaleDtlConfirmValid(voDtlList, codeScanningMode);
            if (tempBaseResultBool.success == false) return tempBaseResultBool;

            foreach (BmsCenSaleDtlConfirmVO vo in voDtlList)
            {
                BmsCenSaleDtlConfirm saleDtlConfirm = vo.BmsCenSaleDtlConfirm;
                if (saleDtlConfirm.BmsCenOrderDtl == null)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("试剂【{0}】的关联订单明细信息为空！", saleDtlConfirm.ReaGoodsName);
                }
                else if (tempBaseResultBool.success == true && saleDtlConfirm.BmsCenOrderDoc == null)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("试剂【{0}】的关联订单信息为空！", saleDtlConfirm.ReaGoodsName);
                }
                //同一订单明细的本次购进总数+同一订单明细的已购进的总数与订单明细的购进数比较
                if (tempBaseResultBool.success == true)
                {
                    //同一订单明细的本次购进总数
                    float sumCurGoodsQty = voDtlList.Where(p => p.BmsCenSaleDtlConfirm.BmsCenOrderDtl.Id == saleDtlConfirm.BmsCenOrderDtl.Id).Sum(p => p.BmsCenSaleDtlConfirm.GoodsQty);
                    //同一订单明细的已验收数量
                    IList<BmsCenSaleDtlConfirm> dtlConfirmList = this.SearchListByHQL(string.Format("bmscensaledtlconfirm.BmsCenOrderDtl.Id={0} and bmscensaledtlconfirm.Status!={1}", saleDtlConfirm.BmsCenOrderDtl.Id, int.Parse(BmsCenSaleDtlConfirmStatus.待继续验收.Key)));
                    float sumCurGoodsQty2 = dtlConfirmList.Sum(p => p.GoodsQty);
                    //订单明细的购进数
                    BmsCenOrderDtl orderDtl = IDBmsCenOrderDtlDao.Get(saleDtlConfirm.BmsCenOrderDtl.Id);
                    if (orderDtl != null && (sumCurGoodsQty + sumCurGoodsQty2) > orderDtl.GoodsQty)
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = string.Format("试剂为【{0}】,同一订单明细的本次购进总数+已购进的总数({1})大于订单购进数({2})", saleDtlConfirm.ReaGoodsName, (sumCurGoodsQty + sumCurGoodsQty2), orderDtl.GoodsQty); ;
                    }
                }
                if (tempBaseResultBool.success == false)
                    break;
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 获取验收明细单号
        /// </summary>
        /// <returns></returns>
        private string GetReqDocNo()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));
            Random ran = new Random();
            int randKey = ran.Next(0, 999);
            strb.Append(randKey.ToString().PadLeft(3, '0'));//左补零
            strb.Append(DateTime.Now.ToString("HHmmssfff"));
            return strb.ToString();
        }
        public BaseResultBool DelReaBmsCenSaleDtlConfirm(long id, string confirmSourceType, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            this.Entity = this.Get(id);
            if (this.Entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = string.Format("不存在验收明细ID为{0}的数据!", id);
                return tempBaseResultBool;
            }
            switch (confirmSourceType)
            {
                case "order"://订单验收
                    BmsCenOrderDoc model = this.Entity.BmsCenOrderDoc;
                    if (this.Entity.BmsCenOrderDoc == null)
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = string.Format("验收明细ID为{0},获取对应的订单信息为空,不能删除!", id);
                        return tempBaseResultBool;
                    }
                    model.Status = int.Parse(ReaBmsOrderDocStatus.部分验收.Key);
                    tempBaseResultBool = EditReaBmsCenOrderDoc(model, empID, empName);
                    break;
                case "sale"://供货验收
                    break;
                default:
                    break;
            }
            if (tempBaseResultBool.success == true)
                tempBaseResultBool.success = this.Remove();
            if (tempBaseResultBool.success == false)
                tempBaseResultBool.ErrorInfo = string.Format("验收明细ID为{0},删除失败!", id);
            return tempBaseResultBool;
        }
        private BaseResultBool EditReaBmsCenOrderDoc(BmsCenOrderDoc entity, long empID, string empName)
        {
            entity.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
            List<string> tmpa = new List<string>();
            tmpa.Add("Id=" + entity.Id + " ");
            tmpa.Add("Status=" + entity.Status + " ");
            tmpa.Add("StatusName='" + entity.StatusName + "' ");
            BaseResultBool tempBaseResultBool = IBBmsCenOrderDoc.EditReaBmsCenOrderDocAndDt(entity, tmpa.ToArray(), null, null, empID, empName);
            return tempBaseResultBool;
        }
        /// <summary>
        /// 判断订单是否可以新增验收或继续验收
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public BaseResultBool SearchOrderIsConfirmOfByOrderId(long orderId)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                IList<BmsCenSaleDtlConfirm> dtlConfirmList = this.SearchListByHQL(String.Format("bmscensaledtlconfirm.BmsCenOrderDoc.Id={0} and bmscensaledtlconfirm.Status={1}", orderId, int.Parse(BmsCenSaleDtlConfirmStatus.待继续验收.Key)));
                if (dtlConfirmList != null && dtlConfirmList.Count > 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "订单Id为：" + orderId + "已存在验收暂存的信息,请继续完成该订单验收暂存后再操作!";
                    return tempBaseResultBool;
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultBool;
        }
        #endregion
        /// <summary>
        /// (验收入库)获取客户端验收明细及验收条码明细信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public EntityList<BmsCenSaleDtlConfirmVO> SearchReaDtlConfirmVOOfStoreInByHQL(string strHqlWhere, string order, int page, int limit)
        {
            EntityList<BmsCenSaleDtlConfirmVO> entityVOList = new EntityList<BmsCenSaleDtlConfirmVO>();
            entityVOList.list = new List<BmsCenSaleDtlConfirmVO>();

            if (!string.IsNullOrEmpty(strHqlWhere))
            {
                strHqlWhere = strHqlWhere.Replace("bmscensaledtlconfirmvo.", "");
                strHqlWhere = strHqlWhere.Replace("BmsCenSaleDtlConfirm", "bmscensaledtlconfirm");
            }

            if (!string.IsNullOrEmpty(order))
            {
                order = order.Replace("bmscensaledtlconfirmvo.", "");
                order = order.Replace("BmsCenSaleDtlConfirm", "bmscensaledtlconfirm");
            }
            EntityList<BmsCenSaleDtlConfirm> el = this.SearchListByHQL(strHqlWhere, order, page, limit);

            IList<BmsCenSaleDtlConfirmVO> tempDtlConfirmList = new List<BmsCenSaleDtlConfirmVO>();

            foreach (var model in el.list)
            {
                BmsCenSaleDtlConfirmVO vo = new BmsCenSaleDtlConfirmVO();
                vo.BmsCenSaleDtlConfirm = model;
                ReaGoods reaGoods = IDReaGoodsDao.Get(model.ReaGoodsID.Value);
                vo.ReaGoodsSName = reaGoods.SName;
                vo.ReaGoodsEName = reaGoods.EName;

                IList<ReaBmsInDtl> tempReaBmsInDtlList = IDReaBmsInDtlDao.GetListByHQL(string.Format("reabmsindtl.SaleDtlConfirmID={0}", model.Id));
                //ServiceCommon.ParseObjectProperty tempParseObjectProperty = new ServiceCommon.ParseObjectProperty();
                //vo.ReaBmsInDtlListStr = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempReaBmsInDtlList);

                switch (model.BarCodeMgr)
                {
                    case 1:
                        IList<ReaGoodsBarcodeOperation> dtlBarCodeList = null;
                        StringBuilder inDtlStr = new StringBuilder();
                        foreach (var reaBmsInDtl in tempReaBmsInDtlList)
                        {
                            inDtlStr.Append(reaBmsInDtl.Id + ",");
                        }
                        if (!string.IsNullOrEmpty(inDtlStr.ToString()))
                        {
                            dtlBarCodeList = IBReaGoodsBarcodeOperation.SearchListByHQL(string.Format("reagoodsbarcodeoperation.BDtlID in ({0}) and reagoodsbarcodeoperation.OperTypeID={1}", inDtlStr.ToString().TrimEnd(','), ReaGoodsBarcodeOperType.验货入库.Key));
                            ServiceCommon.ParseObjectProperty tempParseObjectProperty2 = new ServiceCommon.ParseObjectProperty();
                            vo.ReaBmsInDtlLinkListStr = tempParseObjectProperty2.GetObjectPropertyNoPlanish(dtlBarCodeList);
                        }
                        GetReaBmsCenSaleDtlConfirmLinkVO(model.Id, vo, dtlBarCodeList);
                        break;
                    default:

                        break;
                }
                tempDtlConfirmList.Add(vo);
            }
            entityVOList.count = tempDtlConfirmList.Count;
            entityVOList.list = tempDtlConfirmList;
            return entityVOList;
        }

    }

}