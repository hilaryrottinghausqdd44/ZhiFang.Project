
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.BLL.Business
{
    /// <summary>
    ///
    /// </summary>
    public class BGMGroupItem : BaseBLL<GMGroupItem>, ZhiFang.Digitlab.IBLL.Business.IBGMGroupItem
    {
        private ZhiFang.Digitlab.IBLL.Business.IBEPEquipItem IBEPEquipItem { get; set; }

        public ZhiFang.Digitlab.IDAO.IDItemAllItemDao IDItemAllItemDao { get; set; }

        public bool JudgeGMGroupItemByItemID(long gmGroupID, long itemID)
        {
            bool tempBool = false;
            //IList<GMGroupItem> listGMGroupItem = this.SearchListByHQL("gmgroupitem.GMGroup.Id=" + gmGroupID.ToString());
            IList<GMGroupItem> listGMGroupItem = ((IDAO.IDGMGroupItemDao)DBDao).JudgeGMGroupItemByItemID(gmGroupID);
            if (listGMGroupItem != null && listGMGroupItem.Count > 0)
            {
                IList<GMGroupItem> tempList = listGMGroupItem.Where(p => (p.ItemAllItem.Id == itemID)).ToList();
                tempBool = (listGMGroupItem != null && listGMGroupItem.Count > 0);
            }
            return tempBool;
        }

        public bool DeleteGMGroupItemAndEquipItem(long groupItemID)
        {
            bool resultBool = false;
            GMGroupItem gmGroupItem = this.Get(groupItemID);
            if (gmGroupItem != null && gmGroupItem.GMGroup != null && gmGroupItem.ItemAllItem != null)
            {
                IList<EPEquipItem> listEPEquipItem = IBEPEquipItem.SearchListByHQL(" epequipitem.GMGroup.Id=" + gmGroupItem.GMGroup.Id.ToString() +
                    " and epequipitem.ItemAllItem.Id=" + gmGroupItem.ItemAllItem.Id.ToString());
                if (listEPEquipItem != null && listEPEquipItem.Count > 0)
                {
                    foreach (EPEquipItem epEquipItem in listEPEquipItem)
                    {
                        IBEPEquipItem.Entity = epEquipItem;
                        IBEPEquipItem.Remove();
                    }
                }
                this.Entity = gmGroupItem;
                resultBool = this.Remove();
            }
            return resultBool;
        }

        public IList<GMGroupItem> JudgeGMGroupItemByItemID(long gmGroupID)
        {
            return ((IDAO.IDGMGroupItemDao)DBDao).JudgeGMGroupItemByItemID(gmGroupID);
        }

        public IList<GMGroupItem> SearchListByGroupId(string GroupId, int page, int limit)
        {
            return ((IDAO.IDGMGroupItemDao)DBDao).SearchListByGroupId(GroupId);
        }

        public EntityList<GMGroupItem> SearchListByGroupId(long GroupId, string sort, int page, int limit)
        {
            return ((IDAO.IDGMGroupItemDao)DBDao).GetListByHQL(" ", page, limit);
        }
        public IList<GMGroupItem> SearchGMGroupItemByHQL(string strHqlWhere, int page, int count)
        {
            return ((IDAO.IDGMGroupItemDao)DBDao).SearchGMGroupItemByHQL(strHqlWhere, page, count);
        }
        /// <summary>
        /// 根传入的小组类型Url获取小组项目(过滤相同检验项目)
        /// </summary>
        /// <param name="gmgroupTypeUrl"></param>
        /// <returns></returns>
        public IList<GMGroupItem> SearchGMGroupItemByGMGroupTypeUrl(string gmgroupTypeUrl)
        {
            return ((IDAO.IDGMGroupItemDao)DBDao).SearchGMGroupItemByGMGroupTypeUrl(gmgroupTypeUrl);
        }
    }
}