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
using System.IO;
using ZhiFang.ReagentSys.Client.Common;
using System.Web;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaConfirm;
using Newtonsoft.Json.Linq;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsCenOrderDoc : BaseBLL<ReaBmsCenOrderDoc>, IBReaBmsCenOrderDoc
    {
        IDReaGoodsOrgLinkDao IDReaGoodsOrgLinkDao { get; set; }
        IBReaBmsCenOrderDtl IBReaBmsCenOrderDtl { get; set; }
        IBReaBmsReqDtl IBReaBmsReqDtl { get; set; }
        IBReaReqOperation IBReaReqOperation { get; set; }
        IDReaCenOrgDao IDReaCenOrgDao { get; set; }
        IDReaBmsCenOrderDtlDao IDReaBmsCenOrderDtlDao { get; set; }
        IDReaBmsQtyDtlDao IDReaBmsQtyDtlDao { get; set; }
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IDCenOrgDao IDCenOrgDao { get; set; }
        //IDReaBmsCenOrderDtlDao IDReaBmsCenOrderDtlDao { get; set; }

        #region 客户端订单处理
        public BaseResultBool AddReaBmsCenOrderDocAndDtl(ReaBmsCenOrderDoc entity, Dictionary<string, ReaBmsCenOrderDtl> dtAddList, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(entity.OrderDocNo))
                entity.OrderDocNo = this.GetReqDocNo();
            if (string.IsNullOrEmpty(entity.StatusName) && entity.Status > 0)
                entity.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
            if (!entity.PayStaus.HasValue)
                entity.PayStaus = long.Parse(ReaBmsOrderDocPayStaus.未付款.Key);
            this.Entity = entity;
            tempBaseResultBool.success = this.Add();
            if (tempBaseResultBool.success)
            {
                foreach (var orderDtl in dtAddList)
                {
                    orderDtl.Value.OrderDocNo = entity.OrderDocNo;
                    if (string.IsNullOrEmpty(orderDtl.Value.OrderDtlNo))
                        orderDtl.Value.OrderDtlNo = this.GetReqDocNo();
                    orderDtl.Value.OrderDocID = entity.Id;
                    IBReaBmsCenOrderDtl.Entity = orderDtl.Value;
                    tempBaseResultBool.success = IBReaBmsCenOrderDtl.Add();
                }
            }
            if (tempBaseResultBool.success) AddReaReqOperation(entity, empID, empName);
            return tempBaseResultBool;
        }
        private BaseResultDataValue AddReaBmsCenOrderDocAndDtOfMob(ref ReaBmsCenOrderDoc entity, ref IList<ReaBmsCenOrderDtl> dtAddList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();

            for (int i = 0; i < dtAddList.Count; i++)
            {
                if (tempBaseResultDataValue.success == false) break;

                if (!dtAddList[i].CompGoodsLinkID.HasValue)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "订单明细存在CompGoodsLinkID为空的数据,不能保存!";
                    break;
                    //continue;
                }

                ReaGoodsOrgLink orgLink = IDReaGoodsOrgLinkDao.Get(dtAddList[i].CompGoodsLinkID.Value);
                if (orgLink == null)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "获取CompGoodsLinkID为:" + dtAddList[i].CompGoodsLinkID.Value + ",的货品信息为空!";
                    break;
                }
                if (!dtAddList[i].Price.HasValue && orgLink.Price <= 0)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "获取CompGoodsLinkID为:" + dtAddList[i].CompGoodsLinkID.Value + ",的货品单价为空!";
                    break;
                }
                dtAddList[i].ReaGoodsName = orgLink.ReaGoods.CName;
                dtAddList[i].GoodsUnit = orgLink.ReaGoods.UnitName;
                dtAddList[i].UnitMemo = orgLink.ReaGoods.UnitMemo;
                dtAddList[i].ProdOrgName = orgLink.ReaGoods.ProdOrgName;
                dtAddList[i].ProdGoodsNo = orgLink.ReaGoods.ProdGoodsNo;

                dtAddList[i].BarCodeType = orgLink.BarCodeType;
                dtAddList[i].IsPrintBarCode = orgLink.IsPrintBarCode;
                dtAddList[i].ReaGoodsNo = orgLink.ReaGoods.ReaGoodsNo;
                dtAddList[i].ProdGoodsNo = orgLink.ReaGoods.ProdGoodsNo;
                dtAddList[i].GoodsNo = orgLink.ReaGoods.GoodsNo;
                dtAddList[i].GoodsSort = orgLink.ReaGoods.GoodsSort;

                dtAddList[i].CenOrgGoodsNo = orgLink.CenOrgGoodsNo;
                if (!dtAddList[i].Price.HasValue)
                    dtAddList[i].Price = orgLink.Price;
                dtAddList[i].SumTotal = (orgLink.Price * dtAddList[i].GoodsQty).Value;

                dtAddList[i].SuppliedQty = 0;//已供数量，默认=0
                dtAddList[i].UnSupplyQty = dtAddList[i].GoodsQty.Value;//未供数量，默认=订货数量
            }
            if (!entity.ReaCompID.HasValue)
                entity.ReaCompID = entity.CompID;
            if (string.IsNullOrEmpty(entity.ReaCompanyName))
                entity.ReaCompanyName = entity.CompanyName;
            entity.TotalPrice = dtAddList.Sum(p => p.SumTotal);
            entity.DataUpdateTime = DateTime.Now;

            return tempBaseResultDataValue;
        }
        public BaseResultDataValue AddReaBmsCenOrderDocAndDt(ReaBmsCenOrderDoc entity, IList<ReaBmsCenOrderDtl> dtAddList, int otype, long empID, string empName)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(entity.OrderDocNo))
                entity.OrderDocNo = this.GetReqDocNo();
            if (string.IsNullOrEmpty(entity.StatusName))
                entity.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
            //移动端的订单新增处理
            if (otype == 2)
            {
                tempBaseResultDataValue = AddReaBmsCenOrderDocAndDtOfMob(ref entity, ref dtAddList);
                if (!tempBaseResultDataValue.success) return tempBaseResultDataValue;
            }
            if (!entity.LabOrderDocID.HasValue)
                entity.LabOrderDocID = entity.Id;
            if (!entity.PayStaus.HasValue)
                entity.PayStaus = long.Parse(ReaBmsOrderDocPayStaus.未付款.Key);

            entity.SupplyStatus = int.Parse(ReaBmsOrderDocSupplyStatus.未供货.Key);

            this.Entity = entity;
            tempBaseResultDataValue.success = this.Add();
            if (tempBaseResultDataValue.success)
            {
                if (dtAddList != null && dtAddList.Count > 0)
                {
                    BaseResultBool tempBaseResultBool = IBReaBmsCenOrderDtl.AddDtList(dtAddList, this.Entity, empID, empName);
                    tempBaseResultDataValue.success = tempBaseResultBool.success;
                    tempBaseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                }
            }
            if (tempBaseResultDataValue.success) AddReaReqOperation(this.Entity, empID, empName);
            return tempBaseResultDataValue;
        }
        public BaseResultBool EditReaBmsCenOrderDocAndDt(ReaBmsCenOrderDoc entity, string[] tempArray, IList<ReaBmsCenOrderDtl> dtAddList, IList<ReaBmsCenOrderDtl> dtEditList, string checkIsUploaded, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            this.Entity = entity;
            ReaBmsCenOrderDoc oldEntity = ((IDReaBmsCenOrderDocDao)base.DBDao).Get(entity.Id);
            List<string> tmpa = tempArray.ToList();
            if (!EditReaBmsCenOrderDocStatusCheck(entity, oldEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "订单ID：" + entity.Id + "的状态为：" + ReaBmsOrderDocStatus.GetStatusDic()[oldEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }
            //订单审核通过同时是否直接订单上传:checkIsUploaded(1:是;2:否;)

            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                ReaBmsCenOrderDoc tempEntity = new ReaBmsCenOrderDoc();
                tempEntity.Id = entity.Id;
                tempEntity.Status = entity.Status;
                if (dtAddList != null && dtAddList.Count > 0) tempBaseResultBool = IBReaBmsCenOrderDtl.AddDtList(dtAddList, tempEntity, empID, empName);
                if (dtEditList != null && dtEditList.Count > 0) tempBaseResultBool = IBReaBmsCenOrderDtl.EditDtList(dtEditList, tempEntity);

                //如果是客户端的申请开单,需要给开单明细打上审核退回原因
                if (tempBaseResultBool.success == true && dtEditList != null && dtEditList.Count > 0 && entity.Status.ToString() == ReaBmsOrderDocStatus.审核退回.Key)
                {
                    tempBaseResultBool = EditReaBmsReqDtl(dtEditList);
                }

                if (tempBaseResultBool.success) AddReaReqOperation(tempEntity, empID, empName);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditReaBmsCenOrderDocByPay(ReaBmsCenOrderDoc entity, string[] tempArray, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            this.Entity = entity;
            ReaBmsCenOrderDoc serverEntity = ((IDReaBmsCenOrderDocDao)base.DBDao).Get(entity.Id);
            List<string> tmpa = tempArray.ToList();
            if (!EditReaBmsCenOrderDocStatusCheckByPay(entity, serverEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "订单ID：" + entity.Id + "的状态为：" + ReaBmsOrderDocStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }
            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                //ReaBmsCenOrderDoc tempEntity = new ReaBmsCenOrderDoc();
                //tempEntity.Id = entity.Id;
                //tempEntity.Status = entity.Status;
                //if (tempBaseResultBool.success) AddReaReqOperation(tempEntity, empID, empName);
            }
            return tempBaseResultBool;
        }
        bool EditReaBmsCenOrderDocStatusCheckByPay(ReaBmsCenOrderDoc entity, ReaBmsCenOrderDoc serverEntity, List<string> tmpa, long empID, string empName)
        {
            if (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.审核通过.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.审批通过.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.订单上传.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.供应商确认.Key)
            {
                return false;
            }
            return true;
        }
        private BaseResultBool EditReaBmsReqDtl(IList<ReaBmsCenOrderDtl> dtEditList)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.success = true;
            StringBuilder idStr = new StringBuilder();
            foreach (var item in dtEditList)
            {
                idStr.Append("reabmsreqdtl.OrderDtlID=" + item.Id + " or ");
            }
            char[] trimChars = new char[] { ' ', 'o', 'r' };
            IList<ReaBmsReqDtl> reqDtlList = IBReaBmsReqDtl.SearchListByHQL("(" + idStr.ToString().TrimEnd(trimChars) + ")");
            if (reqDtlList != null && reqDtlList.Count > 0)
            {
                foreach (var reqDtl in reqDtlList)
                {
                    reqDtl.OrderStatus = this.Entity.Status;
                    reqDtl.OrderCheckMemo = this.Entity.CheckMemo;
                    IBReaBmsReqDtl.Entity = reqDtl;
                    tempBaseResultBool.success = IBReaBmsReqDtl.Edit();
                }
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 计算订单总额
        /// </summary>
        /// <param name="docID">订单ID</param>
        /// <returns></returns>
        public BaseResultDataValue EditReaBmsCenOrderDocTotalPrice(long docID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            ReaBmsCenOrderDoc orderDoc = this.Get(docID);
            IList<ReaBmsCenOrderDtl> dtlList = IBReaBmsCenOrderDtl.SearchListByHQL("reabmscenorderdtl.OrderDocID=" + docID);

            if (dtlList != null && dtlList.Count > 0)
            {
                double totalPrice = 0;
                foreach (ReaBmsCenOrderDtl orderDtl in dtlList)
                {
                    if (orderDtl.Price.HasValue && orderDtl.GoodsQty.HasValue)
                        totalPrice = totalPrice + orderDtl.Price.Value * orderDtl.GoodsQty.Value;
                }
                orderDoc.TotalPrice = totalPrice;
                this.Entity = orderDoc;
                baseResultDataValue.success = this.Edit();
            }
            return baseResultDataValue;
        }
        bool EditReaBmsCenOrderDocStatusCheck(ReaBmsCenOrderDoc entity, ReaBmsCenOrderDoc serverEntity, List<string> tmpa, long empID, string empName)
        {
            #region 暂存
            if (entity.Status.ToString() == ReaBmsOrderDocStatus.暂存.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.暂存.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.审核退回.Key)
                {
                    return false;
                }
                tmpa.Add("UserID=" + empID + " ");
                tmpa.Add("UserName='" + empName + "'");
            }
            #endregion

            #region 申请
            if (entity.Status.ToString() == ReaBmsOrderDocStatus.申请.Key)
            {
                //审核应用时,可以先编辑保存状态为已申请的申请单
                if (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.暂存.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.申请.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.审核退回.Key)
                {
                    return false;
                }
                entity.UserID = empID;
                if (string.IsNullOrEmpty(entity.UserName)) entity.UserName = empName;
                tmpa.Add("UserID=" + entity.UserID + " ");
                tmpa.Add("UserName='" + entity.UserName + "'");
                if (!entity.OperDate.HasValue) tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

                tmpa.Add("CheckerID=null");
                tmpa.Add("Checker=null");
                tmpa.Add("CheckTime=null");
            }
            #endregion

            if (entity.Status.ToString() == ReaBmsOrderDocStatus.审核通过.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.申请.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.审核退回.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.取消上传.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.审批退回.Key)
                {
                    return false;
                }
                entity.CheckerID = empID;
                entity.Checker = empName;
                entity.IOFlag = int.Parse(ReaBmsOrderDocIOFlag.未提取.Key);
                tmpa.Add("CheckerID=" + entity.CheckerID + " ");
                tmpa.Add("Checker='" + entity.Checker + "'");
                tmpa.Add("CheckTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("IOFlag=" + entity.IOFlag + " ");

            }

            if (entity.Status.ToString() == ReaBmsOrderDocStatus.审核退回.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.申请.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.审批退回.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.取消上传.Key)
                {
                    return false;
                }
            }

            if (entity.Status.ToString() == ReaBmsOrderDocStatus.审批通过.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.审核通过.Key)
                {
                    return false;
                }
                entity.IsHasApproval = true;
                entity.ApprovalID = empID;
                entity.ApprovalCName = empName;
                tmpa.Add("IsHasApproval=1");
                tmpa.Add("ApprovalID=" + entity.ApprovalID + " ");
                tmpa.Add("ApprovalCName='" + entity.ApprovalCName + "'");
                tmpa.Add("ApprovalTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            if (entity.Status.ToString() == ReaBmsOrderDocStatus.审批退回.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.审核通过.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.取消上传.Key)
                {
                    return false;
                }
                entity.IsHasApproval = true;
                entity.ApprovalID = empID;
                entity.ApprovalCName = empName;
                tmpa.Add("IsHasApproval=1");
                tmpa.Add("ApprovalID=" + entity.ApprovalID + " ");
                tmpa.Add("ApprovalCName='" + entity.ApprovalCName + "'");
                tmpa.Add("ApprovalTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            if (entity.Status.ToString() == ReaBmsOrderDocStatus.订单上传.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.审核通过.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.审批通过.Key && serverEntity.IOFlag.ToString() == ReaBmsOrderDocIOFlag.已上传.Key)
                {
                    return false;
                }
                entity.IOFlag = int.Parse(ReaBmsOrderDocIOFlag.已上传.Key);
            }
            if (entity.Status.ToString() == ReaBmsOrderDocStatus.取消上传.Key)
            {
                if ((serverEntity.Status.ToString() != ReaBmsOrderDocStatus.订单上传.Key && serverEntity.IOFlag.ToString() != ReaBmsOrderDocIOFlag.已上传.Key) && (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.取消上传.Key))
                {
                    return false;
                }
                entity.IOFlag = int.Parse(ReaBmsOrderDocIOFlag.取消上传.Key);
            }
            if (entity.Status.ToString() == ReaBmsOrderDocStatus.部分验收.Key || entity.Status.ToString() == ReaBmsOrderDocStatus.全部验收.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.审核通过.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.审批通过.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.订单上传.Key && serverEntity.Status.ToString() != ReaBmsOrderDocStatus.部分验收.Key)
                {
                    return false;
                }
            }
            if (entity.Status.ToString() == ReaBmsOrderDocStatus.订单转供货.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.供应商确认.Key && serverEntity.IOFlag.ToString() != ReaBmsOrderDocIOFlag.供应商确认.Key)
                {
                    return false;
                }
            }
            //供应商确认,供应商取消确认需到BReaBmsCenOrderDocOfService里实现
            return true;
        }

        /// <summary>
        /// 添加订单操作记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        public void AddReaReqOperation(ReaBmsCenOrderDoc entity, long empID, string empName)
        {
            if (entity.Status.ToString() == ReaBmsOrderDocStatus.暂存.Key) return;

            ReaReqOperation sco = new ReaReqOperation();
            sco.BobjectID = entity.Id;
            sco.CreatorID = empID;
            if (empName != null && empName.Trim() != "")
                sco.CreatorName = empName;
            sco.BusinessModuleCode = "ReaBmsCenOrderDoc";
            if (!string.IsNullOrEmpty(entity.CheckMemo))
                sco.Memo = entity.CheckMemo;
            sco.IsUse = true;
            sco.Type = entity.Status;
            sco.TypeName = ReaBmsOrderDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
            IBReaReqOperation.Entity = sco;
            IBReaReqOperation.Add();
        }
        /// <summary>
        /// 获取订单总单号
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
        #endregion

        #region (客户端与平台都在同一数据库)订单上传/取消上传
        public BaseResultBool EditReaBmsCenOrderDocOfUploadOfIdStr(string idStr, bool isVerifyProdGoodsNo, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(idStr))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "idStr为空！";
                return tempBaseResultBool;
            }
            //待上传的订单当前状态为"审核通过"或"取消上传",并且订单的数据标志不为"已上传"
            IList<ReaBmsCenOrderDoc> tempDocList = this.SearchListByHQL("(reabmscenorderdoc.Status=" + ReaBmsOrderDocStatus.审核通过.Key + " or reabmscenorderdoc.Status = " + ReaBmsOrderDocStatus.审批通过.Key + " or reabmscenorderdoc.Status = " + ReaBmsOrderDocStatus.取消上传.Key + ") and reabmscenorderdoc.IOFlag!=" + ReaBmsOrderDocIOFlag.已上传.Key + " and reabmscenorderdoc.Id in(" + idStr + ")");

            #region 订单的供应商所属机构平台编码是否为空
            for (int i = 0; i < tempDocList.Count; i++)
            {
                if (string.IsNullOrEmpty(tempDocList[i].ReaServerCompCode))
                {
                    if (!tempDocList[i].CompID.HasValue)
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "订单号为：" + tempDocList[i].OrderDocNo + "的所属供应商信息为空！";
                        break;
                    }
                    ReaCenOrg reaCenOrg = IDReaCenOrgDao.Get(tempDocList[i].CompID.Value);
                    if (tempBaseResultBool.success == true && reaCenOrg.PlatformOrgNo <= 0)
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "订单号为：" + tempDocList[i].OrderDocNo + "的供应商所属机构平台编码为空！";
                        break;
                    }
                    tempDocList[i].ReaServerCompCode = reaCenOrg.PlatformOrgNo.Value.ToString();
                    if (tempBaseResultBool.success == false) break;
                }
            }
            if (tempBaseResultBool.success == false) return tempBaseResultBool;
            #endregion

            IList<ReaBmsCenOrderDtl> orderDtlList = new List<ReaBmsCenOrderDtl>();
            foreach (var entity in tempDocList)
            {
                if (tempBaseResultBool.success == false) break;

                orderDtlList.Clear();
                orderDtlList = IDReaBmsCenOrderDtlDao.GetListByHQL("reabmscenorderdtl.OrderDocID=" + entity.Id);
                if (orderDtlList == null || orderDtlList.Count <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "订单号为：" + entity.OrderDocNo + "的订单明细信息为空！";
                    break;
                }

                #region 更新订单及其明细
                foreach (var orderDtl in orderDtlList)
                {
                    if (string.IsNullOrEmpty(orderDtl.GoodsNo) || string.IsNullOrEmpty(orderDtl.ProdGoodsNo) && orderDtl.CompGoodsLinkID.HasValue)
                    {
                        ReaGoodsOrgLink goodLink = IDReaGoodsOrgLinkDao.Get(orderDtl.CompGoodsLinkID.Value);
                        orderDtl.GoodsNo = goodLink.CenOrgGoodsNo;
                        if (string.IsNullOrEmpty(orderDtl.ProdGoodsNo))
                            orderDtl.ProdGoodsNo = goodLink.ProdGoodsNo;
                    }
                    orderDtl.IOFlag = int.Parse(ReaBmsOrderDocIOFlag.已上传.Key);
                    IBReaBmsCenOrderDtl.Entity = orderDtl;
                    tempBaseResultBool.success = IBReaBmsCenOrderDtl.Edit();
                }
                entity.Status = int.Parse(ReaBmsOrderDocStatus.订单上传.Key);
                entity.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
                entity.IOFlag = int.Parse(ReaBmsOrderDocIOFlag.已上传.Key);
                entity.IsVerifyProdGoodsNo = isVerifyProdGoodsNo;
                this.Entity = entity;
                tempBaseResultBool.success = this.Edit();

                if (tempBaseResultBool.success) AddReaReqOperation(entity, empID, empName);
                #endregion

                if (tempBaseResultBool.success == false)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "订单号为：" + entity.OrderDocNo + "上传失败！";
                    break;
                }
            }

            return tempBaseResultBool;
        }
        public BaseResultBool EditReaBmsCenOrderDocOfCancelUpload(ReaBmsCenOrderDoc entity, string[] tempArray, bool isVerifyProdGoodsNo, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入参数（entity）为空！";
                return tempBaseResultBool;
            }
            ReaBmsCenOrderDoc serverEntity = this.Get(entity.Id);
            List<string> tmpa = tempArray.ToList();
            if (!EditReaBmsCenOrderDocStatusCheck(entity, serverEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "订单ID：" + entity.Id + "的状态为：" + ReaBmsOrderDocStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + ",数据标志为:" + ReaBmsOrderDocIOFlag.GetStatusDic()[serverEntity.IOFlag.ToString()].Name + "！";
                return tempBaseResultBool;
            }

            IList<ReaBmsCenOrderDtl> orderDtlList = IDReaBmsCenOrderDtlDao.GetListByHQL("reabmscenorderdtl.OrderDocID=" + entity.Id);
            if (orderDtlList == null || orderDtlList.Count <= 0)
            {
                List<string> tmpaDtl = new List<string>();
                foreach (var orderDtl in orderDtlList)
                {
                    orderDtl.IOFlag = int.Parse(ReaBmsOrderDocIOFlag.取消上传.Key);
                    IBReaBmsCenOrderDtl.Entity = orderDtl;
                    tempBaseResultBool.success = IBReaBmsCenOrderDtl.Edit();
                }
            }
            if (tempBaseResultBool.success == false) return tempBaseResultBool;

            serverEntity.Status = int.Parse(ReaBmsOrderDocStatus.取消上传.Key);
            serverEntity.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[serverEntity.Status.ToString()].Name;
            serverEntity.IOFlag = int.Parse(ReaBmsOrderDocIOFlag.取消上传.Key);
            serverEntity.IsVerifyProdGoodsNo = isVerifyProdGoodsNo;
            serverEntity.LabMemo = entity.LabMemo;
            this.Entity = serverEntity;
            tempBaseResultBool.success = this.Edit();

            //tempArray = tmpa.ToArray();
            //tempBaseResultBool.success = this.Update(tempArray);

            if (tempBaseResultBool.success) AddReaReqOperation(entity, empID, empName);

            return tempBaseResultBool;
        }
        #endregion

        #region PDF清单及统计

        public Stream SearchPdfReportOfTypeById(string reaReportClass, long id, long labID, string labCName, string breportType, string frx, ref string pdfFileName)
        {
            Stream stream = null;
            ReaBmsCenOrderDoc orderDoc = this.Get(id);
            if (orderDoc == null)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取订货单PDF清单数据信息为空!");
            }
            IList<ReaBmsCenOrderDtl> dtlList = IBReaBmsCenOrderDtl.SearchListByHQL("reabmscenorderdtl.OrderDocID=" + id);
            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取订货单PDF清单明细信息为空!");
            }
            else
            {
                for (int i = 0; i < dtlList.Count; i++)
                {
                    ReaGoods reaGoods = null;
                    if (dtlList[i].ReaGoodsID.HasValue)
                        reaGoods = IDReaGoodsDao.Get(dtlList[i].ReaGoodsID.Value);
                    if (reaGoods != null)
                    {
                        dtlList[i].GoodsClass = reaGoods.GoodsClass;
                        dtlList[i].GoodsClassType = reaGoods.GoodsClassType;
                        dtlList[i].GoodEName = reaGoods.EName;
                        dtlList[i].GoodSName = reaGoods.SName;
                        dtlList[i].NetGoodsNo = reaGoods.NetGoodsNo;
                    }

                }
            }
            //获取订货单所属供应商的发票信息
            SearchReaCompInfo(orderDoc);

            pdfFileName = orderDoc.OrderDocNo + ".pdf";
            //string milliseconds = "";
            if (reaReportClass == ReaReportClass.Frx.Key)
            {
                //Stopwatch watch = new Stopwatch();
                ////开始计时
                //watch.Start();
                stream = CreatePdfReportOfFrxById(orderDoc, dtlList, frx);
                //watch.Stop();
                //milliseconds = watch.ElapsedMilliseconds.ToString();
                //ZhiFang.Common.Log.Log.Debug("FrxToPDF.Milliseconds:" + milliseconds);
            }
            else if (reaReportClass == ReaReportClass.Excel.Key)
            {
                //Stopwatch watch = new Stopwatch();
                ////开始计时
                //watch.Start();
                string excelFileFullDir = "";
                //获取订货单模板
                if (string.IsNullOrEmpty(frx))
                    frx = "订货清单.xlsx";
                string fileExt = frx.Substring(frx.LastIndexOf("."));
                string excelFile = orderDoc.OrderDocNo.ToString() + fileExt;
                Stream stream2 = null;
                ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
                stream2 = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsCenOrderDoc, ReaBmsCenOrderDtl>(orderDoc, dtlList, excelCommand, breportType, orderDoc.LabID, frx, excelFile, ref excelFileFullDir);
                stream2.Close();
                string pdfFullDir = "";

                bool result = ExcelToPdfReportHelp.ExcelToPDF(excelFileFullDir, breportType, orderDoc.LabID, pdfFileName, ref pdfFullDir);
                if (result)
                {
                    stream = PdfReportHelp.GetReportPDF(pdfFullDir);
                    //watch.Stop();
                    //milliseconds = watch.ElapsedMilliseconds.ToString();
                }
                //ZhiFang.Common.Log.Log.Debug("ExcelToPDF.Milliseconds:" + milliseconds);
            }

            return stream;
        }
        private void SearchReaCompInfo(ReaBmsCenOrderDoc orderDoc)
        {
            ReaCenOrg reaCenOrg = null;
            if (orderDoc.CompID.HasValue)
                reaCenOrg = IDReaCenOrgDao.Get(orderDoc.CompID.Value);
            if (reaCenOrg != null)
            {
                orderDoc.CompBankName = reaCenOrg.BankName;
                orderDoc.CompBankAccount = reaCenOrg.BankAccount;
                orderDoc.CompAddress = reaCenOrg.Address;
                orderDoc.CompTel = reaCenOrg.Tel;
                orderDoc.CompTel1 = reaCenOrg.Tel1;
                orderDoc.CompHotTel = reaCenOrg.HotTel;
                orderDoc.CompHotTel1 = reaCenOrg.HotTel1;
                orderDoc.CompFox = reaCenOrg.Fox;
                orderDoc.CompContact = reaCenOrg.Contact;
                orderDoc.CompEmail = reaCenOrg.Email;
            }
        }

        private Stream CreatePdfReportOfFrxById(ReaBmsCenOrderDoc orderDoc, IList<ReaBmsCenOrderDtl> dtlList, string frx)
        {
            Stream stream = null;
            DataSet dataSet = new DataSet();
            dataSet.DataSetName = "ZhiFang.ReagentSys.Client";

            List<ReaBmsCenOrderDoc> docList = new List<ReaBmsCenOrderDoc>();
            docList.Add(orderDoc);
            DataTable docDt = ReportBTemplateHelp.ToDataTable<ReaBmsCenOrderDoc>(docList, null);
            docDt.TableName = "Rea_BmsCenOrderDoc";
            dataSet.Tables.Add(docDt);

            DataTable dtDtl = ReportBTemplateHelp.ToDataTable<ReaBmsCenOrderDtl>(dtlList, null);
            if (dtDtl != null)
            {
                dtDtl.TableName = "Rea_BmsCenOrderDtl";
                dataSet.Tables.Add(dtDtl);
            }
            //获取订货单Frx模板
            //string parentPath = ReportBTemplateHelp.GetSaveBTemplatePath(this.Entity.LabID, "订货清单");
            string pdfName = DateTime.Now.ToString("yyyyMMddHHmmss") +".pdf";
                // orderDoc.OrderDocNo.ToString() + ".pdf";
            //如果当前实验室还没有维护订货单报表模板,默认使用公共的订货单模板
            if (string.IsNullOrEmpty(frx))
                frx = "订货清单.frx";
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            excelCommand.EEC_Total = dtlList.Sum(p=>p.SumTotal).ToString();

            ExcelReportHelp.CreateEECDataTable(excelCommand, ref dataSet);
            stream = FrxToPdfReportHelp.SavePdfReport(dataSet, orderDoc.LabID, pdfName, FrxToPdfReportHelp.PublicTemplateDir, BTemplateType.GetStatusDic()[BTemplateType.订货清单.Key].Name, frx, false);

            return stream;
        }
        public Stream SearchExcelReportOfExcelById(long id, long labID, string labCName, string breportType, string frx, ref string fileName)
        {
            Stream stream = null;
            ReaBmsCenOrderDoc orderDoc = this.Get(id);
            if (orderDoc == null)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取订货单数据信息为空!");
            }
            //IList<ReaBmsCenOrderDtl> dtlList = IBReaBmsCenOrderDtl.SearchListByHQL("reabmscenorderdtl.OrderDocID=" + id);
            IList<ReaBmsCenOrderDtl> dtlList = IBReaBmsCenOrderDtl.GetReaBmsCenOrderDtlListByDocId(id);
            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取订货单明细信息为空!");
            }
            //获取订货单所属供应商的发票信息
            SearchReaCompInfo(orderDoc);

            //获取订货单模板
            if (string.IsNullOrEmpty(frx))
                frx = "订货清单.xlsx";
            string saveFullPath = "";
            string fileExt = frx.Substring(frx.LastIndexOf("."));
            string excelFile = orderDoc.OrderDocNo.ToString() + fileExt;
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsCenOrderDoc, ReaBmsCenOrderDtl>(orderDoc, dtlList, excelCommand, breportType, orderDoc.LabID, frx, excelFile, ref saveFullPath);
            fileName = orderDoc.DeptName + "订货信息" + fileExt;
            return stream;
        }
        public Stream SearchReaBmsCenOrderDocOfExcelByIdStr(string reaReportClass, string idStr, long labID, string labCName, string breportType, string frx, ref string fileName)
        {
            Stream stream = null;
            ReaBmsCenOrderDoc orderDoc = new ReaBmsCenOrderDoc();// this.Get(id);
            if (string.IsNullOrEmpty(idStr))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("传入的订货清单信息(idStr)为空!");
            }

            string dtlHql = "reabmscenorderdtl.OrderDocID in (" + idStr + ")";
            IList<ReaBmsCenOrderDtl> dtlList = IBReaBmsCenOrderDtl.SearchReaBmsCenOrderDtlListByHQL("", dtlHql, "", "", -1, -1);

            //判断订货清单是否都审核通过及供货商都已存在
            var tempList = dtlList.Where(p => p.CompGoodsLinkID.HasValue == false);
            if (tempList.Count() > 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("存在供应商为空的订货清单信息,不能合并报表!");
            }
            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取订货清单明细信息为空!");
            }

            dtlList = SearchReaBmsCenOrderDtlListOfGroupBy1(dtlList);
            //获取订货单模板
            if (string.IsNullOrEmpty(frx))
                frx = "订单合并报表.xlsx";
            string saveFullPath = "";
            string fileExt = frx.Substring(frx.LastIndexOf("."));
            string excelFile = DateTime.Now.ToString("yyyyMMddHHmmss") + fileExt;
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            excelCommand.EEC_Total = dtlList.Sum(p => p.SumTotal).ToString();

            stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsCenOrderDoc, ReaBmsCenOrderDtl>(orderDoc, dtlList, excelCommand, breportType, orderDoc.LabID, frx, excelFile, ref saveFullPath);
            fileName = orderDoc.DeptName + "订单合并报表" + fileExt;
            return stream;

        }
        /// <summary>
        /// 将选择的多个订单按供货商+货品编码+包装单位合并后,生成PDF报表文件
        /// </summary>
        /// <param name="reaReportClass"></param>
        /// <param name="id"></param>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="breportType"></param>
        /// <param name="frx"></param>
        /// <param name="pdfFileName"></param>
        /// <returns></returns>
        public Stream SearchReaBmsCenOrderDocOfPdfByIdStr(string reaReportClass, string idStr, long labID, string labCName, string breportType, string frx, ref string pdfFileName)
        {
            Stream stream = null;
            ReaBmsCenOrderDoc orderDoc = new ReaBmsCenOrderDoc();// this.Get(id);
            if (string.IsNullOrEmpty(idStr))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("传入的订货清单信息(idStr)为空!");
            }
            
            string dtlHql = "reabmscenorderdtl.OrderDocID in (" + idStr + ")";
            IList<ReaBmsCenOrderDtl> dtlList = IBReaBmsCenOrderDtl.SearchReaBmsCenOrderDtlListByHQL("", dtlHql, "", "", -1, -1);
            
            //判断订货清单是否都审核通过及供货商都已存在
            var tempList = dtlList.Where(p => p.CompGoodsLinkID.HasValue == false);
            if (tempList.Count() > 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("存在供应商为空的订货清单信息,不能合并报表!");
            }
            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取订货清单明细信息为空!");
            }

            dtlList = SearchReaBmsCenOrderDtlListOfGroupBy1(dtlList);
            pdfFileName = "订单合并报表.pdf";//GetReqDocNo() + ".pdf";//
            if (string.IsNullOrEmpty(frx))
                frx = "订单合并报表.frx";
            if (reaReportClass == ReaReportClass.Frx.Key)
            {
                stream = CreatePdfReportOfFrxById(orderDoc, dtlList, frx);
            }
            return stream;
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
                p.Price,
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
                Price = g.Key.Price,
                ReqGoodsQty = g.Sum(k => k.ReqGoodsQty), //g.ElementAt(0).ReqGoodsQty,
                GoodsQty = g.Sum(k => k.GoodsQty),
                SumTotal = g.Sum(k => k.SumTotal),
               
                CurrentQty = g.ElementAt(0).CurrentQty,
                ProdID = g.ElementAt(0).ProdID,
                ProdOrgName = g.ElementAt(0).ProdOrgName,
                BarCodeType = g.ElementAt(0).BarCodeType,

                OrderDocID = g.ElementAt(0).OrderDocID,
                DataAddTime = g.ElementAt(0).DataAddTime,
                ArrivalTime = g.ElementAt(0).ArrivalTime,
                Memo = g.ElementAt(0).Memo
            }).OrderBy(p => p.DeptID).ThenBy(p => p.CompanyName).ThenBy(p => p.CenOrgGoodsNo).ThenBy(p => p.GoodsUnit).ToList();
        }

        public Stream SearchReaBmsCenOrderDocListReportOfPdf(string where, string frx, long labID, string labCName, ref string fileName)
        {
            Stream stream = null;
            if (string.IsNullOrEmpty(where))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取货品耗材月订货清单的查询条件不能为空!");
            }
            IList<ReaBmsCenOrderDoc> docList = this.SearchListByHQL(where);
            if (docList == null || docList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取货品耗材月订货清单数据信息为空!");
            }
            StringBuilder strb = new StringBuilder();
            foreach (var orderDoc in docList)
            {
                strb.Append("reabmscenorderdtl.OrderDocID=" + orderDoc.Id + " or ");
            }
            char[] trimChars = new char[] { ' ', 'o', 'r' };
            IList<ReaBmsCenOrderDtl> orderDtlList = IBReaBmsCenOrderDtl.SearchListByHQL("(" + strb.ToString().TrimEnd(trimChars) + ")");
            if (orderDtlList == null || orderDtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取货品耗材月订货清单数据信息为空!");
            }

            //按供应商货品关系ID+产品编码+包装单位分组货品信息并统计
            var dtlList = orderDtlList.GroupBy(p => new { p.CompGoodsLinkID, p.ReaGoodsNo, p.CenOrgGoodsNo, p.GoodsUnit }).Select(g => new
            ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaBmsCenOrderDtlVOOfReport
            {
                ProdGoodsNo = g.ElementAt(0).ProdGoodsNo,
                ReaGoodsNo = g.ElementAt(0).ReaGoodsNo,
                CenOrgGoodsNo = g.ElementAt(0).CenOrgGoodsNo,

                ReaGoodsName = g.ElementAt(0).ReaGoodsName,
                GoodsUnit = g.ElementAt(0).GoodsUnit,
                UnitMemo = g.ElementAt(0).UnitMemo,
                ReqGoodsQty = g.Sum(k => k.ReqGoodsQty),
                GoodsQty = g.Sum(k => k.GoodsQty),

                SumTotal = g.Sum(k => k.SumTotal),
                Price = g.Sum(k => k.SumTotal) / g.Sum(k => k.GoodsQty),
                Memo = g.ElementAt(0).Memo,
                ProdOrgName = g.ElementAt(0).ProdOrgName,
                CompanyName = IDReaGoodsOrgLinkDao.Get(g.ElementAt(0).CompGoodsLinkID.Value).CenOrg.CName,
                CurrentQty = SearchCurrentQtyInfo(g.ElementAt(0).CompGoodsLinkID.Value, g.ElementAt(0).ReaGoodsNo)//当前库存数量
            }).ToList();
            fileName = docList[0].DeptName + ".pdf";

            return stream;
        }
        private string SearchCurrentQtyInfo(long compGoodsLinkID, string reaGoodsNo)
        {
            string currentQty = "";
            string qtyHql = "reabmsqtydtl.GoodsQty>0 and reabmsqtydtl.ReaGoodsNo='" + reaGoodsNo + "' and reabmsqtydtl.CompGoodsLinkID=" + compGoodsLinkID;
            IList<ReaBmsQtyDtl> qtyList = IDReaBmsQtyDtlDao.GetListByHQL(qtyHql);
            if (qtyList == null || qtyList.Count <= 0) return currentQty;

            currentQty = IDReaBmsQtyDtlDao.GetCurrentQtyInfo(qtyList, reaGoodsNo);
            return currentQty;
        }
        #endregion

        #region 订单验收供货订单Excel导入处理

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="labID"></param>
        /// <param name="labName"></param>
        /// <param name="orderDtlOfConfirmVOList"></param>
        /// <returns></returns>
        public BaseResultDataValue UploadSupplyReaOrderDataByExcel(HttpPostedFile file, long labID, string labName, ref EntityList<ReaOrderDtlOfConfirmVO> orderDtlOfConfirmVOList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            //供货信息的所属订单验证(订货单号,订货机构码,供货机构码,订货明细)
            //供货的订单总单信息
            Dictionary<string, string> docInfo = new Dictionary<string, string>();
            docInfo.Add("ReaServerLabcCode", "");
            docInfo.Add("OrderDocNo", "");
            docInfo.Add("ReaServerCompCode", "");
            docInfo.Add("SaleDocNo", "");

            //明细的开始行索引
            int dtlFristRowIndex = 0;
            //明细行的总列数
            int dtlCellCount = 0;
            int dCellIndex = -1;
            //明细内容信息
            DataTable dtlTable = new DataTable();
            //明细有效列信息
            Dictionary<string, int> dtlColumns = new Dictionary<string, int>();

            try
            {
                GetSheetCellInfo(file, ref docInfo, ref dtlFristRowIndex, ref dtlCellCount, ref dCellIndex, ref dtlTable, ref dtlColumns);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "解析供货文件出错:" + ex.Message;
                ZhiFang.Common.Log.Log.Error("解析供货文件出错:" + ex.StackTrace);
                return baseResultDataValue;
            }

            if (string.IsNullOrEmpty(docInfo["OrderDocNo"]))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "解析供货文件的订货单号信息为空,不能导入!";
                ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            if (string.IsNullOrEmpty(docInfo["ReaServerLabcCode"]))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "解析供货文件的订货机构码信息为空,不能导入!";
                ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            if (string.IsNullOrEmpty(docInfo["ReaServerCompCode"]))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "解析供货文件的供货机构码信息为空,不能导入!";
                ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            if (dtlTable == null || dtlTable.Rows.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "解析供货文件的供货明细信息为空,不能导入!";
                ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }

            string docHql = "reabmscenorderdoc.OrderDocNo='" + docInfo["OrderDocNo"] + "' and reabmscenorderdoc.ReaServerLabcCode='" + docInfo["ReaServerLabcCode"] + "' and reabmscenorderdoc.ReaServerCompCode='" + docInfo["ReaServerCompCode"] + "'";
            IList<ReaBmsCenOrderDoc> docList = this.SearchListByHQL(docHql);
            if (docList == null || docList.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = string.Format("获取订货单号为:{0},订货机构码为:{1},供货机构码为:{2},的订货信息为空!", docInfo["OrderDocNo"], docInfo["ReaServerLabcCode"], docInfo["ReaServerCompCode"]);
                ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            if (docList.Count > 1)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = string.Format("获取订货单号为:{0},订货机构码为:{1},供货机构码为:{2},的订货信息记录数为{3}!", docInfo["OrderDocNo"], docInfo["ReaServerLabcCode"], docInfo["ReaServerCompCode"], docList.Count);
                ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }

            ReaBmsCenOrderDoc doc = docList[0];
            if (doc.Status <= 2 || doc.Status.ToString() == ReaBmsOrderDocStatus.全部验收.Key)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "订货单号：" + doc.OrderDocNo + "的状态为：" + ReaBmsOrderDocStatus.GetStatusDic()[doc.Status.ToString()].Name + "！";
                return baseResultDataValue;
            }
            string dtlHql = "reabmscenorderdtl.OrderDocID=" + doc.Id;
            //待验收的订货明细
            orderDtlOfConfirmVOList = IBReaBmsCenOrderDtl.SearchReaOrderDtlOfConfirmVOListByHQL(dtlHql, "", -1, -1);
            if (orderDtlOfConfirmVOList == null || orderDtlOfConfirmVOList.count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = string.Format("获取订货单号为:{0},订货机构码为:{1},供货机构码为:{2},的订货明细信息为空!", docInfo["OrderDocNo"], docInfo["ReaServerLabcCode"], docInfo["ReaServerCompCode"]);
                ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            ZhiFang.Common.Log.Log.Debug("订单供货导入信息.OrderDocNo:" + doc.OrderDocNo + ",CompanyName:" + doc.CompanyName + ",SaleDocNo:" + docInfo["SaleDocNo"]);
            //供货时相同货品多批号的新增记录信息
            IList<ReaOrderDtlOfConfirmVO> addOfConfirmVOList = new List<ReaOrderDtlOfConfirmVO>();
            //for (int i = 0; i < orderDtlOfConfirmVOList.list.Count; i++)
            for (int i = orderDtlOfConfirmVOList.list.Count - 1; i >= 0; i--)
            {
                orderDtlOfConfirmVOList.list[i].ReaCompID = doc.ReaCompID.Value;
                orderDtlOfConfirmVOList.list[i].CompanyName = doc.CompanyName;
                orderDtlOfConfirmVOList.list[i].ReaServerCompCode = doc.ReaServerCompCode;
                orderDtlOfConfirmVOList.list[i].ReaCompCode = doc.ReaCompCode;
                orderDtlOfConfirmVOList.list[i].SaleDocNo = docInfo["SaleDocNo"];

                #region 2020-04-23解析
                //按供货商货品编码位+批号+包装单分组
                var cenOrgGoodsNo = orderDtlOfConfirmVOList.list[i].CenOrgGoodsNo;
                var goodsUnit = orderDtlOfConfirmVOList.list[i].GoodsUnit;
                var dataRowList = dtlTable.Rows.Cast<DataRow>().Where(dataRow => dataRow["CenOrgGoodsNo"].ToString() == cenOrgGoodsNo && dataRow["GoodsUnit"].ToString() == goodsUnit).ToList<DataRow>();
                //.GroupBy(dataRow => new { CenOrgGoodsNo = dataRow["CenOrgGoodsNo"].ToString(), LotNo = dataRow["LotNo"].ToString(), GoodsUnit = dataRow["GoodsUnit"].ToString() });
                if (dataRowList.Count > 0)
                {
                    //先处理第一条记录
                    orderDtlOfConfirmVOList.list[i] = GetOrderDtlVO(orderDtlOfConfirmVOList.list[i], dataRowList[0]);
                    //同供货货品存在多批号的多条供货记录
                    if (dataRowList.Count > 1)
                    {
                        //将其他的批号记录添加到orderDtlList中
                        for (int j = 1; j < dataRowList.Count; j++)
                        {
                            ReaOrderDtlOfConfirmVO vo = ClassMapperHelp.GetMapper<ReaOrderDtlOfConfirmVO, ReaOrderDtlOfConfirmVO>(orderDtlOfConfirmVOList.list[i]);
                            vo = GetOrderDtlVO(vo, dataRowList[j]);
                            addOfConfirmVOList.Add(vo);
                        }
                    }
                }
                else
                {
                    //本次不存在订货货品的供货信息,移除订货信息
                    orderDtlOfConfirmVOList.list.RemoveAt(i);
                }
                #endregion

            }
            if (addOfConfirmVOList.Count > 0)
            {
                orderDtlOfConfirmVOList.list = orderDtlOfConfirmVOList.list.Concat(addOfConfirmVOList).ToList();
            }
            orderDtlOfConfirmVOList.list = orderDtlOfConfirmVOList.list.OrderBy(p => p.CenOrgGoodsNo).ThenBy(p => p.LotNo).ToList();
            orderDtlOfConfirmVOList.count = orderDtlOfConfirmVOList.list.Count();
            //ZhiFang.Common.Log.Log.Debug("orderDtlList.count:" + orderDtlOfConfirmVOList.count);
            return baseResultDataValue;
        }
        private ReaOrderDtlOfConfirmVO GetOrderDtlVO(ReaOrderDtlOfConfirmVO orderDtlVO, DataRow curDataRow)
        {
            if (curDataRow.Table.Columns.Contains("LotNo"))
                orderDtlVO.LotNo = curDataRow["LotNo"].ToString();
            if (curDataRow.Table.Columns.Contains("ProdDate"))
                orderDtlVO.ProdDate = curDataRow["ProdDate"].ToString();
            if (curDataRow.Table.Columns.Contains("InvalidDate"))
                orderDtlVO.InvalidDate = curDataRow["InvalidDate"].ToString();
            if (curDataRow.Table.Columns.Contains("RegisterNo"))
                orderDtlVO.RegisterNo = curDataRow["RegisterNo"].ToString();
            if (curDataRow.Table.Columns.Contains("ProdOrgName"))
                orderDtlVO.ProdOrgName = curDataRow["ProdOrgName"].ToString();
            if (curDataRow.Table.Columns.Contains("StorageType"))
                orderDtlVO.StorageType = curDataRow["StorageType"].ToString();
            double saleGoodsQty = 0;
            if (curDataRow.Table.Columns.Contains("SaleGoodsQty") && !string.IsNullOrEmpty(curDataRow["SaleGoodsQty"].ToString()))
            {
                double.TryParse(curDataRow["SaleGoodsQty"].ToString(), out saleGoodsQty);
            }
            orderDtlVO.SaleGoodsQty = saleGoodsQty;
            orderDtlVO.DtlGoodsQty = saleGoodsQty;
            ZhiFang.Common.Log.Log.Debug("订单供货导入.CenOrgGoodsNo:" + orderDtlVO.CenOrgGoodsNo + ",LotNo:" + orderDtlVO.LotNo + ",GoodsUnit:" + orderDtlVO.GoodsUnit);
            return orderDtlVO;
        }
        private void GetSheetCellInfo(HttpPostedFile file, ref Dictionary<string, string> docInfo, ref int dtlFristRowIndex, ref int dtlCellCount, ref int dCellIndex, ref DataTable dtlTable, ref Dictionary<string, int> dtlColumns)
        {
            //明细的结束行索引
            int dtlLastRowIndex = -1;
            Stream fsTemplate = file.InputStream;
            IWorkbook wbReslut;
            //Excel模板Sheet
            ISheet templateSheet;
            string excelName = file.FileName;
            //string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            //string subPath = "\\TempExcelFile\\" + labID + "\\ReaBmsCenOrderDoc\\";
            //string filePath = basePath + subPath + excelName;
            //if (!Directory.Exists(basePath + subPath))
            //{
            //    Directory.CreateDirectory(basePath + subPath);
            //}
            if (excelName.Contains(".xlsx"))
            {
                wbReslut = ExcelRuleInfoHelp.ReadTemplateIWorkbook(fsTemplate) as XSSFWorkbook;
                templateSheet = wbReslut.GetSheetAt(0) as XSSFSheet;
            }
            else
            {
                wbReslut = ExcelRuleInfoHelp.ReadTemplateIWorkbook(fsTemplate) as HSSFWorkbook;
                templateSheet = wbReslut.GetSheetAt(0) as HSSFSheet;
            }
            int rowCount = templateSheet.LastRowNum;
            IRow curRow = null;
            for (int rowIndex = 1; rowIndex <= rowCount; rowIndex++)
            {
                curRow = templateSheet.GetRow(rowIndex);
                //判断每行第一列,识别出当前行是明细内容行还是总单信息行或其他内容行
                //dtlFristRowIndex>0并且当前行的列数等于第一行的明细内容行列数
                #region 明细内容行处理
                if (curRow != null && dtlFristRowIndex > 0 && dtlLastRowIndex < 0 && dtlCellCount > 0 && curRow.LastCellNum == dtlCellCount)
                {
                    //ZhiFang.Common.Log.Log.Debug("RowIndex:" + rowIndex);
                    int cellCount = curRow.LastCellNum;
                    ICell cell = curRow.GetCell(dCellIndex);
                    if (cell == null)
                        continue;
                    string cellValue = ExcelRuleInfoHelp.MyGetCellValue(cell);
                    if (string.IsNullOrEmpty(cellValue))
                        continue;
                    //ZhiFang.Common.Log.Log.Debug("RowIndex:" + rowIndex + ",dCellIndex:" + dCellIndex + ",CellValue:" + cellValue);
                    DataRow dataRow = dtlTable.NewRow();
                    foreach (var column in dtlColumns)
                    {
                        ICell cell2 = curRow.GetCell(column.Value);
                        cellValue = ExcelRuleInfoHelp.MyGetCellValue(cell2);
                        if (column.Key == "DispOrder")
                        {
                            int dispOrder = 0;
                            //明细内容行的序列号列是否有值,没有值时表示明细内容行结束
                            if (string.IsNullOrEmpty(cellValue) || !int.TryParse(cellValue, out dispOrder))
                            {
                                dtlLastRowIndex = rowIndex;
                                dataRow = null;
                                ZhiFang.Common.Log.Log.Debug("明细内容结束行索引:" + dtlLastRowIndex);
                                break;
                            }
                        }
                        if (dataRow != null)
                            dataRow[column.Key] = cellValue;
                        //ZhiFang.Common.Log.Log.Debug("CommonName:" + column.Key + ",CellValue:" + cellValue);
                    }

                    if (dataRow != null && dataRow["DispOrder"].ToString().Length > 0)
                        dtlTable.Rows.Add(dataRow);

                }
                #endregion
                #region 总单信息及明细规则信息处理
                else if (curRow != null)
                {
                    int cellCount = curRow.LastCellNum;
                    #region  for ICell
                    for (int cellIndex = 0; cellIndex < cellCount; cellIndex++)
                    {
                        ICell cell = curRow.GetCell(cellIndex);
                        if (cell != null)
                        {
                            string cellValue = ExcelRuleInfoHelp.MyGetCellValue(cell);
                            string columnName = "";
                            if (!string.IsNullOrEmpty(cellValue))
                            {
                                //ZhiFang.Common.Log.Log.Debug(",CellValue:" + cellValue);
                                #region 订单总单信息
                                if (cellValue == "订货机构码")
                                {
                                    string cellValue2 = ExcelRuleInfoHelp.MyGetCellValue(curRow.GetCell(cellIndex + 1));
                                    docInfo["ReaServerLabcCode"] = cellValue2;
                                }
                                else if (cellValue == "订货单号")
                                {
                                    string cellValue2 = ExcelRuleInfoHelp.MyGetCellValue(curRow.GetCell(cellIndex + 1));
                                    docInfo["OrderDocNo"] = cellValue2;
                                }
                                else if (cellValue == "供货机构码")
                                {
                                    string cellValue2 = ExcelRuleInfoHelp.MyGetCellValue(curRow.GetCell(cellIndex + 1));
                                    docInfo["ReaServerCompCode"] = cellValue2;
                                }
                                else if (cellValue == "供货单号")
                                {
                                    string saleDocNo = ExcelRuleInfoHelp.MyGetCellValue(curRow.GetCell(cellIndex + 1));
                                    docInfo["SaleDocNo"] = saleDocNo;
                                }
                                #endregion
                                #region 订单明细信息
                                else if (cellValue == "序号")
                                {
                                    dtlFristRowIndex = rowIndex + 1;
                                    //明细行的总列数
                                    dtlCellCount = cellCount;
                                    dCellIndex = cellIndex;
                                    columnName = "DispOrder";
                                    //ZhiFang.Common.Log.Log.Debug("明细内容开始行索引:" + dtlFristRowIndex);
                                }
                                else if (cellValue == "供货商货品码")
                                {
                                    //供货商货品编码
                                    columnName = "CenOrgGoodsNo";
                                }
                                else if (cellValue == "单位")
                                {
                                    columnName = "GoodsUnit";
                                }
                                else if (cellValue == "规格")
                                {
                                    columnName = "UnitMemo";
                                }
                                else if (cellValue == "数量")
                                {
                                    //本次供货数
                                    columnName = "SaleGoodsQty";
                                }
                                else if (cellValue == "批号")
                                {
                                    columnName = "LotNo";
                                }
                                else if (cellValue == "生产日期")
                                {
                                    columnName = "ProdDate";
                                }
                                else if (cellValue == "有效期至" || cellValue == "失效日期")
                                {
                                    columnName = "InvalidDate";
                                }
                                else if (cellValue == "注册证号")
                                {
                                    columnName = "RegisterNo";
                                }
                                else if (cellValue == "生产许可证号")
                                {
                                    //dtlColumns.Add("RegisterNo", cellIndex);
                                    columnName = "";
                                }
                                else if (cellValue == "生产厂家")
                                {
                                    columnName = "ProdOrgName";
                                }
                                else if (cellValue == "温度保存条件")
                                {
                                    columnName = "StorageType";
                                }
                                else
                                {
                                    //ZhiFang.Common.Log.Log.Debug("CommonName:" + columnName + ",RowIndex:" + rowIndex + ",CellIndex:" + cellIndex+ ",CellValue:"+ cellValue);
                                }
                                #endregion
                            }
                            if (!string.IsNullOrEmpty(columnName))
                            {
                                dtlColumns.Add(columnName, cellIndex);
                                DataColumn column = new DataColumn(columnName);
                                dtlTable.Columns.Add(column);
                                //ZhiFang.Common.Log.Log.Debug("CommonName:" + columnName + ",CellIndex:" + cellIndex);
                            }
                        }
                    }
                    #endregion
                }
                #endregion
            }
        }

        #endregion

        #region 客户端供货验收后，改写订单的单据状态（部分验收、全部验收）

        public BaseResultBool EditReaBmsCenOrderDocStatus(long orderDocID, IList<ReaBmsCenSaleDtlConfirm> tempDtlConfirmList, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            bool isAll = true;
            ReaBmsCenOrderDoc reaBmsCenOrderDoc = this.Get(orderDocID);
            IList<ReaBmsCenOrderDtl> orderDtlList = IBReaBmsCenOrderDtl.SearchListByHQL(string.Format("OrderDocID={0}", orderDocID));
            foreach (var orderDtl in orderDtlList)
            {
                var sumCount = tempDtlConfirmList.Where(p => p.ReaGoodsID == orderDtl.ReaGoodsID && p.Status > 0 && p.Status < 4).Sum(p => p.AcceptCount);
                if (sumCount != orderDtl.GoodsQty.Value)
                {
                    isAll = false;
                    break;
                }
            }

            if (isAll)
            {
                //订单状态=全部验收
                reaBmsCenOrderDoc.Status = int.Parse(ReaBmsOrderDocStatus.全部验收.Key);
                reaBmsCenOrderDoc.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[reaBmsCenOrderDoc.Status.ToString()].Name;
            }
            else
            {
                //订单状态=部分验收
                reaBmsCenOrderDoc.Status = int.Parse(ReaBmsOrderDocStatus.部分验收.Key);
                reaBmsCenOrderDoc.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[reaBmsCenOrderDoc.Status.ToString()].Name;
            }
            this.Entity = reaBmsCenOrderDoc;
            tempBaseResultBool.success = this.Edit();

            if (tempBaseResultBool.success) AddReaReqOperation(reaBmsCenOrderDoc, empID, empName);

            return tempBaseResultBool;
        }

        #endregion

        /// <summary>
        /// 更新订单同步到第三方系统标志，1为已经同步到第三方
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <returns></returns>
        public BaseResultDataValue EditReaBmsCenOrderDocThirdFlagAndStatus(long id, int isThirdFlag, int status)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ReaBmsCenOrderDoc orderDoc = this.Get(id);
            orderDoc.IsThirdFlag = isThirdFlag;
            if (isThirdFlag == int.Parse(ReaBmsOrderDocThirdFlag.同步成功.Key))
            {
                //第三方数据同步成功，也要改单据状态=订单上传
                orderDoc.Status = status;
                orderDoc.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[status.ToString()].Name;
            }
            this.Entity = orderDoc;
            this.Edit();
            return brdv;
        }

        /// <summary>
        /// 更新订单同步到第三方系统标志，1为已经同步到第三方
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <returns></returns>
        public BaseResultDataValue EditReaBmsCenOrderDocThirdFlag(long id, int isThirdFlag)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ReaBmsCenOrderDoc orderDoc = this.Get(id);
            orderDoc.IsThirdFlag = isThirdFlag;
            this.Entity = orderDoc;
            this.Edit();
            return brdv;
        }

        public BaseResultDataValue EditReaBmsCenOrderDocThirdFlag(long id, int isThirdFlag, string otherOrderDocID, string otherOrderDocNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            ReaBmsCenOrderDoc orderDoc = this.Get(id);
            orderDoc.IsThirdFlag = isThirdFlag;
            this.Entity = orderDoc;
            brdv.success = this.Edit();
            if (brdv.success && ((!string.IsNullOrEmpty(otherOrderDocID)) || (!string.IsNullOrEmpty(otherOrderDocNo))))
                brdv = AddOtherOrderDocNoByInterface(id, otherOrderDocID, otherOrderDocNo);
            return brdv;
        }


        public BaseResultDataValue AddOtherOrderDocNoByInterface(long orderDocID, string otherOrderDocID, string otherOrderDocNo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            IList<ReaBmsCenOrderDtl> listBmsCenOrderDtl = IBReaBmsCenOrderDtl.SearchListByHQL(" reabmscenorderdtl.OrderDocID=" + orderDocID.ToString());
            if (listBmsCenOrderDtl != null && listBmsCenOrderDtl.Count > 0)
            {
                if (otherOrderDocNo != null && otherOrderDocNo.Trim().Length > 0)
                {
                    foreach (ReaBmsCenOrderDtl orderDtl in listBmsCenOrderDtl)
                    {
                        if (!string.IsNullOrEmpty(otherOrderDocNo))
                            orderDtl.OtherOrderDocNo = otherOrderDocNo;
                        else
                            orderDtl.OtherOrderDocNo = otherOrderDocID;
                        IBReaBmsCenOrderDtl.Entity = orderDtl;
                        IBReaBmsCenOrderDtl.Edit();
                    }
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "AddOtherOrderDocNoByInterface：订货单中无货品明细信息！ID：" + orderDocID.ToString();
                ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        #region 客户端与平台不在同一数据库--客户端部分

        public BaseResultDataValue GetUploadPlatformReaOrderDocAndDtl(long id, ref JObject jPostData, ref ReaBmsCenOrderDoc orderDoc)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();
            jPostData = new JObject();
            orderDoc = this.Get(id);
            //订单是否已经上传判断
            if (orderDoc.IOFlag == int.Parse(ReaBmsOrderDocIOFlag.已上传.Key))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "订单号为:" + orderDoc.OrderDocNo + ",已经上传!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }

            ReaBmsCenOrderDoc orderDoc2 = ClassMapperHelp.GetMapper<ReaBmsCenOrderDoc, ReaBmsCenOrderDoc>(orderDoc);
            orderDoc2.LabOrderDocID = orderDoc2.Id;
            IList<ReaBmsCenOrderDtl> orderDtlList = IDReaBmsCenOrderDtlDao.GetListByHQL("reabmscenorderdtl.OrderDocID=" + id);

            orderDoc2.DataTimeStamp = null;
            string orderDocStr = JsonHelper.ObjectToJson(orderDoc2);
            ZhiFang.Common.Log.Log.Debug("orderDocStr:" + orderDocStr);
            //ReaBmsCenOrderDoc orderDoc3 = JsonHelper.JsonDeserializeObject<ReaBmsCenOrderDoc>(orderDocStr);

            JObject jorderDoc = JsonHelper.GetPropertyInfo<ReaBmsCenOrderDoc>(orderDoc2);
            jPostData.Add(ZFPlatformHelp.订货总单.Key, jorderDoc);

            ////订单明细货品平台编码+供应商货品编码验证
            JArray jdtlList = new JArray();
            foreach (ReaBmsCenOrderDtl orderDtl in orderDtlList)
            {
                //判断是否存在供货商货品编码
                if (string.IsNullOrEmpty(orderDtl.CenOrgGoodsNo))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "订货明细的货品为:" + orderDtl.ReaGoodsName + ",供货商货品编码为空!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }
                ReaBmsCenOrderDtl orderDtl2 = ClassMapperHelp.GetMapper<ReaBmsCenOrderDtl, ReaBmsCenOrderDtl>(orderDtl);
                orderDtl2.DataTimeStamp = null;
                //判断是否存在平台货品编码
                if (string.IsNullOrEmpty(orderDtl2.GoodsNo))
                {
                    orderDtl2.GoodsNo = orderDtl2.CenOrgGoodsNo;
                }
                JObject jorderDtl = JsonHelper.GetPropertyInfo<ReaBmsCenOrderDtl>(orderDtl2);
                jdtlList.Add(jorderDtl);
            }
            jPostData.Add(ZFPlatformHelp.订货明细单.Key, jdtlList);//"orderDtlList"

            return baseresultdata;
        }

        #endregion

        #region 客户端与平台不在同一数据库--平台部分
        public BaseResultDataValue AddReaOrderDocAndDtlOfUpload(ReaBmsCenOrderDoc orderDoc, bool isVerifyProdGoodsNo, IList<ReaBmsCenOrderDtl> orderDtlList, long empID, string empName)
        {
            BaseResultDataValue baseresultdata = new BaseResultDataValue();

            if (orderDoc == null)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入参数(orderDoc)订单信息为空!";
                return baseresultdata;
            }
            if (orderDtlList == null || orderDtlList.Count < 0)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "传入参数(orderDtlList)订单明细为空!";
                return baseresultdata;
            }

            #region 测试还原客户端订单信息
            //JObject jPostData = new JObject();
            //JObject jorderDoc = GetPropertyInfo<ReaBmsCenOrderDoc>(orderDoc);
            //ZhiFang.Common.Log.Log.Debug("jorderDoc:" + jorderDoc.ToString().Replace(Environment.NewLine, ""));
            //jPostData.Add("orderDoc", jorderDoc);
            //JArray jdtlList = new JArray();
            //foreach (ReaBmsCenOrderDtl orderDtl in orderDtlList)
            //{
            //    JObject jorderDtl = GetPropertyInfo<ReaBmsCenOrderDtl>(orderDtl);
            //    jdtlList.Add(jorderDtl);
            //}
            //jPostData.Add("orderDtlList", jdtlList);
            //string postDataStr = jPostData.ToString().Replace(Environment.NewLine, "");
            //postDataStr = "{\"postData\":\"" + postDataStr.Replace("\"", "\\\"") + "\"}";
            //ZhiFang.Common.Log.Log.Debug("还原客户端订单信息.postData:" + postDataStr);
            #endregion

            //if (!orderDoc.IOFlag.HasValue)
            orderDoc.IOFlag = int.Parse(ReaBmsOrderDocIOFlag.已上传.Key);
            orderDoc.Status = int.Parse(ReaBmsOrderDocStatus.订单上传.Key);
            orderDoc.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[orderDoc.Status.ToString()].Name;

            string orderDocNo = orderDoc.OrderDocNo;
            //订货方所属机构平台编码
            string reaServerLabcCode = orderDoc.ReaServerLabcCode;
            //供货方所属机构平台编码
            string reaServerCompCode = orderDoc.ReaServerCompCode;
            ZhiFang.Common.Log.Log.Debug(string.Format("订单号为:{0},订货方所属机构平台编码为:{1},供货方所属机构平台编码为:{2}", orderDocNo, reaServerLabcCode, reaServerCompCode));
            if (string.IsNullOrEmpty(orderDocNo))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "订单号为空!";
                return baseresultdata;
            }
            if (string.IsNullOrEmpty(reaServerLabcCode))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "订货方所属机构平台编码为空!";
                return baseresultdata;
            }
            if (string.IsNullOrEmpty(reaServerCompCode))
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "供货方所属机构平台编码为空!";
                return baseresultdata;
            }

            //订单是否已经上传过(订货方所属机构平台编码+订单号)
            IList<ReaBmsCenOrderDoc> tempDocList = this.SearchListByHQL("(reabmscenorderdoc.OrderDocNo='" + orderDocNo + "' and reabmscenorderdoc.ReaServerLabcCode ='" + reaServerLabcCode + "')");
            if (tempDocList.Count > 0)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "订单号为:" + orderDocNo + ",订货方所属机构平台编码为:" + reaServerLabcCode + ",已上传过,请不要重复上传!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            //验证客户端上传的订单的labId是否存在平台上
            IList<CenOrg> compCenOrgList = IDCenOrgDao.GetListByHQL("cenorg.Visible=1 and cenorg.OrgNo=" + reaServerCompCode);
            if (compCenOrgList == null || compCenOrgList.Count < 0)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "获取订单所属供货方机构平台编码机构(CenOrg)信息为空!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            else if (compCenOrgList.Count > 1)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "获取订单所属供货方机构平台编码的机构(CenOrg)信息存在多个!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            //供货方机构信息
            CenOrg compCenOrg = compCenOrgList[0];
            orderDoc.LabID = compCenOrg.LabID;
            ZhiFang.Common.Log.Log.Debug("平台供货方机构Id(LabID):" + compCenOrg.LabID);
            ZhiFang.Common.Log.Log.Debug("平台供货方Id(CenOrg):" + compCenOrg.Id);

            //平台供货商的订货方信息
            IList<ReaCenOrg> labcReaCenOrgList = IDReaCenOrgDao.GetListByHQL(string.Format("reacenorg.Visible=1 and reacenorg.PlatformOrgNo={0} and reacenorg.LabID={1}", reaServerLabcCode, compCenOrg.LabID));
            if (labcReaCenOrgList == null || labcReaCenOrgList.Count < 0)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "获取平台供货方机构的所属订货方信息(ReaCenOrg)为空!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            else if (labcReaCenOrgList.Count > 1)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "获取平台供货方机构的所属订货方信息(ReaCenOrg)存在多个!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }
            //平台供货方机构的所属订货方信息
            ReaCenOrg labcReaCenOrg = labcReaCenOrgList[0];
            ZhiFang.Common.Log.Log.Debug("平台供货方机构的所属订货方ID:" + labcReaCenOrg.Id);

            //平台供货商是否存在订货方的订货方货品信息
            IList<ReaGoodsOrgLink> orglinkList = IDReaGoodsOrgLinkDao.GetListByHQL(string.Format("reagoodsorglink.Visible=1 and reagoodsorglink.CenOrg.Id={0} and reagoodsorglink.CenOrg.OrgType={1} and reagoodsorglink.LabID={2}", labcReaCenOrg.Id, ReaCenOrgType.订货方.Key, compCenOrg.LabID));
            if (orglinkList == null || orglinkList.Count < 0)
            {
                baseresultdata.success = false;
                baseresultdata.ErrorInfo = "获取平台供货方机构的所属订货方货品信息(ReaGoodsOrgLink)为空!";
                ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                return baseresultdata;
            }

            #region 转换客户端订单明细的货品为平台供货方机构的订货方货品
            for (int i = 0; i < orderDtlList.Count; i++)
            {
                if (!orderDtlList[i].LabOrderDtlID.HasValue)
                    orderDtlList[i].LabOrderDtlID = orderDtlList[i].Id;
                ReaBmsCenOrderDtl orderDtl = orderDtlList[i];

                //判断是否存在供货商货品编码
                if (string.IsNullOrEmpty(orderDtl.CenOrgGoodsNo))
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "订货明细的货品为:" + orderDtl.ReaGoodsName + ",供货商货品编码为空!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }

                //订单货品明细转换为平台供货商的订货方货品信息
                ReaGoodsOrgLink orglink = null;
                var tempOrgLinkList = orglinkList.Where(p => p.CenOrgGoodsNo == orderDtl.CenOrgGoodsNo);
                if (tempOrgLinkList == null || tempOrgLinkList.Count() <= 0)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "订货明细的供应商货品编码为:" + orderDtl.CenOrgGoodsNo + ",货品平台编码为:" + orderDtl.GoodsNo + ",的订货方货品关系不存在!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }
                else if (tempOrgLinkList.Count() > 1)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "订货明细的供应商货品编码为:" + orderDtl.CenOrgGoodsNo + ",条码类型为:" + orderDtl.BarCodeType + ",的订货方货品关系存在多个,请重新维护再后再上传!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }
                else
                {
                    //订货方货品关系信息
                    orglink = tempOrgLinkList.ElementAt(0);
                }
                if (orglink == null)
                {
                    baseresultdata.success = false;
                    baseresultdata.ErrorInfo = "订货明细的供应商货品编码为:" + orderDtl.CenOrgGoodsNo + "转换为平台供货商的订货方货品信息为空!";
                    ZhiFang.Common.Log.Log.Error(baseresultdata.ErrorInfo);
                    return baseresultdata;
                }
                GetCenOrderDtl(compCenOrg, orglink, orderDoc, ref orderDtl);
                orderDtlList[i] = orderDtl;
            }
            #endregion

            baseresultdata = AddReaBmsCenOrderDocAndDt(orderDoc, orderDtlList, 1, empID, empName);

            return baseresultdata;
        }
        //将客户端上传的订货明细的货品信息转换为平台的订货方货品信息
        private void GetCenOrderDtl(CenOrg saleCcenOrg, ReaGoodsOrgLink orglink, ReaBmsCenOrderDoc orderDoc, ref ReaBmsCenOrderDtl orderDtl)
        {
            orderDtl.LabID = saleCcenOrg.LabID;
            orderDtl.OrderDocID = orderDoc.Id;
            orderDtl.OrderDocNo = orderDoc.OrderDocNo;
            orderDtl.IOFlag = orderDoc.IOFlag;
            orderDtl.BarCodeType = orglink.BarCodeType;
            orderDtl.IsPrintBarCode = orglink.IsPrintBarCode;
            orderDtl.ProdOrgNo = orglink.ProdGoodsNo;
            orderDtl.ReaGoodsID = orglink.ReaGoods.Id;
            orderDtl.ReaGoodsName = orglink.ReaGoods.CName;
            orderDtl.GoodsID = orglink.ReaGoods.Id;
            orderDtl.GoodsName = orglink.ReaGoods.CName;

            orderDtl.ReaGoodsNo = orglink.ReaGoods.ReaGoodsNo;
            orderDtl.GoodsNo = orglink.ReaGoods.GoodsNo;
            orderDtl.GoodsUnit = orglink.ReaGoods.UnitName;
            orderDtl.UnitMemo = orglink.ReaGoods.UnitMemo;
            orderDtl.GoodsSort = orglink.ReaGoods.GoodsSort;
            //供应商货品机构关系ID
            orderDtl.CompGoodsLinkID = orglink.Id;
            //实验室货品机构关系ID
            //orderDtl.LabcGoodsLinkID = orglink.ReaGoods.CName;
        }

        #endregion

    }
}