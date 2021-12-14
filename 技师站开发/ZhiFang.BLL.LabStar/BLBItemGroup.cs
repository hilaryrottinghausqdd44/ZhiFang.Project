using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBItemGroup : BaseBLL<LBItemGroup>, ZhiFang.IBLL.LabStar.IBLBItemGroup
    {
        ZhiFang.IBLL.LabStar.IBLBItem IBLBItem { get; set; }

        public BaseResultDataValue AddDelLBItemGroup(IList<LBItemGroup> addEntityList, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (addEntityList != null && addEntityList.Count > 0)
                {
                    foreach (LBItemGroup endtity in addEntityList)
                    {
                        endtity.DataAddTime = DateTime.Now;
                        endtity.DataUpdateTime = endtity.DataAddTime;
                        this.Entity = endtity;
                        this.Add();
                    }
                }

                if (!string.IsNullOrWhiteSpace(delIDList))
                {
                    IList<string> listID = delIDList.Split(',').ToList();
                    foreach (string id in listID)
                    {
                        this.RemoveByHQL(long.Parse(id));
                    }
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "AddDelLBItemGroup Error:" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue AddCopyLBItemGroup(long fromGroupItemID, long toGroupItemID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                LBItem lbGroupItem = IBLBItem.Get(toGroupItemID);
                if (lbGroupItem == null)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无法获取新增的组合项目信息：" + lbGroupItem.CName;
                    return baseResultDataValue;
                }
                IList<LBItemGroup> listToItemGroup = this.SearchListByHQL(" lbitemgroup.LBGroup.Id=" + toGroupItemID.ToString());
                IList<LBItemGroup> listFromItemGroup = this.SearchListByHQL(" lbitemgroup.LBGroup.Id=" + fromGroupItemID.ToString());
                if (listFromItemGroup != null && listFromItemGroup.Count > 0)
                {
                    foreach (LBItemGroup entity in listFromItemGroup)
                    {
                        IList<LBItemGroup> tempList = listToItemGroup.Where(p => p.LBItem.Id == entity.LBItem.Id).ToList();
                        if (tempList == null || tempList.Count == 0)
                        {
                            LBItemGroup newEntity = new LBItemGroup();
                            newEntity.LabID = lbGroupItem.LabID;
                            newEntity.LBGroup = lbGroupItem;
                            newEntity.LBItem = entity.LBItem;
                            newEntity.DataAddTime = DateTime.Now;
                            newEntity.DataUpdateTime = newEntity.DataAddTime;
                            this.Entity = newEntity;
                            if (this.Add())
                                listToItemGroup.Add(newEntity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "AddCopyLBItemGroup Error:" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        public IList<LBItemGroup> QueryLBItemGroup(string strHqlWhere, string fields)
        {
            IList<string> listEntityName = LisCommonMethod.GetJoinEntityNameByFields(fields);
            return this.SearchListByHQL(strHqlWhere, listEntityName.ToArray());
        }


        public EntityList<LBItemGroup> QueryLBItemGroup(string strHqlWhere, string order, int start, int count, string fields)
        {
            IList<string> listEntityName = LisCommonMethod.GetJoinEntityNameByFields(fields);
            listEntityName = LisCommonMethod.GetJoinEntityNameByOrderFields(listEntityName, ref order);
            return this.SearchListByHQL(strHqlWhere, order, start, count, listEntityName.ToArray());
        }

    }
}