
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.IDAO.NHB.BloodTransfusion;
using ZhiFang.Entity.Base;

namespace ZhiFang.BLL.BloodTransfusion
{
	/// <summary>
	///
	/// </summary>
	public  class BBloodBagChargeLink : BaseBLL<BloodBagChargeLink>, ZhiFang.IBLL.BloodTransfusion.IBBloodBagChargeLink
	{
        IBSCOperation IBSCOperation { get; set; }

        //public override bool Add()
        //{
        //    //bloodchargeitemlink.IsUse=1 and 
        //    IList<BloodBagChargeLink> tempList = ((IDBloodBagChargeLinkDao)base.DBDao).GetListByHQL("bloodbagchargelink.ChargeType.Id=" + this.Entity.ChargeType.Id + " and bloodbagchargelink.BloodChargeItem.Id=" + this.Entity.BloodChargeItem.Id);
        //    if (tempList != null && tempList.Count > 0)
        //    {
        //        ZhiFang.Common.Log.Log.Info("新增收费类型费用项目关系提示:费用类型字典Id为:" + this.Entity.ChargeType.Id + ",费用项目Id为:" + this.Entity.BloodChargeItem.Id + "，已经存在！");
        //        return true;
        //    }
        //    else
        //    {
        //        bool a = DBDao.Save(this.Entity);
        //        return a;
        //    }
        //}
        #region 修改信息记录
        public void AddSCOperation(BloodBagChargeLink serverEntity, string[] arrFields, long empID, string empName)
        {
            StringBuilder strbMemo = new StringBuilder();
            EditGetUpdateMemoHelp.EditGetUpdateMemo<BloodBagChargeLink>(serverEntity, this.Entity, this.Entity.GetType(), arrFields, ref strbMemo);
            if (strbMemo.Length > 0)
            {
                SCOperation sco = new SCOperation();
                sco.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                sco.LabID = serverEntity.LabID;
                sco.BobjectID = this.Entity.Id;
                sco.CreatorID = empID;
                if (empName != null && empName.Trim() != "")
                    sco.CreatorName = empName;
                sco.BusinessModuleCode = "BloodBagChargeLink";
                strbMemo.Insert(0, "本次修改记录:" + System.Environment.NewLine);
                //ZhiFang.Common.Log.Log.Debug("修改人:" + empName + "," + strbMemo.ToString());
                sco.Memo = strbMemo.ToString();
                sco.IsUse = true;
                sco.Type = long.Parse(UpdateOperationType.血袋费用项目关系.Key);
                sco.TypeName = UpdateOperationType.GetStatusDic()[sco.Type.ToString()].Name;
                IBSCOperation.Entity = sco;
                IBSCOperation.Add();
            }
        }
        #endregion
    }
}