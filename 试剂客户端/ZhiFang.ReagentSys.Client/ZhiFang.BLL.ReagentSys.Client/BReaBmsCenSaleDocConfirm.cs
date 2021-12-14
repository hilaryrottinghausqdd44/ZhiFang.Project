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
using ZhiFang.Entity.RBAC;
using ZhiFang.Common.Public;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsCenSaleDocConfirm : BaseBLL<ReaBmsCenSaleDocConfirm>, IBReaBmsCenSaleDocConfirm
    {
        IBReaBmsCenSaleDtlConfirm IBReaBmsCenSaleDtlConfirm { get; set; }
        IDReaCenOrgDao IDReaCenOrgDao { get; set; }
        IBReaBmsCenOrderDoc IBReaBmsCenOrderDoc { get; set; }
        IBReaBmsCenOrderDtl IBReaBmsCenOrderDtl { get; set; }
        IBReaBmsCenSaleDoc IBReaBmsCenSaleDoc { get; set; }
        IBReaBmsCenSaleDtl IBReaBmsCenSaleDtl { get; set; }
        IBReaCheckInOperation IBReaCheckInOperation { get; set; }
        IBLL.RBAC.IBHRDept IBHRDept { get; set; }

        #region 手工验收
        public BaseResultDataValue AddReaSaleDocConfirmOfManualInput(ReaBmsCenSaleDocConfirm entity, IList<ReaSaleDtlConfirmVO> dtAddList, int secAccepterType, string codeScanningMode, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            try
            {
                if (dtAddList != null && dtAddList.Count > 0)
                {
                    entity.TotalPrice = dtAddList.Sum(p => p.ReaBmsCenSaleDtlConfirm.SumTotal);
                    BaseResultBool tempBaseResultBool = IBReaBmsCenSaleDtlConfirm.EditBmsCenSaleDtlConfirmValid(dtAddList, codeScanningMode);
                    if (tempBaseResultBool.success == false)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                        return baseResultDataValue;
                    }

                    if (entity.Status == 0)
                        entity.Status = int.Parse(ReaBmsCenSaleDocConfirmStatus.待继续验收.Key);
                    entity.StatusName = ReaBmsCenSaleDocConfirmStatus.GetStatusDic()[entity.Status.ToString()].Name;
                    if (!entity.AcceptTime.HasValue)
                        entity.AcceptTime = DateTime.Now;
                    if (!entity.AccepterID.HasValue || string.IsNullOrEmpty(entity.AccepterName))
                    {
                        entity.AccepterID = empID;
                        entity.AccepterName = empName;
                    }
                    if (string.IsNullOrEmpty(entity.SaleDocConfirmNo))
                        entity.SaleDocConfirmNo = this.GetSaleDocConfirmNo();
                    bool IsAcceptError = dtAddList.Sum(p => p.ReaBmsCenSaleDtlConfirm.RefuseCount) > 0 ? true : false;
                    entity.IsAcceptError = IsAcceptError;
                    this.Entity = entity;
                    GetReaServerCompCode();

                    if (this.Add())
                    {
                        tempBaseResultBool = IBReaBmsCenSaleDtlConfirm.AddDtlConfirmOfList(entity, dtAddList, codeScanningMode, empID, empName);
                        baseResultDataValue.success = tempBaseResultBool.success;
                        baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                        if (baseResultDataValue.success)
                            AddReaCheckInOperation(entity, empID, empName);

                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "错误信息：保存验收主单信息失败！";
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：验收明细信息为空！";
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("Exception：" + ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultBool EditReaSaleDocConfirmOfManualInput(string[] tempArray, IList<ReaSaleDtlConfirmVO> dtAddList, IList<ReaSaleDtlConfirmVO> dtEditList, int secAccepterType, string codeScanningMode, long empID, string empName, string fieldsDtl)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            ReaBmsCenSaleDocConfirm serverEntity = this.Get(this.Entity.Id);
            List<string> tmpa = tempArray.ToList();
            if (!EditBmsCenSaleDocConfirmStatusCheck(this.Entity, serverEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "验收单ID：" + this.Entity.Id + "的状态为：" + ReaBmsCenSaleDtlConfirmStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }
            bool IsAcceptError = false;
            if (dtAddList != null && dtAddList.Count > 0) IsAcceptError = dtAddList.Sum(p => p.ReaBmsCenSaleDtlConfirm.RefuseCount) > 0 ? true : false;
            if (IsAcceptError == false && dtEditList != null && dtEditList.Count > 0) IsAcceptError = dtEditList.Sum(p => p.ReaBmsCenSaleDtlConfirm.RefuseCount) > 0 ? true : false;
            tmpa.Add("IsAcceptError=" + IsAcceptError);
            tempArray = tmpa.ToArray();

            if (dtAddList != null && dtAddList.Count > 0)
            {
                tempBaseResultBool = IBReaBmsCenSaleDtlConfirm.EditBmsCenSaleDtlConfirmValid(dtAddList, codeScanningMode);
                if (tempBaseResultBool.success == false)
                    return tempBaseResultBool;
            }
            if (dtEditList != null && dtEditList.Count > 0)
            {
                tempBaseResultBool = IBReaBmsCenSaleDtlConfirm.EditBmsCenSaleDtlConfirmValid(dtEditList, codeScanningMode);
                if (tempBaseResultBool.success == false)
                    return tempBaseResultBool;
            }

            if (this.Update(tempArray))
            {
                if (dtAddList != null && dtAddList.Count > 0)
                    tempBaseResultBool = IBReaBmsCenSaleDtlConfirm.AddDtlConfirmOfList(this.Entity, dtAddList, codeScanningMode, empID, empName);
                //验收明细更新
                if (tempBaseResultBool.success == true && dtEditList != null && dtEditList.Count > 0)
                    tempBaseResultBool = IBReaBmsCenSaleDtlConfirm.EditDtlConfirmOfList(this.Entity, dtEditList, fieldsDtl, codeScanningMode, empID, empName);
            }
            else
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "验收单ID：" + this.Entity.Id + "更新失败！";
                return tempBaseResultBool;
            }
            if (tempBaseResultBool.success == true && this.Entity.Status != serverEntity.Status)
            {
                ReaBmsCenSaleDocConfirm tempEntity = new ReaBmsCenSaleDocConfirm();
                tempEntity.Id = this.Entity.Id;
                tempEntity.Status = this.Entity.Status;
                tempEntity.StatusName = ReaBmsCenSaleDocConfirmStatus.GetStatusDic()[tempEntity.Status.ToString()].Name;
                if (tempBaseResultBool.success) AddReaCheckInOperation(tempEntity, empID, empName);
            }
            return tempBaseResultBool;
        }

        #endregion

        #region 订单验收
        public BaseResultDataValue AddReaSaleDocConfirmOfOrder(IList<ReaSaleDtlConfirmVO> dtAddList, int secAccepterType, string codeScanningMode, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            this.Entity.StatusName = ReaBmsCenSaleDtlConfirmStatus.GetStatusDic()[this.Entity.Status.ToString()].Name;

            BaseResultBool tempBaseResultBool2 = IBReaBmsCenSaleDtlConfirm.EditBmsCenSaleDtlConfirmValidOfOrder(dtAddList, codeScanningMode);
            baseResultDataValue.success = tempBaseResultBool2.success;
            baseResultDataValue.ErrorInfo = tempBaseResultBool2.ErrorInfo;
            if (baseResultDataValue.success == false) return baseResultDataValue;

            try
            {
                if (!this.Entity.AcceptTime.HasValue)
                    this.Entity.AcceptTime = DateTime.Now;
                if (!this.Entity.AccepterID.HasValue || string.IsNullOrEmpty(this.Entity.AccepterName))
                {
                    this.Entity.AccepterID = empID;
                    this.Entity.AccepterName = empName;
                }
                bool IsAcceptError = dtAddList.Sum(p => p.ReaBmsCenSaleDtlConfirm.RefuseCount) > 0 ? true : false;
                this.Entity.IsAcceptError = IsAcceptError;
                if (string.IsNullOrEmpty(this.Entity.SaleDocConfirmNo))
                    this.Entity.SaleDocConfirmNo = this.GetSaleDocConfirmNo();
                GetReaServerCompCode();

                this.Entity.TotalPrice = dtAddList.Sum(p => p.ReaBmsCenSaleDtlConfirm.SumTotal);

                if (this.Add())
                {
                    BaseResultBool tempBaseResultBool = IBReaBmsCenSaleDtlConfirm.AddDtlConfirmOfList(this.Entity, dtAddList, codeScanningMode, empID, empName);
                    if (tempBaseResultBool.success == false)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                        return baseResultDataValue;
                    }

                    if (this.Entity.Status == int.Parse(ReaBmsCenSaleDtlConfirmStatus.已验收.Key))
                    {
                        ReaBmsCenOrderDoc orderDoc = new ReaBmsCenOrderDoc();
                        orderDoc.Id = this.Entity.OrderDocID.Value;

                        //更新订单状态
                        bool isAllConfirm = SearchBmsCenOrderDtlValidIsAllConfirmByOrderId(this.Entity.OrderDocID.Value, dtAddList);
                        if (isAllConfirm == true)
                            orderDoc.Status = int.Parse(ReaBmsOrderDocStatus.全部验收.Key);
                        else
                            orderDoc.Status = int.Parse(ReaBmsOrderDocStatus.部分验收.Key);
                        BaseResultBool tempBaseResultBool3 = EditReaBmsCenOrderDoc(orderDoc, empID, empName);
                        baseResultDataValue.success = tempBaseResultBool3.success;
                        baseResultDataValue.ErrorInfo = tempBaseResultBool3.ErrorInfo;
                    }
                    AddReaCheckInOperation(this.Entity, empID, empName);
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "订单验收--新增供货验收单失败！";
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("订单验收--新增供货验收单失败！：" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public BaseResultBool EditReaSaleDocConfirmOfOrder(string[] tempArray, IList<ReaSaleDtlConfirmVO> dtAddList, IList<ReaSaleDtlConfirmVO> dtEditList, int secAccepterType, string codeScanningMode, long empID, string empName, string fieldsDtl)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            ReaBmsCenSaleDocConfirm serverEntity = this.Get(this.Entity.Id);
            List<string> tmpa = tempArray.ToList();
            if (!EditBmsCenSaleDocConfirmStatusCheck(this.Entity, serverEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "验收单ID：" + this.Entity.Id + "的状态为：" + ReaBmsCenSaleDtlConfirmStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }

            #region 保存前验证处理
            if (dtAddList != null && dtAddList.Count > 0)
            {
                tempBaseResultBool = IBReaBmsCenSaleDtlConfirm.EditBmsCenSaleDtlConfirmValidOfOrder(dtAddList, codeScanningMode);
            }

            if (tempBaseResultBool.success == true && dtEditList != null && dtEditList.Count > 0)
            {
                tempBaseResultBool = IBReaBmsCenSaleDtlConfirm.EditBmsCenSaleDtlConfirmValidOfOrder(dtEditList, codeScanningMode);
            }
            if (tempBaseResultBool.success == false) return tempBaseResultBool;

            #endregion
            bool IsAcceptError = false;
            if (dtAddList != null && dtAddList.Count > 0) IsAcceptError = dtAddList.Sum(p => p.ReaBmsCenSaleDtlConfirm.RefuseCount) > 0 ? true : false;
            if (IsAcceptError == false && dtEditList != null && dtEditList.Count > 0) IsAcceptError = dtEditList.Sum(p => p.ReaBmsCenSaleDtlConfirm.RefuseCount) > 0 ? true : false;
            tmpa.Add("IsAcceptError=" + IsAcceptError);
            tempArray = tmpa.ToArray();

            if (this.Update(tempArray))
            {
                if (dtAddList != null && dtAddList.Count > 0)
                    tempBaseResultBool = IBReaBmsCenSaleDtlConfirm.AddDtlConfirmOfList(this.Entity, dtAddList, codeScanningMode, empID, empName);
                //验收明细更新
                if (tempBaseResultBool.success == true && dtEditList != null && dtEditList.Count > 0)
                    tempBaseResultBool = IBReaBmsCenSaleDtlConfirm.EditDtlConfirmOfList(this.Entity, dtEditList, fieldsDtl, codeScanningMode, empID, empName);

                if (this.Entity.Status == int.Parse(ReaBmsCenSaleDtlConfirmStatus.已验收.Key))
                {
                    ReaBmsCenOrderDoc orderDoc = new ReaBmsCenOrderDoc();
                    orderDoc.Id = this.Entity.OrderDocID.Value;
                    //订货单是否全部验收完,如果验收完,需要更新订单状态
                    IList<ReaSaleDtlConfirmVO> dtlConfirmVOList = dtAddList.Concat(dtEditList).ToList();
                    bool isAllConfirm = SearchBmsCenOrderDtlValidIsAllConfirmByOrderId(orderDoc.Id, dtlConfirmVOList);
                    if (isAllConfirm == true)
                        orderDoc.Status = int.Parse(ReaBmsOrderDocStatus.全部验收.Key);
                    else
                        orderDoc.Status = int.Parse(ReaBmsOrderDocStatus.部分验收.Key);
                    tempBaseResultBool = EditReaBmsCenOrderDoc(orderDoc, empID, empName);
                }
            }
            else
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "验收单ID：" + this.Entity.Id + "更新失败！";
                return tempBaseResultBool;
            }

            if (tempBaseResultBool.success == true && this.Entity.Status != serverEntity.Status)
            {
                ReaBmsCenSaleDocConfirm tempEntity = new ReaBmsCenSaleDocConfirm();
                tempEntity.Id = this.Entity.Id;
                tempEntity.Status = this.Entity.Status;
                tempEntity.StatusName = ReaBmsCenSaleDocConfirmStatus.GetStatusDic()[tempEntity.Status.ToString()].Name;
                if (tempBaseResultBool.success) AddReaCheckInOperation(tempEntity, empID, empName);
            }
            return tempBaseResultBool;
        }
        private BaseResultBool EditReaBmsCenOrderDoc(ReaBmsCenOrderDoc entity, long empID, string empName)
        {
            entity.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
            List<string> tmpa = new List<string>();
            tmpa.Add("Id=" + entity.Id + " ");
            tmpa.Add("Status=" + entity.Status + " ");
            tmpa.Add("StatusName='" + entity.StatusName + "' ");
            BaseResultBool tempBaseResultBool = IBReaBmsCenOrderDoc.EditReaBmsCenOrderDocAndDt(entity, tmpa.ToArray(), null, null, "", empID, empName);
            return tempBaseResultBool;
        }
        /// <summary>
        /// 判断某一订单是否完全验收完
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="dtAddList">当次新增保存的订单验收明细</param>
        /// <returns></returns>
        private bool SearchBmsCenOrderDtlValidIsAllConfirmByOrderId(long orderId, IList<ReaSaleDtlConfirmVO> dtAddList)
        {
            bool isAllConfirm = true;
            IList<ReaBmsCenOrderDtl> orderDtlList = IBReaBmsCenOrderDtl.SearchListByHQL(string.Format("reabmscenorderdtl.OrderDocID={0}", orderId));
            IList<ReaBmsCenSaleDtlConfirm> dtlConfirmList = dtlConfirmList = IBReaBmsCenSaleDtlConfirm.SearchListByHQL(string.Format("reabmscensaledtlconfirm.OrderDocID={0}", orderId));
            if (dtAddList != null)
            {
                for (int i = 0; i < dtAddList.Count; i++)
                {
                    var entity = dtAddList[i].ReaBmsCenSaleDtlConfirm;
                    //当前编辑的验收明细是否存在数据库中
                    var dtlConfirm = dtlConfirmList.Where(p => p.Id == entity.Id);
                    //以当次编辑的数据作比较
                    if (dtlConfirm.Count() > 0)
                        dtlConfirmList.Remove(dtlConfirm.ElementAt(0));
                    dtlConfirmList.Add(entity);
                }
            }

            if (orderDtlList != null && dtlConfirmList != null)
            {
                foreach (var model in orderDtlList)
                {
                    var dtlList = dtlConfirmList.Where(p => p.OrderDtlID.Value == model.Id).ToList();
                    //某一货品明细可验收数默认等于订单购进数
                    double confirmCount = 0;
                    if (model.GoodsQty.HasValue)
                        confirmCount = model.GoodsQty.Value;
                    if (dtlList != null && dtlList.Count() > 0)
                        confirmCount = confirmCount - (dtlList.Sum(p => p.AcceptCount) + dtlList.Sum(p => p.RefuseCount));
                    //订单其中的某一货品明细的可验收数大于0,订单为可继续验收
                    if (confirmCount > 0)
                    {
                        isAllConfirm = false;
                        break;
                    }
                }
            }
            else
                isAllConfirm = false;
            return isAllConfirm;
        }
        #endregion

        #region 供货验收
        public BaseResultDataValue AddReaSaleDocConfirmOfSale(IList<ReaSaleDtlConfirmVO> dtAddList, int secAccepterType, string codeScanningMode, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            this.Entity.StatusName = ReaBmsCenSaleDtlConfirmStatus.GetStatusDic()[this.Entity.Status.ToString()].Name;

            BaseResultBool tempBaseResultBool2 = IBReaBmsCenSaleDtlConfirm.EditBmsCenSaleDtlConfirmValidOfSale(dtAddList, codeScanningMode);
            baseResultDataValue.success = tempBaseResultBool2.success;
            baseResultDataValue.ErrorInfo = tempBaseResultBool2.ErrorInfo;
            if (baseResultDataValue.success == false) return baseResultDataValue;

            try
            {
                this.Entity.TotalPrice = dtAddList.Sum(p => p.ReaBmsCenSaleDtlConfirm.SumTotal);
                if (!this.Entity.AcceptTime.HasValue)
                    this.Entity.AcceptTime = DateTime.Now;
                if (!this.Entity.AccepterID.HasValue || string.IsNullOrEmpty(this.Entity.AccepterName))
                {
                    this.Entity.AccepterID = empID;
                    this.Entity.AccepterName = empName;
                }
                bool IsAcceptError = dtAddList.Sum(p => p.ReaBmsCenSaleDtlConfirm.RefuseCount) > 0 ? true : false;
                this.Entity.IsAcceptError = IsAcceptError;
                if (string.IsNullOrEmpty(this.Entity.SaleDocConfirmNo))
                    this.Entity.SaleDocConfirmNo = this.GetSaleDocConfirmNo();
                GetReaServerCompCode();

                if (this.Add())
                {
                    BaseResultBool tempBaseResultBool = IBReaBmsCenSaleDtlConfirm.AddDtlConfirmOfList(this.Entity, dtAddList, codeScanningMode, empID, empName);
                    if (tempBaseResultBool.success == false)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                        return baseResultDataValue;
                    }

                    if (this.Entity.Status == int.Parse(ReaBmsCenSaleDtlConfirmStatus.已验收.Key))
                    {
                        //更新供货单及供货明细状态
                        ReaBmsCenSaleDoc saleDoc = IBReaBmsCenSaleDoc.Get(this.Entity.SaleDocID.Value);
                        bool isAllConfirm = SearchBmsCenOrderDtlValidIsAllConfirmBySaleDocID(this.Entity.SaleDocID.Value, dtAddList);
                        if (isAllConfirm == true)
                            saleDoc.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.全部验收.Key);
                        else
                            saleDoc.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.部分验收.Key);
                        BaseResultBool tempBaseResultBool3 = IBReaBmsCenSaleDoc.EditReaBmsCenSaleDocAndDtlOfConfirm(saleDoc, empID, empName);
                        baseResultDataValue.success = tempBaseResultBool3.success;
                        baseResultDataValue.ErrorInfo = tempBaseResultBool3.ErrorInfo;
                    }
                    AddReaCheckInOperation(this.Entity, empID, empName);
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "订单验收--新增供货验收单失败！";
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("订单验收--新增供货验收单失败！：" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public BaseResultBool EditReaSaleDocConfirmOfSale(string[] tempArray, IList<ReaSaleDtlConfirmVO> dtAddList, IList<ReaSaleDtlConfirmVO> dtEditList, int secAccepterType, string codeScanningMode, long empID, string empName, string fieldsDtl)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            ReaBmsCenSaleDocConfirm serverEntity = this.Get(this.Entity.Id);
            List<string> tmpa = tempArray.ToList();
            if (!EditBmsCenSaleDocConfirmStatusCheck(this.Entity, serverEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "验收单ID：" + this.Entity.Id + "的状态为：" + ReaBmsCenSaleDtlConfirmStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }

            #region 保存前验证处理
            if (dtAddList != null && dtAddList.Count > 0)
            {
                tempBaseResultBool = IBReaBmsCenSaleDtlConfirm.EditBmsCenSaleDtlConfirmValidOfSale(dtAddList, codeScanningMode);
            }

            if (tempBaseResultBool.success == true && dtEditList != null && dtEditList.Count > 0)
            {
                tempBaseResultBool = IBReaBmsCenSaleDtlConfirm.EditBmsCenSaleDtlConfirmValidOfSale(dtEditList, codeScanningMode);
            }
            if (tempBaseResultBool.success == false) return tempBaseResultBool;

            #endregion

            bool IsAcceptError = false;
            if (dtAddList != null && dtAddList.Count > 0) IsAcceptError = dtAddList.Sum(p => p.ReaBmsCenSaleDtlConfirm.RefuseCount) > 0 ? true : false;
            if (IsAcceptError == false && dtEditList != null && dtEditList.Count > 0) IsAcceptError = dtEditList.Sum(p => p.ReaBmsCenSaleDtlConfirm.RefuseCount) > 0 ? true : false;
            tmpa.Add("IsAcceptError=" + IsAcceptError);
            tempArray = tmpa.ToArray();

            if (this.Update(tempArray))
            {
                if (dtAddList != null && dtAddList.Count > 0)
                    tempBaseResultBool = IBReaBmsCenSaleDtlConfirm.AddDtlConfirmOfList(this.Entity, dtAddList, codeScanningMode, empID, empName);
                //验收明细更新
                if (tempBaseResultBool.success == true && dtEditList != null && dtEditList.Count > 0)
                    tempBaseResultBool = IBReaBmsCenSaleDtlConfirm.EditDtlConfirmOfList(this.Entity, dtEditList, fieldsDtl, codeScanningMode, empID, empName);

                if (this.Entity.Status == int.Parse(ReaBmsCenSaleDtlConfirmStatus.已验收.Key))
                {
                    //更新供货单及供货明细状态
                    ReaBmsCenSaleDoc saleDoc = IBReaBmsCenSaleDoc.Get(this.Entity.SaleDocID.Value);
                    IList<ReaSaleDtlConfirmVO> dtlConfirmVOList = dtAddList.Concat(dtEditList).ToList();
                    bool isAllConfirm = SearchBmsCenOrderDtlValidIsAllConfirmBySaleDocID(this.Entity.SaleDocID.Value, dtlConfirmVOList);
                    if (isAllConfirm == true)
                        saleDoc.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.全部验收.Key);
                    else
                        saleDoc.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.部分验收.Key);
                    tempBaseResultBool = IBReaBmsCenSaleDoc.EditReaBmsCenSaleDocAndDtlOfConfirm(saleDoc, empID, empName);
                }
            }
            else
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "验收单ID：" + this.Entity.Id + "更新失败！";
                return tempBaseResultBool;
            }

            if (tempBaseResultBool.success == true && this.Entity.Status != serverEntity.Status)
            {
                ReaBmsCenSaleDocConfirm tempEntity = new ReaBmsCenSaleDocConfirm();
                tempEntity.Id = this.Entity.Id;
                tempEntity.Status = this.Entity.Status;
                tempEntity.StatusName = ReaBmsCenSaleDocConfirmStatus.GetStatusDic()[tempEntity.Status.ToString()].Name;
                if (tempBaseResultBool.success) AddReaCheckInOperation(tempEntity, empID, empName);
            }
            return tempBaseResultBool;
        }
        private bool SearchBmsCenOrderDtlValidIsAllConfirmBySaleDocID(long saleDocID, IList<ReaSaleDtlConfirmVO> dtAddList)
        {
            bool isAllConfirm = true;
            IList<ReaBmsCenSaleDtl> saleDtlList = IBReaBmsCenSaleDtl.SearchListByHQL(string.Format("reabmscensaledtl.SaleDocID={0}", saleDocID));

            StringBuilder idStr = new StringBuilder();
            foreach (var model in saleDtlList)
            {
                idStr.Append(model.Id + ",");
            }
            IList<ReaBmsCenSaleDtlConfirm> dtlConfirmList = IBReaBmsCenSaleDtlConfirm.SearchListByHQL(string.Format("reabmscensaledtlconfirm.SaleDtlID in ({0})", idStr.ToString().TrimEnd(',')));
            if (dtAddList != null)
            {
                for (int i = 0; i < dtAddList.Count; i++)
                {
                    var entity = dtAddList[i].ReaBmsCenSaleDtlConfirm;
                    //当前编辑的验收明细是否存在数据库中
                    var dtlConfirm = dtlConfirmList.Where(p => p.Id == entity.Id);
                    //以当次编辑的数据作比较
                    if (dtlConfirm.Count() > 0)
                        dtlConfirmList.Remove(dtlConfirm.ElementAt(0));
                    dtlConfirmList.Add(entity);
                }
            }

            if (saleDtlList != null && dtlConfirmList != null)
            {
                foreach (var model in saleDtlList)
                {
                    var dtlList = dtlConfirmList.Where(p => p.SaleDtlID.Value == model.Id).ToList();
                    //某一货品明细可验收数默认等于供货购进数
                    double confirmCount = model.GoodsQty.Value;
                    if (dtlList != null && dtlList.Count() > 0)
                        confirmCount = confirmCount - (dtlList.Sum(p => p.AcceptCount) + dtlList.Sum(p => p.RefuseCount));
                    //供货其中的某一货品明细的可验收数大于0,供货为可继续验收
                    if (confirmCount > 0)
                    {
                        isAllConfirm = false;
                        break;
                    }
                }
            }
            else
                isAllConfirm = false;
            return isAllConfirm;
        }
        #endregion

        #region 其他
        private void GetReaServerCompCode()
        {
            ReaCenOrg reaCenOrg = null;
            if (string.IsNullOrEmpty(this.Entity.ReaServerCompCode) && this.Entity.CompID.HasValue)
            {
                reaCenOrg = IDReaCenOrgDao.Get(this.Entity.CompID.Value);
                if (reaCenOrg != null && reaCenOrg.PlatformOrgNo.HasValue)
                    this.Entity.ReaServerCompCode = reaCenOrg.PlatformOrgNo.Value.ToString();
            }
            if (string.IsNullOrEmpty(this.Entity.ReaServerCompCode) && this.Entity.ReaCompID.HasValue)
            {
                reaCenOrg = IDReaCenOrgDao.Get(this.Entity.ReaCompID.Value);
                if (reaCenOrg != null && reaCenOrg.PlatformOrgNo.HasValue)
                    this.Entity.ReaServerCompCode = reaCenOrg.PlatformOrgNo.Value.ToString();
            }
            if (!this.Entity.ReaCompID.HasValue)
                this.Entity.ReaCompID = this.Entity.CompID;
            if (string.IsNullOrEmpty(this.Entity.ReaCompanyName))
                this.Entity.ReaCompanyName = this.Entity.CompanyName;
        }
        public BaseResultBool EditReaBmsCenSaleDocConfirmOfConfirmType(string[] tempArray, int secAccepterType, string codeScanningMode, long empID, string empName, string confirmType)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            if (confirmType == "reaorder" && !this.Entity.OrderDocID.HasValue)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "验收单ID：" + this.Entity.Id + "的所属订单信息为空！";
                return tempBaseResultBool;
            }
            if (confirmType == "reasale" && !this.Entity.SaleDocID.HasValue)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "验收单ID：" + this.Entity.Id + "的所属供货单信息为空！";
                return tempBaseResultBool;
            }
            ReaBmsCenSaleDocConfirm serverEntity = this.Get(this.Entity.Id);
            List<string> tmpa = tempArray.ToList();
            if (!EditBmsCenSaleDocConfirmStatusCheck(this.Entity, serverEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "验收单ID：" + this.Entity.Id + "的状态为：" + ReaBmsCenSaleDtlConfirmStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }

            IList<ReaBmsCenSaleDtlConfirm> dtlConfirmList = IBReaBmsCenSaleDtlConfirm.SearchListByHQL(string.Format("reabmscensaledtlconfirm.SaleDocConfirmID={0}", this.Entity.Id));
            if (dtlConfirmList == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "验收单ID：" + this.Entity.Id + "的验收明细信息为空！";
                return tempBaseResultBool;
            }

            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                //验收明细更新
                IList<ReaSaleDtlConfirmVO> dtEditList = new List<ReaSaleDtlConfirmVO>();
                foreach (ReaBmsCenSaleDtlConfirm dtlConfirm in dtlConfirmList)
                {
                    dtlConfirm.Status = int.Parse(ReaBmsCenSaleDtlConfirmStatus.已验收.Key);
                    dtlConfirm.StatusName = ReaBmsCenSaleDtlConfirmStatus.GetStatusDic()[dtlConfirm.Status.ToString()].Name;
                    ReaSaleDtlConfirmVO vo = new ReaSaleDtlConfirmVO();
                    vo.ReaBmsCenSaleDtlConfirm = dtlConfirm;
                    dtEditList.Add(vo);

                    IBReaBmsCenSaleDtlConfirm.Entity = dtlConfirm;
                    tempBaseResultBool.success = IBReaBmsCenSaleDtlConfirm.Edit();
                }

                switch (confirmType)
                {
                    case "reaorder":
                        ReaBmsCenOrderDoc orderDoc = new ReaBmsCenOrderDoc();
                        orderDoc.Id = this.Entity.OrderDocID.Value;
                        //更新订单状态
                        bool isAllConfirm = SearchBmsCenOrderDtlValidIsAllConfirmByOrderId(orderDoc.Id, dtEditList);
                        if (isAllConfirm == true)
                            orderDoc.Status = int.Parse(ReaBmsOrderDocStatus.全部验收.Key);
                        else
                            orderDoc.Status = int.Parse(ReaBmsOrderDocStatus.部分验收.Key);
                        tempBaseResultBool = EditReaBmsCenOrderDoc(orderDoc, empID, empName);
                        break;
                    case "reasale":
                        //更新供货单及供货明细状态
                        ReaBmsCenSaleDoc saleDoc = IBReaBmsCenSaleDoc.Get(this.Entity.SaleDocID.Value);
                        isAllConfirm = SearchBmsCenOrderDtlValidIsAllConfirmBySaleDocID(this.Entity.SaleDocID.Value, dtEditList);
                        if (isAllConfirm == true)
                            saleDoc.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.全部验收.Key);
                        else
                            saleDoc.Status = int.Parse(ReaBmsCenSaleDocAndDtlStatus.部分验收.Key);

                        tempBaseResultBool = IBReaBmsCenSaleDoc.EditReaBmsCenSaleDocAndDtlOfConfirm(saleDoc, empID, empName);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "供货验收单ID：" + this.Entity.Id + "更新失败！";
                return tempBaseResultBool;
            }
            if (tempBaseResultBool.success == true)
            {
                ReaBmsCenSaleDocConfirm tempEntity = new ReaBmsCenSaleDocConfirm();
                tempEntity.Id = this.Entity.Id;
                tempEntity.Status = this.Entity.Status;
                tempEntity.StatusName = ReaBmsCenSaleDocConfirmStatus.GetStatusDic()[tempEntity.Status.ToString()].Name;
                if (tempBaseResultBool.success) AddReaCheckInOperation(tempEntity, empID, empName);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditOfJudgeIsSameOrg(int secAccepterType, string compID, string secAccepterAccount, string secAccepterPwd, RBACUser rbacUser)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            int flagSameDocOrg = 0;
            //select Dept where Comp.OrgNo+''=Dept.UseCode
            HRDept dept = IBHRDept.Get(long.Parse(compID));
            IList<HRDept> listHRDept = null;
            if (dept != null && !string.IsNullOrEmpty(dept.UseCode))
                listHRDept = IBHRDept.SearchListByHQL(" hrdept.UseCode=\'" + dept.UseCode + "\'");
            if (listHRDept != null && listHRDept.Count == 1)
            {
                if (listHRDept[0].Id == rbacUser.HREmployee.HRDept.Id)
                    flagSameDocOrg = 1;
                else
                {
                    flagSameDocOrg = -1;
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：次验收人不属于验货单所属的供应商！";
                }
            }
            else
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：未找到验货单所属供应商的机构信息！";
                return tempBaseResultBool;
            }

            if (secAccepterType == 2)
            {
                if (flagSameDocOrg == -1 || flagSameDocOrg == 0)
                    return tempBaseResultBool;
            }
            else
            {
                string deptId = SessionHelper.GetSessionValue(DicCookieSession.HRDeptID);
                bool isSameSessionOrg = (deptId == rbacUser.HREmployee.HRDept.Id.ToString());
                if (secAccepterType == 3)
                {
                    if (!(flagSameDocOrg == 1 || isSameSessionOrg))
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "错误信息：次验收人必须属于供应商或本机构！";
                        return tempBaseResultBool;
                    }
                }
                else if (!isSameSessionOrg)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：主验收人和次验收人不属于同一机构！";
                    return tempBaseResultBool;
                }
            }
            string strPassWord = SecurityHelp.MD5Encrypt(secAccepterPwd, SecurityHelp.PWDMD5Key);
            bool tempBool = (rbacUser.Account == secAccepterAccount) && (rbacUser.PWD == strPassWord) && (!rbacUser.AccLock);
            if (!tempBool)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：次验收人登录密码错误！";
                return tempBaseResultBool;
            }
            return tempBaseResultBool;
        }

        public BaseResultBool EditReaSaleDocConfirmAndDtl(string[] tempArray, IList<ReaBmsCenSaleDtlConfirm> dtEditList, int secAccepterType, string codeScanningMode, string fieldsDtl, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            ReaBmsCenSaleDocConfirm serverEntity = this.Get(this.Entity.Id);
            List<string> tmpa = tempArray.ToList();
            if (!EditBmsCenSaleDocConfirmStatusCheck(this.Entity, serverEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "验收单ID：" + this.Entity.Id + "的状态为：" + ReaBmsCenSaleDtlConfirmStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }
            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                //验收明细更新
                if (dtEditList != null)
                {
                    foreach (ReaBmsCenSaleDtlConfirm saleDtlConfirm in dtEditList)
                    {
                        IBReaBmsCenSaleDtlConfirm.Entity = saleDtlConfirm;
                        string[] tempDtlArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsCenSaleDtlConfirm.Entity, fieldsDtl);
                        tempBaseResultBool.success = IBReaBmsCenSaleDtlConfirm.Update(tempDtlArray);
                    }
                }
            }
            else
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "验收单ID：" + this.Entity.Id + "更新失败！";
                return tempBaseResultBool;
            }
            if (tempBaseResultBool.success == true && this.Entity.Status != serverEntity.Status)
            {
                ReaBmsCenSaleDocConfirm tempEntity = new ReaBmsCenSaleDocConfirm();
                tempEntity.Id = this.Entity.Id;
                tempEntity.Status = this.Entity.Status;
                tempEntity.StatusName = ReaBmsCenSaleDocConfirmStatus.GetStatusDic()[tempEntity.Status.ToString()].Name;
                if (tempBaseResultBool.success) AddReaCheckInOperation(tempEntity, empID, empName);
            }
            return tempBaseResultBool;
        }
        bool EditBmsCenSaleDocConfirmStatusCheck(ReaBmsCenSaleDocConfirm entity, ReaBmsCenSaleDocConfirm serverEntity, List<string> tmpa, long empID, string empName)
        {
            if (entity.Status.ToString() == ReaBmsCenSaleDtlConfirmStatus.待继续验收.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsCenSaleDtlConfirmStatus.待继续验收.Key)
                {
                    return false;
                }
            }
            if (entity.Status.ToString() == ReaBmsCenSaleDtlConfirmStatus.已验收.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsCenSaleDtlConfirmStatus.待继续验收.Key)
                {
                    return false;
                }
            }
            if (entity.Status.ToString() == ReaBmsCenSaleDtlConfirmStatus.部分入库.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsCenSaleDtlConfirmStatus.已验收.Key && serverEntity.Status.ToString() != ReaBmsCenSaleDtlConfirmStatus.部分入库.Key)
                {
                    return false;
                }
            }
            if (entity.Status.ToString() == ReaBmsCenSaleDtlConfirmStatus.全部入库.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsCenSaleDtlConfirmStatus.已验收.Key && serverEntity.Status.ToString() != ReaBmsCenSaleDtlConfirmStatus.部分入库.Key)
                {
                    return false;
                }
            }
            return true;
        }
        public void AddReaCheckInOperation(ReaBmsCenSaleDocConfirm entity, long empID, string empName)
        {
            if (entity.Status.ToString() == ReaBmsCenSaleDtlConfirmStatus.待继续验收.Key) return;

            ReaCheckInOperation sco = new ReaCheckInOperation();
            sco.BobjectID = entity.Id;
            sco.CreatorID = empID;
            if (empName != null && empName.Trim() != "")
                sco.CreatorName = empName;
            sco.BusinessModuleCode = "ReaBmsCenSaleDocConfirm";
            sco.IsUse = true;
            sco.Type = entity.Status;
            sco.TypeName = ReaBmsCenSaleDocConfirmStatus.GetStatusDic()[entity.Status.ToString()].Name;
            IBReaCheckInOperation.Entity = sco;
            IBReaCheckInOperation.Add();
        }
        /// <summary>
        /// 获取验收总单号
        /// </summary>
        /// <returns></returns>
        private string GetSaleDocConfirmNo()
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
    }
}