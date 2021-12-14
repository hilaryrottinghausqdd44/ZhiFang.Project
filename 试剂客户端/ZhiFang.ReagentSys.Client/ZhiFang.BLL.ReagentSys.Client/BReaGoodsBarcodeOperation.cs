using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using System.Collections.Generic;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaGoodsScanCode;
using System.Linq;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaConfirm;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject;
using System.Text;
using System;

namespace ZhiFang.BLL.ReagentSys.Client
{
    public class BReaGoodsBarcodeOperation : BaseBLL<ReaGoodsBarcodeOperation>, IBReaGoodsBarcodeOperation
    {
        IDReaBmsInDtlDao IDReaBmsInDtlDao { get; set; }
        IDReaBmsQtyDtlDao IDReaBmsQtyDtlDao { get; set; }
        IDReaBmsCenSaleDtlDao IDReaBmsCenSaleDtlDao { get; set; }
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IDReaBmsCenSaleDocConfirmDao IDReaBmsCenSaleDocConfirmDao { get; set; }
        IDReaBmsCenSaleDtlConfirmDao IDReaBmsCenSaleDtlConfirmDao { get; set; }
        IBReaCenBarCodeFormat IBReaCenBarCodeFormat { get; set; }
        IBReaBmsCenOrderDtl IBReaBmsCenOrderDtl { get; set; }
        IBReaGoodsOrgLink IBReaGoodsOrgLink { get; set; }
        public BaseResultBool AddBarcodeOperationOfList(IList<ReaGoodsBarcodeOperation> dtAddList, long operTypeID, long empID, string empName, long labID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.success = true;
            foreach (ReaGoodsBarcodeOperation operation in dtAddList)
            {
                if (labID > 0)
                    operation.LabID = labID;
                if (operTypeID > 0)
                {
                    operation.OperTypeID = operTypeID;
                    operation.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[operation.OperTypeID.ToString()].Name;
                }
                operation.Visible = true;
                operation.CreaterID = empID;
                operation.CreaterName = empName;
                operation.DataUpdateTime = System.DateTime.Now;
                if (string.IsNullOrEmpty(operation.PUsePackSerial))
                    operation.PUsePackSerial = operation.Id.ToString();

                if (!operation.MinBarCodeQty.HasValue)
                {
                    ReaGoods reaGoods = IDReaGoodsDao.Get(operation.GoodsID.Value);
                    double gonvertQty = 1;
                    if (reaGoods != null) gonvertQty = reaGoods.GonvertQty;
                    if (gonvertQty <= 0)
                    {
                        ZhiFang.Common.Log.Log.Warn("货品编码为:" + operation.ReaGoodsNo + ",货品名称为:" + operation.GoodsCName + ",货品包装单位的换算系数值为" + gonvertQty + ",维护不合理!");
                        gonvertQty = 1;
                    }
                    operation.MinBarCodeQty = gonvertQty;
                }
                if (!operation.ScanCodeQty.HasValue)
                    operation.ScanCodeQty = 1;
                if (string.IsNullOrEmpty(operation.ScanCodeGoodsUnit))
                    operation.ScanCodeGoodsUnit = operation.GoodsUnit;
                this.Entity = operation;
                tempBaseResultBool.success = this.Add();
                if (tempBaseResultBool.success == false)
                    tempBaseResultBool.ErrorInfo = "新增货品条码操作记录信息失败！";

                if (tempBaseResultBool.success == false) break;
            }
            return tempBaseResultBool;
        }
        public BaseResultBool AddBarcodeOperation(ReaGoodsBarcodeOperation operation, long operTypeID, long empID, string empName, long labID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.success = true;

            if (labID > 0)
                operation.LabID = labID;
            if (operTypeID > 0)
            {
                operation.OperTypeID = operTypeID;
                operation.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[operation.OperTypeID.ToString()].Name;
            }
            operation.Visible = true;
            operation.CreaterID = empID;
            operation.CreaterName = empName;
            operation.DataUpdateTime = System.DateTime.Now;
            if (string.IsNullOrEmpty(operation.PUsePackSerial))
                operation.PUsePackSerial = operation.Id.ToString();

            if (!operation.MinBarCodeQty.HasValue)
            {
                ReaGoods reaGoods = IDReaGoodsDao.Get(operation.GoodsID.Value);
                double gonvertQty = 1;
                if (reaGoods != null) gonvertQty = reaGoods.GonvertQty;
                if (gonvertQty <= 0)
                {
                    ZhiFang.Common.Log.Log.Warn("货品编码为:" + operation.ReaGoodsNo + ",货品名称为:" + operation.GoodsCName + ",货品包装单位的换算系数值为" + gonvertQty + ",维护不合理!");
                    gonvertQty = 1;
                }
                operation.MinBarCodeQty = gonvertQty;
            }
            if (!operation.ScanCodeQty.HasValue)
                operation.ScanCodeQty = 1;
            if (string.IsNullOrEmpty(operation.ScanCodeGoodsUnit))
                operation.ScanCodeGoodsUnit = operation.GoodsUnit;
            this.Entity = operation;
            tempBaseResultBool.success = this.Add();
            if (tempBaseResultBool.success == false)
                tempBaseResultBool.ErrorInfo = "新增货品条码操作记录信息失败！";

            return tempBaseResultBool;
        }
        public ReaGoodsScanCodeVO DecodingReaGoodsScanCodeVOOfConfirm(long reaCompID, string serialNo, string scanCodeType, long bobjectID)
        {
            ReaGoodsScanCodeVO vo = new ReaGoodsScanCodeVO();
            vo.BoolFlag = true;
            vo.ReaBarCodeVOList = new List<ReaBarCodeVO>();

            if (string.IsNullOrEmpty(serialNo))
            {
                vo.ErrorInfo = string.Format("验收货品扫码--条码值为空!");
                vo.BoolFlag = false;
                return vo;
            }

            //已被供货验收接收和拒收的货品条码信息
            IList<ReaGoodsBarcodeOperation> dtlBarCodeList = this.SearchListByHQL(string.Format("(reagoodsbarcodeoperation.UsePackSerial='{0}' or reagoodsbarcodeoperation.UsePackQRCode='{1}') and (reagoodsbarcodeoperation.OperTypeID={2} or reagoodsbarcodeoperation.OperTypeID={3})", serialNo, serialNo, ReaGoodsBarcodeOperType.验货接收.Key, ReaGoodsBarcodeOperType.验货拒收.Key));

            if (dtlBarCodeList.Count > 0)
            {
                ReaGoodsBarcodeOperation operation = dtlBarCodeList.OrderByDescending(p => p.DataAddTime).ToList()[0];
                vo.BoolFlag = false;
                vo.ErrorInfo = string.Format("条码为:{0},已被{1},验收明细Id为{2},操作人为{3}!", serialNo, ReaGoodsBarcodeOperType.GetStatusDic()[operation.OperTypeID.ToString()].Name, operation.BDtlID, operation.CreaterName);
                return vo;
            }
            if (serialNo.Length < SysPublicSet.UseSerialNoLength)
            {
                vo.ErrorInfo = string.Format("当前条码长度为" + serialNo.Length + ",系统预置的条码最小长度为:" + SysPublicSet.UseSerialNoLength);
                vo.BoolFlag = false;
                return vo;
            }

            if (serialNo.Length > SysPublicSet.UseSerialNoLength)
            {
                //调用条码解码规则匹配               
                vo = IBReaCenBarCodeFormat.GetReaGoodsScanCodeVOBySanBarCode(reaCompID, serialNo);
            }
            else if (scanCodeType == "reasale")
            {
                //(供货明细)一维条码处理
                string hql = string.Format("(reagoodsbarcodeoperation.UsePackSerial='{0}' and reagoodsbarcodeoperation.OperTypeID={1})", serialNo, ReaGoodsBarcodeOperType.供货.Key);
                if (bobjectID > 0)
                    hql = hql + " and reagoodsbarcodeoperation.BDocID=" + bobjectID;
                IList<ReaGoodsBarcodeOperation> operationList = this.SearchListByHQL(hql);
                if (operationList != null && operationList.Count > 0)
                {
                    ReaGoodsBarcodeOperation operation = operationList.OrderByDescending(p => p.DataAddTime).ToList()[0];
                    ReaBarCodeVO reaBarCodeVO = GetReaBarCodeVO(operation);
                    vo.ReaBarCodeVOList.Add(reaBarCodeVO);
                }
            }

            //订单验收(当条码所属的货品属于当前订单明细中)
            if (vo.BoolFlag == true && scanCodeType == "reaorder")
            {
                DecodingReaGoodsScanCodeVOOfConfirmTOOrdert(ref vo, bobjectID);
            }
            return vo;
        }
        private ReaBarCodeVO GetReaBarCodeVO(ReaGoodsBarcodeOperation operation)
        {
            ReaBarCodeVO reaBarCodeVO = new ReaBarCodeVO();
            ReaGoods reaGoods = IDReaGoodsDao.Get(operation.GoodsID.Value);

            reaBarCodeVO.ReaGoodsID = operation.GoodsID;
            reaBarCodeVO.CName = operation.GoodsCName;
            reaBarCodeVO.UnitName = operation.GoodsUnit;
            reaBarCodeVO.UnitMemo = operation.UnitMemo;
            reaBarCodeVO.BarCodeType = operation.BarCodeType;
            reaBarCodeVO.CompGoodsLinkID = operation.CompGoodsLinkID;
            reaBarCodeVO.MinBarCodeQty = operation.MinBarCodeQty;
            if (reaGoods != null)
            {
                reaBarCodeVO.SName = reaGoods.SName;
                reaBarCodeVO.EName = reaGoods.EName;
                reaBarCodeVO.ApproveDocNo = reaGoods.ApproveDocNo;
                reaBarCodeVO.RegistNo = reaGoods.RegistNo;
                reaBarCodeVO.RegistDate = reaGoods.RegistDate;
                reaBarCodeVO.RegistNoInvalidDate = reaGoods.RegistNoInvalidDate;
                if (!reaBarCodeVO.MinBarCodeQty.HasValue || reaBarCodeVO.MinBarCodeQty <= 0)
                {
                    reaBarCodeVO.MinBarCodeQty = reaGoods.GonvertQty;
                }
            }

            if (operation.OperTypeID.Value.ToString() == ReaGoodsBarcodeOperType.供货.Key)
            {
                ReaBmsCenSaleDtl saleDtl = IDReaBmsCenSaleDtlDao.Get(operation.BDtlID.Value);
                if (saleDtl != null)
                {
                    reaBarCodeVO.BarCodeType = saleDtl.BarCodeType;
                    if (!reaBarCodeVO.CompGoodsLinkID.HasValue)
                        reaBarCodeVO.CompGoodsLinkID = saleDtl.CompGoodsLinkID;
                    reaBarCodeVO.Price = saleDtl.Price.Value;
                    reaBarCodeVO.BiddingNo = saleDtl.BiddingNo;
                }
            }

            reaBarCodeVO.OtherPackSerial = operation.OtherPackSerial;
            reaBarCodeVO.UsePackSerial = operation.UsePackSerial;
            reaBarCodeVO.UsePackQRCode = operation.UsePackQRCode;
            reaBarCodeVO.SysPackSerial = operation.SysPackSerial;

            reaBarCodeVO.ReaGoodsNo = operation.ReaGoodsNo;
            reaBarCodeVO.ProdGoodsNo = operation.ProdGoodsNo;
            reaBarCodeVO.CenOrgGoodsNo = operation.CenOrgGoodsNo;
            reaBarCodeVO.GoodsNo = operation.GoodsNo;
            return reaBarCodeVO;
        }
        private void DecodingReaGoodsScanCodeVOOfConfirmTOOrdert(ref ReaGoodsScanCodeVO vo, long bobjectID)
        {
            if (vo.ReaBarCodeVOList != null && vo.ReaBarCodeVOList.Count > 0)
            {
                EntityList<ReaOrderDtlOfConfirmVO> orderDtlVOList = IBReaBmsCenOrderDtl.SearchReaOrderDtlOfConfirmVOListByHQL(string.Format("reabmscenorderdtl.OrderDocID={0}", bobjectID), "", -1, -1);
                if (orderDtlVOList.list == null || orderDtlVOList.count <= 0)
                {
                    vo.BoolFlag = false;
                    vo.ErrorInfo = string.Format("订单Id为{0},当前可验收的订单信息为空!", bobjectID);
                    return;
                }
                //订单明细的盒条码信息
                var orderDtlVOList2 = orderDtlVOList.list.Where(p => p.BarCodeType == int.Parse(ReaGoodsBarCodeType.盒条码.Key));
                var reaBarCodeVOList = vo.ReaBarCodeVOList;
                for (int i = 0; i < reaBarCodeVOList.Count; i++)
                {
                    ReaBarCodeVO model = reaBarCodeVOList[i];
                    var tempList = orderDtlVOList2.Where(p => p.CompGoodsLinkID.Value == model.CompGoodsLinkID.Value);
                    if (tempList.Count() <= 0)
                    {
                        //货品不存在订单明细中
                        vo.ReaBarCodeVOList.Remove(model);
                    }
                    else if (tempList.Count() > 0)
                    {
                        //订单明细的货品可验收数小于等于0
                        if (tempList.Sum(p => p.ConfirmCount) <= 0)
                            vo.ReaBarCodeVOList.Remove(model);
                    }
                }
            }
        }
        public ReaGoodsScanCodeVO DecodingReaGoodsScanCodeVOOfReaBmsInByCompIDAndSerialNo(long reaCompID, string serialNo, long docConfirmID)
        {
            ReaGoodsScanCodeVO vo = new ReaGoodsScanCodeVO();
            vo.BoolFlag = true;
            vo.ReaBarCodeVOList = new List<ReaBarCodeVO>();
            if (string.IsNullOrEmpty(serialNo))
            {
                vo.ErrorInfo = string.Format("验收货品扫码--条码为空!");
                vo.BoolFlag = false;
                return vo;
            }

            //(是否允许重复入库?)当前条码是否存在盒条码操作记录里
            IList<ReaGoodsBarcodeOperation> hasBarCodeList = this.SearchListByHQL(string.Format("(reagoodsbarcodeoperation.UsePackSerial='{0}' or reagoodsbarcodeoperation.UsePackQRCode='{1}') and reagoodsbarcodeoperation.OperTypeID in({2},{3})", serialNo, serialNo, ReaGoodsBarcodeOperType.验货入库.Key, ReaGoodsBarcodeOperType.库存初始化.Key));
            if (hasBarCodeList.Count > 0)
            {
                ReaGoodsBarcodeOperation operation = hasBarCodeList.OrderByDescending(p => p.DataAddTime).ToList()[0];
                vo.BoolFlag = false;
                vo.ErrorInfo = string.Format("条码为:{0},已被扫码验货入库{1},入库明细Id为{2},操作人为{3}!", serialNo, ReaGoodsBarcodeOperType.GetStatusDic()[operation.OperTypeID.ToString()].Name, operation.BDtlID, operation.CreaterName);
                return vo;
            }
            if (serialNo.Length < SysPublicSet.UseSerialNoLength)
            {
                vo.ErrorInfo = string.Format("当前条码长度为" + serialNo.Length + ",系统预置的条码最小长度为:" + SysPublicSet.UseSerialNoLength);
                vo.BoolFlag = false;
                return vo;
            }

            if (serialNo.Length > SysPublicSet.UseSerialNoLength)
            {
                //调用条码解码规则匹配
                vo = IBReaCenBarCodeFormat.GetReaGoodsScanCodeVOBySanBarCode(reaCompID, serialNo);
                if (vo.BoolFlag == false || docConfirmID <= 0) return vo;

                //如果是验收入库,需要判断当前条码的货品是否存在当前的入库验收单里
                for (int i = 0; i < vo.ReaBarCodeVOList.Count; i++)
                {
                    GetReaGoodsScanCodeVOOfReaBmsIn(docConfirmID, vo.ReaBarCodeVOList[i].ReaGoodsID.Value, serialNo, ref vo);
                    if (vo.BoolFlag == false)
                    {
                        vo.ReaBarCodeVOList.RemoveAt(i);
                    }
                }
                if (vo.ReaBarCodeVOList.Count <= 0)
                {
                    vo.BoolFlag = false;
                    vo.ErrorInfo = string.Format("条码为:{0},其货品不存在当前入库验收单里,不能扫码入库!", serialNo);
                }
            }
            else
            {
                //验货接收一维盒条码处理
                IList<ReaGoodsBarcodeOperation> dtlBarCodeList = this.SearchListByHQL(string.Format("reagoodsbarcodeoperation.UsePackSerial='{0}'and reagoodsbarcodeoperation.OperTypeID={1}", serialNo, ReaGoodsBarcodeOperType.验货接收.Key));
                if (dtlBarCodeList.Count > 0)
                {
                    ReaGoodsBarcodeOperation operation = dtlBarCodeList.OrderByDescending(p => p.DataAddTime).ToList()[0];
                    GetReaGoodsScanCodeVOOfReaBmsIn(docConfirmID, operation.GoodsID.Value, serialNo, ref vo);
                    if (vo.BoolFlag == false) return vo;

                    ReaBarCodeVO reaBarCodeVO = GetReaBarCodeVO(operation);
                    vo.ReaBarCodeVOList.Add(reaBarCodeVO);
                    return vo;
                }
                else
                {
                    //供货一维盒条码处理
                    dtlBarCodeList = this.SearchListByHQL(string.Format("reagoodsbarcodeoperation.UsePackSerial='{0}' and reagoodsbarcodeoperation.OperTypeID={1}", serialNo, ReaGoodsBarcodeOperType.供货.Key));
                    if (dtlBarCodeList.Count > 0)
                    {
                        ReaGoodsBarcodeOperation operation = dtlBarCodeList.OrderByDescending(p => p.DataAddTime).ToList()[0];
                        //需要判断该供货条码的所属供货单是否进行过验收操作
                        IList<ReaBmsCenSaleDocConfirm> tempListConfirm = IDReaBmsCenSaleDocConfirmDao.GetListByHQL("reabmscensaledocconfirm.SaleDocID=" + operation.BDocID.Value);
                        if (tempListConfirm == null || tempListConfirm.Count <= 0)
                        {
                            vo.BoolFlag = false;
                            vo.ErrorInfo = string.Format("条码为:{0},所属的供货单号为:{1},未进行验收,不能扫码入库!", serialNo, operation.BDocNo);
                            return vo;
                        }

                        GetReaGoodsScanCodeVOOfReaBmsIn(docConfirmID, operation.GoodsID.Value, serialNo, ref vo);
                        if (vo.BoolFlag == false) return vo;
                        ReaBarCodeVO reaBarCodeVO = GetReaBarCodeVO(operation);
                        vo.ReaBarCodeVOList.Add(reaBarCodeVO);
                        return vo;
                    }
                }
            }
            return vo;
        }
        private void GetReaGoodsScanCodeVOOfReaBmsIn(long docConfirmID, long goodsID, string serialNo, ref ReaGoodsScanCodeVO vo)
        {
            //如果是验收入库,需要判断当前条码的货品是否存在当前的入库验收单里
            if (docConfirmID <= 0 || goodsID <= 0) return;

            IList<ReaBmsCenSaleDtlConfirm> dtlList = IDReaBmsCenSaleDtlConfirmDao.GetListByHQL("reabmscensaledtlconfirm.SaleDocConfirmID=" + docConfirmID + " and reabmscensaledtlconfirm.ReaGoodsID=" + goodsID);

            if (dtlList == null || dtlList.Count <= 0)
            {
                vo.BoolFlag = false;
                vo.ErrorInfo = string.Format("条码为:{0},其货品不存在当前入库验收明细中,不能扫码入库!", serialNo);
            }
        }
        #region 获取入库确认后的库存货品(生成)条码信息
        public EntityList<ReaGoodsPrintBarCodeVO> SearchReaGoodsPrintBarCodeVOListByInDocId(long inDocId, string dtlIdStr, string boxHql, int page, int limit)
        {
            EntityList<ReaGoodsPrintBarCodeVO> entityList = new EntityList<ReaGoodsPrintBarCodeVO>();
            entityList.list = new List<ReaGoodsPrintBarCodeVO>();

            string hql = "reabmsindtl.InDocID=" + inDocId;
            if (!string.IsNullOrEmpty(dtlIdStr))
                hql = hql + " and reabmsindtl.Id in(" + dtlIdStr.Trim().TrimEnd(',') + ")";
            IList<ReaBmsInDtl> inDtlList = IDReaBmsInDtlDao.GetListByHQL(hql);
            if (inDtlList == null || inDtlList.Count <= 0)
                return entityList;

            StringBuilder boxBarCodeHql = new StringBuilder();
            StringBuilder qtyHql = new StringBuilder();
            foreach (var inDtl in inDtlList)
            {
                if (inDtl.ReaGoods.IsPrintBarCode == 1)
                {
                    boxBarCodeHql.Append("reagoodsbarcodeoperation.BDtlID=" + inDtl.Id + " or ");
                    qtyHql.Append("reabmsqtydtl.InDtlID=" + inDtl.Id + " or ");
                }
            }
            if (string.IsNullOrEmpty(boxBarCodeHql.ToString()))
                return entityList;
            char[] trimChars = new char[] { ' ', 'o', 'r' };
            string boxHql2 = "(" + boxBarCodeHql.ToString().TrimEnd(trimChars) + ") and reagoodsbarcodeoperation.BarcodeCreatType=" + ReaGoodsBarcodeOperationSerialType.条码生成.Key + " and reagoodsbarcodeoperation.OperTypeID=" + ReaGoodsBarcodeOperType.验货入库.Key;
            boxHql2.ToString().TrimEnd(trimChars);
            if (string.IsNullOrEmpty(boxHql))
            {
                boxHql = boxHql2;
            }
            else
            {
                boxHql = boxHql + " and (" + boxHql2 + ")";
            }
            IList<ReaBmsQtyDtl> qtyList = IDReaBmsQtyDtlDao.GetListByHQL("(" + qtyHql.ToString().TrimEnd(trimChars) + ")");

            //如果当前的入库明细条码类型为批条码
            var batchInDtlList = inDtlList.Where(p => p.BarCodeType == int.Parse(ReaGoodsBarCodeType.批条码.Key));
            if (batchInDtlList != null && batchInDtlList.Count() > 0)
            {
                entityList.list = SearchBatchBarCodePrintVOList(batchInDtlList.ToList(), qtyList);
            }

            //入库明细的盒条码集合信息
            var boxInDtlList = inDtlList.Where(p => p.BarCodeType == int.Parse(ReaGoodsBarCodeType.盒条码.Key));
            if (inDtlList != null && inDtlList.Count() > 0)
            {
                IList<ReaGoodsPrintBarCodeVO> voBoxList = SearchBoxBarCodePrintVOList(inDtlList, qtyList, boxHql);
                if (entityList.list != null && entityList.list.Count > 0)
                    entityList.list = voBoxList.Union(entityList.list).ToList();
                else
                    entityList.list = voBoxList;
            }
            if (entityList.list != null && entityList.list.Count > 0)
            {
                IList<ReaGoodsPrintBarCodeVO> entityList2 = entityList.list.OrderBy(p => p.GoodsID).ThenBy(p => p.LotNo).ThenBy(p => p.DispOrder).ToList();
                entityList.count = entityList2.Count;
                //分页处理
                if (entityList2.Count > 0)
                {
                    if (limit > 0 && limit < entityList2.Count)
                    {
                        int startIndex = limit * (page - 1);
                        int endIndex = limit;
                        var list = entityList2.Skip(startIndex).Take(endIndex);
                        if (list != null)
                        {
                            entityList.list = list.ToList();
                        }
                    }
                }
            }
            return entityList;
        }
        public EntityList<ReaGoodsPrintBarCodeVO> SearchReaGoodsPrintBarCodeVOListByInDtlId(long inDtlId, string boxHql, int page, int limit)
        {
            EntityList<ReaGoodsPrintBarCodeVO> entityList = new EntityList<ReaGoodsPrintBarCodeVO>();
            entityList.list = new List<ReaGoodsPrintBarCodeVO>();

            ReaBmsInDtl inDtl = IDReaBmsInDtlDao.Get(inDtlId);
            if (inDtl == null)
                return entityList;

            StringBuilder boxBarCodeHql = new StringBuilder();
            StringBuilder qtyHql = new StringBuilder();

            boxBarCodeHql.Append("reagoodsbarcodeoperation.BDtlID=" + inDtlId);
            qtyHql.Append("reabmsqtydtl.InDtlID=" + inDtlId);

            if (string.IsNullOrEmpty(boxBarCodeHql.ToString()))
                return entityList;
            char[] trimChars = new char[] { ' ', 'o', 'r' };
            string boxHql2 = "(" + boxBarCodeHql.ToString().TrimEnd(trimChars) + ") and reagoodsbarcodeoperation.BarcodeCreatType=" + ReaGoodsBarcodeOperationSerialType.条码生成.Key + " and reagoodsbarcodeoperation.OperTypeID=" + ReaGoodsBarcodeOperType.验货入库.Key;
            boxHql2.ToString().TrimEnd(trimChars);
            if (string.IsNullOrEmpty(boxHql))
            {
                boxHql = boxHql2;
            }
            else
            {
                boxHql = boxHql + " and (" + boxHql2 + ")";
            }

            IList<ReaBmsQtyDtl> qtyList = IDReaBmsQtyDtlDao.GetListByHQL("(" + qtyHql.ToString().TrimEnd(trimChars) + ")");
            IList<ReaBmsInDtl> inDtlList = new List<ReaBmsInDtl>();
            inDtlList.Add(inDtl);

            //入库明细的批条码集合信息
            if (inDtl.BarCodeType == int.Parse(ReaGoodsBarCodeType.批条码.Key))
            {
                var batchInDtlList = inDtlList.Where(p => p.BarCodeType == int.Parse(ReaGoodsBarCodeType.批条码.Key));
                if (batchInDtlList != null && batchInDtlList.Count() > 0)
                {
                    entityList.list = SearchBatchBarCodePrintVOList(batchInDtlList.ToList(), qtyList);
                }
            }

            //入库明细的盒条码集合信息
            var boxInDtlList = inDtlList.Where(p => p.BarCodeType == int.Parse(ReaGoodsBarCodeType.盒条码.Key));
            if (inDtlList != null && inDtlList.Count() > 0)
            {
                IList<ReaGoodsPrintBarCodeVO> voBoxList = SearchBoxBarCodePrintVOList(inDtlList, qtyList, boxHql);
                if (entityList.list != null && entityList.list.Count > 0)
                    entityList.list = voBoxList.Union(entityList.list).ToList();
                else
                    entityList.list = voBoxList;
            }
            if (entityList.list != null && entityList.list.Count > 0)
            {
                IList<ReaGoodsPrintBarCodeVO> entityList2 = entityList.list.OrderBy(p => p.GoodsID).ThenBy(p => p.LotNo).ThenBy(p => p.DispOrder).ToList();
                entityList.count = entityList2.Count;
                //分页处理
                if (entityList2.Count > 0)
                {
                    if (limit > 0 && limit < entityList2.Count)
                    {
                        int startIndex = limit * (page - 1);
                        int endIndex = limit;
                        var list = entityList2.Skip(startIndex).Take(endIndex);
                        if (list != null)
                        {
                            entityList.list = list.ToList();
                        }
                    }
                }
            }
            return entityList;
        }
        /// <summary>
        /// 获取入库货品明细的批条码信息
        /// </summary>
        /// <param name="batchInDtlList"></param>
        /// <param name="qtyList"></param>
        /// <returns></returns>
        private IList<ReaGoodsPrintBarCodeVO> SearchBatchBarCodePrintVOList(IList<ReaBmsInDtl> batchInDtlList, IList<ReaBmsQtyDtl> qtyList)
        {
            IList<ReaGoodsPrintBarCodeVO> voLotList = new List<ReaGoodsPrintBarCodeVO>();
            if ((batchInDtlList == null || batchInDtlList.Count <= 0) || (qtyList == null || qtyList.Count <= 0))
                return voLotList;

            foreach (var inDtl in batchInDtlList)
            {
                ReaGoodsPrintBarCodeVO vo = null;
                var tempQty = qtyList.Where(p => p.InDtlID == inDtl.Id);
                if (tempQty == null || tempQty.Count() <= 0)
                    continue;
                //如果库存货品的条码类型不是批条码,或者批条码值为空
                if (tempQty.ElementAt(0).BarCodeType != int.Parse(ReaGoodsBarCodeType.批条码.Key) || string.IsNullOrEmpty(tempQty.ElementAt(0).LotSerial))
                    continue;

                vo = new ReaGoodsPrintBarCodeVO();
                vo.GroupValue = tempQty.ElementAt(0).GoodsID.Value.ToString() + inDtl.LotNo;
                vo.Id = tempQty.ElementAt(0).Id;//vo的ID为库存ID,以方便条码打印后更新库存货品的条码打印次数
                vo.BarCodeType = inDtl.BarCodeType;
                vo.GoodsID = tempQty.ElementAt(0).GoodsID;
                vo.GoodsName = tempQty.ElementAt(0).GoodsName;
                if (inDtl.ReaGoods != null)
                {
                    vo.SName = inDtl.ReaGoods.SName;
                    vo.ShortCode = inDtl.ReaGoods.ShortCode;
                    vo.EName = inDtl.ReaGoods.EName;
                    vo.GoodsClass = inDtl.ReaGoods.GoodsClass;
                    vo.GoodsClassType = inDtl.ReaGoods.GoodsClassType;
                    vo.MinBarCodeQty = inDtl.ReaGoods.GonvertQty;
                }
                vo.GoodsUnit = tempQty.ElementAt(0).GoodsUnit;
                vo.UnitMemo = tempQty.ElementAt(0).UnitMemo;

                vo.Price = tempQty.ElementAt(0).Price;
                vo.PUsePackSerial = tempQty.ElementAt(0).Id.ToString();
                vo.UsePackSerial = tempQty.ElementAt(0).LotSerial;
                vo.UsePackQRCode = tempQty.ElementAt(0).LotQRCode;
                vo.LotQRCode = tempQty.ElementAt(0).LotQRCode;
                vo.LotSerial = tempQty.ElementAt(0).LotSerial;

                if (tempQty.ElementAt(0).InvalidDate.HasValue)
                    vo.InvalidDate = tempQty.ElementAt(0).InvalidDate;
                vo.ProdOrgNo = "";

                vo.CompOrgNo = tempQty.ElementAt(0).ReaServerCompCode;
                vo.SaleDocNo = inDtl.InDocNo;//入库总单号
                vo.DispOrder = 1;
                vo.GoodsQty = inDtl.GoodsQty;

                vo.ProdDate = tempQty.ElementAt(0).ProdDate;
                vo.BiddingNo = inDtl.BiddingNo;
                vo.RegisterInvalidDate = inDtl.RegisterInvalidDate;
                vo.SumTotal = tempQty.ElementAt(0).Price * vo.GoodsQty;
                vo.RegisterNo = inDtl.RegisterNo;

                vo.LotNo = inDtl.LotNo;
                vo.BDocID = inDtl.InDocID;
                vo.BDtlID = tempQty.ElementAt(0).Id;
                vo.QtyDtlID = tempQty.ElementAt(0).Id;
                vo.PrintCount = tempQty.ElementAt(0).PrintCount;
                vo.ReaGoodsNo = tempQty.ElementAt(0).ReaGoodsNo;
                vo.ProdGoodsNo = tempQty.ElementAt(0).ProdGoodsNo;
                vo.CenOrgGoodsNo = tempQty.ElementAt(0).CenOrgGoodsNo;
                vo.GoodsNo = tempQty.ElementAt(0).GoodsNo;
                if (vo != null)
                    voLotList.Add(vo);
            }
            return voLotList;
        }
        /// <summary>
        /// 获取入库货品明细的盒条码信息
        /// </summary>
        /// <param name="boxInDtlList">入库货品明细</param>
        /// <param name="qtyList">入库货品明细对应的库存信息</param>
        /// <param name="boxHql"></param>
        /// <returns></returns>
        private IList<ReaGoodsPrintBarCodeVO> SearchBoxBarCodePrintVOList(IList<ReaBmsInDtl> boxInDtlList, IList<ReaBmsQtyDtl> qtyList, string boxHql)
        {
            IList<ReaGoodsPrintBarCodeVO> voBoxList = new List<ReaGoodsPrintBarCodeVO>();
            if ((boxInDtlList == null || boxInDtlList.Count <= 0) || (qtyList == null || qtyList.Count <= 0))
                return voBoxList;

            IList<ReaGoodsBarcodeOperation> boxBarCodeList = this.SearchListByHQL(boxHql);
            if (boxBarCodeList == null || boxBarCodeList.Count <= 0)
                return voBoxList;

            boxBarCodeList = boxBarCodeList.OrderBy(p => p.DispOrder).ToList();
            foreach (var barCode in boxBarCodeList)
            {
                ReaGoodsPrintBarCodeVO vo = null;
                ////如果使用条码等于父条码,不加入条码集合里
                //if (barCode.UsePackSerial == barCode.PUsePackSerial)
                //    continue;

                //条码对应的库存信息
                var tempQty = qtyList.Where(p => (p.Id == barCode.QtyDtlID || p.Id == barCode.BDtlID));
                if (tempQty == null || tempQty.Count() <= 0)
                    continue;

                var inDtl = boxInDtlList.Where(p => p.Id == tempQty.ElementAt(0).InDtlID);
                if (inDtl == null || inDtl.Count() <= 0)
                    continue;

                vo = new ReaGoodsPrintBarCodeVO();
                vo.GroupValue = tempQty.ElementAt(0).GoodsID.Value.ToString() + inDtl.ElementAt(0).LotNo;
                vo.Id = barCode.Id;
                vo.BarCodeType = tempQty.ElementAt(0).BarCodeType;
                vo.GoodsID = tempQty.ElementAt(0).GoodsID;
                vo.GoodsName = tempQty.ElementAt(0).GoodsName;
                vo.MinBarCodeQty = barCode.MinBarCodeQty;

                if (inDtl.ElementAt(0).ReaGoods != null)
                {
                    vo.GoodsClass = inDtl.ElementAt(0).ReaGoods.GoodsClass;
                    vo.GoodsClassType = inDtl.ElementAt(0).ReaGoods.GoodsClassType;
                    vo.SName = inDtl.ElementAt(0).ReaGoods.SName;
                    vo.ShortCode = inDtl.ElementAt(0).ReaGoods.ShortCode;
                    vo.EName = inDtl.ElementAt(0).ReaGoods.EName;
                    if (!vo.MinBarCodeQty.HasValue || vo.MinBarCodeQty <= 0)
                        vo.MinBarCodeQty = inDtl.ElementAt(0).ReaGoods.GonvertQty;
                }
                vo.GoodsUnit = barCode.GoodsUnit;
                if (string.IsNullOrEmpty(barCode.UnitMemo))
                    vo.UnitMemo = tempQty.ElementAt(0).UnitMemo;
                else
                    vo.UnitMemo = barCode.UnitMemo;

                vo.Price = tempQty.ElementAt(0).Price;
                vo.PUsePackSerial = barCode.PUsePackSerial;
                vo.UsePackSerial = barCode.UsePackSerial;
                vo.UsePackQRCode = barCode.UsePackQRCode;
                if (tempQty.ElementAt(0).InvalidDate.HasValue)
                    vo.InvalidDate = tempQty.ElementAt(0).InvalidDate;

                vo.ProdOrgNo = "";
                vo.CompOrgNo = tempQty.ElementAt(0).ReaServerCompCode;
                vo.SaleDocNo = inDtl.ElementAt(0).InDocNo;//入库总单号              
                vo.DispOrder = barCode.DispOrder;

                vo.GoodsQty = barCode.GoodsQty;
                vo.ProdDate = tempQty.ElementAt(0).ProdDate;
                vo.BiddingNo = inDtl.ElementAt(0).BiddingNo;
                vo.RegisterInvalidDate = inDtl.ElementAt(0).RegisterInvalidDate;
                vo.SumTotal = tempQty.ElementAt(0).Price * vo.GoodsQty;

                vo.RegisterNo = inDtl.ElementAt(0).RegisterNo;
                vo.LotNo = barCode.LotNo;
                vo.BDocID = barCode.BDocID;
                vo.QtyDtlID = barCode.QtyDtlID;
                vo.BDtlID = barCode.BDtlID;
                vo.PrintCount = barCode.PrintCount;

                vo.ReaGoodsNo = tempQty.ElementAt(0).ReaGoodsNo;
                vo.ProdGoodsNo = tempQty.ElementAt(0).ProdGoodsNo;
                vo.CenOrgGoodsNo = tempQty.ElementAt(0).CenOrgGoodsNo;
                vo.GoodsNo = tempQty.ElementAt(0).GoodsNo;
                if (vo != null)
                    voBoxList.Add(vo);
            }
            return voBoxList;
        }
        #endregion

        #region 获取供货单货品(生成)条码信息
        public EntityList<ReaGoodsPrintBarCodeVO> SearchReaGoodsPrintBarCodeVOListBySaledocId(long saledocId, string dtlIdStr, int page, int limit)
        {
            EntityList<ReaGoodsPrintBarCodeVO> entityList = new EntityList<ReaGoodsPrintBarCodeVO>();
            entityList.list = new List<ReaGoodsPrintBarCodeVO>();

            string hql = "reabmscensaledtl.SaleDocID=" + saledocId;
            if (!string.IsNullOrEmpty(dtlIdStr))
                hql = hql + " and reabmscensaledtl.Id in(" + dtlIdStr.Trim().TrimEnd(',') + ")";
            IList<ReaBmsCenSaleDtl> saleDtlList = IDReaBmsCenSaleDtlDao.GetListByHQL(hql);
            if (saleDtlList == null || saleDtlList.Count <= 0)
                return entityList;

            StringBuilder boxBarCodeHql = new StringBuilder();
            IList<ReaGoods> reaGoodsList = new List<ReaGoods>();
            foreach (var saledtl in saleDtlList)
            {
                if (saledtl.IsPrintBarCode == 1)
                    boxBarCodeHql.Append("reagoodsbarcodeoperation.BDtlID=" + saledtl.Id + " or ");
            }
            if (string.IsNullOrEmpty(boxBarCodeHql.ToString()))
                return entityList;
            char[] trimChars = new char[] { ' ', 'o', 'r' };
            string boxHql = "(" + boxBarCodeHql.ToString().TrimEnd(trimChars) + ") and reagoodsbarcodeoperation.BarcodeCreatType=" + ReaGoodsBarcodeOperationSerialType.条码生成.Key + " and reagoodsbarcodeoperation.OperTypeID=" + ReaGoodsBarcodeOperType.供货.Key;

            //如果当前的供货明细条码类型为批条码
            var batchSaleDtlList = saleDtlList.Where(p => p.BarCodeType == int.Parse(ReaGoodsBarCodeType.批条码.Key));
            if (batchSaleDtlList != null && batchSaleDtlList.Count() > 0)
            {
                entityList.list = SearchBatchBarCodePrintVOList(batchSaleDtlList.ToList(), reaGoodsList);
            }

            //供货明细的盒条码集合信息
            var boxSaleDtlList = saleDtlList.Where(p => p.BarCodeType == int.Parse(ReaGoodsBarCodeType.盒条码.Key));
            if (boxSaleDtlList != null && boxSaleDtlList.Count() > 0)
            {
                IList<ReaGoodsPrintBarCodeVO> voBoxList = SearchBoxBarCodePrintVOList(boxSaleDtlList.ToList(), reaGoodsList, boxHql);
                if (entityList.list != null && entityList.list.Count > 0)
                    entityList.list = voBoxList.Union(entityList.list).ToList();
                else
                    entityList.list = voBoxList;
            }
            if (entityList.list != null && entityList.list.Count > 0)
            {
                entityList.list = entityList.list.OrderBy(p => p.GoodsID).ThenBy(p => p.LotNo).ThenBy(p => p.DispOrder).ToList();
                entityList.count = entityList.list.Count;
                //分页处理
                if (entityList.list.Count > 0)
                {
                    if (limit > 0 && limit < entityList.list.Count)
                    {
                        int startIndex = limit * (page - 1);
                        int endIndex = limit;
                        var list = entityList.list.Skip(startIndex).Take(endIndex);
                        if (list != null)
                        {
                            entityList.list = list.ToList();
                        }
                    }
                }
            }
            return entityList;
        }
        public EntityList<ReaGoodsPrintBarCodeVO> SearchReaGoodsPrintBarCodeVOListBySaleDtlId(long saleDtlId, int page, int limit)
        {
            EntityList<ReaGoodsPrintBarCodeVO> entityList = new EntityList<ReaGoodsPrintBarCodeVO>();
            entityList.list = new List<ReaGoodsPrintBarCodeVO>();

            ReaBmsCenSaleDtl saleDtl = IDReaBmsCenSaleDtlDao.Get(saleDtlId);
            if (saleDtl == null)
                return entityList;

            StringBuilder boxBarCodeHql = new StringBuilder();

            boxBarCodeHql.Append("reagoodsbarcodeoperation.BDtlID=" + saleDtlId);

            if (string.IsNullOrEmpty(boxBarCodeHql.ToString()))
                return entityList;
            char[] trimChars = new char[] { ' ', 'o', 'r' };
            string boxHql = "(" + boxBarCodeHql.ToString().TrimEnd(trimChars) + ") and reagoodsbarcodeoperation.BarcodeCreatType=" + ReaGoodsBarcodeOperationSerialType.条码生成.Key + " and reagoodsbarcodeoperation.OperTypeID=" + ReaGoodsBarcodeOperType.供货.Key;

            IList<ReaGoods> reaGoodsList = new List<ReaGoods>();
            IList<ReaBmsCenSaleDtl> saleDtlList = new List<ReaBmsCenSaleDtl>();
            saleDtlList.Add(saleDtl);

            ReaGoods reaGoods = IDReaGoodsDao.Get(saleDtl.ReaGoodsID.Value);
            if (reaGoods != null)
                reaGoodsList.Add(reaGoods);

            //供货明细的批条码集合信息
            if (saleDtl.BarCodeType == int.Parse(ReaGoodsBarCodeType.批条码.Key))
            {
                entityList.list = SearchBatchBarCodePrintVOList(saleDtlList, reaGoodsList);
            }

            //供货明细的盒条码集合信息
            var boxInDtlList = saleDtlList.Where(p => p.BarCodeType == int.Parse(ReaGoodsBarCodeType.盒条码.Key));
            if (saleDtlList != null && saleDtlList.Count() > 0)
            {
                IList<ReaGoodsPrintBarCodeVO> voBoxList = SearchBoxBarCodePrintVOList(saleDtlList, reaGoodsList, boxHql);
                if (entityList.list != null && entityList.list.Count > 0)
                    entityList.list = voBoxList.Union(entityList.list).ToList();
                else
                    entityList.list = voBoxList;
            }
            if (entityList.list != null && entityList.list.Count > 0)
            {
                entityList.list = entityList.list.OrderBy(p => p.GoodsID).ThenBy(p => p.LotNo).ThenBy(p => p.DispOrder).ToList();
                if (entityList.list != null)
                    entityList.count = entityList.list.Count;

                //分页处理
                if (entityList.list.Count > 0)
                {
                    if (limit > 0 && limit < entityList.list.Count)
                    {
                        int startIndex = limit * (page - 1);
                        int endIndex = limit;
                        var list = entityList.list.Skip(startIndex).Take(endIndex);
                        if (list != null)
                        {
                            entityList.list = list.ToList();
                        }
                    }
                }
            }
            return entityList;
        }
        /// <summary>
        /// 获取供货明细的货品批条码信息
        /// </summary>
        /// <param name="saleDtlList"></param>
        /// <param name="reaGoodsList"></param>
        /// <returns></returns>
        private IList<ReaGoodsPrintBarCodeVO> SearchBatchBarCodePrintVOList(IList<ReaBmsCenSaleDtl> saleDtlList, IList<ReaGoods> reaGoodsList)
        {
            IList<ReaGoodsPrintBarCodeVO> voLotList = new List<ReaGoodsPrintBarCodeVO>();
            if ((saleDtlList == null || saleDtlList.Count <= 0))
                return voLotList;

            foreach (var saleDtl in saleDtlList)
            {
                ReaGoodsPrintBarCodeVO vo = null;
                if (saleDtl.GoodsQty <= 0)
                    continue;
                //如果库存货品的条码类型不是批条码,或者批条码值为空
                if (saleDtl.BarCodeType != int.Parse(ReaGoodsBarCodeType.批条码.Key) || string.IsNullOrEmpty(saleDtl.LotSerial))
                    continue;

                ReaGoods reaGoods = null;
                if (reaGoods != null)
                    reaGoods = reaGoodsList.Where(p => p.Id == saleDtl.ReaGoodsID.Value).ElementAt(0);
                vo = new ReaGoodsPrintBarCodeVO();

                if (reaGoods != null)
                {
                    vo.SName = reaGoods.SName;
                    vo.ShortCode = reaGoods.ShortCode;
                    vo.EName = reaGoods.EName;
                    vo.GoodsClass = reaGoods.GoodsClass;
                    vo.GoodsClassType = reaGoods.GoodsClassType;
                    vo.MinBarCodeQty = reaGoods.GonvertQty;
                }
                vo.GroupValue = saleDtl.ReaGoodsID.Value.ToString() + saleDtl.LotNo;
                vo.Id = saleDtl.Id;//vo的ID为供货明细ID,以方便条码打印后更新库存货品的条码打印次数
                vo.BarCodeType = saleDtl.BarCodeType;
                vo.GoodsID = saleDtl.ReaGoodsID;
                vo.GoodsName = saleDtl.ReaGoodsName;

                vo.GoodsUnit = saleDtl.GoodsUnit;
                vo.UnitMemo = saleDtl.UnitMemo;
                vo.Price = saleDtl.Price;
                vo.PUsePackSerial = saleDtl.Id.ToString();
                vo.UsePackSerial = saleDtl.LotSerial;
                vo.UsePackQRCode = saleDtl.LotQRCode;
                vo.LotSerial = saleDtl.LotSerial;
                vo.LotQRCode = saleDtl.LotQRCode;

                if (saleDtl.InvalidDate.HasValue)
                    vo.InvalidDate = saleDtl.InvalidDate;
                vo.ProdOrgNo = "";
                vo.CompOrgNo = saleDtl.ReaServerCompCode;
                vo.SaleDocNo = saleDtl.SaleDocNo;//总单号

                vo.DispOrder = 1;
                vo.GoodsQty = saleDtl.GoodsQty;
                vo.ProdDate = saleDtl.ProdDate;
                vo.BiddingNo = saleDtl.BiddingNo;
                vo.RegisterInvalidDate = saleDtl.RegisterInvalidDate;

                vo.SumTotal = saleDtl.Price * vo.GoodsQty;
                vo.RegisterNo = saleDtl.RegisterNo;
                vo.LotNo = saleDtl.LotNo;
                vo.BDocID = saleDtl.SaleDocID;
                vo.BDtlID = saleDtl.Id;

                vo.PrintCount = saleDtl.PrintCount;
                vo.ReaGoodsNo = saleDtl.ReaGoodsNo;
                vo.ProdGoodsNo = saleDtl.ProdGoodsNo;
                vo.CenOrgGoodsNo = saleDtl.CenOrgGoodsNo;
                vo.GoodsNo = saleDtl.GoodsNo;

                if (vo != null)
                    voLotList.Add(vo);
            }
            return voLotList;
        }
        /// <summary>
        /// 获取供货明细的盒条码信息
        /// </summary>
        /// <param name="saleDtlList"></param>
        /// <param name="reaGoodsList"></param>
        /// <param name="boxHql"></param>
        /// <returns></returns>
        private IList<ReaGoodsPrintBarCodeVO> SearchBoxBarCodePrintVOList(IList<ReaBmsCenSaleDtl> saleDtlList, IList<ReaGoods> reaGoodsList, string boxHql)
        {
            IList<ReaGoodsPrintBarCodeVO> voBoxList = new List<ReaGoodsPrintBarCodeVO>();
            if (saleDtlList == null || saleDtlList.Count <= 0)
                return voBoxList;

            IList<ReaGoodsBarcodeOperation> boxBarCodeList = this.SearchListByHQL(boxHql);
            if (boxBarCodeList == null || boxBarCodeList.Count <= 0)
                return voBoxList;

            boxBarCodeList = boxBarCodeList.OrderBy(p => p.DispOrder).ToList();
            foreach (var barCode in boxBarCodeList)
            {
                ReaGoodsPrintBarCodeVO vo = null;
                //如果使用条码等于父条码,不加入条码集合里
                //if (barCode.UsePackSerial == barCode.PUsePackSerial)
                //    continue;

                var saleDtl = saleDtlList.Where(p => p.Id == barCode.BDtlID.Value).ElementAt(0);
                if (saleDtl == null)
                    continue;

                ReaGoods reaGoods = null;
                if (reaGoodsList != null && reaGoodsList.Count > 0)
                    reaGoods = reaGoodsList.Where(p => p.Id == saleDtl.ReaGoodsID.Value).ElementAt(0);

                vo = new ReaGoodsPrintBarCodeVO();
                if (reaGoods != null)
                {
                    vo.SName = reaGoods.SName;
                    vo.ShortCode = reaGoods.ShortCode;
                    vo.EName = reaGoods.EName;
                    vo.GoodsClass = reaGoods.GoodsClass;
                    vo.GoodsClassType = reaGoods.GoodsClassType;
                }
                vo.GroupValue = saleDtl.ReaGoodsID.Value.ToString() + saleDtl.LotNo;
                vo.Id = barCode.Id;
                vo.BarCodeType = saleDtl.BarCodeType;
                vo.GoodsID = saleDtl.ReaGoodsID;
                vo.GoodsName = saleDtl.ReaGoodsName;
                vo.GoodsUnit = saleDtl.GoodsUnit;

                if (string.IsNullOrEmpty(barCode.UnitMemo))
                    vo.UnitMemo = saleDtl.UnitMemo;
                else
                    vo.UnitMemo = barCode.UnitMemo;
                vo.Price = saleDtl.Price;
                vo.PUsePackSerial = barCode.PUsePackSerial;
                vo.UsePackSerial = barCode.UsePackSerial;
                vo.UsePackQRCode = barCode.UsePackQRCode;

                if (saleDtl.InvalidDate.HasValue)
                    vo.InvalidDate = saleDtl.InvalidDate;
                vo.ProdOrgNo = "";
                vo.CompOrgNo = saleDtl.ReaServerCompCode;
                vo.SaleDocNo = saleDtl.SaleDocNo;//总单号

                vo.DispOrder = barCode.DispOrder;
                vo.GoodsQty = barCode.GoodsQty;
                vo.MinBarCodeQty = barCode.MinBarCodeQty;
                vo.ProdDate = saleDtl.ProdDate;
                vo.BiddingNo = saleDtl.BiddingNo;
                vo.RegisterInvalidDate = saleDtl.RegisterInvalidDate;

                vo.SumTotal = saleDtl.Price * vo.GoodsQty;
                vo.RegisterNo = saleDtl.RegisterNo;
                vo.LotNo = barCode.LotNo;
                vo.BDocID = barCode.BDocID;
                vo.BDtlID = barCode.BDtlID;
                vo.QtyDtlID = barCode.QtyDtlID;

                vo.PrintCount = barCode.PrintCount;
                vo.ReaGoodsNo = saleDtl.ReaGoodsNo;
                vo.ProdGoodsNo = saleDtl.ProdGoodsNo;
                vo.CenOrgGoodsNo = saleDtl.CenOrgGoodsNo;
                vo.GoodsNo = saleDtl.GoodsNo;
                if (vo != null)
                    voBoxList.Add(vo);
            }
            return voBoxList;
        }
        #endregion
        public BaseResultBool UpdatePrintCount(IList<long> batchList, string lotType, IList<long> boxList)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            //盒条码的打印次数更新
            if (boxList != null && boxList.Count > 0)
                tempBaseResultBool = ((IDReaGoodsBarcodeOperationDao)base.DBDao).UpdatePrintCount(boxList);

            //批条码的打印次数更新
            switch (lotType)
            {
                case "ReaBmsCenSaleDtl":
                    tempBaseResultBool = IDReaBmsCenSaleDtlDao.UpdatePrintCount(batchList);
                    break;
                case "ReaBmsInDtl":
                    tempBaseResultBool = IDReaBmsQtyDtlDao.UpdatePrintCount(batchList);
                    break;
                default:
                    break;
            }
            return tempBaseResultBool;
        }
        public EntityList<ReaGoodsOrgLink> SearchReaGoodsOrgLinkBySanBarCode(string barcode)
        {
            EntityList<ReaGoodsOrgLink> entityList = new EntityList<ReaGoodsOrgLink>();
            if (barcode.Length > SysPublicSet.UseSerialNoLength)
            {
                Dictionary<string, Dictionary<string, string>> dicMultiBarCode = new Dictionary<string, Dictionary<string, string>>();
                int barCodeType = -1;//1:一维条码;2:二维条码
                BaseResultDataValue brdv = IBReaCenBarCodeFormat.DecodingSanBarCode(barcode, ref dicMultiBarCode, ref barCodeType);
                if (brdv.success == false)
                {
                    return entityList;
                }

                switch (barCodeType)
                {
                    case 1:
                        /***
                         * 如果解析条码规则类型为一维条码
                         * 盒条码信息:从货品条码操作记录里查找
                         **/
                        break;
                    case 2:
                        entityList = IBReaGoodsOrgLink.SearchReaGoodsOrgLinkByScanBarCode(barcode, dicMultiBarCode);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                //一维盒条码处理
                entityList = SearchReaGoodsOrgLinkOfBarcode(barcode);
            }
            return entityList;
        }
        public EntityList<ReaGoods> SearchReaGoodsBySanBarCode(string barcode)
        {
            EntityList<ReaGoods> entityList = new EntityList<ReaGoods>();
            if (barcode.Length > SysPublicSet.UseSerialNoLength)
            {
                Dictionary<string, Dictionary<string, string>> dicMultiBarCode = new Dictionary<string, Dictionary<string, string>>();
                int barCodeType = -1;//1:一维条码;2:二维条码
                BaseResultDataValue brdv = IBReaCenBarCodeFormat.DecodingSanBarCode(barcode, ref dicMultiBarCode, ref barCodeType);
                if (brdv.success == false)
                {
                    return entityList;
                }

                switch (barCodeType)
                {
                    case 1:
                        /***
                         * 如果解析条码规则类型为一维条码
                         * 盒条码信息:从货品条码操作记录里查找
                         **/
                        break;
                    case 2:
                        entityList = IBReaGoodsOrgLink.SearchReaGoodsByScanBarCode(barcode, dicMultiBarCode);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                entityList.list = new List<ReaGoods>();

                EntityList<ReaGoodsOrgLink> entityList2 = SearchReaGoodsOrgLinkOfBarcode(barcode);
                if (entityList2 != null && entityList2.count > 0)
                {
                    foreach (ReaGoodsOrgLink entity in entityList2.list)
                    {
                        if (entity.ReaGoods != null && !entityList.list.Contains(entity.ReaGoods))
                        {
                            entityList.list.Add(entity.ReaGoods);
                        }
                    }//foreach
                }
            }
            return entityList;
        }
        private EntityList<ReaGoodsOrgLink> SearchReaGoodsOrgLinkOfBarcode(string barcode)
        {
            EntityList<ReaGoodsOrgLink> entityList = new EntityList<ReaGoodsOrgLink>();
            string boxBarCodeHql = string.Format("reagoodsbarcodeoperation.BarcodeCreatType={0} and (reagoodsbarcodeoperation.UsePackSerial='{1}' or reagoodsbarcodeoperation.UsePackQRCode='{2}')", long.Parse(ReaGoodsBarcodeOperationSerialType.条码生成.Key), barcode, barcode);
            IList<ReaGoodsBarcodeOperation> operationList = this.SearchListByHQL(boxBarCodeHql);
            StringBuilder strb = new StringBuilder();
            foreach (ReaGoodsBarcodeOperation operation in operationList)
            {
                if (operation.CompGoodsLinkID.HasValue)
                {
                    strb.Append(" reagoodsorglink.Id=" + operation.CompGoodsLinkID.Value);
                    strb.Append(" or ");
                }
            }
            if (strb.Length > 0)
            {
                char[] trimChars = new char[] { ' ', 'o', 'r' };
                string hql = "(" + strb.ToString().TrimEnd(trimChars) + ")";
                entityList = IBReaGoodsOrgLink.SearchListByHQL(hql, "", 0, 0);
            }

            //如果条码操作记录没有,从库存信息获取批条码信息
            if (entityList.count <= 0)
            {
                string qtyHql = string.Format("reabmsqtydtl.GoodsQty>0 and (reabmsqtydtl.LotSerial='{0}' or reabmsqtydtl.LotQRCode='{1}')", barcode, barcode);
                IList<ReaBmsQtyDtl> qtyList = IDReaBmsQtyDtlDao.GetListByHQL(qtyHql);
                foreach (ReaBmsQtyDtl qty in qtyList)
                {
                    if (qty.CompGoodsLinkID.HasValue)
                    {
                        strb.Append(" reagoodsorglink.Id=" + qty.CompGoodsLinkID.Value);
                        strb.Append(" or ");
                    }
                }
                if (strb.Length > 0)
                {
                    char[] trimChars = new char[] { ' ', 'o', 'r' };
                    string hql = "(" + strb.ToString().TrimEnd(trimChars) + ")";
                    entityList = IBReaGoodsOrgLink.SearchListByHQL(hql, "", 0, 0);
                }
            }
            return entityList;
        }
        public double SearchOverageQty(string barcode, IList<ReaGoodsBarcodeOperation> barcodeAllList)
        {
            IList<ReaGoods> reaGoodsList = new List<ReaGoods>();
            double overageQty = 0;
            var groupByList = barcodeAllList.GroupBy(p => new
            {
                p.OperTypeID,
                p.GoodsUnit
            });
            //当前扫码条码的累计库存总数
            double overageAllQty = 0;
            //总共扫码用掉的条码数
            double outAllQty = 0;
            foreach (var groupBy in groupByList)
            {

                var operTypeID = groupBy.Key.OperTypeID.Value.ToString();
                //总共扫码用掉的条码数计算
                if (operTypeID == ReaGoodsBarcodeOperType.验货入库.Key || operTypeID == ReaGoodsBarcodeOperType.库存初始化.Key || operTypeID == ReaGoodsBarcodeOperType.移库入库.Key || operTypeID == ReaGoodsBarcodeOperType.退库入库.Key || operTypeID == ReaGoodsBarcodeOperType.盘盈入库.Key || operTypeID == ReaGoodsBarcodeOperType.借调入库.Key)
                {
                    overageAllQty = overageAllQty + SearchScanCodeQty(groupBy.ToList(), ref reaGoodsList);

                }
                else if (operTypeID == ReaGoodsBarcodeOperType.移库出库.Key || operTypeID == ReaGoodsBarcodeOperType.使用出库.Key || operTypeID == ReaGoodsBarcodeOperType.报损出库.Key || operTypeID == ReaGoodsBarcodeOperType.退供应商.Key || operTypeID == ReaGoodsBarcodeOperType.盘亏出库.Key)
                {
                    outAllQty = outAllQty + SearchScanCodeQty(groupBy.ToList(), ref reaGoodsList);
                }
            }
            ZhiFang.Common.Log.Log.Debug("当前扫码条码:" + barcode + ",当前扫码条码的累计库存总数.overageAllQty:" + overageAllQty);
            ZhiFang.Common.Log.Log.Debug("当前扫码条码:" + barcode + ",总共扫码用掉的条码数.outAllQty:" + outAllQty);
            overageQty = overageAllQty - outAllQty;
            ZhiFang.Common.Log.Log.Debug("当前扫码条码:" + barcode + ",当前条码剩余的可扫次数.overageQty:" + overageQty);
            if (overageQty < 0)
                overageQty = 0;
            return overageQty;
        }
        public double SearchScanCodeQty(IList<ReaGoodsBarcodeOperation> barcodeList, ref IList<ReaGoods> reaGoodsList)
        {
            ReaGoods reaGoods = null;
            double scanCodeQty = 0;
            ZhiFang.Common.Log.Log.Debug("reaGoodsList.Count=" + reaGoodsList.Count);
            foreach (var barcode in barcodeList)
            {
                ZhiFang.Common.Log.Log.Debug("当前扫码条码:" + barcode.UsePackQRCode + ",operTypeID:" + barcode.OperTypeID + ",Id:" + barcode.Id);
                long scanCodeGoodsID = 0;
                if (barcode.ScanCodeGoodsID.HasValue)
                {
                    scanCodeGoodsID = barcode.ScanCodeGoodsID.Value;
                }
                else
                {
                    scanCodeGoodsID = barcode.GoodsID.Value;
                }
                ZhiFang.Common.Log.Log.Debug("scanCodeGoodsID=" + scanCodeGoodsID);

                var tempList2 = reaGoodsList.Where(p => p.Id == scanCodeGoodsID);
                if (tempList2 != null && tempList2.Count() > 0)
                {
                    reaGoods = tempList2.ElementAt(0);
                    ZhiFang.Common.Log.Log.Debug("+reaGoods.Id=" + reaGoods.Id + ",reaGoods.CName=" + reaGoods.CName + ",reaGoods.ReaGoodsNo=" + reaGoods.ReaGoodsNo);
                }
                else
                {
                    reaGoods = IDReaGoodsDao.Get(scanCodeGoodsID);
                    if (reaGoods != null && !reaGoodsList.Contains(reaGoods))
                    {
                        ZhiFang.Common.Log.Log.Debug("-reaGoods.Id=" + reaGoods.Id + ",reaGoods.CName=" + reaGoods.CName + ",reaGoods.ReaGoodsNo=" + reaGoods.ReaGoodsNo);
                        reaGoodsList.Add(reaGoods);
                    }
                    else
                        ZhiFang.Common.Log.Log.Debug("reaGoods=NULL");
                }
                //兼容1.0.0.86之前生成的条码处理
                if (barcode.GoodsUnit != barcode.ScanCodeGoodsUnit && barcode.GoodsID == barcode.ScanCodeGoodsID)
                {
                    ZhiFang.Common.Log.Log.Debug("根据条件 reagoods.ReaGoodsNo='" + reaGoods.ReaGoodsNo + "' and reagoods.UnitName='" + barcode.ScanCodeGoodsUnit + "' 查询表Rea_Goods获取货品");

                    IList<ReaGoods> reaGoodsList2 = IDReaGoodsDao.GetListByHQL(" reagoods.ReaGoodsNo='" + reaGoods.ReaGoodsNo + "' and reagoods.UnitName='" + barcode.ScanCodeGoodsUnit + "' ");
                    if (reaGoodsList2 != null && reaGoodsList2.Count > 0)
                    {
                        reaGoods = reaGoodsList2[0];
                        if (reaGoods != null && !reaGoodsList.Contains(reaGoods))
                            reaGoodsList.Add(reaGoods);
                        else
                            ZhiFang.Common.Log.Log.Debug("根据条件能获取到货品信息，但是从reaGoodsList里找不到。");
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("根据条件未能获取到货品信息");
                    }
                }
                double scanCodeQty2 = 0;
                if (barcode.ScanCodeQty.HasValue)
                    scanCodeQty2 = barcode.ScanCodeQty.Value;
                if (reaGoods == null)
                {
                    ZhiFang.Common.Log.Log.Debug("reaGoods=NULL");
                }
                else
                {
                    scanCodeQty = scanCodeQty + (reaGoods.GonvertQty * scanCodeQty2);
                }
            }
            return scanCodeQty;
        }
        public IList<ReaGoodsBarcodeOperation> GetListByHQL(string strHqlWhere)
        {
            return DBDao.GetListByHQL(strHqlWhere);
        }
        public EntityList<ReaGoodsBarcodeOperation> SearchOverReaGoodsBarcodeOperationByHQL(string where, string sort, int page, int limit)
        {
            EntityList<ReaGoodsBarcodeOperation> entityList = new EntityList<ReaGoodsBarcodeOperation>();
            IList<ReaGoodsBarcodeOperation> operBarCodeList = new List<ReaGoodsBarcodeOperation>();

            IList<ReaGoodsBarcodeOperation> operationList = this.SearchListByHQL(where);
            var groupByList = operationList.GroupBy(p => new
            {
                p.UsePackQRCode
            });
            foreach (var groupBy in groupByList)
            {
                var tempList = groupBy.OrderByDescending(p => p.DataAddTime).ToList();
                //只要条码最后操作记录不是"出库"操作,就是剩余条码
                var operTypeID = tempList[0].OperTypeID.Value.ToString();
                if (operTypeID != ReaGoodsBarcodeOperType.移库出库.Key && operTypeID != ReaGoodsBarcodeOperType.使用出库.Key && operTypeID != ReaGoodsBarcodeOperType.报损出库.Key && operTypeID != ReaGoodsBarcodeOperType.退供应商.Key && operTypeID != ReaGoodsBarcodeOperType.盘亏出库.Key)
                {
                    if (tempList[0].OverageQty.HasValue && tempList[0].OverageQty.Value > 0)
                        operBarCodeList.Add(tempList[0]);
                }
            }

            entityList.count = operBarCodeList.Count;
            //分页处理
            if (operBarCodeList.Count > 0)
            {
                if (limit > 0 && limit < operBarCodeList.Count)
                {
                    int startIndex = limit * (page - 1);
                    int endIndex = limit;
                    var list = operBarCodeList.Skip(startIndex).Take(endIndex);
                    if (list != null)
                    {
                        operBarCodeList = list.ToList();
                    }
                }
            }
            entityList.list = operBarCodeList;
            return entityList;
        }

        #region 客户端与平台不在同一数据库--客户端部分
        public BaseResultDataValue AddBarcodeOperationListOfPlatformExtract(IList<ReaBmsCenSaleDtl> saleDtlList, IList<ReaGoodsBarcodeOperation> barcodeOperationList, long empID, string empName)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            foreach (var entity in barcodeOperationList)
            {
                if (baseresultdata.success == false) break;
                try
                {
                    ReaBmsCenSaleDtl saleDtl = saleDtlList.Where(p => p.Id == entity.BDtlID).ElementAt(0);
                    entity.GoodsLotID = saleDtl.GoodsLotID;
                    this.Entity = entity;
                    baseresultdata.success = this.Add();
                }
                catch (Exception ex)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = string.Format("新增供货单号为:{0},供货明细Id为:{1},货品名称为:{2}的货品条码信息失败", entity.BDocNo, entity.BDtlID, entity.GoodsCName);
                    ZhiFang.Common.Log.Log.Error("新增供货明细条码信息失败,错误信息:" + baseresultdata.ErrorInfo + ex.Message);
                    throw ex;
                }
            }
            return baseresultdata;
        }
        private void AddReaGoodsLot(ref ReaGoodsLot reaGoodsLot, long empID, string empName)
        {
            //if (this.Entity == null) return;
            //if (string.IsNullOrEmpty(this.Entity.LotNo)) return;
            //if (this.Entity.GoodsID == null) return;
            //if (!this.Entity.InvalidDate.HasValue) return;

            //ReaGoodsLot lot = new ReaGoodsLot();
            //lot.LabID = this.Entity.LabID;
            //lot.Visible = true;
            //lot.GoodsID = this.Entity.GoodsID.Value;
            //lot.ReaGoodsNo = this.Entity.ReaGoodsNo;
            //lot.LotNo = this.Entity.LotNo;
            ////lot.ProdDate = this.Entity.ProdDate;
            //lot.GoodsCName = this.Entity.GoodsCName;
            //lot.InvalidDate = this.Entity.InvalidDate;
            //lot.CreaterID = empID;
            //lot.CreaterName = empName;
            //IBReaGoodsLot.Entity = lot;
            //BaseResultBool baseResultBool = IBReaGoodsLot.AddAndValid(ref reaGoodsLot);
        }
        #endregion

        #region 赣南医学院附属第一医院，通过入库接口写入后，将对方的条码信息写入到条码操作表

        /// <summary>
        /// 货品属性，是否打印条码=否，不使用智方的规则生成条码
        /// 将HRP的条码信息保存到条码操作表
        /// 盒条码、不存在大小包装单位转换
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue AddReaGoodsBarcodeOperationByHRPInterface(IList<ReaBmsInDtl> inDtlList, long empID, string empName)
        {
            ZhiFang.Common.Log.Log.Info("将HRP条码写入到条码操作表开始");
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            IList<ReaBmsQtyDtl> qtyDtlList = IDReaBmsQtyDtlDao.GetListByHQL(string.Format("InDtlID in ({0})", string.Join(",", inDtlList.Select(p => p.Id).ToArray())));

            for (int i = 0; i < inDtlList.Count; i++)
            {
                var inDtl = inDtlList[i];
                ZhiFang.Common.Log.Log.Info("货品ID=" + inDtl.ReaGoods.Id + "，货品编码=" + inDtl.ReaGoods.ReaGoodsNo + "，货品名称=" + inDtl.ReaGoods.CName + "，HRP条码=" + inDtl.LotQRCode);

                if (inDtl.ReaGoods.IsPrintBarCode == 1)
                {
                    ZhiFang.Common.Log.Log.Info("当前货品属性：是否打印条码=是，不能保存HRP条码！");
                    continue;
                }

                var l = qtyDtlList.Where(p => p.InDtlID == inDtl.Id).ToList();
                if (l.Count == 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "未找到入库明细ID=" + inDtl.Id + "，HRP条码=" + inDtl.LotQRCode + "的库存记录信息！条码操作表保存失败！";
                    ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
                    break;
                }

                ReaBmsQtyDtl qtyDtl = l[0];
                ReaGoodsBarcodeOperation barcode = new ReaGoodsBarcodeOperation();
                barcode.LabID = inDtl.LabID;
                barcode.BarcodeCreatType = long.Parse(ReaGoodsBarcodeOperationSerialType.条码生成.Key);
                barcode.OperTypeID = long.Parse(ReaGoodsBarcodeOperType.验货入库.Key);
                barcode.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[barcode.OperTypeID.ToString()].Name;
                barcode.BDocID = inDtl.InDocID;

                barcode.BDocNo = inDtl.InDocNo;
                barcode.BDtlID = inDtl.Id;
                barcode.QtyDtlID = qtyDtl.Id;
                barcode.Visible = true;
                barcode.CreaterID = empID;

                barcode.CreaterName = empName;
                barcode.DataUpdateTime = DateTime.Now;
                barcode.GoodsID = qtyDtl.GoodsID;
                barcode.ScanCodeGoodsID = qtyDtl.GoodsID;
                barcode.GoodsUnitID = qtyDtl.GoodsUnitID;
                barcode.GoodsCName = qtyDtl.GoodsName;

                barcode.GoodsUnit = qtyDtl.GoodsUnit;
                barcode.GoodsLotID = qtyDtl.GoodsLotID;
                barcode.LotNo = qtyDtl.LotNo;
                barcode.ReaCompanyID = qtyDtl.ReaCompanyID;
                barcode.CompanyName = qtyDtl.CompanyName;
                barcode.SysPackSerial = inDtl.LotQRCode;
                barcode.UsePackQRCode = inDtl.LotQRCode;
                barcode.UsePackSerial = inDtl.LotQRCode;
                barcode.StorageID = qtyDtl.StorageID;
                barcode.PlaceID = qtyDtl.PlaceID;
                barcode.GoodsQty = qtyDtl.GoodsQty;
                barcode.MinBarCodeQty = 1;
                barcode.OverageQty = barcode.MinBarCodeQty;
                barcode.UnitMemo = qtyDtl.UnitMemo;

                barcode.DispOrder = i + 1;
                barcode.PUsePackSerial = barcode.Id.ToString();

                barcode.ReaGoodsNo = qtyDtl.ReaGoodsNo;
                barcode.ProdGoodsNo = qtyDtl.ProdGoodsNo;
                barcode.CenOrgGoodsNo = qtyDtl.CenOrgGoodsNo;
                barcode.GoodsNo = qtyDtl.GoodsNo;
                barcode.ReaCompCode = qtyDtl.ReaCompCode;

                barcode.GoodsSort = qtyDtl.GoodsSort;
                barcode.CompGoodsLinkID = qtyDtl.CompGoodsLinkID;
                barcode.BarCodeType = qtyDtl.BarCodeType;

                BaseResultBool baseResultBool = AddBarcodeOperation(barcode, 4, empID, empName, inDtl.LabID);
                if (!baseResultBool.success)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "入库明细ID=" + inDtl.Id + "，HRP条码=" + inDtl.LotQRCode + "保存到条码操作表失败！" + baseResultBool.ErrorInfo;
                    ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
                    break;
                }
            }
            ZhiFang.Common.Log.Log.Info("将HRP条码写入到条码操作表结束");
            return baseResultDataValue;
        }

        #endregion


    }
}