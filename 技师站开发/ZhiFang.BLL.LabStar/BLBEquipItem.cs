using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBEquipItem : BaseBLL<LBEquipItem>, ZhiFang.IBLL.LabStar.IBLBEquipItem
    {
        ZhiFang.IDAO.LabStar.IDLBEquipItemVODao IDLBEquipItemVODao { get; set; }

        ZhiFang.IBLL.LabStar.IBLisEquipItem IBLisEquipItem { get; set; }

        public BaseResultDataValue AddDelLBEquipItem(IList<LBEquipItem> addEntityList, bool isCheckEntityExist, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (addEntityList != null && addEntityList.Count > 0)
                {
                    foreach (LBEquipItem endtity in addEntityList)
                    {
                        IList<LBEquipItem> tempList = this.SearchListByHQL(" lbequipitem.LBEquip.Id=" + endtity.LBEquip.Id +
                            " and lbequipitem.LBItem.Id=" + endtity.LBItem.Id);
                        if (tempList == null || tempList.Count == 0)
                        {
                            endtity.IsUse = true;
                            endtity.DataAddTime = DateTime.Now;
                            endtity.DataUpdateTime = endtity.DataAddTime;
                            this.Entity = endtity;
                            this.Add();
                        }
                    }
                }

                DeleteLBEquipItem(delIDList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "AddDelLBEquipItem Error:" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue DeleteLBEquipItem(string equipItemIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (!string.IsNullOrWhiteSpace(equipItemIDList))
            {
                IList<string> listID = equipItemIDList.Split(',').ToList();
                bool delFlag = true;
                string strDelInfo = "";
                foreach (string id in listID)
                {
                    baseResultDataValue = DeleteLBEquipItem(long.Parse(id));
                    if (!baseResultDataValue.success)
                    {
                        delFlag = false;
                        if (!string.IsNullOrEmpty(baseResultDataValue.ErrorInfo))
                        {
                            if (string.IsNullOrEmpty(strDelInfo))
                                strDelInfo = baseResultDataValue.ErrorInfo;
                            else
                                strDelInfo += "</br>" + baseResultDataValue.ErrorInfo;
                        }
                    }
                }
                baseResultDataValue.success = delFlag;
                if (!delFlag)
                    baseResultDataValue.ErrorInfo = strDelInfo;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue DeleteLBEquipItem(long equipItemID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                LBEquipItem equipItem = this.Get(equipItemID);
                if (equipItem != null)
                {
                    long equipID = equipItem.LBEquip.Id;
                    long itemID = equipItem.LBItem.Id;
                    bool tempBool = IBLisEquipItem.QueryIsExistEquipItemResult(equipID, itemID);
                    if (tempBool)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "项目【" + equipItem.LBItem.CName + "】已经设置为小组仪器项目，不能删除或取消！";
                        return baseResultDataValue;
                    }
                    //this.Entity = equipItem;
                    //baseResultDataValue.success = this.Remove();
                    baseResultDataValue.success = this.RemoveByHQL(equipItemID);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "DeleteLBEquipItem Error:" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
                throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public EntityList<LBEquipItem> QueryLBEquipItem(string strHqlWhere, string Order, int start, int count)
        {
            return (this.DBDao as IDLBEquipItemDao).QueryLBEquipItemDao(strHqlWhere, Order, start, count);
        }

        public EntityList<LBEquipItemVO> QueryLBEquipItemVO(string strHqlWhere, string Order, int start, int count)
        {
            return (this.DBDao as IDLBEquipItemDao).QueryLBEquipItemVODao(strHqlWhere, Order, start, count);
        }

        public IList<LBEquipItem> QueryIsExistSectionItem(long sectionID, long itemID)
        {
            return (this.DBDao as IDLBEquipItemDao).QueryIsExistSectionItemDao(sectionID, itemID);
        }

    }
}