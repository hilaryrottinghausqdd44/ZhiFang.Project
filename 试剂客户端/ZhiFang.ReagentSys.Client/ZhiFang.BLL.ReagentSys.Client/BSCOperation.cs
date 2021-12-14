using System;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public  class BSCOperation : BaseBLL<SCOperation>, IBSCOperation
	{
        /// <summary>
        /// 操作记录登记
        /// </summary>
        /// <param name="ffile"></param>
        /// <param name="type"></param>
        public void AddOperation(long bobjectID, long labID, int type, string operationMemo)
        {
            SCOperation operation = new SCOperation();
            operation.BobjectID = bobjectID;
            operation.LabID = labID;
            operation.IsUse = true;
            operation.DataAddTime = DateTime.Now;
            operation.Type = type;
            operation.Memo = operationMemo;
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);

            if (employeeID != "-1" && !String.IsNullOrEmpty(employeeID))
            {
                operation.CreatorID = long.Parse(employeeID);
            }
            operation.CreatorName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            this.Entity = operation;
            this.Add();
        }

        /// <summary>
        /// 操作记录登记---增加实体的状态操作
        /// </summary>
        /// <param name="operEntity"></param>
        public void AddOperationEntityStatus(BaseEntity operEntity)
        {
            bool isAdd = false;
            SCOperation operation = new SCOperation();
            string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
            string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            if (empid != null && empid.Trim() != "")
                operation.CreatorID = long.Parse(empid);
            if (empname != null && empname.Trim() != "")
                operation.CreatorName = empname;
            operation.BobjectID = operEntity.Id;
            //if (operEntity is PInvoice)
            //{
            //    operation.BusinessModuleCode = "PInvoice";
            //    operation.Memo = ((PInvoice)operEntity).OperationMemo;
            //    operation.Type = ((PInvoice)operEntity).Status;
            //    operation.TypeName = PInvoiceStatus.GetStatusDic()[((PInvoice)operEntity).Status.ToString()].Name;
            //    isAdd = true;
            //}
            //else if (operEntity is PExpenseAccount)
            //{
            //    operation.BusinessModuleCode = "PExpenseAccount";
            //    operation.Memo = ((PExpenseAccount)operEntity).OperationMemo;
            //    operation.Type = ((PExpenseAccount)operEntity).Status;
            //    operation.TypeName = PExpenseAccountStatus.GetStatusDic()[((PExpenseAccount)operEntity).Status.ToString()].Name;
            //    isAdd = true;
            //}
            //else if (operEntity is PContract)
            //{
            //    operation.BusinessModuleCode = "PContract";
            //    operation.Memo = ((PContract)operEntity).OperationMemo;
            //    operation.Type = long.Parse(((PContract)operEntity).ContractStatus);
            //    operation.TypeName = PContractStatus.GetStatusDic()[((PContract)operEntity).ContractStatus.ToString()].Name;
            //    isAdd = true;
            //}
            //else if (operEntity is PReceivePlan)
            //{
            //    operation.BusinessModuleCode = "PReceivePlan";
            //    operation.Memo = ((PReceivePlan)operEntity).OperationMemo;
            //    operation.Type = ((PReceivePlan)operEntity).Status;
            //    operation.TypeName = PReceivePlanStatus.GetStatusDic()[((PReceivePlan)operEntity).Status.ToString()].Name;
            //    isAdd = true;
            //}
            if (isAdd)
            {
                this.Entity = operation;
                this.Add();
            }
        }
    }
}