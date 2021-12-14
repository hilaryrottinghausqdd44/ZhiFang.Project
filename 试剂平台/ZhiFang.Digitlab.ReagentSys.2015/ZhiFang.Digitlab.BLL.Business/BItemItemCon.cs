using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.BLL.Business
{
    public class BItemItemCon : ZhiFang.Digitlab.BLL.BaseBLL<ItemItemCon>, ZhiFang.Digitlab.IBLL.Business.IBItemItemCon
    {
        /// <summary>
        /// 根据组项目ID查询包含该项目的关系树
        /// </summary>
        /// <param name="longItemID">项目ID</param>
        /// <returns></returns>
        public BaseResultTree SearchItemTreeByItemID(long longItemID)
        {
            return SearchItemParentTreeByItemID(longItemID);
        }
        /// <summary>
        /// 根据组项目ID查询包含该项目的直接父项目
        /// </summary>
        /// <param name="longItemID">项目ID</param>
        /// <returns></returns>
        public BaseResultTree SearchItemParentTreeByItemID(long longItemID)
        {
            BaseResultTree baseResultTree = new BaseResultTree();

            EntityList<ItemItemCon> entityList = this.SearchListByHQL(" itemitemcon.ItemAllItem.Id=" + longItemID.ToString(), 0, 0);
            List<tree> listTree = new List<tree>();
            if (entityList != null && entityList.count > 0)
            {
                tree childTree = new tree();
                childTree.text = entityList.list[0].ItemAllItem.CName;
                childTree.tid = entityList.list[0].ItemAllItem.Id.ToString();
                childTree.Tree = SearchItemChildTreeByItemID(longItemID);
                childTree.expanded = (childTree.Tree.Count > 0);
                childTree.leaf = (childTree.Tree.Count <= 0);               
                List<tree> listChildTree = new List<tree>();
                listChildTree.Add(childTree);
                foreach (var tempEntity in entityList.list)
                {
                    tree ParentTree = new tree();
                    ParentTree.text = entityList.list[0].Group.CName;
                    ParentTree.tid = entityList.list[0].Group.Id.ToString();
                    ParentTree.expanded = true;
                    ParentTree.leaf = false;
                    ParentTree.Tree = listChildTree;
                    listTree.Add(ParentTree);
                }
            }
            baseResultTree.Tree = listTree;
            return baseResultTree;       
        }

        /// <summary>
        /// 根据组项目ID查询包含该项目的直接子项目
        /// </summary>
        /// <param name="longItemID">项目ID</param>
        /// <returns></returns>
        public List<tree> SearchItemChildTreeByItemID(long longItemID)
        {
            EntityList<ItemItemCon> entityList = this.SearchListByHQL(" itemitemcon.Group.Id=" + longItemID.ToString(), 0, 0);
            List<tree> listTree = new List<tree>();
            foreach (var tempEntity in entityList.list)
            {
                tree tree = new tree();
                tree.text = tempEntity.ItemAllItem.CName;
                tree.tid = tempEntity.ItemAllItem.Id.ToString();
                listTree.Add(tree);
            }
            return listTree;
        }

        public IList<ItemAllItem> SearchChildItemByGroupItemID(long longGroupItemID)
        {
            IList<ItemAllItem> tempItemAllItemList = new List<ItemAllItem>();
            EntityList<ItemItemCon> tempItemItemConList = this.SearchListByHQL(" itemitemcon.Group.Id=" + longGroupItemID.ToString(), 0, 0);
            foreach (ItemItemCon tempItemItemCon in tempItemItemConList.list)
            {
                IList<ItemAllItem> tempList = SearchChildItemByGroupItemID(tempItemItemCon.ItemAllItem.Id);
                if (tempList.Count == 0)
                    tempItemAllItemList.Add(tempItemItemCon.ItemAllItem);
                else
                    tempItemAllItemList = tempItemAllItemList.Concat(tempList).ToList();
            }
            return tempItemAllItemList;
        }
    }
}
