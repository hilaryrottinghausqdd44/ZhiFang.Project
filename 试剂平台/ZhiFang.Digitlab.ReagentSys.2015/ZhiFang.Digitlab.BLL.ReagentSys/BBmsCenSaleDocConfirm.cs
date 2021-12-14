using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.IBLL.Business;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Common.Public;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaConfirm;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BBmsCenSaleDocConfirm : BaseBLL<BmsCenSaleDocConfirm>, ZhiFang.Digitlab.IBLL.ReagentSys.IBBmsCenSaleDocConfirm
    {
        IBBSampleOperate IBBSampleOperate { get; set; }
        IBLL.ReagentSys.IBBmsCenSaleDoc IBBmsCenSaleDoc { get; set; }
        IBLL.ReagentSys.IBBmsCenSaleDtl IBBmsCenSaleDtl { get; set; }
        IBLL.ReagentSys.IBBmsCenSaleDtlConfirm IBBmsCenSaleDtlConfirm { get; set; }

        IDAO.ReagentSys.IDBmsCenOrderDtlDao IDBmsCenOrderDtlDao { get; set; }
        IBLL.ReagentSys.IBBmsCenOrderDoc IBBmsCenOrderDoc { get; set; }
        IBLL.ReagentSys.IBReaCheckInOperation IBReaCheckInOperation { get; set; }
        IBLL.RBAC.IBHRDept IBHRDept { get; set; }
        public BaseResultDataValue AddBmsCenSaleDocConfirm(IList<BmsCenSaleDtlConfirm> listEntity, string secAccepterID, string secAccepterName, string accepterMemo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (listEntity != null && listEntity.Count > 0)
                {
                    BmsCenSaleDoc saleDoc = null;
                    bool IsAcceptError = false;
                    BmsCenSaleDocConfirm saleDocConfirm = new BmsCenSaleDocConfirm();
                    foreach (BmsCenSaleDtlConfirm saleDtlConfirm in listEntity)
                    {
                        BmsCenSaleDtl saleDtl = IBBmsCenSaleDtl.Get(saleDtlConfirm.BmsCenSaleDtl.Id);
                        if (saleDtlConfirm.AcceptCount <= 0 && saleDtlConfirm.RefuseCount <= 0)
                            throw new Exception("试剂【" + saleDtl.Goods.CName + "】的验收数量和拒收数量都为零，不能验收！");
                        if (saleDtl.GoodsQty < saleDtlConfirm.AcceptCount)
                            throw new Exception("试剂【" + saleDtl.Goods.CName + "】的验收数量大于总量，不能验收！");
                        if (saleDtl.GoodsQty < (saleDtl.AcceptCount + saleDtlConfirm.AcceptCount))
                            throw new Exception("试剂【" + saleDtl.Goods.CName + "】的验收总数量大于总量，不能验收！");
                        if (saleDtl.GoodsQty < saleDtlConfirm.RefuseCount)
                            throw new Exception("试剂【" + saleDtl.Goods.CName + "】的拒收数量大于总量，不能验收！");
                        if (saleDtl.GoodsQty < (saleDtl.RefuseCount + saleDtlConfirm.RefuseCount))
                            throw new Exception("试剂【" + saleDtl.Goods.CName + "】的拒收总数量大于总量，不能验收！");
                        if (saleDtl.GoodsQty < (saleDtlConfirm.AcceptCount + saleDtlConfirm.RefuseCount))
                            throw new Exception("试剂【" + saleDtl.Goods.CName + "】的验收和拒收数量大于总量，不能验收！");
                        if (saleDtl.GoodsQty < (saleDtl.AcceptCount + saleDtl.RefuseCount + saleDtlConfirm.AcceptCount + saleDtlConfirm.RefuseCount))
                            throw new Exception("试剂【" + saleDtl.Goods.CName + "】的验收和拒收总数量大于总量，不能验收！");
                        if (saleDoc == null)
                            saleDoc = saleDtl.BmsCenSaleDoc;
                        saleDtlConfirm.BmsCenSaleDocConfirm = saleDocConfirm;
                        saleDtlConfirm.SaleDocConfirmNo = saleDoc.SaleDocNo;
                        _bmsCenSaleDtlConfirmCopy(saleDtlConfirm, saleDtl);
                        if (saleDtlConfirm.RefuseCount > 0)
                            IsAcceptError = true;
                        IBBmsCenSaleDtlConfirm.Entity = saleDtlConfirm;
                        if (IBBmsCenSaleDtlConfirm.Add())
                        {
                            IBBSampleOperate.AddObjectOperate(saleDtlConfirm, saleDtlConfirm.GetType().Name, "AddEntity", "新增实体操作");
                            saleDtl.AcceptCount += saleDtlConfirm.AcceptCount;
                            saleDtl.RefuseCount += saleDtlConfirm.RefuseCount;
                            if (saleDtl.GoodsQty == (saleDtl.AcceptCount + saleDtl.RefuseCount))
                                saleDtl.AcceptFlag = 1;
                            IBBmsCenSaleDtl.Entity = saleDtl;
                            if (IBBmsCenSaleDtl.Edit())
                                IBBSampleOperate.AddObjectOperate(saleDtl, saleDtl.GetType().Name, "EditEntity", "修改实体操作");
                        }
                        else
                            throw new Exception("新增供货验收明细单失败！");
                    }
                    saleDocConfirm.IsAcceptError = IsAcceptError;
                    _bmsCenSaleDocConfirmCopy(saleDocConfirm, saleDoc, secAccepterID, secAccepterName, accepterMemo);
                    this.Entity = saleDocConfirm;
                    if (this.Add())
                    {
                        IBBSampleOperate.AddObjectOperate(saleDocConfirm, saleDocConfirm.GetType().Name, "AddEntity", "新增实体操作");
                        IBBmsCenSaleDoc.EidtSaleDocAcceptFlag(saleDoc.Id);
                    }
                    else
                        throw new Exception("新增供货验收单失败！");
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：验收明细列表参数不能为空！";
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

        private void _bmsCenSaleDtlConfirmCopy(BmsCenSaleDtlConfirm saleDtlConfirm, BmsCenSaleDtl saleDtl)
        {
            saleDtlConfirm.BmsCenSaleDtl = saleDtl;
            saleDtlConfirm.SaleDtlConfirmNo = saleDtl.SaleDtlNo;
            saleDtlConfirm.Goods = saleDtl.Goods;
            saleDtlConfirm.Prod = saleDtl.Prod;
            saleDtlConfirm.ProdGoodsNo = saleDtl.ProdGoodsNo;
            saleDtlConfirm.ProdOrgName = saleDtl.ProdOrgName;
            saleDtlConfirm.GoodsName = saleDtl.GoodsName;
            saleDtlConfirm.GoodsUnit = saleDtl.GoodsUnit;
            saleDtlConfirm.UnitMemo = saleDtl.UnitMemo;
            saleDtlConfirm.StorageType = saleDtl.StorageType;
            saleDtlConfirm.TempRange = saleDtl.TempRange;
            saleDtlConfirm.GoodsQty = (int)saleDtl.GoodsQty;
            saleDtlConfirm.Price = saleDtl.Price;
            saleDtlConfirm.SumTotal = saleDtl.SumTotal;
            saleDtlConfirm.TaxRate = saleDtl.TaxRate;
            saleDtlConfirm.LotNo = saleDtl.LotNo;
            saleDtlConfirm.ProdDate = saleDtl.ProdDate;
            saleDtlConfirm.InvalidDate = saleDtl.InvalidDate;
            saleDtlConfirm.BiddingNo = saleDtl.BiddingNo;
            saleDtlConfirm.ApproveDocNo = saleDtl.ApproveDocNo;
            saleDtlConfirm.RegisterNo = saleDtl.RegisterNo;
            saleDtlConfirm.RegisterInvalidDate = saleDtl.RegisterInvalidDate;
            saleDtlConfirm.GoodsSerial = saleDtl.GoodsSerial;
            //saleDtlConfirm.PackSerial = saleDtl.PackSerial;
            saleDtlConfirm.LotSerial = saleDtl.LotSerial;
            //saleDtlConfirm.MixSerial = saleDtl.MixSerial;
            saleDtlConfirm.DataUpdateTime = DateTime.Now;
            saleDtlConfirm.DataAddTime = DateTime.Now;
        }

        private void _bmsCenSaleDocConfirmCopy(BmsCenSaleDocConfirm saleDocConfirm, BmsCenSaleDoc saleDoc, string secAccepterID, string secAccepterName, string accepterMemo)
        {
            saleDocConfirm.BmsCenSaleDoc = saleDoc;
            saleDocConfirm.SaleDocConfirmNo = saleDoc.SaleDocNo;
            saleDocConfirm.SaleDocNo = saleDoc.SaleDocNo;
            saleDocConfirm.Comp = saleDoc.Comp;
            saleDocConfirm.CompanyName = saleDoc.CompanyName;
            saleDocConfirm.Lab = saleDoc.Lab;
            saleDocConfirm.LabName = saleDoc.LabName;
            saleDocConfirm.Status = 1;//1为验收状态 
            if (!string.IsNullOrEmpty(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)))
                saleDocConfirm.AccepterID = Int64.Parse(SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
            saleDocConfirm.AccepterName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            saleDocConfirm.AcceptTime = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(secAccepterID))
            {
                saleDocConfirm.SecAccepterID = long.Parse(secAccepterID);
                saleDocConfirm.SecAccepterName = secAccepterName;
            }
            if (!string.IsNullOrWhiteSpace(accepterMemo))
                saleDocConfirm.AcceptMemo = accepterMemo;
            saleDocConfirm.DataUpdateTime = DateTime.Now;
            saleDocConfirm.DataAddTime = DateTime.Now;
        }

        #region 客户端验收

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
        public BaseResultDataValue AddReaSaleDocConfirmOfManualInput(BmsCenSaleDocConfirm entity, IList<BmsCenSaleDtlConfirmVO> dtAddList, int secAccepterType, string codeScanningMode, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            try
            {
                if (dtAddList != null && dtAddList.Count > 0)
                {
                    if (entity.Status == 0)
                        entity.Status = int.Parse(BmsCenSaleDocConfirmStatus.待继续验收.Key);
                    entity.StatusName = BmsCenSaleDocConfirmStatus.GetStatusDic()[entity.Status.ToString()].Name;
                    if (!entity.AcceptTime.HasValue)
                        entity.AcceptTime = DateTime.Now;
                    if (!entity.AccepterID.HasValue || string.IsNullOrEmpty(entity.AccepterName))
                    {
                        entity.AccepterID = empID;
                        entity.AccepterName = empName;
                    }
                    BaseResultBool tempBaseResultBool = IBBmsCenSaleDtlConfirm.AddDtlConfirmOfList(entity, dtAddList, codeScanningMode, empID, empName);
                    baseResultDataValue.success = tempBaseResultBool.success;
                    baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                    if (baseResultDataValue.success)
                    {
                        if (string.IsNullOrEmpty(entity.SaleDocConfirmNo))
                            entity.SaleDocConfirmNo = this.GetSaleDocConfirmNo();
                        bool IsAcceptError = dtAddList.Sum(p => p.BmsCenSaleDtlConfirm.RefuseCount) > 0 ? true : false;
                        entity.IsAcceptError = IsAcceptError;
                        this.Entity = entity;
                        if (this.Add())
                            AddReaCheckInOperation(entity, empID, empName);
                        else
                            throw new Exception("新增验收单失败！");
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：验收明细列表参数不能为空！";
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
        public BaseResultBool EditReaSaleDocConfirmOfManualInput(string[] tempArray, IList<BmsCenSaleDtlConfirmVO> dtAddList, IList<BmsCenSaleDtlConfirmVO> dtEditList, int secAccepterType, string codeScanningMode, long empID, string empName, string fieldsDtl)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            BmsCenSaleDocConfirm serverEntity = this.Get(this.Entity.Id);
            List<string> tmpa = tempArray.ToList();
            if (!EditBmsCenSaleDocConfirmStatusCheck(this.Entity, serverEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "验收单ID：" + this.Entity.Id + "的状态为：" + BmsCenSaleDtlConfirmStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }
            bool IsAcceptError = false;
            if (dtAddList != null && dtAddList.Count > 0) IsAcceptError = dtAddList.Sum(p => p.BmsCenSaleDtlConfirm.RefuseCount) > 0 ? true : false;
            if (IsAcceptError == false && dtEditList != null && dtEditList.Count > 0) IsAcceptError = dtEditList.Sum(p => p.BmsCenSaleDtlConfirm.RefuseCount) > 0 ? true : false;
            tmpa.Add("IsAcceptError=" + IsAcceptError);
            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                if (dtAddList != null && dtAddList.Count > 0)
                    tempBaseResultBool = IBBmsCenSaleDtlConfirm.AddDtlConfirmOfList(this.Entity, dtAddList, codeScanningMode, empID, empName);
                //验收明细更新
                if (tempBaseResultBool.success == true && dtEditList != null && dtEditList.Count > 0)
                    tempBaseResultBool = IBBmsCenSaleDtlConfirm.EditDtlConfirmOfList(this.Entity, dtEditList, fieldsDtl, codeScanningMode, empID, empName);
            }
            else
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "验收单ID：" + this.Entity.Id + "更新失败！";
                return tempBaseResultBool;
            }
            if (tempBaseResultBool.success == true && this.Entity.Status != serverEntity.Status)
            {
                BmsCenSaleDocConfirm tempEntity = new BmsCenSaleDocConfirm();
                tempEntity.Id = this.Entity.Id;
                tempEntity.Status = this.Entity.Status;
                tempEntity.StatusName = BmsCenSaleDocConfirmStatus.GetStatusDic()[tempEntity.Status.ToString()].Name;
                if (tempBaseResultBool.success) AddReaCheckInOperation(tempEntity, empID, empName);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditConfirmOfManualInput(string[] tempArray, int secAccepterType, string codeScanningMode, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            BmsCenSaleDocConfirm serverEntity = this.Get(this.Entity.Id);
            List<string> tmpa = tempArray.ToList();
            if (!EditBmsCenSaleDocConfirmStatusCheck(this.Entity, serverEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "验收单ID：" + this.Entity.Id + "的状态为：" + BmsCenSaleDtlConfirmStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }


            IList<BmsCenSaleDtlConfirm> dtlList = IBBmsCenSaleDtlConfirm.SearchListByHQL(String.Format("bmscensaledtlconfirm.BmsCenSaleDocConfirm.Id={0}", this.Entity.Id));
            if (dtlList == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "验收单ID：" + this.Entity.Id + "的验收明细信息为空！";
                return tempBaseResultBool;
            }

            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                //验收明细更新
                foreach (BmsCenSaleDtlConfirm saleDtlConfirm in dtlList)
                {
                    IBBmsCenSaleDtlConfirm.Entity = saleDtlConfirm;
                    List<string> tempDtlArray = new List<string>();
                    tempDtlArray.Add("Id=" + saleDtlConfirm.Id + " ");
                    tempDtlArray.Add("Status=" + BmsCenSaleDtlConfirmStatus.已验收.Key + " ");
                    tempBaseResultBool.success = IBBmsCenSaleDtlConfirm.Update(tempDtlArray.ToArray());
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
                BmsCenSaleDocConfirm tempEntity = new BmsCenSaleDocConfirm();
                tempEntity.Id = this.Entity.Id;
                tempEntity.Status = this.Entity.Status;
                tempEntity.StatusName = BmsCenSaleDocConfirmStatus.GetStatusDic()[tempEntity.Status.ToString()].Name;
                if (tempBaseResultBool.success) AddReaCheckInOperation(tempEntity, empID, empName);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool EditReaSaleDocConfirmAndDtl(string[] tempArray, int secAccepterType, string codeScanningMode, string fieldsDtl, long empID, string empName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            BmsCenSaleDocConfirm serverEntity = this.Get(this.Entity.Id);
            List<string> tmpa = tempArray.ToList();
            if (!EditBmsCenSaleDocConfirmStatusCheck(this.Entity, serverEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "验收单ID：" + this.Entity.Id + "的状态为：" + BmsCenSaleDtlConfirmStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }
            tempArray = tmpa.ToArray();
            if (this.Update(tempArray))
            {
                //验收明细更新
                if (this.Entity.BmsCenSaleDtlConfirmList != null)
                {
                    foreach (BmsCenSaleDtlConfirm saleDtlConfirm in this.Entity.BmsCenSaleDtlConfirmList)
                    {
                        IBBmsCenSaleDtlConfirm.Entity = saleDtlConfirm;
                        string[] tempDtlArray = ServiceCommon.CommonServiceMethod.GetUpdateFieldValueStr(IBBmsCenSaleDtlConfirm.Entity, fieldsDtl);
                        tempBaseResultBool.success = IBBmsCenSaleDtlConfirm.Update(tempDtlArray);
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
                BmsCenSaleDocConfirm tempEntity = new BmsCenSaleDocConfirm();
                tempEntity.Id = this.Entity.Id;
                tempEntity.Status = this.Entity.Status;
                tempEntity.StatusName = BmsCenSaleDocConfirmStatus.GetStatusDic()[tempEntity.Status.ToString()].Name;
                if (tempBaseResultBool.success) AddReaCheckInOperation(tempEntity, empID, empName);
            }
            return tempBaseResultBool;
        }
        bool EditBmsCenSaleDocConfirmStatusCheck(BmsCenSaleDocConfirm entity, BmsCenSaleDocConfirm serverEntity, List<string> tmpa, long empID, string empName)
        {
            if (entity.Status.ToString() == BmsCenSaleDtlConfirmStatus.待继续验收.Key)
            {
                if (serverEntity.Status.ToString() != BmsCenSaleDtlConfirmStatus.待继续验收.Key)
                {
                    return false;
                }
            }
            if (entity.Status.ToString() == BmsCenSaleDtlConfirmStatus.已验收.Key)
            {
                if (serverEntity.Status.ToString() != BmsCenSaleDtlConfirmStatus.待继续验收.Key)
                {
                    return false;
                }
            }
            if (entity.Status.ToString() == BmsCenSaleDtlConfirmStatus.部分入库.Key)
            {
                if (serverEntity.Status.ToString() != BmsCenSaleDtlConfirmStatus.已验收.Key && serverEntity.Status.ToString() != BmsCenSaleDtlConfirmStatus.部分入库.Key)
                {
                    return false;
                }
            }
            if (entity.Status.ToString() == BmsCenSaleDtlConfirmStatus.全部入库.Key)
            {
                if (serverEntity.Status.ToString() != BmsCenSaleDtlConfirmStatus.已验收.Key && serverEntity.Status.ToString() != BmsCenSaleDtlConfirmStatus.部分入库.Key)
                {
                    return false;
                }
            }
            return true;
        }
        public BaseResultDataValue AddReaSaleDocConfirmOfOrder(IList<BmsCenSaleDtlConfirmVO> dtAddList, int secAccepterType, string codeScanningMode, long empID, string empName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            this.Entity.StatusName = BmsCenSaleDtlConfirmStatus.GetStatusDic()[this.Entity.Status.ToString()].Name;

            //保存前验证处理
            BaseResultBool tempBaseResultBool2 = IBBmsCenSaleDtlConfirm.EditBmsCenSaleDtlConfirmValidOfOrder(dtAddList, codeScanningMode);
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

                BaseResultBool tempBaseResultBool = IBBmsCenSaleDtlConfirm.AddDtlConfirmOfList(this.Entity, dtAddList, codeScanningMode, empID, empName);
                baseResultDataValue.success = tempBaseResultBool.success;
                baseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                bool IsAcceptError = dtAddList.Sum(p => p.BmsCenSaleDtlConfirm.RefuseCount) > 0 ? true : false;
                this.Entity.IsAcceptError = IsAcceptError;
                if (string.IsNullOrEmpty(this.Entity.SaleDocConfirmNo))
                    this.Entity.SaleDocConfirmNo = this.GetSaleDocConfirmNo();
                if (this.Add())
                {
                    if (this.Entity.Status == int.Parse(BmsCenSaleDtlConfirmStatus.已验收.Key))
                    {
                        //更新订单状态
                        bool isAllConfirm = SearchBmsCenOrderDtlValidIsAllConfirmByOrderId(this.Entity.BmsCenOrderDoc.Id, dtAddList);
                        if (isAllConfirm == true)
                            this.Entity.BmsCenOrderDoc.Status = int.Parse(ReaBmsOrderDocStatus.全部验收.Key);
                        else
                            this.Entity.BmsCenOrderDoc.Status = int.Parse(ReaBmsOrderDocStatus.部分验收.Key);
                        BaseResultBool tempBaseResultBool3 = EditReaBmsCenOrderDoc(this.Entity.BmsCenOrderDoc, empID, empName);
                        baseResultDataValue.success = tempBaseResultBool3.success;
                        baseResultDataValue.ErrorInfo = tempBaseResultBool3.ErrorInfo;
                    }
                    AddReaCheckInOperation(this.Entity, empID, empName);
                }
                else
                    throw new Exception("订单验收--新增供货验收单失败！");
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("订单验收--新增供货验收单失败！：" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public BaseResultBool EditReaSaleDocConfirmOfOrder(string[] tempArray, IList<BmsCenSaleDtlConfirmVO> dtAddList, IList<BmsCenSaleDtlConfirmVO> dtEditList, int secAccepterType, string codeScanningMode, long empID, string empName, string fieldsDtl)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            BmsCenSaleDocConfirm serverEntity = this.Get(this.Entity.Id);
            List<string> tmpa = tempArray.ToList();
            if (!EditBmsCenSaleDocConfirmStatusCheck(this.Entity, serverEntity, tmpa, empID, empName))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "验收单ID：" + this.Entity.Id + "的状态为：" + BmsCenSaleDtlConfirmStatus.GetStatusDic()[serverEntity.Status.ToString()].Name + "！";
                return tempBaseResultBool;
            }

            #region 保存前验证处理
            if (dtAddList != null && dtAddList.Count > 0)
                tempBaseResultBool = IBBmsCenSaleDtlConfirm.EditBmsCenSaleDtlConfirmValidOfOrder(dtAddList, codeScanningMode);

            if (tempBaseResultBool.success == true && dtEditList != null && dtEditList.Count > 0)
                tempBaseResultBool = IBBmsCenSaleDtlConfirm.EditBmsCenSaleDtlConfirmValidOfOrder(dtEditList, codeScanningMode);
            if (tempBaseResultBool.success == false) return tempBaseResultBool;

            #endregion
            bool IsAcceptError = false;
            if (dtAddList != null && dtAddList.Count > 0) IsAcceptError = dtAddList.Sum(p => p.BmsCenSaleDtlConfirm.RefuseCount) > 0 ? true : false;
            if (IsAcceptError == false && dtEditList != null && dtEditList.Count > 0) IsAcceptError = dtEditList.Sum(p => p.BmsCenSaleDtlConfirm.RefuseCount) > 0 ? true : false;
            tmpa.Add("IsAcceptError=" + IsAcceptError);
            tempArray = tmpa.ToArray();

            if (this.Update(tempArray))
            {
                if (dtAddList != null && dtAddList.Count > 0)
                    tempBaseResultBool = IBBmsCenSaleDtlConfirm.AddDtlConfirmOfList(this.Entity, dtAddList, codeScanningMode, empID, empName);
                //验收明细更新
                if (tempBaseResultBool.success == true && dtEditList != null && dtEditList.Count > 0)
                    tempBaseResultBool = IBBmsCenSaleDtlConfirm.EditDtlConfirmOfList(this.Entity, dtEditList, fieldsDtl, codeScanningMode, empID, empName);

                if (this.Entity.Status == int.Parse(BmsCenSaleDtlConfirmStatus.已验收.Key))
                {
                    //订货单是否全部验收完,如果验收完,需要更新订单状态
                    bool isAllConfirm = SearchBmsCenOrderDtlValidIsAllConfirmByOrderId(this.Entity.BmsCenOrderDoc.Id, dtAddList);
                    if (isAllConfirm == true)
                        this.Entity.BmsCenOrderDoc.Status = int.Parse(ReaBmsOrderDocStatus.全部验收.Key);
                    else
                        this.Entity.BmsCenOrderDoc.Status = int.Parse(ReaBmsOrderDocStatus.部分验收.Key);
                    tempBaseResultBool = EditReaBmsCenOrderDoc(this.Entity.BmsCenOrderDoc, empID, empName);
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
                BmsCenSaleDocConfirm tempEntity = new BmsCenSaleDocConfirm();
                tempEntity.Id = this.Entity.Id;
                tempEntity.Status = this.Entity.Status;
                tempEntity.StatusName = BmsCenSaleDocConfirmStatus.GetStatusDic()[tempEntity.Status.ToString()].Name;
                if (tempBaseResultBool.success) AddReaCheckInOperation(tempEntity, empID, empName);
            }
            return tempBaseResultBool;
        }

        private BaseResultBool EditReaBmsCenOrderDoc(BmsCenOrderDoc entity, long empID, string empName)
        {
            //entity.Status = int.Parse(ReaBmsOrderDocStatus.已验收.Key);
            entity.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
            List<string> tmpa = new List<string>();
            tmpa.Add("Id=" + entity.Id + " ");
            tmpa.Add("Status=" + entity.Status + " ");
            tmpa.Add("StatusName='" + entity.StatusName + "' ");
            BaseResultBool tempBaseResultBool = IBBmsCenOrderDoc.EditReaBmsCenOrderDocAndDt(entity, tmpa.ToArray(), null, null, empID, empName);
            return tempBaseResultBool;
        }
        /// <summary>
        /// 判断某一订单是否完全验收完
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="dtAddList">当次新增保存的订单验收明细</param>
        /// <returns></returns>
        private bool SearchBmsCenOrderDtlValidIsAllConfirmByOrderId(long orderId, IList<BmsCenSaleDtlConfirmVO> dtAddList)
        {
            bool isAllConfirm = true;
            IList<BmsCenOrderDtl> orderDtlList = IDBmsCenOrderDtlDao.GetListByHQL(String.Format("bmscenorderdtl.BmsCenOrderDoc.Id={0}", orderId));
            //已保存的订单验收明细信息
            IList<BmsCenSaleDtlConfirm> dtlConfirmList = dtlConfirmList = IBBmsCenSaleDtlConfirm.SearchListByHQL(String.Format("bmscensaledtlconfirm.BmsCenOrderDoc.Id={0}", orderId));
            if (dtAddList != null)
            {
                for (int i = 0; i < dtAddList.Count; i++)
                {
                    var entity = dtAddList[i].BmsCenSaleDtlConfirm;
                    dtlConfirmList.Add(entity);
                }
            }

            if (orderDtlList != null && dtlConfirmList != null)
            {
                foreach (var model in orderDtlList)
                {
                    var dtlList = dtlConfirmList.Where(p => p.BmsCenOrderDtl.Id == model.Id).ToList();
                    //某一货品明细可验收数默认等于订单购进数
                    float confirmCount = model.GoodsQty;
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

        /// <summary>
        /// 添加申请操作记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        private void AddReaCheckInOperation(BmsCenSaleDocConfirm entity, long empID, string empName)
        {
            if (entity.Status.ToString() == BmsCenSaleDtlConfirmStatus.待继续验收.Key) return;

            ReaCheckInOperation sco = new ReaCheckInOperation();
            sco.BobjectID = entity.Id;
            sco.CreatorID = empID;
            if (empName != null && empName.Trim() != "")
                sco.CreatorName = empName;
            sco.BusinessModuleCode = "BmsCenSaleDocConfirm";
            // if (!string.IsNullOrEmpty(entity.CheckMemo))
            //sco.Memo = entity.CheckMemo;
            sco.IsUse = true;
            sco.Type = entity.Status;
            sco.TypeName = BmsCenSaleDocConfirmStatus.GetStatusDic()[entity.Status.ToString()].Name;
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