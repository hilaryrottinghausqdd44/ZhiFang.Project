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

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsCenSaleDtlConfirm : BaseBLL<ReaBmsCenSaleDtlConfirm>, IBReaBmsCenSaleDtlConfirm
    {
        IDReaStorageDao IDReaStorageDao { get; set; }
        IDReaStorageGoodsLinkDao IDReaStorageGoodsLinkDao { get; set; }
        IDReaBmsCenSaleDocConfirmDao IDReaBmsCenSaleDocConfirmDao { get; set; }
        IDReaBmsInDtlDao IDReaBmsInDtlDao { get; set; }
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IDReaGoodsOrgLinkDao IDReaGoodsOrgLinkDao { get; set; }
        IBReaBmsCenOrderDtl IBReaBmsCenOrderDtl { get; set; }
        IBReaGoodsBarcodeOperation IBReaGoodsBarcodeOperation { get; set; }
        IBReaBmsCenSaleDtl IBReaBmsCenSaleDtl { get; set; }
        IBReaCheckInOperation IBReaCheckInOperation { get; set; }
        IBReaGoodsLot IBReaGoodsLot { get; set; }
        private void GetReaBmsCenSaleDtlConfirmLinkVO(long dtlConfirmID, ReaSaleDtlConfirmVO vo)
        {
            IList<ReaGoodsBarcodeOperation> dtlBarCodeList = new List<ReaGoodsBarcodeOperation>();
            string operationHql = string.Format("reagoodsbarcodeoperation.BDtlID={0} and (reagoodsbarcodeoperation.OperTypeID={1} or reagoodsbarcodeoperation.OperTypeID={2})", dtlConfirmID, ReaGoodsBarcodeOperType.验货接收.Key, ReaGoodsBarcodeOperType.验货拒收.Key);

            //某一供货验收明细的(已验货接收或已验货拒收)扫码记录=当前的验收明细扫码记录+同一供货单的其他已扫码记录(同一供货单条码可能拆分为几个验收单)
            if (vo.ReaBmsCenSaleDtlConfirm.SaleDtlID.HasValue)
            {
                ReaBmsCenSaleDtl saleDtl = IBReaBmsCenSaleDtl.Get(vo.ReaBmsCenSaleDtlConfirm.SaleDtlID.Value);
                //找出同一供货单的全部验收单信息(不包含当前验收单)
                IList<ReaBmsCenSaleDocConfirm> docConfirmList = IDReaBmsCenSaleDocConfirmDao.GetListByHQL(string.Format("reabmscensaledocconfirm.SaleDocID={0} and reabmscensaledocconfirm.Id!={1}", saleDtl.SaleDocID, vo.ReaBmsCenSaleDtlConfirm.SaleDocConfirmID));
                string bDocIDStr = "";
                foreach (var item in docConfirmList)
                {
                    bDocIDStr = bDocIDStr + (item.Id + ",");
                }
                if (!string.IsNullOrEmpty(bDocIDStr))
                    operationHql = string.Format("(reagoodsbarcodeoperation.BDtlID={0} or reagoodsbarcodeoperation.BDocID in ({1})) and (reagoodsbarcodeoperation.OperTypeID={2} or reagoodsbarcodeoperation.OperTypeID={3})", dtlConfirmID, bDocIDStr.TrimEnd(','), ReaGoodsBarcodeOperType.验货接收.Key, ReaGoodsBarcodeOperType.验货拒收.Key);
            }
            dtlBarCodeList = IBReaGoodsBarcodeOperation.SearchListByHQL(operationHql);
            IList<ReaGoodsBarcodeOperationVO> dtVOList = GetReaGoodsBarcodeOperationVO(dtlBarCodeList);
            ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
            vo.ReaBmsCenSaleDtlConfirmLinkVOListStr = tempParseObjectProperty.GetObjectPropertyNoPlanish(dtVOList);
        }
        private IList<ReaGoodsBarcodeOperationVO> GetReaGoodsBarcodeOperationVO(IList<ReaGoodsBarcodeOperation> dtlBarCodeList)
        {
            IList<ReaGoodsBarcodeOperationVO> dtVOList = new List<ReaGoodsBarcodeOperationVO>();
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
                    operationVO.ProdGoodsNo = item.Value.ProdGoodsNo;
                    operationVO.CenOrgGoodsNo = item.Value.CenOrgGoodsNo;
                    operationVO.GoodsNo = item.Value.GoodsNo;

                    operationVO.ReaCompCode = item.Value.ReaCompCode;
                    operationVO.GoodsSort = item.Value.GoodsSort;
                    operationVO.CompGoodsLinkID = item.Value.CompGoodsLinkID;
                    operationVO.BarCodeType = item.Value.BarCodeType;

                    dtVOList.Add(operationVO);
                }
            }
            return dtVOList;
        }
        public EntityList<ReaSaleDtlConfirmVO> SearchBmsCenSaleDtlConfirmVOOfConfirmTypeByHQL(string strHqlWhere, string order, int page, int limit, string confirmType)
        {
            EntityList<ReaSaleDtlConfirmVO> entityVOList = new EntityList<ReaSaleDtlConfirmVO>();
            entityVOList.list = new List<ReaSaleDtlConfirmVO>();
            EntityList<ReaBmsCenSaleDtlConfirm> el = this.SearchListByHQL(strHqlWhere, order, -1, -1);

            IList<ReaSaleDtlConfirmVO> tempList = new List<ReaSaleDtlConfirmVO>();
            foreach (var model in el.list)
            {
                ReaSaleDtlConfirmVO vo = new ReaSaleDtlConfirmVO();
                vo.ReaBmsCenSaleDtlConfirm = model;
                ReaGoods reaGoods = IDReaGoodsDao.Get(model.ReaGoodsID.Value);
                if (reaGoods != null)
                {
                    vo.ReaGoodsSName = reaGoods.SName;
                    vo.ReaGoodsEName = reaGoods.EName;
                    vo.GoodsSort = reaGoods.GoodsSort;
                    vo.ReaBmsCenSaleDtlConfirm.ProdOrgName = reaGoods.ProdOrgName;//厂商名称
                }
                switch (confirmType)
                {
                    case "reaorder"://订单验收
                        GetReaBmsCenSaleDtlConfirmVOOfOrderDtl(el.list, vo);
                        break;
                    case "reasale"://供货单验收                     
                        GetReaBmsCenSaleDtlConfirmVOOfSaleDtl(el.list, vo);
                        break;
                    default:
                        GetReaBmsCenSaleDtlConfirmVO(el.list, vo);
                        break;
                }
                if (vo.ReaBmsCenSaleDtlConfirm.BarCodeType == int.Parse(ReaGoodsBarCodeType.盒条码.Key))
                {
                    if (vo.ReaBmsCenSaleDtlConfirm.SaleDtlID.HasValue)
                        GetReaBmsCenSaleDtlLinkVOListStr(vo);
                    GetReaBmsCenSaleDtlConfirmLinkVO(model.Id, vo);
                }
                tempList.Add(vo);
            }
            entityVOList.count = tempList.Count;
            //过滤已验收的验收明细(可验收数大于0或者可验收数为0&&(当前接收数>0或当前拒收数>0))
            tempList = tempList.Where(p => (p.ConfirmCount > 0 || (p.ConfirmCount == 0 && (p.ReaBmsCenSaleDtlConfirm.AcceptCount > 0 || p.ReaBmsCenSaleDtlConfirm.RefuseCount > 0)))).ToList();

            //分页处理
            if (limit > 0 && limit < tempList.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = tempList.Skip(startIndex).Take(endIndex);
                if (list != null)
                    tempList = list.ToList();
            }
            entityVOList.list = tempList;
            return entityVOList;
        }
        private void GetReaBmsCenSaleDtlConfirmVO(IList<ReaBmsCenSaleDtlConfirm> dtlConfirmList, ReaSaleDtlConfirmVO vo)
        {
            if (dtlConfirmList != null)
            {
                var dtlList = dtlConfirmList.Where(p => p.Id == vo.Id).ToList();
                if (dtlList != null && dtlList.Count() > 0)
                {
                    vo.ReceivedCount = dtlList.Where(p => p.Status.Value != int.Parse(ReaBmsCenSaleDtlConfirmStatus.待继续验收.Key)).Sum(p => p.AcceptCount);
                    vo.RejectedCount = dtlList.Where(p => p.Status.Value != int.Parse(ReaBmsCenSaleDtlConfirmStatus.待继续验收.Key)).Sum(p => p.RefuseCount);
                }
                var goodsQty = vo.ReaBmsCenSaleDtlConfirm.AcceptCount + vo.ReaBmsCenSaleDtlConfirm.RefuseCount;
                vo.ConfirmCount = vo.ReaBmsCenSaleDtlConfirm.GoodsQty - goodsQty;
                if (vo.ReaBmsCenSaleDtlConfirm.Status == int.Parse(ReaBmsCenSaleDtlConfirmStatus.待继续验收.Key))
                    vo.ConfirmCount = vo.ReaBmsCenSaleDtlConfirm.GoodsQty;
                if (vo.ConfirmCount < 0) vo.ConfirmCount = 0;
            }
        }
        /// <summary>
        /// 订单验收
        /// </summary>
        /// <param name="dtlConfirmList"></param>
        /// <param name="vo"></param>
        private void GetReaBmsCenSaleDtlConfirmVOOfOrderDtl(IList<ReaBmsCenSaleDtlConfirm> dtlConfirmList, ReaSaleDtlConfirmVO vo)
        {
            if (dtlConfirmList != null)
            {
                //当前验收单的某一订单明细的验收明细信息
                var dtlConfirmList2 = dtlConfirmList.Where(p => p.OrderDtlID.Value == vo.ReaBmsCenSaleDtlConfirm.OrderDtlID.Value).ToList();

                ReaBmsCenOrderDtl orderDtl = IBReaBmsCenOrderDtl.Get(vo.ReaBmsCenSaleDtlConfirm.OrderDtlID.Value);
                if (orderDtl.GoodsQty.HasValue)
                    vo.DtlGoodsQty = orderDtl.GoodsQty.Value;
                //(该订单货品明细的已接收总数+该订单货品明细的已拒收总数)
                var curGoodsQty = vo.ReceivedCount + vo.RejectedCount;

                //除了当前验收单外的某一订单明细的所有验收明细信息
                IList<ReaBmsCenSaleDtlConfirm> otherDtlList = this.SearchListByHQL(string.Format("reabmscensaledtlconfirm.SaleDocConfirmID!={0} and reabmscensaledtlconfirm.OrderDtlID={1}", vo.ReaBmsCenSaleDtlConfirm.SaleDocConfirmID, vo.ReaBmsCenSaleDtlConfirm.OrderDtlID.Value));
                //已接收总数及已拒收总数处理
                if (otherDtlList != null && otherDtlList.Count() > 0)
                {
                    vo.ReceivedCount = otherDtlList.Where(p => p.Status.Value != int.Parse(ReaBmsCenSaleDtlConfirmStatus.待继续验收.Key)).Sum(p => p.AcceptCount);
                    vo.RejectedCount = otherDtlList.Where(p => p.Status.Value != int.Parse(ReaBmsCenSaleDtlConfirmStatus.待继续验收.Key)).Sum(p => p.RefuseCount);
                }
                double otherGoodsQty = 0;
                if (otherDtlList != null && otherDtlList.Count > 0)
                    otherGoodsQty = otherDtlList.Sum(p => p.GoodsQty);

                //某一订单货品明细的可验收数=该订单货品明细的购进数-某一订单明细当前验收单以外的所有验收信息的验收总数
                vo.ConfirmCount = vo.DtlGoodsQty - otherGoodsQty;// -curGoodsQty

                if (vo.ConfirmCount < 0) vo.ConfirmCount = 0;
            }
        }
        /// <summary>
        /// 供货验收
        /// </summary>
        /// <param name="dtlConfirmList"></param>
        /// <param name="vo"></param>
        private void GetReaBmsCenSaleDtlConfirmVOOfSaleDtl(IList<ReaBmsCenSaleDtlConfirm> dtlConfirmList, ReaSaleDtlConfirmVO vo)
        {
            if (dtlConfirmList != null)
            {
                //当前验收单的某一供货明细的验收明细信息
                var dtlList = dtlConfirmList.Where(p => p.SaleDtlID.Value == vo.ReaBmsCenSaleDtlConfirm.SaleDtlID.Value).ToList();

                ReaBmsCenSaleDtl saleDtl = IBReaBmsCenSaleDtl.Get(vo.ReaBmsCenSaleDtlConfirm.SaleDtlID.Value);
                vo.DtlGoodsQty = saleDtl.GoodsQty.Value;
                //当前验收单的验收总数(接收 + 拒收)
                var curGoodsQty = vo.ReaBmsCenSaleDtlConfirm.AcceptCount + vo.ReaBmsCenSaleDtlConfirm.RefuseCount;

                //除了当前验收单外的某一供货明细的所有验收明细信息
                IList<ReaBmsCenSaleDtlConfirm> otherDtlList = this.SearchListByHQL(string.Format("reabmscensaledtlconfirm.SaleDocConfirmID!={0} and reabmscensaledtlconfirm.SaleDtlID={1}", vo.ReaBmsCenSaleDtlConfirm.SaleDocConfirmID, vo.ReaBmsCenSaleDtlConfirm.SaleDtlID.Value));
                if (otherDtlList != null && otherDtlList.Count() > 0)
                {
                    vo.ReceivedCount = otherDtlList.Where(p => p.Status.Value != int.Parse(ReaBmsCenSaleDocConfirmStatus.待继续验收.Key)).Sum(p => p.AcceptCount);
                    vo.RejectedCount = otherDtlList.Where(p => p.Status.Value != int.Parse(ReaBmsCenSaleDocConfirmStatus.待继续验收.Key)).Sum(p => p.RefuseCount);
                }
                double otherGoodsQty = 0;
                if (otherDtlList != null && otherDtlList.Count > 0)
                    otherGoodsQty = otherDtlList.Sum(p => p.GoodsQty);

                //某一供货明细的可验收数=供货明细购进数-某一供货明细当前验收单以外的所有验收信息的验收总数
                vo.ConfirmCount = vo.DtlGoodsQty - otherGoodsQty;// -curGoodsQty
                if (vo.ConfirmCount < 0) vo.ConfirmCount = 0;
            }
        }
        private void GetReaBmsCenSaleDtlLinkVOListStr(ReaSaleDtlConfirmVO vo)
        {
            IList<ReaGoodsBarcodeOperation> dtlBarCodeList = IBReaGoodsBarcodeOperation.SearchListByHQL(string.Format("reagoodsbarcodeoperation.BDtlID in({0}) and (reagoodsbarcodeoperation.OperTypeID={1})", vo.ReaBmsCenSaleDtlConfirm.SaleDtlID.Value, ReaGoodsBarcodeOperType.供货.Key));
            IList<ReaGoodsBarcodeOperationVO> dtVOList = GetReaGoodsBarcodeOperationVO(dtlBarCodeList);
            ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
            vo.ReaBmsCenSaleDtlLinkVOListStr = tempParseObjectProperty.GetObjectPropertyNoPlanish(dtVOList);
        }
        public BaseResultBool AddDtlConfirmOfList(ReaBmsCenSaleDocConfirm docConfirm, IList<ReaSaleDtlConfirmVO> dtAddList, string codeScanningMode, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.success = true;

            foreach (ReaSaleDtlConfirmVO vo in dtAddList)
            {
                ReaBmsCenSaleDtlConfirm entity = vo.ReaBmsCenSaleDtlConfirm;
                entity.SaleDocConfirmNo = docConfirm.SaleDocConfirmNo;
                if (string.IsNullOrEmpty(entity.SaleDtlConfirmNo))
                    entity.SaleDtlConfirmNo = this.GetReqDocNo();
                entity.SaleDocConfirmID = docConfirm.Id;

                entity.ReaCompID = docConfirm.ReaCompID;
                entity.ReaCompanyName = docConfirm.ReaCompanyName;
                entity.ReaServerCompCode = docConfirm.ReaServerCompCode;
                if ((string.IsNullOrEmpty(entity.ProdGoodsNo) || string.IsNullOrEmpty(entity.CenOrgGoodsNo)) && entity.CompGoodsLinkID.HasValue)
                {
                    ReaGoodsOrgLink goodsOrgLink = IDReaGoodsOrgLinkDao.Get(entity.CompGoodsLinkID.Value);
                    if (goodsOrgLink != null)
                    {
                        entity.BarCodeType = goodsOrgLink.ReaGoods.BarCodeMgr;
                        entity.GoodsSort = goodsOrgLink.ReaGoods.GoodsSort;
                        entity.CenOrgGoodsNo = goodsOrgLink.CenOrgGoodsNo;
                        if (string.IsNullOrEmpty(entity.ProdGoodsNo))
                            entity.ProdGoodsNo = goodsOrgLink.ReaGoods.ProdGoodsNo;
                    }
                }

                if (!entity.Status.HasValue)
                    entity.Status = int.Parse(ReaBmsCenSaleDtlConfirmStatus.待继续验收.Key);
                entity.StatusName = ReaBmsCenSaleDtlConfirmStatus.GetStatusDic()[entity.Status.ToString()].Name;

                if (tempBaseResultBool.success == true)
                {
                    if (entity.RefuseCount > 0)
                        docConfirm.IsAcceptError = true;
                    this.Entity = entity;
                    ReaGoodsLot reaGoodsLot = null;
                    AddReaGoodsLot(ref reaGoodsLot, empID, empName);
                    if (reaGoodsLot != null)
                        this.Entity.GoodsLotID = reaGoodsLot.Id;
                    if (tempBaseResultBool.success == true)
                    {
                        entity.BarCodeMgr = (int)entity.BarCodeType;
                        tempBaseResultBool.success = this.Add();
                        //如果货品的条码类型为盒条码,需要添加货品的条码明细关系信息
                        if (vo.ReaBmsCenSaleDtlConfirm.BarCodeType == int.Parse(ReaGoodsBarCodeType.盒条码.Key))
                            tempBaseResultBool = AddBarcodeOperationOfList(docConfirm, entity, vo.ReaGoodsBarcodeOperationList, empID, empName);
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
        public BaseResultBool EditDtlConfirmOfList(ReaBmsCenSaleDocConfirm docConfirm, IList<ReaSaleDtlConfirmVO> dtEditList, string fieldsDtl, string codeScanningMode, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.success = true;
            foreach (ReaSaleDtlConfirmVO vo in dtEditList)
            {
                ReaBmsCenSaleDtlConfirm saleDtlConfirm = vo.ReaBmsCenSaleDtlConfirm;
                saleDtlConfirm.SaleDocConfirmID = docConfirm.Id;
                if (!saleDtlConfirm.Status.HasValue)
                    saleDtlConfirm.Status = int.Parse(ReaBmsCenSaleDtlConfirmStatus.待继续验收.Key);
                saleDtlConfirm.StatusName = ReaBmsCenSaleDtlConfirmStatus.GetStatusDic()[saleDtlConfirm.Status.ToString()].Name;

                if (tempBaseResultBool.success == true)
                {
                    if (saleDtlConfirm.RefuseCount > 0)
                        docConfirm.IsAcceptError = true;
                    this.Entity = saleDtlConfirm;
                    if (tempBaseResultBool.success == true)
                    {
                        this.Entity = saleDtlConfirm;
                        string[] tempDtlArray = CommonServiceMethod.GetUpdateFieldValueStr(this.Entity, fieldsDtl);
                        tempBaseResultBool.success = this.Update(tempDtlArray);
                        //如果货品的条码类型为盒条码,需要添加货品的条码明细关系信息
                        if (vo.ReaBmsCenSaleDtlConfirm.BarCodeType == int.Parse(ReaGoodsBarCodeType.盒条码.Key))
                        {
                            tempBaseResultBool = AddBarcodeOperationOfList(docConfirm, saleDtlConfirm, vo.ReaGoodsBarcodeOperationList, empID, empName);
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
        public BaseResultBool AddBarcodeOperationOfList(ReaBmsCenSaleDocConfirm docConfirm, ReaBmsCenSaleDtlConfirm dtlConfirm, IList<ReaGoodsBarcodeOperation> dtAddList, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (dtAddList == null || dtAddList.Count <= 0) return tempBaseResultBool;

            foreach (ReaGoodsBarcodeOperation barcode in dtAddList)
            {
                barcode.LabID = docConfirm.LabID;
                barcode.BDocID = docConfirm.Id;
                barcode.BDocNo = docConfirm.SaleDocConfirmNo;
                barcode.BDtlID = dtlConfirm.Id;
                barcode.ReaCompanyID = docConfirm.ReaCompID;
                barcode.CompanyName = docConfirm.ReaCompanyName;
                barcode.LotNo = dtlConfirm.LotNo;
                barcode.GoodsID = dtlConfirm.ReaGoodsID;
                barcode.ScanCodeGoodsID = dtlConfirm.GoodsID;
                barcode.GoodsCName = dtlConfirm.ReaGoodsName;
                barcode.GoodsUnit = dtlConfirm.GoodsUnit;
                barcode.UnitMemo = dtlConfirm.UnitMemo;
                barcode.ReaGoodsNo = dtlConfirm.ReaGoodsNo;
                barcode.ProdGoodsNo = dtlConfirm.ProdGoodsNo;
                barcode.CenOrgGoodsNo = dtlConfirm.CenOrgGoodsNo;
                barcode.GoodsNo = dtlConfirm.GoodsNo;
                barcode.ReaCompCode = docConfirm.ReaCompCode;
                barcode.GoodsSort = dtlConfirm.GoodsSort;
                barcode.CompGoodsLinkID = dtlConfirm.CompGoodsLinkID;
                barcode.BarCodeType = (int)dtlConfirm.BarCodeType;
                barcode.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.货品扫码.Key);
                barcode.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[barcode.OperTypeID.ToString()].Name;
                if (!barcode.GoodsQty.HasValue)
                    barcode.GoodsQty = dtlConfirm.GoodsQty;
                if (!barcode.MinBarCodeQty.HasValue)
                {
                    ReaGoods reaGoods = IDReaGoodsDao.Get(dtlConfirm.ReaGoodsID.Value);
                    double gonvertQty = 1;
                    if (reaGoods != null) gonvertQty = reaGoods.GonvertQty;
                    if (gonvertQty <= 0)
                    {
                        ZhiFang.Common.Log.Log.Warn("货品编码为:" + barcode.ReaGoodsNo + ",货品名称为:" + barcode.GoodsCName + ",货品包装单位的换算系数值为" + gonvertQty + ",维护不合理!");
                        gonvertQty = 1;
                    }
                    barcode.MinBarCodeQty = gonvertQty;
                }
                if (barcode.MinBarCodeQty <= 0) barcode.MinBarCodeQty = 1;
                barcode.OverageQty = barcode.MinBarCodeQty;
                if (!barcode.ScanCodeQty.HasValue)
                    barcode.ScanCodeQty = 1;
                if (string.IsNullOrEmpty(barcode.ScanCodeGoodsUnit))
                    barcode.ScanCodeGoodsUnit = dtlConfirm.GoodsUnit;
            }
            tempBaseResultBool = IBReaGoodsBarcodeOperation.AddBarcodeOperationOfList(dtAddList, 0, empID, empName, docConfirm.LabID);
            return tempBaseResultBool;
        }
        public BaseResultBool EditBmsCenSaleDtlConfirmValid(IList<ReaSaleDtlConfirmVO> voDtlList, string codeScanningMode)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            foreach (var vo in voDtlList)
            {
                ReaBmsCenSaleDtlConfirm model = vo.ReaBmsCenSaleDtlConfirm;
                #region 数量判断比较处理
                if (model.GoodsQty <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品【{0}】的购进数为零，不能验收！", model.ReaGoodsName);
                    break;
                }
                if (string.IsNullOrEmpty(model.LotNo))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品【{0}】的批号信息为空，不能验收！", model.ReaGoodsName);
                    break;
                }
                if (model.AcceptCount <= 0 && model.RefuseCount <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品【{ 0}】的验收数量和拒收数量都为零，不能验收！", model.ReaGoodsName);
                    break;
                }
                if (model.GoodsQty < model.AcceptCount)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品【{0}】的验收数量大于总量，不能验收！", model.ReaGoodsName);
                    break;
                }
                if (model.GoodsQty < model.RefuseCount)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品【{0}】的拒收数量大于总量，不能验收！", model.ReaGoodsName);
                    break;
                }
                if (model.GoodsQty < (model.AcceptCount + model.RefuseCount))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品【{0}】的验收和拒收数量大于总量，不能验收！", model.ReaGoodsName);
                    break;
                }
                if (model.Price.HasValue && model.Price.Value < 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品【{0}】的单价为空或小于0，不能验收！", model.ReaGoodsName);
                    break;
                }
                #endregion
                if (!model.InvalidDate.HasValue)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品名称为【{0}】的有效期至为空，不能验收", model.ReaGoodsName);
                    break;
                }
                if (tempBaseResultBool.success == true && vo.ReaBmsCenSaleDtlConfirm.BarCodeType == int.Parse(ReaGoodsBarCodeType.盒条码.Key))
                {
                    if (vo.ReaGoodsBarcodeOperationList != null && vo.ReaGoodsBarcodeOperationList.Count() > 0)
                    {
                        var acceptCountList = vo.ReaGoodsBarcodeOperationList.Where(p => p.OperTypeID == int.Parse(ReaGoodsBarcodeOperType.验货接收.Key));
                        var refuseCountList = vo.ReaGoodsBarcodeOperationList.Where(p => p.OperTypeID == int.Parse(ReaGoodsBarcodeOperType.验货拒收.Key));
                        if (model.AcceptCount < acceptCountList.Count())
                        {
                            tempBaseResultBool.success = false;
                            tempBaseResultBool.ErrorInfo = string.Format("货品【{0}】的接收数{1}小于扫码接收操作记录数{2},下不能验收！", model.ReaGoodsName, model.AcceptCount, acceptCountList.Count());
                            break;
                        }
                        else if (model.RefuseCount < refuseCountList.Count())
                        {
                            tempBaseResultBool.success = false;
                            tempBaseResultBool.ErrorInfo = string.Format("货品【{0}】的拒收数{1}小于扫码拒收操作记录数{2},下不能验收！", model.ReaGoodsName, model.AcceptCount, refuseCountList.Count());
                            break;
                        }
                    }
                    if (codeScanningMode == "strict")
                    {
                        if (vo.ReaGoodsBarcodeOperationList == null || vo.ReaGoodsBarcodeOperationList.Count() <= 0)
                        {
                            tempBaseResultBool.success = false;
                            tempBaseResultBool.ErrorInfo = string.Format("货品【{0}】的扫码操作记录为空,在严格扫码模式下不能验收！", model.ReaGoodsName);
                            break;
                        }
                    }
                }
                if (tempBaseResultBool.success == false)
                {
                    ZhiFang.Common.Log.Log.Warn(tempBaseResultBool.ErrorInfo);
                    break;
                }
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditBmsCenSaleDtlConfirmValidOfOrder(IList<ReaSaleDtlConfirmVO> voDtlList, string codeScanningMode)
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

            foreach (ReaSaleDtlConfirmVO vo in voDtlList)
            {
                ReaBmsCenSaleDtlConfirm saleDtlConfirm = vo.ReaBmsCenSaleDtlConfirm;
                if (!saleDtlConfirm.OrderDtlID.HasValue)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品【{0}】的关联订单明细信息为空！", saleDtlConfirm.ReaGoodsName);
                }
                else if (tempBaseResultBool.success == true && !saleDtlConfirm.OrderDocID.HasValue)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品【{0}】的关联订单信息为空！", saleDtlConfirm.ReaGoodsName);
                }
                //同一订单明细的验收明细的本次购进总数+同一订单明细的已验收明细购进的总数与订单明细的购进总数比较
                if (tempBaseResultBool.success == true)
                {
                    //同一订单明细的本次购进总数
                    double sumCurGoodsQty = voDtlList.Where(p => p.ReaBmsCenSaleDtlConfirm.OrderDtlID.Value == saleDtlConfirm.OrderDtlID.Value).Sum(p => p.ReaBmsCenSaleDtlConfirm.GoodsQty);
                    //同一订单明细的已验收数量
                    IList<ReaBmsCenSaleDtlConfirm> dtlConfirmList = this.SearchListByHQL(string.Format("reabmscensaledtlconfirm.OrderDtlID={0} and reabmscensaledtlconfirm.Status!={1}", saleDtlConfirm.OrderDtlID.Value, int.Parse(ReaBmsCenSaleDtlConfirmStatus.待继续验收.Key)));
                    double sumCurGoodsQty2 = dtlConfirmList.Sum(p => p.GoodsQty);
                    //订单明细的购进数
                    ReaBmsCenOrderDtl orderDtl = IBReaBmsCenOrderDtl.Get(saleDtlConfirm.OrderDtlID.Value);
                    if (orderDtl != null && (sumCurGoodsQty + sumCurGoodsQty2) > orderDtl.GoodsQty)
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = string.Format("货品为【{0}】,同一订单明细的本次购进总数+已购进的总数({1})大于订单购进数({2})", saleDtlConfirm.ReaGoodsName, (sumCurGoodsQty + sumCurGoodsQty2), orderDtl.GoodsQty); ;
                    }
                }
                if (tempBaseResultBool.success == false)
                    break;
            }
            return tempBaseResultBool;
        }
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
            string memo = string.Format("货品为:{0}", this.Entity.ReaGoodsName);
            ReaBmsCenSaleDocConfirm docConfirm = IDReaBmsCenSaleDocConfirmDao.Get(this.Entity.SaleDocConfirmID.Value);
            tempBaseResultBool.success = this.Remove();
            if (tempBaseResultBool.success == true)
                AddReaCheckInOperation(docConfirm, memo, empID, empName);

            if (tempBaseResultBool.success == false)
                tempBaseResultBool.ErrorInfo = string.Format("验收明细ID为{0},删除失败!", id);
            return tempBaseResultBool;
        }
        public void AddReaCheckInOperation(ReaBmsCenSaleDocConfirm entity, string memo, long empID, string empName)
        {
            ReaCheckInOperation sco = new ReaCheckInOperation();
            sco.BobjectID = entity.Id;
            sco.CreatorID = empID;
            if (empName != null && empName.Trim() != "")
                sco.CreatorName = empName;
            sco.BusinessModuleCode = "ReaBmsCenSaleDocConfirm";
            sco.IsUse = true;
            sco.Type = long.Parse(ReaBmsCenSaleDocConfirmStatus.货品删除.Key);
            sco.TypeName = ReaBmsCenSaleDocConfirmStatus.GetStatusDic()[ReaBmsCenSaleDocConfirmStatus.货品删除.Key.ToString()].Name;
            sco.Memo = memo;
            IBReaCheckInOperation.Entity = sco;
            IBReaCheckInOperation.Add();
        }
        public BaseResultBool SearchOrderIsConfirmOfByOrderId(long orderId, long confirmId)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                string hql = String.Format("reabmscensaledtlconfirm.OrderDocID={0} and reabmscensaledtlconfirm.Status={1}", orderId, int.Parse(ReaBmsCenSaleDtlConfirmStatus.待继续验收.Key));
                if (confirmId > 0)
                {
                    hql = hql + " and reabmscensaledtlconfirm.SaleDocConfirmID!=" + confirmId;
                }
                IList<ReaBmsCenSaleDtlConfirm> dtlConfirmList = this.SearchListByHQL(hql);
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
        public EntityList<ReaSaleDtlConfirmVO> SearchReaDtlConfirmVOOfStoreInByHQL(string strHqlWhere, string order, int page, int limit)
        {
            EntityList<ReaSaleDtlConfirmVO> entityVOList = new EntityList<ReaSaleDtlConfirmVO>();
            entityVOList.list = new List<ReaSaleDtlConfirmVO>();
            //ZhiFang.Common.Log.Log.Debug("SearchReaDtlConfirmVOOfStoreInByHQL.strHqlWhere:" + strHqlWhere);
            //验收明细信息
            EntityList<ReaBmsCenSaleDtlConfirm> el = this.SearchListByHQL(strHqlWhere, order, page, limit);
            //ZhiFang.Common.Log.Log.Debug("SearchReaDtlConfirmVOOfStoreInByHQL.el.list:" + el.list.Count);
            IList<ReaSaleDtlConfirmVO> tempDtlConfirmVOList = new List<ReaSaleDtlConfirmVO>();
            //全部库房信息
            IList<ReaStorage> storageAllList = new List<ReaStorage>();
            foreach (var model in el.list)
            {
                ReaSaleDtlConfirmVO vo = new ReaSaleDtlConfirmVO();
                vo.ReaBmsCenSaleDtlConfirm = model;

                //待入库货品的库房试剂关系处理
                IList<ReaStorage> storageList = new List<ReaStorage>();
                string storageGoodsLinkHql = "reastoragegoodslink.Visible=1 and reastoragegoodslink.GoodsID=" + model.ReaGoodsID.Value;
                IList<ReaStorageGoodsLink> storageGoodsLinkList = IDReaStorageGoodsLinkDao.SearchReaStorageGoodsLinkListByAllJoinHql(storageGoodsLinkHql, "", "", "", 1, 10000, "");
                //ZhiFang.Common.Log.Log.Debug("SearchReaDtlConfirmVOOfStoreInByHQL.storageGoodsLinkList.Count:" + storageGoodsLinkList.Count);
                if (storageGoodsLinkList != null && storageGoodsLinkList.Count > 0)
                {
                    ReaStorage reaStorage = storageGoodsLinkList.OrderByDescending(p => p.ReaStorage.IsMainStorage).OrderBy(p => p.ReaStorage.DispOrder).ElementAt(0).ReaStorage;
                    vo.StorageID = reaStorage.Id;
                    vo.StorageCName = reaStorage.CName;
                    foreach (var item in storageGoodsLinkList)
                    {
                        storageList.Add(item.ReaStorage);
                    }
                }
                else
                {
                    if (storageAllList.Count <= 0)
                        storageAllList = IDReaStorageDao.GetListByHQL("reastorage.Visible=1");
                    storageList = storageAllList;
                    if (storageList.Count > 0)
                    {
                        ReaStorage reaStorage = storageList.OrderByDescending(p => p.IsMainStorage).OrderBy(p => p.DispOrder).ElementAt(0);
                        vo.StorageID = reaStorage.Id;
                        vo.StorageCName = reaStorage.CName;
                    }
                }

                ParseObjectProperty tempstorageprop2 = new ParseObjectProperty("ReaStorage_Id,ReaStorage_CName");
                vo.ReaStorageList = tempstorageprop2.GetObjectListPlanish<ReaStorage>(storageList);

                ReaGoods reaGoods = null;
                if (model.ReaGoodsID.HasValue)
                    reaGoods = IDReaGoodsDao.Get(model.ReaGoodsID.Value);
                if (reaGoods != null)
                {
                    vo.ReaGoodsSName = reaGoods.SName;
                    vo.ReaGoodsEName = reaGoods.EName;
                    vo.GoodsSort = reaGoods.GoodsSort;
                    vo.ReaBmsCenSaleDtlConfirm.ProdOrgName = reaGoods.ProdOrgName;
                }
                //某一验收明细的所有入库明细信息
                IList<ReaBmsInDtl> tempReaBmsInDtlList = IDReaBmsInDtlDao.GetListByHQL(string.Format("reabmsindtl.SaleDtlConfirmID={0}", model.Id));
                switch (model.BarCodeType)
                {
                    case 1:
                        StringBuilder inDtlStr = new StringBuilder();
                        foreach (var reaBmsInDtl in tempReaBmsInDtlList)
                        {
                            inDtlStr.Append(reaBmsInDtl.Id + ",");
                        }
                        if (!string.IsNullOrEmpty(inDtlStr.ToString()))
                        {
                            //当前入库明细的同一验收明细的所有入库扫码记录集合
                            IList<ReaGoodsBarcodeOperation> dtlBarCodeList = IBReaGoodsBarcodeOperation.SearchListByHQL(string.Format("reagoodsbarcodeoperation.BDtlID in ({0}) and reagoodsbarcodeoperation.OperTypeID={1}", inDtlStr.ToString().TrimEnd(','), ReaGoodsBarcodeOperType.验货入库.Key));
                            ParseObjectProperty tempParseObjectProperty2 = new ParseObjectProperty();
                            vo.ReaBmsInDtlLinkListStr = tempParseObjectProperty2.GetObjectPropertyNoPlanish(dtlBarCodeList);
                        }
                        //供货明细的条码信息集合
                        if (vo.ReaBmsCenSaleDtlConfirm.SaleDtlID.HasValue)
                            GetReaBmsCenSaleDtlLinkVOListStr(vo);
                        //验收的扫码记录集合
                        GetReaBmsCenSaleDtlConfirmLinkVO(model.Id, vo);
                        break;
                    default:
                        break;
                }
                tempDtlConfirmVOList.Add(vo);
            }

            entityVOList.count = tempDtlConfirmVOList.Count;
            entityVOList.list = tempDtlConfirmVOList;
            return entityVOList;
        }
        public BaseResultBool EditBmsCenSaleDtlConfirmValidOfSale(IList<ReaSaleDtlConfirmVO> voDtlList, string codeScanningMode)
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
            //未验证完
            foreach (ReaSaleDtlConfirmVO vo in voDtlList)
            {
                ReaBmsCenSaleDtlConfirm saleDtlConfirm = vo.ReaBmsCenSaleDtlConfirm;
                if (!saleDtlConfirm.SaleDtlID.HasValue)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("货品【{0}】的关联供货明细信息为空！", saleDtlConfirm.ReaGoodsName);
                }
                //同一供货明细的验收明细的本次购进总数+同一供货明细的已验收明细购进总数与供货明细的购进总数比较
                if (tempBaseResultBool.success == true)
                {
                    //同一供货明细的验收明细的本次购进总数
                    double sumCurGoodsQty = voDtlList.Where(p => p.ReaBmsCenSaleDtlConfirm.SaleDtlID.Value == saleDtlConfirm.SaleDtlID.Value).Sum(p => p.ReaBmsCenSaleDtlConfirm.GoodsQty);
                    //同一供货明细的已验收明细购进总数
                    IList<ReaBmsCenSaleDtlConfirm> dtlConfirmList = this.SearchListByHQL(string.Format("reabmscensaledtlconfirm.SaleDtlID={0} and reabmscensaledtlconfirm.Status!={1}", saleDtlConfirm.SaleDtlID.Value, int.Parse(ReaBmsCenSaleDtlConfirmStatus.待继续验收.Key)));
                    double sumCurGoodsQty2 = dtlConfirmList.Sum(p => p.GoodsQty);
                    //供货明细的购进数
                    ReaBmsCenSaleDtl saleDtl = IBReaBmsCenSaleDtl.Get(saleDtlConfirm.SaleDtlID.Value);
                    if (saleDtl != null && (sumCurGoodsQty + sumCurGoodsQty2) > saleDtl.GoodsQty)
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = string.Format("货品为【{0}】,(同一供货明细的本次购进总数+已购进的总数){1}大于订单购进数{2}", saleDtlConfirm.ReaGoodsName, (sumCurGoodsQty + sumCurGoodsQty2), saleDtl.GoodsQty); ;
                    }
                }
                if (tempBaseResultBool.success == false)
                    break;
            }
            return tempBaseResultBool;
        }
        public BaseResultBool SearchReaSaleIsConfirmOfBySaleDocID(long saleDocID, long confirmId)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                IList<ReaBmsCenSaleDtl> saleDtlList = IBReaBmsCenSaleDtl.SearchListByHQL(string.Format("reabmscensaledtl.SaleDocID={0}", saleDocID));
                StringBuilder idStr = new StringBuilder();
                foreach (var model in saleDtlList)
                {
                    idStr.Append(model.Id + ",");
                }
                string hql = String.Format("reabmscensaledtlconfirm.SaleDtlID in({0}) and reabmscensaledtlconfirm.Status={1}", idStr.ToString().TrimEnd(','), int.Parse(ReaBmsCenSaleDtlConfirmStatus.待继续验收.Key));
                if (confirmId > 0)
                {
                    hql = hql + " and reabmscensaledtlconfirm.SaleDocConfirmID!=" + confirmId;
                }
                IList<ReaBmsCenSaleDtlConfirm> dtlConfirmList = this.SearchListByHQL(hql);
                if (dtlConfirmList != null && dtlConfirmList.Count > 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "供货单Id为：" + saleDocID + "已存在验收暂存的信息,请继续完成该供货单验收后再操作!";
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
        private void AddReaGoodsLot(ref ReaGoodsLot reaGoodsLot, long empID, string empName)
        {
            if (this.Entity == null) return;
            if (string.IsNullOrEmpty(this.Entity.LotNo)) return;
            //if (!this.Entity.ReaGoodsID.HasValue) return;
            if (!this.Entity.InvalidDate.HasValue) return;

            ReaGoodsLot lot = new ReaGoodsLot();
            lot.LabID = this.Entity.LabID;
            lot.Visible = true;
            lot.GoodsID = this.Entity.ReaGoodsID.Value;
            lot.ReaGoodsNo = this.Entity.ReaGoodsNo;
            lot.LotNo = this.Entity.LotNo;

            lot.ProdDate = this.Entity.ProdDate;
            lot.GoodsCName = this.Entity.ReaGoodsName;
            lot.InvalidDate = this.Entity.InvalidDate;
            lot.CreaterID = empID;
            lot.CreaterName = empName;
            IBReaGoodsLot.Entity = lot;
            BaseResultBool baseResultBool = IBReaGoodsLot.AddAndValid(ref reaGoodsLot);
            if (!baseResultBool.success)
                ZhiFang.Common.Log.Log.Error("货品验收保存货品批号信息失败!");

        }

        /// <summary>
        /// 关联货品表查询供货验收明细
        /// </summary>
        public EntityList<ReaBmsCenSaleDtlConfirm> GetReaBmsCenSaleDtlConfirmListByHql(string strHqlWhere, string sort, int page, int limit)
        {
            return ((IDReaBmsCenSaleDtlConfirmDao)base.DBDao).GetReaBmsCenSaleDtlConfirmListByHql(strHqlWhere, sort, page, limit);
        }

    }

}