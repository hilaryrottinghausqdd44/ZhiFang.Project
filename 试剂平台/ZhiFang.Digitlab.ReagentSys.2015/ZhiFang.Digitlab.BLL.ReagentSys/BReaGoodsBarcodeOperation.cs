
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.BLL;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaConfirm;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaGoodsScanCode;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BReaGoodsBarcodeOperation : BaseBLL<ReaGoodsBarcodeOperation>, ZhiFang.Digitlab.IBLL.ReagentSys.IBReaGoodsBarcodeOperation
    {
        IDAO.ReagentSys.IDBmsCenSaleDtlConfirmDao IDBmsCenSaleDtlConfirmDao { get; set; }
        IBLL.ReagentSys.IBReaCenBarCodeFormat IBReaCenBarCodeFormat { get; set; }
        IBLL.ReagentSys.IBBmsCenOrderDtl IBBmsCenOrderDtl { get; set; }

        public BaseResultBool AddBarcodeOperationOfList(IList<ReaGoodsBarcodeOperation> dtAddList, long operTypeID, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.success = true;
            foreach (ReaGoodsBarcodeOperation operation in dtAddList)
            {
                if (operTypeID > 0)
                {
                    operation.OperTypeID = operTypeID;
                    operation.OperTypeName = ReaGoodsBarcodeOperType.GetStatusDic()[operation.OperTypeID.ToString()].Name;
                }
                operation.Visible = true;
                operation.CreaterID = empID;
                operation.CreaterName = empName;
                operation.DataUpdateTime = DateTime.Now;
                //系统内部码生成
                //operation.SysPackSerial = "";
                this.Entity = operation;
                tempBaseResultBool.success = this.Add();
                if (tempBaseResultBool.success == false)
                    tempBaseResultBool.ErrorInfo = "新增货品条码操作记录信息失败！";

                if (tempBaseResultBool.success == false) break;
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 客户端验收货品扫码
        /// </summary>
        /// <param name="reaCompID"></param>
        /// <param name="serialNo"></param>
        /// <param name="scanCodeType">扫码类型</param>
        /// <returns></returns>
        public ReaGoodsScanCodeVO DecodingReaGoodsScanCodeVOOfConfirmByCompIDAndSerialNo(long reaCompID, string serialNo, string scanCodeType, long orderId)
        {
            ReaGoodsScanCodeVO vo = new ReaGoodsScanCodeVO();
            vo.ReaBarCodeVOList = new List<ReaBarCodeVO>();
            if (string.IsNullOrEmpty(serialNo))
            {
                vo.ErrorInfo = string.Format("验收货品扫码--条码为空!");
                vo.BoolFlag = false;
                return vo;
            }
            IList<ReaGoodsBarcodeOperation> dtlBarCodeList = this.SearchListByHQL(string.Format("reagoodsbarcodeoperation.UsePackSerial='{0}' and (reagoodsbarcodeoperation.OperTypeID={1} or reagoodsbarcodeoperation.OperTypeID={2})", serialNo, ReaGoodsBarcodeOperType.验货接收.Key, ReaGoodsBarcodeOperType.验货拒收.Key));
            if (dtlBarCodeList.Count > 0)
            {
                ReaGoodsBarcodeOperation operation = dtlBarCodeList.OrderByDescending(p => p.DataAddTime).ToList()[0];
                vo.BoolFlag = false;
                vo.ErrorInfo = string.Format("条码为:{0},已被{1},验收明细Id为{2},操作人为{3}!", serialNo, ReaGoodsBarcodeOperType.GetStatusDic()[operation.OperTypeID.ToString()].Name, operation.BDtlID, operation.CreaterName);
                return vo;
            }
            //调用条码解码规则匹配
            vo = IBReaCenBarCodeFormat.DecodingReaGoodsScanCodeVOOfCompIDAndSerialNo(reaCompID, serialNo);
            if (vo.ReaBarCodeVOList != null && vo.ReaBarCodeVOList.Count > 0)
            {
                switch (scanCodeType)
                {
                    //订单验收(当条码所属的货品属于当前订单明细中)
                    case "reaorder":
                        EntityList<ReaOrderDtlVO> orderDtlVOList = IBBmsCenOrderDtl.SearchReaOrderDtlVOListByHQL(string.Format("bmscenorderdtl.BmsCenOrderDoc.Id={0}", orderId), "", -1, -1);
                        if (orderDtlVOList.list == null || orderDtlVOList.count <= 0)
                        {
                            vo.BoolFlag = false;
                            vo.ErrorInfo = string.Format("订单Id为{0},当前可验收的订单信息为空!", orderId);
                            return vo;
                        }
                        //订单明细的盒条码信息
                        var orderDtlVOList2 = orderDtlVOList.list.Where(p => p.BarCodeMgr == int.Parse(ReaGoodsBarCodeMgr.盒条码.Key));
                        var reaBarCodeVOList = vo.ReaBarCodeVOList;
                        for (int i = 0; i < reaBarCodeVOList.Count; i++)
                        {
                            ReaBarCodeVO model = reaBarCodeVOList[i];
                            var tempList = orderDtlVOList2.Where(p => p.OrderGoodsID.Value == model.ReaGoodsOrgLinkID.Value);
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
                        break;
                    default:
                        break;
                }
            }
            return vo;
        }
        /// <summary>
        /// 客户端入库货品扫码
        /// </summary>
        /// <param name="reaCompID"></param>
        /// <param name="serialNo"></param>
        /// <param name="docConfirmID"></param>
        /// <returns></returns>
        public ReaGoodsScanCodeVO DecodingReaGoodsScanCodeVOOfReaBmsInByCompIDAndSerialNo(long reaCompID, string serialNo, long docConfirmID)
        {
            ReaGoodsScanCodeVO vo = new ReaGoodsScanCodeVO();
            vo.ReaBarCodeVOList = new List<ReaBarCodeVO>();
            if (string.IsNullOrEmpty(serialNo))
            {
                vo.ErrorInfo = string.Format("验收货品扫码--条码为空!");
                vo.BoolFlag = false;
                return vo;
            }
            IList<ReaGoodsBarcodeOperation> dtlBarCodeList = this.SearchListByHQL(string.Format("reagoodsbarcodeoperation.UsePackSerial='{0}' and reagoodsbarcodeoperation.OperTypeID={1}", serialNo, ReaGoodsBarcodeOperType.验货入库.Key));
            if (dtlBarCodeList.Count > 0)
            {
                ReaGoodsBarcodeOperation operation = dtlBarCodeList.OrderByDescending(p => p.DataAddTime).ToList()[0];
                vo.BoolFlag = false;
                vo.ErrorInfo = string.Format("条码为:{0},已被扫码验货入库{1},入库明细Id为{2},操作人为{3}!", serialNo, ReaGoodsBarcodeOperType.GetStatusDic()[operation.OperTypeID.ToString()].Name, operation.BDtlID, operation.CreaterName);
                return vo;
            }
            //调用条码解码规则匹配
            vo = IBReaCenBarCodeFormat.DecodingReaGoodsScanCodeVOOfCompIDAndSerialNo(reaCompID, serialNo);
            return vo;
        }
    }
}