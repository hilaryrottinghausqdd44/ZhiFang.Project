
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.BLL;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO.ReagentSys;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsReqDoc : BaseBLL<ReaBmsReqDoc>, ZhiFang.Digitlab.IBLL.ReagentSys.IBReaBmsReqDoc
    {

        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IDReaGoodsOrgLinkDao IDReaGoodsOrgLinkDao { get; set; }
        IBLL.ReagentSys.IBReaBmsReqDtl IBReaBmsReqDtl { get; set; }
        IBLL.ReagentSys.IBBmsCenOrderDoc IBBmsCenOrderDoc { get; set; }
        IBLL.ReagentSys.IBReaReqOperation IBReaReqOperation { get; set; }
        /// <summary>
        /// 部门采购申请新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtAddList">新增的明细集合</param>
        /// <returns></returns>
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
        /// <summary>
        /// 部门采购申请更新
        /// </summary>
        /// <param name="entity">待更新的主单信息</param>
        /// <param name="dtAddList">新增的明细集合</param>
        /// <param name="dtEditList">待更新的明细集合</param>
        /// <returns></returns>
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
        /// <summary>
        /// 部门采购申请明细更新(验证主单后只操作明细)
        /// </summary>
        /// <param name="entity">主单信息</param>
        /// <param name="dtAddList">新增的明细集合</param>
        /// <param name="dtEditList">待更新的明细集合</param>
        /// <returns></returns>
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
        /// <summary>
        /// 部门采购申请审核及撤消审核
        /// </summary>
        /// <param name="entity">待审核的主单信息</param>
        /// <param name="dtAddList">审核前的新增的明细集合</param>
        /// <param name="dtEditList">审核前的待更新的明细集合</param>
        /// <returns></returns>
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
                if (serverEntity.Status.ToString() != ReaBmsReqDocStatus.已申请.Key)
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

        /// <summary>
        /// 依据客户端的申请主单(已审核)生成客户端订单信息
        /// 各订单生成是根据申请货品明细的供应商合并(一个供应商对应生成一个订单)
        /// </summary>
        /// <param name="idStr"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        public BaseResultBool AddReaCenOrgBmsCenOrderDocOfReaBmsReqDocIDStr(string idStr, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(idStr))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的申请主单参数(idStr)为空";
                return tempBaseResultBool;
            }
            IList<ReaBmsReqDtl> reabmsreqdtlList = IBReaBmsReqDtl.SearchListByHQL("reabmsreqdtl.ReaBmsReqDoc.Status=" + ReaBmsReqDocStatus.审核通过.Key + "and reabmsreqdtl.ReaBmsReqDoc.Id in(" + idStr + ")");
            if (reabmsreqdtlList == null || reabmsreqdtlList.Count == 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "获取符合生成订单的申请单数据为空!";
                return tempBaseResultBool;
            }

            //临时的货品明细(防止同一货品重复查询数据库)
            Dictionary<long, ReaGoods> reaGoodsTempList = new Dictionary<long, ReaGoods>();
            //临时供应商与货品关系(防止同一货品重复查询数据库)
            Dictionary<long, ReaGoodsOrgLink> goodsOrgLinkTempList = new Dictionary<long, ReaGoodsOrgLink>();
            //临时的申请明细ID与订单明细ID(同一订单保存成功后回填相关的申请明细的订单明细ID)
            Dictionary<ReaBmsReqDtl, long> dtlIdList = new Dictionary<ReaBmsReqDtl, long>();

            #region 生成订单处理
            //按货品明细的所属供应商进行分组
            var reaCenOrgGroup = reabmsreqdtlList.GroupBy(p => p.ReaCenOrg);
            //某一订单的订单明细
            Dictionary<string, BmsCenOrderDtl> orderDtlList = new Dictionary<string, BmsCenOrderDtl>();
            foreach (var reaCenOrg in reaCenOrgGroup)
            {
                dtlIdList.Clear();
                orderDtlList.Clear();

                BmsCenOrderDoc orderDoc = GetBmsCenOrderDoc(empID, empName, reaCenOrg);

                #region 某一订单的订单明细处理               
                //申请明细按货品进行排序
                var tempDtList2 = reaCenOrg.OrderBy(p => p.GoodsID);
                for (int i = 0; i < tempDtList2.Count(); i++)
                {
                    var item = tempDtList2.ElementAt(i);
                    //货品当前库存数量计算

                    //货品的基本信息
                    ReaGoods reaGood = null;
                    //获取货品的单价格
                    ReaGoodsOrgLink goodsOrgLink = null;
                    #region 获取货品信息处理
                    if (!reaGoodsTempList.ContainsKey(item.GoodsID.Value))
                    {
                        reaGood = IDReaGoodsDao.Get(item.GoodsID.Value);
                        reaGoodsTempList.Add(item.GoodsID.Value, reaGood);
                    }
                    else
                    {
                        reaGood = reaGoodsTempList[item.GoodsID.Value];
                    }

                    if (!goodsOrgLinkTempList.ContainsKey(item.GoodsID.Value))
                    {
                        goodsOrgLink = IDReaGoodsOrgLinkDao.Get(item.OrderGoodsID.Value);
                        goodsOrgLinkTempList.Add(item.GoodsID.Value, goodsOrgLink);
                    }
                    else
                    {
                        goodsOrgLink = goodsOrgLinkTempList[item.GoodsID.Value];
                    }
                    #endregion
                    #region 同一订单的订单明细处理
                    //申请明细的货品是否合并为同一订单明细记录条件:货品Id+";"+单位+";"+规格
                    string key = string.Format("{0};{1};{2}", reaGood.Id, reaGood.UnitName, reaGood.UnitMemo);
                    if (!orderDtlList.Keys.Contains(key))
                    {
                        BmsCenOrderDtl dtl = GetBmsCenOrderDtl(orderDoc, item, reaGood, goodsOrgLink);
                        orderDtlList.Add(key, dtl);
                    }
                    else
                    {
                        orderDtlList[key].GoodsQty += (int)item.GoodsQty;
                        orderDtlList[key].Price = goodsOrgLink.Price;
                        orderDtlList[key].SumTotal = orderDtlList[key].GoodsQty * goodsOrgLink.Price;
                    }
                    #endregion
                    if (!dtlIdList.ContainsKey(item)) dtlIdList.Add(item, orderDtlList[key].Id);
                    //主订单总价计算
                    orderDoc.TotalPrice += orderDtlList[key].SumTotal;
                }
                #endregion
                //保存订单及订单明细
                tempBaseResultBool = IBBmsCenOrderDoc.AddReaBmsCenOrderDocAndDtl(orderDoc, orderDtlList, empID, empName);
            }
            #endregion

            if (tempBaseResultBool.success)
            {
                //更新申请单为已生成订单的状态
                tempBaseResultBool.success = ((IDReaBmsReqDocDao)base.DBDao).UpdateStatusByIdStr(idStr, int.Parse(ReaBmsReqDocStatus.转为订单.Key));
                //回填相关的申请明细的订单明细ID
                if (tempBaseResultBool.success) tempBaseResultBool = AddReaCenOrgBmsCenOrderDocOfEditReqDtl(dtlIdList);
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
        /// 主订单生成
        /// </summary>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <param name="reaCenOrg"></param>
        /// <returns></returns>
        private BmsCenOrderDoc GetBmsCenOrderDoc(long empID, string empName, IGrouping<ReaCenOrg, ReaBmsReqDtl> reaCenOrg)
        {
            BmsCenOrderDoc orderDoc = new BmsCenOrderDoc();
            orderDoc.OrderDocNo = this.GetReqDocNo();
            orderDoc.UserID = empID;
            orderDoc.UserName = empName;
            orderDoc.UrgentFlag = reaCenOrg.ElementAt(0).ReaBmsReqDoc.UrgentFlag;
            orderDoc.Status = int.Parse(ReaBmsOrderDocStatus.申请.Key);
            //orderDoc.StatusName = ReaBmsOrderDocStatus.已提交.ToString();
            orderDoc.OperDate = DateTime.Now;
            orderDoc.IOFlag = 0;
            orderDoc.ReaCompID = reaCenOrg.ElementAt(0).ReaCenOrg.Id;
            orderDoc.ReaCompName = reaCenOrg.ElementAt(0).ReaCenOrg.CName;
            orderDoc.ReaServerCompCode = reaCenOrg.ElementAt(0).ReaCenOrg.PlatformOrgNo.ToString();
            //主订单平台供应商处理(暂时不处理)
            return orderDoc;
        }

        /// <summary>
        /// 同一订单保存成功后回填相关的申请明细的订单明细ID
        /// </summary>
        /// <param name="orderDtlIDList"></param>
        /// <returns></returns>
        private BaseResultBool AddReaCenOrgBmsCenOrderDocOfEditReqDtl(Dictionary<ReaBmsReqDtl, long> dtlIdList)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            List<string> tmpa = new List<string>();
            foreach (var item in dtlIdList)
            {
                //tmpa.Clear();
                //tmpa.Add("Id=" + item.Key + " ");
                //tmpa.Add("OrderDtlID=" + item.Value + " ");
                //tempBaseResultBool.success = IBReaBmsReqDtl.Update(tmpa.ToArray());
                ReaBmsReqDtl entity = item.Key;
                entity.OrderFlag = 1;//生成订单标志
                entity.OrderDtlID = item.Value;
                IBReaBmsReqDtl.Entity = entity;
                IBReaBmsReqDtl.Edit();
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 获取新增的订单明细
        /// </summary>
        /// <param name="orderDoc"></param>
        /// <param name="item"></param>
        /// <param name="reaGood"></param>
        /// <param name="goodsOrgLink"></param>
        /// <returns></returns>
        private BmsCenOrderDtl GetBmsCenOrderDtl(BmsCenOrderDoc orderDoc, ReaBmsReqDtl item, ReaGoods reaGood, ReaGoodsOrgLink goodsOrgLink)
        {
            BmsCenOrderDtl dtl = new BmsCenOrderDtl();
            dtl.ProdGoodsNo = reaGood.ProdGoodsNo;
            
            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            dtl.BmsCenOrderDoc = orderDoc;
            dtl.BmsCenOrderDoc.DataTimeStamp = dataTimeStamp;
            dtl.OrderGoodsID = item.OrderGoodsID;
            dtl.OrderDocNo = orderDoc.OrderDocNo;
            dtl.ReaGoodsID = item.GoodsID;
            dtl.GoodsQty = (int)item.GoodsQty;

            dtl.Price = goodsOrgLink.Price;
            dtl.SumTotal = dtl.GoodsQty * goodsOrgLink.Price;
            //货品平台编码处理
            if (!string.IsNullOrEmpty(goodsOrgLink.CenOrgGoodsNo))
                dtl.GoodsNo = goodsOrgLink.CenOrgGoodsNo;
            else
                dtl.GoodsNo = reaGood.GoodsNo;
            dtl.GoodsUnit = reaGood.UnitName;
            dtl.ReaGoodsName = reaGood.CName;
            dtl.UnitMemo = reaGood.UnitMemo;
            return dtl;
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
            Random ran = new Random();
            int randKey = ran.Next(0, 999);
            strb.Append(randKey.ToString().PadLeft(3, '0'));//左补零
            strb.Append(DateTime.Now.ToString("HHmmssfff"));
            return strb.ToString();
        }
    }
}