
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
    public class BBloodChargeItemLink : BaseBLL<BloodChargeItemLink>, ZhiFang.IBLL.BloodTransfusion.IBBloodChargeItemLink
    {
        IBSCOperation IBSCOperation { get; set; }
        IDBloodChargeItemDao IDBloodChargeItemDao { get; set; }

        public override bool Add()
        {
            //bloodchargeitemlink.IsUse=1 and 
            IList<BloodChargeItemLink> tempList = ((IDBloodChargeItemLinkDao)base.DBDao).GetListByHQL("bloodchargeitemlink.ChargeType.Id=" + this.Entity.ChargeType.Id + " and bloodchargeitemlink.BloodChargeItem.Id=" + this.Entity.BloodChargeItem.Id);
            if (tempList != null && tempList.Count > 0)
            {
                ZhiFang.Common.Log.Log.Info("新增收费类型费用项目关系提示:费用类型字典Id为:" + this.Entity.ChargeType.Id + ",费用项目Id为:" + this.Entity.BloodChargeItem.Id + "，已经存在！");
                return true;
            }
            else
            {
                bool a = DBDao.Save(this.Entity);
                return a;
            }
        }

        #region 修改信息记录
        public void AddSCOperation(BloodChargeItemLink serverEntity, string[] arrFields, long empID, string empName)
        {
            StringBuilder strbMemo = new StringBuilder();
            EditGetUpdateMemoHelp.EditGetUpdateMemo<BloodChargeItemLink>(serverEntity, this.Entity, this.Entity.GetType(), arrFields, ref strbMemo);
            if (strbMemo.Length > 0)
            {
                SCOperation sco = new SCOperation();
                sco.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                sco.LabID = serverEntity.LabID;
                sco.BobjectID = this.Entity.Id;
                sco.CreatorID = empID;
                if (empName != null && empName.Trim() != "")
                    sco.CreatorName = empName;
                sco.BusinessModuleCode = "BloodChargeItemLink";
                strbMemo.Insert(0, "本次修改记录:" + System.Environment.NewLine);
                //ZhiFang.Common.Log.Log.Debug("修改人:" + empName + "," + strbMemo.ToString());
                sco.Memo = strbMemo.ToString();
                sco.IsUse = true;
                sco.Type = long.Parse(UpdateOperationType.收费类型费用项目关系.Key);
                sco.TypeName = UpdateOperationType.GetStatusDic()[sco.Type.ToString()].Name;
                IBSCOperation.Entity = sco;
                IBSCOperation.Add();
            }
        }
        #endregion

        public EntityList<BloodChargeItem> SearchBloodChargeItemByChargeItemLinkHQL(int page, int limit, string where, string linkWhere, string sort)
        {
            EntityList<BloodChargeItem> entityList = new EntityList<BloodChargeItem>();

            IList<BloodChargeItemLink> linkList1 = ((IDBloodChargeItemLinkDao)base.DBDao).GetListByHQL(linkWhere);
            if (linkList1 != null && linkList1.Count > 0)
            {
                IList<BloodChargeItem> entityList1 = IDBloodChargeItemDao.GetListByHQL(where, sort, -1, -1).list;
                //找出关系表里的费用项目集合信息
                var linkList = (from list2 in linkList1
                                select list2.BloodChargeItem).ToList<BloodChargeItem>();
                //比较生成两个序列的差集
                List<BloodChargeItem> entityList3 = entityList1.Except(linkList).ToList();

                entityList.count = entityList3.Count;
                //进行分页
                if (limit > 0 && limit < entityList3.Count)
                {
                    int startIndex = limit * (page - 1);
                    int endIndex = limit;
                    var list = entityList3.Skip(startIndex).Take(endIndex);
                    if (list != null)
                        entityList3 = list.ToList();
                }
                entityList.list = entityList3;
            }
            else
            {
                entityList = IDBloodChargeItemDao.GetListByHQL(where, sort, page, limit);
            }

            return entityList;
        }

    }
}