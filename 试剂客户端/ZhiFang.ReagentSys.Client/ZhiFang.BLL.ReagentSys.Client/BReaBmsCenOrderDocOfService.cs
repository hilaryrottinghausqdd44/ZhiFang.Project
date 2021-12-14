using System;
using System.Collections.Generic;
using ZhiFang.BLL.Base;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.ReagentSys.Client.Common;
using System.Linq;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Text;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsCenOrderDocOfService : BaseBLL<ReaBmsCenOrderDoc>, IBReaBmsCenOrderDocOfService
    {
        IDReaGoodsOrgLinkDao IDReaGoodsOrgLinkDao { get; set; }
        IBReaBmsCenOrderDtl IBReaBmsCenOrderDtl { get; set; }
        IBReaReqOperation IBReaReqOperation { get; set; }
        IDReaCenOrgDao IDReaCenOrgDao { get; set; }

        #region 公共
        bool EditReaBmsCenOrderDocStatusCheck(ReaBmsCenOrderDoc entity, ReaBmsCenOrderDoc serverEntity, List<string> tmpa, long empID, string empName)
        {
            entity.DataUpdateTime = DateTime.Now;
            if (entity.Status.ToString() == ReaBmsOrderDocStatus.供应商确认.Key)
            {
                if ((serverEntity.Status.ToString() != ReaBmsOrderDocStatus.订单上传.Key && serverEntity.IOFlag.ToString() != ReaBmsOrderDocIOFlag.已上传.Key) && (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.取消确认.Key && serverEntity.IOFlag.ToString() != ReaBmsOrderDocIOFlag.取消确认.Key))
                {
                    return false;
                }
                entity.ConfirmID = empID;
                entity.Confirm = empName;
                tmpa.Add("ConfirmID=" + entity.ConfirmID + " ");
                tmpa.Add("Confirm='" + entity.Confirm + "'");
                tmpa.Add("ConfirmTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("DataUpdateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            if (entity.Status.ToString() == ReaBmsOrderDocStatus.取消确认.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.供应商确认.Key && serverEntity.IOFlag.ToString() != ReaBmsOrderDocIOFlag.供应商确认.Key)
                {
                    return false;
                }
                tmpa.Add("ConfirmID=null");
                tmpa.Add("Confirm=null");
                tmpa.Add("ConfirmTime=null");
                tmpa.Add("DataUpdateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            if (entity.Status.ToString() == ReaBmsOrderDocStatus.订单转供货.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsOrderDocStatus.供应商确认.Key && serverEntity.IOFlag.ToString() != ReaBmsOrderDocIOFlag.供应商确认.Key)
                {
                    return false;
                }
                tmpa.Add("DataUpdateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
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
        #endregion

        #region (实验室订单与平台同在一数据库)供应商确认/取消确认订单
        public BaseResultBool EditReaBmsCenOrderDocAndDt(ReaBmsCenOrderDoc entity, string[] tempArray, IList<ReaBmsCenOrderDtl> dtEditList, long empID, string empName, bool isUpdate)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            ReaBmsCenOrderDoc oldEntity = this.Get(entity.Id);
            List<string> tmpa = tempArray.ToList();
            if (!EditReaBmsCenOrderDocStatusCheck(entity, oldEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "订单ID：" + entity.Id + "的状态为：" + ReaBmsOrderDocStatus.GetStatusDic()[oldEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }
            if (isUpdate)
            {
                tempArray = tmpa.ToArray();
                tempBaseResultBool.success = this.Update(tempArray);
            }
            else
            {
                this.Entity = entity;
                tempBaseResultBool.success = this.Edit();
            }

            if (tempBaseResultBool.success)
            {
                if (dtEditList != null && dtEditList.Count > 0) tempBaseResultBool = EditDtList(dtEditList, entity);
                if (tempBaseResultBool.success) AddReaReqOperation(entity, empID, empName);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditDtList(IList<ReaBmsCenOrderDtl> dtEditList, ReaBmsCenOrderDoc orderDoc)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            foreach (var orderDtl in dtEditList)
            {
                if (tempBaseResultBool.success == false) break;
                if (orderDoc.IOFlag.HasValue)
                    orderDtl.IOFlag = orderDoc.IOFlag.Value;
                IBReaBmsCenOrderDtl.Entity = orderDtl;
                tempBaseResultBool.success = IBReaBmsCenOrderDtl.Edit();
                if (tempBaseResultBool.success == false)
                {
                    tempBaseResultBool.ErrorInfo = "货品为:" + orderDtl.ReaGoodsName + ",保存失败!";
                }
            }
            return tempBaseResultBool;

        }
        public BaseResultBool EditReaBmsCenOrderDocOfComp(ReaBmsCenOrderDoc orderDoc, string[] tempArray, long labId, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (orderDoc == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入参数（entity）为空！";
                return tempBaseResultBool;
            }
            ReaBmsCenOrderDoc serverEntity = this.Get(orderDoc.Id);
            List<string> tmpa = tempArray.ToList();
            if (!EditReaBmsCenOrderDocStatusCheck(orderDoc, serverEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "订单ID：" + orderDoc.Id + "的状态为：" + ReaBmsOrderDocStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + ",数据标志为:" + ReaBmsOrderDocIOFlag.GetStatusDic()[serverEntity.IOFlag.ToString()].Name + "！";
                return tempBaseResultBool;
            }
            tempArray = tmpa.ToArray();

            if (orderDoc.Status.ToString() == ReaBmsOrderDocStatus.供应商确认.Key)
            {
                //运行参数:供应商确认订单时是否需要强制校验货品编码1:是;2:否;(实验室的订单货品的供货商货品编码是否能在供应商的订货方货品关系里能对照匹配上);如果需要校验,货品所属供货商的订货方货品关系的供应商货品编码不能为空
                if (orderDoc.IsVerifyProdGoodsNo == true)
                {
                    //订单的实验室平台机构码是否存在供应商的订货方信息里
                    if (string.IsNullOrEmpty(serverEntity.ReaServerLabcCode))
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = string.Format("订单号为{0},实验室平台机构码(ReaServerLabcCode)为空,不能进行订单转供货单!", serverEntity.OrderDocNo);
                        return tempBaseResultBool;
                    }

                    //确认订单的实验室平台机构码是否存在供应商的订货方信息里
                    IList<ReaCenOrg> tempReaCenOrgList = IDReaCenOrgDao.GetListByHQL(string.Format("reacenorg.Visible=1 and reacenorg.PlatformOrgNo='{0}' and reacenorg.OrgType={1}", serverEntity.ReaServerLabcCode, ReaCenOrgType.订货方.Key));
                    if (tempReaCenOrgList == null || tempReaCenOrgList.Count <= 0)
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = string.Format("订单号为{0},实验室平台机构码({1}),在当前机构的订货方信息里不存在!", serverEntity.OrderDocNo, serverEntity.ReaServerLabcCode);
                        return tempBaseResultBool;
                    }

                    tempBaseResultBool = EditConfirmReaOrderDtlGoodsNoMatchingOfComp(orderDoc, tempArray, labId, empID, empName);
                }
                else
                {
                    //供应商确认订单处理
                    IList<ReaBmsCenOrderDtl> orderDtlList = IBReaBmsCenOrderDtl.SearchListByHQL("reabmscenorderdtl.OrderDocID=" + orderDoc.Id);
                    tempBaseResultBool = EditConfirmReaOrderDocAndDtlOfComp(orderDoc, tempArray, orderDtlList, empID, empName);
                }
            }
            else if (orderDoc.Status.ToString() == ReaBmsOrderDocStatus.取消确认.Key)
            {
                tempBaseResultBool = EditCancelConfirmReaOrderDocOfComp(orderDoc, tempArray, empID, empName);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 实验室的订单货品的供货商货品编码是否能在供应商的订货方货品关系里能对照匹配上
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tempArray"></param>
        /// <param name="labId"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        private BaseResultBool EditConfirmReaOrderDtlGoodsNoMatchingOfComp(ReaBmsCenOrderDoc entity, string[] tempArray, long labId, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            IList<ReaBmsCenOrderDtl> orderDtlList = IBReaBmsCenOrderDtl.SearchListByHQL("reabmscenorderdtl.OrderDocID=" + entity.Id);

            if (orderDtlList.Count <= 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "订单的订货明细信息为空！";
                return tempBaseResultBool;
            }
            StringBuilder hqlStr = new StringBuilder();
            foreach (var orderDtl in orderDtlList)
            {
                if (!string.IsNullOrEmpty(orderDtl.GoodsNo) && !string.IsNullOrEmpty(orderDtl.UnitMemo))
                {
                    hqlStr.Append(string.Format("(reagoodsorglink.CenOrgGoodsNo='{0}' and reagoodsorglink.ReaGoods.UnitName='{1}') or ", orderDtl.CenOrgGoodsNo, orderDtl.GoodsUnit));
                }
                else
                {
                    tempBaseResultBool.success = false;
                    string msg = "";
                    if (string.IsNullOrEmpty(orderDtl.GoodsNo))
                    {
                        msg = "供应商货品编号(GoodsNo)为空";
                    }
                    if (string.IsNullOrEmpty(orderDtl.UnitMemo))
                    {
                        msg = msg + "供应商货品包装单位(UnitMemo)为空";
                    }
                    tempBaseResultBool.ErrorInfo = string.Format("订货明细的货品名称为:{0},{1}！", orderDtl.ReaGoodsName, msg);
                    break;
                }
            }
            if (tempBaseResultBool.success == false)
                return tempBaseResultBool;

            char[] trimChars = new char[] { ' ', 'o', 'r' };
            string hqlWhere = "";
            hqlWhere = string.Format("reagoodsorglink.Visible=1 and reagoodsorglink.LabID={0} and reagoodsorglink.CenOrg.OrgType={1} and ({2})", labId, ReaCenOrgType.订货方.Key, hqlStr.ToString().TrimEnd(trimChars));
            IList<ReaGoodsOrgLink> dtlLinkList = IDReaGoodsOrgLinkDao.GetListByHQL(hqlWhere);

            if (dtlLinkList.Count <= 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "获取供应商所属的订货方货品关系信息为空！";
                return tempBaseResultBool;
            }

            for (int i = 0; i < orderDtlList.Count; i++)
            {
                var tempLinkList = dtlLinkList.Where(p => p.ProdGoodsNo == orderDtlList[i].ProdGoodsNo && p.ReaGoods.UnitName == orderDtlList[i].GoodsUnit);
                if (tempLinkList.Count() > 0)
                {
                    orderDtlList[i].IOFlag = int.Parse(ReaBmsOrderDocIOFlag.供应商确认.Key);
                    orderDtlList[i].LabcGoodsLinkID = tempLinkList.ElementAt(0).Id;
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = string.Format("订货明细的货品名称为:{0},获取供应商所属的订货方货品关系信息为空！", orderDtlList[i].ReaGoodsName);
                    break;
                }
            }
            if (tempBaseResultBool.success == false)
                return tempBaseResultBool;

            //供应商确认订单处理
            tempBaseResultBool = EditConfirmReaOrderDocAndDtlOfComp(entity, tempArray, orderDtlList, empID, empName);

            return tempBaseResultBool;
        }
        /// <summary>
        /// 供应商确认订单,不需要强制校验货品编码
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tempArray"></param>
        /// <param name="orderDtlList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        private BaseResultBool EditConfirmReaOrderDocAndDtlOfComp(ReaBmsCenOrderDoc entity, string[] tempArray, IList<ReaBmsCenOrderDtl> orderDtlList, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (orderDtlList.Count <= 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "订单的订货明细信息为空！";
                return tempBaseResultBool;
            }
            //this.Entity = entity;
            tempBaseResultBool.success = this.Update(tempArray);
            if (tempBaseResultBool.success)
            {
                foreach (var orderDtl in orderDtlList)
                {
                    IBReaBmsCenOrderDtl.Entity = orderDtl;
                    tempBaseResultBool.success = IBReaBmsCenOrderDtl.Edit();
                    if (tempBaseResultBool.success == false)
                    {
                        tempBaseResultBool.ErrorInfo = "订单明细的货品为:" + orderDtl.ReaGoodsName + ",保存失败!";
                        break;
                    }
                }
            }
            else
            {
                tempBaseResultBool.ErrorInfo = "订单供应商确认保存失败!";
            }
            if (tempBaseResultBool.success)
                AddReaReqOperation(entity, empID, empName);
            return tempBaseResultBool;
        }
        private BaseResultBool EditCancelConfirmReaOrderDocOfComp(ReaBmsCenOrderDoc entity, string[] tempArray, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            IList<ReaBmsCenOrderDtl> orderDtlList = IBReaBmsCenOrderDtl.SearchListByHQL("reabmscenorderdtl.OrderDocID=" + entity.Id);
            if (orderDtlList.Count <= 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "订单的订货明细信息为空！";
                return tempBaseResultBool;
            }
            //this.Entity = entity;
            tempBaseResultBool.success = this.Update(tempArray);
            if (tempBaseResultBool.success)
            {
                foreach (var orderDtl in orderDtlList)
                {
                    orderDtl.IOFlag = int.Parse(ReaBmsOrderDocIOFlag.取消确认.Key);
                    orderDtl.LabcGoodsLinkID = null;
                    IBReaBmsCenOrderDtl.Entity = orderDtl;
                    tempBaseResultBool.success = IBReaBmsCenOrderDtl.Edit();
                    if (tempBaseResultBool.success == false)
                    {
                        tempBaseResultBool.ErrorInfo = "订单明细的货品为:" + orderDtl.ReaGoodsName + ",保存失败!";
                        break;
                    }
                }
            }
            else
            {
                tempBaseResultBool.ErrorInfo = "订单供应商确认保存失败!";
            }
            if (tempBaseResultBool.success)
                AddReaReqOperation(entity, empID, empName);
            return tempBaseResultBool;
        }

        #endregion

    }
}