
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
	/// <summary>
	///
	/// </summary>
	public  class BBEquip : BaseBLL<BEquip>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBBEquip
	{
        public IBSCOperation IBSCOperation { get; set; }

        public BaseResultDataValue AddBEquip(BEquip entity) {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            this.Entity = entity;
            tempBaseResultDataValue.success = this.Add();
            AddOperation(entity, (int)SCOperationType.新增仪器信息, "新增仪器信息");
            return tempBaseResultDataValue;
        }
        public BaseResultBool UpdateBEquip(string[] tempArray)
        {
            BaseResultBool tempBaseResultDataValue = new BaseResultBool();
            tempBaseResultDataValue.success = this.Update(tempArray);
            AddOperation(this.Entity, (int)SCOperationType.修改仪器信息, "修改仪器信息");
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 操作记录登记
        /// </summary>
        /// <param name="ffile"></param>
        /// <param name="type"></param>
        private void AddOperation(BEquip entity, int type, string operationMemo)
        {
            SCOperation sco = new SCOperation();
            sco.BobjectID = entity.Id;
            string empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
            string empname = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            if (empid != null && empid.Trim() != "")
                sco.CreatorID = long.Parse(empid);
            if (empname != null && empname.Trim() != "")
                sco.CreatorName = empname;
            sco.BusinessModuleCode = "BEquip";
            sco.Memo = String.IsNullOrEmpty(entity.OperationMemo)? operationMemo: entity.OperationMemo;

            sco.Type = type;
            sco.TypeName = operationMemo;
            IBSCOperation.Entity = sco;
            IBSCOperation.Add();
        }


    }
}