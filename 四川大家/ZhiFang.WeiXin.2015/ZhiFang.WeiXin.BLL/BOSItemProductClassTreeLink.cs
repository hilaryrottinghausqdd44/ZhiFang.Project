
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.IBLL;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity.ViewObject.Response;

namespace ZhiFang.WeiXin.BLL
{
    /// <summary>
    ///
    /// </summary>
    public class BOSItemProductClassTreeLink : ZhiFang.BLL.Base.BaseBLL<OSItemProductClassTreeLink>, ZhiFang.WeiXin.IBLL.IBOSItemProductClassTreeLink
    {
        IBBLabTestItem IBBLabTestItem { get; set; }
        IBOSItemProductClassTree IBOSItemProductClassTree { get; set; }
        public EntityList<BLabTestItemVO> SearchBLabTestItemVOByTreeId(int page, int limit, string where, string sort, string areaID, string treeId, bool isSearchChild)
        {
            EntityList<BLabTestItemVO> tempEntityList = new EntityList<BLabTestItemVO>();
            //if (String.IsNullOrEmpty(treeId) && String.IsNullOrEmpty(areaID))
            //    return tempEntityList;
            if (String.IsNullOrEmpty(treeId))
                return tempEntityList;

            
            string treeIdStr = IBOSItemProductClassTree.GetIDStrByMaxLayers(treeId, areaID, isSearchChild, "");
            
            if (!String.IsNullOrEmpty(treeIdStr))
            {
               
                StringBuilder itemIDStr = new StringBuilder();
                IList<OSItemProductClassTreeLink> tempList = this.SearchListByHQL("ItemProductClassTreeID in (" + treeIdStr + ")");
                
                foreach (var tempEntity in tempList)
                {
                    if (!itemIDStr.ToString().Contains(tempEntity.ItemID.ToString()+","))
                    {
                        itemIDStr.Append(tempEntity.ItemID);
                        itemIDStr.Append(",");
                    }
                }
                
                string itemIds = itemIDStr.ToString().TrimEnd(',');
                
                if (!String.IsNullOrEmpty(itemIds))
                {
                    string hqlWhere = "Id in (" + itemIds + ")";
                    if (!String.IsNullOrEmpty(where))
                        hqlWhere = hqlWhere + " and " + where;
                    tempEntityList = IBBLabTestItem.SearchBLabTestItemVOList(page, limit, sort, hqlWhere);
                    
                }
                
            }
            
            if (tempEntityList==null || tempEntityList.list == null)
            {
                
                tempEntityList= new EntityList<BLabTestItemVO>();
                tempEntityList.list = new List<BLabTestItemVO>();
                
            }
           
            return tempEntityList;
        }
        public EntityList<OSItemProductClassTreeLinkVO> SearchOSItemProductClassTreeLinkByTreeId(int page, int limit, string where, string sort, string areaId, string treeId, bool isSearchChild)
        {
            EntityList<OSItemProductClassTreeLinkVO> tempEntityVOList = new EntityList<OSItemProductClassTreeLinkVO>();

            if (String.IsNullOrEmpty(treeId) && String.IsNullOrEmpty(areaId))
                return tempEntityVOList;
            if (String.IsNullOrEmpty(treeId) && !String.IsNullOrEmpty(areaId))
                treeId = "0";
            string treeIdStr = IBOSItemProductClassTree.GetIDStrByMaxLayers(treeId, areaId, isSearchChild, "");
            if (!String.IsNullOrEmpty(treeIdStr))
            {
                EntityList<OSItemProductClassTreeLink> tempEntityList = new EntityList<OSItemProductClassTreeLink>();
                tempEntityList = this.SearchListByHQL(" ItemProductClassTreeID in (" + treeIdStr + ")", sort, page, limit);
                //ZhiFang.Common.Log.Log.Debug("SearchOSItemProductClassTreeLinkByTreeId:" + tempEntityList.count);
                tempEntityVOList.count = tempEntityList.count;
                tempEntityVOList.list = TransListVO(tempEntityList.list);
            }
            if (tempEntityVOList.list == null) tempEntityVOList.list = new List<OSItemProductClassTreeLinkVO>();

            return tempEntityVOList;
        }
        private IList<OSItemProductClassTreeLinkVO> TransListVO(IList<OSItemProductClassTreeLink> tempList)
        {
            IList<OSItemProductClassTreeLinkVO> tempEntityVOList = new List<OSItemProductClassTreeLinkVO>();
            if (tempList == null) return tempEntityVOList;

            #region 找出项目分类名称及项目名称
            StringBuilder pTreeIDStr = new StringBuilder();
            StringBuilder itemIDStr = new StringBuilder();
            foreach (var tempEntity in tempList)
            {
                if (!pTreeIDStr.ToString().Contains(tempEntity.ItemProductClassTreeID.ToString()))
                {
                    pTreeIDStr.Append(tempEntity.ItemProductClassTreeID);
                    pTreeIDStr.Append(",");
                }
                if (!itemIDStr.ToString().Contains(tempEntity.ItemID.ToString()))
                {
                    itemIDStr.Append(tempEntity.ItemID);
                    itemIDStr.Append(",");
                }
            }
            IList<OSItemProductClassTree> tempPTreeList = new List<OSItemProductClassTree>();
            string pTreeIds = pTreeIDStr.ToString().TrimEnd(',');
            if (!String.IsNullOrEmpty(pTreeIds))
            {
                tempPTreeList = IBOSItemProductClassTree.SearchListByHQL(" Id in (" + pTreeIds + ")");
            }
            IList<BLabTestItem> tempTestItemList = new List<BLabTestItem>();
            string itemIds = itemIDStr.ToString().TrimEnd(',');
            if (!String.IsNullOrEmpty(itemIds))
            {
                string hqlWhere = "Id in (" + itemIds + ")";
                tempTestItemList = IBBLabTestItem.SearchListByHQL(hqlWhere);
            }
            #endregion

            #region 匹配项目分类名称及项目名称
            foreach (var tempEntity in tempList)
            {
                OSItemProductClassTreeLinkVO vo = TransVO(tempEntity);
                var treeList = tempPTreeList.Where(p => p.Id == tempEntity.ItemProductClassTreeID).ToList();
                if (treeList.Count > 0)
                {
                    vo.ItemProductClassTreeCName = treeList[0].CName;
                }
                var testItemList = tempTestItemList.Where(p => p.Id == tempEntity.ItemID).ToList();
                if (testItemList.Count > 0)
                {
                    vo.ItemCName = testItemList[0].CName;
                }
                tempEntityVOList.Add(vo);
            }
            #endregion
            return tempEntityVOList;
        }
        OSItemProductClassTreeLinkVO TransVO(OSItemProductClassTreeLink entity)
        {
            OSItemProductClassTreeLinkVO vo = new OSItemProductClassTreeLinkVO();
            vo.Id = entity.Id;
            vo.LabID = entity.LabID;
            vo.DataAddTime = entity.DataAddTime;
            vo.AreaID = entity.AreaID;
            vo.ItemProductClassTreeID = entity.ItemProductClassTreeID;
            vo.ItemID = entity.ItemID;
            vo.ItemNo = entity.ItemNo;
            vo.DispOrder = entity.DispOrder;
            vo.IsUse = entity.IsUse;
            vo.CreatorID = entity.CreatorID;
            vo.CreatorName = entity.CreatorName;
            vo.DataUpdateTime = entity.DataUpdateTime;
            vo.DataTimeStamp = entity.DataTimeStamp;
            return vo;
        }
    }
}