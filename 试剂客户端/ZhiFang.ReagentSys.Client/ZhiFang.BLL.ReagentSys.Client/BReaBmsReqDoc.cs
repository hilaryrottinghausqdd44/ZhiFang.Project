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
using ZhiFang.ReagentSys.Client.Common;
using System.Data;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsReqDoc : BaseBLL<ReaBmsReqDoc>, ZhiFang.IBLL.ReagentSys.Client.IBReaBmsReqDoc
    {
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IDReaGoodsOrgLinkDao IDReaGoodsOrgLinkDao { get; set; }
        IBReaBmsReqDtl IBReaBmsReqDtl { get; set; }
        IBReaBmsCenOrderDoc IBReaBmsCenOrderDoc { get; set; }
        IBReaReqOperation IBReaReqOperation { get; set; }

        public BaseResultDataValue AddReaBmsReqDocAndDt(ReaBmsReqDoc entity, IList<ReaBmsReqDtl> dtAddList, long empID, string empName)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(entity.ReqDocNo))
                entity.ReqDocNo = GetReqDocNo();
            //ZhiFang.Common.Log.Log.Debug("申请总单号:" + entity.ReqDocNo);
            this.Entity = entity;
            tempBaseResultDataValue.success = this.Add();
            if (tempBaseResultDataValue.success)
            {
                if (dtAddList != null && dtAddList.Count > 0)
                {
                    BaseResultBool tempBaseResultBool = IBReaBmsReqDtl.AddDtList(dtAddList, this.Entity, empID, empName);
                    tempBaseResultDataValue.success = tempBaseResultBool.success;
                    tempBaseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                }
            }
            if (tempBaseResultDataValue.success) AddReaReqOperation(this.Entity, empID, empName);
            return tempBaseResultDataValue;
        }
        public BaseResultBool UpdateReaBmsReqDocAndDt(ReaBmsReqDoc entity, string[] tempArray, IList<ReaBmsReqDtl> dtAddList, IList<ReaBmsReqDtl> dtEditList, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            this.Entity = entity;
            ReaBmsReqDoc oldEntity = this.Get(entity.Id);
            List<string> tmpa = tempArray.ToList();
            if (!UpdateReaBmsReqDocStatusCheck(entity, oldEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.ErrorInfo = "部门采购申请ID：" + entity.Id + "的状态为：" + ReaBmsReqDocStatus.GetStatusDic()[oldEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }
            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                ReaBmsReqDoc tempEntity = new ReaBmsReqDoc();
                tempEntity.Id = entity.Id;
                tempEntity.Status = entity.Status;
                tempEntity.ReqDocNo = oldEntity.ReqDocNo;
                if (dtAddList != null && dtAddList.Count > 0) tempBaseResultBool = IBReaBmsReqDtl.AddDtList(dtAddList, tempEntity, empID, empName);
                if (dtEditList != null && dtEditList.Count > 0) tempBaseResultBool = IBReaBmsReqDtl.EditDtList(dtEditList, tempEntity);
                if (tempBaseResultBool.success) AddReaReqOperation(tempEntity, empID, empName);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool UpdateReaBmsReqDtlOfCheck(ReaBmsReqDoc entity, string[] tempArray, IList<ReaBmsReqDtl> dtAddList, IList<ReaBmsReqDtl> dtEditList, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            this.Entity = entity;
            ReaBmsReqDoc oldEntity = this.Get(entity.Id);
            List<string> tmpa = tempArray.ToList();
            if (!UpdateReaBmsReqDocStatusCheck(entity, oldEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.ErrorInfo = "部门采购申请ID：" + entity.Id + "的状态为：" + ReaBmsReqDocStatus.GetStatusDic()[oldEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }
            ReaBmsReqDoc tempEntity = new ReaBmsReqDoc();
            tempEntity.Id = entity.Id;
            tempEntity.Status = entity.Status;
            tempEntity.ReqDocNo = oldEntity.ReqDocNo;
            if (dtAddList != null && dtAddList.Count > 0) tempBaseResultBool = IBReaBmsReqDtl.AddDtList(dtAddList, tempEntity, empID, empName);
            if (dtEditList != null && dtEditList.Count > 0) tempBaseResultBool = IBReaBmsReqDtl.EditDtList(dtEditList, tempEntity);
            if (tempBaseResultBool.success) AddReaReqOperation(entity, empID, empName);
            return tempBaseResultBool;
        }
        public BaseResultBool UpdateReaBmsReqDocAndDtOfCheck(ReaBmsReqDoc entity, string[] tempArray, IList<ReaBmsReqDtl> dtAddList, IList<ReaBmsReqDtl> dtEditList, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            this.Entity = entity;
            ReaBmsReqDoc oldEntity = this.Get(entity.Id);
            List<string> tmpa = tempArray.ToList();
            if (!UpdateReaBmsReqDocStatusCheck(entity, oldEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.ErrorInfo = "部门采购申请ID：" + entity.Id + "的状态为：" + ReaBmsReqDocStatus.GetStatusDic()[oldEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }
            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                ReaBmsReqDoc tempEntity = new ReaBmsReqDoc();
                tempEntity.Id = entity.Id;
                tempEntity.Status = this.Entity.Status;
                tempEntity.ReqDocNo = oldEntity.ReqDocNo;
                if (dtAddList != null && dtAddList.Count > 0) tempBaseResultBool = IBReaBmsReqDtl.AddDtList(dtAddList, tempEntity, empID, empName);
                if (dtEditList != null && dtEditList.Count > 0) tempBaseResultBool = IBReaBmsReqDtl.EditDtList(dtEditList, tempEntity);
                if (tempBaseResultBool.success) AddReaReqOperation(tempEntity, empID, empName);
            }
            return tempBaseResultBool;
        }
        bool UpdateReaBmsReqDocStatusCheck(ReaBmsReqDoc entity, ReaBmsReqDoc serverEntity, List<string> tmpa, long empID, string empName)
        {
            #region 暂存
            if (entity.Status.ToString() == ReaBmsReqDocStatus.暂存.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsReqDocStatus.暂存.Key && serverEntity.Status.ToString() != ReaBmsReqDocStatus.审核退回.Key)
                {
                    return false;
                }
                tmpa.Add("ApplyID=" + empID + " ");
                tmpa.Add("ApplyName='" + empName + "'");
                tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("ReviewManID=null");
                tmpa.Add("ReviewManName=null");
                tmpa.Add("ReviewTime=null");
            }
            #endregion
            #region 申请
            if (entity.Status.ToString() == ReaBmsReqDocStatus.已申请.Key)
            {
                //审核应用时,可以先编辑保存状态为已申请的申请单
                if (serverEntity.Status.ToString() != ReaBmsReqDocStatus.暂存.Key && serverEntity.Status.ToString() != ReaBmsReqDocStatus.已申请.Key && serverEntity.Status.ToString() != ReaBmsReqDocStatus.审核退回.Key)
                {
                    return false;
                }
                if (!entity.ApplyID.HasValue) entity.ApplyID = empID;
                if (string.IsNullOrEmpty(entity.ApplyName)) entity.ApplyName = empName;
                tmpa.Add("ApplyID=" + entity.ApplyID + " ");
                tmpa.Add("ApplyName='" + entity.ApplyName + "'");
                if (!entity.ApplyTime.HasValue) tmpa.Add("ApplyTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");

                tmpa.Add("ReviewManID=null");
                tmpa.Add("ReviewManName=null");
                tmpa.Add("ReviewTime=null");

            }
            #endregion

            if (entity.Status.ToString() == ReaBmsReqDocStatus.审核通过.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsReqDocStatus.已申请.Key && serverEntity.Status.ToString() != ReaBmsReqDocStatus.审核退回.Key)
                {
                    return false;
                }
                if (!entity.ReviewManID.HasValue) entity.ReviewManID = empID;
                if (string.IsNullOrEmpty(entity.ReviewManName)) entity.ReviewManName = empName;
                tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("ReviewManID=" + entity.ReviewManID + " ");
                tmpa.Add("ReviewManName='" + entity.ReviewManName + "'");
                tmpa.Add("ReviewTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }

            if (entity.Status.ToString() == ReaBmsReqDocStatus.审核退回.Key)
            {
                //&& serverEntity.Status.ToString() != ReaBmsReqDocStatus.审核通过.Key
                if (serverEntity.Status.ToString() != ReaBmsReqDocStatus.已申请.Key && serverEntity.Status.ToString() != ReaBmsReqDocStatus.审核退回.Key)
                {
                    return false;
                }
                tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("ReviewManID=null");
                tmpa.Add("ReviewManName=null");
                tmpa.Add("ReviewTime=null");
            }

            if (entity.Status.ToString() == ReaBmsReqDocStatus.转为订单.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsReqDocStatus.审核通过.Key)
                {
                    return false;
                }
                tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            return true;
        }
        public BaseResultBool AddReaCenOrgReaBmsCenOrderDocOfReaBmsReqDocIDStr(string idStr, bool commonIsMerge, bool ugentIsMerge, long empID, string empName, long deptID, string deptName, string reaServerLabcCode, string labcName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(idStr))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的申请主单参数(idStr)为空";
                return tempBaseResultBool;
            }
            IList<ReaBmsReqDtl> reqdtlList = IBReaBmsReqDtl.SearchListByHQL("reabmsreqdtl.ReaBmsReqDoc.Status=" + ReaBmsReqDocStatus.审核通过.Key + "and reabmsreqdtl.ReaBmsReqDoc.Id in(" + idStr + ")");
            if (reqdtlList == null || reqdtlList.Count == 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "获取符合生成订单的申请单数据为空!";
                return tempBaseResultBool;
            }

            //临时的申请明细ID与订单明细ID(同一订单保存成功后回填相关的申请明细的订单明细ID)
            Dictionary<ReaBmsReqDtl, long> orderDtlIdList = new Dictionary<ReaBmsReqDtl, long>();

            #region 生成订单处理
            //将申请单按紧急和常规分类
            var reqdtlUgentList = reqdtlList.Where(p => p.ReaBmsReqDoc.UrgentFlag == 1).ToList();
            var reqdtlCommonList = reqdtlList.Where(p => p.ReaBmsReqDoc.UrgentFlag == 0).ToList();

            //常规分类申请单
            if (reqdtlCommonList != null && reqdtlCommonList.Count > 0)
            {
                //常规是否按申请部门合并
                if (commonIsMerge)
                {
                    //常规(按部门)合并:只按供应商分组生成订单
                    ZhiFang.Common.Log.Log.Debug("订单生成.常规(按部门)合并:只按供应商分组生成订单:");
                    var groupByReaCenOrg = reqdtlCommonList.GroupBy(p => p.ReaCenOrg.Id);
                    IList<ReaBmsReqDtl> tempList2 = new List<ReaBmsReqDtl>();
                    foreach (var groupByList in groupByReaCenOrg)
                    {
                        orderDtlIdList.Clear();
                        ZhiFang.Common.Log.Log.Debug("订单生成.常规(按部门)合并:只按供应商分组生成订单:当前供应商为:" + groupByList.ElementAt(0).ReaCenOrg.CName);
                        tempBaseResultBool = AddCenOrderDocAndDtlOfGroupByReaCenOrg(groupByList.ToList(), ref orderDtlIdList, empID, empName, deptID, deptName, reaServerLabcCode, labcName);
                        if (tempBaseResultBool.success) tempBaseResultBool = AddReaCenOrgReaBmsCenOrderDocOfEditReqDtl(orderDtlIdList);
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("订单生成.常规不(按部门)合并:先按部门分组后再按供应商分组生成订单:");
                    //常规不(按部门)合并:先按部门分组后再按供应商分组生成订单
                    var groupByDept = reqdtlCommonList.GroupBy(p => p.ReaBmsReqDoc.DeptID);
                    foreach (var groupBy in groupByDept)
                    {
                        orderDtlIdList.Clear();
                        tempBaseResultBool = AddCenOrderDocAndDtlOfGroupByReaCenOrgAndDept(groupBy.ToList(), ref orderDtlIdList, empID, empName, deptName, reaServerLabcCode, labcName);

                        if (tempBaseResultBool.success) tempBaseResultBool = AddReaCenOrgReaBmsCenOrderDocOfEditReqDtl(orderDtlIdList);
                    }
                }
            }
            //紧急分类申请单
            if (reqdtlUgentList != null && reqdtlUgentList.Count > 0)
            {
                //紧急申请是否按申请部门合并
                if (ugentIsMerge)
                {
                    ZhiFang.Common.Log.Log.Debug("订单生成.紧急(按部门)合并:只按供应商分组后生成订单:");
                    //紧急(按部门)合并:只按供应商分组后生成订单
                    IList<ReaBmsReqDtl> tempList2 = new List<ReaBmsReqDtl>();
                    var groupByReaCenOrg = reqdtlUgentList.GroupBy(p => p.ReaCenOrg.Id);
                    foreach (var groupByList in groupByReaCenOrg)
                    {
                        orderDtlIdList.Clear();
                        ZhiFang.Common.Log.Log.Debug("订单生成.紧急(按部门)合并:只按供应商分组后生成订单:当前供应商为:" + groupByList.ElementAt(0).ReaCenOrg.CName);
                        tempBaseResultBool = AddCenOrderDocAndDtlOfGroupByReaCenOrg(groupByList.ToList(), ref orderDtlIdList, empID, empName, deptID, deptName, reaServerLabcCode, labcName);
                        if (tempBaseResultBool.success) tempBaseResultBool = AddReaCenOrgReaBmsCenOrderDocOfEditReqDtl(orderDtlIdList);
                    }

                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("订单生成.紧急不(按部门)合并:先按部门分组后再按供应商分组生成订单:");
                    //紧急不(按部门)合并:先按部门分组后再按供应商分组生成订单
                    var groupByDept = reqdtlUgentList.GroupBy(p => p.ReaBmsReqDoc.DeptID);
                    foreach (var groupBy in groupByDept)
                    {
                        orderDtlIdList.Clear();
                        tempBaseResultBool = AddCenOrderDocAndDtlOfGroupByReaCenOrgAndDept(groupBy.ToList(), ref orderDtlIdList, empID, empName, deptName, reaServerLabcCode, labcName);

                        if (tempBaseResultBool.success) tempBaseResultBool = AddReaCenOrgReaBmsCenOrderDocOfEditReqDtl(orderDtlIdList);
                    }
                }
            }
            #endregion

            if (tempBaseResultBool.success)
            {
                //更新申请单为已生成订单的状态
                tempBaseResultBool.success = ((IDReaBmsReqDocDao)base.DBDao).UpdateStatusByIdStr(idStr, int.Parse(ReaBmsReqDocStatus.转为订单.Key));

                //各申请单分别添加操作记录
                if (tempBaseResultBool.success)
                {
                    string[] idArr = idStr.Split(',');
                    foreach (var id in idArr)
                    {
                        ReaBmsReqDoc entity = new ReaBmsReqDoc();
                        entity.Id = long.Parse(id);
                        entity.Status = int.Parse(ReaBmsReqDocStatus.转为订单.Key);
                        AddReaReqOperation(entity, empID, empName);
                    }
                }
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 只供应商合并:按某一供应商合并所有申请货品明细,生成订单
        /// </summary>
        /// <param name="reqdtlList"></param>
        /// <param name="orderDtlIdList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <param name="deptName"></param>
        /// <returns></returns>
        public BaseResultBool AddCenOrderDocAndDtlOfGroupByReaCenOrg(IList<ReaBmsReqDtl> reqdtlList, ref Dictionary<ReaBmsReqDtl, long> orderDtlIdList, long empID, string empName, long deptID, string deptName, string reaServerLabcCode, string labcName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            //临时的货品明细(防止同一货品重复查询数据库)
            Dictionary<long, ReaGoods> reaGoodsTempList = new Dictionary<long, ReaGoods>();
            //临时供应商与货品关系(防止同一货品重复查询数据库)
            Dictionary<long, ReaGoodsOrgLink> goodsOrgLinkTempList = new Dictionary<long, ReaGoodsOrgLink>();
            //var groupBy = reqdtlList.GroupBy(p => p.ReaBmsReqDoc.DeptID);
            //某一订单的订单明细
            Dictionary<string, ReaBmsCenOrderDtl> orderDtlList = new Dictionary<string, ReaBmsCenOrderDtl>();
            ReaBmsCenOrderDoc orderDoc = GetReaBmsCenOrderDoc(empID, empName, reqdtlList[0], false, reaServerLabcCode, labcName);
            if (!orderDoc.TotalPrice.HasValue) orderDoc.TotalPrice = 0;
            //订单的所属部门是否取合并人的所属部门?
            if (!orderDoc.DeptID.HasValue)
                orderDoc.DeptID = deptID;
            if (string.IsNullOrEmpty(orderDoc.DeptName))
            {
                orderDoc.DeptName = deptName;
            }
            //订单明细按申请货品进行分组后合并为同一订单货品
            var groupByReaGoods = reqdtlList.GroupBy(p => p.GoodsID);
            orderDtlIdList.Clear();
            foreach (var group in groupByReaGoods)
            {
                ReaGoods reaGood = null;
                ReaGoodsOrgLink goodsOrgLink = null;

                #region 获取货品信息处理
                if (!reaGoodsTempList.ContainsKey(group.ElementAt(0).GoodsID.Value))
                {
                    reaGood = IDReaGoodsDao.Get(group.ElementAt(0).GoodsID.Value);
                    reaGoodsTempList.Add(group.ElementAt(0).GoodsID.Value, reaGood);
                }
                else
                {
                    reaGood = reaGoodsTempList[group.ElementAt(0).GoodsID.Value];
                }

                if (!goodsOrgLinkTempList.ContainsKey(group.ElementAt(0).GoodsID.Value))
                {
                    goodsOrgLink = IDReaGoodsOrgLinkDao.Get(group.ElementAt(0).CompGoodsLinkID.Value);
                    goodsOrgLinkTempList.Add(group.ElementAt(0).GoodsID.Value, goodsOrgLink);
                }
                else
                {
                    goodsOrgLink = goodsOrgLinkTempList[group.ElementAt(0).GoodsID.Value];
                }
                #endregion

                #region 同一订单明细的申请明细处理
                //申请明细的货品是否合并为同一订单明细记录条件:货品Id+";"+单位+";"+规格
                string key = string.Format("{0};{1};{2}", reaGood.Id, reaGood.UnitName, reaGood.UnitMemo);
                if (!orderDtlList.Keys.Contains(key))
                {
                    ZhiFang.Common.Log.Log.Debug("订单生成.常规(按部门)合并:只按供应商分组生成订单:同一订单明细Key为:" + key);
                    ReaBmsCenOrderDtl orderDtl = GetReaBmsCenOrderDtl(orderDoc, group.ElementAt(0), reaGood, goodsOrgLink);
                    foreach (var reqDtl in group)
                    {
                        reqDtl.OrderDtlNo = orderDtl.OrderDtlNo;
                        if (!orderDtlIdList.ContainsKey(reqDtl)) orderDtlIdList.Add(reqDtl, orderDtl.Id);
                    }
                    orderDtl.ReqGoodsQty = group.Sum(p => p.ReqGoodsQty);
                    orderDtl.GoodsQty = group.Sum(p => p.GoodsQty);
                    orderDtl.Price = goodsOrgLink.Price;
                    orderDtl.SumTotal = orderDtl.GoodsQty * orderDtl.Price;
                    orderDoc.TotalPrice = orderDoc.TotalPrice.Value + orderDtl.SumTotal;
                    orderDtlList.Add(key, orderDtl);
                }
                #endregion
            }
            //保存订单及订单明细
            tempBaseResultBool = IBReaBmsCenOrderDoc.AddReaBmsCenOrderDocAndDtl(orderDoc, orderDtlList, empID, empName);
            return tempBaseResultBool;
        }
        /// <summary>
        /// 按供应商--->再按部门分组合并:按某一供应商合并所有申请货品明细,生成订单
        /// </summary>
        /// <param name="reqdtlList"></param>
        /// <param name="orderDtlIdList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <param name="deptName"></param>
        /// <returns></returns>
        public BaseResultBool AddCenOrderDocAndDtlOfGroupByDeptAndReaCenOrg(IList<ReaBmsReqDtl> reqdtlList, ref Dictionary<ReaBmsReqDtl, long> orderDtlIdList, long empID, string empName, string deptName, string reaServerLabcCode, string labcName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            //临时的货品明细(防止同一货品重复查询数据库)
            Dictionary<long, ReaGoods> reaGoodsTempList = new Dictionary<long, ReaGoods>();
            //临时供应商与货品关系(防止同一货品重复查询数据库)
            Dictionary<long, ReaGoodsOrgLink> goodsOrgLinkTempList = new Dictionary<long, ReaGoodsOrgLink>();
            var groupBy = reqdtlList.GroupBy(p => p.ReaBmsReqDoc.DeptID);
            //某一订单的订单明细
            Dictionary<string, ReaBmsCenOrderDtl> orderDtlList = new Dictionary<string, ReaBmsCenOrderDtl>();
            foreach (var group in groupBy)
            {
                orderDtlIdList.Clear();
                orderDtlList.Clear();

                ReaBmsCenOrderDoc orderDoc = GetReaBmsCenOrderDoc(empID, empName, group.ElementAt(0), true, reaServerLabcCode, labcName);
                if (!orderDoc.TotalPrice.HasValue) orderDoc.TotalPrice = 0;
                #region 某一订单的订单明细处理               
                //申请明细按货品进行排序
                var tempDtList2 = group.OrderBy(p => p.GoodsID);
                for (int i = 0; i < tempDtList2.Count(); i++)
                {
                    var reqDtl = tempDtList2.ElementAt(i);
                    ReaGoods reaGood = null;
                    ReaGoodsOrgLink goodsOrgLink = null;
                    #region 获取货品信息处理
                    if (!reaGoodsTempList.ContainsKey(reqDtl.GoodsID.Value))
                    {
                        reaGood = IDReaGoodsDao.Get(reqDtl.GoodsID.Value);
                        reaGoodsTempList.Add(reqDtl.GoodsID.Value, reaGood);
                    }
                    else
                    {
                        reaGood = reaGoodsTempList[reqDtl.GoodsID.Value];
                    }

                    if (!goodsOrgLinkTempList.ContainsKey(reqDtl.GoodsID.Value))
                    {
                        goodsOrgLink = IDReaGoodsOrgLinkDao.Get(reqDtl.CompGoodsLinkID.Value);
                        goodsOrgLinkTempList.Add(reqDtl.GoodsID.Value, goodsOrgLink);
                    }
                    else
                    {
                        goodsOrgLink = goodsOrgLinkTempList[reqDtl.GoodsID.Value];
                    }
                    #endregion
                    #region 同一订单的订单明细处理
                    //申请明细的货品是否合并为同一订单明细记录条件:货品Id+";"+单位+";"+规格
                    string key = string.Format("{0};{1};{2}", reaGood.Id, reaGood.UnitName, reaGood.UnitMemo);
                    if (!orderDtlList.Keys.Contains(key))
                    {
                        ReaBmsCenOrderDtl orderDtl = GetReaBmsCenOrderDtl(orderDoc, reqDtl, reaGood, goodsOrgLink);
                        reqDtl.OrderDtlNo = orderDtl.OrderDtlNo;
                        orderDtlList.Add(key, orderDtl);
                    }
                    else
                    {
                        orderDtlList[key].ReqGoodsQty += reqDtl.ReqGoodsQty;
                        orderDtlList[key].GoodsQty += reqDtl.GoodsQty;
                        orderDtlList[key].Price = goodsOrgLink.Price;
                        orderDtlList[key].SumTotal = orderDtlList[key].GoodsQty * goodsOrgLink.Price;
                    }
                    #endregion
                    if (!orderDtlIdList.ContainsKey(reqDtl)) orderDtlIdList.Add(reqDtl, orderDtlList[key].Id);
                    if (!orderDtlList[key].SumTotal.HasValue) orderDtlList[key].SumTotal = 0;
                    orderDoc.TotalPrice = orderDoc.TotalPrice.Value + orderDtlList[key].SumTotal.Value;
                }
                #endregion
                //保存订单及订单明细
                tempBaseResultBool = IBReaBmsCenOrderDoc.AddReaBmsCenOrderDocAndDtl(orderDoc, orderDtlList, empID, empName);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 按部门合并-->再按供应商合并:按部门分组后某一部门的所有申请货品明细再按供应商分组生成订单
        /// </summary>
        /// <param name="reqdtlList"></param>
        /// <param name="orderDtlIdList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <param name="deptName"></param>
        /// <returns></returns>
        public BaseResultBool AddCenOrderDocAndDtlOfGroupByReaCenOrgAndDept(IList<ReaBmsReqDtl> reqdtlList, ref Dictionary<ReaBmsReqDtl, long> orderDtlIdList, long empID, string empName, string deptName, string reaServerLabcCode, string labcName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            //临时的货品明细(防止同一货品重复查询数据库)
            Dictionary<long, ReaGoods> reaGoodsTempList = new Dictionary<long, ReaGoods>();
            //临时供应商与货品关系(防止同一货品重复查询数据库)
            Dictionary<long, ReaGoodsOrgLink> goodsOrgLinkTempList = new Dictionary<long, ReaGoodsOrgLink>();
            var groupBy = reqdtlList.GroupBy(p => p.ReaCenOrg.Id);
            //某一订单的订单明细
            Dictionary<string, ReaBmsCenOrderDtl> orderDtlList = new Dictionary<string, ReaBmsCenOrderDtl>();
            foreach (var group in groupBy)
            {
                orderDtlIdList.Clear();
                orderDtlList.Clear();
                ReaBmsCenOrderDoc orderDoc = GetReaBmsCenOrderDoc(empID, empName, group.ElementAt(0), true, reaServerLabcCode, labcName);
                if (!orderDoc.TotalPrice.HasValue) orderDoc.TotalPrice = 0;
                #region 某一订单的订单明细处理               
                //申请明细按货品进行排序
                var tempDtList2 = group.OrderBy(p => p.GoodsID);
                for (int i = 0; i < tempDtList2.Count(); i++)
                {
                    var reqDtl = tempDtList2.ElementAt(i);
                    ReaGoods reaGood = null;
                    ReaGoodsOrgLink goodsOrgLink = null;
                    #region 获取货品信息处理
                    if (!reaGoodsTempList.ContainsKey(reqDtl.GoodsID.Value))
                    {
                        reaGood = IDReaGoodsDao.Get(reqDtl.GoodsID.Value);
                        reaGoodsTempList.Add(reqDtl.GoodsID.Value, reaGood);
                    }
                    else
                    {
                        reaGood = reaGoodsTempList[reqDtl.GoodsID.Value];
                    }

                    if (!goodsOrgLinkTempList.ContainsKey(reqDtl.GoodsID.Value))
                    {
                        goodsOrgLink = IDReaGoodsOrgLinkDao.Get(reqDtl.CompGoodsLinkID.Value);
                        goodsOrgLinkTempList.Add(reqDtl.GoodsID.Value, goodsOrgLink);
                    }
                    else
                    {
                        goodsOrgLink = goodsOrgLinkTempList[reqDtl.GoodsID.Value];
                    }
                    #endregion
                    #region 同一订单的订单明细处理
                    //申请明细的货品是否合并为同一订单明细记录条件:货品Id+";"+单位+";"+规格
                    string key = string.Format("{0};{1};{2}", reaGood.Id, reaGood.UnitName, reaGood.UnitMemo);
                    if (!orderDtlList.Keys.Contains(key))
                    {
                        ReaBmsCenOrderDtl orderDtl = GetReaBmsCenOrderDtl(orderDoc, reqDtl, reaGood, goodsOrgLink);
                        reqDtl.OrderDtlNo = orderDtl.OrderDtlNo;
                        orderDtlList.Add(key, orderDtl);
                    }
                    else
                    {
                        orderDtlList[key].ReqGoodsQty += reqDtl.ReqGoodsQty;
                        orderDtlList[key].GoodsQty += reqDtl.GoodsQty;
                        orderDtlList[key].Price = goodsOrgLink.Price;
                        orderDtlList[key].SumTotal = orderDtlList[key].GoodsQty * goodsOrgLink.Price;
                    }
                    #endregion
                    if (!orderDtlIdList.ContainsKey(reqDtl)) orderDtlIdList.Add(reqDtl, orderDtlList[key].Id);
                    if (!orderDtlList[key].SumTotal.HasValue) orderDtlList[key].SumTotal = 0;
                    orderDoc.TotalPrice = orderDoc.TotalPrice.Value + orderDtlList[key].SumTotal.Value;
                }
                #endregion
                //保存订单及订单明细
                tempBaseResultBool = IBReaBmsCenOrderDoc.AddReaBmsCenOrderDocAndDtl(orderDoc, orderDtlList, empID, empName);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 封装主订单
        /// </summary>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <param name="reqDtl"></param>
        /// <param name="isDeptGroupBy">是否按部门分组</param>
        /// <param name="reaServerLabcCode"></param>
        /// <param name="labcName"></param>
        /// <returns></returns>
        private ReaBmsCenOrderDoc GetReaBmsCenOrderDoc(long empID, string empName, ReaBmsReqDtl reqDtl, bool isDeptGroupBy, string reaServerLabcCode, string labcName)
        {
            ReaBmsCenOrderDoc orderDoc = new ReaBmsCenOrderDoc();
            orderDoc.LabID = reqDtl.LabID;
            orderDoc.ReaServerLabcCode = reaServerLabcCode;
            orderDoc.OrderDocNo = this.GetReqDocNo();
            orderDoc.UserID = empID;
            orderDoc.UserName = empName;

            orderDoc.UrgentFlag = reqDtl.ReaBmsReqDoc.UrgentFlag;
            orderDoc.Status = int.Parse(ReaBmsOrderDocStatus.申请.Key);
            orderDoc.OperDate = DateTime.Now;
            orderDoc.IOFlag = 0;
            orderDoc.LabcName = labcName;
            orderDoc.DeleteFlag = 0;

            orderDoc.CompID = reqDtl.ReaCenOrg.Id;
            orderDoc.CompanyName = reqDtl.ReaCenOrg.CName;
            orderDoc.ReaCompID = reqDtl.ReaCenOrg.Id;
            if (reqDtl.ReaCenOrg.OrgNo.HasValue)
                orderDoc.ReaCompCode = reqDtl.ReaCenOrg.OrgNo.Value.ToString();

            orderDoc.ReaCompanyName = reqDtl.ReaCenOrg.CName;
            orderDoc.ReaServerCompCode = reqDtl.ReaCenOrg.PlatformOrgNo.ToString();
            //按部门分组时,订单带申请部门信息
            if (isDeptGroupBy)
            {
                orderDoc.DeptID = reqDtl.ReaBmsReqDoc.DeptID;
                orderDoc.DeptName = reqDtl.ReaBmsReqDoc.DeptName;
            }
            else
            {
                //订单的所属部门是否取合并人的所属部门?

            }
            orderDoc.PayStaus = long.Parse(ReaBmsOrderDocPayStaus.未付款.Key);
            orderDoc.LabOrderDocID = orderDoc.Id;
            orderDoc.Memo = reqDtl.Memo;

            return orderDoc;
        }
        /// <summary>
        /// 同一订单保存成功后回填相关的申请明细的订单明细ID
        /// </summary>
        /// <param name="orderDtlIDList"></param>
        /// <returns></returns>
        private BaseResultBool AddReaCenOrgReaBmsCenOrderDocOfEditReqDtl(Dictionary<ReaBmsReqDtl, long> dtlIdList)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            List<string> tmpa = new List<string>();
            foreach (var reqDtl in dtlIdList)
            {
                ReaBmsReqDtl entity = reqDtl.Key;
                entity.OrderFlag = 1;//生成订单标志
                entity.OrderDtlID = reqDtl.Value;
                IBReaBmsReqDtl.Entity = entity;
                IBReaBmsReqDtl.Edit();
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 获取新增的订单明细
        /// </summary>
        /// <param name="orderDoc"></param>
        /// <param name="reqDtl"></param>
        /// <param name="reaGood"></param>
        /// <param name="goodsOrgLink"></param>
        /// <returns></returns>
        private ReaBmsCenOrderDtl GetReaBmsCenOrderDtl(ReaBmsCenOrderDoc orderDoc, ReaBmsReqDtl reqDtl, ReaGoods reaGood, ReaGoodsOrgLink goodsOrgLink)
        {
            ReaBmsCenOrderDtl orderDtl = new ReaBmsCenOrderDtl();
            orderDtl.LabID = reqDtl.LabID;
            orderDtl.ProdGoodsNo = reaGood.ProdGoodsNo;

            //byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            orderDtl.OrderDocID = orderDoc.Id;
            orderDtl.CompGoodsLinkID = reqDtl.CompGoodsLinkID;
            orderDtl.OrderDocNo = orderDoc.OrderDocNo;
            orderDtl.OrderDtlNo = GetReqDocNo();
            orderDtl.ReaGoodsID = reqDtl.GoodsID;

            orderDtl.ReqGoodsQty = reqDtl.ReqGoodsQty;
            orderDtl.GoodsQty = reqDtl.GoodsQty;
            orderDtl.Price = goodsOrgLink.Price;
            orderDtl.SumTotal = orderDtl.GoodsQty * goodsOrgLink.Price;
            orderDtl.GoodsUnit = reaGood.UnitName;

            orderDtl.ReaGoodsName = reaGood.CName;
            orderDtl.UnitMemo = reaGood.UnitMemo;
            orderDtl.BarCodeType = goodsOrgLink.BarCodeType;
            orderDtl.IsPrintBarCode = goodsOrgLink.IsPrintBarCode;
            orderDtl.ReaGoodsNo = reaGood.ReaGoodsNo;

            orderDtl.ProdGoodsNo = reaGood.ProdGoodsNo;
            orderDtl.GoodsNo = reaGood.GoodsNo;
            orderDtl.CenOrgGoodsNo = goodsOrgLink.CenOrgGoodsNo;
            orderDtl.GoodsSort = reaGood.GoodsSort;
            orderDtl.ArrivalTime = reqDtl.ArrivalTime;

            orderDtl.ExpectedStock = reqDtl.ExpectedStock;
            orderDtl.MonthlyUsage = reqDtl.MonthlyUsage;
            orderDtl.LastMonthlyUsage = reqDtl.LastMonthlyUsage;
            orderDtl.ProdID = reqDtl.ProdID;
            orderDtl.ProdOrgName = reqDtl.ProdOrgName;

            orderDtl.LabOrderDtlID = orderDtl.Id;

            orderDtl.SuppliedQty = 0;
            orderDtl.UnSupplyQty = orderDtl.GoodsQty.Value;
            return orderDtl;
        }
        /// <summary>
        /// 添加申请操作记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        private void AddReaReqOperation(ReaBmsReqDoc entity, long empID, string empName)
        {
            if (entity.Status.ToString() == ReaBmsReqDocStatus.暂存.Key) return;

            ReaReqOperation sco = new ReaReqOperation();
            sco.BobjectID = entity.Id;
            sco.CreatorID = empID;
            if (empName != null && empName.Trim() != "")
                sco.CreatorName = empName;
            sco.BusinessModuleCode = "ReaBmsReqDoc";
            sco.Memo = "";
            sco.IsUse = true;
            sco.Type = entity.Status;
            sco.TypeName = ReaBmsReqDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
            IBReaReqOperation.Entity = sco;
            IBReaReqOperation.Add();
        }
        /// <summary>
        /// 获取申请总单号及订单总单号
        /// </summary>
        /// <returns></returns>
        private string GetReqDocNo()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));
            //Random ran = new Random();
            Random ran = new Random(Guid.NewGuid().GetHashCode());//使用GUID的随机3位，普通的在毫秒内会重现重复的随机数，导致订单号一样。
            int randKey = ran.Next(0, 999);
            strb.Append(randKey.ToString().PadLeft(3, '0'));//左补零
            strb.Append(DateTime.Now.ToString("HHmmssfff"));
            ZhiFang.Common.Log.Log.Info("动态生成DocNo=" + strb.ToString());
            return strb.ToString();
        }
        public BaseResultDataValue AddReaBmsReqDocAndDtOfCopyApply(long id, long deptID, string deptName, long empID, string empName)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();

            ReaBmsReqDoc serverEntity = this.Get(id);
            if (serverEntity == null)
            {
                tempBaseResultDataValue.ErrorInfo = "申请ID：" + id + "不存在！";
                return tempBaseResultDataValue;
            }
            IList<ReaBmsReqDtl> reqdtlList = IBReaBmsReqDtl.SearchListByHQL("reabmsreqdtl.ReaBmsReqDoc.Id=" + serverEntity.Id);
            if (reqdtlList == null || reqdtlList.Count <= 0)
            {
                tempBaseResultDataValue.ErrorInfo = "申请ID：" + id + "申请明细为空！";
                return tempBaseResultDataValue;
            }

            ReaBmsReqDoc copyEntity = new ReaBmsReqDoc();
            copyEntity.Visible = true;
            copyEntity.ReqDocNo = GetReqDocNo();
            copyEntity.ApplyID = empID;
            copyEntity.ApplyName = empName;
            copyEntity.ApplyTime = DateTime.Now;
            copyEntity.OperDate = DateTime.Now;
            copyEntity.DataUpdateTime = DateTime.Now;
            copyEntity.Status = int.Parse(ReaBmsReqDocStatus.暂存.Key);

            copyEntity.DeptID = deptID;
            copyEntity.DeptName = deptName;
            copyEntity.UrgentFlag = serverEntity.UrgentFlag;
            copyEntity.ZX1 = serverEntity.ZX1;
            copyEntity.ZX2 = serverEntity.ZX2;
            copyEntity.ZX3 = serverEntity.ZX3;

            this.Entity = copyEntity;
            tempBaseResultDataValue.success = this.Add();

            IList<ReaBmsReqDtl> dtAddList = new List<ReaBmsReqDtl>();
            foreach (var reqdtl in reqdtlList)
            {
                ReaBmsReqDtl addDtl = new ReaBmsReqDtl();
                addDtl.ReqDtlNo = GetReqDocNo();
                addDtl.Visible = true;
                addDtl.CreaterID = empID;
                addDtl.CreaterName = empName;
                addDtl.DataUpdateTime = DateTime.Now;

                addDtl.ReqDocNo = copyEntity.ReqDocNo;
                addDtl.ReaCenOrg = reqdtl.ReaCenOrg;
                addDtl.GoodsID = reqdtl.GoodsID;
                addDtl.GoodsCName = reqdtl.GoodsCName;
                addDtl.ReqGoodsQty = reqdtl.ReqGoodsQty;
                addDtl.GoodsQty = reqdtl.GoodsQty;
                addDtl.ZX1 = reqdtl.ZX1;
                addDtl.ZX2 = reqdtl.ZX2;
                addDtl.ZX3 = reqdtl.ZX3;

                addDtl.OrgName = reqdtl.OrgName;
                addDtl.Memo = reqdtl.Memo;
                addDtl.GoodsUnit = reqdtl.GoodsUnit;
                addDtl.CompGoodsLinkID = reqdtl.CompGoodsLinkID;
                addDtl.ReaGoodsNo = reqdtl.ReaGoodsNo;
                addDtl.CenOrgGoodsNo = reqdtl.CenOrgGoodsNo;
                addDtl.ReaBmsReqDoc = copyEntity;

                addDtl.UnitMemo = reqdtl.UnitMemo;
                addDtl.ProdID = reqdtl.ProdID;
                addDtl.ProdOrgName = reqdtl.ProdOrgName;
                addDtl.ArrivalTime = reqdtl.ArrivalTime;
                addDtl.ExpectedStock = reqdtl.ExpectedStock;
                addDtl.CurrentQty = reqdtl.CurrentQty;
                addDtl.MonthlyUsage = reqdtl.MonthlyUsage;
                //addDtl.LastMonthlyUsage = reqdtl.LastMonthlyUsage;
                addDtl.Price = reqdtl.Price;
                addDtl.SumTotal = reqdtl.SumTotal;

                dtAddList.Add(addDtl);
            }
            if (tempBaseResultDataValue.success)
            {
                if (dtAddList != null && dtAddList.Count > 0)
                {
                    BaseResultBool tempBaseResultBool = IBReaBmsReqDtl.AddDtList(dtAddList, this.Entity, empID, empName);
                    tempBaseResultDataValue.success = tempBaseResultBool.success;
                    tempBaseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                }
            }
            if (tempBaseResultDataValue.success) AddReaReqOperation(this.Entity, empID, empName);
            return tempBaseResultDataValue;
        }
        #region PDF清单及统计
        public Stream SearchReaBmsReqDocMergeReportOfPdfByIdStr(string reaReportClass, string idStr, long labID, string labCName, string breportType, string frx, ref string pdfFileName)
        {
            Stream stream = null;
            ReaBmsReqDoc reqDoc = new ReaBmsReqDoc();// this.Get(id);
            if (string.IsNullOrEmpty(idStr))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("传入的采购申请信息(idStr)为空!");
            }
            IList<ReaBmsReqDtl> dtlList = IBReaBmsReqDtl.SearchListByHQL("reabmsreqdtl.ReaBmsReqDoc.Id in (" + idStr + ")");
            //判断申请单是否都审核通过及供货商都已存在
            var tempList = dtlList.Where(p => p.ReaCenOrg == null);
            if (tempList.Count() > 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("存在供应商为空的采购申请信息,不能合并报表!");
            }

            dtlList = dtlList.GroupBy(p => new
            {
                p.ReaCenOrg.Id,
                p.ReaGoodsNo,
                p.GoodsUnit
            }).Select(g => new ReaBmsReqDtl
            {
                ReaCenOrg = g.ElementAt(0).ReaCenOrg,
                ReaCompanyName = g.ElementAt(0).ReaCenOrg.CName,
                ReaCompCode = g.ElementAt(0).ReaCenOrg.OrgNo.ToString(),
                ReaGoodsNo = g.ElementAt(0).ReaGoodsNo,
                GoodsCName = g.ElementAt(0).GoodsCName,
                GoodsUnit = g.ElementAt(0).GoodsUnit,
                GoodsUnitID = g.ElementAt(0).GoodsUnitID,
                UnitMemo = g.ElementAt(0).UnitMemo,
                ReqGoodsQty = g.Sum(k => k.ReqGoodsQty),
                GoodsQty = g.Sum(k => k.GoodsQty),
                SumTotal = g.Sum(k => k.SumTotal),
                Price = g.Sum(k => k.SumTotal) / g.Sum(k => k.GoodsQty),
                ZX1 = g.ElementAt(0).ZX1,
                ZX2 = g.ElementAt(0).ZX2,
                ZX3 = g.ElementAt(0).ZX3,
                Memo = g.ElementAt(0).Memo,
                DispOrder = g.ElementAt(0).DispOrder,
                Visible = g.ElementAt(0).Visible,
                CreaterID = g.ElementAt(0).CreaterID,
                CreaterName = g.ElementAt(0).CreaterName,
                DataUpdateTime = g.ElementAt(0).DataUpdateTime,
                CenOrgGoodsNo = g.ElementAt(0).CenOrgGoodsNo
            }).OrderBy(p => p.ReaCompanyName).ThenBy(p => p.ReaGoodsNo).ThenBy(p => p.GoodsUnit).ToList();

            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取采购申请PDF清单明细信息为空!");
            }
            pdfFileName = "采购申请合并报表.pdf";//GetReqDocNo() + ".pdf";//
            if (string.IsNullOrEmpty(frx))
                frx = "按供应商合并双列.frx";
            if (reaReportClass == ReaReportClass.Frx.Key)
            {
                stream = CreatePdfReportOfFrxById(reqDoc, dtlList, frx);
            }

            return stream;
        }
        public Stream SearchPdfReportOfTypeById(string reaReportClass, long id, long labID, string labCName, string breportType, string frx, ref string pdfFileName)
        {
            Stream stream = null;
            ReaBmsReqDoc reqDoc = this.Get(id);
            if (reqDoc == null)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取采购申请PDF清单数据信息为空!");
            }
            //IList<ReaBmsReqDtl> dtlList = IBReaBmsReqDtl.SearchListByHQL("reabmsreqdtl.ReaBmsReqDoc.Id=" + id, "DispOrder ASC", -1, -1).list;
            IList<ReaBmsReqDtl> dtlList = IBReaBmsReqDtl.GetReaBmsReqDtlListByHQL("reabmsreqdoc.Id=" + id, "reabmsreqdtl.DispOrder ASC", -1, -1).list;
            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取采购申请PDF清单明细信息为空!");
            }
            pdfFileName = reqDoc.DeptName + ".pdf";

            if (reaReportClass == ReaReportClass.Frx.Key)
            {
                stream = CreatePdfReportOfFrxById(reqDoc, dtlList, frx);
            }
            else if (reaReportClass == ReaReportClass.Excel.Key)
            {
                string excelFileFullDir = "";
                //获取采购申请单
                if (string.IsNullOrEmpty(frx))
                    frx = "采购申请单.xlsx";
                string fileExt = frx.Substring(frx.LastIndexOf("."));
                string excelFile = reqDoc.ReqDocNo.ToString() + fileExt;
                ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
                Stream stream2 = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsReqDoc, ReaBmsReqDtl>(reqDoc, dtlList, excelCommand, breportType, reqDoc.LabID, frx, excelFile, ref excelFileFullDir);
                stream2.Close();
                string pdfFullDir = "";
                bool result = ExcelToPdfReportHelp.ExcelToPDF(excelFileFullDir, breportType, reqDoc.LabID, pdfFileName, ref pdfFullDir);
                if (result)
                {
                    stream = PdfReportHelp.GetReportPDF(pdfFullDir);
                }
            }
            return stream;
        }
        private Stream CreatePdfReportOfFrxById(ReaBmsReqDoc reqDoc, IList<ReaBmsReqDtl> dtlList, string frx)
        {
            Stream stream = null;
            DataSet dataSet = new DataSet();
            dataSet.DataSetName = "ZhiFang.ReagentSys.Client";

            List<ReaBmsReqDoc> docList = new List<ReaBmsReqDoc>();
            docList.Add(reqDoc);
            DataTable docDt = ReportBTemplateHelp.ToDataTable<ReaBmsReqDoc>(docList, null);
            docDt.TableName = "Rea_BmsReqDoc";
            dataSet.Tables.Add(docDt);

            DataTable dtDtl = ReportBTemplateHelp.ToDataTable<ReaBmsReqDtl>(dtlList, null);
            if (dtDtl != null)
            {
                dtDtl.TableName = "Rea_BmsReqDtl";
                dataSet.Tables.Add(dtDtl);
            }
            //获取货品耗材采购申请模板
            string fileName = reqDoc.ReqDocNo;
            if (string.IsNullOrEmpty(fileName))
                fileName = this.GetReqDocNo();
            string pdfName = fileName + ".pdf";
            //如果当前实验室还没有维护采购申请清单报表模板,默认使用公共的采购申请清单模板
            if (string.IsNullOrEmpty(frx))
                frx = "采购申请单.frx";
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            ExcelReportHelp.CreateEECDataTable(excelCommand, ref dataSet);
            stream = FrxToPdfReportHelp.SavePdfReport(dataSet, reqDoc.LabID, pdfName, FrxToPdfReportHelp.PublicTemplateDir, BTemplateType.GetStatusDic()[BTemplateType.采购申请.Key].Name, frx, false);

            return stream;
        }
        public Stream SearchExcelReportOfExcelById(long id, long labID, string labCName, string breportType, string frx, ref string fileName)
        {
            Stream stream = null;
            ReaBmsReqDoc reqDoc = this.Get(id);
            if (reqDoc == null)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取货品耗材采购申请Excel清单数据信息为空!");
            }
            //IList<ReaBmsReqDtl> dtlList = IBReaBmsReqDtl.SearchListByHQL("reabmsreqdtl.ReaBmsReqDoc.Id=" + id, "DispOrder ASC", -1, -1).list;
            IList<ReaBmsReqDtl> dtlList = IBReaBmsReqDtl.GetReaBmsReqDtlListByHQL("reabmsreqdoc.Id=" + id, "reabmsreqdtl.DispOrder ASC", -1, -1).list;
            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取货品耗材采购申请Excel清单明细信息为空!");
            }

            fileName = reqDoc.DeptName + "采购申请信息.xlsx";
            //获取订货单模板
            if (string.IsNullOrEmpty(frx))
                frx = "采购申请单.xlsx";
            string saveFullPath = "";
            string fileExt = frx.Substring(frx.LastIndexOf("."));
            string excelFile = reqDoc.ReqDocNo.ToString() + fileExt;
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsReqDoc, ReaBmsReqDtl>(reqDoc, dtlList, excelCommand, breportType, reqDoc.LabID, frx, excelFile, ref saveFullPath);

            return stream;
        }
        #endregion
    }
}