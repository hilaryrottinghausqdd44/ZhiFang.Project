
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.Base;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using System.Collections;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaStoreIn;
using ZhiFang.ServiceCommon.RBAC;
using System.Data;
using ZhiFang.ReagentSys.Client.Common;
using System.IO;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaBmsCheckDoc : BaseBLL<ReaBmsCheckDoc>, IBLL.ReagentSys.Client.IBReaBmsCheckDoc
    {
        IBReaBmsCheckDtl IBReaBmsCheckDtl { get; set; }
        IDReaBmsQtyDtlDao IDReaBmsQtyDtlDao { get; set; }
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        IBReaBmsInDoc IBReaBmsInDoc { get; set; }
        IBReaBmsInDtl IBReaBmsInDtl { get; set; }
        IBReaBmsOutDoc IBReaBmsOutDoc { get; set; }
        IBReaBmsOutDtl IBReaBmsOutDtl { get; set; }
        IDReaEquipReagentLinkDao IDReaEquipReagentLinkDao { get; set; }
        IDReaTestEquipLabDao IDReaTestEquipLabDao { get; set; }

        public BaseResultDataValue AddReaBmsCheckDoc(int mergeType, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            BaseResultBool tempBaseResultBool = ((IDReaBmsCheckDocDao)base.DBDao).EditValidIsLock(this.Entity, "");
            baseResultDataValue.success = tempBaseResultBool.success;
            baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
            if (baseResultDataValue.success == false) return baseResultDataValue;

            this.Entity.IsLock = int.Parse(ReaBmsCheckDocLock.已锁定.Key);
            this.Entity.Visible = true;
            this.Entity.CreaterID = empID;
            this.Entity.CreaterName = empName;
            this.Entity.CheckerID = empID;
            this.Entity.CheckerName = empName;
            this.Entity.CheckDateTime = DateTime.Now;
            this.Entity.DataUpdateTime = DateTime.Now;
            this.Entity.IsException = 0;
            this.Entity.IsHandleException = 0;
            this.Entity.Status = int.Parse(ReaBmsCheckDocStatus.盘库锁定.Key);
            this.Entity.StatusName = ReaBmsCheckDocStatus.GetStatusDic()[this.Entity.Status.ToString()].Name;
            this.Entity.CheckDocNo = this.GetCheckDocNo();
            this.Entity.BmsCheckResult = int.Parse(ReaBmsCheckResult.未盘盈及未盘亏.Key);

            IList<ReaBmsCheckDtl> dtAddList = new List<ReaBmsCheckDtl>();
            //当前盘库条件说明
            StringBuilder checkCondition = new StringBuilder();
            dtAddList = GetAddCheckDtlList(this.Entity, "", "", 0, empID, empName, ref checkCondition);

            if (dtAddList.Count <= 0)
            {
                baseResultDataValue.success = false;
                if (checkCondition.ToString().Length <= 0)
                    checkCondition.Append("按全部");
                baseResultDataValue.ErrorInfo = string.Format("获取盘库条件为:{0},的有效库存数据信息为空!", checkCondition.ToString());
                return baseResultDataValue;
            }

            if (this.Add())
            {
                tempBaseResultBool = IBReaBmsCheckDtl.AddCheckDtlOfList(this.Entity, dtAddList, empID, empName, false);
                baseResultDataValue.success = tempBaseResultBool.success;
                baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：保存盘库主单信息失败！";
            }

            return baseResultDataValue;
        }
        public ReaBmsCheckDtl GetReaBmsCheckDtl(ReaBmsCheckDoc checkDoc, ReaBmsQtyDtl qty, long empID, string empName)
        {
            ReaBmsCheckDtl dtl = new ReaBmsCheckDtl();
            dtl.CheckDocID = checkDoc.Id;
            dtl.ReaCompanyID = qty.ReaCompanyID;
            dtl.ReaServerCompCode = qty.ReaServerCompCode;
            dtl.ReaCompCode = qty.ReaCompCode;
            dtl.CompanyName = qty.CompanyName;

            dtl.CheckQty = null;
            dtl.StorageID = qty.StorageID;
            dtl.PlaceID = qty.PlaceID;
            dtl.StorageName = qty.StorageName;
            dtl.PlaceName = qty.PlaceName;

            dtl.GoodsID = qty.GoodsID;
            dtl.GoodsName = qty.GoodsName;
            dtl.GoodsLotID = qty.GoodsLotID;
            dtl.LotNo = qty.LotNo;

            dtl.GoodsUnit = qty.GoodsUnit;
            dtl.UnitMemo = qty.UnitMemo;
            dtl.BarCodeType = qty.BarCodeType;
            dtl.DataUpdateTime = DateTime.Now;

            dtl.ProdDate = qty.ProdDate;
            dtl.InvalidDate = qty.InvalidDate;
            dtl.LotSerial = qty.LotSerial;
            dtl.LotQRCode = qty.LotQRCode;
            dtl.SysLotSerial = qty.SysLotSerial;

            dtl.CompGoodsLinkID = qty.CompGoodsLinkID;
            dtl.IsException = 1;
            dtl.IsHandleException = 0;
            dtl.ReaGoodsNo = qty.ReaGoodsNo;
            dtl.ProdGoodsNo = qty.ProdGoodsNo;

            dtl.CenOrgGoodsNo = qty.CenOrgGoodsNo;
            dtl.GoodsNo = qty.GoodsNo;
            dtl.GoodsSort = qty.GoodsSort;
            dtl.ZX1 = qty.ZX1;
            dtl.ZX2 = qty.ZX2;
            dtl.ZX3 = qty.ZX3;
            dtl.Visible = true;
            dtl.DispOrder = qty.DispOrder;

            dtl.GoodsSName = qty.SName;
            dtl.ProdOrgName = qty.ProdOrgName;

            return dtl;
        }
        public BaseResultBool DelReaBmsCheckDoc(long id)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            ReaBmsCheckDoc entity = this.Get(id);
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "盘库单ID：" + id + "不存在数据库中！";
                return tempBaseResultBool;
            }
            if (entity.Status.ToString() != ReaBmsCheckDocStatus.盘库锁定.Key)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "盘库单ID：" + entity.Id + "的状态为：" + ReaBmsCheckDocStatus.GetStatusDic()[entity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }
            IList<ReaBmsCheckDtl> delDtlList = IBReaBmsCheckDtl.SearchListByHQL("reabmscheckdtl.CheckDocID=" + entity.Id);
            foreach (var dtl in delDtlList)
            {
                IBReaBmsCheckDtl.Entity = dtl;
                tempBaseResultBool.success = IBReaBmsCheckDtl.Remove();
            }
            this.Entity = entity;
            tempBaseResultBool.success = this.Remove();

            return tempBaseResultBool;
        }
        public BaseResultBool EditReaBmsCheckDocAndDtl(string[] tempArray, IList<ReaBmsCheckDtl> dtEditList, string fieldsDtl, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (this.Entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "entity为空,不能操作！";
                return tempBaseResultBool;
            }
            List<string> tmpa = tempArray.ToList();
            ReaBmsCheckDoc serverEntity = this.Get(this.Entity.Id);
            if (serverEntity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "盘库单ID：" + this.Entity.Id + "不存在数据库中！";
                return tempBaseResultBool;
            }
            if (this.Entity.Status.ToString() == ReaBmsCheckDocStatus.确认盘库.Key)
            {
                //验证实盘数是否都不为空
                var tempList = dtEditList.Where(p => p.CheckQty < 0 || p.CheckQty == null);
                if (tempList.Count() > 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "盘库单ID：" + this.Entity.Id + ",存在实盘数为0或为空的盘库明细！";
                    return tempBaseResultBool;
                }
            }
            if (!EditReaBmsCheckDocStatusCheck(this.Entity, serverEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "盘库单ID：" + this.Entity.Id + "的状态为：" + ReaBmsCheckDocStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }
            //是否异常,1:是;0:否
            int isException = 0;
            //异常是否处理,1:是;0:否
            int isHandleException = 1;
            if (dtEditList.Where(p => p.IsException == 1).Count() > 0)
                isException = 1;
            isHandleException = (isException == 1 ? 0 : 1);
            tmpa.Add("IsException=" + isException + " ");
            tmpa.Add("IsHandleException=" + isHandleException + " ");
            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                tempBaseResultBool = IBReaBmsCheckDtl.EditCheckDtlOfList(this.Entity, dtEditList, fieldsDtl, empID, empName);
            }
            else
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "盘库单ID：" + this.Entity.Id + "更新失败！";
                return tempBaseResultBool;
            }
            return tempBaseResultBool;
        }
        bool EditReaBmsCheckDocStatusCheck(ReaBmsCheckDoc entity, ReaBmsCheckDoc serverEntity, List<string> tmpa, long empID, string empName)
        {
            if (entity.Status.ToString() == ReaBmsCheckDocStatus.盘库锁定.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsCheckDocStatus.盘库锁定.Key)
                {
                    return false;
                }

                tmpa.Add("CheckerID=" + empID + " ");
                tmpa.Add("CheckerName='" + empName + "'");
                tmpa.Add("CheckDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            }
            if (entity.Status.ToString() == ReaBmsCheckDocStatus.确认盘库.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsCheckDocStatus.盘库锁定.Key)
                {
                    return false;
                }
                tmpa.Add("ExaminerID=" + empID + " ");
                tmpa.Add("ExaminerName='" + empName + "'");
                tmpa.Add("ExaminerDateTime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
                tmpa.Add("IsLock=" + int.Parse(ReaBmsCheckDocLock.已解锁.Key));
            }
            if (entity.Status.ToString() == ReaBmsCheckDocStatus.差异调整中.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsCheckDocStatus.确认盘库.Key && serverEntity.Status.ToString() != ReaBmsCheckDocStatus.差异调整中.Key)
                {
                    return false;
                }
            }
            if (entity.Status.ToString() == ReaBmsCheckDocStatus.差异调整完成.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsCheckDocStatus.确认盘库.Key && serverEntity.Status.ToString() != ReaBmsCheckDocStatus.差异调整中.Key)
                {
                    return false;
                }
            }
            return true;
        }
        public IList<ReaBmsCheckDtl> GetAddCheckDtlList(ReaBmsCheckDoc checkDoc, string qtyHql0, string reaGoodHql, int zeroQtyDays, long empID, string empName, ref StringBuilder checkCondition)
        {
            IList<ReaBmsCheckDtl> dtAddList = new List<ReaBmsCheckDtl>();
            //库存货品条件
            StringBuilder qtyHql = new StringBuilder();
            qtyHql.Append("reabmsqtydtl.Visible=1");
            //是否包括库存数为零的库存货品
            if (checkDoc.IsHasZeroQty == true)
            {
                qtyHql.Append(" and reabmsqtydtl.GoodsQty>=0 ");
                //入库时间范围作为盘库数据过滤条件：包含最近___天内库存数为0的货品（生效的前提是盘库条件的“包括库存数为0的试剂勾选上”）;
                if (zeroQtyDays > 0)
                {
                    checkCondition.Append("盘库时包括库存数为零的库存货品且包含最近入库库存时间为：" + zeroQtyDays + "天内库存数为0的货品");
                }
                else
                {
                    checkCondition.Append("盘库时包括库存数为零的库存货品");
                }
            }
            else
            {
                qtyHql.Append(" and reabmsqtydtl.GoodsQty>0 ");
                checkCondition.Append("盘库时不包括库存数为零的库存货品");
            }

            if (!string.IsNullOrEmpty(qtyHql0))
                qtyHql.Append(" and " + qtyHql0);

            if (checkDoc.ReaCompanyID.HasValue)
            {
                qtyHql.Append(" and reabmsqtydtl.ReaCompanyID=" + checkDoc.ReaCompanyID.Value);
                checkCondition.Append("供应商为:" + checkDoc.CompanyName);
            }
            if (checkDoc.StorageID.HasValue)
            {
                qtyHql.Append(" and reabmsqtydtl.StorageID=" + checkDoc.StorageID.Value);
                checkCondition.Append("库房为:" + checkDoc.CompanyName);
            }
            if (checkDoc.PlaceID.HasValue)
            {
                qtyHql.Append("and reabmsqtydtl.PlaceID=" + checkDoc.PlaceID.Value);
                checkCondition.Append("货架为:" + checkDoc.PlaceName);
            }
            //过滤库存数量为0，且不是这个库房的库存货品
            if (checkDoc.IsStorageGoodsLink == true)
            {
                qtyHql.Append("and reabmsqtydtl.QtyDtlMark!=" + ReaBmsQtyDtlMark.过滤库存数量为0且不是这个库房的货品.Key);
                checkCondition.Append("过滤库存数量为0，且不是这个库房:");
            }

            //盘库的机构货品条件
            StringBuilder reaGoodsHql = new StringBuilder();
            if (!string.IsNullOrEmpty(checkDoc.GoodsClass))
            {
                reaGoodsHql.Append(" reagoods.GoodsClass='" + checkDoc.GoodsClass + "' and ");
                checkCondition.Append("机构货品一级分类为:" + checkDoc.GoodsClass + ",");
            }
            if (!string.IsNullOrEmpty(checkDoc.GoodsClassType))
            {
                reaGoodsHql.Append(" reagoods.GoodsClassType='" + checkDoc.GoodsClassType + "' and ");
                checkCondition.Append("机构货品二级分类为:" + checkDoc.GoodsClassType + ",");
            }
            if (!string.IsNullOrEmpty(reaGoodHql))
            {
                reaGoodsHql.Append(reaGoodHql + " and ");
                checkCondition.Append("传入的机构货品条件:" + reaGoodHql + ",");
            }
            //获取当前选择盘库条件的库存数据
            IList<ReaBmsQtyDtl> qtyList = new List<ReaBmsQtyDtl>();
            if (reaGoodsHql.Length > 0)
            {
                char[] trimChars = new char[] { ' ', 'a', 'n', 'd' };
                qtyList = IDReaBmsQtyDtlDao.SearchReaBmsQtyDtlListByHql(qtyHql.ToString(), "", reaGoodsHql.ToString().TrimEnd(trimChars), "", -1, -1);
            }
            else
            {
                qtyList = IDReaBmsQtyDtlDao.GetListByHQL(qtyHql.ToString());
            }
            ZhiFang.Common.Log.Log.Error("(未按盘库条件分组合并)符合盘库条件的库存货品记录数为:" + qtyList.Count());
            //是否包括库存数为零的库存货品
            if (checkDoc.IsHasZeroQty == true&& zeroQtyDays > 0)
            {
                //入库时间范围作为盘库数据过滤条件：包含最近___天内库存数为0的货品（生效的前提是盘库条件的“包括库存数为0的试剂勾选上”）;
                string sdateStr = DateTime.Now.AddDays(-zeroQtyDays).ToString("yyyy-MM-dd 00:00:00");
                DateTime sdate = DateTime.Parse(sdateStr);
                var qtyList2 = qtyList.Where(p => (p.GoodsQty.Value <= 0|| p.GoodsQty.HasValue == false) && p.DataAddTime.Value>=sdate).ToList();
                ZhiFang.Common.Log.Log.Error("(未按盘库条件分组合并)是否包括库存数为零的库存货品为:是,按大于等于入库库存时间:" + sdateStr + "开始过滤库存货品，符合大于等于最近" + zeroQtyDays + "天内库存数为0的库存货品记录数为:" + qtyList2.Count);

                var qtyList3 = qtyList.Where(p => (p.GoodsQty.Value <= 0 || p.GoodsQty.HasValue == false) && p.DataAddTime.Value < sdate).ToList();
                ZhiFang.Common.Log.Log.Error("(未按盘库条件分组合并)是否包括库存数为零的库存货品为:是,按小于入库库存时间:" + sdateStr + "开始过滤库存货品，符合小于最近" + zeroQtyDays + "天内库存数为0的库存货品记录数为:" + qtyList3.Count);
                if (qtyList3.Count > 0)
                {
                    for (int i = 0; i < qtyList3.Count; i++)
                    {
                        qtyList.Remove(qtyList3[i]);
                    }
                }
                ZhiFang.Common.Log.Log.Error("(未按盘库条件分组合并)二次过滤处理后,符合盘库条件的库存货品记录数为:" + qtyList.Count());
            }
            
            //盘库的库存数据合并是否区分供应商
            if (checkDoc.IsCompFlag == true)
            {
                //盘库的库存数据合并方式:按供货方ID+货品ID+批号+库房ID+货架Id分组
                var groupBy = qtyList.GroupBy(p => new { p.ReaCompanyID, p.GoodsID, p.LotNo, p.StorageID, p.PlaceID });
                foreach (var item in groupBy)
                {
                    ReaBmsCheckDtl checkDtl = GetReaBmsCheckDtl(checkDoc, item.ElementAt(0), empID, empName);
                    checkDtl.Price = item.OrderByDescending(p => p.DataAddTime).ElementAt(0).Price;
                    if (!checkDtl.Price.HasValue)
                        checkDtl.Price = 0;
                    checkDtl.GoodsQty = item.Sum(p => p.GoodsQty);
                    checkDtl.SumTotal = item.Sum(p => p.SumTotal);
                    if (checkDtl.GoodsQty.HasValue && checkDtl.GoodsQty.Value > 0)
                        checkDtl.Price = checkDtl.SumTotal / checkDtl.GoodsQty;
                    dtAddList.Add(checkDtl);
                }
            }
            else
            {
                //盘库的库存数据合并方式:按货品ID+批号+库房ID+货架Id分组
                var groupBy = qtyList.GroupBy(p => new { p.GoodsID, p.LotNo, p.StorageID, p.PlaceID });
                foreach (var item in groupBy)
                {
                    ReaBmsCheckDtl checkDtl = GetReaBmsCheckDtl(checkDoc, item.ElementAt(0), empID, empName);
                    checkDtl.Price = item.OrderByDescending(p => p.DataAddTime).ElementAt(0).Price;
                    //if (!checkDtl.Price.HasValue)
                    //    checkDtl.Price = 0;
                    checkDtl.GoodsQty = item.Sum(p => p.GoodsQty);
                    checkDtl.SumTotal = item.Sum(p => p.SumTotal);
                    if (checkDtl.GoodsQty.HasValue && checkDtl.GoodsQty.Value > 0)
                        checkDtl.Price = checkDtl.SumTotal / checkDtl.GoodsQty;
                    dtAddList.Add(checkDtl);
                }
            }
            dtAddList = dtAddList.OrderBy(p => p.StorageID).ThenBy(p => p.PlaceID).ThenBy(p => p.ReaGoodsNo).ThenBy(p => p.DispOrder).ThenBy(p => p.LotNo).ToList();
            return dtAddList;
        }
        public EntityList<ReaBmsCheckDtl> SearchAddReaBmsCheckDtlByHQL(ReaBmsCheckDoc checkDoc, string reaGoodHql, int days, int zeroQtyDays, string sort, int page, int limit, long empID, string empName, ref BaseResultDataValue baseResultDataValue)
        {
            EntityList<ReaBmsCheckDtl> entityList = new EntityList<ReaBmsCheckDtl>();
            //是否被盘库锁定
            //BaseResultBool tempBaseResultBool = ((IDReaBmsCheckDocDao)base.DBDao).EditValidIsLock(checkDoc, reaGoodHql);
            //baseResultDataValue.success = tempBaseResultBool.success;
            //baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
            //if (baseResultDataValue.success == false) return entityList;

            IList<ReaBmsCheckDtl> checkDtlList = new List<ReaBmsCheckDtl>();
            string qtyHql = "";
            //相同盘库条件已经作过盘库的货品信息
            StringBuilder checkDtlHql = new StringBuilder();
            checkDtlList = SearchReaBmsCheckDtlByJoinHQL(checkDoc, reaGoodHql, days);
            foreach (var checkDtl in checkDtlList)
            {
                string tempGoodsID = checkDtl.GoodsID + ",";
                if (!checkDtlHql.ToString().Contains(tempGoodsID))
                    checkDtlHql.Append(tempGoodsID);
            }
            if (checkDtlHql.Length > 0 && days > 0)//增加days>0才过滤的判断条件 by douss 2021-09-01
            {
                qtyHql = " reabmsqtydtl.GoodsID not in (" + checkDtlHql.ToString().TrimEnd(',') + ") ";
            }

            //盘库时待选择的盘库明细明细集合
            IList<ReaBmsCheckDtl> dtAddList = new List<ReaBmsCheckDtl>();
            //当前盘库条件说明
            StringBuilder checkCondition = new StringBuilder();
            dtAddList = GetAddCheckDtlList(checkDoc, qtyHql, reaGoodHql, zeroQtyDays, empID, empName, ref checkCondition);
            
            entityList.count = dtAddList.Count;
            //分页处理
            if (limit > 0 && limit < dtAddList.Count)
            {
                int startIndex = limit * (page - 1);
                int endIndex = limit;
                var list = dtAddList.Skip(startIndex).Take(endIndex);
                if (list != null)
                    dtAddList = list.ToList();
            }
            entityList.list = dtAddList;

            return entityList;
        }
        public IList<ReaBmsCheckDtl> SearchReaBmsCheckDtlByJoinHQL(ReaBmsCheckDoc checkDoc, string reaGoodHql, int days)
        {
            IList<ReaBmsCheckDtl> checkDtlList = new List<ReaBmsCheckDtl>();
            StringBuilder checkMemo = new StringBuilder();

            //盘库已锁定的盘库货品
            string checkHql = ((IDReaBmsCheckDocDao)base.DBDao).GetIsLockCheckHql(checkDoc, int.Parse(ReaBmsCheckDocLock.已锁定.Key), ref checkMemo);
            ZhiFang.Common.Log.Log.Debug("盘库已锁定的盘库条件:" + checkHql);
            //最近days天内已盘库的盘库货品
            string checkHql2 = "";
            if (days > 0)
            {
                checkHql2 = ((IDReaBmsCheckDocDao)base.DBDao).GetIsLockCheckHql(checkDoc, -1, ref checkMemo);
                string sdate = DateTime.Now.AddDays(-days).ToString("yyyy-MM-dd");
                string edate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                checkHql2 = checkHql2 + " and reabmscheckdoc.DataAddTime>='" + sdate + " 00:00:00' and reabmscheckdoc.DataAddTime<='" + edate + "')";
                ZhiFang.Common.Log.Log.Debug("最近" + days + "天内已盘库的盘库条件:" + checkHql2);
            }
            if (!string.IsNullOrEmpty(checkHql2))
            {
                checkHql = "((" + checkHql + ") or (" + checkHql2 + "))";
            }
            checkDtlList = IBReaBmsCheckDtl.SearchReaBmsCheckDtlListByJoinHQL(checkHql, "", reaGoodHql, "", -1, -1);
            return checkDtlList;
        }
        public BaseResultDataValue AddReaBmsCheckDocAndDtlList(ReaBmsCheckDoc checkDoc, IList<ReaBmsCheckDtl> dtAddList, long empID, string empName, bool isTakenFromQty)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            BaseResultBool tempBaseResultBool = ((IDReaBmsCheckDocDao)base.DBDao).EditValidIsLock(checkDoc, dtAddList);
            baseResultDataValue.success = tempBaseResultBool.success;
            baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
            if (baseResultDataValue.success == false) return baseResultDataValue;

            checkDoc.IsLock = int.Parse(ReaBmsCheckDocLock.已锁定.Key);
            checkDoc.Visible = true;
            checkDoc.CreaterID = empID;
            checkDoc.CreaterName = empName;
            checkDoc.CheckerID = empID;
            checkDoc.CheckerName = empName;
            checkDoc.CheckDateTime = DateTime.Now;
            checkDoc.DataUpdateTime = DateTime.Now;
            checkDoc.IsException = 0;
            checkDoc.IsHandleException = 0;
            checkDoc.Status = int.Parse(ReaBmsCheckDocStatus.盘库锁定.Key);
            checkDoc.StatusName = ReaBmsCheckDocStatus.GetStatusDic()[checkDoc.Status.ToString()].Name;
            checkDoc.CheckDocNo = this.GetCheckDocNo();
            checkDoc.BmsCheckResult = int.Parse(ReaBmsCheckResult.未盘盈及未盘亏.Key);

            //当前盘库条件说明
            StringBuilder checkCondition = new StringBuilder();
            if (dtAddList.Count <= 0)
            {
                baseResultDataValue.success = false;
                if (checkCondition.ToString().Length <= 0)
                    checkCondition.Append("按全部");
                baseResultDataValue.ErrorInfo = string.Format("获取盘库条件为:{0},的有效库存数据信息为空!", checkCondition.ToString());
                return baseResultDataValue;
            }
            this.Entity = checkDoc;
            if (this.Add())
            {
                tempBaseResultBool = IBReaBmsCheckDtl.AddCheckDtlOfList(this.Entity, dtAddList, empID, empName, isTakenFromQty);
                baseResultDataValue.success = tempBaseResultBool.success;
                baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：保存盘库主单信息失败！";
            }

            return baseResultDataValue;
        }
        public BaseResultDataValue SearchReaBmsInDocOfCheckDocID(long id, bool isPlanish, string fields, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            ReaBmsInDoc inDoc = null;
            ReaBmsCheckDoc entity = this.Get(id);
            if (entity == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "盘库单ID：" + id + "不存在数据库中！";
                return baseResultDataValue;
            }
            if (entity.BmsCheckResult == int.Parse(ReaBmsCheckResult.已盘盈.Key) || entity.BmsCheckResult == int.Parse(ReaBmsCheckResult.已盘盈已盘亏.Key))
            {
                IList<ReaBmsInDoc> inDocList = IBReaBmsInDoc.SearchListByHQL("reabmsindoc.CheckDocID=" + id);
                if (inDocList != null && inDocList.Count == 1)
                    inDoc = inDocList[0];
            }
            else
            {
                IList<ReaBmsCheckDtl> dtlCheckList = IBReaBmsCheckDtl.SearchListByHQL("reabmscheckdtl.GoodsQty>0 and reabmscheckdtl.CheckQty>reabmscheckdtl.GoodsQty and reabmscheckdtl.CheckDocID=" + entity.Id);
                if (dtlCheckList == null || dtlCheckList.Count <= 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "盘库单ID：" + id + ",没有符合差异调整的盘盈入库信息！";
                    return baseResultDataValue;
                }
                if (dtlCheckList != null)
                {
                    inDoc = new ReaBmsInDoc();
                    inDoc.CheckDocID = id;
                    inDoc.OperDate = DateTime.Now;
                    inDoc.DataUpdateTime = DateTime.Now;
                    inDoc.InDocNo = GetCheckDocNo();
                    inDoc.UserID = empID;
                    inDoc.UserName = empName;
                    inDoc.CreaterID = empID;
                    inDoc.CreaterName = empName;
                    inDoc.Status = int.Parse(ReaBmsInDocStatus.待继续入库.Key);
                    inDoc.StatusName = ReaBmsInDocStatus.GetStatusDic()[inDoc.Status.ToString()].Name;
                    inDoc.InType = int.Parse(ReaBmsInDocInType.盘盈入库.Key);
                    inDoc.SourceType = int.Parse(ReaBmsInSourceType.盘盈入库.Key);
                    if (inDoc.InType.HasValue)
                        inDoc.InTypeName = ReaBmsInDocInType.GetStatusDic()[inDoc.InType.Value.ToString()].Name;
                }
            }
            if (inDoc != null)
            {
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsInDoc>(inDoc);
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "盘库单ID：" + id + ",没有符合差异调整的盘盈入库信息！";
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue SearchReaBmsInDtlListOfCheckDocID(long id, bool isPlanish, string fields, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsInDtl> entityList = new EntityList<ReaBmsInDtl>();
            entityList.list = new List<ReaBmsInDtl>();

            ReaBmsCheckDoc entity = this.Get(id);
            if (entity == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "盘库单ID：" + id + "不存在数据库中！";
                return baseResultDataValue;
            }
            if (entity.BmsCheckResult == int.Parse(ReaBmsCheckResult.已盘盈.Key) || entity.BmsCheckResult == int.Parse(ReaBmsCheckResult.已盘盈已盘亏.Key))
            {
                IList<ReaBmsInDoc> tempList = IBReaBmsInDoc.SearchListByHQL("reabmsindoc.CheckDocID=" + id);
                if (tempList.Count != 1)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "盘库单ID：" + id + ",在入库单里的相关记录数为:" + tempList.Count;
                    return baseResultDataValue;
                }
                entityList = IBReaBmsInDtl.GetReaBmsInDtlListByHql("reabmsindtl.InDocID=" + tempList[0].Id + "", "", -1, -1);//IBReaBmsInDtl.SearchListByHQL("reabmsindtl.InDocID=" + tempList[0].Id + "", -1, -1);
            }
            else
            {
                IList<ReaBmsCheckDtl> dtlCheckList = IBReaBmsCheckDtl.SearchListByHQL("reabmscheckdtl.GoodsQty>0 and reabmscheckdtl.CheckQty>reabmscheckdtl.GoodsQty and reabmscheckdtl.CheckDocID=" + entity.Id);
                if (dtlCheckList == null || dtlCheckList.Count <= 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "盘库单ID：" + id + ",没有符合差异调整的盘盈入库信息！";
                    return baseResultDataValue;
                }
                if (dtlCheckList != null)
                {
                    entityList.count = dtlCheckList.Count;
                    foreach (var dtlCheck in dtlCheckList)
                    {
                        ReaGoods goods = IDReaGoodsDao.Get(dtlCheck.GoodsID.Value);

                        ReaBmsInDtl inDtl = new ReaBmsInDtl();
                        inDtl.ReaGoods = goods;
                        inDtl.BarCodeType = goods.BarCodeMgr;
                        inDtl.GoodsCName = dtlCheck.GoodsName;
                        inDtl.GoodsUnit = dtlCheck.GoodsUnit;
                        inDtl.UnitMemo = dtlCheck.UnitMemo;
                        inDtl.Price = dtlCheck.Price;
                        if (!inDtl.Price.HasValue)
                            inDtl.Price = 0;
                        inDtl.GoodsQty = (dtlCheck.CheckQty - dtlCheck.GoodsQty);
                        inDtl.SumTotal = inDtl.Price * inDtl.GoodsQty;

                        inDtl.GoodsLotID = dtlCheck.GoodsLotID;
                        inDtl.LotNo = dtlCheck.LotNo;
                        inDtl.StorageID = dtlCheck.StorageID;
                        inDtl.StorageName = dtlCheck.StorageName;
                        inDtl.PlaceID = dtlCheck.PlaceID;
                        inDtl.PlaceName = dtlCheck.PlaceName;

                        inDtl.ReaCompanyID = dtlCheck.ReaCompanyID;
                        inDtl.CompanyName = dtlCheck.CompanyName;
                        inDtl.ReaServerCompCode = dtlCheck.ReaServerCompCode;
                        inDtl.ReaCompCode = dtlCheck.ReaCompCode;

                        inDtl.ProdDate = dtlCheck.ProdDate;
                        inDtl.InvalidDate = dtlCheck.InvalidDate;
                        inDtl.LotSerial = dtlCheck.LotSerial;
                        inDtl.LotQRCode = dtlCheck.LotQRCode;
                        inDtl.SysLotSerial = dtlCheck.SysLotSerial;

                        inDtl.CompGoodsLinkID = dtlCheck.CompGoodsLinkID;
                        inDtl.Visible = true;
                        inDtl.DataUpdateTime = DateTime.Now;
                        inDtl.ReaGoodsNo = dtlCheck.ReaGoodsNo;
                        inDtl.ProdGoodsNo = dtlCheck.ProdGoodsNo;
                        inDtl.CenOrgGoodsNo = dtlCheck.CenOrgGoodsNo;
                        inDtl.GoodsNo = dtlCheck.GoodsNo;
                        inDtl.GoodsSort = dtlCheck.GoodsSort;

                        inDtl.ProdOrgName = goods.ProdOrgName;
                        inDtl.GoodSName = goods.SName;
                        entityList.list.Add(inDtl);
                    }
                }
            }

            if (entityList.count > 0)
            {
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsInDtl>(entityList);
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "盘库单ID：" + id + ",没有符合差异调整的盘盈入库信息！";
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue SearchReaBmsOutDocOfCheckDocID(long id, bool isPlanish, string fields, long deptID, string deptName, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            ReaBmsOutDoc outDoc = null;
            ReaBmsCheckDoc entity = this.Get(id);
            if (entity == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "盘库单ID：" + id + "不存在数据库中！";
                return baseResultDataValue;
            }
            if (entity.BmsCheckResult == int.Parse(ReaBmsCheckResult.已盘亏.Key) || entity.BmsCheckResult == int.Parse(ReaBmsCheckResult.已盘盈已盘亏.Key))
            {
                IList<ReaBmsOutDoc> outDocList = IBReaBmsOutDoc.SearchListByHQL("reabmsoutdoc.CheckDocID=" + id);
                if (outDocList != null && outDocList.Count == 1)
                    outDoc = outDocList[0];
            }
            else
            {
                IList<ReaBmsCheckDtl> dtlCheckList = IBReaBmsCheckDtl.SearchListByHQL("reabmscheckdtl.GoodsQty>0 and reabmscheckdtl.GoodsQty>reabmscheckdtl.CheckQty and reabmscheckdtl.CheckDocID=" + entity.Id);
                if (dtlCheckList == null || dtlCheckList.Count <= 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "盘库单ID：" + id + ",没有符合差异调整的盘亏出库信息！";
                    return baseResultDataValue;
                }
                if (dtlCheckList != null)
                {
                    outDoc = new ReaBmsOutDoc();
                    outDoc.DeptID = deptID;
                    outDoc.DeptName = deptName;
                    outDoc.OperDate = DateTime.Now;
                    outDoc.DataAddTime = DateTime.Now;
                    outDoc.CheckDocID = id;
                    outDoc.DataUpdateTime = DateTime.Now;
                    outDoc.OutDocNo = GetCheckDocNo();
                    outDoc.OutBoundID = empID;
                    outDoc.OutBoundName = empName;
                    outDoc.CreaterID = empID;
                    outDoc.CreaterName = empName;
                    outDoc.TakerID = empID;
                    outDoc.TakerName = empName;
                    outDoc.CheckID = empID;
                    outDoc.CheckName = empName;
                    //outDoc.Status = int.Parse(ReaBmsInDocStatus.已入库.Key);
                    outDoc.OutType = int.Parse(ReaBmsOutDocOutType.盘亏出库.Key);//报损出库
                    outDoc.OutTypeName = ReaBmsOutDocOutType.GetStatusDic()[outDoc.OutType.ToString()].Name;
                }
            }
            if (outDoc != null)
            {
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ReaBmsOutDoc>(outDoc);
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "盘库单ID：" + id + ",没有符合差异调整的盘亏出库信息！";
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue SearchReaBmsOutDtlListOfCheckDocID(long id, bool isPlanish, string fields, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsOutDtl> entityList = new EntityList<ReaBmsOutDtl>();
            entityList.list = new List<ReaBmsOutDtl>();
            ReaBmsCheckDoc entity = this.Get(id);

            if (entity == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "盘库单ID：" + id + "不存在数据库中！";
                return baseResultDataValue;
            }
            if (entity.BmsCheckResult == int.Parse(ReaBmsCheckResult.已盘亏.Key) || entity.BmsCheckResult == int.Parse(ReaBmsCheckResult.已盘盈已盘亏.Key))
            {
                IList<ReaBmsOutDoc> outDocList = IBReaBmsOutDoc.SearchListByHQL("reabmsoutdoc.CheckDocID=" + id);
                ReaBmsOutDoc outDoc = null;
                if (outDocList != null && outDocList.Count == 1)
                    outDoc = outDocList[0];
                if (outDoc != null)
                    entityList = IBReaBmsOutDtl.GetListByHQL("reabmsoutdtl.OutDocID=" + outDoc.Id, "", -1, -1, true); //IBReaBmsOutDtl.SearchListByHQL("reabmsoutdtl.OutDocID=" + outDoc.Id, "", -1, -1);
            }
            else
            {
                IList<ReaBmsCheckDtl> dtlCheckList = IBReaBmsCheckDtl.SearchListByHQL("reabmscheckdtl.GoodsQty>0 and reabmscheckdtl.GoodsQty>reabmscheckdtl.CheckQty and reabmscheckdtl.CheckDocID=" + entity.Id);
                if (dtlCheckList == null || dtlCheckList.Count <= 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "盘库单ID：" + id + ",没有符合差异调整的盘亏出库信息！";
                    return baseResultDataValue;
                }
                if (dtlCheckList != null)
                {
                    entityList.count = dtlCheckList.Count;

                    #region 获取出库货品的默认仪器信息
                    StringBuilder strbHql = new StringBuilder();
                    foreach (var dtlCheck in dtlCheckList)
                    {
                        if (dtlCheck.GoodsID.HasValue)
                            strbHql.Append(" reaequipreagentlink.GoodsID=" + dtlCheck.GoodsID.Value + " or");
                    }
                    IList<ReaEquipReagentLink> tempEquipLinkList = new List<ReaEquipReagentLink>();
                    char[] trimChars = new char[] { ' ', 'o', 'r' };
                    if (strbHql.ToString().Length > 0)
                    {
                        string hql = "reaequipreagentlink.Visible=1 and (" + strbHql.ToString().TrimEnd(trimChars) + ")";
                        tempEquipLinkList = IDReaEquipReagentLinkDao.GetListByHQL(hql);
                    }

                    IList<ReaTestEquipLab> tempEquipList = new List<ReaTestEquipLab>();
                    if (tempEquipLinkList.Count > 0)
                    {
                        StringBuilder strbHql2 = new StringBuilder();
                        var groupBy = tempEquipLinkList.GroupBy(p => p.TestEquipID);
                        foreach (var item in groupBy)
                        {
                            if (item.Key.HasValue)
                                strbHql2.Append(" reatestequiplab.Id=" + item.Key.Value + " or");
                        }
                        if (strbHql2.ToString().Length > 0)
                        {
                            string hql = "(" + strbHql2.ToString().TrimEnd(trimChars) + ")";
                            tempEquipList = IDReaTestEquipLabDao.GetListByHQL(hql);
                        }
                    }
                    #endregion

                    foreach (var dtlCheck in dtlCheckList)
                    {
                        ReaBmsOutDtl outDtl = new ReaBmsOutDtl();
                        ReaGoods goods = IDReaGoodsDao.Get(dtlCheck.GoodsID.Value);
                        outDtl.BarCodeType = goods.BarCodeMgr;
                        outDtl.GoodsID = dtlCheck.GoodsID.Value;
                        outDtl.GoodsCName = dtlCheck.GoodsName;
                        outDtl.GoodsUnit = dtlCheck.GoodsUnit;
                        outDtl.UnitMemo = dtlCheck.UnitMemo;
                        outDtl.SName = goods.SName;
                        outDtl.ProdOrgName = goods.ProdOrgName;

                        if (dtlCheck.Price.HasValue)
                            outDtl.Price = dtlCheck.Price.Value;
                        else
                            outDtl.Price = 0;
                        outDtl.GoodsQty = (dtlCheck.GoodsQty.Value - dtlCheck.CheckQty.Value);
                        outDtl.ReqGoodsQty = outDtl.GoodsQty;
                        outDtl.SumTotal = outDtl.Price * outDtl.GoodsQty;

                        outDtl.GoodsLotID = dtlCheck.GoodsLotID;
                        outDtl.LotNo = dtlCheck.LotNo;
                        if (dtlCheck.StorageID.HasValue)
                            outDtl.StorageID = dtlCheck.StorageID.Value;
                        outDtl.StorageName = dtlCheck.StorageName;
                        if (dtlCheck.PlaceID.HasValue)
                            outDtl.PlaceID = dtlCheck.PlaceID.Value;
                        outDtl.PlaceName = dtlCheck.PlaceName;

                        outDtl.ReaCompanyID = dtlCheck.ReaCompanyID;
                        outDtl.CompanyName = dtlCheck.CompanyName;
                        outDtl.ReaServerCompCode = dtlCheck.ReaServerCompCode;

                        outDtl.ProdDate = dtlCheck.ProdDate;
                        outDtl.InvalidDate = dtlCheck.InvalidDate;
                        outDtl.LotSerial = dtlCheck.LotSerial;
                        outDtl.LotQRCode = dtlCheck.LotQRCode;
                        outDtl.SysLotSerial = dtlCheck.SysLotSerial;
                        if (dtlCheck.CompGoodsLinkID.HasValue)
                            outDtl.CompGoodsLinkID = dtlCheck.CompGoodsLinkID.Value;
                        outDtl.Visible = true;
                        outDtl.DataUpdateTime = DateTime.Now;
                        //实验室检验仪器默认值
                        var tempEquipList2 = tempEquipLinkList.Where(p => p.GoodsID == outDtl.GoodsID).OrderBy(p => p.DispOrder);
                        if (tempEquipList2 != null && tempEquipList2.Count() > 0)
                        {
                            outDtl.TestEquipID = tempEquipList2.ElementAt(0).TestEquipID.Value;
                            outDtl.TestEquipName = tempEquipList.Where(p => p.Id == outDtl.TestEquipID).ElementAt(0).CName;
                        }
                        outDtl.ReaGoodsNo = dtlCheck.ReaGoodsNo;
                        outDtl.ProdGoodsNo = dtlCheck.ProdGoodsNo;
                        outDtl.CenOrgGoodsNo = dtlCheck.CenOrgGoodsNo;
                        outDtl.GoodsNo = dtlCheck.GoodsNo;

                        outDtl.ReaCompCode = dtlCheck.ReaCompCode;
                        outDtl.GoodsSort = dtlCheck.GoodsSort;
                        entityList.list.Add(outDtl);
                    }
                }
            }
            if (entityList.count > 0)
            {
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsOutDtl>(entityList);
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "盘库单ID：" + id + ",没有符合差异调整的盘亏出库信息！";
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue AddReaBmsInDocAndDtlOfCheckDocID(long checkDocID, ReaBmsInDoc inDoc, IList<ReaBmsInDtlVO> dtAddList, string codeScanningMode, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            ReaBmsCheckDoc checkDoc = this.Get(checkDocID);
            if (checkDoc == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "盘库单ID：" + checkDocID + "不存在数据库中！";
                return baseResultDataValue;
            }
            if (checkDoc.Status.ToString() != ReaBmsCheckDocStatus.确认盘库.Key && checkDoc.Status.ToString() != ReaBmsCheckDocStatus.差异调整中.Key)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "盘库单ID：" + checkDoc.Id + "的状态为：" + ReaBmsCheckDocStatus.GetStatusDic()[checkDoc.Status.ToString()].Name + "！";
                return baseResultDataValue;
            }
            if (checkDoc.BmsCheckResult.ToString() == ReaBmsCheckResult.已盘盈.Key || checkDoc.BmsCheckResult.ToString() == ReaBmsCheckResult.已盘盈已盘亏.Key)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "盘库单ID：" + checkDoc.Id + "的盘库结果为：" + ReaBmsCheckResult.GetStatusDic()[checkDoc.BmsCheckResult.ToString()].Name + "！";
                return baseResultDataValue;
            }
            if (dtAddList == null || dtAddList.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "盘库单ID：" + checkDocID + ",盘盈入库信息为空！";
                return baseResultDataValue;
            }
            inDoc.Visible = true;
            IBReaBmsInDoc.Entity = inDoc;
            baseResultDataValue = IBReaBmsInDoc.AddReaBmsInDocAndDtlOfVO(dtAddList, codeScanningMode, empID, empName);
            if (baseResultDataValue.success == false) return baseResultDataValue;

            if (checkDoc.BmsCheckResult.ToString() == ReaBmsCheckResult.未盘盈及未盘亏.Key)
            {
                //是否存在盘亏出库明细,如果不存在盘亏出库,盘盈入库后盘库结果直接是"已盘盈已盘亏"
                IList<ReaBmsCheckDtl> dtlCheckOutList = IBReaBmsCheckDtl.SearchListByHQL("reabmscheckdtl.GoodsQty>0 and reabmscheckdtl.GoodsQty>reabmscheckdtl.CheckQty and reabmscheckdtl.CheckDocID=" + checkDoc.Id);
                if (dtlCheckOutList == null || dtlCheckOutList.Count <= 0)
                {
                    checkDoc.BmsCheckResult = int.Parse(ReaBmsCheckResult.已盘亏.Key);
                }
                checkDoc.Status = int.Parse(ReaBmsCheckDocStatus.差异调整中.Key);
                checkDoc.StatusName = ReaBmsCheckDocStatus.GetStatusDic()[checkDoc.Status.ToString()].Name;
            }

            if (checkDoc.BmsCheckResult.ToString() == ReaBmsCheckResult.未盘盈及未盘亏.Key)
            {
                checkDoc.BmsCheckResult = int.Parse(ReaBmsCheckResult.已盘盈.Key);
                checkDoc.Status = int.Parse(ReaBmsCheckDocStatus.差异调整中.Key);
                checkDoc.StatusName = ReaBmsCheckDocStatus.GetStatusDic()[checkDoc.Status.ToString()].Name;
            }
            else if (checkDoc.BmsCheckResult.ToString() == ReaBmsCheckResult.已盘亏.Key)
            {
                checkDoc.IsHandleException = 1;
                checkDoc.BmsCheckResult = int.Parse(ReaBmsCheckResult.已盘盈已盘亏.Key);
                checkDoc.Status = int.Parse(ReaBmsCheckDocStatus.差异调整完成.Key);
                checkDoc.StatusName = ReaBmsCheckDocStatus.GetStatusDic()[checkDoc.Status.ToString()].Name;
            }

            checkDoc.StatusName = ReaBmsCheckDocStatus.GetStatusDic()[checkDoc.Status.ToString()].Name;
            this.Entity = checkDoc;
            baseResultDataValue.success = this.Edit();
            //盘盈入库的盘库明细的异常是否已处理更新为"已处理"           
            IList<ReaBmsCheckDtl> dtlCheckList = IBReaBmsCheckDtl.SearchListByHQL("reabmscheckdtl.GoodsQty>0 and reabmscheckdtl.CheckQty>reabmscheckdtl.GoodsQty and reabmscheckdtl.CheckDocID=" + checkDoc.Id);
            foreach (var entity in dtlCheckList)
            {
                entity.IsHandleException = 1;
                IBReaBmsCheckDtl.Entity = entity;
                baseResultDataValue.success = IBReaBmsCheckDtl.Edit();
            }

            return baseResultDataValue;
        }
        public BaseResultDataValue AddReaBmsOutDocAndDtlOfCheckDocID(long checkDocID, ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> dtAddList, string codeScanningMode, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            ReaBmsCheckDoc checkDoc = this.Get(checkDocID);
            if (checkDoc == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "盘库单ID：" + checkDocID + "不存在数据库中！";
                return baseResultDataValue;
            }
            if (checkDoc.Status.ToString() != ReaBmsCheckDocStatus.确认盘库.Key && checkDoc.Status.ToString() != ReaBmsCheckDocStatus.差异调整中.Key)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "盘库单ID：" + checkDoc.Id + "的状态为：" + ReaBmsCheckDocStatus.GetStatusDic()[checkDoc.Status.ToString()].Name + "！";
                return baseResultDataValue;
            }
            if (checkDoc.BmsCheckResult.ToString() == ReaBmsCheckResult.已盘亏.Key || checkDoc.BmsCheckResult.ToString() == ReaBmsCheckResult.已盘盈已盘亏.Key)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "盘库单ID：" + checkDoc.Id + "的盘库结果为：" + ReaBmsCheckResult.GetStatusDic()[checkDoc.BmsCheckResult.ToString()].Name + "！";
                return baseResultDataValue;
            }
            if (dtAddList == null || dtAddList.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "盘库单ID：" + checkDocID + ",盘亏出库信息为空！";
                return baseResultDataValue;
            }

            //IBReaBmsOutDoc.Entity = outDoc;
            outDoc.Visible = true;
            outDoc.CheckID = empID;
            outDoc.CheckName = empName;
            outDoc.CheckTime = DateTime.Now;
            outDoc.OutType = int.Parse(ReaBmsOutDocOutType.盘亏出库.Key);//报损出库
            outDoc.OutTypeName = ReaBmsOutDocOutType.GetStatusDic()[outDoc.OutType.ToString()].Name;
            if (outDoc.Status <= 0)
                outDoc.Status = int.Parse(ReaBmsOutDocStatus.出库完成.Key);
            outDoc.StatusName = ReaBmsOutDocStatus.GetStatusDic()[outDoc.Status.ToString()].Name;

            baseResultDataValue = IBReaBmsOutDoc.AddOutDocAndOutDtlListOfComp(ref outDoc, dtAddList, false, false, empID, empName);
            if (baseResultDataValue.success == false) return baseResultDataValue;

            if (checkDoc.BmsCheckResult.ToString() == ReaBmsCheckResult.未盘盈及未盘亏.Key)
            {
                //是否存在盘盈入库明细,如果不存在盘盈入库,盘亏出库后盘库结果直接是"已盘盈已盘亏"
                IList<ReaBmsCheckDtl> dtlCheckInList = IBReaBmsCheckDtl.SearchListByHQL("reabmscheckdtl.GoodsQty>0 and reabmscheckdtl.CheckQty>reabmscheckdtl.GoodsQty and reabmscheckdtl.CheckDocID=" + checkDoc.Id);
                if (dtlCheckInList == null || dtlCheckInList.Count <= 0)
                {
                    checkDoc.BmsCheckResult = int.Parse(ReaBmsCheckResult.已盘盈.Key);
                }
                checkDoc.Status = int.Parse(ReaBmsCheckDocStatus.差异调整中.Key);
                checkDoc.StatusName = ReaBmsCheckDocStatus.GetStatusDic()[checkDoc.Status.ToString()].Name;
            }

            if (checkDoc.BmsCheckResult.ToString() == ReaBmsCheckResult.未盘盈及未盘亏.Key)
            {
                checkDoc.BmsCheckResult = int.Parse(ReaBmsCheckResult.已盘亏.Key);
                checkDoc.Status = int.Parse(ReaBmsCheckDocStatus.差异调整中.Key);
                checkDoc.StatusName = ReaBmsCheckDocStatus.GetStatusDic()[checkDoc.Status.ToString()].Name;
            }
            else if (checkDoc.BmsCheckResult.ToString() == ReaBmsCheckResult.已盘盈.Key)
            {
                checkDoc.IsHandleException = 1;
                checkDoc.BmsCheckResult = int.Parse(ReaBmsCheckResult.已盘盈已盘亏.Key);
                checkDoc.Status = int.Parse(ReaBmsCheckDocStatus.差异调整完成.Key);
                checkDoc.StatusName = ReaBmsCheckDocStatus.GetStatusDic()[checkDoc.Status.ToString()].Name;
            }

            checkDoc.StatusName = ReaBmsCheckDocStatus.GetStatusDic()[checkDoc.Status.ToString()].Name;
            this.Entity = checkDoc;
            baseResultDataValue.success = this.Edit();
            //盘亏出库的盘库明细的异常是否已处理更新为"已处理" 
            IList<ReaBmsCheckDtl> dtlCheckList = IBReaBmsCheckDtl.SearchListByHQL("reabmscheckdtl.GoodsQty>0 and reabmscheckdtl.GoodsQty>reabmscheckdtl.CheckQty and reabmscheckdtl.CheckDocID=" + checkDoc.Id);

            foreach (var entity in dtlCheckList)
            {
                entity.IsHandleException = 1;
                IBReaBmsCheckDtl.Entity = entity;
                baseResultDataValue.success = IBReaBmsCheckDtl.Edit();
            }

            return baseResultDataValue;
        }
        /// <summary>
        /// 获取盘库总单号
        /// </summary>
        /// <returns></returns>
        private string GetCheckDocNo()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));
            Random ran = new Random();
            int randKey = ran.Next(0, 999);
            strb.Append(randKey.ToString().PadLeft(3, '0'));//左补零
            strb.Append(DateTime.Now.ToString("HHmmssfff"));
            return strb.ToString();
        }
        public Stream GetReaBmsCheckDocAndDtlOfPdf(long id, string sort, string frx)
        {
            Stream stream = null;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(sort))
            {
                sort = "reabmscheckdtl.StorageID ASC,reabmscheckdtl.PlaceID ASC,reabmscheckdtl.ReaGoodsNo ASC,reabmscheckdtl.LotNo ASC,reabmscheckdtl.GoodsSort ASC";
            }
            ReaBmsCheckDoc checkDoc = this.Get(id);
            if (checkDoc == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "盘库单ID：" + id + "不存在数据库中！";
                return stream;
            }

            //IList<ReaBmsCheckDtl> dtlList = IBReaBmsCheckDtl.SearchListByHQL("reabmscheckdtl.CheckDocID=" + id, sort, -1, -1).list;
            IList<ReaBmsCheckDtl> dtlList = IBReaBmsCheckDtl.SearchReaBmsCheckDtlListByJoinHQL("", "reabmscheckdtl.CheckDocID=" + id, "", sort, -1, -1);
            if (dtlList == null || dtlList.Count <= 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "盘库单ID：" + id + ",盘库明细信息为空！";
                return stream;
            }

            DataSet dataSet = new DataSet();
            dataSet.DataSetName = "ZhiFang.ReagentSys.Client";
            try
            {
                List<ReaBmsCheckDoc> docList = new List<ReaBmsCheckDoc>();
                docList.Add(checkDoc);

                DataTable dtDoc = ReportBTemplateHelp.ToDataTable<ReaBmsCheckDoc>(docList, null);
                if (dtDoc != null)
                {
                    dtDoc.TableName = "ReaBmsCheckDoc";
                    dataSet.Tables.Add(dtDoc);
                }
                //dtlList = dtlList.OrderBy(p => p.StorageID).ThenBy(p => p.PlaceID).ToList();
                DataTable dtDtl = ReportBTemplateHelp.ToDataTable<ReaBmsCheckDtl>(dtlList, null);
                if (dtDtl != null)
                {
                    dtDtl.TableName = "ReaBmsCheckDtlList";
                    dataSet.Tables.Add(dtDtl);
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("盘库单数据转换为DataTable出错:" + ex.Message);
                throw ex;
            }

            if (string.IsNullOrEmpty(frx))
                frx = "盘点清单.frx";
            string pdfName = checkDoc.Id.ToString() + ".pdf." + FileExt.zf;
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            ExcelReportHelp.CreateEECDataTable(excelCommand, ref dataSet);
            stream = FrxToPdfReportHelp.SavePdfReport(dataSet, checkDoc.LabID, pdfName, FrxToPdfReportHelp.PublicTemplateDir, "盘库清单", frx, false);
            return stream;
        }
    }
}