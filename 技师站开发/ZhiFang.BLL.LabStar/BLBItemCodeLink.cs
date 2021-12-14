using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BLBItemCodeLink : BaseBLL<LBItemCodeLink>, ZhiFang.IBLL.LabStar.IBLBItemCodeLink
    {
        IDAO.LabStar.IDLBItemDao IDLBItemDao { get; set; }
        IDAO.LabStar.IDLBSectionItemDao IDLBSectionItemDao { get; set; }
        IDAO.LabStar.IDLBSickTypeDao IDLBSickTypeDao { get; set; }

        public EntityList<LBItemCodeLinkVO> LS_UDTO_SearchLBItemAndLBItemCodeLink(long SectionID, int GroupType, long SickTypeID, string ItemCName, string sort, int page, int limit)
        {
            #region 数组分页
            List<LBItem> lBItems = new List<LBItem>();
            if (SectionID == 0)
            {
                string where = " IsUse = 1 ";
                if (GroupType > 0)
                {
                    where += " and GroupType = " + (GroupType - 1);
                }
                if (!string.IsNullOrEmpty(ItemCName))
                {
                    where += " and (CName like '%" + ItemCName + "%' or SName like '%" + ItemCName + "%')";
                }
                lBItems = IDLBItemDao.GetListByHQL(where).ToList();
            }
            else if (SectionID > 0)
            {
                string where = " lbsection.IsUse = 1 and lbitem.IsUse = 1  and lbsection.Id=" + SectionID;
                if (GroupType > 0)
                {
                    where += " and lbitem.GroupType = " + (GroupType - 1);
                }
                if (!string.IsNullOrEmpty(ItemCName))
                {
                    where += " and (lbitem.CName like '%" + ItemCName + "%' or lbitem.SName like '%" + ItemCName + "%')";
                }
                string fields = "LBSectionItem_LBSection_Id,LBSectionItem_LBSection_LabID,LBSectionItem_LBItem_LabID,LBSectionItem_LBItem_Id,LBSectionItem_LBItem_CName,LBSectionItem_LBItem_SName,LBSectionItem_LBItem_GroupType,LBSectionItem_LBItem_Shortcode,LBSectionItem_LBItem_DispOrder";
                IList<string> listEntityName = LisCommonMethod.GetJoinEntityNameByFields(fields);
                IList<LBSectionItem> lBSectionItems = IDLBSectionItemDao.GetListByHQL(where, listEntityName.ToArray());
                foreach (var item in lBSectionItems)
                {
                    LBItem lBItem = new LBItem();
                    lBItem.LabID = item.LBItem.LabID;
                    lBItem.Id = item.LBItem.Id;
                    lBItem.CName = item.LBItem.CName;
                    lBItem.EName = item.LBItem.SName;
                    lBItem.GroupType = item.LBItem.GroupType;
                    lBItem.Shortcode = item.LBItem.Shortcode;
                    lBItem.DispOrder = item.LBItem.DispOrder;
                    lBItems.Add(lBItem);
                }
            }
            IList<LBItemCodeLink> lBItemCodeLinks = DBDao.GetListByHQL("LinkSystemID=" + SickTypeID);
            List<LBItemCodeLinkVO> lBItemCodeLinkVOs = new List<LBItemCodeLinkVO>();
            foreach (var item in lBItems)
            {
                List<LBItemCodeLink> lBItemCodeLinkbyid = lBItemCodeLinks.Where(a => a.DicDataID == item.Id).ToList();
                if (lBItemCodeLinkbyid.Count > 0)
                {
                    foreach (var lBItemCodeLink in lBItemCodeLinkbyid)
                    {
                        LBItemCodeLinkVO lBItemCodeLinkVO = new LBItemCodeLinkVO();
                        lBItemCodeLinkVO = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LBItemCodeLinkVO, LBItemCodeLink>(lBItemCodeLink);
                        lBItemCodeLinkVO.DicDataID = item.Id;
                        lBItemCodeLinkVO.DicDataName = item.CName;
                        lBItemCodeLinkVO.DicDataCode = item.Shortcode;
                        lBItemCodeLinkVO.GroupType = item.GroupType;
                        lBItemCodeLinkVO.DispOrder = item.DispOrder;
                        lBItemCodeLinkVOs.Add(lBItemCodeLinkVO);
                    }
                }
                else
                {
                    LBItemCodeLinkVO lBItemCodeLinkVO = new LBItemCodeLinkVO();
                    lBItemCodeLinkVO.LabID = item.LabID;
                    lBItemCodeLinkVO.DicDataID = item.Id;
                    lBItemCodeLinkVO.Id = 0;
                    lBItemCodeLinkVO.DicDataName = item.CName;
                    lBItemCodeLinkVO.DicDataCode = item.Shortcode;
                    lBItemCodeLinkVO.GroupType = item.GroupType;
                    lBItemCodeLinkVO.DispOrder = item.DispOrder;
                    lBItemCodeLinkVOs.Add(lBItemCodeLinkVO);
                }
            }
            lBItemCodeLinkVOs = lBItemCodeLinkVOs.OrderBy(a => a.DispOrder).ToList();
            EntityList<LBItemCodeLinkVO> entityList = new EntityList<LBItemCodeLinkVO>();
            entityList.count = lBItemCodeLinkVOs.Count;
            entityList.list = (lBItemCodeLinkVOs.Skip((page - 1) * limit).Take(limit)).ToList();
            return entityList;
            #endregion
        }
        public List<LBItem> LS_UDTO_SearchLBItemAndLBItemBySickTypeID(long SectionID, int GroupType, long SickTypeID, string ItemCName)
        {
            #region 数组分页
            List<LBItem> lBItems = new List<LBItem>();
            if (SectionID == 0)
            {
                string where = " IsUse = 1 ";
                if (GroupType > 0)
                {
                    where += " and GroupType = " + (GroupType - 1);
                }
                if (!string.IsNullOrEmpty(ItemCName))
                {
                    where += " and (CName like '%" + ItemCName + "%' or SName like '%" + ItemCName + "%')";
                }
                lBItems = IDLBItemDao.GetListByHQL(where).ToList();
            }
            else if (SectionID > 0)
            {
                string where = " IsUse = 1 and LBItem.IsUse = 1  and LBSection.Id=" + SectionID;
                if (GroupType > 0)
                {
                    where += " and LBItem.GroupType = " + (GroupType - 1);
                }
                if (!string.IsNullOrEmpty(ItemCName))
                {
                    where += " and (LBItem.CName like '%" + ItemCName + "%' or LBItem.SName like '%" + ItemCName + "%')";
                }
                IList<LBSectionItem> lBSectionItems = IDLBSectionItemDao.GetListByHQL(where);
                foreach (var item in lBSectionItems)
                {
                    LBItem lBItem = new LBItem();
                    lBItem.LabID = item.LBItem.LabID;
                    lBItem.Id = item.LBItem.Id;
                    lBItem.CName = item.LBItem.CName;
                    lBItem.EName = item.LBItem.SName;
                    lBItem.GroupType = item.LBItem.GroupType;
                    lBItem.Shortcode = item.LBItem.Shortcode;
                    lBItem.DispOrder = item.LBItem.DispOrder;
                    lBItems.Add(lBItem);
                }
            }
            return lBItems;
            #endregion
        }
        //项目字典对照复制
        public BaseResultDataValue AddCopyLBItemCodeLinkContrast(string sickTypeIds, string thisSickTypeId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            if (string.IsNullOrEmpty(sickTypeIds)) {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "请选择要复制的对接系统不可为空！";
                return baseResultDataValue;
            }
            if (string.IsNullOrEmpty(thisSickTypeId))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "对接系统不可为空！";
                return baseResultDataValue;
            }
            IList<LBItemCodeLink> lBItemCodeLinks = DBDao.GetListByHQL("LinkSystemID in (" + sickTypeIds+","+ thisSickTypeId + ")");
            if (lBItemCodeLinks.Count > 0)
            {
                var lBSickType = IDLBSickTypeDao.Get(long.Parse(thisSickTypeId));
                //被复制的数据
                var copylbitemcodes = lBItemCodeLinks.Where(a => a.LinkSystemID != long.Parse(thisSickTypeId));
                //本数据
                var thislbitemcode= lBItemCodeLinks.Where(a => a.LinkSystemID == long.Parse(thisSickTypeId));
                if (copylbitemcodes != null && copylbitemcodes.Count() > 0) {
                    var copylbitemcodes2 = copylbitemcodes.ToList();
                    if (thislbitemcode != null && thislbitemcode.Count() > 0)
                    {
                        for (int i = 0; i < copylbitemcodes2.Count(); i++)
                        {
                            LBItemCodeLink lBItemCodeLink = new LBItemCodeLink();
                            lBItemCodeLink = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LBItemCodeLink, LBItemCodeLink>(copylbitemcodes2[i]); ;
                            lBItemCodeLink.LinkSystemID = lBSickType.Id;
                            lBItemCodeLink.LinkSystemName = lBSickType.CName;
                            lBItemCodeLink.LinkSystemCode = lBSickType.Shortcode;
                            var lBItemCodeLinksByDicData = thislbitemcode.Where(a => a.DicDataID == copylbitemcodes2[i].DicDataID && a.LinkDicDataCode == copylbitemcodes2[i].LinkDicDataCode);
                            if (lBItemCodeLinksByDicData != null && lBItemCodeLinksByDicData.Count() > 0)
                            {
                                lBItemCodeLink.Id = lBItemCodeLinksByDicData.First().Id;
                                lBItemCodeLink.DataUpdateTime = DateTime.Now;
                                if (!DBDao.Update(lBItemCodeLink))
                                {
                                    baseResultDataValue.success = false;
                                    baseResultDataValue.ErrorInfo = "复制失败！";
                                    return baseResultDataValue;
                                }
                            }
                            else
                            {
                                lBItemCodeLink.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                                lBItemCodeLink.DataAddTime = DateTime.Now;
                                if (!DBDao.Save(lBItemCodeLink))
                                {
                                    baseResultDataValue.success = false;
                                    baseResultDataValue.ErrorInfo = "复制失败！";
                                    return baseResultDataValue;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < copylbitemcodes2.Count(); i++)
                        {
                            LBItemCodeLink lBItemCodeLink = new LBItemCodeLink();
                            lBItemCodeLink = ZhiFang.LabStar.Common.ClassMapperHelp.GetMapper<LBItemCodeLink, LBItemCodeLink>(copylbitemcodes2[i]);
                            lBItemCodeLink.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                            lBItemCodeLink.LinkSystemID = lBSickType.Id;
                            lBItemCodeLink.LinkSystemName = lBSickType.CName;
                            lBItemCodeLink.LinkSystemCode = lBSickType.Shortcode;
                            lBItemCodeLink.DataAddTime = DateTime.Now;
                            lBItemCodeLink.DataTimeStamp = null;
                            if (!DBDao.Save(lBItemCodeLink))
                            {
                                baseResultDataValue.success = false;
                                baseResultDataValue.ErrorInfo = "复制失败！";
                                return baseResultDataValue;
                            }
                        }
                    }
                }
            }
            return baseResultDataValue;
        }
    }
}