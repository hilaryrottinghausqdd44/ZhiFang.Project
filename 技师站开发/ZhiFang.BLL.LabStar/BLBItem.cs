using System;
using System.Linq;
using System.Collections.Generic;
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
    public class BLBItem : BaseBLL<LBItem>, ZhiFang.IBLL.LabStar.IBLBItem
    {
        IDLBItemGroupDao IDLBItemGroupDao { get; set; }
        IDLBItemRangeDao IDLBItemRangeDao { get; set; }
        IDLBSampleItemDao IDLBSampleItemDao { get; set; }
        IDLBReportDateItemDao IDLBReportDateItemDao { get; set; }
        IDLBSamplingItemDao IDLBSamplingItemDao { get; set; }
        IDLBSectionItemDao IDLBSectionItemDao { get; set; }

        public void Test(long id, string strHqlWhere, string Order, int limit)
        {

            ((IDLBItemDao)this.DBDao).TestDao(id, strHqlWhere, Order, limit);
        }
        public void TestAnWeiYu()
        {
            IList<LBItem> bb = SearchListByHQL(" lbitem.DispOrder=1 or lbitem.DispOrder=0 ");
            IList<LBItem> aa = SearchListByHQL(" lbitem.DispOrder & 4=4 ");
        }

        /// <summary>
        /// 更新检验项目的默认参考值范围
        /// </summary>
        /// <param name="itemID">检验项目ID</param>
        /// <returns></returns>
        public BaseResultDataValue EditLBItemDefaultRange(long itemID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            LBItem item = this.Get(itemID);
            if (item != null)
            {
                string refRange = "";
                string rangeAllInfo = "";
                IList<LBItemRange> listItemRange = IDLBItemRangeDao.GetListByHQL(" lbitemrange.LBItem.Id=" + itemID);
                if (listItemRange != null && listItemRange.Count > 0)
                {
                    IList<LBItemRange> tempList = listItemRange.Where(p => p.IsDefault == true).OrderBy(p => p.DispOrder).ToList();
                    if (tempList != null && tempList.Count > 0)
                    {
                        refRange = tempList[0].RefRange;
                    }
                    tempList = listItemRange.Where(p => p.IsDefault == false).OrderBy(p => p.DispOrder).ToList();
                    if (tempList != null && tempList.Count > 0)
                    {
                        foreach (LBItemRange itemRange in tempList)
                        {
                            if (rangeAllInfo == "")
                                rangeAllInfo = itemRange.ConditionName + "(" + itemRange.RefRange + ")";
                            else
                                rangeAllInfo += "," + itemRange.ConditionName + "(" + itemRange.RefRange + ")";
                        }
                    }
                    if (rangeAllInfo == "")
                        rangeAllInfo = "默认(" + refRange + ")";
                    else if (refRange != "")
                        rangeAllInfo += "," + "默认(" + refRange + ")";
                }
                item.DefaultRange = refRange;
                item.RangeAllInfo = rangeAllInfo;
                this.Entity = item;
                baseResultDataValue.success = this.Edit();
            }
            return baseResultDataValue;
        }

        public EntityList<LBItem> SearchLBItemEntityListByLBSectionItemHQL(string strHqlWhere, string Order, int page, int count, string lbsectionId)
        {
            EntityList<LBItem> el = new EntityList<LBItem>();
            if (!string.IsNullOrEmpty(lbsectionId))
            {
                if (string.IsNullOrEmpty(strHqlWhere))
                {
                    strHqlWhere = "lbsectionitem.LBSection.Id=" + lbsectionId;
                }
                else
                {
                    strHqlWhere = strHqlWhere + " and lbsectionitem.LBSection.Id=" + lbsectionId;
                }
                el = ((IDLBItemDao)this.DBDao).SearchLBItemEntityListByLBSectionItemHQL(strHqlWhere, Order, page, count);
            }
            else
            {
                el = ((IDLBItemDao)this.DBDao).GetListByHQL(strHqlWhere, Order, page, count);
            }
            return el;
        }
        public EntityList<LBItem> SearchNotLBParItemSplitPLBItemListByHQL(string strHqlWhere, string Order, int page, int count)
        {
            EntityList<LBItem> el = new EntityList<LBItem>();
            el = ((IDLBItemDao)this.DBDao).SearchNotLBParItemSplitPLBItemListByHQL(strHqlWhere, Order, page, count);
            return el;
        }
        public EntityList<LBItem> SearchAlreadyLBParItemSplitPLBItemListByHQL(string strHqlWhere, string Order, int page, int count)
        {
            EntityList<LBItem> el = new EntityList<LBItem>();
            el = ((IDLBItemDao)this.DBDao).SearchAlreadyLBParItemSplitPLBItemListByHQL(strHqlWhere, Order, page, count);
            return el;
        }

        #region 医嘱开单项目树
        /// <summary>
        /// 医嘱开单 获得项目树
        /// </summary>
        /// <returns></returns>
        public List<tree> GetItemModelTree()
        {
            List<tree> trees = new List<tree>();
            var lbItems = DBDao.GetListByHQL("GroupType=" + ItemGroupType.组套.Key);
            if (lbItems.Count > 0)
            {
                foreach (var a in lbItems)
                {
                    tree itemMode = new tree();
                    itemMode.text = a.CName;
                    itemMode.expanded = true;
                    itemMode.leaf = false;
                    itemMode.url = "";
                    itemMode.icon = "";
                    itemMode.tid = a.Id.ToString();
                    itemMode.Tree = GetItemsTree(a.Id);
                    if (itemMode.Tree.Count > 0)
                    {
                        trees.Add(itemMode);
                    }
                }
            }
            return trees;
        }

        private List<tree> GetItemsTree(long Id)
        {
            List<tree> trees = new List<tree>();
            IList<LBItemGroup> lbItems = IDLBItemGroupDao.GetListByHQL("LBGroup.Id=" + Id);
            foreach (var a in lbItems)
            {
                if ((a.LBItem.GroupType.ToString() == ItemGroupType.单项.Key || a.LBItem.GroupType.ToString() == ItemGroupType.组合.Key) && a.LBItem.IsOrderItem)
                {
                    tree itemMode = new tree();
                    itemMode.text = a.LBItem.CName;
                    itemMode.expanded = true;
                    itemMode.leaf = false;
                    itemMode.url = "";
                    itemMode.icon = "";
                    itemMode.pid = Id.ToString();
                    itemMode.tid = a.LBItem.Id.ToString();
                    IList<LBSampleItem> lBSampleItems = IDLBSampleItemDao.GetListByHQL("LBItem.Id=" + a.LBItem.Id);
                    OrderItemsTreeParaVO orderItemsTreeParaVO = new OrderItemsTreeParaVO();
                    LBItemTreePara lBItemTreePara = new LBItemTreePara();
                    lBItemTreePara.ItemNo = a.LBItem.ItemNo.ToString();
                    lBItemTreePara.ItemCharge = a.LBItem.ItemCharge.ToString();
                    orderItemsTreeParaVO.LBItem = lBItemTreePara;
                    if (lBSampleItems.Count > 0)
                    {
                        List<LBSampleItemTreePara> lBSampleItemTreeParas = new List<LBSampleItemTreePara>();
                        foreach (var sami in lBSampleItems)
                        {
                            LBSampleItemTreePara entity = new LBSampleItemTreePara();
                            entity.CName = sami.LBSampleType.CName;
                            entity.SampleTypeID = ""+sami.LBSampleType.Id.ToString();
                            lBSampleItemTreeParas.Add(entity);
                        }
                        orderItemsTreeParaVO.LBSampleType = lBSampleItemTreeParas;
                    }
                    itemMode.Para = Newtonsoft.Json.JsonConvert.SerializeObject(orderItemsTreeParaVO);
                    trees.Add(itemMode);
                }
                else if (a.LBItem.GroupType.ToString() == ItemGroupType.组合.Key && !a.LBItem.IsOrderItem)
                {
                    tree itemMode = new tree();
                    itemMode.text = a.LBItem.CName;
                    itemMode.expanded = true;
                    itemMode.leaf = false;
                    itemMode.url = "";
                    itemMode.icon = "";
                    itemMode.pid = Id.ToString();
                    itemMode.tid = a.LBItem.Id.ToString();
                    itemMode.Tree = GetItemsTree(a.LBItem.Id);
                    trees.Add(itemMode);
                }
            }
            return trees;
        }

        #endregion

        #region 项目树，树形结构：组套--组合--单项。各类型之间可嵌套。

        /// <summary>
        /// 根据项目ID字符串获取项目树
        /// </summary>
        /// <param name="strItemID">项目ID字符串</param>
        /// <returns></returns>
        public List<tree> GetItemTreeByItemIDList(string strItemID)
        {
            List<tree> treeItem = new List<tree>();
            if (string.IsNullOrWhiteSpace(strItemID))
                return treeItem;
            long[] arrayItemID = Array.ConvertAll<string, long>(strItemID.Split(','), p => long.Parse(p));
            string strWhere = " lbitem.Id in (" + String.Join(",", arrayItemID) + ")";
            IList<LBItem> listLBItem = this.SearchListByHQL(strWhere);
            if (listLBItem != null && listLBItem.Count > 0)
            {
                foreach (LBItem item in listLBItem)
                {
                    tree itemMode = new tree();
                    itemMode.text = item.CName;
                    itemMode.expanded = true;
                    itemMode.leaf = false;
                    itemMode.url = "";
                    itemMode.icon = "";
                    itemMode.tid = item.Id.ToString();
                    if (item.GroupType.ToString() == ItemGroupType.组套.Key || item.GroupType.ToString() == ItemGroupType.组合.Key)
                        itemMode.Tree = GetChildItemTreeByItemID(item.Id);
                    treeItem.Add(itemMode);
                }
            }
            return treeItem;
        }

        private List<tree> GetChildItemTreeByItemID(long itemID)
        {
            List<tree> treeItem = new List<tree>();
            IList<LBItemGroup> listLBItemGroup = IDLBItemGroupDao.GetListByHQL(" lbitemgroup.LBGroup.Id=" + itemID);
            if (listLBItemGroup != null && listLBItemGroup.Count > 0)
            {
                foreach (LBItemGroup itemGroup in listLBItemGroup)
                {
                    tree itemMode = new tree();
                    itemMode.text = itemGroup.LBItem.CName;
                    itemMode.expanded = true;
                    itemMode.leaf = false;
                    itemMode.url = "";
                    itemMode.icon = "";
                    itemMode.pid = itemID.ToString();
                    itemMode.tid = itemGroup.LBItem.Id.ToString();
                    if (itemGroup.LBItem.GroupType.ToString() == ItemGroupType.组套.Key || itemGroup.LBItem.GroupType.ToString() == ItemGroupType.组合.Key)
                        GetChildItemTreeByItemID(itemGroup.LBItem.Id);
                    treeItem.Add(itemMode);
                }
            }
            return treeItem;
        }

        #endregion

        #region 采样组项目与取单时间分类项目定制服务
        public EntityList<LBItem> LS_UDTO_SearchLBItemBySamplingGroupID(long SamplingGroupID, long SectionID, int page, int limit, string where, string sort)
        {
            EntityList<LBItem> entityList = new EntityList<LBItem>();
            if (SamplingGroupID != 0)
            {
                IList<LBSamplingItem> lBSamplingItems = IDLBSamplingItemDao.GetListByHQL("LBSamplingGroup.Id=" + SamplingGroupID);
                if (lBSamplingItems.Count > 0)
                {
                    List<long> ids = new List<long>();
                    foreach (var item in lBSamplingItems)
                    {
                        ids.Add(item.LBItem.Id);
                    }
                    if (string.IsNullOrEmpty(where))
                    {
                        where = "LBItem.Id not in (" + string.Join(",", ids) + ")";
                    }
                    else
                    {
                        where += " and LBItem.Id not in (" + string.Join(",", ids) + ")";
                    }
                }               
            }
            if (SectionID != 0)
            {
                if (string.IsNullOrEmpty(where))
                {
                    where = "Id=" + SectionID;
                }
                else
                {
                    where += " and Id=" + SectionID;

                }
                EntityList<LBSectionItem> sectionitems = IDLBSectionItemDao.GetListByHQL( where, sort, page, limit);
                entityList.count = sectionitems.count;
                List<LBItem> lBItems = new List<LBItem>();
                if (sectionitems.list.Count() > 0)
                {
                    foreach (var item in sectionitems.list)
                    {
                        LBItem lBItem = new LBItem();
                        lBItem.Id = item.LBItem.Id;
                        lBItem.ItemNo = item.LBItem.ItemNo;
                        lBItem.CName = item.LBItem.CName;
                        lBItem.EName = item.LBItem.EName;
                        lBItem.SName = item.LBItem.SName;
                        lBItem.ValueType = item.LBItem.ValueType;
                        lBItem.RangeAllInfo = item.LBItem.RangeAllInfo;
                        lBItem.ClinicalInfo = item.LBItem.ClinicalInfo;
                        lBItem.UseCode = item.LBItem.UseCode;
                        lBItem.StandCode = item.LBItem.StandCode;
                        lBItem.DeveCode = item.LBItem.DeveCode;
                        lBItem.Shortcode = item.LBItem.Shortcode;
                        lBItem.PinYinZiTou = item.LBItem.PinYinZiTou;
                        lBItem.Comment = item.LBItem.Comment;
                        lBItem.DispOrder = item.LBItem.DispOrder;
                        lBItems.Add(lBItem);
                    }
                }
                entityList.list = lBItems;
            }
            else {
                if (!string.IsNullOrEmpty(where)) 
                {
                    where = where.Replace("LBItem.", "");
                }
                entityList = DBDao.GetListByHQL(where,sort,page,limit);
            }
            return entityList;
        }

        public EntityList<LBItem> LS_UDTO_SearchLBItemByReportDateID(long ReportDateID, long SectionID, int page, int limit, string where, string sort)
        {
            EntityList<LBItem> entityList = new EntityList<LBItem>();
            if (ReportDateID != 0)
            {
                IList<LBReportDateItem> lBReportDateItems = IDLBReportDateItemDao.GetListByHQL("LBReportDate.Id=" + ReportDateID);
                if (lBReportDateItems.Count > 0)
                {
                    List<long> ids = new List<long>();
                    foreach (var item in lBReportDateItems)
                    {
                        ids.Add(item.LBItem.Id);
                    }
                    if (string.IsNullOrEmpty(where))
                    {
                        where = "LBItem.Id not in (" + string.Join(",", ids) + ")";
                    }
                    else
                    {
                        where += " and LBItem.Id not in (" + string.Join(",", ids) + ")";
                    }
                }
            }
            if (SectionID != 0)
            {
                if (string.IsNullOrEmpty(where))
                {
                    where = "Id=" + SectionID;
                }
                else 
                {
                    where += " and Id=" + SectionID;

                }
                EntityList<LBSectionItem> sectionitems = IDLBSectionItemDao.GetListByHQL(where, sort, page, limit);
                entityList.count = sectionitems.count;
                List<LBItem> lBItems = new List<LBItem>();
                if (sectionitems.list.Count() > 0)
                {
                    foreach (var item in sectionitems.list)
                    {
                        LBItem lBItem = new LBItem();
                        lBItem.Id = item.LBItem.Id;
                        lBItem.ItemNo = item.LBItem.ItemNo;
                        lBItem.CName = item.LBItem.CName;
                        lBItem.EName = item.LBItem.EName;
                        lBItem.SName = item.LBItem.SName;
                        lBItem.ValueType = item.LBItem.ValueType;
                        lBItem.RangeAllInfo = item.LBItem.RangeAllInfo;
                        lBItem.ClinicalInfo = item.LBItem.ClinicalInfo;
                        lBItem.UseCode = item.LBItem.UseCode;
                        lBItem.StandCode = item.LBItem.StandCode;
                        lBItem.DeveCode = item.LBItem.DeveCode;
                        lBItem.Shortcode = item.LBItem.Shortcode;
                        lBItem.PinYinZiTou = item.LBItem.PinYinZiTou;
                        lBItem.Comment = item.LBItem.Comment;
                        lBItem.DispOrder = item.LBItem.DispOrder;
                        lBItems.Add(lBItem);
                    }
                }
                entityList.list = lBItems;
            }
            else
            {
                if (!string.IsNullOrEmpty(where))
                {
                    where = where.Replace("LBItem.", "");
                }
                entityList = DBDao.GetListByHQL(where, sort, page, limit);
            }
            return entityList;
        }

        public List<tree> GetModelItemsTree(string tid, List<LBItem> lBItems)
        {
            //List<tree> trees=GetItemsTree(itemID);
            List<tree> trees = new List<tree>();
            foreach (var lbitem in lBItems)
            {
                if (lbitem.GroupType.ToString() == ItemGroupType.组套.Key || (lbitem.GroupType.ToString() == ItemGroupType.组合.Key && !lbitem.IsOrderItem))
                {
                    tree itemTree = new tree();
                    itemTree.text = lbitem.CName;
                    itemTree.expanded = true;
                    itemTree.leaf = false;
                    itemTree.url = "";
                    itemTree.icon = "";
                    itemTree.pid = tid;
                    itemTree.tid = lbitem.Id.ToString();
                    itemTree.Tree = GetItemsTree(lbitem.Id);
                    trees.Add(itemTree);
                }
                else if((lbitem.GroupType.ToString() == ItemGroupType.组合.Key || lbitem.GroupType.ToString() == ItemGroupType.单项.Key) && lbitem.IsOrderItem)
                {
                    tree itemTree = new tree();
                    itemTree.text = lbitem.CName;
                    itemTree.expanded = true;
                    itemTree.leaf = false;
                    itemTree.url = "";
                    itemTree.icon = "";
                    itemTree.pid = tid;
                    itemTree.tid = lbitem.Id.ToString();
                    IList<LBSampleItem> lBSampleItems = IDLBSampleItemDao.GetListByHQL("LBItem.Id=" + lbitem.Id);
                    OrderItemsTreeParaVO orderItemsTreeParaVO = new OrderItemsTreeParaVO();
                    LBItemTreePara lBItemTreePara = new LBItemTreePara();
                    lBItemTreePara.ItemNo = lbitem.ItemNo.ToString();
                    lBItemTreePara.ItemCharge = lbitem.ItemCharge.ToString();
                    orderItemsTreeParaVO.LBItem = lBItemTreePara;
                    if (lBSampleItems.Count > 0)
                    {
                        List<LBSampleItemTreePara> lBSampleItemTreeParas = new List<LBSampleItemTreePara>();
                        foreach (var sami in lBSampleItems)
                        {
                            LBSampleItemTreePara entity = new LBSampleItemTreePara();
                            entity.CName = sami.LBSampleType.CName;
                            entity.SampleTypeID = sami.LBSampleType.Id+"";
                            lBSampleItemTreeParas.Add(entity);
                        }
                        orderItemsTreeParaVO.LBSampleType = lBSampleItemTreeParas;
                    }
                    itemTree.Para = Newtonsoft.Json.JsonConvert.SerializeObject(orderItemsTreeParaVO);
                    trees.Add(itemTree);
                }
            }
            return trees;
        }
        #endregion
    }
}