
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.BloodTransfusion
{
	/// <summary>
	///
	/// </summary>
	public  class BBloodAggluItem : BaseBLL<BloodAggluItem>, ZhiFang.IBLL.BloodTransfusion.IBBloodAggluItem
	{
        IBSCOperation IBSCOperation { get; set; }

        #region 修改信息记录
        public void AddSCOperation(BloodAggluItem serverEntity, string[] arrFields, long empID, string empName)
        {
            StringBuilder strbMemo = new StringBuilder();
            EditGetUpdateMemoHelp.EditGetUpdateMemo<BloodAggluItem>(serverEntity, this.Entity, this.Entity.GetType(), arrFields, ref strbMemo);
            if (strbMemo.Length > 0)
            {
                SCOperation sco = new SCOperation();
                sco.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                sco.LabID = serverEntity.LabID;
                sco.BobjectID = this.Entity.Id;
                sco.CreatorID = empID;
                if (empName != null && empName.Trim() != "")
                    sco.CreatorName = empName;
                sco.BusinessModuleCode = "BloodAggluItem";
                strbMemo.Insert(0, "本次修改记录:" + System.Environment.NewLine);
                //ZhiFang.Common.Log.Log.Debug("修改人:" + empName + "," + strbMemo.ToString());
                sco.Memo = strbMemo.ToString();
                sco.IsUse = true;
                sco.Type = long.Parse(UpdateOperationType.凝集规则明细字典.Key);
                sco.TypeName = UpdateOperationType.GetStatusDic()[sco.Type.ToString()].Name;
                IBSCOperation.Entity = sco;
                IBSCOperation.Add();
            }
        }
        #endregion

    }
}