using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaStoreIn;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using ZhiFang.Common.Public;
using System.IO;
using ZhiFang.ReagentSys.Client.Common;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    /// 确认入库(状态为已入库)时需要判断(供应商,库房,货架)是否正在被盘库锁定
    /// </summary>
    public class BReaBmsInDoc : BaseBLL<ReaBmsInDoc>, ZhiFang.IBLL.ReagentSys.Client.IBReaBmsInDoc
    {
        IBReaBmsInDtl IBReaBmsInDtl { get; set; }
        IBReaBmsCenSaleDocConfirm IBReaBmsCenSaleDocConfirm { get; set; }
        IBReaBmsCenSaleDtlConfirm IBReaBmsCenSaleDtlConfirm { get; set; }
        IBReaCheckInOperation IBReaCheckInOperation { get; set; }
        IDReaBmsCheckDocDao IDReaBmsCheckDocDao { get; set; }
        IBReaBmsOutDtl IBReaBmsOutDtl { get; set; }
        IBReaBmsQtyDtl IBReaBmsQtyDtl { get; set; }
        IDCenOrgDao IDCenOrgDao { get; set; }
        IDReaBmsCenSaleDocDao IDReaBmsCenSaleDocDao { get; set; }
        IBReaGoodsBarcodeOperation IBReaGoodsBarcodeOperation { get; set; }
        IBReaBmsQtyDtlOperation IBReaBmsQtyDtlOperation { get; set; }

        public override bool Add()
        {
            if (this.Entity.ReconciliationMark <= 0)
            {
                this.Entity.ReconciliationMark = int.Parse(ReaBmsInDocReconciliationMark.未对帐.Key);
            }
            if (this.Entity.LockMark <= 0)
            {
                this.Entity.LockMark = int.Parse(ReaBmsInDocLockMark.未锁定.Key);
            }
            string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            if (!this.Entity.UserID.HasValue && !string.IsNullOrWhiteSpace(employeeID))
            {
                this.Entity.UserID = long.Parse(employeeID);
                this.Entity.UserName = empName;
            }
            if (!this.Entity.CreaterID.HasValue && !string.IsNullOrWhiteSpace(employeeID))
            {
                this.Entity.CreaterID = long.Parse(employeeID);
                this.Entity.CreaterName = empName;
            }
            if (!this.Entity.DeptID.HasValue)
            {
                string deptID = SessionHelper.GetSessionValue(DicCookieSession.HRDeptID);
                string deptName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptName);
                if (!string.IsNullOrWhiteSpace(deptID))
                {
                    this.Entity.DeptID = long.Parse(deptID);
                    this.Entity.DeptName = deptName;
                }
            }
            return base.Add();
        }
        public BaseResultBool EditValidIsLockOfDtlVOList(ReaBmsInDoc entity, IList<ReaBmsInDtlVO> dtlVOList)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.success = true;
            if (entity.Status.ToString() == ReaBmsInDocStatus.已入库.Key)
            {
                //按供应商ID+库房ID+货架分组
                var groupBy = dtlVOList.GroupBy(p => new { p.ReaBmsInDtl.ReaCompanyID, p.ReaBmsInDtl.StorageID, p.ReaBmsInDtl.PlaceID });
                foreach (var item in groupBy)
                {
                    var p = item.ElementAt(0);
                    tempBaseResultBool = IDReaBmsCheckDocDao.EditValidIsLock(p.ReaBmsInDtl.ReaCompanyID, p.ReaBmsInDtl.CompanyName, p.ReaBmsInDtl.StorageID, p.ReaBmsInDtl.StorageName, p.ReaBmsInDtl.PlaceID, p.ReaBmsInDtl.PlaceName, p.ReaBmsInDtl.ReaGoods.Id);
                    if (tempBaseResultBool.success == false) break;
                }
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditValidIsLockOfDtlList(ReaBmsInDoc entity, IList<ReaBmsInDtl> dtlVOList)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.success = true;
            //按供应商ID+库房ID+货架分组
            var groupBy = dtlVOList.GroupBy(p => new { p.ReaCompanyID, p.StorageID, p.PlaceID });
            foreach (var item in groupBy)
            {
                var p = item.ElementAt(0);
                tempBaseResultBool = IDReaBmsCheckDocDao.EditValidIsLock(p.ReaCompanyID, p.CompanyName, p.StorageID, p.StorageName, p.PlaceID, p.PlaceName, p.ReaGoods.Id);
                if (tempBaseResultBool.success == false) break;
            }
            return tempBaseResultBool;
        }
        public BaseResultDataValue AddInDocAndInDtlList(ReaBmsInDoc entity, IList<ReaBmsInDtl> dtlAddList, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            ZhiFang.Common.Log.Log.Info("开始保存新增入库单！");
            baseResultDataValue.success = true;

            if (entity == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "入库主单信息(entity)为空，不能入库！";
                ZhiFang.Common.Log.Log.Warn(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            if (dtlAddList == null || dtlAddList.Count <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "入库明细(entity)为空，不能入库！";
                ZhiFang.Common.Log.Log.Warn(baseResultDataValue.ErrorInfo);
                return baseResultDataValue;
            }
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool.success = true;
            foreach (var inDtl in dtlAddList)
            {
                tempBaseResultBool = IBReaBmsInDtl.EditsInDtlBasicValid(inDtl);
                if (!tempBaseResultBool.success)
                {
                    baseResultDataValue.success = tempBaseResultBool.success;
                    baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                    ZhiFang.Common.Log.Log.Warn(baseResultDataValue.ErrorInfo);
                    break;
                }
            }
            if (!baseResultDataValue.success)
                return baseResultDataValue;

            if (!entity.TotalPrice.HasValue)
                entity.TotalPrice = dtlAddList.Sum(p => p.SumTotal.Value);
            entity.Visible = true;
            if (string.IsNullOrEmpty(entity.InDocNo))
                entity.InDocNo = this.GetInDocNo();
            entity.CreaterID = empID;
            entity.CreaterName = empName;
            entity.OperDate = DateTime.Now;
            entity.DataUpdateTime = DateTime.Now;
            entity.UserID = empID;
            entity.UserName = empName;
            if (entity.Status.ToString() != ReaBmsInDocStatus.待继续入库.Key && entity.Status.ToString() != ReaBmsInDocStatus.已入库.Key)
                entity.Status = int.Parse(ReaBmsInDocStatus.待继续入库.Key);
            entity.StatusName = ReaBmsInDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
            if (!entity.InType.HasValue || entity.InType <= 0)
                entity.InType = long.Parse(ReaBmsInDocInType.验货入库.Key);
            if (!entity.SourceType.HasValue || entity.SourceType <= 0)
                entity.SourceType = long.Parse(ReaBmsInSourceType.验货入库.Key);
            if (entity.InType.HasValue)
                entity.InTypeName = ReaBmsInDocInType.GetStatusDic()[entity.InType.Value.ToString()].Name;

            //判断是否正在被盘库锁定
            if (entity.Status.ToString() == ReaBmsInDocStatus.已入库.Key)
            {
                tempBaseResultBool = EditValidIsLockOfDtlList(entity, dtlAddList);
                if (tempBaseResultBool.success == false)
                {
                    baseResultDataValue.success = tempBaseResultBool.success;
                    baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                    ZhiFang.Common.Log.Log.Warn(tempBaseResultBool.ErrorInfo);
                    return baseResultDataValue;
                }
            }
            this.Entity = entity;
            if (this.Add())
            {
                baseResultDataValue = IBReaBmsInDtl.AddReaBmsInDtlList(entity, dtlAddList, empID, empName);
                if (baseResultDataValue.success)
                    AddReaCheckInOperation(entity, empID, empName);
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "保存入库主单信息失败!";
                ZhiFang.Common.Log.Log.Warn(baseResultDataValue.ErrorInfo);
            }
            ZhiFang.Common.Log.Log.Info("结束保存新增入库单！");
            return baseResultDataValue;
        }
        public BaseResultDataValue AddReaBmsInDocAndDtlOfVO(IList<ReaBmsInDtlVO> dtAddList, string codeScanningMode, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            BaseResultBool tempBaseResultBool = IBReaBmsInDtl.EditReaBmsInDtlListVOOfValid(dtAddList, codeScanningMode);
            if (tempBaseResultBool.success == false)
            {
                baseResultDataValue.success = tempBaseResultBool.success;
                baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                return baseResultDataValue;
            }
            //判断是否正在被盘库锁定
            if (this.Entity.Status.ToString() == ReaBmsInDocStatus.已入库.Key)
            {
                tempBaseResultBool = EditValidIsLockOfDtlVOList(this.Entity, dtAddList);
                if (tempBaseResultBool.success == false)
                {
                    baseResultDataValue.success = tempBaseResultBool.success;
                    baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                    return baseResultDataValue;
                }
            }
            baseResultDataValue = AddReaBmsInDocAndDtl(dtAddList, codeScanningMode, empID, empName);
            if (baseResultDataValue.success)
                AddReaCheckInOperation(this.Entity, empID, empName);
            return baseResultDataValue;
        }
        public BaseResultDataValue AddReaBmsInDocAndDtlByInterface(IList<ReaBmsInDtlVO> dtAddList, string codeScanningMode, string iSNeedCreateBarCode, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            BaseResultBool tempBaseResultBool = IBReaBmsInDtl.EditReaBmsInDtlListVOOfValid(dtAddList, codeScanningMode);
            if (tempBaseResultBool.success == false)
            {
                baseResultDataValue.success = tempBaseResultBool.success;
                baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                return baseResultDataValue;
            }
            //判断是否正在被盘库锁定
            if (this.Entity.Status.ToString() == ReaBmsInDocStatus.已入库.Key)
            {
                tempBaseResultBool = EditValidIsLockOfDtlVOList(this.Entity, dtAddList);
                if (tempBaseResultBool.success == false)
                {
                    baseResultDataValue.success = tempBaseResultBool.success;
                    baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                    return baseResultDataValue;
                }
            }
            baseResultDataValue = AddReaBmsInDtlOfVOByInterface(dtAddList, codeScanningMode, iSNeedCreateBarCode, empID, empName);
            if (baseResultDataValue.success)
                AddReaCheckInOperation(this.Entity, empID, empName);
            return baseResultDataValue;
        }
        public BaseResultBool EditReaBmsInDocAndDtlOfVO(ReaBmsInDoc entity, string[] tempArray, IList<ReaBmsInDtlVO> dtAddList, IList<ReaBmsInDtlVO> dtEditList, string fieldsDtl, string codeScanningMode, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "entity为空，不能操作入库！";
                return tempBaseResultBool;
            }
            ReaBmsInDoc serverEntity = this.Get(entity.Id);

            List<string> tmpa = tempArray.ToList();
            if (!EditReaBmsInDocStatusCheck(entity, serverEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "入库单ID：" + entity.Id + "的状态为：" + ReaBmsInDocStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }
            //判断是否正在被盘库锁定
            if (entity.Status.ToString() == ReaBmsInDocStatus.已入库.Key)
            {
                IList<ReaBmsInDtlVO> dtlVOList = new List<ReaBmsInDtlVO>();
                if (dtAddList != null && dtAddList.Count > 0)
                    dtlVOList = dtAddList;
                if (dtEditList != null && dtEditList.Count > 0)
                    dtlVOList = dtlVOList.Union(dtEditList).ToList();
                tempBaseResultBool = EditValidIsLockOfDtlVOList(entity, dtlVOList);
                if (tempBaseResultBool.success == false)
                    return tempBaseResultBool;
            }

            if (dtAddList != null && dtAddList.Count > 0)
                tempBaseResultBool = IBReaBmsInDtl.EditReaBmsInDtlListVOOfValid(dtAddList, codeScanningMode);
            if (tempBaseResultBool.success == false)
                return tempBaseResultBool;

            if (dtEditList != null && dtEditList.Count > 0)
                tempBaseResultBool = IBReaBmsInDtl.EditReaBmsInDtlListVOOfValid(dtEditList, codeScanningMode);
            if (tempBaseResultBool.success == false)
                return tempBaseResultBool;

            entity.DataUpdateTime = DateTime.Now;
            entity.UserID = empID;
            entity.UserName = empName;
            entity.StatusName = ReaBmsInDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
            if (entity.InType.HasValue)
                entity.InTypeName = ReaBmsInDocInType.GetStatusDic()[entity.InType.Value.ToString()].Name;
            double totalPrice = 0;
            if (dtAddList != null && dtAddList.Count > 0)
                totalPrice = dtAddList.Sum(p => p.ReaBmsInDtl.SumTotal.Value);
            if (dtEditList != null && dtEditList.Count > 0)
                totalPrice = totalPrice + dtEditList.Sum(p => p.ReaBmsInDtl.SumTotal.Value);
            entity.TotalPrice = totalPrice;
            this.Entity = entity;

            tmpa.Add("UserID=" + this.Entity.UserID + " ");
            tmpa.Add("UserName='" + entity.UserName + "'");
            tmpa.Add("StatusName='" + this.Entity.StatusName + "'");
            tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            tmpa.Add("TotalPrice=" + this.Entity.TotalPrice + " ");
            tempArray = tmpa.ToArray();
            tempBaseResultBool.success = this.Update(tempArray);

            if (tempBaseResultBool.success)
            {
                tempBaseResultBool = IBReaBmsInDtl.EditReaBmsInDtlOfVO(entity, dtAddList, dtEditList, fieldsDtl, empID, empName);
                if (tempBaseResultBool.success)
                    AddReaCheckInOperation(entity, empID, empName);

            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditReaBmsInDocAndDtlOfVOByInterface(ReaBmsInDoc entity, string[] tempArray, IList<ReaBmsInDtlVO> dtEditList, string iSNeedCreateBarCode, string fieldsDtl, string codeScanningMode, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "entity为空，不能操作入库！";
                return tempBaseResultBool;
            }
            ReaBmsInDoc serverEntity = this.Get(entity.Id);

            List<string> tmpa = tempArray.ToList();
            if (!EditReaBmsInDocStatusCheck(entity, serverEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "入库单ID：" + entity.Id + "的状态为：" + ReaBmsInDocStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }
            //判断是否正在被盘库锁定
            if (entity.Status.ToString() == ReaBmsInDocStatus.已入库.Key)
            {
                if (dtEditList != null && dtEditList.Count > 0)
                    tempBaseResultBool = EditValidIsLockOfDtlVOList(entity, dtEditList);
                if (tempBaseResultBool.success == false)
                    return tempBaseResultBool;
            }
            if (tempBaseResultBool.success == false)
                return tempBaseResultBool;

            if (dtEditList != null && dtEditList.Count > 0)
                tempBaseResultBool = IBReaBmsInDtl.EditReaBmsInDtlListVOOfValid(dtEditList, codeScanningMode);
            if (tempBaseResultBool.success == false)
                return tempBaseResultBool;

            entity.DataUpdateTime = DateTime.Now;
            entity.UserID = empID;
            entity.UserName = empName;
            entity.StatusName = ReaBmsInDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
            if (entity.InType.HasValue)
                entity.InTypeName = ReaBmsInDocInType.GetStatusDic()[entity.InType.Value.ToString()].Name;
            double totalPrice = 0;
            if (dtEditList != null && dtEditList.Count > 0)
                totalPrice = totalPrice + dtEditList.Sum(p => p.ReaBmsInDtl.SumTotal.Value);
            entity.TotalPrice = totalPrice;
            if (string.IsNullOrEmpty(entity.InDocNo))
            {
                entity.InDocNo = serverEntity.InDocNo;
            }
            if (string.IsNullOrEmpty(entity.OtherDocNo))
            {
                entity.OtherDocNo = serverEntity.OtherDocNo;
            }
            if (string.IsNullOrEmpty(entity.SaleDocNo))
            {
                entity.SaleDocNo = serverEntity.SaleDocNo;
            }
            if (!entity.SaleDocID.HasValue)
            {
                entity.SaleDocID = serverEntity.SaleDocID;
            }
            if (!entity.SaleDocConfirmID.HasValue)
            {
                entity.SaleDocConfirmID = serverEntity.SaleDocConfirmID;
            }
            this.Entity = entity;
            tmpa.Add("UserID=" + this.Entity.UserID + " ");
            tmpa.Add("UserName='" + this.Entity.UserName + "'");
            tmpa.Add("StatusName='" + this.Entity.StatusName + "'");
            tmpa.Add("OperDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'");
            tmpa.Add("TotalPrice=" + this.Entity.TotalPrice + " ");
            tempArray = tmpa.ToArray();
            tempBaseResultBool.success = this.Update(tempArray);

            if (tempBaseResultBool.success)
            {
                tempBaseResultBool = IBReaBmsInDtl.EditReaBmsInDtlOfVOByInterface(entity, dtEditList, iSNeedCreateBarCode, fieldsDtl, empID, empName);
                if (tempBaseResultBool.success)
                    AddReaCheckInOperation(entity, empID, empName);

            }
            return tempBaseResultBool;
        }
        public BaseResultDataValue AddReaBmsInDocAndDtlOfVO(IList<ReaBmsInDtlVO> dtAddList, long docConfirmID, string dtlDocConfirmIDStr, string codeScanningMode, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (docConfirmID <= 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "本次入库的验收主单ID为空，不能入库！";
                return baseResultDataValue;
            }
            if (string.IsNullOrEmpty(dtlDocConfirmIDStr))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "本次入库的验收明细IDStr为空，不能入库！";
                return baseResultDataValue;
            }
            BaseResultBool tempBaseResultBool = IBReaBmsInDtl.EditReaBmsInDtlListVOOfValid(dtAddList, codeScanningMode);
            if (tempBaseResultBool.success == false)
            {
                baseResultDataValue.success = tempBaseResultBool.success;
                baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                return baseResultDataValue;
            }

            //判断是否正在被盘库锁定
            if (this.Entity.Status.ToString() == ReaBmsInDocStatus.已入库.Key)
            {
                tempBaseResultBool = EditValidIsLockOfDtlVOList(this.Entity, dtAddList);
                if (tempBaseResultBool.success == false)
                {
                    baseResultDataValue.success = tempBaseResultBool.success;
                    baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                    return baseResultDataValue;
                }
            }
            if (baseResultDataValue.success)
                AddReaCheckInOperation(this.Entity, empID, empName);

            try
            {
                //入库的验收主单信息
                ReaBmsCenSaleDocConfirm docConfirm = IBReaBmsCenSaleDocConfirm.Get(docConfirmID);
                if (docConfirm.Status != int.Parse(ReaBmsCenSaleDocConfirmStatus.已验收.Key) && docConfirm.Status != int.Parse(ReaBmsCenSaleDocConfirmStatus.部分入库.Key))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = string.Format("本次入库的验收主单状态为{0}，不能入库！", ReaBmsCenSaleDocConfirmStatus.GetStatusDic()[docConfirm.Status.ToString()].Name);
                    return baseResultDataValue;
                }

                IList<ReaBmsCenSaleDtlConfirm> listDtlConfirm = null;
                listDtlConfirm = IBReaBmsCenSaleDtlConfirm.SearchListByHQL(string.Format("reabmscensaledtlconfirm.SaleDocConfirmID={0}", docConfirmID));
                //验收明细的状态为已验收及部分入库集合
                var tempListDtlConfirm = listDtlConfirm.Where(p => (p.Status.Value == int.Parse(ReaBmsCenSaleDtlConfirmStatus.已验收.Key) || p.Status.Value == int.Parse(ReaBmsCenSaleDtlConfirmStatus.部分入库.Key))).ToList();
                if (tempListDtlConfirm == null || tempListDtlConfirm.Count <= 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = string.Format("当前的入库信息不符合入库条件,入库的验收明细的状态必须为{0}或{1}!", ReaBmsCenSaleDtlConfirmStatus.GetStatusDic()[ReaBmsCenSaleDtlConfirmStatus.已验收.Key].Name, ReaBmsCenSaleDtlConfirmStatus.GetStatusDic()[ReaBmsCenSaleDtlConfirmStatus.部分入库.Key].Name);
                }
                baseResultDataValue = AddReaBmsInDocAndDtl(dtAddList, codeScanningMode, empID, empName);
                if (baseResultDataValue.success == true)
                {
                    BaseResultBool baseResultBool = EditDocConfirmAndDtl(dtAddList, docConfirm, listDtlConfirm, tempListDtlConfirm, empID, empName);
                    baseResultDataValue.success = baseResultBool.success;
                    baseResultDataValue.ErrorInfo = baseResultBool.ErrorInfo;
                }

            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = ex.Message;
                ZhiFang.Common.Log.Log.Error("验收入库错误.AddReaBmsInDocAndDtlOfVO:" + ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultBool EditReaBmsInDocOfConfirmStock(long id, string codeScanningMode, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            ReaBmsInDoc entity = this.Get(id);
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "入库单ID:" + id + ",的入库单信息为空，不能操作！";
                return tempBaseResultBool;
            }
            if (entity.Status.ToString() != ReaBmsInDocStatus.待继续入库.Key)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "入库单ID：" + this.Entity.Id + "的状态为：" + ReaBmsInDocStatus.GetStatusDic()[entity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }

            IList<ReaBmsInDtl> dtEditList = IBReaBmsInDtl.SearchListByHQL("reabmsindtl.InDocID=" + id);
            if (dtEditList == null || dtEditList.Count <= 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "入库单ID:" + id + ",的入库明细信息为空，不能操作！";
                return tempBaseResultBool;
            }

            tempBaseResultBool = IBReaBmsInDtl.EditReaBmsInDtlListOfValid(dtEditList, codeScanningMode);
            if (tempBaseResultBool.success == false)
                return tempBaseResultBool;

            //判断是否正在被盘库锁定
            tempBaseResultBool = EditValidIsLockOfDtlList(entity, dtEditList);
            if (tempBaseResultBool.success == false)
                return tempBaseResultBool;

            entity.UserID = empID;
            entity.UserName = empName;
            entity.Status = int.Parse(ReaBmsInDocStatus.已入库.Key);
            entity.OperDate = DateTime.Now;
            entity.StatusName = ReaBmsInDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
            entity.TotalPrice = dtEditList.Sum(p => p.SumTotal.Value);

            this.Entity = entity;
            tempBaseResultBool.success = this.Edit();
            if (tempBaseResultBool.success)
            {
                tempBaseResultBool = IBReaBmsInDtl.EditReaBmsInDtl(this.Entity, dtEditList, false, empID, empName);
                if (tempBaseResultBool.success)
                    AddReaCheckInOperation(this.Entity, empID, empName);
            }

            return tempBaseResultBool;
        }
        public BaseResultBool EditConfirmInDocByInterface(long id, string codeScanningMode, string iSNeedCreateBarCode, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            ReaBmsInDoc entity = this.Get(id);
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "入库单ID:" + id + ",的入库单信息为空，不能操作！";
                return tempBaseResultBool;
            }
            if (entity.Status.ToString() != ReaBmsInDocStatus.待继续入库.Key)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "入库单ID：" + this.Entity.Id + "的状态为：" + ReaBmsInDocStatus.GetStatusDic()[entity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }

            IList<ReaBmsInDtl> dtEditList = IBReaBmsInDtl.SearchListByHQL("reabmsindtl.InDocID=" + id);
            if (dtEditList == null || dtEditList.Count <= 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "入库单ID:" + id + ",的入库明细信息为空，不能操作！";
                return tempBaseResultBool;
            }

            tempBaseResultBool = IBReaBmsInDtl.EditReaBmsInDtlListOfValid(dtEditList, codeScanningMode);
            if (tempBaseResultBool.success == false)
                return tempBaseResultBool;

            //判断是否正在被盘库锁定
            tempBaseResultBool = EditValidIsLockOfDtlList(entity, dtEditList);
            if (tempBaseResultBool.success == false)
                return tempBaseResultBool;

            entity.UserID = empID;
            entity.UserName = empName;
            entity.Status = int.Parse(ReaBmsInDocStatus.已入库.Key);
            entity.OperDate = DateTime.Now;
            entity.StatusName = ReaBmsInDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
            entity.TotalPrice = dtEditList.Sum(p => p.SumTotal.Value);

            this.Entity = entity;
            tempBaseResultBool.success = this.Edit();
            if (tempBaseResultBool.success)
            {
                tempBaseResultBool = IBReaBmsInDtl.EditReaBmsInDtlByInterface(entity, dtEditList, false, iSNeedCreateBarCode, empID, empName);
                if (tempBaseResultBool.success)
                    AddReaCheckInOperation(this.Entity, empID, empName);
            }

            return tempBaseResultBool;
        }
        bool EditReaBmsInDocStatusCheck(ReaBmsInDoc entity, ReaBmsInDoc serverEntity, List<string> tmpa, long empID, string empName)
        {
            if (entity.Status.ToString() == ReaBmsInDocStatus.待继续入库.Key || entity.Status.ToString() == ReaBmsInDocStatus.已入库.Key)
            {
                if (serverEntity.Status.ToString() != ReaBmsInDocStatus.待继续入库.Key)
                {
                    return false;
                }
            }
            return true;
        }
        private BaseResultDataValue AddReaBmsInDocAndDtl(IList<ReaBmsInDtlVO> dtAddList, string codeScanningMode, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (this.Entity == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "entity为空，不能入库！";
                return baseResultDataValue;
            }
            if (!this.Entity.TotalPrice.HasValue)
                this.Entity.TotalPrice = dtAddList.Sum(p => p.ReaBmsInDtl.SumTotal.Value);
            this.Entity.Visible = true;
            this.Entity.InDocNo = this.GetInDocNo();
            this.Entity.CreaterID = empID;
            this.Entity.CreaterName = empName;
            this.Entity.OperDate = DateTime.Now;
            this.Entity.DataUpdateTime = DateTime.Now;
            this.Entity.UserID = empID;
            this.Entity.UserName = empName;
            this.Entity.StatusName = ReaBmsInDocStatus.GetStatusDic()[this.Entity.Status.ToString()].Name;
            if (this.Entity.InType.HasValue)
                this.Entity.InTypeName = ReaBmsInDocInType.GetStatusDic()[this.Entity.InType.Value.ToString()].Name;
            if (this.Add())
            {
                BaseResultBool baseResultBool = IBReaBmsInDtl.AddReaBmsInDtlOfVO(this.Entity, dtAddList, empID, empName);
                if (baseResultBool.success)
                    AddReaCheckInOperation(this.Entity, empID, empName);
                baseResultDataValue.success = baseResultBool.success;
                baseResultDataValue.ErrorInfo = baseResultBool.ErrorInfo;
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "保存入库主单信息失败!";
            }
            return baseResultDataValue;
        }
        private BaseResultDataValue AddReaBmsInDtlOfVOByInterface(IList<ReaBmsInDtlVO> dtAddList, string codeScanningMode, string iSNeedCreateBarCode, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (this.Entity == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "entity为空，不能入库！";
                return baseResultDataValue;
            }
            if (!this.Entity.TotalPrice.HasValue)
                this.Entity.TotalPrice = dtAddList.Sum(p => p.ReaBmsInDtl.SumTotal.Value);
            this.Entity.Visible = true;
            this.Entity.InDocNo = this.GetInDocNo();
            this.Entity.CreaterID = empID;
            this.Entity.CreaterName = empName;
            this.Entity.OperDate = DateTime.Now;
            this.Entity.DataUpdateTime = DateTime.Now;
            this.Entity.UserID = empID;
            this.Entity.UserName = empName;
            this.Entity.StatusName = ReaBmsInDocStatus.GetStatusDic()[this.Entity.Status.ToString()].Name;
            if (this.Entity.InType.HasValue)
                this.Entity.InTypeName = ReaBmsInDocInType.GetStatusDic()[this.Entity.InType.Value.ToString()].Name;
            if (this.Add())
            {
                BaseResultBool baseResultBool = IBReaBmsInDtl.AddReaBmsInDtlOfVOByInterface(this.Entity, dtAddList, iSNeedCreateBarCode, empID, empName);
                if (baseResultBool.success)
                    AddReaCheckInOperation(this.Entity, empID, empName);
                baseResultDataValue.success = baseResultBool.success;
                baseResultDataValue.ErrorInfo = baseResultBool.ErrorInfo;
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "保存入库主单信息失败!";
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 更新入库单对应的验收单
        /// </summary>
        /// <param name="dtAddList"></param>
        /// <param name="docConfirm"></param>
        /// <param name="listDtlConfirm"></param>
        /// <param name="tempListDtlConfirm"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        private BaseResultBool EditDocConfirmAndDtl(IList<ReaBmsInDtlVO> dtAddList, ReaBmsCenSaleDocConfirm docConfirm, IList<ReaBmsCenSaleDtlConfirm> listDtlConfirm, List<ReaBmsCenSaleDtlConfirm> tempListDtlConfirm, long empID, string empName)
        {
            //验收明细待更新的集合
            IList<ReaBmsCenSaleDtlConfirm> listEditDtlConfirm = new List<ReaBmsCenSaleDtlConfirm>();
            BaseResultBool baseResultBool = new BaseResultBool();
            foreach (var vo in dtAddList)
            {
                var model = vo.ReaBmsInDtl;
                ReaBmsCenSaleDtlConfirm dtlConfirm = listDtlConfirm.Where(p => p.Id == model.SaleDtlConfirmID.Value).ElementAt(0);
                double acceptCount = dtlConfirm.AcceptCount;

                IList<ReaBmsInDtl> listInDtl = IBReaBmsInDtl.SearchListByHQL(string.Format("reabmsindtl.SaleDtlConfirmID={0}", model.SaleDtlConfirmID.Value));
                double inCount = 0;
                //验收明细的已入库总数
                if (listInDtl.Count > 0) inCount = listInDtl.Sum(p => p.GoodsQty.Value);
                //验收明细的当次入库总数
                inCount = inCount + dtAddList.Where(p => p.ReaBmsInDtl.SaleDtlConfirmID.Value == model.SaleDtlConfirmID.Value).Sum(p => p.ReaBmsInDtl.GoodsQty.Value);

                if (inCount >= acceptCount)
                {
                    //当前验收明细从部分入库集合移除
                    if (tempListDtlConfirm != null && tempListDtlConfirm.Contains(dtlConfirm))
                        tempListDtlConfirm.Remove(dtlConfirm);

                    dtlConfirm.Status = int.Parse(ReaBmsCenSaleDtlConfirmStatus.全部入库.Key);
                    dtlConfirm.StatusName = ReaBmsCenSaleDtlConfirmStatus.GetStatusDic()[dtlConfirm.Status.ToString()].Name;
                }
                else if (inCount < acceptCount)
                {
                    dtlConfirm.Status = int.Parse(ReaBmsCenSaleDtlConfirmStatus.部分入库.Key);
                    dtlConfirm.StatusName = ReaBmsCenSaleDtlConfirmStatus.GetStatusDic()[dtlConfirm.Status.ToString()].Name;
                }
                dtlConfirm.InCount = (int)inCount;
                listEditDtlConfirm.Add(dtlConfirm);
            }

            foreach (ReaBmsCenSaleDtlConfirm dtEdit in listEditDtlConfirm)
            {
                IBReaBmsCenSaleDtlConfirm.Entity = dtEdit;
                baseResultBool.success = IBReaBmsCenSaleDtlConfirm.Edit();
                if (baseResultBool.success == false)
                {
                    baseResultBool.ErrorInfo = "入库操作更新验收明细失败!";
                    break;
                }
            }
            if (baseResultBool.success == false) return baseResultBool;

            int docOldStatus = docConfirm.Status;
            //验收主单更新处理
            if (tempListDtlConfirm != null && tempListDtlConfirm.Count > 0)
            {
                docConfirm.Status = int.Parse(ReaBmsCenSaleDocConfirmStatus.部分入库.Key);
                docConfirm.StatusName = ReaBmsCenSaleDocConfirmStatus.GetStatusDic()[docConfirm.Status.ToString()].Name;
            }
            else
            {
                docConfirm.Status = int.Parse(ReaBmsCenSaleDocConfirmStatus.全部入库.Key);
                docConfirm.StatusName = ReaBmsCenSaleDocConfirmStatus.GetStatusDic()[docConfirm.Status.ToString()].Name;
            }
            IBReaBmsCenSaleDocConfirm.Entity = docConfirm;
            baseResultBool.success = IBReaBmsCenSaleDocConfirm.Edit();
            IBReaBmsCenSaleDocConfirm.AddReaCheckInOperation(docConfirm, empID, empName);
            return baseResultBool;
        }
        /// <summary>
        /// 入库总单号
        /// </summary>
        /// <returns></returns>
        private string GetInDocNo()
        {
            StringBuilder strb = new StringBuilder();
            strb.Append(DateTime.Now.ToString("yyMMdd"));
            Random ran = new Random();
            int randKey = ran.Next(0, 999);
            strb.Append(randKey.ToString().PadLeft(3, '0'));//左补零
            strb.Append(DateTime.Now.ToString("HHmmssfff"));
            return strb.ToString();
        }
        public void AddReaCheckInOperation(ReaBmsInDoc entity, long empID, string empName)
        {
            if (entity.Status.ToString() == ReaBmsInDocStatus.待继续入库.Key) return;

            ReaCheckInOperation sco = new ReaCheckInOperation();
            sco.BobjectID = entity.Id;
            sco.CreatorID = empID;
            if (empName != null && empName.Trim() != "")
                sco.CreatorName = empName;
            sco.BusinessModuleCode = "ReaBmsInDoc";
            sco.IsUse = true;
            sco.Type = entity.Status;
            sco.TypeName = ReaBmsInDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
            IBReaCheckInOperation.Entity = sco;
            IBReaCheckInOperation.Add();
        }
        public BaseResultDataValue StatisticalAnalysisBmsInDocByGoodsNo(string startDate, string endDate, string cenOrgIdList, string goodsNoList, string comOrgIdList, string bmsInDocTypeList, string dateType, string cenOrgLevel)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                DateTime StartDateTime;
                if (DateTime.TryParse(startDate, out StartDateTime))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "开始时间错误！";
                    return brdv;
                }
                DateTime EndDateTime;
                if (DateTime.TryParse(endDate, out EndDateTime))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "结束时间错误！";
                    return brdv;
                }
                if (StartDateTime > EndDateTime)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "开始时间大于结束时间！";
                    return brdv;
                }
                ZhiFang.Common.Log.Log.Error("StatisticalAnalysisBmsInDocByGoodsNo.startDate:" + startDate + ",endDate:" + endDate + ",cenOrgIdList:" + cenOrgIdList + ",goodsNoList:" + goodsNoList + ",comOrgIdList:" + comOrgIdList + ",bmsInDocTypeList:" + bmsInDocTypeList + ",dateType:" + dateType + ",cenOrgLevel:" + cenOrgLevel);
                switch (dateType.Trim())
                {
                    case "0": break;
                    case "1": break;
                    case "2": break;
                    case "3": break;
                    case "4": break;
                    case "5": break;
                }
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "入库统计异常！";
                ZhiFang.Common.Log.Log.Error("StatisticalAnalysisBmsInDocByGoodsNo.异常：" + e.ToString());
                return brdv;
            }
        }

        public BaseResultDataValue AddReaBmsInDocByReturnReaGoods(int inputType, ref ReaBmsInDoc inDoc, IList<ReaBmsInDtl> listReaBmsInDtl, ref IList<ReaBmsOutDtl> listOutDtlOfIn, ref IList<ReaBmsInDtl> addInDtlList, ref IList<ReaBmsQtyDtl> addQtyDtlList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (string.IsNullOrEmpty(employeeID))
            {
                brdv.success = false;
                brdv.ErrorInfo = "无法获取当前用户的ID信息,请重新登录！";
                return brdv;
            }
            long empID = long.Parse(employeeID);
            string employeeName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            IList<ReaBmsOutDtl> listAllOutDtl = IBReaBmsOutDtl.SearchListByHQL(" reabmsoutdtl.OutDocID=" + inDoc.Id.ToString());

            if (inputType == 0)//整单入库
            {
                if (!(listAllOutDtl.Count > 0 && listAllOutDtl.Count == listReaBmsInDtl.Count))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = string.Format("退库入库：整单入库时货品明细数量不对！货品明细数量：{0}，要入库的货品明细数量：{1}", listAllOutDtl.Count, listReaBmsInDtl.Count);
                    return brdv;
                }
            }
            inDoc = GetReaBmsInDocByReaBmsOutDoc(inDoc, empID, employeeName);
            inDoc.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();

            double totalPrice = 0;
            //IList<ReaBmsInDtl> 
            if (addInDtlList == null)
                addInDtlList = new List<ReaBmsInDtl>();
            foreach (ReaBmsInDtl reaBmsInDtl in listReaBmsInDtl)
            {
                IList<ReaBmsOutDtl> listOutDtl = listAllOutDtl.Where(p => p.Id == reaBmsInDtl.Id).ToList();
                if (listOutDtl == null || listOutDtl.Count <= 0)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = string.Format("货品【{0}】入库信息获取失败！", reaBmsInDtl.GoodsCName);
                    return brdv;
                }
                if (inputType == 0)//整单入库
                {
                    if (!(reaBmsInDtl.GoodsQty > 0 && listOutDtl[0].GoodsQty == reaBmsInDtl.GoodsQty))
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = string.Format("退库入库：整单入库时货品数量不对！货品入库数量：{0}，要入库的货品数量：{1}", listAllOutDtl.Count, listReaBmsInDtl.Count);
                        return brdv;
                    }
                }
                else //部分入库
                {
                    if (!(reaBmsInDtl.GoodsQty > 0 && listOutDtl[0].GoodsQty >= reaBmsInDtl.GoodsQty))
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = string.Format("退库入库：部分入库时货品数量不对！货品入库数量：{0}，要入库的货品数量：{1}", listAllOutDtl.Count, listReaBmsInDtl.Count);
                        return brdv;
                    }
                }
                //退库接口的出库信息
                ReaBmsOutDtl mapperOutDtl = ClassMapperHelp.GetMapper<ReaBmsOutDtl, ReaBmsOutDtl>(listOutDtl[0]);
                mapperOutDtl.GoodsQty = reaBmsInDtl.GoodsQty.Value;
                mapperOutDtl.SumTotal = mapperOutDtl.GoodsQty * mapperOutDtl.Price;
                if (!listOutDtlOfIn.Contains(mapperOutDtl))
                    listOutDtlOfIn.Add(mapperOutDtl);
                ReaBmsInDtl inDtl = IBReaBmsInDtl.SearchReaBmsInDtlByReaBmsOutDtl(inDoc, listOutDtl[0], (double)reaBmsInDtl.GoodsQty, empID, employeeName);
                totalPrice += (inDtl.SumTotal == null ? 0 : (double)inDtl.SumTotal);
                addInDtlList.Add(inDtl);
            }//foreach

            ZhiFang.Common.Log.Log.Info("退库入库.入库总单号:" + inDoc.InDocNo);
            inDoc.TotalPrice = totalPrice;
            this.Entity = inDoc;
            if (!this.Add())
                throw new Exception("退库入库总单保存失败！");

            //当前机构所属机构码
            IList<CenOrg> cenOrgList = IDCenOrgDao.GetListByHQL("cenorg.LabID=" + inDoc.LabID.ToString());

            foreach (ReaBmsInDtl entity in addInDtlList)
            {
                entity.InDocID = inDoc.Id;
                IBReaBmsInDtl.Entity = entity;
                if (!IBReaBmsInDtl.Add())
                    throw new Exception("退库入库明细单保存失败！");
                else
                {
                    ReaBmsQtyDtl addQtyDtl = new ReaBmsQtyDtl();
                    BaseResultBool brb = IBReaBmsQtyDtl.AddReaBmsQtyDtl(entity, cenOrgList[0], long.Parse(ReaBmsQtyDtlOperationOperType.退库入库.Key), empID, employeeName, ref addQtyDtl);
                    addQtyDtlList.Add(addQtyDtl);
                    if (!brb.success)
                        throw new Exception("退库入库新增库存信息失败！");
                }
            }
            return brdv;
        }
        //, IList<ReaBmsInDtl> addInDtlList, IList<ReaBmsQtyDtl> addQtyDtlList
        public BaseResultBool DelReaBmsInDocByReturn(ReaBmsInDoc inDoc)
        {
            BaseResultBool brdv = new BaseResultBool();
            int counts = 0;

            //删除入库明细对应的库存盒条码集合信息
            counts = IBReaGoodsBarcodeOperation.DeleteByHql("From ReaGoodsBarcodeOperation reagoodsbarcodeoperation where reagoodsbarcodeoperation.BDocID=" + inDoc.Id);
            ZhiFang.Common.Log.Log.Info("退库入库调用退库接口失败.删除入库明细对应的库存盒条码集合信息.返回执行记录数:" + counts);

            //删除库存记录对应的库存变化操作记录信息
            counts = 0;
            counts = IBReaBmsQtyDtlOperation.DeleteByHql("From ReaBmsQtyDtlOperation reabmsqtydtloperation where reabmsqtydtloperation.BDocID =" + inDoc.Id);
            ZhiFang.Common.Log.Log.Info("退库入库调用退库接口失败.删除库存记录对应的库存变化操作记录信息.返回执行记录数:" + counts);

            //删除入库明细对应的库存记录集合信息
            counts = 0;
            counts = IBReaBmsQtyDtl.DeleteByHql("From ReaBmsQtyDtl reabmsqtydtl where reabmsqtydtl.InDocNo ='" + inDoc.InDocNo + "'");
            ZhiFang.Common.Log.Log.Info("退库入库调用退库接口失败.删除入库明细对应的库存记录集合信息.返回执行记录数:" + counts);

            //删除入库明细记录信息
            counts = 0;
            counts = IBReaBmsInDtl.DeleteByHql("From ReaBmsInDtl reabmsindtl where reabmsindtl.InDocID =" + inDoc.Id);
            ZhiFang.Common.Log.Log.Info("退库入库调用退库接口失败.删除入库明细记录信息.返回执行记录数:" + counts);

            //删除入库操作记录信息
            counts = 0;
            counts = IBReaCheckInOperation.DeleteByHql("From ReaCheckInOperation reacheckInoperation where reacheckInoperation.BobjectID =" + inDoc.Id);
            ZhiFang.Common.Log.Log.Info("退库入库调用退库接口失败.删除入库操作记录信息.返回执行记录数:" + counts);

            //删除入库主单信息
            counts = 0;
            counts = this.DeleteByHql("From ReaBmsInDoc reabmsindoc where reabmsindoc.Id =" + inDoc.Id);
            ZhiFang.Common.Log.Log.Info("退库入库调用退库接口失败.删除入库主单信息.返回执行记录数:" + counts);

            return brdv;
        }
        private ReaBmsInDoc GetReaBmsInDocByReaBmsOutDoc(ReaBmsInDoc inDoc, long empID, string empName)
        {
            if (inDoc == null)
                inDoc = new ReaBmsInDoc();
            //inDoc.ReaCenOrg = outDoc
            if (string.IsNullOrEmpty(inDoc.InDocNo))
                inDoc.InDocNo = this.GetInDocNo();
            //inDoc.CompanyID = outDoc
            //inDoc.CompanyName
            //inDoc.Carrier
            inDoc.UserID = empID;
            inDoc.UserName = empName;
            inDoc.OperDate = DateTime.Now;
            //inDoc.InvoiceNo = outDoc     
            inDoc.SourceType = long.Parse(ReaBmsInSourceType.退库入库.Key);
            inDoc.InType = long.Parse(ReaBmsInDocInType.退库入库.Key);
            inDoc.InTypeName = ReaBmsInDocInType.退库入库.Value.Name;
            inDoc.Status = int.Parse(ReaBmsInDocStatus.已入库.Key);
            inDoc.StatusName = ReaBmsInDocStatus.已入库.Value.Name;
            //inDoc.PrintTimes
            //inDoc.TotalPrice
            //inDoc.ZX1
            //inDoc.ZX2
            //inDoc.ZX3
            //inDoc.DispOrder
            //inDoc.Memo
            inDoc.Visible = true;
            inDoc.CreaterID = empID;
            inDoc.CreaterName = empName;
            inDoc.DataAddTime = DateTime.Now;
            inDoc.DataUpdateTime = DateTime.Now;
            return inDoc;
        }

        #region 入库撤消
        public BaseResultBool EditCancelReaBmsInDocById(long id, long empID, string empNam)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            ReaBmsInDoc inDoc = this.Get(id);
            if (inDoc == null)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "入库单ID为:" + id + ",不存在!";
                return baseResultBool;
            }
            if (inDoc.Status.ToString() != ReaBmsInDocStatus.已入库.Key)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "入库单ID为:" + id + ",当前状态为:+" + ReaBmsInDocStatus.GetStatusDic()[inDoc.Status.ToString()].Name + "!";
                return baseResultBool;
            }

            IList<ReaBmsInDtl> inDtlList = IBReaBmsInDtl.SearchListByHQL("reabmsindtl.InDocID=" + inDoc.Id);
            if (inDtlList.Count <= 0)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "入库单ID为:" + id + ",入库明细信息为空!";

                inDoc.Status = int.Parse(ReaBmsInDocStatus.待继续入库.Key);
                inDoc.StatusName = ReaBmsInDocStatus.待继续入库.Value.Name;
                this.Entity = inDoc;
                this.Edit();

                return baseResultBool;
            }

            foreach (ReaBmsInDtl inDtl in inDtlList)
            {
                IList<ReaBmsQtyDtl> qtyDtlList = IBReaBmsQtyDtl.SearchListByHQL("reabmsqtydtl.InDtlID=" + inDtl.Id);

                //库存货品是否有入库明细记录

                //库存货品是否有移库记录

                //库存货品是否已进行过库存结转记录

                //库存货品的条码是否已被打印过

            }

            return baseResultBool;
        }
        #endregion
        #region PDF清单及统计
        public Stream SearchPdfReportOfTypeById(string reaReportClass, long id, long labID, string labCName, string breportType, string frx, ref string pdfFileName)
        {
            Stream stream = null;
            ReaBmsInDoc outDoc = this.Get(id);
            if (outDoc == null)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取入库单PDF清单数据信息为空!");
            }
            IList<ReaBmsInDtl> dtlList = IBReaBmsInDtl.SearchListByHQL("reabmsindtl.InDocID=" + id);
            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取入库单PDF清单明细信息为空!");
            }
            SearchReaGoodsOfDtl(dtlList);
            pdfFileName = outDoc.InDocNo + ".pdf";
            if (reaReportClass == ReaReportClass.Frx.Key)
            {
                stream = CreatePdfReportOfFrxById(outDoc, dtlList, frx);
            }
            else if (reaReportClass == ReaReportClass.Excel.Key)
            {
                string excelFileFullDir = "";
                //获取入库单模板
                if (string.IsNullOrEmpty(frx))
                    frx = "入库清单.xlsx";
                string fileExt = frx.Substring(frx.LastIndexOf("."));
                string excelFile = outDoc.InDocNo.ToString() + fileExt;
                Stream stream2 = null;
                ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
                stream2 = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsInDoc, ReaBmsInDtl>(outDoc, dtlList, excelCommand, breportType, outDoc.LabID, frx, excelFile, ref excelFileFullDir);
                stream2.Close();
                string pdfFullDir = "";

                bool result = ExcelToPdfReportHelp.ExcelToPDF(excelFileFullDir, breportType, outDoc.LabID, pdfFileName, ref pdfFullDir);
                if (result)
                {
                    stream = PdfReportHelp.GetReportPDF(pdfFullDir);
                }
            }
            return stream;
        }
        private Stream CreatePdfReportOfFrxById(ReaBmsInDoc outDoc, IList<ReaBmsInDtl> dtlList, string frx)
        {
            Stream stream = null;
            DataSet dataSet = new DataSet();
            dataSet.DataSetName = "ZhiFang.ReagentSys.Client";

            List<ReaBmsInDoc> docList = new List<ReaBmsInDoc>();
            docList.Add(outDoc);
            DataTable docDt = ReportBTemplateHelp.ToDataTable<ReaBmsInDoc>(docList, null);
            docDt.TableName = "Rea_BmsInDoc";
            dataSet.Tables.Add(docDt);

            DataTable dtDtl = ReportBTemplateHelp.ToDataTable<ReaBmsInDtl>(dtlList, null);
            if (dtDtl != null)
            {
                dtDtl.TableName = "Rea_BmsInDtl";
                dataSet.Tables.Add(dtDtl);
            }
            //获取入库单Frx模板
            //string parentPath = ReportBTemplateHelp.GetSaveBTemplatePath(this.Entity.LabID, "入库清单");
            string pdfName = outDoc.InDocNo.ToString() + ".pdf";
            //如果当前实验室还没有维护入库单报表模板,默认使用公共的入库单模板
            if (string.IsNullOrEmpty(frx))
                frx = "入库清单.frx";
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            ExcelReportHelp.CreateEECDataTable(excelCommand, ref dataSet);
            stream = FrxToPdfReportHelp.SavePdfReport(dataSet, outDoc.LabID, pdfName, FrxToPdfReportHelp.PublicTemplateDir, BTemplateType.GetStatusDic()[BTemplateType.入库清单.Key].Name, frx, false);

            return stream;
        }
        public Stream SearchExcelReportOfExcelById(long id, long labID, string labCName, string breportType, string frx, ref string fileName)
        {
            Stream stream = null;
            ReaBmsInDoc orderDoc = this.Get(id);
            if (orderDoc == null)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取入库单数据信息为空!");
            }
            IList<ReaBmsInDtl> dtlList = IBReaBmsInDtl.SearchListByHQL("reabmsindtl.InDocID=" + id);
            if (dtlList == null || dtlList.Count <= 0)
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取入库单明细信息为空!");
            }
            SearchReaGoodsOfDtl(dtlList);
            //获取入库单模板
            if (string.IsNullOrEmpty(frx))
                frx = "入库清单.xlsx";
            string saveFullPath = "";
            string fileExt = frx.Substring(frx.LastIndexOf("."));
            string excelFile = orderDoc.InDocNo.ToString() + fileExt;
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaBmsInDoc, ReaBmsInDtl>(orderDoc, dtlList, excelCommand, breportType, orderDoc.LabID, frx, excelFile, ref saveFullPath);
            fileName = "入库清单信息" + fileExt;
            return stream;
        }
        private void SearchReaGoodsOfDtl(IList<ReaBmsInDtl> dtlList)
        {
            for (int i = 0; i < dtlList.Count; i++)
            {
                dtlList[i].Purpose = dtlList[i].ReaGoods.Purpose;
                dtlList[i].StorageType = dtlList[i].ReaGoods.StorageType;
                dtlList[i].UnitMemo = dtlList[i].ReaGoods.UnitMemo;
                dtlList[i].ProdOrgName = dtlList[i].ReaGoods.ProdOrgName;
                dtlList[i].NetGoodsNo = dtlList[i].ReaGoods.NetGoodsNo;
                dtlList[i].RegisterNo = (string.IsNullOrWhiteSpace(dtlList[i].RegisterNo) ? dtlList[i].ReaGoods.RegistNo : dtlList[i].RegisterNo);
                dtlList[i].GoodSName = dtlList[i].ReaGoods.SName;
            }
        }
        #endregion
        public EntityList<ReaBmsInDoc> SearchListByDocAndDtlHQL(string docHql, string dtlHql, string order, int page, int count)
        {
            EntityList<ReaBmsInDoc> entityList = new EntityList<ReaBmsInDoc>();
            if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
                return entityList;

            //IList<ReaBmsInDtl> dtlList = new List<ReaBmsInDtl>();
            entityList = ((IDReaBmsInDocDao)base.DBDao).SearchListByDocAndDtlHQL(docHql, dtlHql, order, page, count);

            #region 供货方信息处理
            for (int i = 0; i < entityList.list.Count; i++)
            {
                //供货验收单ID
                if (entityList.list[i].SaleDocConfirmID.HasValue)
                {
                    ReaBmsCenSaleDocConfirm saleDocConfirm = IBReaBmsCenSaleDocConfirm.Get(entityList.list[i].SaleDocConfirmID.Value);
                    if (saleDocConfirm != null)
                    {
                        entityList.list[i].ReaCompID = saleDocConfirm.ReaCompID;
                        entityList.list[i].ReaCompCode = saleDocConfirm.ReaCompCode;
                        entityList.list[i].ReaCompanyName = saleDocConfirm.ReaCompanyName;
                    }
                }
                else if (entityList.list[i].SaleDocID.HasValue)
                {
                    ReaBmsCenSaleDoc saleDoc = IDReaBmsCenSaleDocDao.Get(entityList.list[i].SaleDocID.Value);
                    if (saleDoc != null)
                    {
                        entityList.list[i].ReaCompID = saleDoc.ReaCompID;
                        entityList.list[i].ReaCompCode = saleDoc.ReaCompCode;
                        entityList.list[i].ReaCompanyName = saleDoc.ReaCompanyName;
                    }
                }
            }
            #endregion

            //dtlList = SearchReaBmsOutDtlListOfGroupBy1(dtlList);
            //entityList.count = dtlList.Count;
            ////分页处理
            //if (limit > 0 && limit < dtlList.Count)
            //{
            //    int startIndex = limit * (page - 1);
            //    int endIndex = limit;
            //    var list = dtlList.Skip(startIndex).Take(endIndex);
            //    if (list != null)
            //        dtlList = list.ToList();
            //}
            //entityList.list = dtlList;
            return entityList;
        }

        public bool GetInDocInfoByOutDtl(long outDtlID, ref ReaBmsInDoc reaBmsInDoc, ref ReaBmsInDtl reaBmsInDtl)
        {
            ReaBmsOutDtl outDtl = IBReaBmsOutDtl.Get(outDtlID);
            if (outDtl == null || (string.IsNullOrEmpty(outDtl.QtyDtlID)))
                return false;
            ZhiFang.Common.Log.Log.Info("outDtlID：" + outDtlID.ToString() + "，QtyDtlID：" + outDtl.QtyDtlID);
            IList<string> list = outDtl.QtyDtlID.Split(',').ToList();
            foreach (string dtlID in list)
            {
                ReaBmsQtyDtl qtyDtl = IBReaBmsQtyDtl.Get(long.Parse(dtlID));
                if (qtyDtl == null)
                    return false;
                reaBmsInDtl = IBReaBmsInDtl.Get((long)qtyDtl.InDtlID);
                if (reaBmsInDtl == null)
                    return false;
                reaBmsInDoc = this.Get(reaBmsInDtl.InDocID);
                if ((!string.IsNullOrWhiteSpace(reaBmsInDoc.OtherDocNo)) && (!string.IsNullOrWhiteSpace(reaBmsInDtl.ZX3)))
                    break;
            }
            return true;
        }

        public bool GetInDocInfoByOutDtl(ReaBmsOutDtl outDtl, ref ReaBmsInDoc reaBmsInDoc, ref ReaBmsInDtl reaBmsInDtl)
        {
            ZhiFang.Common.Log.Log.Info("outDtlID：" + outDtl.Id.ToString() + "，QtyDtlID：" + outDtl.QtyDtlID);
            IList<string> list = outDtl.QtyDtlID.Split(',').ToList();
            foreach (string dtlID in list)
            {
                ReaBmsQtyDtl qtyDtl = IBReaBmsQtyDtl.Get(long.Parse(dtlID));
                if (qtyDtl == null)
                    return false;
                reaBmsInDtl = IBReaBmsInDtl.Get((long)qtyDtl.InDtlID);
                if (reaBmsInDtl == null)
                    return false;
                reaBmsInDoc = this.Get(reaBmsInDtl.InDocID);
                if ((!string.IsNullOrWhiteSpace(reaBmsInDoc.OtherDocNo)) && (!string.IsNullOrWhiteSpace(reaBmsInDtl.ZX3)))
                    break;
            }
            return true;
        }

        public EntityList<ReaBmsInDoc> SearchReaBmsInDocOfQtyGEZeroByJoinHql(string docHql, string dtlHql, string reaGoodsHql, string sort, int page, int limit)
        {
            EntityList<ReaBmsInDoc> entityList = new EntityList<ReaBmsInDoc>();
            entityList = ((IDReaBmsInDocDao)base.DBDao).SearchReaBmsInDocOfQtyGEZeroByJoinHql(docHql, dtlHql, reaGoodsHql, sort, page, limit);
            return entityList;
        }

        public BaseResultDataValue GetInterfaceNo(string goodsBarCode, string goodsNo, string goodsBatNo, ref string otherDocNo, ref string otherBatNo, ref string otherDtlNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            IList<ReaBmsInDtl> listReaBmsInDtl = IBReaBmsInDtl.SearchListByHQL("reabmsindtl.GoodsSerial=\'" + goodsBarCode +
                "\' and reabmsindtl.ZX3 is not null");
            if (listReaBmsInDtl != null && listReaBmsInDtl.Count > 0)
            {
                listReaBmsInDtl = listReaBmsInDtl.OrderByDescending(p => p.DataAddTime).ToList();
                otherBatNo = listReaBmsInDtl[0].ZX3;
                otherDtlNo = listReaBmsInDtl[0].OtherDtlNo;
                ReaBmsInDoc inDoc = this.Get(listReaBmsInDtl[0].InDocID);
                if (inDoc != null)
                {
                    otherDocNo = inDoc.OtherDocNo;
                }
            }
            return brdv;
        }
    }
}