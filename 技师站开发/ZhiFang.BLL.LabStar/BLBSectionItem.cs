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
    public class BLBSectionItem : BaseBLL<LBSectionItem>, ZhiFang.IBLL.LabStar.IBLBSectionItem
    {
        ZhiFang.IBLL.LabStar.IBLBItem IBLBItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBEquipItem IBLBEquipItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBSection IBLBSection { get; set; }

        ZhiFang.IBLL.LabStar.IBLisTestItem IBLisTestItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBItemGroup IBLBItemGroup { get; set; }

        ZhiFang.IDAO.LabStar.IDLBSectionItemVODao IDLBSectionItemVODao { get; set; }

        public BaseResultDataValue AddDelLBSectionItem(IList<LBSectionItem> addEntityList, bool isCheckEntityExist, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (addEntityList != null && addEntityList.Count > 0)
                {
                    foreach (LBSectionItem endtity in addEntityList)
                    {
                        IList<LBSectionItem> tempList = this.SearchListByHQL(" lbsectionitem.LBSection.Id=" + endtity.LBSection.Id +
                            " and lbsectionitem.LBItem.Id=" + endtity.LBItem.Id);
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

                DeleteLBSectionItem(delIDList);

            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "AddDelLBSectionItem Error:" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue AddCopyLBSectionItem(long fromSectionID, long toSectionID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                LBSection fromLBSection = IBLBSection.Get(fromSectionID);
                if (fromLBSection == null)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无法获取要复制项目的小组信息：Id=" + fromSectionID;
                    return baseResultDataValue;
                }
                LBSection toLBSection = IBLBSection.Get(toSectionID);
                if (toLBSection == null)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无法获取新增项目的小组信息：Id=" + toSectionID;
                    return baseResultDataValue;
                }
                IList<LBSectionItemVO> listToSectionItem = IDLBSectionItemVODao.QueryLBSectionItemVODao(" lbsection.Id=" + toSectionID.ToString());
                IList<LBSectionItemVO> listFromSectionItem = IDLBSectionItemVODao.QueryLBSectionItemVODao(" lbsection.Id=" + fromSectionID.ToString());
                if (listFromSectionItem != null && listFromSectionItem.Count > 0)
                {
                    foreach (LBSectionItemVO entity in listFromSectionItem)
                    {
                        IList<LBSectionItemVO> tempList = listToSectionItem.Where(p => p.LBItem.Id == entity.LBItem.Id).ToList();
                        if ((tempList == null || tempList.Count == 0) && (entity.LBItem != null) && (entity.LBSectionItem != null))
                        {
                            LBSectionItem newEntity = new LBSectionItem();
                            newEntity.LabID = toLBSection.LabID;
                            newEntity.LBSection = toLBSection;
                            newEntity.LBItem = entity.LBItem;
                            newEntity.GroupItemID = entity.LBSectionItem.GroupItemID;
                            newEntity.EquipID = entity.LBSectionItem.EquipID;
                            newEntity.DefultValue = entity.LBSectionItem.DefultValue;
                            newEntity.DispOrder = entity.LBSectionItem.DispOrder;
                            newEntity.IsDefult = entity.LBSectionItem.IsDefult;
                            newEntity.IsUse = entity.LBSectionItem.IsUse;
                            newEntity.DataAddTime = DateTime.Now;
                            newEntity.DataUpdateTime = newEntity.DataAddTime;
                            this.Entity = newEntity;
                            if (this.Add())
                            {
                                LBSectionItemVO newVO = new LBSectionItemVO();
                                newVO.LBSectionItem = newEntity;
                                newVO.LBSection = entity.LBSection;
                                newVO.LBItem = entity.LBItem;
                                listToSectionItem.Add(newVO);
                            }
                        }
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "小组【" + fromLBSection.CName + "】中无项目信息！";
                    ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "AddCopyLBSectionItem Error:" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue DeleteLBSectionItem(string sectionItemIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (!string.IsNullOrWhiteSpace(sectionItemIDList))
            {
                IList<string> listID = sectionItemIDList.Split(',').ToList();
                bool delFlag = true;
                string strDelInfo = "";
                foreach (string id in listID)
                {
                    baseResultDataValue = DeleteLBSectionItem(long.Parse(id));
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

        public BaseResultDataValue DeleteLBSectionItem(long sectionItemID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                LBSectionItem sectionItem = this.Get(sectionItemID);
                if (sectionItem != null)
                {
                    long sectionID = sectionItem.LBSection.Id;
                    long itemID = sectionItem.LBItem.Id;
                    bool tempBool = IBLisTestItem.QueryIsExistTestItemResult(sectionID, itemID);
                    if (tempBool)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "项目【" + sectionItem.LBItem.CName + "】已经存在项目结果，不能删除或取消！";
                        return baseResultDataValue;
                    }
                    IList<LBEquipItem> listLBEquipItem = IBLBEquipItem.QueryIsExistSectionItem(sectionID, itemID);
                    if (listLBEquipItem != null && listLBEquipItem.Count > 0)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "项目【" + sectionItem.LBItem.CName + "】已经设置为小组仪器项目，不能删除或取消！";
                        return baseResultDataValue;
                    }
                    //DateTime dataAddTime = DateTime.Parse(((DateTime)sectionItem.DataAddTime).ToString("yyyy-MM-dd"));
                    //DateTime nowTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                    //if (dataAddTime < nowTime)
                    //{

                    //}
                    //this.Entity = sectionItem;
                    //baseResultDataValue.success = this.Remove();
                    baseResultDataValue.success = this.RemoveByHQL(sectionItemID);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "DeleteLBSectionItem Error:" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
                throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public EntityList<LBSectionItemVO> QueryLBSectionItemVO(string strHqlWhere, string Order, int start, int count)
        {
            //return IDLBSectionItemVODao.QueryLBSectionItemVODao(strHqlWhere, Order, start, count);
            return (DBDao as IDLBSectionItemDao).QueryLBSectionItemVODao(strHqlWhere, Order, start, count);
        }

        /// <summary>
        /// 查询检验小组默认项目（组合项目拆分单项）
        /// </summary>
        /// <param name="sectionID">小组ID</param>
        /// <returns></returns>
        public EntityList<LBSectionSingleItemVO> QueryLBSectionDefultSingleItemVO(long sectionID)
        {
            EntityList<LBSectionSingleItemVO> entityList = new EntityList<LBSectionSingleItemVO>();
            IList<LBSectionSingleItemVO> list = new List<LBSectionSingleItemVO>();
            IList<LBSectionItem> listAllSectionItem = (DBDao as IDLBSectionItemDao).QueryLBSectionItemDao(" lbsectionitem.LBSection.Id=" + sectionID.ToString());
            //倒序排列，组合项目先拆分子项
            IList<LBSectionItem> listLBSectionItem = listAllSectionItem.Where(p => p.IsDefult && p.LBItem.IsUse).OrderByDescending(p => p.LBItem.GroupType).ToList();
            //IList<LBSectionItem> listLBSectionItem = (DBDao as IDLBSectionItemDao).QueryLBSectionItemDao(" lbsectionitem.LBSection.Id=" + sectionID.ToString() + " and lbsectionitem.IsDefult=1 and lbsectionitem.LBItem.IsUse=1");
            foreach (LBSectionItem sectionItem in listLBSectionItem)
            {
                LBItem tempParItem = null;
                LBItem tempItem = sectionItem.LBItem;
                IList<LBItem> listAddItem = new List<LBItem>();
                if (tempItem.GroupType ==0)
                {
                    listAddItem.Add(tempItem);
                    if (sectionItem.GroupItemID != null)
                        tempParItem = IBLBItem.Get(sectionItem.GroupItemID.Value);
                }
                else if (tempItem.GroupType == 1  )
                {
                    IList<LBItemGroup> listLBItemGroup = IBLBItemGroup.QueryLBItemGroup(" lbgroup.Id=" + tempItem.Id, "LBItemGroup_LBItem_Id,LBItemGroup_LBGroup_Id");
                    listAddItem = listLBItemGroup.Select(p => p.LBItem).ToList();
                    tempParItem = IBLBItem.Get(tempItem.Id);
                }
                else if (tempItem.GroupType == 2)
                {
                    //ZhiFang.LabStar.Common.LogHelp.Info("新增小组默认项目【" + tempItem.CName + "】：该项目为组套项目，不能新增！");
                    continue;
                }
                foreach (LBItem item in listAddItem)
                {
                    IList<LBSectionSingleItemVO> tempList = list.Where(p => p.LBItem.Id == item.Id).ToList();
                    if (tempList == null || tempList.Count == 0)
                    {
                        LBSectionSingleItemVO entity = new LBSectionSingleItemVO();
                        IList<LBSectionItem> listDefultItem = listAllSectionItem.Where(p => p.LBItem.Id == item.Id).ToList();
                        if (listDefultItem != null && listDefultItem.Count > 0)
                            entity.LBSectionItem = listDefultItem[0];
                        entity.LBSection = sectionItem.LBSection;
                        entity.LBItem = item;
                        entity.LBParItem = tempParItem;
                        list.Add(entity);
                    }
                }
            }
            entityList.list = list;
            entityList.count = list.Count;
            return entityList;
        }

    }
}